//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

using System;
using System.Configuration;
using System.Globalization;

using Gnip.Data.Common;

namespace Gnip.Ruler.Common
{
	public class GnipRulerConfSection : ConfigurationSection
	{
        [ConfigurationProperty("account", DefaultValue = "", IsRequired = false)]
        public string Account
        {
            get
            {
                return (String)this["account"];
            }
            set
            {
                this["account"] = value;
            }
        }

		[ConfigurationProperty("username", DefaultValue = "", IsRequired = false)]
		public string Username
		{
			get
			{
				return (String)this["username"];
			}
			set
			{
				this["username"] = value;
			}
		}

		[ConfigurationProperty("password", DefaultValue = "", IsRequired = false)]
		public string Password
		{
			get
			{
				return (String)this["password"];
			}
			set
			{
				this["password"] = value;
			}
		}

		[ConfigurationProperty("source", DefaultValue = GnipSources.Twitter, IsRequired = true)]
		public GnipSources Source
		{
			get
			{
				return (GnipSources)this["source"];
			}
			set
			{
				this["source"] = value;
			}
		}

		[ConfigurationProperty("live", DefaultValue = true, IsRequired = true)]
		public bool Live
		{
			get
			{
				return (bool)this["live"];
			}
			set
			{
				this["live"] = value;
			}
		}
	}
}
