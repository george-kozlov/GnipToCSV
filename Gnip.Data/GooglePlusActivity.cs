
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>11/01/2013</date>
// <summary>YouTubeActivity class</summary>

using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
    [CSVContract(Name = "GooglePlus activity")]
    public class GooglePlusActivity : EnterpriseActivity
    {
        public GooglePlusActivity(DynamicObject dynamicObj)
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
