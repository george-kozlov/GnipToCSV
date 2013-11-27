//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

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
