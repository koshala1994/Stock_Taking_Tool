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
    public partial class frm_Cashier_Login : Form
    {
        private const string AllowedSymbols = "@#$+*/-!%^&()_=[]{};:'\",.<>?|\\~`";
        public frm_Cashier_Login()
        {
            InitializeComponent();
            txt_Password.KeyPress += txt_Password_KeyPress;
            Shown += delegate { txt_Password.Focus(); };

            HookKeyBordButtons(pnl_Front);
            HookKeyBordButtons(pnl_Back);
        }
        private void HookKeyBordButtons(Control parent)
        {
            foreach(Control c in parent.Controls)
            {
                if(c is Button btn)
                {
                    if (btn == btn_symbol || btn == btn_flip || btn == btn_Space) continue;
                    btn.Click += KeyboardButton_Click;
                }
                else if (c.HasChildren)
                {
                    HookKeyBordButtons(c);
                }
            }
        }
        private void KeyboardButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            InsertText(btn.Text);
        }
        private void btn_symbol_Click(object sender, EventArgs e)
        {
            pnl_Front.Hide();
            pnl_Back.Show();
            txt_Password.Focus();
        }
        private void btn_flip_Click(object sender, EventArgs e)
        {
            pnl_Front.Show();
            pnl_Back.Hide();
            txt_Password.Focus();
        }
        private void frm_Cashier_Login_Load(object sender, EventArgs e)
        {
            txt_Password.Focus();
        }
        private void txt_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
                (e.KeyChar >= 'A' && e.KeyChar <= 'Z')) return;
            if (e.KeyChar == '+' ||
                e.KeyChar == '-' ||
                e.KeyChar == '*' ||
                e.KeyChar == '/' ||
                e.KeyChar == '=' ||
                e.KeyChar == '!' ||
                e.KeyChar == '@' ||
                e.KeyChar == '#' ||
                e.KeyChar == '$' ||
                e.KeyChar == '%' ||
                e.KeyChar == '^' ||
                e.KeyChar == '&' ||
                e.KeyChar == '(' ||
                e.KeyChar == ')') return;
            e.Handled = true;
        }
        private void btn_Login_Click(object sender, EventArgs e)
        {
            string pw = txt_Password.Text.Trim();
            if(pw == "")
            {
                MessageBox.Show("Enter the Password");
                txt_Password.Focus();
                return;
            }

            string name = cls_DbCreate.FindCashierByPassword(pw);
            if(name == "")
            {
                MessageBox.Show("Invalid Password!!");
                txt_Password.Clear();
                txt_Password.Focus();
                return;
            }

            cls_Session.Cashier_Name = name;
            DialogResult = DialogResult.OK;
        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Password.Clear();
        }
        private void btn_Space_Click(object sender, EventArgs e)
        {
            InsertText(" ");
        }
        private void InsertText(string text)
        {
            int pos = txt_Password.SelectionStart;
            txt_Password.Text = txt_Password.Text.Remove(pos, txt_Password.SelectionLength).Insert(pos, text);
            txt_Password.SelectionStart = pos + text.Length;
            txt_Password.Focus();
        }
    }
}
