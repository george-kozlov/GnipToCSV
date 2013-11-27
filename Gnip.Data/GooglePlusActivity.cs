//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

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
