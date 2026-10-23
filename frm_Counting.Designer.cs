namespace Stock_Taking_Tool
{
    partial class frm_Counting
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Counting));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txt_Item_enter = new System.Windows.Forms.TextBox();
            this.btn_Item_Find = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_Back = new System.Windows.Forms.Button();
            this.btn_Zero = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Three = new System.Windows.Forms.Button();
            this.btn_Two = new System.Windows.Forms.Button();
            this.btn_One = new System.Windows.Forms.Button();
            this.btn_Six = new System.Windows.Forms.Button();
            this.btn_Five = new System.Windows.Forms.Button();
            this.btn_Four = new System.Windows.Forms.Button();
            this.btn_Nine = new System.Windows.Forms.Button();
            this.btn_Eight = new System.Windows.Forms.Button();
            this.btn_Seven = new System.Windows.Forms.Button();
            this.btn_Save_and_Print = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgv_Transaction = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_User = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_Unit_No = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Void = new System.Windows.Forms.Button();
            this.chk_Error = new System.Windows.Forms.CheckBox();
            this.rdo_Manual = new System.Windows.Forms.RadioButton();
            this.rdo_Auto = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Transaction)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.btn_Save);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btn_Save_and_Print);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1924, 966);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.txt_Item_enter);
            this.panel5.Controls.Add(this.btn_Item_Find);
            this.panel5.Location = new System.Drawing.Point(3, 833);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1391, 121);
            this.panel5.TabIndex = 2;
            // 
            // txt_Item_enter
            // 
            this.txt_Item_enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Item_enter.Location = new System.Drawing.Point(42, 41);
            this.txt_Item_enter.Name = "txt_Item_enter";
            this.txt_Item_enter.Size = new System.Drawing.Size(1147, 53);
            this.txt_Item_enter.TabIndex = 0;
            this.txt_Item_enter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Item_enter_KeyDown);
            // 
            // btn_Item_Find
            // 
            this.btn_Item_Find.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btn_Item_Find.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Item_Find.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Item_Find.ForeColor = System.Drawing.Color.White;
            this.btn_Item_Find.Location = new System.Drawing.Point(1195, 32);
            this.btn_Item_Find.Name = "btn_Item_Find";
            this.btn_Item_Find.Size = new System.Drawing.Size(150, 70);
            this.btn_Item_Find.TabIndex = 6;
            this.btn_Item_Find.Text = "find";
            this.btn_Item_Find.UseVisualStyleBackColor = false;
            this.btn_Item_Find.Click += new System.EventHandler(this.btn_Item_Find_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btn_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Save.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.btn_Save.FlatAppearance.BorderSize = 2;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.ForeColor = System.Drawing.Color.White;
            this.btn_Save.Location = new System.Drawing.Point(1454, 864);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(210, 72);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btn_Back);
            this.panel4.Controls.Add(this.btn_Zero);
            this.panel4.Controls.Add(this.btn_Clear);
            this.panel4.Controls.Add(this.btn_Three);
            this.panel4.Controls.Add(this.btn_Two);
            this.panel4.Controls.Add(this.btn_One);
            this.panel4.Controls.Add(this.btn_Six);
            this.panel4.Controls.Add(this.btn_Five);
            this.panel4.Controls.Add(this.btn_Four);
            this.panel4.Controls.Add(this.btn_Nine);
            this.panel4.Controls.Add(this.btn_Eight);
            this.panel4.Controls.Add(this.btn_Seven);
            this.panel4.Location = new System.Drawing.Point(1400, 97);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(659, 730);
            this.panel4.TabIndex = 1;
            // 
            // btn_Back
            // 
            this.btn_Back.BackColor = System.Drawing.Color.White;
            this.btn_Back.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Back.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Back.Location = new System.Drawing.Point(446, 583);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(180, 125);
            this.btn_Back.TabIndex = 11;
            this.btn_Back.Text = "<=";
            this.btn_Back.UseVisualStyleBackColor = false;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // btn_Zero
            // 
            this.btn_Zero.BackColor = System.Drawing.Color.White;
            this.btn_Zero.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Zero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Zero.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Zero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Zero.Location = new System.Drawing.Point(239, 583);
            this.btn_Zero.Name = "btn_Zero";
            this.btn_Zero.Size = new System.Drawing.Size(180, 125);
            this.btn_Zero.TabIndex = 10;
            this.btn_Zero.Text = "0";
            this.btn_Zero.UseVisualStyleBackColor = false;
            // 
            // btn_Clear
            // 
            this.btn_Clear.BackColor = System.Drawing.Color.White;
            this.btn_Clear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Clear.Location = new System.Drawing.Point(32, 583);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(180, 125);
            this.btn_Clear.TabIndex = 9;
            this.btn_Clear.Text = "C";
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Three
            // 
            this.btn_Three.BackColor = System.Drawing.Color.White;
            this.btn_Three.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Three.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Three.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Three.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Three.Location = new System.Drawing.Point(446, 390);
            this.btn_Three.Name = "btn_Three";
            this.btn_Three.Size = new System.Drawing.Size(180, 125);
            this.btn_Three.TabIndex = 8;
            this.btn_Three.Text = "3";
            this.btn_Three.UseVisualStyleBackColor = false;
            // 
            // btn_Two
            // 
            this.btn_Two.BackColor = System.Drawing.Color.White;
            this.btn_Two.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Two.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Two.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Two.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Two.Location = new System.Drawing.Point(239, 390);
            this.btn_Two.Name = "btn_Two";
            this.btn_Two.Size = new System.Drawing.Size(180, 125);
            this.btn_Two.TabIndex = 7;
            this.btn_Two.Text = "2";
            this.btn_Two.UseVisualStyleBackColor = false;
            // 
            // btn_One
            // 
            this.btn_One.BackColor = System.Drawing.Color.White;
            this.btn_One.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_One.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_One.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_One.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_One.Location = new System.Drawing.Point(32, 390);
            this.btn_One.Name = "btn_One";
            this.btn_One.Size = new System.Drawing.Size(180, 125);
            this.btn_One.TabIndex = 6;
            this.btn_One.Text = "1";
            this.btn_One.UseVisualStyleBackColor = false;
            // 
            // btn_Six
            // 
            this.btn_Six.BackColor = System.Drawing.Color.White;
            this.btn_Six.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Six.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Six.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Six.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Six.Location = new System.Drawing.Point(446, 202);
            this.btn_Six.Name = "btn_Six";
            this.btn_Six.Size = new System.Drawing.Size(180, 125);
            this.btn_Six.TabIndex = 5;
            this.btn_Six.Text = "6";
            this.btn_Six.UseVisualStyleBackColor = false;
            // 
            // btn_Five
            // 
            this.btn_Five.BackColor = System.Drawing.Color.White;
            this.btn_Five.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Five.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Five.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Five.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Five.Location = new System.Drawing.Point(239, 202);
            this.btn_Five.Name = "btn_Five";
            this.btn_Five.Size = new System.Drawing.Size(180, 125);
            this.btn_Five.TabIndex = 4;
            this.btn_Five.Text = "5";
            this.btn_Five.UseVisualStyleBackColor = false;
            // 
            // btn_Four
            // 
            this.btn_Four.BackColor = System.Drawing.Color.White;
            this.btn_Four.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Four.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Four.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Four.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Four.Location = new System.Drawing.Point(32, 202);
            this.btn_Four.Name = "btn_Four";
            this.btn_Four.Size = new System.Drawing.Size(180, 125);
            this.btn_Four.TabIndex = 3;
            this.btn_Four.Text = "4";
            this.btn_Four.UseVisualStyleBackColor = false;
            // 
            // btn_Nine
            // 
            this.btn_Nine.BackColor = System.Drawing.Color.White;
            this.btn_Nine.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Nine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Nine.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Nine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Nine.Location = new System.Drawing.Point(446, 17);
            this.btn_Nine.Name = "btn_Nine";
            this.btn_Nine.Size = new System.Drawing.Size(180, 125);
            this.btn_Nine.TabIndex = 2;
            this.btn_Nine.Text = "9";
            this.btn_Nine.UseVisualStyleBackColor = false;
            // 
            // btn_Eight
            // 
            this.btn_Eight.BackColor = System.Drawing.Color.White;
            this.btn_Eight.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Eight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Eight.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Eight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Eight.Location = new System.Drawing.Point(239, 17);
            this.btn_Eight.Name = "btn_Eight";
            this.btn_Eight.Size = new System.Drawing.Size(180, 125);
            this.btn_Eight.TabIndex = 1;
            this.btn_Eight.Text = "8";
            this.btn_Eight.UseVisualStyleBackColor = false;
            // 
            // btn_Seven
            // 
            this.btn_Seven.BackColor = System.Drawing.Color.White;
            this.btn_Seven.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(222)))), ((int)(((byte)(235)))));
            this.btn_Seven.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Seven.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Seven.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btn_Seven.Location = new System.Drawing.Point(32, 17);
            this.btn_Seven.Name = "btn_Seven";
            this.btn_Seven.Size = new System.Drawing.Size(180, 125);
            this.btn_Seven.TabIndex = 0;
            this.btn_Seven.Text = "7";
            this.btn_Seven.UseVisualStyleBackColor = false;
            // 
            // btn_Save_and_Print
            // 
            this.btn_Save_and_Print.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btn_Save_and_Print.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Save_and_Print.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btn_Save_and_Print.FlatAppearance.BorderSize = 2;
            this.btn_Save_and_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save_and_Print.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save_and_Print.ForeColor = System.Drawing.Color.White;
            this.btn_Save_and_Print.Location = new System.Drawing.Point(1776, 864);
            this.btn_Save_and_Print.Name = "btn_Save_and_Print";
            this.btn_Save_and_Print.Size = new System.Drawing.Size(210, 72);
            this.btn_Save_and_Print.TabIndex = 8;
            this.btn_Save_and_Print.Text = "Print";
            this.btn_Save_and_Print.UseVisualStyleBackColor = false;
            this.btn_Save_and_Print.Click += new System.EventHandler(this.btn_Save_and_Print_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.dgv_Transaction);
            this.panel3.Location = new System.Drawing.Point(3, 101);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1391, 728);
            this.panel3.TabIndex = 1;
            // 
            // dgv_Transaction
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Transaction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Transaction.ColumnHeadersHeight = 29;
            this.dgv_Transaction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_Transaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Transaction.Location = new System.Drawing.Point(0, 0);
            this.dgv_Transaction.Name = "dgv_Transaction";
            this.dgv_Transaction.RowHeadersWidth = 51;
            this.dgv_Transaction.RowTemplate.Height = 24;
            this.dgv_Transaction.Size = new System.Drawing.Size(1389, 726);
            this.dgv_Transaction.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbl_User);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lbl_Unit_No);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btn_Void);
            this.panel2.Controls.Add(this.chk_Error);
            this.panel2.Controls.Add(this.rdo_Manual);
            this.panel2.Controls.Add(this.rdo_Auto);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(2230, 92);
            this.panel2.TabIndex = 0;
            // 
            // lbl_User
            // 
            this.lbl_User.AutoSize = true;
            this.lbl_User.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_User.Location = new System.Drawing.Point(1220, 27);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(169, 36);
            this.lbl_User.TabIndex = 7;
            this.lbl_User.Text = "Cashier 01";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1114, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 36);
            this.label3.TabIndex = 6;
            this.label3.Text = "User :";
            // 
            // lbl_Unit_No
            // 
            this.lbl_Unit_No.AutoSize = true;
            this.lbl_Unit_No.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Unit_No.Location = new System.Drawing.Point(715, 27);
            this.lbl_Unit_No.Name = "lbl_Unit_No";
            this.lbl_Unit_No.Size = new System.Drawing.Size(51, 36);
            this.lbl_Unit_No.TabIndex = 5;
            this.lbl_Unit_No.Text = "01";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(559, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 36);
            this.label1.TabIndex = 4;
            this.label1.Text = "Unit  No :";
            // 
            // btn_Void
            // 
            this.btn_Void.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btn_Void.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Void.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Void.FlatAppearance.BorderSize = 2;
            this.btn_Void.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Void.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Void.ForeColor = System.Drawing.Color.White;
            this.btn_Void.Location = new System.Drawing.Point(1840, 21);
            this.btn_Void.Name = "btn_Void";
            this.btn_Void.Size = new System.Drawing.Size(142, 48);
            this.btn_Void.TabIndex = 3;
            this.btn_Void.Text = "Void";
            this.btn_Void.UseVisualStyleBackColor = false;
            this.btn_Void.Click += new System.EventHandler(this.btn_Void_Click);
            // 
            // chk_Error
            // 
            this.chk_Error.AutoSize = true;
            this.chk_Error.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_Error.Location = new System.Drawing.Point(1636, 28);
            this.chk_Error.Name = "chk_Error";
            this.chk_Error.Size = new System.Drawing.Size(101, 35);
            this.chk_Error.TabIndex = 2;
            this.chk_Error.Text = "Error";
            this.chk_Error.UseVisualStyleBackColor = true;
            // 
            // rdo_Manual
            // 
            this.rdo_Manual.AutoSize = true;
            this.rdo_Manual.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo_Manual.Location = new System.Drawing.Point(176, 29);
            this.rdo_Manual.Name = "rdo_Manual";
            this.rdo_Manual.Size = new System.Drawing.Size(123, 33);
            this.rdo_Manual.TabIndex = 1;
            this.rdo_Manual.TabStop = true;
            this.rdo_Manual.Text = "Manual";
            this.rdo_Manual.UseVisualStyleBackColor = true;
            // 
            // rdo_Auto
            // 
            this.rdo_Auto.AutoSize = true;
            this.rdo_Auto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdo_Auto.Location = new System.Drawing.Point(42, 29);
            this.rdo_Auto.Name = "rdo_Auto";
            this.rdo_Auto.Size = new System.Drawing.Size(90, 33);
            this.rdo_Auto.TabIndex = 0;
            this.rdo_Auto.TabStop = true;
            this.rdo_Auto.Text = "Auto";
            this.rdo_Auto.UseVisualStyleBackColor = true;
            // 
            // frm_Counting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 966);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Counting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Counting";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_Counting_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Transaction)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdo_Manual;
        private System.Windows.Forms.RadioButton rdo_Auto;
        private System.Windows.Forms.CheckBox chk_Error;
        private System.Windows.Forms.Button btn_Void;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgv_Transaction;
        private System.Windows.Forms.Button btn_Nine;
        private System.Windows.Forms.Button btn_Eight;
        private System.Windows.Forms.Button btn_Seven;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Button btn_Zero;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Three;
        private System.Windows.Forms.Button btn_Two;
        private System.Windows.Forms.Button btn_One;
        private System.Windows.Forms.Button btn_Six;
        private System.Windows.Forms.Button btn_Five;
        private System.Windows.Forms.Button btn_Four;
        private System.Windows.Forms.Button btn_Item_Find;
        private System.Windows.Forms.TextBox txt_Item_enter;
        private System.Windows.Forms.Button btn_Save_and_Print;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_Unit_No;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_User;
    }
}