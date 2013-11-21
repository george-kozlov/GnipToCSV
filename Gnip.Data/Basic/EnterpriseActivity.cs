
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>DynamicActivityBase class</summary>

using System;
using System.Linq;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
using System.Runtime.CompilerServices;

using Gnip.Data.Common;

namespace Gnip.Data
{
    [CSVContract(Name = "Enterprise activity")]
    public class EnterpriseActivity : DynamicActivityBase
    {
        public EnterpriseActivity(DynamicObject dynamicObj)
            : base(dynamicObj)
        {
        }

        [CSVMember(Name = "Post title", ValuePath = "Title", Default = true)]
        public virtual string Title
        {
            get
            {
                return GetValueOrDefault<string>("title", string.Empty);
            }
        }

        [CSVMember(Name = "Posted time", ValuePath = "PostedTime", Default = true)]
        public virtual DateTime PostedTime
        {
            get
            {
                return DateTime.Parse(GetValueOrDefault<string>("published", string.Empty));
            }
        }

        [CSVMember(Name = "Posted type", ValuePath = "PostType", Default = true)]
        public virtual string PostType
        {
            get
            {
                return GetNestedValueOrDefault<string>("category.[label]", string.Empty);
            }
        }

        [CSVMember(Name = "Post content", ValuePath = "Content", Default = true)]
        public virtual string Content
        {
            get
            {
                return GetNestedValueOrDefault<string>("object.content.[]", string.Empty);
            }
        }

        [CSVMember(Name = "Author ID", ValuePath = "AuthorId", Default = true)]
        public virtual string AuthorId
        {
            get
            {
                return GetNestedValueOrDefault<string>("author.name", string.Empty);
            }
        }

        [CSVMember(Name = "Matching rule", ValuePath = "MatchingRule")]
        public virtual string MatchingRule
        {
            get
            {
                return GetNestedValueOrDefault<string>("matching_rules.[0].[tag]", string.Empty);
            }
        }

        public override string ToString()
        {
            return Content;
        }
    }
}
