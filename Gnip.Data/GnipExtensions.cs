
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>GnipExtensions class</summary>

using System.Collections.Generic;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
	[CSVContract(Name = "GNIP extensions")] 
	[DataContract(Name = "gnip")]
	public class GnipExtensions
	{
		[CSVMember(Name = "Matching rules", ValuePath = "MatchingRules")]
		[DataMember(Name = "matching_rules")]
		public List<MatchingRule> MatchingRules
		{
			get;
			set;
		}

		[CSVMember(Name = "URLs", ValuePath = "Urls")]
		[DataMember(Name = "urls")]
		public List<Url> Urls
		{
			get;
			set;
		}

		[CSVMember(Name = "Language", ValuePath = "Language.Value")]
		[DataMember (Name = "language")]
		public Language Language
		{
			get;
			set;
		}
	}
}
