
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>TwitterActor class</summary>

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data.Twitter
{
	[CSVContract(Name = "Tweet author")] 
	[DataContract(Name = "actor")]
	public class TwitterActor : ActorBase
	{
		[DataMember(Name = "image")]
		public string Image
		{
			get;
			set;
		}

		[CSVMember(Name = "BIO", ValuePath = "Summary")]
		[DataMember(Name = "summary")]
		public string Summary
		{
			get;
			set;
		}

		[DataMember(Name = "postedTime")]
		private string _postedTimeString;

		[CSVMember(Name = "Account created", ValuePath = "PostedTime")]
		public DateTime PostedTime
		{
			get
			{
				DateTime time = DateTime.Now;
				DateTime.TryParse(_postedTimeString, out time);
				return time;
			}
		}

		[DataMember(Name = "links")]
		public List<Link> Links
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

		[DataMember(Name = "utcOffset")]
		public int? UtcOffset
		{
			get;
			set;
		}

		[DataMember(Name = "prefferedUsername")]
		public string PrefferedUsername
		{
			get;
			set;
		}

		[DataMember(Name = "languages")]
		public List<string> Languages
		{
			get;
			set;
		}

		#region GNIP Extensions

		[DataMember(Name = "twitterTimeZone")]
		public string exTwitterTimeZone
		{
			get;
			set;
		}

		[CSVMember(Name = "Friends count", ValuePath = "exFriendsCount")]
		[DataMember(Name = "friendsCount")]
		public int exFriendsCount
		{
			get;
			set;
		}

		[CSVMember(Name = "Followers count", ValuePath = "exFollowersCount")]
		[DataMember(Name = "followersCount")]
		public int exFollowersCount
		{
			get;
			set;
		}

		[DataMember(Name = "listedCount")]
		public int exListedCount
		{
			get;
			set;
		}

		[DataMember(Name = "statusesCount")]
		public int exStatusesCount
		{
			get;
			set;
		}

		[DataMember(Name = "favoritesCount")]
		public int exFavoritesCount
		{
			get;
			set;
		}

		[CSVMember(Name = "Is verified", ValuePath = "Verified")]
		[DataMember(Name = "verified")]
		public bool Verified
		{
			get;
			set;
		}

		#endregion
	}
}
