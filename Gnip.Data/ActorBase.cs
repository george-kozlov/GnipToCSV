
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>ActorBase class class</summary>

using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
	[CSVContract(Name = "Author")] 
	[DataContract(Name = "actor")]
	public class ActorBase
	{
		[DataMember(Name = "id")]
		public string Id
		{
			get;
			set;
		}

		[DataMember(Name = "objectType")]
		public string ObjectType
		{
			get;
			set;
		}

		[DataMember(Name = "link")]
		public string Link
		{
			get;
			set;
		}

		[CSVMember(Name = "Display name", ValuePath = "DisplayName", Default = true)]
		[DataMember(Name = "displayName")]
		public string DisplayName
		{
			get;
			set;
		}
	}
}
