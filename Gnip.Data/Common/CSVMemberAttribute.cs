
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>CSVMember and CSVContract attributes</summary>

using System;

namespace Gnip.Data.Common
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class CSVContractAttribute : Attribute
	{
		public CSVContractAttribute()
		{
		}

		public string Name
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}
	}

	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	public class CSVMemberAttribute : Attribute
	{
		public CSVMemberAttribute()
		{
			Default = false;
		}

		public string Name
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string ValuePath
		{
			get;
			set;
		}

		public bool Default
		{
			get;
			set;
		}
	}
}
