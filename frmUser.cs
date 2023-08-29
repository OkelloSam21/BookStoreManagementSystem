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
    public partial class frmUser : Form
    {
        string userIdToUpdate;
        public frmUser()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            pnlDataGrid.Visible = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            userIdToUpdate = "";
            txtPassword.Text = "";
            txtPassword2.Text = "";
            txtUserId.Text = "";
            txtUserName.Text = "";
            btnSave.Text = "Save";
            pnlDataGrid.Visible = false;
            btnHide.Visible = true;
            btnHide1.Visible = true;
            btnShow1.Visible = true;
            btnShow.Visible = true;
            txtPassword.Visible = true;
            txtPassword2.Visible = true;
            cmbPrivileges.Visible = false;
            lblPrivileges.Visible = false;
            lblPassword.Visible = true;
            lblPassword2.Visible = true;
        }

        private void frmUser_Load(object sender, EventArgs e)
        {

            pnlUser.Left = (this.Width - pnlUser.Width) / 2;
            pnlUser.Top = (this.Height - pnlUser.Height) / 2;
            userIdToUpdate = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
            btnHide.Visible = false;
            btnHide1.Visible = false;
            btnShow1.Visible = false;
            btnShow.Visible = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchVal.Text = "";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           

                try
                {
                    conn connect = new conn();

                    string insertQuery;

                    if (userIdToUpdate == "")
                    {
                        if (string.IsNullOrWhiteSpace(txtUserId.Text) | string.IsNullOrWhiteSpace(txtUserName.Text) | string.IsNullOrWhiteSpace(txtPassword.Text) | string.IsNullOrWhiteSpace(txtPassword2.Text))
                        {
                            MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (!(txtPassword.Text.Trim().Equals(txtPassword2.Text.Trim())))
                        {
                            MessageBox.Show("password does not match");
                        }
                        else
                        {
                            if (connect.openConnection() == true)
                            {
                                insertQuery = "INSERT INTO users(user_id,user_name,password) VALUES('" + txtUserId.Text.ToString().Trim() + "','" + txtUserName.Text.Trim() + "','" + txtPassword.Text.Trim() + "')";
                                MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);
                                try
                                {
                                    command.ExecuteNonQuery();

                                    MessageBox.Show("User Data inserted successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    userIdToUpdate = "";
                                    txtUserId.Text = "";
                                    txtUserName.Text = "";
                                    txtPassword.Text = "";
                                    txtPassword2.Text = "";

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    // MessageBox.Show("Book Author with ID " + txtBookOrderId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtUserId.Focus();
                                }
                            }
                        }
                        



                        
                    }
                    else
                    {

                        if (string.IsNullOrWhiteSpace(txtUserId.Text) | string.IsNullOrWhiteSpace(txtUserName.Text) | string.IsNullOrWhiteSpace(cmbPrivileges.Text))
                        {
                            MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                       
                        if(pnlDataGrid.Visible ==true)
                        {
                            btnSave.Enabled=false;
                        }

                        if (connect.openConnection() == true)
                        {
                            insertQuery = "Update users set user_id='" + txtUserId.Text.ToString().Trim() + "',user_name='" + txtUserName.Text.Trim() + "',privileges='" + cmbPrivileges.Text.Trim() + "',status='"+cmbStatus.Text+"'where user_id='" + userIdToUpdate + "'";

                            MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);
                            try
                            {
                                command.ExecuteNonQuery();

                                MessageBox.Show("User Data updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


                                userIdToUpdate = "";
                                txtUserId.Text = "";
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtPassword2.Text = "";
                                cmbPrivileges.Text = "";
                                cmbStatus.Text = "";
                                txtUserId.Focus();
                                btnSave.Text = "Save";

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                // MessageBox.Show("Book Author with ID " + txtBookOrderId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtUserId.Focus();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message);
                }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            if (txtPassword2.PasswordChar == '\0')
            {
                btnShow.BringToFront();
                txtPassword2.PasswordChar = '*';
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (txtPassword2.PasswordChar == '*')
            {
                btnHide.BringToFront();
                txtPassword2.PasswordChar = '\0';
            }
        }

        private void btnHide1_Click(object sender, EventArgs e)
        {
            
            if (txtPassword.PasswordChar == '\0')
            {
                btnShow1.BringToFront();
                txtPassword.PasswordChar = '*';
            }
        }

        private void btnShow1_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                btnHide1.BringToFront();
                txtPassword.PasswordChar = '\0';
            } 
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            string criteria;
            string searchValue;
            string query = "";

            if (string.IsNullOrWhiteSpace(cmbSearchVal.Text))
            {
                MessageBox.Show("fill this field");
                cmbSearchVal.Focus();
            }
            else if (string.IsNullOrWhiteSpace(txtSearchVal.Text))
            {
                MessageBox.Show("fill this field");
                txtSearchVal.Focus();
            }
            else
            {
                criteria = cmbSearchVal.Text.ToString();
                searchValue = "%" + txtSearchVal.Text.ToString() + "%";

                if (criteria == "User Id")
                {
                    query = "SELECT * FROM users WHERE user_id LIKE '" + searchValue + "' ORDER BY user_id ASC";
                }
                else if (criteria == "User Name")
                {
                    query = "SELECT * FROM users WHERE user_name LIKE '" + searchValue + "' ORDER BY user_name ASC";
                }

                else if (criteria == "Privileges")
                {
                    query = "SELECT * FROM users WHERE privileges LIKE '" + searchValue + "' ORDER BY privileges ASC";
                }

                else if (criteria == "All")
                {
                    query = "SELECT * FROM users ORDER BY user_id ASC";
                }
                else
                {
                    MessageBox.Show("Invalid Criteria!", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                conn connect = new conn();

                if (connect.openConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connect.connection);

                    MySqlDataReader dataReader = command.ExecuteReader();

                    this.dataGridView1.Rows.Clear();

                    while (dataReader.Read())
                    {
                        string[] row = new string[] { dataReader["user_id"].ToString(), dataReader["user_name"].ToString(), dataReader["privileges"].ToString(), dataReader["status"].ToString() };
                        dataGridView1.Rows.Add(row);
                    }
                    dataReader.Close();
                    connect.closeConnection();
                }


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUserId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtUserName.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            cmbPrivileges.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            cmbStatus.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            pnlDataGrid.Visible = false;

            userIdToUpdate = txtUserId.Text;

            btnSave.Text = "Update";
            txtPassword.Visible = false;
            txtPassword2.Visible = false;
            lblPassword.Visible = false;
            lblPassword2.Visible = false;
            btnShow.Visible = false;
            btnShow1.Visible = false;
            btnHide.Visible = false;
            btnHide1.Visible = false;
            lblPrivileges.Visible= true;
            cmbPrivileges.Visible = true;
            lblStatus.Visible = true;
            cmbStatus.Visible = true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            btnSave.Text = "Save";
            userIdToUpdate = "";
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtPassword.Visible = true;
            txtPassword2.Visible = true;
            lblPassword.Visible = true;
            lblPassword2.Visible = true;
            btnShow.Visible = true;
            btnShow1.Visible = true;
            btnHide.Visible = true;
            btnHide1.Visible = true;
            lblPrivileges.Visible = false;
            cmbPrivileges.Visible = false;
            lblStatus.Visible = false;
            cmbStatus.Visible = false;
            pnlDataGrid.Visible = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            btnSave.Text = "Save";
            userIdToUpdate = "";
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtPassword.Visible = true;
            txtPassword2.Visible = true;
            lblPassword.Visible = true;
            lblPassword2.Visible = true;
            btnShow.Visible = true;
            btnShow1.Visible = true;
            btnHide.Visible = true;
            btnHide1.Visible = true;
            lblPrivileges.Visible = false;
            cmbPrivileges.Visible = false;
            lblStatus.Visible = false;
            cmbStatus.Visible = false;
            pnlDataGrid.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (userIdToUpdate != "")
            {
                if (MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deleteQuery = "Delete from users where user_id='" + userIdToUpdate + "'";

                    conn connect = new conn();

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);


                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data Successfully deleted");


                            userIdToUpdate = "";
                            txtUserId.Text = "";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtPassword2.Text = "";
                            cmbPrivileges.Text = "";
                            cmbStatus.Text = "";
                            txtUserId.Focus();

                        }
                        catch (MySqlException ex)
                        {
                            if (ex.Number == 1451 | ex.Number == 1452) // Check for the specific error number for foreign key constraint violation
                            {
                                MessageBox.Show("Deletion denied. Only administrator Can detele this Item");
                            }
                            else
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }



                        connect.closeConnection();
                    }
                }

            }
        }
    }
}
