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
    public partial class frmBook : Form
    {
        
       string bookIdToUpdate;

        public frmBook()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void frmBook_Load(object sender, EventArgs e)
        {
            pnlBook.Left = (this.Width - pnlBook.Width) / 2;
            pnlBook.Top = (this.Height - pnlBook.Height) / 2;
            pnlDataGrid.Visible = false;
            bookIdToUpdate = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookId.Text) | string.IsNullOrWhiteSpace(txtTitle.Text) | string.IsNullOrWhiteSpace(cmbGenre.Text) |  string.IsNullOrWhiteSpace(txtBookPrice.Text) | string.IsNullOrWhiteSpace(cmbPubYear.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
            try
            {
                // MessageBox.Show("ACCESS GRANTED", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn connect = new conn();
                string insertquery;
                if (bookIdToUpdate == "")
                {
                    insertquery = "INSERT INTO book(book_id,title,genre,book_price,pub_year) VALUES('" + txtBookId.Text.Trim() + "','" + txtTitle.Text.Trim() + "','" + cmbGenre.Text + "','" + txtBookPrice.Text + "','" + cmbPubYear.Text + "')";

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand command = new MySqlCommand(insertquery, connect.connection);


                            command.ExecuteNonQuery();
                            MessageBox.Show("Book Data inserted successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            bookIdToUpdate = "";
                            txtBookId.Text = "";
                            txtTitle.Text = "";
                            cmbGenre.Text = "";
                            txtBookPrice.Text = "";
                            cmbPubYear.Text = "";
                            txtBookId.Focus();
                            connect.closeConnection();



                        }
                        catch (MySqlException ex)
                        {
                            //MessageBox.Show(txtBookId.Text.ToString() + ", Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(ex.Message);
                        }


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
                    insertquery = "Update book set book_id='" + txtBookId.Text.Trim() +"',title='" + txtTitle.Text.Trim() +"',genre='" + cmbGenre.Text + "',book_price='" + txtBookPrice.Text.Trim() + "',pub_year='" + cmbPubYear.Text + "'where book_id='"+bookIdToUpdate+"'";

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand command = new MySqlCommand(insertquery, connect.connection);


                            command.ExecuteNonQuery();
                            MessageBox.Show("Book Data Updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            bookIdToUpdate = "";
                            txtBookId.Text = "";
                            txtTitle.Text = "";
                            cmbGenre.Text = "";
                            txtBookPrice.Text = "";
                            cmbPubYear.Text = "";
                            txtBookId.Focus();
                            connect.closeConnection();


                            btnSave.Text = "Save";
                            pnlDataGrid.Visible = true;
                        }
                        catch (MySqlException ex)
                        {
                            //MessageBox.Show(txtBookId.Text.ToString() + ", Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show(ex.Message, "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


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
            
            catch (Exception ex)
            {

                MessageBox.Show(" " + ex.Message);
            }
        }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtBookId_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookId.Text))
            {
               
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtBookId.Text = "";
            txtTitle.Text = "";
            cmbGenre.Text = "";
            txtBookPrice.Text = "";
            cmbPubYear.Text = "";
            pnlDataGrid.Visible = false;
            txtBookId.Focus();
        }

        private void txtGenre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelectBookId_Click(object sender, EventArgs e)
        {
            populateTextBoxes();
            if (string.IsNullOrWhiteSpace(txtBookId.Text))
            {
                if (pnlDataGrid.Visible == false)
                {
                    MessageBox.Show("Enter Book Id you want yo update", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBookId.Focus();
                }
                else
                {
                    pnlDataGrid.Visible = false;
                }
                
            }
            else
            {
                btnSave.Text = "Update";
            }
            
           // PopulateComboxBoxBookGenre();
        }

        private void populateTextBoxes()
        {
          
                try
                {
                    bookIdToUpdate = txtBookId.Text;
                    conn connect = new conn();

                    string selectQuery = "SELECT book_id, title, genre,book_price,pub_year FROM book WHERE book_id = @bookId;";

                    if (connect.openConnection() == true)
                    {
                        MySqlCommand command = new MySqlCommand(selectQuery, connect.connection);

                        command.Parameters.AddWithValue("@bookId", bookIdToUpdate);


                        MySqlDataReader dataReader = command.ExecuteReader();

                        if (dataReader.Read())
                        {
                            txtBookId.Text = dataReader["book_id"].ToString();
                            txtTitle.Text = dataReader["title"].ToString();
                            cmbGenre.Text = dataReader["genre"].ToString();
                            txtBookPrice.Text= dataReader["book_price"].ToString();
                            cmbPubYear.Text = dataReader["pub_year"].ToString();
                        }
                        else
                            {
                                if (!string.IsNullOrWhiteSpace(txtBookId.Text))
                                {
                                    MessageBox.Show("Book Not Found", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtBookId.Focus();
                                }
                                
                            }
                        dataReader.Close();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message);
                }
            
        }

        private void cmbGenre_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        // not of any use where
        private void PopulateComboxBoxBookGenre()
        {
            try
            {
                conn connect = new conn();

                string query = "SELECT genre FROM book";

                if (connect.openConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, connect.connection);
                    MySqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        cmbGenre.Items.Add(dataReader["genre"].ToString());
                    }
                    dataReader.Close();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) | string.IsNullOrWhiteSpace(txtBookPrice.Text) | string.IsNullOrWhiteSpace(cmbGenre.Text) | string.IsNullOrWhiteSpace(cmbPubYear.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string updateTitle = txtTitle.Text.ToString().Trim();
                    string updateGenre = cmbGenre.Text.ToString();
                    string updateBookPrice = txtBookPrice.Text.ToString().Trim();
                    string updatePubYear = cmbPubYear.Text.ToString().Trim();
                    conn connect = new conn();
                    string updateQuery = "UPDATE book SET title=@updateTitle,genre=@updateGenre,book_price=@updateBookPrice,pub_year=@updatePubYear WHERE book_id=@bookId;";

                    if (connect.openConnection() == true)
                    {
                        MySqlCommand command = new MySqlCommand(updateQuery, connect.connection);

                        command.Parameters.AddWithValue("@updateTitle", updateTitle);
                        command.Parameters.AddWithValue("@updateGenre", updateGenre);
                        command.Parameters.AddWithValue("@updateBookPrice", updateBookPrice);
                        command.Parameters.AddWithValue("@updatePubYear", updatePubYear);
                        command.Parameters.AddWithValue("@bookId", txtBookId.Text.ToString().Trim());

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Book Data Updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                txtBookId.Text = "";
                                txtTitle.Text = "";
                                cmbGenre.Text = "";
                                txtBookPrice.Text = "";
                                cmbPubYear.Text = "";
                                txtBookId.Focus();
                               
                                btnSave.Visible = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           
                        }

                        {

                        }

                    }
                    
                       
                    

                }
            catch(Exception ex)
                {

                }

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbPubYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                searchValue ="%" + txtSearchVal.Text.ToString() + "%";

                if (criteria == "Book Id")
                {
                    query="SELECT * FROM book WHERE book_id LIKE '" + searchValue + "' ORDER BY book_id ASC";
                }
                else if (criteria == "Title")
                {
                    query = "SELECT * FROM book WHERE title LIKE '" + searchValue + "' ORDER BY title ASC";
                }
                
                else if (criteria == "Genre")
                {
                    query = "SELECT * FROM book WHERE genre LIKE '" + searchValue + "' ORDER BY genre ASC";
                }
                else if (criteria == "Price")
                {
                    query = "SELECT * FROM book WHERE book_price LIKE '" + searchValue + "' ORDER BY book_price ASC";
                }
                else if (criteria == "Year")
                {
                    query = "SELECT * FROM book WHERE pub_year LIKE '" + searchValue + "' ORDER BY pub_year ASC";
                }
                else if (criteria == "All" | criteria=="")
                {
                    query = "SELECT * FROM book ORDER BY book_id ASC";
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

                    while(dataReader.Read())
                    {
                        string[] row = new string[]{ dataReader["book_id"].ToString(), dataReader["title"].ToString(), dataReader["genre"].ToString(), dataReader["book_price"].ToString(), dataReader["pub_year"].ToString() };
                        dataGridView1.Rows.Add(row);
                    }
                    dataReader.Close();
                    connect.closeConnection();
                }


            }

        }

        private void pnlDataGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbSearchVal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearchVal_TextChanged(object sender, EventArgs e)
        {

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
          
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
            btnSave.Visible = true;
            //btnUpdate.Text = "Save Changes";
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtBookId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtTitle.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            cmbGenre.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            txtBookPrice.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            cmbPubYear.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString();
            pnlDataGrid.Visible = false;

            bookIdToUpdate = txtBookId.Text;

            btnSave.Text = "Update";



        }

        private void pnlBook_Paint(object sender, PaintEventArgs e)
        {
            //pnlBook.Visible = true;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
            bookIdToUpdate = "";
            txtBookId.Text = "";
            txtTitle.Text = "";
            cmbGenre.Text = "";
            txtBookPrice.Text = "";
            cmbPubYear.Text = "";
            btnSave.Text = "Save";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bookIdToUpdate != "")
            {
                 if(MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                string deleteQuery="Delete from book where book_id='"+bookIdToUpdate+"'";

                     conn connect=new conn();

                     if (connect.openConnection() == true)
                     {
                         try
                         {
                             MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);


                             cmd.ExecuteNonQuery();

                             MessageBox.Show("data deleted successfully");
                             bookIdToUpdate = "";
                             txtBookId.Text = "";
                             txtBookPrice.Text = "";
                             txtTitle.Text = "";
                             cmbGenre.Text = "";
                             cmbPubYear.Text = "";
                       

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

        private void label10_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
        }
    }
}
