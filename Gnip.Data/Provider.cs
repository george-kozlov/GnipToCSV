
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>Provider class</summary>

using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
	[CSVContract(Name = "Data provider")] 
	[DataContract(Name = "provider")]
	public class Provider
	{
		[DataMember(Name = "objectType")]
		public string ObjectType
		{
			get;
			set;
		}

		[CSVMember(Name = "Display name", ValuePath = "DisplayName")]
		[DataMember(Name = "displayName")]
		public string DisplayName
		{
			get;
			set;
		}

		[CSVMember(Name = "Link", ValuePath = "Link")]
		[DataMember(Name = "link")]
		public string Link
		{
			get;
			set;
		}
	}
}
