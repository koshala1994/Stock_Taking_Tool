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
using System.Globalization;
using System.IO;
using ClosedXML.Excel;

namespace Stock_Taking_Tool
{
    public partial class frm_Stock_Taking_Tool : Form
    {
        public frm_Stock_Taking_Tool()
        {
            InitializeComponent();
            ApplyModernDesign();
        }
        private void ApplyModernDesign()
        {
            // Main Form - existing size and layout preserved
            this.Text = "Onimta Stock Taking Tool";
            this.BackColor = Color.FromArgb(243, 247, 252);
            this.Font = new Font("Segoe UI", 9.5F);

            // Apply modern styles to existing controls
            ApplyControlStyles(this.Controls);
        }

        private void ApplyControlStyles(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                // MenuStrip: keep existing File, Report, About items
                if (c is MenuStrip menu)
                {
                    menu.BackColor = Color.White;
                    menu.ForeColor = Color.FromArgb(35, 49, 69);
                    menu.Font = new Font("Segoe UI", 9.5F);
                    menu.RenderMode = ToolStripRenderMode.Professional;
                }

                // Existing ProgressBar: keep its position and size
                if (c is ProgressBar progress)
                {
                    progress.Minimum = 0;
                    progress.Maximum = 100;
                    progress.Style = ProgressBarStyle.Continuous;
                }

                // Style existing buttons without changing their size
                if (c is Button btn)
                {
                    string caption = btn.Text.ToLowerInvariant();
                    Color buttonColor = Color.FromArgb(66, 85, 110);

                    if (caption.Contains("item") &&
                        caption.Contains("download"))
                    {
                        buttonColor = Color.FromArgb(45, 112, 220);
                    }
                    else if (caption.Contains("cashier"))
                    {
                        buttonColor = Color.FromArgb(25, 157, 128);
                    }
                    else if (caption.Contains("stock") ||
                             caption.Contains("counting"))
                    {
                        buttonColor = Color.FromArgb(119, 86, 218);
                    }
                    else if (caption.Contains("export"))
                    {
                        buttonColor = Color.FromArgb(238, 139, 35);
                    }

                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor =
                        ControlPaint.Dark(buttonColor, 0.08F);

                    btn.BackColor = buttonColor;
                    btn.ForeColor = Color.White;
                    btn.UseVisualStyleBackColor = false;
                    btn.Font = new Font("Segoe UI Semibold", 10.5F);
                    btn.Cursor = Cursors.Hand;

                    // Intentionally do not change:
                    // btn.Size, btn.Location, btn.Name,
                    // btn.Click or existing image settings.
                }

                // Style existing panels without changing layout
                if (c is Panel panel)
                {
                    panel.BackColor = Color.FromArgb(243, 247, 252);
                }

                // Apply recursively to controls inside panels
                if (c.HasChildren)
                {
                    ApplyControlStyles(c.Controls);
                }
            }
        }
        private bool AdminLoging()
        {
            cls_Session.Role = null;

            using (var lg = new frm_Loging())
            {
                if (lg.ShowDialog(this) != DialogResult.OK) return false;
            }
            if(!cls_Session.IsAdmin)
            {
                MessageBox.Show("All Access are Admin Login Only!!!!");
                return false;
            }
            return true;
        }
        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AdminLoging()) return;
            
            using (var f = new frm_Setting())
            {
                f.ShowDialog(this);
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
                MessageBox.Show(ex.ToString(),"Download Eror", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private async void btn_Export_Click(object sender, EventArgs e)
        {
            string folderpath = Properties.Settings.Default.ExportPath;
            MessageBox.Show("Saved Path: \n [" + folderpath + "]");
            if(string.IsNullOrWhiteSpace(folderpath))
            {
                MessageBox.Show("Setup the  Export Path in Setting Tab");
                return;
            }
            string filename  =  "Stock_Taking_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xlsx";

            string path = Path.Combine(folderpath.Trim(), filename);
            btn_Export.Enabled = false;
            pb_Download.Value = 0;
            pb_Download.Visible = true;

            var progress = new Progress<int>(p => pb_Download.Value = Math.Max(0, Math.Min(100, p)));

            try
            {
                int n = await Task.Run(() => DoExport(path, progress));

                if(n == 0)
                {
                    MessageBox.Show("There is no Data to the Export");
                }
                else
                {
                    pb_Download.Value = 100;
                    MessageBox.Show(n + "row export una: \n" + path);
                }
            }
            catch (IOException )
            {
                MessageBox.Show("There is a file open with a nother tab (ex: Excel).close and try again");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Export Faild \n" + ex.Message);
            }
            finally
            {
                pb_Download.Visible = false;
                btn_Export.Enabled = true;
            }
        }
        private int DoExport(string path, IProgress<int> progress)
        {
            progress?.Report(5);

            if(string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Export file path is empty");
            }
            path = path.Trim();

            
            if (!Path.IsPathRooted(path))
            {
                throw new ArgumentException("Export Path must be absolute path: " + path);
            } 
            
            if(!string.Equals(Path.GetExtension(path), ".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Expected a complete .xlsx file path, but received: " + path);
            }
            string dir = Path.GetDirectoryName(path);

            if (string.IsNullOrWhiteSpace(dir))
            {
                throw new ArgumentException("Export folder is Invalid: " + path);
            } 

            Directory.CreateDirectory(dir);
            DataTable dt = cls_DbCreate.GetSavedTransaction();

            int total = dt.Rows.Count;

            if(total == 0)
            {
                return 0;
            }
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Stock_Transaction");

                string[] head = { "NO","Unit No", "Prod Code", "Prod Name", "Barcode", "Qty", "Selling Price", "Amount", "User", "Saved At", "Batch No"};
                for(int c = 0; c < head.Length; c++)
                {
                    var h = ws.Cell(1, c + 1);
                    h.Value = head[c];
                    h.Style.Font.Bold = true;
                    h.Style.Fill.BackgroundColor = XLColor.LightSeaGreen;
                }
                progress.Report(10);

                double totalQty = 0, totalAmt = 0;
                int row = 2, i = 0;
                foreach(DataRow r in dt.Rows)
                {
                    double qty = Convert.ToDouble(r["Qty"]);
                    double price = Convert.ToDouble(r["Selling_Price"]);
                    double amt = qty * price;
                    totalQty += qty;
                    totalAmt += amt;

                    ws.Cell(row, 1).Value = row - 1;
                    ws.Cell(row, 2).Value = Convert.ToString(r["Unit_No"]);
                    ws.Cell(row, 3).Value = Convert.ToString(r["Prod_Code"]);
                    ws.Cell(row, 4).Value = Convert.ToString(r["Prod_Name"]);
                    ws.Cell(row, 5).Value = Convert.ToString(r["Barcode"]);
                    ws.Cell(row, 6).Value = qty;
                    ws.Cell(row, 7).Value = price;
                    ws.Cell(row, 8).Value = amt;
                    ws.Cell(row, 9).Value = Convert.ToString(r["User_Name"]);
                    ws.Cell(row, 10).Value = Convert.ToString(r["Saved_At"]);
                    ws.Cell(row, 11).Value = Convert.ToString(r["Batch_No"]);

                    ws.Cell(row, 5).Style.NumberFormat.Format = "@";          
                    ws.Cell(row, 6).Style.NumberFormat.Format = "0.##";
                    ws.Cell(row, 7).Style.NumberFormat.Format = "#,##0.00";
                    ws.Cell(row, 8).Style.NumberFormat.Format = "#,##0.00";

                    row++;
                    i++;
                    if (i % 100 == 0) progress.Report(10 + (int)(80L * 1 / total));
                }
                ws.Cell(row, 5).Value = "Total";
                ws.Cell(row, 6).Value = totalQty;
                ws.Cell(row, 8).Value = totalAmt;
                ws.Cell(row, 6).Style.NumberFormat.Format = "0.##";
                ws.Cell(row, 8).Style.NumberFormat.Format = "#,##0.00";
                ws.Range(row, 5, row, 8).Style.Font.Bold = true;
                progress.Report(92);

                ws.SheetView.FreezeRows(1);
                if(total <= 5000)
                {
                    ws.Columns().AdjustToContents();
                }
                else
                {
                    double[] w = { 6, 10, 14, 40, 18, 8, 14, 14, 18, 20, 16 };
                    for (int c = 0; c < w.Length; c++) ws.Column(c + 1).Width = w[c];
                }
                progress.Report(96);
                MessageBox.Show("Path : ["+ path +"]\n" + "Extension: [" + Path.GetExtension(path)+ "]");
                wb.SaveAs(path);
            }
            progress.Report(100);
            return total;
        }
    }
}
