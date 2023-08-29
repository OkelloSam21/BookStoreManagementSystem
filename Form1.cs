using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bims
{
    public partial class frmSplash : Form
    {

        int flag=0;

        public frmSplash()
        {
            InitializeComponent();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            pnlMain.Left = (this.Width - pnlMain.Width) / 2;
            pnlMain.Top = (this.Height - pnlMain.Height) / 2;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == 100 && flag==0)
            {
                frmLogin login = new frmLogin();
                this.Hide();
                login.Visible = true;
                flag = 1;
                
            }
            if (progressBar1.Value < 100)
            {
                progressBar1.Value = progressBar1.Value + 10;
                lblProgressStatus.Text = "progress..." + progressBar1.Value + "%";
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
