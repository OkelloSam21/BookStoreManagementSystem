using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace bims
{
    public partial class frmMain : Form
    {
        DataTable dataTable = new DataTable();
        public frmMain()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            PopulateDataTable();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Height = this.MdiParent.Height;
            this.Width = this.MdiParent.Width;
            //label1.Left = (this.Width - label1.Width) / 2;
            //dataGridView1.DataSource = dataTable;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
         private void PopulateDataTable()
            {
                try
                {
                    conn connect = new conn();

                     string selectQuery = "SELECT * FROM book";
                    if (connect.openConnection() == true)
                    {
                      MySqlDataAdapter dataAdapter = new MySqlDataAdapter(selectQuery, connect.connection);
                   

                        
                            dataAdapter.Fill(dataTable);
                    
                
               
                   }
           
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
    
         }

         private void pnlMain_Paint(object sender, PaintEventArgs e)
         {
             pnlMain.Left = (this.Width - pnlMain.Width) / 2;
             label1.Left = (pnlMain.Width - label1.Width) / 2;
             //pnlMain.Top = (this.Height - pnlMain.Height) / 2;
         }

    }
}
