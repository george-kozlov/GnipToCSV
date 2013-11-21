
namespace Gnip.Ruler
{
	partial class GnipRuler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GnipRuler));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.rbReply = new System.Windows.Forms.RadioButton();
            this.rbLive = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSource = new System.Windows.Forms.ComboBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btExport = new System.Windows.Forms.Button();
            this.btImport = new System.Windows.Forms.Button();
            this.btRuleRemove = new System.Windows.Forms.Button();
            this.btRuleAdd = new System.Windows.Forms.Button();
            this.TbRuleQuery = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbRuleTag = new System.Windows.Forms.TextBox();
            this.lvRules = new System.Windows.Forms.ListView();
            this.cTag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cRule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btStart = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.btAbout = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.rbReply);
            this.groupBox1.Controls.Add(this.rbLive);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbSource);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbUsername);
            this.groupBox1.Location = new System.Drawing.Point(12, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 107);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GNIP API Details";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(80, 75);
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(150, 20);
            this.tbAccount.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Account:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(99, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "or";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Setup";
            // 
            // rbReply
            // 
            this.rbReply.AutoSize = true;
            this.rbReply.Location = new System.Drawing.Point(121, 20);
            this.rbReply.Name = "rbReply";
            this.rbReply.Size = new System.Drawing.Size(52, 17);
            this.rbReply.TabIndex = 24;
            this.rbReply.Text = "Reply";
            this.rbReply.UseVisualStyleBackColor = true;
            // 
            // rbLive
            // 
            this.rbLive.AutoSize = true;
            this.rbLive.Checked = true;
            this.rbLive.Location = new System.Drawing.Point(48, 20);
            this.rbLive.Name = "rbLive";
            this.rbLive.Size = new System.Drawing.Size(45, 17);
            this.rbLive.TabIndex = 23;
            this.rbLive.TabStop = true;
            this.rbLive.Text = "Live";
            this.rbLive.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(385, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "data stream";
            // 
            // cbSource
            // 
            this.cbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSource.FormattingEnabled = true;
            this.cbSource.Location = new System.Drawing.Point(179, 18);
            this.cbSource.Name = "cbSource";
            this.cbSource.Size = new System.Drawing.Size(200, 21);
            this.cbSource.TabIndex = 21;
            this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbSource_SelectedIndexChanged);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(318, 47);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(148, 20);
            this.tbPassword.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(80, 47);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(150, 20);
            this.tbUsername.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.lvRules);
            this.groupBox2.Location = new System.Drawing.Point(12, 178);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(475, 320);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rules settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btExport);
            this.groupBox3.Controls.Add(this.btImport);
            this.groupBox3.Controls.Add(this.btRuleRemove);
            this.groupBox3.Controls.Add(this.btRuleAdd);
            this.groupBox3.Controls.Add(this.TbRuleQuery);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.tbRuleTag);
            this.groupBox3.Location = new System.Drawing.Point(10, 185);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(456, 125);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rule detail";
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(93, 92);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(70, 23);
            this.btExport.TabIndex = 34;
            this.btExport.Text = "Export";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // btImport
            // 
            this.btImport.Location = new System.Drawing.Point(10, 92);
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(70, 23);
            this.btImport.TabIndex = 33;
            this.btImport.Text = "Import";
            this.btImport.UseVisualStyleBackColor = true;
            this.btImport.Click += new System.EventHandler(this.btImport_Click);
            // 
            // btRuleRemove
            // 
            this.btRuleRemove.Location = new System.Drawing.Point(92, 45);
            this.btRuleRemove.Name = "btRuleRemove";
            this.btRuleRemove.Size = new System.Drawing.Size(70, 23);
            this.btRuleRemove.TabIndex = 32;
            this.btRuleRemove.Text = "Remove";
            this.btRuleRemove.UseVisualStyleBackColor = true;
            this.btRuleRemove.Click += new System.EventHandler(this.btRuleRemove_Click);
            // 
            // btRuleAdd
            // 
            this.btRuleAdd.Location = new System.Drawing.Point(10, 45);
            this.btRuleAdd.Name = "btRuleAdd";
            this.btRuleAdd.Size = new System.Drawing.Size(70, 23);
            this.btRuleAdd.TabIndex = 31;
            this.btRuleAdd.Text = "Add";
            this.btRuleAdd.UseVisualStyleBackColor = true;
            this.btRuleAdd.Click += new System.EventHandler(this.btRuleAdd_Click);
            // 
            // TbRuleQuery
            // 
            this.TbRuleQuery.Location = new System.Drawing.Point(169, 19);
            this.TbRuleQuery.Multiline = true;
            this.TbRuleQuery.Name = "TbRuleQuery";
            this.TbRuleQuery.Size = new System.Drawing.Size(280, 96);
            this.TbRuleQuery.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Tag:";
            // 
            // tbRuleTag
            // 
            this.tbRuleTag.Location = new System.Drawing.Point(42, 19);
            this.tbRuleTag.Name = "tbRuleTag";
            this.tbRuleTag.Size = new System.Drawing.Size(120, 20);
            this.tbRuleTag.TabIndex = 0;
            // 
            // lvRules
            // 
            this.lvRules.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cTag,
            this.cRule});
            this.lvRules.FullRowSelect = true;
            this.lvRules.HideSelection = false;
            this.lvRules.Location = new System.Drawing.Point(10, 19);
            this.lvRules.Name = "lvRules";
            this.lvRules.Size = new System.Drawing.Size(456, 160);
            this.lvRules.TabIndex = 7;
            this.lvRules.UseCompatibleStateImageBehavior = false;
            this.lvRules.View = System.Windows.Forms.View.Details;
            this.lvRules.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvRules_ColumnClick);
            this.lvRules.SelectedIndexChanged += new System.EventHandler(this.lvRules_SelectedIndexChanged);
            // 
            // cTag
            // 
            this.cTag.Text = "Tag";
            this.cTag.Width = 150;
            // 
            // cRule
            // 
            this.cRule.Text = "Rule";
            this.cRule.Width = 280;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(250, 511);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 4;
            this.btStart.Text = "Get rules";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(412, 511);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 5;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // btAbout
            // 
            this.btAbout.Location = new System.Drawing.Point(12, 511);
            this.btAbout.Name = "btAbout";
            this.btAbout.Size = new System.Drawing.Size(75, 23);
            this.btAbout.TabIndex = 7;
            this.btAbout.Text = "About";
            this.btAbout.UseVisualStyleBackColor = true;
            this.btAbout.Click += new System.EventHandler(this.btAbout_Click);
            // 
            // btSave
            // 
            this.btSave.Enabled = false;
            this.btSave.Location = new System.Drawing.Point(331, 511);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(75, 23);
            this.btSave.TabIndex = 8;
            this.btSave.Text = "Save rules";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // GnipRuler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 546);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.btAbout);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GnipRuler";
            this.Text = "Gnip.Ruler";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUsername;
		private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button btAbout;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rbReply;
        private System.Windows.Forms.RadioButton rbLive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSource;
        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvRules;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TbRuleQuery;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbRuleTag;
        private System.Windows.Forms.Button btRuleRemove;
        private System.Windows.Forms.Button btRuleAdd;
        private System.Windows.Forms.ColumnHeader cTag;
        private System.Windows.Forms.ColumnHeader cRule;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.Button btImport;
	}
}

