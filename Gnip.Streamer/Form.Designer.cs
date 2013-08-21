
namespace Gnip.Streamer
{
	partial class GnipStreamer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GnipStreamer));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rbReply = new System.Windows.Forms.RadioButton();
            this.rbLive = new System.Windows.Forms.RadioButton();
            this.nTotal = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nAppend = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSource = new System.Windows.Forms.ComboBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tvSelector = new System.Windows.Forms.TreeView();
            this.bOutputBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.bStart = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.tbTrace = new System.Windows.Forms.TextBox();
            this.btAbout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAppend)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-1, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 60);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbAccount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rbReply);
            this.groupBox1.Controls.Add(this.rbLive);
            this.groupBox1.Controls.Add(this.nTotal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.nAppend);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbSource);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbUsername);
            this.groupBox1.Location = new System.Drawing.Point(12, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 136);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GNIP API Details";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(70, 103);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(160, 20);
            this.tbAccount.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Account:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(82, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "or";
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(386, 103);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(80, 20);
            this.dtpTo.TabIndex = 19;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(300, 103);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(80, 20);
            this.dtpFrom.TabIndex = 18;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(236, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Interval:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(383, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "records";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Use";
            // 
            // rbReply
            // 
            this.rbReply.AutoSize = true;
            this.rbReply.Location = new System.Drawing.Point(102, 19);
            this.rbReply.Name = "rbReply";
            this.rbReply.Size = new System.Drawing.Size(52, 17);
            this.rbReply.TabIndex = 14;
            this.rbReply.Text = "Reply";
            this.rbReply.UseVisualStyleBackColor = true;
            this.rbReply.CheckedChanged += new System.EventHandler(this.StreamType_CheckedChanged);
            // 
            // rbLive
            // 
            this.rbLive.AutoSize = true;
            this.rbLive.Checked = true;
            this.rbLive.Location = new System.Drawing.Point(38, 19);
            this.rbLive.Name = "rbLive";
            this.rbLive.Size = new System.Drawing.Size(45, 17);
            this.rbLive.TabIndex = 13;
            this.rbLive.TabStop = true;
            this.rbLive.Text = "Live";
            this.rbLive.UseVisualStyleBackColor = true;
            this.rbLive.CheckedChanged += new System.EventHandler(this.StreamType_CheckedChanged);
            // 
            // nTotal
            // 
            this.nTotal.Location = new System.Drawing.Point(70, 47);
            this.nTotal.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nTotal.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nTotal.Name = "nTotal";
            this.nTotal.Size = new System.Drawing.Size(100, 20);
            this.nTotal.TabIndex = 11;
            this.nTotal.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Collect";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "records and save to file every";
            this.label5.UseMnemonic = false;
            // 
            // nAppend
            // 
            this.nAppend.Location = new System.Drawing.Point(327, 47);
            this.nAppend.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nAppend.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nAppend.Name = "nAppend";
            this.nAppend.Size = new System.Drawing.Size(50, 20);
            this.nAppend.TabIndex = 8;
            this.nAppend.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "data stream";
            // 
            // cbSource
            // 
            this.cbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSource.Enabled = false;
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Items.AddRange(new object[] {
            "Bitly",
            "Board Reader",
            "Dailymotion",
            "Delicious",
            "Disqus",
            "Estimize",
            "Facebook",
            "Flickr",
            "GetGlue",
            "Google Plus",
            "Identi.ca",
            "Instagram",
            "IntenseDebate",
            "Metacafe",
            "Newsgator",
            "Panoramio",
            "Photobucket",
            "Plurk",
            "Reddit",
            "StackOverflow",
            "StockTwits",
            "Tumblr",
            "Twitter",
            "Vimeo",
            "Wordpress",
            "YouTube"});
            this.cbSource.Location = new System.Drawing.Point(157, 18);
            this.cbSource.Name = "cbSource";
            this.cbSource.Size = new System.Drawing.Size(200, 21);
            this.cbSource.TabIndex = 5;
            this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbService_SelectedIndexChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(300, 75);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(166, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(70, 75);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(160, 20);
            this.tbUsername.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tvSelector);
            this.groupBox2.Controls.Add(this.bOutputBrowse);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbOutput);
            this.groupBox2.Location = new System.Drawing.Point(12, 207);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 174);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CSV file settings";
            // 
            // tvSelector
            // 
            this.tvSelector.CheckBoxes = true;
            this.tvSelector.Location = new System.Drawing.Point(9, 47);
            this.tvSelector.Name = "tvSelector";
            this.tvSelector.Size = new System.Drawing.Size(459, 120);
            this.tvSelector.TabIndex = 3;
            this.tvSelector.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvSelector_AfterCheck);
            // 
            // bOutputBrowse
            // 
            this.bOutputBrowse.Location = new System.Drawing.Point(393, 18);
            this.bOutputBrowse.Name = "bOutputBrowse";
            this.bOutputBrowse.Size = new System.Drawing.Size(75, 23);
            this.bOutputBrowse.TabIndex = 2;
            this.bOutputBrowse.Text = "Browse";
            this.bOutputBrowse.UseVisualStyleBackColor = true;
            this.bOutputBrowse.Click += new System.EventHandler(this.bOutputBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Output file: ";
            // 
            // tbOutput
            // 
            this.tbOutput.Enabled = false;
            this.tbOutput.Location = new System.Drawing.Point(80, 20);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(307, 20);
            this.tbOutput.TabIndex = 0;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(12, 483);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(475, 23);
            this.pbProgress.TabIndex = 3;
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(331, 513);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 4;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(412, 513);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 5;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // tbTrace
            // 
            this.tbTrace.Location = new System.Drawing.Point(21, 387);
            this.tbTrace.Multiline = true;
            this.tbTrace.Name = "tbTrace";
            this.tbTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbTrace.Size = new System.Drawing.Size(459, 90);
            this.tbTrace.TabIndex = 6;
            // 
            // btAbout
            // 
            this.btAbout.Location = new System.Drawing.Point(12, 513);
            this.btAbout.Name = "btAbout";
            this.btAbout.Size = new System.Drawing.Size(75, 23);
            this.btAbout.TabIndex = 7;
            this.btAbout.Text = "About";
            this.btAbout.UseVisualStyleBackColor = true;
            this.btAbout.Click += new System.EventHandler(this.btAbout_Click);
            // 
            // GnipStreamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 546);
            this.Controls.Add(this.btAbout);
            this.Controls.Add(this.tbTrace);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.bStart);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GnipStreamer";
            this.Text = "Gnip.Streamer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nAppend)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbUsername;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbSource;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nAppend;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button bOutputBrowse;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox tbOutput;
		private System.Windows.Forms.ProgressBar pbProgress;
		private System.Windows.Forms.Button bStart;
		private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.TreeView tvSelector;
		private System.Windows.Forms.NumericUpDown nTotal;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox tbTrace;
		private System.Windows.Forms.RadioButton rbReply;
		private System.Windows.Forms.RadioButton rbLive;
		private System.Windows.Forms.DateTimePicker dtpTo;
		private System.Windows.Forms.DateTimePicker dtpFrom;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btAbout;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label label4;
	}
}

