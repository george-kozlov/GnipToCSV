
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>TwitterActor class</summary>

using System.Runtime.Serialization;
using System.Collections.Generic;

using Gnip.Data.Common;

namespace Gnip.Data.Twitter
{
	[CSVContract(Name = "Tweet entities")]
	[DataContract(Name = "entities")]
	public class TwitterEntities
	{
		[CSVMember(Name = "Hashtags", ValuePath = "Hashtags")]
		[DataMember(Name = "hashtags")]
		public List<TwitterHashtag> Hashtags
		{
			get;
			set;
		}

		[CSVMember(Name = "Mentions", ValuePath = "Mentions")]
		[DataMember(Name = "user_mentions")]
		public List<TwitterMention> Mentions
		{
			get;
			set;
		}
	}
}
