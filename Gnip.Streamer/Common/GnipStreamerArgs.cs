using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnip.Data.Common;

namespace Gnip.Streamer.Common
{
	public sealed class GnipStreamerArgs
	{
        [Argument(ArgumentType.Required, HelpText = "GNIP account name.")]
        public string account;

		[Argument(ArgumentType.Required, HelpText = "GNIP username.")]
		public string username;

		[Argument(ArgumentType.Required, HelpText = "GNIP password.")]
		public string password;

		[Argument(ArgumentType.Required, HelpText = "Path to the output CSV file with received data.")]
		public string output;

		[Argument(ArgumentType.AtMostOnce, DefaultValue = GnipSources.Twitter, HelpText = "Data source on Gnip. Can be \"Twitt\", \"Facebook\", etc...")]
		public GnipSources source;

		[Argument(ArgumentType.AtMostOnce, ShortName = "a", DefaultValue = 10, HelpText = "Append every N records to file.")]
		public int append;

		[Argument(ArgumentType.AtMostOnce, ShortName = "y", DefaultValue = 100, HelpText = "Total records which need to be collected.")]
		public int total;

		[Argument(ArgumentType.AtMostOnce, ShortName = "l", DefaultValue = true, HelpText = "Defines whether live or reply stream should be used.")]
		public bool live;

		[Argument(ArgumentType.AtMostOnce, HelpText = "If live streaming is false, defines \"from\" date for reply interval.")]
		public string from;

		[Argument(ArgumentType.AtMostOnce, HelpText = "If live streaming is false, defines \"to\" date for reply interval.")]
		public string to;
	}
}
