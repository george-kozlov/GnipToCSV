
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>10/03/2013</date>
// <summary>RedditActivity class</summary>

using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
    [CSVContract(Name = "Reddit activity")]
    public class RedditActivity : EnterpriseActivity
    {
        public RedditActivity(DynamicObject dynamicObj)
            : base(dynamicObj)
        {
        }

        [CSVMember(Name = "Post title", ValuePath = "Title", Default = true)]
        public override string Title
        {
            get
            {
                return GetNestedValueOrDefault<string>("title.value", string.Empty);
            }
        }

        [CSVMember(Name = "Post content", ValuePath = "Content", Default = true)]
        public override string Content
        {
            get
            {
                return GetNestedValueOrDefault<string>("object.content.value", string.Empty);
            }
        }
    }
}
