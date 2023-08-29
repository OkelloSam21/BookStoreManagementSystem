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
    public partial class frmBookAuthor : Form
    {
        string bookAuthorIdTobeUpdated;

        public frmBookAuthor()

           
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            PopulateComboBoxBookId();
            PopulateComboxBoxAuthorId();
        }

        private void pnlBookAuthor_Paint(object sender, PaintEventArgs e)
        {
            pnlBookAuthor.Left = (this.Width - pnlBookAuthor.Width) / 2;
            pnlBookAuthor.Top = (this.Height - pnlBookAuthor.Height) / 2;

            bookAuthorIdTobeUpdated = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtBookId_TextChanged(object sender, EventArgs e)
        {

        }

        private void PopulateComboBoxBookId()
        {
            try
            {
                conn connect = new conn();
                
                   string query = "SELECT book_id FROM book"; 

                   if(connect.openConnection() == true)
                    {
                        MySqlCommand cmd = new MySqlCommand(query, connect.connection);
                        MySqlDataReader dataReader = cmd.ExecuteReader();
                        this.cmbBookId.Items.Clear();

                        while (dataReader.Read())
                        {
                            cmbBookId.Items.Add(dataReader["book_id"].ToString());
                        }

                        dataReader.Close();
                    }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void PopulateComboxBoxAuthorId()
        {
            try
            {
                conn connect = new conn();

                string query = "SELECT author_id FROM author";

                if (connect.openConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, connect.connection);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    this.cmbAuthorId.Items.Clear();

                    while (dataReader.Read())
                    { 
                        cmbAuthorId.Items.Add(dataReader["author_id"].ToString());
                    }
                    dataReader.Close();
                }

            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void cmbAuthorId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbBookId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbAuthorId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookAuthorId.Text) | string.IsNullOrWhiteSpace(cmbBookId.Text)  | string.IsNullOrWhiteSpace(cmbAuthorId.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string input = txtBookAuthorId.Text;
                string bookAuthorId = input.Trim();
                try
                {
                    conn connect = new conn();
                    string selectedValue = cmbBookId.SelectedItem.ToString();
                    string selectedValue1 = cmbAuthorId.SelectedItem.ToString();

                    string insertQuery;

                    if (bookAuthorIdTobeUpdated == "")
                    {

                        insertQuery = "INSERT INTO book_author(book_author_id,book_id,author_id) VALUES('"+txtBookAuthorId.Text.ToString().Trim()+"','"+cmbBookId.Text.ToString().Trim()+"','"+cmbAuthorId.Text.ToString().Trim()+"')";

                       

                        if (connect.openConnection() == true)
                        {
                             MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);
              
                            try
                            {
                               command.ExecuteNonQuery();
                              
                              
                                    MessageBox.Show("Book Author Data inserted successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                     
                                    bookAuthorIdTobeUpdated="";
                                    txtBookAuthorId.Text = "";
                                    cmbAuthorId.Text = "";
                                    cmbBookId.Text = "";
                                    txtBookAuthorId.Focus();
                               

                            }
                            catch (Exception ex)
                            {
                                
                                MessageBox.Show(ex.Message,"BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBookAuthorId.Focus();
                            }
                        }
                    }
                    else
                    {
                        insertQuery="Update book_author set book_author_id='"+txtBookAuthorId.Text.ToString().Trim()+"',book_id='"+cmbBookId.Text+"',author_id='"+cmbAuthorId.Text+"'where book_author_id='"+bookAuthorIdTobeUpdated+"'";

                          if (connect.openConnection() == true)
                        {
                             MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);
              
                            try
                            {
                               command.ExecuteNonQuery();
                              
                              
                                    MessageBox.Show("Book Author Data Updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                     
                                    bookAuthorIdTobeUpdated="";
                                    txtBookAuthorId.Text = "";
                                    cmbAuthorId.Text = "";
                                    cmbBookId.Text = "";
                                    txtBookAuthorId.Focus();
                               

                            }
                            catch (Exception ex)
                            {
                                
                                MessageBox.Show(ex.Message,"BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBookAuthorId.Focus();
                            }
                        }
                    }
                   
                }
                catch(Exception ex) {
                    MessageBox.Show("" + ex.Message);
                }
            }
        }

        private void txtBookAuthorId_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbBookId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtBookAuthorId.Text) | string.IsNullOrWhiteSpace(cmbAuthorId.Text) | string.IsNullOrWhiteSpace(cmbBookId.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {

                    conn connect = new conn();

                    string bookIdToUpdate = txtBookAuthorId.Text;

                    string updateQuery = "UPDATE book_author SET book_id=@bookId,author_id=@authorId WHERE book_author_id=@bookAuthorId;";


                    if (connect.openConnection() == true)
                    {
                        MySqlCommand command = new MySqlCommand(updateQuery, connect.connection);

                        command.Parameters.AddWithValue("@bookId", cmbBookId.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@authorId", cmbAuthorId.Text.ToString().Trim());
                        command.Parameters.AddWithValue("@bookAuthorId", bookIdToUpdate);

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {

                                MessageBox.Show("Book Author data updated successfully");

                                txtBookAuthorId.Text = "";
                                cmbAuthorId.Text = "";
                                cmbBookId.Text = "";

                               
                                
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtBookAuthorId.Text = "";
            cmbAuthorId.Text = "";
            cmbBookId.Text = "";
            txtBookAuthorId.Focus();
            pnlDataGrid.Visible = false;
            
        }

        private void frmBookAuthor_Load(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;

            txtBookAuthorId.Text = "";
            cmbAuthorId.Text = "";
            cmbBookId.Text = "";
            txtBookAuthorId.Focus();
         
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

                if (criteria == "Book Author Id")
                {
                    query = "SELECT * FROM book_author WHERE book_author_id LIKE '" + searchValue + "' ORDER BY book_author_id ASC";
                }
                else if (criteria == "Book Id")
                {
                    query = "SELECT * FROM book_author WHERE book_id LIKE '" + searchValue + "' ORDER BY book_id ASC";
                }

                else if (criteria == "Author Id")
                {
                    query = "SELECT * FROM book_author WHERE author_id LIKE '" + searchValue + "' ORDER BY author_id ASC";
                }

                else if (criteria == "All")
                {
                    query = "SELECT * FROM book_author ORDER BY book_author_id ASC";
                }
                else
                {
                    MessageBox.Show("Invalid Criteria!", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                try
                {
                conn connect = new conn();

                if (connect.openConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connect.connection);

                    MySqlDataReader dataReader = command.ExecuteReader();

                    this.dataGridView1.Rows.Clear();

                    while (dataReader.Read())
                    {
                        string[] row = new string[] { dataReader["book_author_id"].ToString(), dataReader["book_id"].ToString(), dataReader["author_id"].ToString() };
                        dataGridView1.Rows.Add(row);
                    }
                    dataReader.Close();
                    connect.closeConnection();
                }


            }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

       }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bookAuthorIdTobeUpdated = "";
            txtBookAuthorId.Text = "";
            cmbAuthorId.Text = "";
            cmbBookId.Text = "";
            txtBookAuthorId.Focus();
            pnlDataGrid.Visible = false;
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtBookAuthorId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            cmbBookId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            cmbAuthorId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            pnlDataGrid.Visible = false;

            bookAuthorIdTobeUpdated = txtBookAuthorId.Text;
            btnSave.Text = "Update";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bookAuthorIdTobeUpdated != "")
            {
                if (MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deleteQuery = "Delete from book_author where book_author_id='" + bookAuthorIdTobeUpdated + "'";

                    conn connect = new conn();

                   
                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);


                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Data Successfully deleted");

                            bookAuthorIdTobeUpdated="";
                            txtBookAuthorId.Text = "";
                            cmbAuthorId.Text = "";
                            cmbBookId.Text = "";
                        

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
