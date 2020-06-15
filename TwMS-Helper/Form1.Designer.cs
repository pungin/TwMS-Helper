namespace TwMS_Helper
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.setAccountTool = new System.Windows.Forms.Button();
            this.setHKBFEnvironment = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startGame = new System.Windows.Forms.Button();
            this.msPath = new System.Windows.Forms.TextBox();
            this.autoCopyPwd = new System.Windows.Forms.CheckBox();
            this.browseHKBF = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.setAccountTool);
            this.groupBox1.Controls.Add(this.setHKBFEnvironment);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 114);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "環境";
            // 
            // setAccountTool
            // 
            this.setAccountTool.Location = new System.Drawing.Point(273, 34);
            this.setAccountTool.Name = "setAccountTool";
            this.setAccountTool.Size = new System.Drawing.Size(261, 46);
            this.setAccountTool.TabIndex = 1;
            this.setAccountTool.Text = "網頁開啟帳密工具";
            this.setAccountTool.UseVisualStyleBackColor = true;
            this.setAccountTool.Click += new System.EventHandler(this.setAccountTool_Click);
            // 
            // setHKBFEnvironment
            // 
            this.setHKBFEnvironment.Location = new System.Drawing.Point(6, 34);
            this.setHKBFEnvironment.Name = "setHKBFEnvironment";
            this.setHKBFEnvironment.Size = new System.Drawing.Size(261, 46);
            this.setHKBFEnvironment.TabIndex = 0;
            this.setHKBFEnvironment.Text = "IE相容性(香港BF)";
            this.setHKBFEnvironment.UseVisualStyleBackColor = true;
            this.setHKBFEnvironment.Click += new System.EventHandler(this.setHKBFEnvironment_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.browseHKBF);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.startGame);
            this.groupBox2.Controls.Add(this.msPath);
            this.groupBox2.Controls.Add(this.autoCopyPwd);
            this.groupBox2.Location = new System.Drawing.Point(12, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(540, 190);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工具";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "楓之谷無關語係運行";
            // 
            // startGame
            // 
            this.startGame.Location = new System.Drawing.Point(456, 110);
            this.startGame.Name = "startGame";
            this.startGame.Size = new System.Drawing.Size(78, 46);
            this.startGame.TabIndex = 4;
            this.startGame.Text = "啟動";
            this.startGame.UseVisualStyleBackColor = true;
            this.startGame.Click += new System.EventHandler(this.startGame_Click);
            // 
            // msPath
            // 
            this.msPath.Location = new System.Drawing.Point(10, 110);
            this.msPath.Name = "msPath";
            this.msPath.ReadOnly = true;
            this.msPath.Size = new System.Drawing.Size(440, 35);
            this.msPath.TabIndex = 3;
            this.msPath.Click += new System.EventHandler(this.msPath_Click);
            // 
            // autoCopyPwd
            // 
            this.autoCopyPwd.AutoSize = true;
            this.autoCopyPwd.Location = new System.Drawing.Point(10, 44);
            this.autoCopyPwd.Name = "autoCopyPwd";
            this.autoCopyPwd.Size = new System.Drawing.Size(282, 28);
            this.autoCopyPwd.TabIndex = 0;
            this.autoCopyPwd.Text = "帳密工具自動複製密碼";
            this.autoCopyPwd.UseVisualStyleBackColor = true;
            this.autoCopyPwd.CheckedChanged += new System.EventHandler(this.autoCopyPwd_CheckedChanged);
            // 
            // browseHKBF
            // 
            this.browseHKBF.Location = new System.Drawing.Point(298, 34);
            this.browseHKBF.Name = "browseHKBF";
            this.browseHKBF.Size = new System.Drawing.Size(236, 46);
            this.browseHKBF.TabIndex = 1;
            this.browseHKBF.Text = "用IE開啟香港BF";
            this.browseHKBF.UseVisualStyleBackColor = true;
            this.browseHKBF.Click += new System.EventHandler(this.browseHKBF_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(563, 336);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::TwMS_Helper.Properties.Resources.Icon;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "楓之谷助手";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button setHKBFEnvironment;
        private System.Windows.Forms.Button setAccountTool;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startGame;
        private System.Windows.Forms.TextBox msPath;
        private System.Windows.Forms.CheckBox autoCopyPwd;
        private System.Windows.Forms.Button browseHKBF;
    }
}

