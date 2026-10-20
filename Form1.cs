using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Stock_Taking_Tool
{
    public partial class frm_Stock_Taking_Tool : Form
    {
        public frm_Stock_Taking_Tool()
        {
            InitializeComponent();
        }
        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var f = new frm_Setting())
            {
                f.ShowDialog();
            }

        }
        private async void btn_Download_Click(object sender, EventArgs e)
        {
            if (!cls_MainDb.IsConfigurd)
            {
                MessageBox.Show("Add mainserver Connection AND Databse");
                return;
            }

            btn_Download.Enabled = false;
            pb_Download.Value = 0;
            pb_Download.Visible = true;

            var progress = new Progress<KeyValuePair<int, string>>(p =>
            {
                pb_Download.Value = Math.Max(0, Math.Min(100, p.Key));
            });
            try
            {
                string connStr = cls_MainDb.ConnectionString;
                int[] result = await Task.Run(() => DoDownload(connStr, progress));

                pb_Download.Value = 100;
                MessageBox.Show(result[0] + "new Item Download.\n(Main DB: " + result[1] + "items)");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Download Faild:\n " + ex.Message);
            }
            finally
            {
                pb_Download.Visible = false;
                btn_Download.Enabled = true;
            }
            
        }
        private int[] DoDownload(string connStr, IProgress<KeyValuePair<int, string>> progress)
        {
            Action<int, string> report = (pct, msg) => progress.Report(new KeyValuePair<int, string>(pct, msg));

            HashSet<string> local = cls_DbCreate.GetlocalBarcode();
            var rows = new List<string[]>();
            int mainCount = 0;

            using (var main = new SqlConnection(connStr))
            {
                main.Open();

                int total;
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Product WITH (NOLOCK) WHERE Prod_Code IS NOT Null", main))
                {
                    cmd.CommandTimeout = 300;
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
                
                using (var cmd =  new SqlCommand(@"SELECT Prod_Code,Prod_Name,Barcode,Selling_Price FROM dbo.Product WITH (NOLOCK) WHERE Prod_Code IS NOT NULL", main))
                {
                    cmd.CommandTimeout = 300;
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            mainCount++;
                            string bc = rd.GetValue(0).ToString();
                            string code = rd.IsDBNull(0) ? "" : rd.GetValue(0).ToString().Trim();

                            if (code != "" && !local.Contains(code))
                            {
                                rows.Add(new string[]
                                {
                                    code,
                                    rd.IsDBNull(1) ? "" : rd.GetValue(1).ToString(),
                                    rd.IsDBNull(2) ? "" : rd.GetValue(2).ToString(),
                                    rd.IsDBNull(3) ? "" : rd.GetValue(3).ToString(),
                                });
                            }
                            if (total > 0 && mainCount % 2000 == 0) report((int)(70L * mainCount / total), "Reading main DB.." + mainCount + "/" + total);
                        }
                    }
                }
            }
            report(70, rows.Count + "  Find the new Items");
            int added = cls_DbCreate.AddNewProduct(rows, done =>
            {
                if (rows.Count > 0) report(70 + (int)(30L * done / rows.Count), "Saving..." + done + "/" + rows.Count);
            });

            return new int[] { added, mainCount };
        }
        private void btn_Counting_Click(object sender, EventArgs e)
        {
            cls_Session.Cashier_Name = "";

            using (var lg = new frm_Cashier_Login())
            {
                if (lg.ShowDialog(this) != DialogResult.OK) return;
            }
            using (var f = new frm_Counting())
            {
                f.ShowDialog(this);
            }

        }

        private async void btn_Cashier_Download_Click(object sender, EventArgs e)
        {
            //if (!cls_Session.RequireAdmin(this)) return;
            if (!cls_MainDb.IsConfigurd)
            {
                MessageBox.Show("Setup to Main DB Connection...");
                return;
            }
            btn_Cashier_Download.Enabled = false;
            btn_Download.Enabled = false;
            pb_Download.Value = 0;
            pb_Download.Visible = true;

            var progress = new Progress<int>(p => pb_Download.Value = Math.Max(0, Math.Min(100, p)));

            try
            {
                string connStr = cls_MainDb.ConnectionString;
                int[] result = await Task.Run(() => DoDownloadCashier(connStr, progress));

                pb_Download.Value = 100;
                MessageBox.Show(result[0] + "Faind Casher." + (result[1] > 0 ? "\n" + result[1] + " Find Same Password then Skiped it" : ""));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cashier Download Faield:\n" + ex.Message);
            }
            finally
            {
                pb_Download.Visible = false;
                btn_Cashier_Download.Enabled = true;
                btn_Download.Enabled = true;
            }
        }

        private int[] DoDownloadCashier(string connStr,IProgress<int> progress)
        {
            var rows = new List<string[]>();

            using(var main = new SqlConnection(connStr))
            {
                main.Open();
                progress.Report(10);

                int total;
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.tb_CashierDetails WITH (NOLOCK)", main))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
                using (var cmd= new SqlCommand("SELECT Emp_Name AS N, Pass_Word AS p FROM dbo.tb_CashierDetails WITH (NOLOCK)", main))
                using (var rd = cmd.ExecuteReader())
                {
                    int IN = rd.GetOrdinal("N"), IP = rd.GetOrdinal("P");
                    int n = 0;
                    while (rd.Read())
                    {
                        n++;
                        rows.Add(new string[]
                        {
                            rd.IsDBNull(IN) ? "" : rd.GetValue(IN).ToString(),
                            rd.IsDBNull(IP) ? "" : rd.GetValue(IP).ToString()
                        });
                        if (total > 0) progress.Report(10 + (int)(50L * n / total));
                    }
                }
            }
            int skipped = cls_DbCreate.ReplaceCashier(rows, done =>
            {
                if (rows.Count > 0) progress.Report(60 + (int)(40L * done / rows.Count));
            });
            return new int[] { rows.Count, skipped };
        }
    }
}
