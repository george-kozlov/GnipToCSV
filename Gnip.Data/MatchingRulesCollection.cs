
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>MatchingRule class</summary>

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gnip.Data
{
    [DataContract(Name = "rulesCollection")]
    public class MatchingRulesCollection
    {
        [DataMember(Name = "rules")]
        public List<MatchingRule> Rules { get; set; }
    }
}
