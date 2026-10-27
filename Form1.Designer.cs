namespace Stock_Taking_Tool
{
    partial class frm_Stock_Taking_Tool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Stock_Taking_Tool));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Export = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pb_Download = new System.Windows.Forms.ProgressBar();
            this.btn_Cashier_Download = new System.Windows.Forms.Button();
            this.btn_Counting = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Export);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 346);
            this.panel1.TabIndex = 0;
            // 
            // btn_Export
            // 
            this.btn_Export.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Export.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Export.Image = global::Stock_Taking_Tool.Properties.Resources.Export;
            this.btn_Export.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Export.Location = new System.Drawing.Point(6, 155);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(272, 67);
            this.btn_Export.TabIndex = 4;
            this.btn_Export.Text = "Export Data";
            this.btn_Export.UseVisualStyleBackColor = false;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.pb_Download);
            this.panel2.Controls.Add(this.btn_Cashier_Download);
            this.panel2.Controls.Add(this.btn_Counting);
            this.panel2.Controls.Add(this.btn_Download);
            this.panel2.Location = new System.Drawing.Point(3, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(858, 118);
            this.panel2.TabIndex = 1;
            // 
            // pb_Download
            // 
            this.pb_Download.Location = new System.Drawing.Point(3, 67);
            this.pb_Download.Name = "pb_Download";
            this.pb_Download.Size = new System.Drawing.Size(849, 46);
            this.pb_Download.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_Download.TabIndex = 3;
            this.pb_Download.Visible = false;
            // 
            // btn_Cashier_Download
            // 
            this.btn_Cashier_Download.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(224)))), ((int)(((byte)(198)))));
            this.btn_Cashier_Download.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cashier_Download.Image = global::Stock_Taking_Tool.Properties.Resources.Account;
            this.btn_Cashier_Download.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Cashier_Download.Location = new System.Drawing.Point(264, 3);
            this.btn_Cashier_Download.Name = "btn_Cashier_Download";
            this.btn_Cashier_Download.Size = new System.Drawing.Size(312, 58);
            this.btn_Cashier_Download.TabIndex = 2;
            this.btn_Cashier_Download.Text = "Cashier Download";
            this.btn_Cashier_Download.UseVisualStyleBackColor = false;
            this.btn_Cashier_Download.Click += new System.EventHandler(this.btn_Cashier_Download_Click);
            // 
            // btn_Counting
            // 
            this.btn_Counting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(167)))), ((int)(((byte)(224)))));
            this.btn_Counting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Counting.Image = global::Stock_Taking_Tool.Properties.Resources.stocktaking;
            this.btn_Counting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Counting.Location = new System.Drawing.Point(582, 3);
            this.btn_Counting.Name = "btn_Counting";
            this.btn_Counting.Size = new System.Drawing.Size(270, 58);
            this.btn_Counting.TabIndex = 1;
            this.btn_Counting.Text = "Stock Counting";
            this.btn_Counting.UseVisualStyleBackColor = false;
            this.btn_Counting.Click += new System.EventHandler(this.btn_Counting_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(223)))), ((int)(((byte)(146)))));
            this.btn_Download.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Download.Image = global::Stock_Taking_Tool.Properties.Resources.Download;
            this.btn_Download.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Download.Location = new System.Drawing.Point(3, 3);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(255, 58);
            this.btn_Download.TabIndex = 0;
            this.btn_Download.Text = "Item Download";
            this.btn_Download.UseVisualStyleBackColor = false;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(864, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(139, 26);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.reportToolStripMenuItem.Text = "Report";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // frm_Stock_Taking_Tool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 349);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frm_Stock_Taking_Tool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Onimta  Stock Taking Tool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.Button btn_Counting;
        private System.Windows.Forms.Button btn_Cashier_Download;
        private System.Windows.Forms.ProgressBar pb_Download;
        private System.Windows.Forms.Button btn_Export;
    }
}

