using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnip.Data.Common;

namespace Gnip.Ruler.Common
{
	public sealed class GnipRulerArgs
	{
        [Argument(ArgumentType.Required, HelpText = "GNIP account name.")]
        public string account;

		[Argument(ArgumentType.Required, HelpText = "GNIP username.")]
		public string username;

		[Argument(ArgumentType.Required, HelpText = "GNIP password.")]
		public string password;

		[Argument(ArgumentType.AtMostOnce, DefaultValue = GnipSources.Twitter, HelpText = "Data source on Gnip. Can be \"Twitt\", \"Facebook\", etc...")]
		public GnipSources source;

		[Argument(ArgumentType.AtMostOnce, ShortName = "l", DefaultValue = true, HelpText = "Defines whether live or reply stream should be used.")]
		public bool live;
	}
}
