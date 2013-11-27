//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

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
