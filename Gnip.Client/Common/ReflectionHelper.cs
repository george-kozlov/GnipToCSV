
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>ReflectionHelper and AttributeInfo classes</summary>

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Collections;

namespace Gnip.Client.Common
{
	#region AttributeInfo type

	public class AttributeInfo<T>
	{
		public AttributeInfo()
		{
		}

		public T Attribute
		{
			get;
			set;
		}

		public Type MemberType
		{
			get;
			set;
		}

		public string MemberName
		{
			get;
			set;
		}
	}

	#endregion

	public static class ReflectionHelper
	{
		#region Static Methods

		/// <summary>
		/// Returns type name from full (type, assembly) description
		/// </summary>
		/// <param name="description">Type, Assembly Description</param>
		/// <returns>Type name</returns>
		public static string GetTypeNameFromDescription(string description)
		{
			string sType = string.Empty;
			if (description.Contains(","))
				sType = description.Substring(0, description.IndexOf(","));
			else
				sType = description;

			return sType.Trim();
		}

		/// <summary>
		/// Returns assembly name from full (type, assembly) description
		/// </summary>
		/// <param name="description">Type, Assembly Description</param>
		/// <returns>Assembly name</returns>
		public static string GetAsmNamefromDescription(string description)
		{
			string sAsm = string.Empty;
			if (description.Contains(","))
				sAsm = description.Substring(description.IndexOf(",") + 1);
			else
				sAsm = description;

			return sAsm.Trim();
		}

		/// <summary>
		/// Returns first Attribute of the specified type from Type declaration
		/// </summary>
		/// <typeparam name="T">Attribute type</typeparam>
		/// <param name="typeName">Type, Assembly Description</param>
		/// <returns>Attribute if exists or null</returns>
		public static T GetTypeAttribute<T>(string typeName) where T : class
		{
			Type type = Type.GetType(typeName, false, true);
			if (type != null)
			{
				object[] attributes = type.GetCustomAttributes(typeof(T), false);
				if (attributes != null && attributes.Length > 0)
					return (T)attributes[0];
			}

			return null;
		}

		/// <summary>
		/// Returns first Attribute of the specified type from Type declaration
		/// </summary>
		/// <typeparam name="T">Attribute type</typeparam>
		/// <param name="type">Type, Assembly Description</param>
		/// <returns>Attribute if exists or null</returns>
		public static T GetTypeAttribute<T>(Type type) where T : class
		{
			if (type != null)
			{
				object[] attributes = type.GetCustomAttributes(typeof(T), false);
				if (attributes != null && attributes.Length > 0)
					return (T)attributes[0];
			}

			return null;
		}

		/// <summary>
		/// Returns all Attributes of the specified type from Type declaration
		/// </summary>
		/// <typeparam name="T">Attribute type</typeparam>
		/// <param name="type">Type</param>
		/// <returns>Attributes list if exists or null</returns>
		public static IList<T> GetTypeAttributes<T>(Type type) where T : class
		{
			IList<T> tList = new List<T>();

			if (type != null)
			{
				object[] attributes = type.GetCustomAttributes(typeof(T), false);
				if (attributes != null && attributes.Length > 0)
					foreach (T attr in attributes)
						tList.Add(attr);

				return tList;
			}

			return null;
		}

		/// <summary>
		/// Returns all Attributes of the specified type applied to the type members
		/// </summary>
		/// <typeparam name="T">Attribute type</typeparam>
		/// <param name="type">Type</param>
		/// <returns>Attributes list if exists or null</returns>
		public static IList<T> GetTypeMembersAttributes<T>(Type type) where T : class
		{
			List<T> tList = new List<T>();

			if (type != null)
			{
				MemberInfo[] members = type.GetMembers();

				foreach (MemberInfo memb in members)
				{
					object[] attributes = memb.GetCustomAttributes(typeof(T), false);
					if (attributes != null && attributes.Length > 0)
						tList.AddRange((IEnumerable<T>)attributes);
				}

				return tList;
			}

			return null;
		}

		public static List<AttributeInfo<T>> GetTypeMembersAttributesInfo<T>(Type type) where T : class
		{
			List<AttributeInfo<T>> results = new List<AttributeInfo<T>>();

			if (type != null)
			{
				MemberInfo[] members = type.GetMembers();

				foreach (MemberInfo memb in members)
				{
					object[] attributes = memb.GetCustomAttributes(typeof(T), false);
					if (attributes != null && attributes.Length > 0)
					{
						foreach (T attr in attributes)
						{
							if (memb.MemberType == MemberTypes.Field)
								results.Add(new AttributeInfo<T>()
								{
									Attribute = attr,
									MemberType = ((FieldInfo)memb).FieldType,
									MemberName = ((FieldInfo)memb).Name
								});

							if (memb.MemberType == MemberTypes.Property)
								results.Add(new AttributeInfo<T>()
								{
									Attribute = attr,
									MemberType = ((PropertyInfo)memb).PropertyType,
									MemberName = ((PropertyInfo)memb).Name
								});
						}
					}
				}
			}

			return results;
		}

		/// <summary>
		/// Creates default application domain
		/// </summary>
		/// <param name="domainName">AppDomain name</param>
		/// <returns>Application domain</returns>
		public static AppDomain CreateDefaultAppDomain(string domainName)
		{
			Evidence evidence = new Evidence(AppDomain.CurrentDomain.Evidence);

			AppDomainSetup setup = new AppDomainSetup();
			setup.ApplicationName = "RSS Data Hub Client";
			setup.ShadowCopyFiles = "true";

			return AppDomain.CreateDomain(domainName, evidence, setup);
		}

		/// <summary>
		/// Unloads cpecified application domain
		/// </summary>
		/// <param name="domain">AppDomain to be unloaded</param>
		public static void UnloadAppDomain(AppDomain domain)
		{
			if (domain == null)
				return;

			System.Threading.ThreadPool.QueueUserWorkItem(delegate(object obj)
			{
				AppDomain unlDomain = (AppDomain)obj;

				try
				{
					AppDomain.Unload(unlDomain);
				}
				catch (CannotUnloadAppDomainException ex)
				{
                    System.Diagnostics.Debug.WriteLine(ex.Message);
					UnloadAppDomain(unlDomain);
				}

			}, domain);
		}

		/// <summary>
		/// Gets all types from assemblies loaded into current AppDomain which marked by specified attribute
		/// </summary>
		/// <typeparam name="T">Attribute type</typeparam>
		/// <returns>Types list</returns>
		public static IEnumerable<Type> GetTypesByAttribute<T>() where T : class
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			IList<Type> rTypes = new List<Type>();

			foreach (Assembly asm in assemblies)
			{
				Type[] types = asm.GetTypes();
				foreach (Type t in types)
				{
					object[] attributes = t.GetCustomAttributes(typeof(T), false);
					if (attributes != null && attributes.Length > 0)
						rTypes.Add(t);
				}
			}

			return rTypes;
		}

		/// <summary>
		/// Gets Type instance from Type description
		/// </summary>
		/// <param name="typeDesc">Type description</param>
		/// <returns>Instance of the Type</returns>
		public static Type GetTypeFromDescription(string typeDesc)
		{
			return Type.GetType(typeDesc);
		}

		/// <summary>
		/// Creates an instance of type and cast to T type
		/// </summary>
		/// <typeparam name="T">Type to instance should be custed</typeparam>
		/// <param name="type">Type instance of should be created</param>
		/// <returns>Instance of created and custed type</returns>
		public static T CreateAndCastInstance<T>(Type type)
		{
			T instance = (T)Activator.CreateInstance(type);
			return instance;
		}

        /// <summary>
        /// Creates an instance of type and cast to T type
        /// </summary>
        /// <typeparam name="T">Type to instance should be custed</typeparam>
        /// <param name="type">Type instance of should be created</param>
        /// <param name="args">Array of constructor parameters in case when not default constructor should be called</param>
        /// <returns>Instance of created and custed type</returns>
        public static object CreateAndCastInstance(Type type, params object[] args)
        {
            object instance = Activator.CreateInstance(type, args);
            return instance;
        }

		/// <summary>
		/// Creates an instance of type and cast to T type
		/// </summary>
		/// <typeparam name="T">Type to instance should be custed</typeparam>
		/// <param name="type">Type instance of should be created</param>
		/// <param name="args">Array of constructor parameters in case when not default constructor should be called</param>
		/// <returns>Instance of created and custed type</returns>
		public static T CreateAndCastInstance<T>(Type type, params object[] args)
		{
			T instance = (T)Activator.CreateInstance(type, args);
			return instance;
		}

		/// <summary>
		/// Check whether instance can be custed to type Type
		/// </summary>
		/// <param name="type">Type to instance should be custed</param>
		/// <param name="instance">Instance that should be custed to type Type</param>
		/// <returns>true if can and vice versa</returns>
		public static bool IsValidCast(Type type, object instance)
		{
			if (instance == null)
				return true;

			if (instance.GetType() == type)
				return true;

			if (instance.GetType().IsSubclassOf(type))
				return true;

			if (type.IsInterface)
			{
				Type[] interfaces = instance.GetType().GetInterfaces();
				foreach (Type iType in interfaces)
					if (iType == type)
						return true;
			}

			return false;
		}

		/// <summary>
		/// Gets the resource stream by name from loaded assemblies
		/// </summary>
		/// <param name="resName"> Resource name</param>
		/// <returns>Stream of resource file</returns>
		public static Stream GetResourceStream(string resName)
		{
			AppDomain domain = AppDomain.CurrentDomain;
			Assembly[] assemblies = domain.GetAssemblies();

			foreach (Assembly asm in assemblies)
			{
				string[] resources = asm.GetManifestResourceNames();
				foreach (string res in resources)
					if (res.Contains(resName))
						return asm.GetManifestResourceStream(res);
			}

			return null;
		}

		/// <summary>
		/// Creates an instance of the generic type with specific generic and constructor parameters
		/// </summary>
		/// <param name="genericType">Type of generic class to be created</param>
		/// <param name="genericParams">List of generic parameters</param>
		/// <param name="cnstrParams">List of constructor parameters</param>
		/// <returns></returns>
		public static object CreateGenericInstance(Type genericType, Type[] genericParams, object[] cnstrParams)
		{
			Type type = genericType.MakeGenericType(genericParams);
			if (type != null)
			{
				object instance = Activator.CreateInstance(type, cnstrParams);

				return instance;
			}

			return null;
		}

		/// <summary>
		/// Gets property value by property name from object instance
		/// </summary>
		/// <param name="propName">Name of the property</param>
		/// <param name="obj">Gets property value by property name from object instance</param>
		/// <param name="publicOnly">Identifies whether only public properties will be enumerated
		/// while looking for desired property value</param>
		/// <returns>Property value</returns>
		public static object GetPropertyValue(string propName, object obj, bool publicOnly = false)
		{
			if (obj == null)
				return null;

			Type objType = obj.GetType();
			BindingFlags flags = BindingFlags.Static | BindingFlags.Instance |
				BindingFlags.IgnoreCase | BindingFlags.Public;
			if (!publicOnly)
				flags |= BindingFlags.NonPublic;

			PropertyInfo propInfo = objType.GetProperty(propName, flags);
			if (propInfo != null)
				return propInfo.GetValue(obj, null);
			else
				return null;
		}

		/// <summary>
		/// Examines whether provided object is enumerable or not
		/// </summary>
		/// <param name="obj">Object that need to be examined</param>
		/// <returns></returns>
		public static bool IsEnumerable(object obj)
		{
			if (obj.GetType().IsClass)
				return obj is IEnumerable;
			else
				return false;
		}

		/// <summary>
		/// Gets value of nested property by the property path from object instance
		/// </summary>
		/// <param name="path">Path of the nested property</param>
		/// <param name="obj">Gets property value by property name from object instance</param>
		/// <param name="publicOnly">Identifies whether only public properties will be enumerated
		/// while looking for desired property value</param>
		/// <returns>Property value</returns>
		public static object GetNestedPropertyValue(string path, object obj, bool publicOnly = false)
		{
			if (!string.IsNullOrEmpty(path) && !path.Contains('.'))
				return GetPropertyValue(path, obj, publicOnly);

			string[] properties = path.Split('.');
			object iterative = obj;

			foreach (string prop in properties)
				iterative = GetPropertyValue(prop, iterative);

			return iterative;
		}

		/// <summary>
		/// Gets type of generic argument by its index
		/// </summary>
		/// <param name="type">Type of the object which generic argument should be extracted</param>
		/// <param name="index">Index of generic argument.
		/// If there is only one generic argument in desired type this argument will be returned
		/// not depending from this index value. This index should starts from 0.</param>
		/// <returns>Type of generic argument</returns>
		public static Type GetGenericArgumentType(Type type, int index)
		{
			Type[] genTypes = type.GetGenericArguments();
			if (genTypes != null && genTypes.Length == 0)
				return null;

			if (index >= genTypes.Length)
				return genTypes.Last();

			return genTypes[index];
		}

		/// <summary>
		/// Invokes method by its name
		/// </summary>
		/// <param name="instance">Instance of the object whose method should be invoked</param>
		/// <param name="methodName">Method name</param>
		/// <param name="parameters">Parameters set that should be put while method invoking</param>
		/// <returns>Method invokation result</returns>
		public static object InvokeMethod(object instance, string methodName, object[] parameters, bool publicOnly)
		{
			if (instance == null)
				return null;

			Type type = instance.GetType();
			BindingFlags flags = BindingFlags.Static | BindingFlags.Instance |
				BindingFlags.IgnoreCase | BindingFlags.Public;
			if (!publicOnly)
				flags |= BindingFlags.NonPublic;

			MethodInfo mInfo = type.GetMethod(methodName, flags);
			if (mInfo == null)
				return null;

			return mInfo.Invoke(instance, parameters);
		}

		/// <summary>
		/// Invokes generic method by its name
		/// </summary>
		/// <param name="instance">Instance of the object whose method should be invoked</param>
		/// <param name="methodName">Generic method name</param>
		/// <param name="genericParams">Generic types set that should be put while method invoking</param>
		/// <param name="parameters">Parameters set that should be put while method invoking</param>
		/// <returns>Method invokation result</returns>
		public static object InvokeGenericMethod(object instance, string methodName, Type[] genericParams,
			object[] parameters, bool publicOnly)
		{
			if (instance == null)
				return null;

			Type type = instance.GetType();
			BindingFlags flags = BindingFlags.Static | BindingFlags.Instance |
				BindingFlags.IgnoreCase | BindingFlags.Public;
			if (!publicOnly)
				flags |= BindingFlags.NonPublic;

			MethodInfo mInfo = type.GetMethod(methodName, flags);
			if (mInfo == null)
				return null;

			MethodInfo genInfo = mInfo.MakeGenericMethod(genericParams);
			if (genInfo == null)
				return null;

			return genInfo.Invoke(instance, parameters);
		}

		#endregion
	}
}
