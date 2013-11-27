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

using Gnip.Ruler.Common;

namespace Gnip.Ruler
{
    #region Auxiliary types

    public enum RuleState
    {
        Added,
        Removed,
        Genuine
    }

    #endregion

    public partial class GnipRuler : Form
    {
        #region Nested types

        internal class ListViewColumnSorter : IComparer
        {
            private int _columnToSort;
            private SortOrder _orderOfSort;
            private CaseInsensitiveComparer ObjectCompare;

            public ListViewColumnSorter()
            {
                _columnToSort = 0;
                _orderOfSort = SortOrder.None;

                ObjectCompare = new CaseInsensitiveComparer();
            }

            public int Compare(object x, object y)
            {
                int compareResult;
                ListViewItem listviewX, listviewY;
                listviewX = (ListViewItem)x;
                listviewY = (ListViewItem)y;

                compareResult = ObjectCompare.Compare(listviewX.SubItems[_columnToSort].Text, listviewY.SubItems[_columnToSort].Text);

                if (_orderOfSort == SortOrder.Ascending)
                    return compareResult;
                else if (_orderOfSort == SortOrder.Descending)
                    return (-compareResult);
                else
                    return 0;
            }

            public int SortColumn
            {
                set
                {
                    _columnToSort = value;
                }
                get
                {
                    return _columnToSort;
                }
            }

            public SortOrder Order
            {
                set
                {
                    _orderOfSort = value;
                }
                get
                {
                    return _orderOfSort;
                }
            }
        }

        #endregion

        #region Private members

        bool _hasChanges = false;
		GnipRulerArgs _settings = null;
        IRulesProcessor _processor = null;

        List<MatchingRule> _rules = new List<MatchingRule>();

		#endregion

		#region Constructor

		public GnipRuler(GnipRulerArgs args)
		{
			_settings = new GnipRulerArgs();
			if (args != null)
				_settings = args;

			InitializeComponent();
            lvRules.ListViewItemSorter = new ListViewColumnSorter();
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
                return ReflectionHelper.InvokeMethod(control, methodName, methodParams, true);
		}

		#endregion

		#region Private Methods

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

			return true;
		}

		private void PopulateSettings()
		{
            _settings.account = tbAccount.Text;
			_settings.username = tbUsername.Text;
			_settings.password = tbPassword.Text;
            _settings.source = (GnipSources)Enum.Parse(typeof(GnipSources), cbSource.Text);

            if (rbLive.Checked)
                _settings.live = true;
            else if (rbReply.Checked)
                _settings.live = false;
		}

		public void EnableControls(bool enabled)
		{
            tbAccount.Enabled = enabled;
            tbUsername.Enabled = enabled;
            tbPassword.Enabled = enabled;
            cbSource.Enabled = enabled;
            rbLive.Enabled = enabled;
            rbReply.Enabled = enabled;
		}

		private void FillControls()
		{
            tbAccount.Text = _settings.account;
			tbUsername.Text = _settings.username;
			tbPassword.Text = _settings.password;

            cbSource.Items.Clear();
            foreach (GnipSources source in Enum.GetValues(typeof(GnipSources)))
                cbSource.Items.Add(source.ToString());
            cbSource.SelectedItem = _settings.source.ToString();

            if (_settings.live)
                rbLive.Checked = true;
            else
                rbReply.Checked = true;
		}

        private void ReadRules()
        {
            _rules.Clear();
            _rules = _processor.GetRules();

            lvRules.Items.Clear();
            foreach (MatchingRule rule in _rules)
            {
                if (rule.Tag == null)
                    rule.Tag = string.Empty;

                ListViewItem item = new ListViewItem(new string[] { rule.Tag, rule.Rule });
                item.Tag = RuleState.Genuine;
                lvRules.Items.Add(item);
            }
        }

		#endregion

		#region Event handlers

		void Application_ThreadException(object sender, ThreadExceptionEventArgs ea)
		{
			System.Diagnostics.Debug.WriteLine(string.Format("Error happened:{0}\t{1}{0}\t{2}", Environment.NewLine, ea.Exception.Message, ea.Exception.StackTrace));
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

		private void btStart_Click(object sender, EventArgs e)
		{
            if (btStart.Text.Equals("Cancel") && _hasChanges)
			{
				DialogResult cancelResult = MessageBox.Show("You have unsaved changes in rules. Are you sure you want to cancel the changes? All unsaved changes will be lost.", "Cancel changes",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (cancelResult == DialogResult.Yes)
				{
                    _hasChanges = false;
					btStart.Text = "Get rules";
                    btSave.Enabled = false;
                    lvRules.Items.Clear();
                    EnableControls(true);
				}

				return;
			}
            else if (btStart.Text.Equals("Cancel") && !_hasChanges)
            {
                _hasChanges = false;
                btStart.Text = "Get rules";
                btSave.Enabled = false;
                lvRules.Items.Clear();
                EnableControls(true);

                return;
            }

			if (!IsValidParameters())
				return;

            cbSource_SelectedIndexChanged(cbSource, new EventArgs());
			EnableControls(false);
            ReadRules();

            btStart.Text = "Cancel";
		}

        private void ReInitProcessor()
        {
        }

        private void cbSource_SelectedIndexChanged(object sender, EventArgs ea)
        {
            PopulateSettings();

            if (_processor != null)
                _processor.ErrorHappened -= processor_ErrorHappened;

            //EnterpriseConnection connection = new EnterpriseConnection(_settings.username, _settings.password, _settings.account, _settings.source);
            //connection.ConnectionId = "4";

            Twitter1PercentConnection connection = new Twitter1PercentConnection(_settings.username, _settings.password, _settings.account);
            connection.IsLive = _settings.live;

            IRulesProcessor processor = GnipRulesProcessor.CreateRulesProcessor(connection);
            processor.ErrorHappened += processor_ErrorHappened;

            _processor = processor;
        }

        private void lvRules_SelectedIndexChanged(object sender, EventArgs ea)
        {
            if (lvRules.SelectedItems.Count == 1)
            {
                ListViewItem item = lvRules.SelectedItems[0];

                tbRuleTag.Text = item.Text;
                TbRuleQuery.Text = item.SubItems[1].Text;
            }
            else if (lvRules.SelectedItems.Count > 1)
            {
                tbRuleTag.Text = string.Empty;
                TbRuleQuery.Text = string.Empty;
            }

        }

		void processor_ErrorHappened(object sender, Exception ex)
		{
            MessageBox.Show(string.Format("Error happened:{0}\t{1}", Environment.NewLine, ex.Message), "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);

			EnableControls(true);
			_hasChanges = false;
		}

        private void btRuleAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TbRuleQuery.Text))
                MessageBox.Show("You try to add a rule with an empty query that isn't allowed action.", "Add rule", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (_rules.Any(item => item.Rule.Equals(TbRuleQuery.Text)))
                MessageBox.Show("Such rule already exists, please try another one.", "Add rule", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                _rules.Add(new MatchingRule() { Tag = tbRuleTag.Text, Rule = TbRuleQuery.Text });
                ListViewItem item = new ListViewItem(new string[] { tbRuleTag.Text, TbRuleQuery.Text }) { Tag = RuleState.Added, BackColor = System.Drawing.Color.LightGreen };
                lvRules.Items.Add(item);

                if (!_hasChanges)
                {
                    _hasChanges = true;
                    btStart.Text = "Cancel";
                    btSave.Enabled = true;
                }
            }
        }

        private void btRuleRemove_Click(object sender, EventArgs ea)
        {
            if (string.IsNullOrEmpty(TbRuleQuery.Text))
                MessageBox.Show("You try to remove an empty rule that isn't allowed action.", "Remove rule", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (!_rules.Any(item => item.Rule.Equals(TbRuleQuery.Text)))
                MessageBox.Show("You try to remove not existing item. Select another one and try again.", "Remove rule", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (lvRules.SelectedItems.Count != 0)
                {
                    foreach (ListViewItem selected in lvRules.SelectedItems)
                    {
                        if (_rules.Any(item => item.Rule == selected.SubItems[1].Text))
                        {
                            MatchingRule rule = _rules.First(item => item.Rule == selected.SubItems[1].Text);

                            if (((RuleState)selected.Tag) == RuleState.Added)
                                lvRules.Items.Remove(selected);
                            else
                            {
                                selected.BackColor = System.Drawing.Color.LightCoral;
                                selected.Tag = RuleState.Removed;
                            }
                        }

                        if (!_hasChanges)
                        {
                            _hasChanges = true;
                            btStart.Text = "Cancel";
                            btSave.Enabled = true;
                        }
                    }
                }
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            List<MatchingRule> addRules = new List<MatchingRule>();
            List<MatchingRule> removeRules = new List<MatchingRule>();

            foreach (ListViewItem lvItem in lvRules.Items)
            {
                if (!_rules.Any(item => item.Rule == lvItem.SubItems[1].Text))
                    continue;

                MatchingRule rule = _rules.First(item => item.Rule == lvItem.SubItems[1].Text);

                if (((RuleState)lvItem.Tag) == RuleState.Added)
                    addRules.Add(rule);
                else if (((RuleState)lvItem.Tag) == RuleState.Removed)
                    removeRules.Add(rule);
            }

            _processor.AddRules(addRules);
            _processor.RemoveRules(removeRules);
            ReadRules();

            _hasChanges = false;
            btStart.Text = "Get rules";
            btSave.Enabled = false;
            EnableControls(true);
        }

        private void lvRules_ColumnClick(object sender, ColumnClickEventArgs ea)
        {
            ListViewColumnSorter lvSorter = lvRules.ListViewItemSorter as ListViewColumnSorter;

            if (ea.Column == lvSorter.SortColumn)
            {
                if (lvSorter.Order == SortOrder.Ascending)
                    lvSorter.Order = SortOrder.Descending;
                else
                    lvSorter.Order = SortOrder.Ascending;
            }
            else
            {
                lvSorter.SortColumn = ea.Column;
                lvSorter.Order = SortOrder.Ascending;
            }

            this.lvRules.Sort();
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV files|*.csv";

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                int count = 0;

                try
                {
                    using (CsvFileReader reader = new CsvFileReader(dialog.FileName))
                    {
                        List<string> columns = new List<string>();
                        while (reader.ReadRow(columns))
                        {
                            MatchingRule rule = new MatchingRule();

                            if (columns.Count == 1)
                                rule.Rule = columns[0];
                            else if (columns.Count == 2)
                            {
                                rule.Tag = columns[0];
                                rule.Rule = columns[1];
                            }

                            if (rule.Tag == null)
                                rule.Tag = string.Empty;

                            if (!_rules.Any(item => item.Rule.Equals(rule.Rule)))
                            {
                                _rules.Add(rule);
                                lvRules.Items.Add(new ListViewItem(new string[] { rule.Tag, rule.Rule }) { Tag = RuleState.Added, BackColor = System.Drawing.Color.LightGreen });
                                count++;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    processor_ErrorHappened(this, ex);
                }

                if (count > 0)
                {
                    _hasChanges = true;
                    btStart.Text = "Cancel";
                    btSave.Enabled = true;
                }

                MessageBox.Show(string.Format("{0} record(s) have been imported.", count), "Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            FileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV files|*.csv";

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                int count = 0;

                try
                {
                    using (CsvFileWriter writer = new CsvFileWriter(dialog.FileName))
                    {
                        foreach (MatchingRule rule in _rules)
                        {
                            List<string> columns = new List<string>();
                            columns.AddRange(new List<string>() { rule.Tag, rule.Rule });
                            writer.WriteRow(columns);
                            count++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    processor_ErrorHappened(this, ex);
                }

                MessageBox.Show(string.Format("{0} record(s) have been exported.", count), "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion
    }
}
