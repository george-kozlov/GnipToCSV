
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>Location class</summary>

using System.Runtime.Serialization;

namespace Gnip.Data
{
	[DataContract(Name = "location")]
	public class Location
	{
		[DataMember(Name = "objectType")]
		public string ObjectType
		{
			get;
			set;
		}

		[DataMember(Name = "displayName")]
		public string DisplayName
		{
			get;
			set;
		}
	}
}
