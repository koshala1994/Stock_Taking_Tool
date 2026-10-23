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
    public partial class frm_Item_Find : Form
    {
        public string SelCode = "";
        public string SelName = "";
        public string SelBarcode = "";
        public double SelPrice = 0;
        public frm_Item_Find(DataTable dt)
        {
            InitializeComponent();

            dgv_Item_Find.AllowUserToAddRows = false;
            dgv_Item_Find.ReadOnly = true;
            dgv_Item_Find.MultiSelect = false; 
            dgv_Item_Find.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Item_Find.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_Item_Find.DataSource = dt;

            dgv_Item_Find.CellClick += dgv_Item_Find_CellClick;
            Shown += delegate { ForcustFirstRow(); };
        }
        public void ForcustFirstRow()
        {
            dgv_Item_Find.Focus();
            if (dgv_Item_Find.Rows.Count == 0) return;

            DataGridViewColumn firse = dgv_Item_Find.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
            dgv_Item_Find.ClearSelection();
            dgv_Item_Find.Rows[0].Selected = true;
        }
        private void dgv_Item_Find_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            PicRow(e.RowIndex);          
        }
        private bool _picked = false;
        private void PicRow(int rowIndex)
        {
            if (_picked || rowIndex < 0) return;
            _picked = true;
            DataRowView r = (DataRowView)dgv_Item_Find.Rows[rowIndex].DataBoundItem;
            SelCode = r["Prod_Code"].ToString();
            SelName = r["Prod_Name"].ToString();
            SelBarcode = r["Barcode"].ToString();
            double p;
            double.TryParse(r["Selling_Price"].ToString(), out p);
            SelPrice = p;

            DialogResult = DialogResult.OK;
        }
        private bool _busy = false;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter && dgv_Item_Find.CurrentRow != null)
            {
                if (_busy) return true;
                _busy = true;
                try { PicRow(dgv_Item_Find.CurrentRow.Index); }
                finally { _busy = false; }
                
                return true;
            }
            if(keyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                return true;
            }
            return base.ProcessCmdKey(ref msg,keyData);
        }
    }
}
