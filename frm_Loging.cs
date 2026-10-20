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
    public partial class frm_Loging : Form
    {
        private TextBox activeTextBox;
        private const string AllowedSymbols = "@#$+*/-!%^&()_=[]{};:'\",.<>?|\\~`";
        private int attempt = 0;
        public frm_Loging()
        {
            InitializeComponent();

            txt_Username.KeyPress += txt_Password_KeyPress;
            txt_Password.KeyPress += txt_Password_KeyPress;

            txt_Username.Enter += TextBox_Enter;
            txt_Password.Enter += TextBox_Enter;

            activeTextBox = txt_Username;
            Shown += delegate { txt_Username.Focus(); };

            HookKeyBordButtons(pnl_Front);
            HookKeyBordButtons(pnl_Back);
        }
        private void txt_Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
                (e.KeyChar >= 'A' && e.KeyChar <= 'Z')) return;
            e.Handled = true;
        }
        private string CheackLoging(string user,string pass)
        {
            if (user == "ADMIN" && pass == "ONIMTA1+")return "ADMIN";
            if (user == "USER" && pass == "1111") return "USER";
            return null;
        }
        private void HookKeyBordButtons(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
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
        private void btn_Login_Click(object sender, EventArgs e)
        {
            string Username = txt_Username.Text.Trim();
            string role = CheackLoging(Username, txt_Password.Text);

            if(role != null)
            {
                cls_Session.Username = Username;
                cls_Session.Role= role;
                DialogResult = DialogResult.OK;
                return;
            }
            attempt++;
            if(attempt >= 3)
            {
                MessageBox.Show("Too many faild attempt!!!");
                DialogResult = DialogResult.Cancel;
                return;
            }
            MessageBox.Show("Username or Password is incorrect.");
            txt_Password.Clear();
            txt_Password.Focus();

        }
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Username.Clear();
            txt_Password.Clear();
        }
        private void TextBox_Enter(object sender, EventArgs e)
        {
            activeTextBox = (TextBox)sender;
        }
        private void btn_flip_Click(object sender, EventArgs e)
        {
            pnl_Back.Hide();
            pnl_Front.Show();
        }
        private void btn_symbol_Click(object sender, EventArgs e)
        {
            pnl_Front.Hide();
            pnl_Back.Show();
        }
        private void btn_Space_Click(object sender, EventArgs e)
        {
            InsertText(" ");
        }
        private void InsertText(string text)
        {
            if (activeTextBox == null) return;
            int pos = activeTextBox.SelectionStart;
            activeTextBox.Text = activeTextBox.Text.Remove(pos, activeTextBox.SelectionLength).Insert(pos, text);
            activeTextBox.SelectionStart = pos + text.Length;
            activeTextBox.Focus();
        }
    }
}
