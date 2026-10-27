using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock_Taking_Tool
{
    public partial class frm_Setting : Form
    {
        public frm_Setting()
        {
            InitializeComponent();
        }
        private bool _loadingExport = false;
        private void frm_Setting_Load(object sender, EventArgs e)
        {
            txt_Servername.Text = cls_MainDb.Server;
            txt_Databese.Text = cls_MainDb.Database;
            btn_Save_Connection.Enabled = false;
            txt_Machine_id.ReadOnly = true;
            btn_Cheack_MachineID.Enabled = true;
            string unit, machineid;
            if (cls_DbCreate.GetSystemDetails(out unit, out machineid))
            {
                txt_Unit_No.Text = unit;
                txt_Machine_id.Text = machineid;
                LockContrals();
            }
            else
            {
                btn_Save.Enabled = false;
            }
            InitExporTab();
        }
        private void txt_Servername_TextChanged(object sender, EventArgs e)
        {
            btn_Save_Connection.Enabled = true;
        }
        private void btn_Test_Connection_Click(object sender, EventArgs e)
        {
            if (txt_Servername.Text.Trim() == "" || txt_Databese.Text.Trim() == "")
            {
                MessageBox.Show("Please enter server name and database name.");
                return;
            }
            btn_Save_Connection.Enabled = false;
            // Cursor == Cursors.WaitCursor;

            try
            {
                string cs = cls_MainDb.BuildConnectionString(txt_Servername.Text, txt_Databese.Text);
                using (var con = new SqlConnection(cs))
                {
                    con.Open();
                }
                btn_Save_Connection.Enabled = true;
                MessageBox.Show("Connection successful.");
            }
            catch (Exception ex)
            {
                btn_Save_Connection.Enabled = false;
                MessageBox.Show("Connection failed:\n " + ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void btn_Save_Connection_Click(object sender, EventArgs e)
        {
            cls_MainDb.Save(txt_Servername.Text, txt_Databese.Text);
            MessageBox.Show("Saved");
            DialogResult = DialogResult.OK;
        }
        private void btn_Cheack_MachineID_Click(object sender, EventArgs e)
        {
            string id = cls_MachineInfo.GetprocessorId();
            if (id == "")
            {
                MessageBox.Show("Cant Taking Machine Id..!!");
                return;
            }
            txt_Machine_id.Text = id;
            txt_Unit_No.ReadOnly = false;
            txt_Unit_No.Focus();
            btn_Save.Enabled = true;
        }
        private void txtUnitNo_TextChanged(object sender, EventArgs e)
        {
            UpdateSaveStatus();
        }
        private void UpdateSaveStatus()
        {
            btn_Save.Enabled = txt_Unit_No.ReadOnly && txt_Unit_No.Text.Trim() != "" && txt_Machine_id.Text.Trim() != "";
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("if unit number is saved", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                cls_DbCreate.SaveSystemDetails(txt_Unit_No.Text, txt_Machine_id.Text);
                LockContrals();
                MessageBox.Show("Saved Details");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Saved Failed: \n" + ex.Message);
            }
        }
        private void LockContrals()
        {
            txt_Unit_No.ReadOnly = true;
            txt_Machine_id.ReadOnly = true;
            btn_Save.Enabled = false;
        }
        private static bool GetExportLocked()
        {
            try
            {
                object v = Properties.Settings.Default["GetExportLocked"];
                return v != null && Convert.ToBoolean(v);
            }
            catch (Exception)
            {

                return false;
            }
        }
        private void InitExporTab()
        {
            btn_Brows.Click -= btn_Brows_Click;
            btn_Brows.Click += btn_Brows_Click;

            btn_Save_Path.Click -= btn_Save_Path_Click;
            btn_Save_Path.Click += btn_Save_Path_Click;

            chk_Lock.CheckedChanged -= chk_Lock_CheckedChanged;
            chk_Lock.CheckedChanged += chk_Lock_CheckedChanged;

            txt_Path.ReadOnly = true;

            _loadingExport = true;

            Properties.Settings.Default.Reload();
            txt_Path.Text = Properties.Settings.Default.ExportPath ?? "";
            chk_Lock.Checked = GetExportLocked();

            _loadingExport = false;

            ApplyLock();
        }
        private void ApplyLock()
        {
            bool locked = chk_Lock.Checked;
            
            btn_Save_Path.Enabled = !locked;
            btn_Brows.Enabled = !locked;
            
            txt_Path.ReadOnly = true;
        }
        private void chk_Lock_CheckedChanged(object sender, EventArgs e)
        {
            if(_loadingExport)
            {
                return;
            }
            if(chk_Lock.Checked && string.IsNullOrWhiteSpace(Properties.Settings.Default.ExportPath))
            {
                MessageBox.Show("Plase Save the Export Path first.");

                _loadingExport = true;
                chk_Lock.Checked = false;
                _loadingExport = false;

                ApplyLock();
                return;
            }
            Properties.Settings.Default.ExportLocked = chk_Lock.Checked;
            Properties.Settings.Default.Save();

            ApplyLock();
        }
        private void btn_Brows_Click(object sender, EventArgs e)
        {
            using(var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "select the save path in excel";
                if (!string.IsNullOrWhiteSpace(txt_Path.Text) && Directory.Exists(txt_Path.Text)) dlg.SelectedPath = txt_Path.Text;

                if (dlg.ShowDialog(this) == DialogResult.OK) txt_Path.Text = dlg.SelectedPath;
            }
        }
        private void btn_Save_Path_Click(object sender, EventArgs e)
        {
            string p = txt_Path.Text.Trim();
            if(string.IsNullOrWhiteSpace(p))
            {
                MessageBox.Show("brows the Path");
                return;
            }
            try
            {
                if (!Directory.Exists(p))
                {
                    Directory.CreateDirectory(p);
                }
                Properties.Settings.Default.ExportPath = p;
                Properties.Settings.Default.ExportLocked = true;

                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();

                MessageBox.Show("Saved Path: \n" + Properties.Settings.Default.ExportPath);

                _loadingExport = true;
                txt_Path.Text = Properties.Settings.Default.ExportPath;
                chk_Lock.Checked = Properties.Settings.Default.ExportLocked;
                _loadingExport = false;

                ApplyLock();

                MessageBox.Show("Export Path Sved Successfully! \n" + p);
            }
            catch (Exception ex)
            {
                _loadingExport = false;
                MessageBox.Show("Canot Save Export: \n" + ex.Message);
            }
        }
    }
}
