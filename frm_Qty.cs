using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Taking_Tool
{
    public partial class frm_Qty : Form
    {
        public double Qty = 0;
        public bool Accepted = false;
        public frm_Qty( string Item_Name, double Price)
        {
            InitializeComponent();

            AcceptButton = null;
            CancelButton = null;
            FixButtons(this);

            

            lbl_Item_Name.Text = Item_Name + "  (" + Price.ToString("N2") + ")";

            foreach (Button b in new Button[] { btn_Zero, btn_one, btn_Two, btn_Three, btn_Four, btn_Five, btn_Six, btn_Seven, btn_Eight, btn_nine })
                b.Click += Digit_Click;

            //btn_Star.Click += btn_Star_Click;
            btn_Back.Click += btn_Back_Click;
            btn_Enter.Click -= btn_Enter_Click;
            btn_Enter.Click += btn_Enter_Click;

            txt_Qty.KeyPress += txt_Qty_KeyPress;
            Shown += delegate { txt_Qty.Focus(); };
        }
        private void FixButtons(Control parent)
        {
            foreach(Control c in parent.Controls)
            {
                Button b = c as Button;
                if(b != null)
                {
                    if (b.DialogResult == DialogResult.Cancel) b.Click += delegate { Close(); };
                    b.DialogResult = DialogResult.None;
                }
                else
                {
                    FixButtons(c);
                }
            }
        }
        private void Digit_Click(object sendder, EventArgs e)
        {
            if (txt_Qty.Text.Length < 10) txt_Qty.Text += ((Button)sendder).Text;
            FocusEnd();
        }
        //private void btn_Star_Click(object sender, EventArgs e)
        //{
        //    if (txt_Qty.Text.Length > 0 && !txt_Qty.Text.Contains("*"))
        //        txt_Qty.Text += "*";
        //    FocusEnd();
        //}
        private void btn_Back_Click(object sender, EventArgs e)
        {
            if (txt_Qty.Text.Length > 0)
                txt_Qty.Text = txt_Qty.Text.Substring(0, txt_Qty.Text.Length - 1);
            FocusEnd();
        }
        private void FocusEnd()
        {
            txt_Qty.Focus();
            txt_Qty.SelectionStart = txt_Qty.Text.Length;
        }
        private void txt_Qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;

            //if (e.KeyChar == '*' && txt_Qty.Text.Length > 0 && !txt_Qty.Text.Contains("*")) return;

            e.Handled = true;
        }
        private void btn_Enter_Click(object sender, EventArgs e)
        {
            double q;
            if(!TryGetQty(txt_Qty.Text, out q) || q <= 0)
            {
                MessageBox.Show("Enter the Qty Correct Format. (ex: 12 )");
                txt_Qty.Focus();
                return;
            }
            Qty = q;
            Accepted = true;
            DialogResult = DialogResult.OK;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btn_Enter_Click(this, EventArgs.Empty);
                return true;
            }
            if (keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
                return true;

            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private static bool TryGetQty(string text, out double result)
        {
            result = 0;
            string[] parts = text.Split('*');

            if (parts.Length == 1) return double.TryParse(parts[0], out result);

            double a, b;
            if(parts.Length == 2 && double.TryParse(parts[0],out a) && double.TryParse(parts[1], out b))
            {
                result = a * b;
                return true;
            }
            return false;
        }
    }
}
