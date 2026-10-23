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
    public partial class frm_Splash : Form
    {
        public frm_Splash()
        {
            InitializeComponent();
            this.Shown += frm_Splash_Shown;
        }

        private async void frm_Splash_Shown(object sender, EventArgs e)
        {
            pic_Loading.Image = Properties.Resources.Onimta_Loading;
            pic_Loading.SizeMode = PictureBoxSizeMode.Zoom;

            await Task.Delay(100);

            try
            {
                await Task.Run(() => 
                {
                    cls_MainDb.Load();
                    cls_DbCreate.Init();
                });
                await Task.Delay(5000);

                this.Hide();

                using (var main = new frm_Stock_Taking_Tool())
                {
                    main.ShowDialog();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Startup Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Close();
        }
    }
}
