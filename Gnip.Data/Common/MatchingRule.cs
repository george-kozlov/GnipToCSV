
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>MatchingRule class</summary>

using System.Runtime.Serialization;

namespace Gnip.Data
{
	[DataContract(Name = "matching_rule")]
	public class MatchingRule
	{
		[DataMember(Name = "value")]
		public string Rule
		{
			get;
			set;
		}

		[DataMember(Name = "tag")]
        public string Tag
		{
			get;
			set;
		}

		public override string ToString()
		{
			return (!string.IsNullOrEmpty(Tag)) ? Tag : Rule;
		}
	}
}