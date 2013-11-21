
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>09/25/2013</date>
// <summary>YouTubeActivity class</summary>

using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
    [CSVContract(Name = "YouTubeActivity activity")]
    public class YouTubeActivity : EnterpriseActivity
    {
        public YouTubeActivity(DynamicObject dynamicObj)
            : base(dynamicObj)
        {
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
