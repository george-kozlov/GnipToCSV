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
using System.Windows.Forms;
using System.Configuration;

using Gnip.Streamer.Common;

namespace Gnip.Streamer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{

			Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			ConfigurationSectionGroup group = conf.GetSectionGroup("settings");
			if (group == null)
				throw new Exception("settings group isn't available in application configuration file");

			GnipStreamerConfSection section = group.Sections["gnip.streamer"] as GnipStreamerConfSection;
			if (section == null)
                throw new Exception("gnip.ruler section isn't available in application configuration file");

			GnipStreamerArgs appArgs = new GnipStreamerArgs();
			if (args.Length > 0)
				Parser.ParseArgumentsWithUsage(args, appArgs);
			else
			{
                appArgs.account = section.Account;
				appArgs.username = section.Username;
				appArgs.password = section.Password;
				appArgs.source = section.Source;
				appArgs.output = section.Output;
				appArgs.append = section.Append;
				appArgs.total = section.Total;
				appArgs.live = section.Live;
				appArgs.from = section.From.ToString("yyyyMMddHHmm");
				appArgs.to = section.To.ToString("yyyyMMddHHmm");
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new GnipStreamer(appArgs));
		}
	}
}
