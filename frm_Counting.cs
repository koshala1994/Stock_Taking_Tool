using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Stock_Taking_Tool.cls_DbCreate;

namespace Stock_Taking_Tool
{
    public partial class frm_Counting : Form
    {
        private DataTable printData;
        private int printIndex;
        private double printSubTotal;
        public frm_Counting()
        {
            InitializeComponent();
        }
        private void frm_Counting_Load(object sender, EventArgs e)
        {
            btn_Item_Find.Click += btn_Item_Find_Click;
            btn_Void.Click += btn_Void_Click;
            btn_Save.Click += btn_Save_Click;
            btn_Save_and_Print.Click += btn_Save_and_Print_Click;
            btn_Clear.Click += btn_Clear_Click;
            btn_Back.Click += btn_Back_Click;
            txt_Item_enter.KeyDown += txt_Item_enter_KeyDown;
            chk_Error.CheckedChanged += delegate { txt_Item_enter.Focus(); };

            foreach (Button b in new Button[] { btn_Zero, btn_One, btn_Two, btn_Three, btn_Four, btn_Five, btn_Six, btn_Seven, btn_Eight, btn_Nine })
                b.Click += KeyPad_Click;

            dgv_Transaction.ReadOnly = true;
            dgv_Transaction.AllowUserToAddRows = false;
            dgv_Transaction.MultiSelect = false;
            dgv_Transaction.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // make total column width same as dgv full width

            dgv_Transaction.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Transaction.RowHeadersVisible = false;
            // ---------------

            lbl_Unit_No.Text = cls_DbCreate.GetUnit_No();
            lbl_User.Text = cls_Session.Cashier_Name;

            rdo_Auto.Checked = true;
            LoadGrid();

            ActiveControl = txt_Item_enter;
            Shown += delegate { txt_Item_enter.Focus(); };
            KeyPreview = true;
            KeyDown += Form_KeyDown;
            dgv_Transaction.MultiSelect = false;
            dgv_Transaction.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


        }
        private bool _busy = false;
        private void txt_Item_enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.Handled = true;
            e.SuppressKeyPress = true;

            if (_busy) return;
            _busy = true; 
            try{ ProcessScan(); }
            finally { _busy = false; }
        }
        private void ProcessScan()
        {
            string text = txt_Item_enter.Text.Trim();

            if (text == "") return;

            if (chk_Error.Checked)
            {
                DoError(text);
                return;
            }
            string Unit = cls_DbCreate.GetUnit_No();
            if (Unit == "")
            {
                MessageBox.Show("Save the unit no Using in Setting Form!!");
                return;
            }

            DataTable dt = cls_DbCreate.FindProduct(text);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Item Not Find");
                txt_Item_enter.SelectAll();
                txt_Item_enter.Focus();
                return;
            }
            cls_Item item;

            using (var f = new frm_Item_Find(dt))
            {
                if (f.ShowDialog(this) != DialogResult.OK) return;

                item = new cls_Item
                {
                    Prod_Code = f.SelCode,
                    Prod_Name = f.SelName,
                    Barcode = f.SelBarcode,
                    Selling_Price = f.SelPrice
                };
            }

            if (rdo_Manual.Checked)
            {
                double qty;
                using (var q = new frm_Qty(item.Prod_Name, item.Selling_Price))
                {
                    q.ShowDialog(this);
                    if(!q.Accepted)
                    {
                        txt_Item_enter.SelectAll();
                        txt_Item_enter.Focus();
                        return;
                    }
                    qty = q.Qty;
                }
                cls_DbCreate.AddorUpdateTempTransaction(Unit, item, qty);
            }
            else
            {
                cls_DbCreate.AddTemp(Unit, item, 1);
            }

            txt_Item_enter.Clear();
            LoadGrid();
            txt_Item_enter.Focus();
        }
        private void KeyPad_Click(object sender, EventArgs e)
        {
            txt_Item_enter.Text += ((Button)sender).Text;
            txt_Item_enter.Focus();
            txt_Item_enter.SelectionStart = txt_Item_enter.Text.Length;
        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Item_enter.Clear();
            txt_Item_enter.Focus();
        }
        private void btn_Back_Click(object sender, EventArgs e)
        {
            if (txt_Item_enter.Text.Length > 0) txt_Item_enter.Text = txt_Item_enter.Text.Substring(0, txt_Item_enter.Text.Length - 1);
            txt_Item_enter.Focus();
            txt_Item_enter.SelectionStart = txt_Item_enter.Text.Length;
        }
        private void btn_Item_Find_Click(object sender, EventArgs e)
        {
            ProcessScan();
        }
        private void DoError(string text)
        {
            double n = 1;
            string key = text;

            int star = text.IndexOf('*');
            if (star > 0)
            {
                double parsed;
                if (!double.TryParse(text.Substring(0,star).Trim(), out parsed) || parsed <= 0)
                {
                    MessageBox.Show("Enter the Data Using ths format 5*1000001 ");
                    txt_Item_enter.Focus();
                    return;
                }
                n = parsed;
                key = text.Substring(star + 1).Trim();
            }
            string msg = cls_DbCreate.ReduceQty(key, n);
            if (msg != "") MessageBox.Show(msg);

            chk_Error.Checked = false;
            txt_Item_enter.Clear();
            LoadGrid();
            txt_Item_enter.Focus();
        }
        private DateTime _laseVoid = DateTime.MinValue;
        public void VoidSelected()
        {
            if ((DateTime.Now - _laseVoid).TotalMilliseconds < 100) return;
            DataGridViewRow row = dgv_Transaction.SelectedRows.Count > 0 ? dgv_Transaction.SelectedRows[0] : dgv_Transaction.CurrentRow;
            if(row == null)
            {
                MessageBox.Show("There is no Sellected Row");
                return;
            }
            DataRowView drv = row.DataBoundItem as DataRowView;
            if (drv == null) return;

            long id = Convert.ToInt64(drv["Id"]);
            cls_DbCreate.VoidRow(id);
            _laseVoid = DateTime.Now;

            LoadGrid();
            txt_Item_enter.Focus();
        }
        private void btn_Void_Click(object sender, EventArgs e)
        {
            VoidSelected();
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                VoidSelected();
            }
            if(e.Control)
            {
                btn_Item_Find.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        private void LoadGrid()
        {
            dgv_Transaction.DataSource = cls_DbCreate.GetTemp();
            if (dgv_Transaction.Columns["id"] != null) dgv_Transaction.Columns["id"].Visible = false;
            var price = dgv_Transaction.Columns["Selling_Price"];
                if(price != null)
                {
                    price.DefaultCellStyle.Format = "N2";
                    price.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            var qty = dgv_Transaction.Columns["Qty"];
            if(qty != null)
            {
                qty.DefaultCellStyle.Format = "0.##";
                qty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            }

            // Data Grid View column sizes change

            if (dgv_Transaction.Columns["Prod_Code"] != null) dgv_Transaction.Columns["Prod_Code"].FillWeight = 80;
            if (dgv_Transaction.Columns["Prod_Name"] != null) dgv_Transaction.Columns["Prod_Name"].FillWeight = 200;
            if (dgv_Transaction.Columns["Barcode"] != null) dgv_Transaction.Columns["Barcode"].FillWeight = 100;
            if (dgv_Transaction.Columns["Qty"] != null) dgv_Transaction.Columns["Qty"].FillWeight = 50;
            if (dgv_Transaction.Columns["Selling_Price"] != null) dgv_Transaction.Columns["Selling_Price"].FillWeight = 80;

            // ----------------------------------

            if (dgv_Transaction.Rows.Count > 0) dgv_Transaction.FirstDisplayedScrollingRowIndex = dgv_Transaction.Rows.Count - 1;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveBill(false);
            txt_Item_enter.Focus();
        }
        private static double ToD(object o)
        {
            return (o == null || o == DBNull.Value) ? 0 : Convert.ToDouble(o);
        }
        private void btn_Save_and_Print_Click(object sender, EventArgs e)
        {
            SaveBill(true);
        }
        private DateTime LastSaved = DateTime.MinValue;
        private void SaveBill(bool printAfter)
        {
            if(dgv_Transaction.Rows.Count ==0)
            {
                if ((DateTime.Now - LastSaved).TotalSeconds > 2) MessageBox.Show("There is a no Data in a Grid");
                return;
            }
            if (MessageBox.Show("Items" + dgv_Transaction.Rows.Count + "Save?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            DataTable snapshot = cls_DbCreate.GetTemp();

            try
            {
                int n = cls_DbCreate.SaveTransaction(cls_Session.Username ?? "");
                LoadGrid();
                if (!printAfter) MessageBox.Show(n + " Rows Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saved Failed : \n" + ex.Message);
                return;
            }
            if (printAfter) PrintData(snapshot);

            txt_Item_enter.Focus();
        }
        private void PrintData(DataTable data)
        {
            if(data == null || data.Rows.Count ==0)
            {
                MessageBox.Show("There is no data ina print");
                return;
            }
            printData = data;

            printSubTotal = 0;
            foreach (DataRow r in printData.Rows) printSubTotal += ToD(r["Qty"]) * ToD(r["Selling_Price"]);

            printIndex = 0;

            var doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;

            using (var dlg = new PrintPreviewDialog())
            {
                dlg.Document = doc;
                doc.Print();
            }
            txt_Item_enter.Focus();
        }
        private void Doc_PrintPage(Object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            float left = e.MarginBounds.Left;
            float width = e.MarginBounds.Width;
            float y = e.MarginBounds.Top;

            float wCode = 110, wQty = 70, wPrice = 100;
            float wName = width - wCode - wQty - wPrice;

            using (Font fTitle = new Font("Arial", 14, FontStyle.Bold))
            using (Font f = new Font("Arial", 10))
            using (Font fb = new Font("Arial", 10, FontStyle.Bold))
            using (var fmtL = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap})
            using (var fmtR = new StringFormat { Trimming = StringTrimming.EllipsisCharacter, FormatFlags = StringFormatFlags.NoWrap, Alignment = StringAlignment.Far})
            {
                float lineH = f.GetHeight(g) + 6;

                if(printIndex == 0)
                {
                    g.DrawString("STOCK COUNT", fTitle, Brushes.Black, Left, y);
                    y += fTitle.GetHeight(g) + 4;
                    g.DrawString("Unit No : " + cls_DbCreate.GetUnit_No() + "  " + DateTime.Now.ToString("dd-MM-yyyy HH:mm"), f, Brushes.Black, left, y);
                    y += lineH;
                    g.DrawLine(Pens.Black, left, y, left + width, y);
                    y += 4;

                    while (printIndex < printData.Rows.Count && y + lineH * 3 <e.MarginBounds.Bottom)
                    {
                        DataRow r = printData.Rows[printIndex];
                        g.DrawString(r["Prod_Code"].ToString(), f, Brushes.Black, new RectangleF(left, y, wCode, lineH), fmtL);
                        g.DrawString(r["Prod_Name"].ToString(), f, Brushes.Black, new RectangleF(left + wCode, y, wName, lineH), fmtL);
                        g.DrawString(ToD(r["Qty"]).ToString("0.##"), f, Brushes.Black, new RectangleF(left + wCode + wName, y, wQty,lineH), fmtR);
                        g.DrawString(ToD(r["Selling_Price"]).ToString("N2"), f, Brushes.Black, new RectangleF(left + wCode + wName + wQty, y, wPrice, lineH), fmtR);

                        y += lineH;
                        printIndex++;
                    }
                    if(printIndex < printData.Rows.Count)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    g.DrawLine(Pens.Black, left, y, left + width, y);
                    y += 4;
                    g.DrawString("Sub Total :", fb, Brushes.Black, new RectangleF(left + wCode, y, wName + wQty, lineH), fmtR);
                    g.DrawString(printSubTotal.ToString("N2"), fb, Brushes.Black, new RectangleF(left + wCode + wName + wQty, y, wPrice, lineH), fmtR);

                    e.HasMorePages = false;
                }
            }
        }
    }
}
