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
    public partial class frmAuthor : Form
    {

        string authorIdToUpdate;

        DataTable dataTable = new DataTable();


        public frmAuthor()

        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
          
        }

        private void frmAuthor_Load(object sender, EventArgs e)
        {
            pnlAuthor.Left = (this.Width - pnlAuthor.Width) / 2;
            pnlAuthor.Top = (this.Height - pnlAuthor.Height) / 2;
            pnlDataGrid.Visible = false;
            authorIdToUpdate = "";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pnlAuthor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAuthorId.Text) | string.IsNullOrWhiteSpace(txtFirstName.Text) | string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string input = txtAuthorId.Text;
                string authorId = input.Trim();
                string input2 = txtFirstName.Text;
                string firstName = input2.Trim();
                string input3 = txtLastName.Text;
                string lastName = input3.Trim();
                try
                {
                    conn connect = new conn();

                    string insertquery ;


                    if (authorIdToUpdate == "")
                    {
                        insertquery = "INSERT INTO author(author_id,first_name,last_name) VALUES('" + txtAuthorId.Text.ToString().Trim() + "','" + txtFirstName.Text.ToString().Trim() + "','" + txtLastName.Text.ToString().Trim() + "')";

                        if (connect.openConnection() == true)
                        {
                            MySqlCommand command = new MySqlCommand(insertquery, connect.connection);



                            try
                            {
                                command.ExecuteNonQuery();


                                MessageBox.Show("Author Data inserted successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show("Auther with ID " + txtAuthorId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            connect.closeConnection();
                        }
                        else
                        {
                            if (connect.openConnection() == false)
                            {

                                MessageBox.Show("Server Not Found.", "SERVER CONNECTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        insertquery = "Update author set author_id='" + txtAuthorId.Text.ToString().Trim() + "',first_name='" + txtFirstName.Text.ToString().Trim() + "',last_name='" + txtLastName.Text.ToString().Trim() + "'where author_id='"+authorIdToUpdate+"'";

                        if (connect.openConnection() == true)
                        {
                            MySqlCommand command = new MySqlCommand(insertquery, connect.connection);



                            try
                            {
                                command.ExecuteNonQuery();


                                MessageBox.Show("Author Data Updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                authorIdToUpdate = "";
                                txtAuthorId.Text = "";
                                txtFirstName.Text = "";
                                txtLastName.Text = "";
                                

                            }
                            catch (MySqlException ex)
                            {
                                MessageBox.Show("Auther with ID " + txtAuthorId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            connect.closeConnection();
                        }
                        else
                        {
                            if (connect.openConnection() == false)
                            {

                                MessageBox.Show("Server Not Found.", "SERVER CONNECTION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                   
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(" " + ex);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAuthorId.Text) | string.IsNullOrWhiteSpace(txtFirstName.Text) | string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    string updatedFirstName = txtFirstName.Text;
                    string updatedLastName = txtLastName.Text;
                    conn connect = new conn();

                    string updateQuery = "UPDATE author SET first_name=@updatedFirstName,last_name=@updatedLastName WHERE author_id=@authorId;";


                    if (connect.openConnection() == true)
                    {
                        MySqlCommand command = new MySqlCommand(updateQuery, connect.connection);

                        command.Parameters.AddWithValue("@updatedFirstName", updatedFirstName.Trim());
                        command.Parameters.AddWithValue("@updatedLastName", updatedLastName.Trim());
                        command.Parameters.AddWithValue("@authorId", txtAuthorId.Text.Trim());

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {

                                     MessageBox.Show("Author data updated successfully");

                                     txtAuthorId.Text = "";
                                     txtFirstName.Text = "";
                                     txtLastName.Text = "";

                                     btnSave.Visible = true;
                                     
                                }

                               
                            
                            else
                            {
                                MessageBox.Show("Author data did not update");
                            }
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void populateTextBoxes()
        {
            if (string.IsNullOrWhiteSpace(txtAuthorId.Text))
            {
                MessageBox.Show("Enter Author Id you want yo update", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAuthorId.Focus();
            }
            else
            {
                try
                {
                    authorIdToUpdate = txtAuthorId.Text;
                    conn connect = new conn();

                    string selectQuery = "SELECT author_id, first_name, last_name FROM author WHERE author_id = @authorId;";

                    if (connect.openConnection() == true)
                    {
                        MySqlCommand command = new MySqlCommand(selectQuery, connect.connection);

                        command.Parameters.AddWithValue("@authorId", authorIdToUpdate);


                        MySqlDataReader dataReader = command.ExecuteReader();

                        if (dataReader.Read())
                        {
                            txtAuthorId.Text = dataReader["author_id"].ToString();
                            txtFirstName.Text = dataReader["first_name"].ToString();
                            txtLastName.Text = dataReader["last_name"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Author Not Found");
                        }
                        dataReader.Close();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message);
                }
            }
        }

        private void btnSelectId_Click(object sender, EventArgs e)
        {
            populateTextBoxes();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
            
            
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
            txtAuthorId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
           
       
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            txtSearchVal.Text = "";
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

                if (criteria == "Author Id")
                {
                    query = "SELECT * FROM author WHERE author_id LIKE '" + searchValue + "' ORDER BY author_id ASC";
                }
                else if (criteria == "First Name")
                {
                    query = "SELECT * FROM author WHERE first_name LIKE '" + searchValue + "' ORDER BY first_name ASC";
                }

                else if (criteria == "Last Name")
                {
                    query = "SELECT * FROM author WHERE last_name LIKE '" + searchValue + "' ORDER BY last_name ASC";
                }
               
                else if (criteria == "All")
                {
                    query = "SELECT * FROM author ORDER BY author_id ASC";
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
                        string[] row = new string[] { dataReader["author_id"].ToString(), dataReader["first_name"].ToString(), dataReader["last_name"].ToString()};
                        dataGridView1.Rows.Add(row);
                    }
                    dataReader.Close();
                    connect.closeConnection();
                }


            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAuthorId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            txtLastName.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            pnlDataGrid.Visible = false;

            authorIdToUpdate = txtAuthorId.Text;

            btnSave.Text = "Update";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            authorIdToUpdate = "";
            txtAuthorId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            btnSave.Text = "Save";
            txtAuthorId.Focus();
           
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            authorIdToUpdate = "";
            txtAuthorId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (authorIdToUpdate != "")
            {
                if (MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deleteQuery = "Delete from author where author_id='" + authorIdToUpdate + "'";

                    conn connect = new conn();

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);


                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data Successfully deleted");
                            authorIdToUpdate = "";
                            txtAuthorId.Text = "";
                            txtFirstName.Text = "";
                            txtLastName.Text = "";
                        

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
