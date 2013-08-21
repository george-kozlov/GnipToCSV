
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>TwitterActivity class</summary>

using System.Collections.Generic;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data.Twitter
{
	[CSVContract(Name = "Twitter status")] 
	[DataContract(Name = "activity")]
	public class TwitterActivity : ActivityBase
	{
		[CSVMember(Name = "Author")]
		[DataMember(Name = "actor")]
		public TwitterActor Actor
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

		[CSVMember(Name = "Message", ValuePath = "Body", Default = true)]
		[DataMember(Name = "body")]
		public string Body
		{
			get;
			set;
		}

		[CSVMember(Name = "Location", ValuePath = "Location.DisplayName")]
		[DataMember(Name = "location")]
		public Location Location
		{
			get;
			set;
		}

		[CSVMember(Name = "Entities")]
		[DataMember(Name = "twitter_entities")]
		public TwitterEntities Entities
		{
			get;
			set;
		}
	}
}
