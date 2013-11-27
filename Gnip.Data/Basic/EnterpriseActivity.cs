//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

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
