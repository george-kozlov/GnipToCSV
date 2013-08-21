
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>Url class</summary>

using System.Runtime.Serialization;

namespace Gnip.Data
{
	[DataContract(Name = "url")]
	public class Url
	{
		[DataMember(Name = "url")]
		public string URL
		{
			get;
			set;
		}

		[DataMember(Name = "expanded_url")]
		public string ExpandedURL
		{
			get;
			set;
		}

		public override string ToString()
		{
			return URL;
		}
	}
}
