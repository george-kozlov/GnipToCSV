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
