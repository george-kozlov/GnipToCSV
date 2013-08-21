
// <author>George Kozlov (george.kozlov@outlook.com)</author>
// <date>07/05/2013</date>
// <summary>About form class</summary>

using System.Windows.Forms;

namespace Gnip.Ruler
{
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
			lVersion.Text = string.Format("Version {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
		}

		private void llEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			llEmail.LinkVisited = true;
			System.Diagnostics.Process.Start("mailto:george.kozlov@outlook.com");
		}
	}
}
