
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/06/2013</date>
// <summary>GnipToCSVConfigurationSection class</summary>

using System;
using System.Configuration;
using System.Globalization;

using Gnip.Data.Common;

namespace Gnip.Streamer.Common
{
	public class GnipStreamerConfSection : ConfigurationSection
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

		[ConfigurationProperty("output", DefaultValue = "", IsRequired = false)]
		public string Output
		{
			get
			{
				return (String)this["output"];
			}
			set
			{
				this["output"] = value;
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

		[ConfigurationProperty("append", DefaultValue = 10, IsRequired = true)]
		[IntegerValidator(MinValue = 10, MaxValue = 1000)]
		public int Append
		{
			get
			{
				return (int)this["append"];
			}
			set
			{
				this["append"] = value;
			}
		}

		[ConfigurationProperty("total", DefaultValue = 100, IsRequired = true)]
		[IntegerValidator(MinValue = 100, MaxValue = 1000000)]
		public int Total
		{
			get
			{
				return (int)this["total"];
			}
			set
			{
				this["total"] = value;
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

		[ConfigurationProperty("from", IsRequired = false)]
		public DateTime From
		{
			get
			{
				if (!((DateTime)this["from"]).Equals(DateTime.MinValue))
					return (DateTime)this["from"];
				else
					return DateTime.Today.AddDays(-3);
			}
			set
			{
				this["from"] = value.ToString("yyyyMMddHHmm");
			}
		}

		[ConfigurationProperty("to", IsRequired = false)]
		public DateTime To
		{
			get
			{
				if (!((DateTime)this["to"]).Equals(DateTime.MinValue))
					return (DateTime)this["to"];
				else
					return DateTime.Today;
			}
			set
			{
				this["to"] = value.ToString("yyyyMMddHHmm");
			}
		}
	}
}
