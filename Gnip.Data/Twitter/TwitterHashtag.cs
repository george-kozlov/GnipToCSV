
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>TwitterActor class</summary>

using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data.Twitter
{
	[DataContract(Name = "hashtag")]
	public class TwitterHashtag
	{
		[DataMember(Name = "text")]
		private string _text = string.Empty;

		public string Text
		{
			get
			{
				return string.Format("#{0}", _text);
			}
		}

		public override string ToString()
		{
			return Text;
		}
	}
}
