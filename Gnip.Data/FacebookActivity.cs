
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>10/01/2013</date>
// <summary>FacebookActivity class</summary>

using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
    [CSVContract(Name = "Facebook activity")]
    public class FacebookActivity : EnterpriseActivity
    {
        public FacebookActivity(DynamicObject dynamicObj)
            : base(dynamicObj)
        {
        }

        [CSVMember(Name = "Post content", ValuePath = "Content", Default = true)]
        public override string Content
        {
            get
            {
                return GetNestedValueOrDefault<string>("object.content", string.Empty);
            }
        }
    }
}
