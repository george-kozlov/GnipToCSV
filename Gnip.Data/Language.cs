
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/24/2013</date>
// <summary>Location class</summary>

using System.Runtime.Serialization;

namespace Gnip.Data
{
	[DataContract(Name = "language")]
	public class Language
	{
		[DataMember(Name = "value")]
		public string Value
		{
			get;
			set;
		}
	}
}
