using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Diagnostics;

namespace Stock_Taking_Tool
{
    public static class cls_DbCreate
    {
        public static readonly string Folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "StockApp");
        public static readonly string FilePath = Path.Combine(Folder, "Stock.db");
        public static string ConnStr 
        {
            get { return "Data Source=" + FilePath + ";Version=3;"; }
        }
        public static void Init() 
        {
            //SQLitePCL.Batteries.Init();
            Directory.CreateDirectory(Folder);

            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();

                string sql = @"CREATE TABLE IF NOT EXISTS Product(Prod_Code TEXT PRIMARY KEY, Prod_Name TEXT, Barcode TEXT, Selling_Price REAL);
                               CREATE INDEX IF NOT EXISTS Idx_Product_Barcode ON Product(Barcode);
                               CREATE TABLE IF NOT EXISTS System_Details (Id INTEGER PRIMARY KEY CHECK(Id=1),Unit_No TEXT,Machine_Id TEXT,UpdatedAt TEXT);
                               CREATE TABLE IF NOT EXISTS Pos_Employee (Emp_Name TEXT, Password TEXT PRIMARY KEY);
                               CREATE TABLE IF NOT EXISTS Temp_Transaction (Id INTEGER PRIMARY KEY AUTOINCREMENT, Unit_No TEXT,Prod_Code TEXT,Prod_Name TEXT,Barcode TEXT,Qty REAL,Selling_Price REAL);
                               CREATE TABLE IF NOT EXISTS Stock_Transaction (Id INTEGER PRIMARY KEY AUTOINCREMENT, Unit_No TEXT,Prod_Code TEXT,Prod_Name TEXT,Barcode TEXT,Qty REAL,Selling_Price REAL,User_Name TEXT,Saved_At TEXT, Batch_No TEXT);
                               CREATE TABLE IF NOT EXISTS Backup_Stock_Transaction (Id INTEGER PRIMARY KEY AUTOINCREMENT, Unit_No TEXT,Prod_Code TEXT,Prod_Name TEXT,Barcode TEXT,Qty REAL,Selling_Price REAL,User_Name TEXT,Saved_At TEXT, Batch_No TEXT);";

                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public class cls_Item
        {
            public string Prod_Code;
            public string Prod_Name;
            public string Barcode;
            public double Selling_Price;
        }
        public static HashSet<string> GetLocalCodes()
        {
            var Set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SQLiteCommand("SELECT Prod_Code FROM Product", con))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) Set.Add(rd.GetString(0).Trim());
                }
            }
                return Set;
        }
        public static bool GetProduct(string barcode,out string code, out string name, out double price)
        {
            code = ""; name = ""; price = 0;
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SQLiteCommand("SELECT Prod_Code,Prod_Name,Selling_Price FROM Product WHERE Barode = @b", con))
                {
                    cmd.Parameters.AddWithValue("@b", barcode.Trim());
                    using (var rd = cmd.ExecuteReader())
                    {
                        if (!rd.Read()) return false;
                        code = rd.IsDBNull(0) ? "" : rd.GetValue(0).ToString();
                        name = rd.IsDBNull(1) ? "" : rd.GetValue(1).ToString();
                        price = rd.IsDBNull(2) ? 0 : Convert.ToDouble(rd.GetValue(2));
                        return true;
                    }
                }
            }
        }
        public static void ReplaceProducts(List<string[]> rows)
        {
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    using (var del = new SQLiteCommand("DELETE FROM Product", con, tx))
                    {
                        del.ExecuteNonQuery();
                    }
                    using (var ins = new SQLiteCommand("INSERT OR REPLACE INTO Product (Prod_Code,Prod_Name,Barcode,Selling_Price) VALUES(@c,@n,@b,@p)", con, tx))
                    {
                        ins.Parameters.Add("@c", DbType.String);
                        ins.Parameters.Add("@n", DbType.String);
                        ins.Parameters.Add("@b", DbType.String);
                        ins.Parameters.Add("@p", DbType.Double);

                        foreach (var r in rows)
                        {
                            if (string.IsNullOrWhiteSpace(r[2])) continue;

                            double price;
                            double.TryParse(r[3], out price);

                            ins.Parameters["@c"].Value = r[0].Trim(); 
                            ins.Parameters["@n"].Value = r[1].Trim();
                            ins.Parameters["@b"].Value = r[2].Trim();
                            ins.Parameters["@p"].Value = price;
                                ins.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
        }
        public static int AddNewProduct(List<string[]> rows, Action<int> onProgress = null)
        {
            int Added = 0, processed = 0;
            using (var con = new SQLiteConnection(ConnStr)) 
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                using (var ins = new SQLiteCommand("INSERT OR IGNORE INTO Product(Prod_Code,Prod_Name,Barcode,Selling_Price) VALUES (@c,@n,@b,@p)", con, tx))
                {
                    var pc = ins.Parameters.AddWithValue("@c", "");
                    var pn = ins.Parameters.AddWithValue("@n", "");
                    var pb = ins.Parameters.AddWithValue("@b", "");
                    var pp = ins.Parameters.AddWithValue("@p", 0.0);
                    foreach (var r in rows)
                   {
                       processed++;
                       if (onProgress != null && processed % 200 == 0) onProgress(processed);
                       string code = (r[0] ?? "").Trim();
                       if (code == "") continue;

                       double price;
                       double.TryParse(r[3], out price);

                        pc.Value = code;
                        pn.Value = (r[1] ?? "").Trim();
                        pb.Value = (r[2] ?? "").Trim();
                        pp.Value = price;
                       try
                       {
                          Added += ins.ExecuteNonQuery();
                       }
                       catch (Exception ex)
                       {
                           throw new Exception("Insert faild row: #" + processed + "\nCode=[" + r[0] + "]\nname=[" + r[1] + "]\nbarcode=[" + r[2] + "]\nprice=[" + r[3] + "]" + "\n\n" + ex.ToString(), ex) ;
                       }
                   }
                    tx.Commit();
                }
            }
            return Added;
        }
        public static HashSet<string> GetlocalBarcode()
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SQLiteCommand("SELECT Barcode FROM Product", con))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) set.Add(rd.GetString(0).Trim());
                }
            }
            return set;
        }
        public static void SaveSystemDetails(string unitNo, string machineId)
        {
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                string sql = @"INSERT INTO System_Details(Id,Unit_No,Machine_Id,UpdatedAt) VALUES(1,@u,@m,@t) ON CONFLICT(Id) DO UPDATE SET Unit_No=@u, Machine_Id=@m, UpdatedAt=@t";

                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@u", unitNo.Trim());
                    cmd.Parameters.AddWithValue("@m", machineId.Trim());
                    cmd.Parameters.AddWithValue("@t", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static bool GetSystemDetails(out string unitNo, out string machineId)
        {
            unitNo = ""; machineId = "";
            using (var con = new SQLiteConnection (ConnStr))
            {
                con.Open();
                using (var cmd = new SQLiteCommand("SELECT Unit_No,Machine_Id FROM System_Details WHERE Id=1", con))
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return false;
                    unitNo = rd.IsDBNull(0) ? "" : rd.GetString(0);
                    machineId = rd.IsDBNull(1) ? "" : rd.GetString(1);
                    return true;
                }
            }
        }
        public static string GetUnit_No()
        {
            string Unit_No, Machine_Id;
            GetSystemDetails(out Unit_No, out Machine_Id);
            return Unit_No;
        }
        public static int ReplaceCashier(List<string[]> rows, Action<int> onProgress = null)
        {
            int skipped = 0, processed = 0;
            var seen = new HashSet<string>();

            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    using (var del = new SQLiteCommand("DELETE FROM Pos_Employee", con,tx))
                    {
                        del.ExecuteNonQuery();
                    }
                    using (var ins = new SQLiteCommand("INSERT INTO Pos_Employee (Emp_Name, Password) VALUES(@n,@p)", con,tx))
                    {
                        var pn = ins.Parameters.AddWithValue("@n", "");
                        var pp = ins.Parameters.AddWithValue("@p", "");

                        foreach(var r in rows)
                        {
                            processed++;
                            if (onProgress != null && processed % 20 == 0) onProgress(processed);
                            string name = (r[0] ?? "").Trim();
                            string pass = (r[1] ?? "").Trim();
                            if (name == "" || pass == "") continue;
                            if(!seen.Add(pass)) { skipped++; continue; }

                            pn.Value = name;
                            pp.Value = pass;
                            ins.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
            }
            if (onProgress != null) onProgress(rows.Count);
            return skipped;
        }
        public static int AddNewCashiers(List<string[]> rows, Action<int> onProgress = null)
        {
            int added = 0, processed = 0;

            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                using (var ins = new SQLiteCommand("INSERT IR IGNORE INTO Pos_Employee (Emp_Name,Password) VALUES (@n,@p)", con, tx ))
                {
                    var pn = ins.Parameters.AddWithValue("@n", "");
                    var pp = ins.Parameters.AddWithValue("@p", "");

                    foreach(var r in rows)
                    {
                        processed++;
                        if (onProgress != null && processed % 20 == -0) onProgress(processed);

                        string name = (r[0] ?? "").Trim();
                        var pass = (r[1] ?? "").Trim();
                        if (name == "" || pass == "") continue;

                        pn.Value = name;
                        pp.Value = pass;
                        added += ins.ExecuteNonQuery();
                    }
                    tx.Commit();
                }
            }
            if (onProgress != null) onProgress(rows.Count);
            return added;
        }
        public static string FindCashierByPassword(string password)
        {
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SQLiteCommand ("SELECT Emp_Name FROM Pos_Employee WHERE Password = @p", con))
                {
                    cmd.Parameters.AddWithValue("@p", (password ?? "").Trim());
                    object v = cmd.ExecuteScalar();
                    return v == null ? "" : v.ToString();
                }
            }
        }
        public static DataTable GetCasher_Name()
        {
            var dt = new DataTable();
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SQLiteCommand("SELECT Emp_Name,Password FROM Pos_Employee ORDER BY Emp_Name", con))
                using (var rd = cmd.ExecuteReader())
                {
                    dt.Load(rd);
                }
            }
            return dt;
        }
        public static DataTable FindProduct(string text)
        {
            string t = text.Trim();
            string like = t.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_") + "%";

            var dt = new DataTable();
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                string sql = @"SELECT Prod_Code, Prod_Name, Barcode,Selling_Price FROM Product WHERE Prod_Code LIKE @p ESCAPE '\' OR Barcode LIKE @p ESCAPE '\' ORDER BY CASE WHEN Prod_Code = @e COLLATE NOCASE OR Barcode = @e THEN 0 ELSE 1 END, Prod_Code LIMIT 200";
                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@p", like);
                    cmd.Parameters.AddWithValue("@e", t);
                    using (var rd = cmd.ExecuteReader())
                    {
                        dt.Load(rd);
                    }
                }
            }
            return dt;
        }
        public static void AddTemp(string Unit_no, cls_Item item, double Qty)
        {
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                string sql = "INSERT INTO Temp_Transaction (Unit_No, Prod_Code, Prod_Name, Barcode, Qty, Selling_Price) VALUES (@u, @c, @n, @b, @q, @p)";
                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@u", Unit_no);
                    cmd.Parameters.AddWithValue("@c", item.Prod_Code);
                    cmd.Parameters.AddWithValue("@n", item.Prod_Name);
                    cmd.Parameters.AddWithValue("@b", item.Barcode);
                    cmd.Parameters.AddWithValue("@q", Qty);
                    cmd.Parameters.AddWithValue("@p", item.Selling_Price);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void AddorUpdateTempTransaction(string Unit_no,cls_Item Item, double Qty)
        {
            bool updated = false;
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                string sql = @"UPDATE Temp_Transaction SET Qty = Qty + @q, Selling_Price = @p WHERE Id = (SELECT MIN(Id) FROM Temp_Transaction WHERE Prod_Code = @c)";
                using (var cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@q", Qty);
                    cmd.Parameters.AddWithValue("@p", Item.Selling_Price);
                    cmd.Parameters.AddWithValue("@c", Item.Prod_Code);
                    updated = cmd.ExecuteNonQuery() > 0;
                }
            }
            if(!updated)AddTemp(Unit_no,  Item,  Qty);
        }
        public static DataTable GetTemp()
        {
            var dt = new DataTable();
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                string sql = @"SELECT Id,Prod_Code,Prod_Name,Barcode,Qty,Selling_Price FROM Temp_Transaction ORDER BY Id";
                using (var cmd = new SQLiteCommand(sql, con))
                using (var rd = cmd.ExecuteReader())
                {
                    dt.Load(rd);
                }
            }
            return dt;
        }
        public static string ReduceQty(string key, double n)
        {
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    var rows = new List<KeyValuePair<long, double>>();
                    using (var cmd = new SQLiteCommand(@"SELECT Id,Qty FROM Temp_Transaction WHERE Prod_Code = @k COLLATE NOCASE OR Barcode = @k ORDER BY Id DESC", con, tx))
                    {
                        cmd.Parameters.AddWithValue("@k", key);
                        using (var rd = cmd.ExecuteReader())
                        {
                            while(rd.Read())
                            {
                                rows.Add(new KeyValuePair<long, double>(Convert.ToInt64(rd.GetValue(0)), Convert.ToDouble(rd.GetValue(1))));
                            }
                        }
                    }
                    if (rows.Count == 0) return "There is not in the datatable";

                    double total = 0;
                    foreach (var r in rows) total += r.Value;
                    if (total < n) return "Item Total Qty" + total + "only" + n + "cant minus";
                    double remaining = n;
                    foreach (var r in rows)
                    {
                        if (remaining <= 0) break;

                        double take = Math.Min(remaining, r.Value);
                        double left = r.Value - take;

                        string sql = left <= 0 ? "DELETE FROM Temp_Transaction WHERE Id = @id" : "UPDATE Temp_Transaction SET Qty = @q WHERE Id = @id";

                        using (var cmd = new SQLiteCommand(sql,con,tx))
                        {
                            cmd.Parameters.AddWithValue("@id", r.Key);
                            if (left > 0) cmd.Parameters.AddWithValue("@q", left);
                            cmd.ExecuteNonQuery();
                        }
                        remaining -= take;
                    }
                    tx.Commit();
                    return "";
                }
            }
        }
        public static void VoidRow(long Id)
        {
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Temp_Transaction WHERE id = @Id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static int SaveTransaction(string User_Name)
        {
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                using (var tx = con.BeginTransaction())
                {
                    int count;
                    using (var c = new SQLiteCommand("SELECT COUNT(*) FROM Temp_Transaction", con))
                    {
                        count = Convert.ToInt32(c.ExecuteScalar());
                    }
                    if (count == 0) return 0;

                    string now = DateTime.Now.ToString("dd-MM-yyyy HH:mm.ss");
                    string batch = DateTime.Now.ToString("ddMMyyHHmmss");

                    foreach (string table in new string[] { "Stock_Transaction", "Backup_Stock_Transaction" })
                    {
                        string sql = "INSERT INTO " + table + @"(Unit_No,Prod_Code,Prod_Name,Barcode,Qty,Selling_Price,User_Name,Saved_At,Batch_No) SELECT Unit_No ,Prod_Code ,Prod_Name ,Barcode ,Qty ,Selling_Price ,@usr ,@s , @b FROM Temp_Transaction ORDER BY Id";
                        using (var cmd = new SQLiteCommand(sql, con, tx))
                        {
                            //cmd.Parameters.AddWithValue("@mode", Scann_Mode);
                            cmd.Parameters.AddWithValue("@usr", User_Name);
                            cmd.Parameters.AddWithValue("@s",now);
                            cmd.Parameters.AddWithValue("@b", batch);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    using (var del = new SQLiteCommand("DELETE FROM Temp_Transaction", con, tx))
                    {
                        del.ExecuteNonQuery();
                    }
                    tx.Commit();
                    return count;
                }
            }
        }
        public static DataTable GetSavedTransaction()
        {
            var dt = new DataTable();
            using (var con = new SQLiteConnection(ConnStr))
            {
                con.Open();
                string sql = @"SELECT Id, Unit_No, Prod_Code, Prod_Name, Barcode, Qty, Selling_Price, User_Name, Saved_At, Batch_No
                       FROM Stock_Transaction ORDER BY Id";
                using (var cmd = new SQLiteCommand(sql, con))
                using (var rd = cmd.ExecuteReader())
                {
                    dt.Load(rd);
                }
            }
            return dt;
        }
    }
}
