
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>Link class</summary>

using System.Runtime.Serialization;

namespace Gnip.Data
{
	[DataContract(Name = "link")]
	public class Link
	{
		[DataMember(Name = "rel")]
		public string RelevantTo
		{
			get;
			set;
		}

		[DataMember(Name = "href")]
		public string Url
		{
			get;
			set;
		}
	}
}
