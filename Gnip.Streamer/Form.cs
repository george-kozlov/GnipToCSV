//
// Gnip.Ruler, Gnip.Streamer
// Copyright (C) 2013 George Kozlov
// These programs are free software: you can redistribute them and/or modify them under the terms of the GNU General Public License as published by the Free Software Foundation. either version 3 of the License, or any later version.
// These programs are distributed in the hope that they will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU General Public License for more details. You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
// For further questions or inquiries, please contact semantapi (at) gmail (dot) com
//

using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using System.Globalization;

using Gnip.Data;
using Gnip.Data.Common;

using Gnip.Client;
using Gnip.Client.Common;
using Gnip.Client.Connections;

using Gnip.Streamer.Common;

namespace Gnip.Streamer
{
	public partial class GnipStreamer : Form
	{
		#region Private members

		bool _isRun = false;
		GnipStreamerArgs _settings = null;
		List<KeyValuePair<string, string>> _headers = new List<KeyValuePair<string, string>>();
		List<List<string>> _results = new List<List<string>>();
		GnipStreamProcessorBase _processor = null;

		#endregion

		#region Constructor

		public GnipStreamer(GnipStreamerArgs args)
		{
			_settings = new GnipStreamerArgs();
			if (args != null)
				_settings = args;

			InitializeComponent();
			FillControls();

			Application.ThreadException += Application_ThreadException;
		}

		#endregion

		#region Thread safe control execution

		private delegate T GetControlPropertyThreadSafeDelegate<T>(Control control, string propertyName);
		private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
		private delegate object InvokeControlMethodThreadSafeDelegate(Control control, string methodName, object[] methodParams);

		private T GetControlPropertyThreadSafe<T>(Control control, string propertyName)
		{
			if (control.InvokeRequired)
				return (T)control.Invoke(new GetControlPropertyThreadSafeDelegate<T>(GetControlPropertyThreadSafe<T>), new object[] { control, propertyName });
			else
				return (T)control.GetType().InvokeMember(propertyName, BindingFlags.GetProperty, null, control, new object[] { });
		}

		private void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
		{
			if (control.InvokeRequired)
				control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
			else
				control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
		}

		private object InvokeControlMethodThreadSafe(Control control, string methodName, object[] methodParams)
		{
			if (control.InvokeRequired)
				return control.Invoke(new InvokeControlMethodThreadSafeDelegate(InvokeControlMethodThreadSafe), new object[] { control, methodName, methodParams });
			else
				return ReflectionHelper.InvokeMethod(tbTrace, methodName, methodParams, true);
		}

		#endregion

		#region Private Methods

		private void PopulateTreeWithTheFields(Type type, TreeNode node, bool overrideName = true )
		{
			CSVContractAttribute conAttribute = ReflectionHelper.GetTypeAttribute<CSVContractAttribute>(type);
			if (conAttribute != null)
			{
                if (overrideName)
				    node.Text = conAttribute.Name;

				IList<AttributeInfo<CSVMemberAttribute>> attributes = ReflectionHelper.GetTypeMembersAttributesInfo<CSVMemberAttribute>(type);
				if (attributes != null && attributes.Count > 0)
				{
					foreach (AttributeInfo<CSVMemberAttribute> attr in attributes)
					{
						TreeNode subNode = new TreeNode();
						subNode.Text = attr.Attribute.Name;
						subNode.Checked = attr.Attribute.Default;

						if (string.IsNullOrEmpty(attr.Attribute.ValuePath))
						{
							subNode.Tag = attr.MemberName;
							conAttribute = ReflectionHelper.GetTypeAttribute<CSVContractAttribute>(attr.MemberType);
							if (conAttribute != null)
								PopulateTreeWithTheFields(attr.MemberType, subNode, false);
						}
						else
						{
							if (string.IsNullOrEmpty((string)node.Tag))
								subNode.Tag = attr.Attribute.ValuePath;
							else
								subNode.Tag = string.Format("{0}.{1}", node.Tag, attr.Attribute.ValuePath);

							UpdateMapping(subNode.Text, (string)subNode.Tag, attr.Attribute.Default);
						}

						node.Nodes.Add(subNode);
					}
				}
			}
		}

		private bool IsValidParameters()
		{
            if (string.IsNullOrEmpty(tbAccount.Text))
            {
                MessageBox.Show("Account name isn't defined. Please state name of your GNIP account.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

			if (string.IsNullOrEmpty(tbUsername.Text) || string.IsNullOrEmpty(tbPassword.Text))
			{
				MessageBox.Show("Credentials for GNIP service aren't provided. Please specify username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (nAppend.Value <= 0 || nAppend.Value > 360)
			{
				MessageBox.Show("Selected interval isn't valid. Please select interval between 1 and 360 seconds.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (string.IsNullOrEmpty(tbOutput.Text) || Uri.IsWellFormedUriString(tbOutput.Text, UriKind.Absolute))
			{
				MessageBox.Show("Output file path isn't valid. Please select output file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return true;
		}

		private void PopulateSettings()
		{
            _settings.account = tbAccount.Text;
			_settings.username = tbUsername.Text;
			_settings.password = tbPassword.Text;

			_settings.output = tbOutput.Text;
			_settings.source = (GnipSources)Enum.Parse(typeof(GnipSources), cbSource.Text);
			_settings.append = (int)nAppend.Value;
			_settings.total = (int)nTotal.Value;

			if (rbLive.Checked)
				_settings.live = true;
			else if (rbReply.Checked)
				_settings.live = false;

			if (!_settings.live)
			{
				_settings.from = dtpFrom.Value.ToString("yyyyMMddHHmm");
				_settings.to = dtpTo.Value.ToString("yyyyMMddHHmm");
			}
		}

		private void UpdateMapping(string header, string path, bool checkd)
		{
			if (_headers.Any(item => item.Key == path))
			{
				if (!checkd)
					_headers.Remove(_headers.First(item => item.Key == path));
			}
			else
			{
				if (checkd)
					_headers.Add(new KeyValuePair<string, string>(path, header));
			}
		}

		private void TraceHeaders()
		{
			string trace = string.Empty;
			foreach (KeyValuePair<string, string> pair in _headers)
			{
				trace += string.Format("{0}, ", pair.Value);
			}

			if (trace.Length > 2)
				trace = trace.Substring(0, trace.Length - 2);

			WriteLineToTrace("Output file will consist following columns in the same order:{0}\t{1}", Environment.NewLine, trace);
		}

		public void WriteLineToTrace(string message)
		{
			string text = GetControlPropertyThreadSafe<string>(tbTrace, "Text");
			SetControlPropertyThreadSafe(tbTrace, "Text", string.Format("{0}{1}{2}", text, message, Environment.NewLine));
			SetControlPropertyThreadSafe(tbTrace, "SelectionStart", tbTrace.Text.Length);
			InvokeControlMethodThreadSafe(tbTrace, "ScrollToCaret", null);
		}

		public void WriteLineToTrace(string format, params object[] args)
		{
			WriteLineToTrace(string.Format(format, args));
		}

		public void EnableControls(bool enabled)
		{
			SetControlPropertyThreadSafe(tbUsername, "Enabled", enabled);
			SetControlPropertyThreadSafe(tbPassword, "Enabled", enabled);
			SetControlPropertyThreadSafe(nAppend, "Enabled", enabled);
			SetControlPropertyThreadSafe(nTotal, "Enabled", enabled);
			SetControlPropertyThreadSafe(tbOutput, "Enabled", enabled);
			SetControlPropertyThreadSafe(bOutputBrowse, "Enabled", enabled);
			SetControlPropertyThreadSafe(tvSelector, "Enabled", enabled);
			SetControlPropertyThreadSafe(rbLive, "Enabled", enabled);
			SetControlPropertyThreadSafe(rbReply, "Enabled", enabled);
			SetControlPropertyThreadSafe(cbSource, "Enabled", enabled);

			if (GetControlPropertyThreadSafe<bool>(rbReply, "Checked"))
			{
				SetControlPropertyThreadSafe(dtpFrom, "Enabled", enabled);
				SetControlPropertyThreadSafe(dtpTo, "Enabled", enabled);
			}
		}

		private void FillControls()
		{
			cbSource.SelectedItem = _settings.source.ToString();
            tbAccount.Text = _settings.account;
            tbUsername.Text = _settings.username;
			tbPassword.Text = _settings.password;
			tbOutput.Text = _settings.output;
			nAppend.Value = _settings.append;
			nTotal.Value = _settings.total;

            cbSource.Items.Clear();
            foreach (GnipSources source in Enum.GetValues(typeof(GnipSources)))
                cbSource.Items.Add(source.ToString());
            cbSource.SelectedItem = _settings.source.ToString();

			if (_settings.live)
				rbLive.Checked = true;
			else
				rbReply.Checked = true;

			dtpFrom.Value = DateTime.ParseExact(_settings.from, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
			dtpTo.Value = DateTime.ParseExact(_settings.to, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
		}

		public void GenerateCsv(List<List<string>> data, bool append = false)
		{
			Stream stream = null;
			bool isEmptyFile = true;

			if (File.Exists(_settings.output))
			{
				FileInfo info = new FileInfo(_settings.output);
				isEmptyFile = (info.Length == 0) ? true : false;
			}

			if (append)
				stream = File.Open(_settings.output, FileMode.Append);
			else
				stream = File.Open(_settings.output, FileMode.Create);

			try
			{
				using (CsvFileWriter writer = new CsvFileWriter(stream))
				{
					if (!append || isEmptyFile)
						writer.WriteRow(_headers.ConvertAll<string>(new Converter<KeyValuePair<string, string>, string>(delegate(KeyValuePair<string, string> item)
							{
								return item.Value;
							})));

					foreach (List<string> nextRow in data)
						writer.WriteRow(nextRow);
				}

				if (append)
					WriteLineToTrace("{0} items have been added to CSV file", data.Count());
				else
					WriteLineToTrace("{0} items have been written to CSV file", data.Count());
			}
			catch (IOException ex)
			{
				WriteLineToTrace(ex.Message);
			}
		}

		#endregion

		#region Event handlers

		void Application_ThreadException(object sender, ThreadExceptionEventArgs ea)
		{
			WriteLineToTrace("Error happened:{0}\t{1}{0}\t{2}", Environment.NewLine, ea.Exception.Message, ea.Exception.StackTrace);
		}

		private void bOutputBrowse_Click(object sender, EventArgs e)
		{
			FileDialog dialog = new SaveFileDialog();
			dialog.Filter = "CSV files|*.csv";

			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK)
				tbOutput.Text = dialog.FileName;
		}

		private void btAbout_Click(object sender, EventArgs e)
		{
			About about = new About();
			about.ShowDialog(this);
		}

		private void bClose_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void bStart_Click(object sender, EventArgs e)
		{
			if (_isRun)
			{
				DialogResult cancelResult = MessageBox.Show("Are you sure you want to cancel data fetching?", "Cancel fetching?",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (cancelResult == DialogResult.Yes)
				{
					_isRun = false;
                    if (_processor != null)
					    _processor.EndStreaming();
					EnableControls(true);
					bStart.Text = "Start";
					pbProgress.Value = 0;

					if (_results.Count > 0)
					{
						DialogResult saveResult = MessageBox.Show("Do you want to save fetched data to the CSV file?", "Save data?",
							MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
						if (saveResult == DialogResult.Yes)
							GenerateCsv(_results);
					}
				}

				return;
			}

			if (!IsValidParameters())
				return;

			_isRun = true;
			PopulateSettings();
			EnableControls(false);
			bStart.Text = "Cancel";
			WriteLineToTrace("Connecting to stream...");

			_results.Clear();

			pbProgress.Minimum = 0;
			pbProgress.Maximum = _settings.total;
			pbProgress.Step = 1;

            SandBoxConnection connection = new SandBoxConnection(_settings.username, _settings.password, _settings.account, _settings.source);
            connection.ReplyFrom = DateTime.ParseExact(_settings.from, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
            connection.ReplyTo = DateTime.ParseExact(_settings.to, "yyyyMMddHHmm", CultureInfo.InvariantCulture);
            connection.IsLive = _settings.live;

            GnipStreamProcessorAsync processor = GnipStreamProcessorBase.CreateGnipProcessor<GnipStreamProcessorAsync>(connection);
			processor.DataReceived += processor_DataReceived;
			processor.ErrorHappened += processor_ErrorHappened;
            processor.BeginStreaming();
		}

		void processor_ErrorHappened(object sender, Exception ex)
		{
			GnipStreamProcessor processor = sender as GnipStreamProcessor;
			if (processor == null)
				return;

			WriteLineToTrace("Error happened:{0}\t{1}", Environment.NewLine, ex.Message);
			WriteLineToTrace("Data fetching has been canceled.");

			processor.EndStreaming();
			SetControlPropertyThreadSafe(pbProgress, "Value", 0);
			SetControlPropertyThreadSafe(bStart, "Text", "Start");
			EnableControls(true);
			_isRun = false;
		}

		private void processor_DataReceived(object sender, DynamicActivityBase activity)
		{
            GnipStreamProcessorAsync processor = sender as GnipStreamProcessorAsync;
			if (processor == null)
				return;

			switch (processor.Connection.DataSource)
			{
				case GnipSources.Twitter:
					{
						List<string> row = new List<string>(_headers.Count);

						foreach (KeyValuePair<string, string> title in _headers)
						{
							object value = ReflectionHelper.GetNestedPropertyValue(title.Key, activity);

							if (value == null)
								row.Add(string.Empty);
							else
							{
								if (ReflectionHelper.IsEnumerable(value) && !ReflectionHelper.IsValidCast(typeof(string), value))
								{
									IEnumerable enumerable = value as IEnumerable;
									string res = string.Empty;

									foreach (object item in enumerable)
										res = string.Format("{0} {1}", res, item);

									res = res.Trim();
									row.Add(res);
								}
								else
									row.Add(value.ToString());
							}
						}

						_results.Add(row);
						WriteLineToTrace("{0} record(s) have been received.", _results.Count);

						int index = ((_results.Count - (_settings.append + 1)) < 0) ? 0 : _results.Count - (_settings.append + 1);
						int count = ((index + _settings.append) > _results.Count) ? _results.Count - index : _settings.append;

						if (_results.Count % _settings.append == 0)
							GenerateCsv(_results.GetRange(index, count), true);

						int progress = GetControlPropertyThreadSafe<int>(pbProgress, "Value");
						SetControlPropertyThreadSafe(pbProgress, "Value", progress + 1);

						if (_results.Count == _settings.total)
						{
							processor.EndStreaming();
							SetControlPropertyThreadSafe(pbProgress, "Value", 0);
							SetControlPropertyThreadSafe(bStart, "Text", "Start");
							EnableControls(true);
							_isRun = false;
						}
					}

					break;
			}
		}

		private void cbService_SelectedIndexChanged(object sender, EventArgs ea)
		{
            _headers.Clear();
			tvSelector.Nodes.Clear();
			TreeNode root = new TreeNode();

			GnipSources source = (GnipSources)Enum.Parse(typeof(GnipSources), cbSource.Text);
			_settings.source = source;
			Type outputType = GnipStreamProcessor.GetObjectTypeByService(source);

			PopulateTreeWithTheFields(outputType, root);
			TraceHeaders();
			tvSelector.Nodes.Add(root);
            tvSelector.ExpandAll();
		}

		private void tvSelector_AfterCheck(object sender, TreeViewEventArgs ea)
		{
			if (ea.Node.Nodes.Count > 0)
			{
				foreach (TreeNode node in ea.Node.Nodes)
					node.Checked = ea.Node.Checked;
			}

			UpdateMapping(ea.Node.Text, (string)ea.Node.Tag, ea.Node.Checked);
			if (ea.Node.Parent == null || ea.Node.Parent.Checked != ea.Node.Checked || (!ea.Node.Parent.Checked && !ea.Node.Checked))
				TraceHeaders();
		}

		private void StreamType_CheckedChanged(object sender, EventArgs ea)
		{
			RadioButton rButton = sender as RadioButton;
			if (rButton == null)
				return;

			if (rButton == rbLive && rButton.Checked)
			{
				dtpFrom.Enabled = false;
				dtpTo.Enabled = false;
			}

			if (rButton == rbReply && rbReply.Checked)
			{
				dtpFrom.Enabled = true;
				dtpTo.Enabled = true;
			}
		}

		#endregion
	}
}
