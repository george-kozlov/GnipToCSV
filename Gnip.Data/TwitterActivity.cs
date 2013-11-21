
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>TwitterActivity class</summary>

using System.Dynamic;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Gnip.Data.Common;

namespace Gnip.Data
{
    [CSVContract(Name = "Twitter activity")]
	public class TwitterActivity : PowerStreamActivity
	{
        public TwitterActivity(DynamicObject dynamicObj)
            : base(dynamicObj)
        {
        }

        [CSVMember(Name = "Tweet message", ValuePath = "Message", Default = true)]
        public string Message
        {
            get
            {
                return GetNestedValueOrDefault<string>("body", string.Empty);
            }
        }

        [CSVMember(Name = "Tweet link", ValuePath = "Username")]
        public string Link
        {
            get
            {
                return GetNestedValueOrDefault<string>("object.link", string.Empty);
            }
        }

        [CSVMember(Name = "Author Username", ValuePath = "Username")]
        public string Username
        {
            get
            {
                return GetNestedValueOrDefault<string>("actor.displayName", string.Empty);
            }
        }

        public override string ToString()
        {
            return Message;
        }
	}
}
