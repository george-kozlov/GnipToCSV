
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>DynamicActivityBase class</summary>

using System;
using System.Linq;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using System.Runtime.CompilerServices;

using Gnip.Data.Common;

namespace Gnip.Data
{
    [CSVContract(Name = "Base Gnip Activity")]
    public class DynamicActivityBase : DynamicObject
    {
        private DynamicObject _dynamicObj;

        public DynamicActivityBase(DynamicObject dynamicObj)
        {
            _dynamicObj = dynamicObj;
        }

        [CSVMember(Name = "Post ID", ValuePath = "Id")]
        public virtual string Id
        {
            get
            {
                return GetValueOrDefault<string>("id", string.Empty);
            }
        }

        [CSVMember(Name = "Verb", ValuePath = "Verb")]
        public virtual string Verb
        {
            get
            {
                return GetValueOrDefault<string>("verb", string.Empty);
            }
        }

        #region Overrides

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dynamicObj.TryGetMember(binder, out result);
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            return _dynamicObj.TryGetIndex(binder, indexes, out result);
        }

        #endregion

        #region Public methods

        public T GetValueOrDefault<T>(string property, T def)
        {

            object result = null;
            IEnumerable<string> names = _dynamicObj.GetDynamicMemberNames();

            if (!names.Contains(property))
                return def;

            try
            {
                CallSiteBinder binder = Binder.GetMember(CSharpBinderFlags.None, property, typeof(DynamicActivityBase), new[] {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
                });

                if (!_dynamicObj.TryGetMember(binder as GetMemberBinder, out result))
                    throw new ArgumentException(string.Format("Exception while accessing object property {0} of {1} path.", property));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception while accessing nested property: {0}", ex.Message);
                return def;
            }

            return (T)result;
        }

        public T GetNestedValueOrDefault<T>(string nestedProperty, T def)
        {
            if (!string.IsNullOrEmpty(nestedProperty) && !nestedProperty.Contains('.'))
                return GetValueOrDefault<T>(nestedProperty, def);

            string[] properties = nestedProperty.Split('.');
            DynamicObject iterative = _dynamicObj;
            object rersult = null;

            foreach (string prop in properties)
            {
                try
                {
                    if (prop.Contains("[") && prop.Contains("]"))
                    {
                        string cleanProp = prop.Replace("[", string.Empty).Replace("]", string.Empty);
                        if (string.IsNullOrEmpty(cleanProp))
                        {
                            rersult = iterative.ToString();
                            break;
                        }

                        CallSiteBinder binder = Binder.GetIndex(CSharpBinderFlags.None, typeof(DynamicActivityBase), new[] {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                        });

                        int index = -1;
                        if (int.TryParse(cleanProp, out index))
                        {
                            if (!iterative.TryGetIndex(binder as GetIndexBinder, new object[] { index }, out rersult))
                                throw new ArgumentException(string.Format("Exception while accessing indexed property {0} of {1} path.", prop, nestedProperty));
                        }
                        else if (!iterative.TryGetIndex(binder as GetIndexBinder, new object[] { cleanProp }, out rersult))
                                throw new ArgumentException(string.Format("Exception while accessing indexed property {0} of {1} path.", prop, nestedProperty));
                    }
                    else
                    {
                        CallSiteBinder binder = Binder.GetMember(CSharpBinderFlags.None, prop, typeof(DynamicActivityBase), new[] {
                            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
                        });

                        if (!iterative.TryGetMember(binder as GetMemberBinder, out rersult))
                            throw new ArgumentException(string.Format("Exception while accessing indexed property {0} of {1} path.", prop, nestedProperty));
                    }

                    if (rersult is DynamicObject)
                        iterative = rersult as DynamicObject;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }

            return (T)rersult;
        }

        #endregion
    }
}
