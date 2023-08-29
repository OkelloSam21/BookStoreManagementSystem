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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            panelMain.Left = (this.Width - panelMain.Width) / 2;
            panelMain.Top = (this.Height - panelMain.Height) / 2;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Username Field is empty", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
            }
            else if (txtPassword.Text == "")
            {
                MessageBox.Show("Password Field is empty", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
            }
          
            else
            {
                try
                {
                    conn connect = new conn();

                    string query = "SELECT * FROM users where user_name = '" + txtUsername.Text.ToString() + "' AND password='" + txtPassword.Text.ToString() + "' AND status=1 ";
                    //open connection
                    if (connect.openConnection() == true)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, connect.connection);
                        MySqlDataReader dataReader = cmd.ExecuteReader();

                        if (dataReader.Read())
                        {

                          
                                MDIParent1 mdi = new MDIParent1();
                                mdi.Visible = true;
                                this.Hide();
                          
                            
                        }
                     
                       
                        else
                        {
                            MessageBox.Show("Invalid Credentials. Please try again", "BIMS LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.Text = "";
                            txtUsername.Text = "";
                            txtUsername.Focus();


                        }
                        dataReader.Close();
                        connect.closeConnection();
                      
                    }
                    else
                    {
                        if (connect.openConnection() == false)
                        {

                            MessageBox.Show("Server Not Found.", "SERVER CONNECTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.Text = "";
                            txtUsername.Text = "";
                            txtUsername.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(" " +ex);
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }
     

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void btnShow_Click_1(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
               btnHide.BringToFront();
                txtPassword.PasswordChar = '\0';
            }
            
        }

      

         private void btnHide_Click(object sender, EventArgs e)
         {
             if (txtPassword.PasswordChar == '\0')
             {
                 btnShow.BringToFront();
                 txtPassword.PasswordChar = '*';
             }
         }

         private void txtPassword_TextChanged(object sender, EventArgs e)
         {
            

         }

  
    }
}
