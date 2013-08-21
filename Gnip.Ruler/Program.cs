using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

using Gnip.Ruler.Common;

namespace Gnip.Ruler
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

			GnipRulerConfSection section = group.Sections["gnip.ruler"] as GnipRulerConfSection;
			if (section == null)
                throw new Exception("gnip.ruler section isn't available in application configuration file");

			GnipRulerArgs appArgs = new GnipRulerArgs();
			if (args.Length > 0)
				Parser.ParseArgumentsWithUsage(args, appArgs);
			else
			{
                appArgs.account = section.Account;
				appArgs.username = section.Username;
				appArgs.password = section.Password;
				appArgs.source = section.Source;
				appArgs.live = section.Live;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new GnipRuler(appArgs));
		}
	}
}
