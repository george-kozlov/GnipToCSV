
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>Generator class</summary>

using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
	[CSVContract(Name = "Data source")] 
	[DataContract(Name = "generator")]
	public class Generator
	{
		[CSVMember(Name = "Display name", ValuePath = "DisplayName")]
		[DataMember(Name = "displayName")]
		public string DisplayName
		{
			get;
			set;
		}

		[CSVMember(Name = "link", ValuePath = "link")]
		[DataMember(Name = "link")]
		public string Link
		{
			get;
			set;
		}
	}
}
