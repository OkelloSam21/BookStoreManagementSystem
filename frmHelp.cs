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
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            pnlHelp.Left = (this.Width - pnlHelp.Width) / 2;
            pnlHelp.Top= (this.Height - pnlHelp.Height) / 2;
        }
    }
}
