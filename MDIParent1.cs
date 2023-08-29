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
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;

        public MDIParent1()
        {
            InitializeComponent();
         
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            frmMain frmmain = new frmMain();
           
            frmmain.MdiParent = this;
            frmmain.Visible = true;

            

            

        }

        private void bookRegistationToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void bookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBook frmbook = new frmBook();
            frmbook.MdiParent = this;
            frmbook.Visible = true;
            
        }

        private void authorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAuthor frmauthor = new frmAuthor();
            frmauthor.MdiParent = this;
            frmauthor.Visible = true;
        }

        private void bookAuthorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBookAuthor frmbookauthor = new frmBookAuthor();
            frmbookauthor.MdiParent = this;
            frmbookauthor.Visible = true;
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomer frmcustomer = new frmCustomer();
            frmcustomer.MdiParent = this;
            frmcustomer.Visible = true;
        }

        private void bookBundleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBundle frmbundle = new frmBundle();
            frmbundle.MdiParent = this;
            frmbundle.Visible = true;
        }

        private void bookBundleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBookBundle frmbookbundle = new frmBookBundle();
            frmbookbundle.MdiParent = this;
            frmbookbundle.Visible = true;
        }

        private void orderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmOrder frmorder= new frmOrder();
            frmorder.MdiParent = this;
            frmorder.Visible = true;  
        }

        private void bookToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmBookOrder frmbookorder = new frmBookOrder();
            frmbookorder.MdiParent = this;
            frmbookorder.Visible = true;  
        }



        private void authorizations()
        {
            try
            {
                conn connect = new conn();

                string query = "SELECT * from users where user_name='";

                if (connect.openConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, connect.connection);
                    MySqlDataReader dataReader = command.ExecuteReader();


                    while(dataReader.Read())
                    {
                        if (dataReader["privileges"].ToString()=="user")
                        {
                            //admininistrationToolStripMenuItem.Visible = false;
                           // MessageBox.Show(dataReader["privileges"].ToString());
                        }
                        else
                        {
                            admininistrationToolStripMenuItem.Visible = true;
                        }
                       
                    }

                    dataReader.Close();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }


        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Visible = true;
            this.Hide();
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Prompt the user with a dialog if needed
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Cancel the form closing
                }
                else
                {
                    // Allow the form to close permanently
                    Application.Exit();
                }
            }
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUser frmuser= new frmUser();
            frmuser.MdiParent = this;
            frmuser.Visible = true;
        }

        private void admininistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHelp frmuser = new frmHelp();
            frmuser.MdiParent = this;
            frmuser.Visible = true;
        }
    }
}
