namespace ScriptableObjectModel
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.basicIdeCtl1 = new WinWrap.Basic.BasicIdeCtl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.basicIdeCtl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1182, 753);
            this.splitContainer1.SplitterDistance = 885;
            this.splitContainer1.TabIndex = 0;
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
            this.basicIdeCtl1.Location = new System.Drawing.Point(0, 0);
            this.basicIdeCtl1.Name = "basicIdeCtl1";
            this.basicIdeCtl1.Secret = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.basicIdeCtl1.SelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.basicIdeCtl1.SelForeColor = System.Drawing.Color.White;
            this.basicIdeCtl1.Size = new System.Drawing.Size(885, 753);
            this.basicIdeCtl1.SmallIcon = ((System.Drawing.Icon)(resources.GetObject("basicIdeCtl1.SmallIcon")));
            this.basicIdeCtl1.TabIndex = 0;
            this.basicIdeCtl1.TaskbarIcon = ((System.Drawing.Icon)(resources.GetObject("basicIdeCtl1.TaskbarIcon")));
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(293, 753);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private WinWrap.Basic.BasicIdeCtl basicIdeCtl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
    }
}

