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
