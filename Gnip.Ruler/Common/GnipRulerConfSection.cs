
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/06/2013</date>
// <summary>GnipToCSVConfigurationSection class</summary>

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
