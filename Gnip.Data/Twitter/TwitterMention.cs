
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>TwitterActor class</summary>

using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data.Twitter
{
	[DataContract(Name = "mention")]
	public class TwitterMention
	{
		[DataMember(Name = "screen_name")]
		private string _screenName = string.Empty;

		public string ScreenName
		{
			get
			{
				return string.Format("@{0}", _screenName);
			}
		}

		[DataMember(Name = "name")]
		public string Name
		{
			get;
			set;
		}

		[DataMember(Name = "id_str")]
		public string Id
		{
			get;
			set;
		}

		public override string ToString()
		{
			return ScreenName;
		}
	}
}
