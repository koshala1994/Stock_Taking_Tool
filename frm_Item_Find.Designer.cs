namespace Stock_Taking_Tool
{
    partial class frm_Item_Find
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Item_Find));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_Item_Find = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Item_Find)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dgv_Item_Find);
            this.panel1.Location = new System.Drawing.Point(2, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1468, 721);
            this.panel1.TabIndex = 0;
            // 
            // dgv_Item_Find
            // 
            this.dgv_Item_Find.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Item_Find.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Item_Find.Location = new System.Drawing.Point(0, 0);
            this.dgv_Item_Find.Name = "dgv_Item_Find";
            this.dgv_Item_Find.RowHeadersWidth = 51;
            this.dgv_Item_Find.RowTemplate.Height = 24;
            this.dgv_Item_Find.Size = new System.Drawing.Size(1466, 719);
            this.dgv_Item_Find.TabIndex = 0;
            this.dgv_Item_Find.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Item_Find_CellClick);
            // 
            // frm_Item_Find
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 724);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Item_Find";
            this.Text = "Item Find";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Item_Find)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_Item_Find;
    }
}