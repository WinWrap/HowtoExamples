namespace Coroutine
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.basicIdeCtl1 = new WinWrap.Basic.BasicIdeCtl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.basicIdeCtl2 = new WinWrap.Basic.BasicIdeCtl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 183);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(624, 454);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.basicIdeCtl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(616, 428);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "IDE#1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // basicIdeCtl1
            // 
            this.basicIdeCtl1.BackColor = System.Drawing.Color.White;
            this.basicIdeCtl1.DefaultMacroName = "Macro";
            this.basicIdeCtl1.DefaultObjectName = "Object.obm|Object";
            this.basicIdeCtl1.DefaultProjectName = "Project.wbp|wbm";
            this.basicIdeCtl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basicIdeCtl1.ForeColor = System.Drawing.Color.Black;
            this.basicIdeCtl1.LargeIcon = ((System.Drawing.Icon)(resources.GetObject("basicIdeCtl1.LargeIcon")));
            this.basicIdeCtl1.Location = new System.Drawing.Point(3, 3);
            this.basicIdeCtl1.Name = "basicIdeCtl1";
            this.basicIdeCtl1.NegotiateMenus = false;
            this.basicIdeCtl1.Secret = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.basicIdeCtl1.SelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.basicIdeCtl1.SelForeColor = System.Drawing.Color.White;
            this.basicIdeCtl1.Size = new System.Drawing.Size(610, 422);
            this.basicIdeCtl1.SmallIcon = ((System.Drawing.Icon)(resources.GetObject("basicIdeCtl1.SmallIcon")));
            this.basicIdeCtl1.TabIndex = 0;
            this.basicIdeCtl1.TaskbarIcon = ((System.Drawing.Icon)(resources.GetObject("basicIdeCtl1.TaskbarIcon")));
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.basicIdeCtl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(616, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "IDE#2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // basicIdeCtl2
            // 
            this.basicIdeCtl2.BackColor = System.Drawing.Color.White;
            this.basicIdeCtl2.DefaultMacroName = "Macro";
            this.basicIdeCtl2.DefaultObjectName = "Object.obm|Object";
            this.basicIdeCtl2.DefaultProjectName = "Project.wbp|wbm";
            this.basicIdeCtl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.basicIdeCtl2.ForeColor = System.Drawing.Color.Black;
            this.basicIdeCtl2.LargeIcon = ((System.Drawing.Icon)(resources.GetObject("basicIdeCtl2.LargeIcon")));
            this.basicIdeCtl2.Location = new System.Drawing.Point(3, 3);
            this.basicIdeCtl2.Name = "basicIdeCtl2";
            this.basicIdeCtl2.NegotiateMenus = false;
            this.basicIdeCtl2.Secret = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.basicIdeCtl2.SelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.basicIdeCtl2.SelForeColor = System.Drawing.Color.White;
            this.basicIdeCtl2.Size = new System.Drawing.Size(610, 422);
            this.basicIdeCtl2.SmallIcon = ((System.Drawing.Icon)(resources.GetObject("basicIdeCtl2.SmallIcon")));
            this.basicIdeCtl2.TabIndex = 0;
            this.basicIdeCtl2.TaskbarIcon = ((System.Drawing.Icon)(resources.GetObject("basicIdeCtl2.TaskbarIcon")));
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(624, 177);
            this.textBox1.TabIndex = 1;
            this.textBox1.WordWrap = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 637);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "WinWrap Basic Co-routine Example";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private WinWrap.Basic.BasicIdeCtl basicIdeCtl1;
        private System.Windows.Forms.TabPage tabPage2;
        private WinWrap.Basic.BasicIdeCtl basicIdeCtl2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
    }
}

