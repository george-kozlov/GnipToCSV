
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>11/05/2013</date>
// <summary>PowerStreamActivity class</summary>

using System;
using System.Dynamic;

using Gnip.Data.Common;

namespace Gnip.Data
{
    [CSVContract(Name = "PowerStream activity")]
    public class PowerStreamActivity : DynamicActivityBase
    {
        public PowerStreamActivity(DynamicObject dynamicObj)
            : base(dynamicObj)
        {
        }

        [CSVMember(Name = "Author ID", ValuePath = "AuthorId")]
        public virtual string AuthorId
        {
            get
            {
                return GetNestedValueOrDefault<string>("actor.id", string.Empty);
            }
        }

        [CSVMember(Name = "Post type", ValuePath = "PostType", Default = true)]
        public virtual string PostType
        {
            get
            {
                return GetNestedValueOrDefault<string>("object.objectType", string.Empty);
            }
        }

        [CSVMember(Name = "Posted time", ValuePath = "PostedTime", Default = true)]
        public virtual DateTime PostedTime
        {
            get
            {
                return DateTime.Parse(GetValueOrDefault<string>("postedTime", string.Empty));
            }
        }

        // Applicable only for Premium sources
        [CSVMember(Name = "Language", ValuePath = "Language")]
        public virtual string Language
        {
            get
            {
                return GetNestedValueOrDefault<string>("gnip.language.value", string.Empty);
            }
        }

        [CSVMember(Name = "Matching rule", ValuePath = "MatchingRule")]
        public virtual string MatchingRule
        {
            get
            {
                return GetNestedValueOrDefault<string>("gnip.matching_rules.[0].[tag]", string.Empty);
            }
        }
    }
}
