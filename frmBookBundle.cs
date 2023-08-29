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
    public partial class frmBookBundle : Form
    {

        string bookBundleIdToUpdate;
        public frmBookBundle()
        {
            InitializeComponent();
            PopulateComboxBoxCustomerId();
            PopulateComboxBoxBundleId();
            PopulateComboxBoxBookId();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void frmBookBundle_Load(object sender, EventArgs e)
        {
            pnlBookBundle.Left = (this.Width - pnlBookBundle.Width) / 2;
            pnlBookBundle.Top = (this.Height - pnlBookBundle.Height) / 2;
            pnlDataGrid.Visible = false;
            bookBundleIdToUpdate="";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(cmbBundleId.Text) | string.IsNullOrWhiteSpace(cmbBookId.Text) | string.IsNullOrWhiteSpace(cmbCustomerId.Text) | string.IsNullOrWhiteSpace(txtBookBundleId.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                try
                {
                    conn connect = new conn();



                    string insertQuery;

                    if (bookBundleIdToUpdate == "")
                    {
                        insertQuery = "INSERT INTO book_bundle(book_bundle_id,bundle_id,customer_id,book_id) VALUES('" + txtBookBundleId.Text.ToString().Trim() + "','" + cmbBundleId.Text + "','" + cmbCustomerId.Text + "','" + cmbBookId.Text + "')";



                        if (connect.openConnection() == true)
                        {
                            MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);

                            try
                            {
                                command.ExecuteNonQuery();

                                MessageBox.Show("Book Bundle Data inserted successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bookBundleIdToUpdate = "";
                                txtBookBundleId.Text = "";
                                cmbBookId.Text = "";
                                cmbCustomerId.Text = "";
                                cmbBundleId.Text = "";
                                txtBookBundleId.Focus();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                // MessageBox.Show("Book Author with ID " + txtBookBundleId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBookBundleId.Focus();
                            }
                        }
                    }
                    else
                    {

                        insertQuery = "UPDATE book_bundle SET book_bundle_id='" + txtBookBundleId.Text.ToString().Trim() + "',bundle_id='" + cmbBundleId.Text + "',customer_id='" + cmbCustomerId.Text + "',book_id='" + cmbBookId.Text + "'where book_bundle_id='"+bookBundleIdToUpdate+"'";



                        if (connect.openConnection() == true)
                        {
                            MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);

                            try
                            {
                                command.ExecuteNonQuery();

                                MessageBox.Show("Book Bundle Data Updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                bookBundleIdToUpdate = "";
                                txtBookBundleId.Text = "";
                                cmbBookId.Text = "";
                                cmbCustomerId.Text = "";
                                cmbBundleId.Text = "";
                                txtBookBundleId.Focus();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                // MessageBox.Show("Book Author with ID " + txtBookBundleId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBookBundleId.Focus();
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex.Message);
                }
            }
        }

        private void PopulateComboxBoxBookId()
        {
            try
            {
                conn connect = new conn();

                string query = "SELECT book_id FROM book";

                if (connect.openConnection() == true)
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

        private void PopulateComboxBoxBundleId()
        {
            try
            {
                conn connect = new conn();

                string query = "SELECT bundle_id FROM bundle";

                if (connect.openConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, connect.connection);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    this.cmbBundleId.Items.Clear();

                    while (dataReader.Read())
                    {
                        cmbBundleId.Items.Add(dataReader["bundle_id"].ToString());
                    }
                    dataReader.Close();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }
        private void PopulateComboxBoxCustomerId()
        {
            try
            {
                conn connect = new conn();

                string query = "SELECT customer_id FROM customer";

                if (connect.openConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, connect.connection);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    this.cmbCustomerId.Items.Clear();

                    while (dataReader.Read())
                    {
                        cmbCustomerId.Items.Add(dataReader["customer_id"].ToString());
                    }
                    dataReader.Close();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBookBundleId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            cmbBundleId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            cmbCustomerId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            cmbBookId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
           
           
            bookBundleIdToUpdate = txtBookBundleId.Text;

            pnlDataGrid.Visible = false;
            btnSave.Text = "Update";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bookBundleIdToUpdate = "";
            txtBookBundleId.Text = "";
            cmbBookId.Text = "";
            cmbCustomerId.Text = "";
            cmbBundleId.Text = "";
            txtBookBundleId.Focus();
            pnlDataGrid.Visible = false;
            btnSave.Text = "Save";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            bookBundleIdToUpdate = "";
            txtBookBundleId.Text = "";
            cmbBookId.Text = "";
            cmbCustomerId.Text = "";
            cmbBundleId.Text = "";
            txtBookBundleId.Focus();
            btnSave.Text = "Save";
            pnlDataGrid.Visible = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
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

                if (criteria == "Book Bundle Id")
                {
                    query = "SELECT * FROM book_bundle WHERE book_bundle_id LIKE '" + searchValue + "' ORDER BY book_bundle_id ASC";
                }
                else if (criteria == "Bundle Id")
                {
                    query = "SELECT * FROM book_bundle WHERE bundle_id LIKE '" + searchValue + "' ORDER BY bundle_id ASC";
                }

                else if (criteria == "GCustomer Id")
                {
                    query = "SELECT * FROM book_bundle WHERE customer_id LIKE '" + searchValue + "' ORDER BY customer_id ASC";
                }
                else if (criteria == "Book Id")
                {
                    query = "SELECT * FROM book_bundle_id WHERE book_id LIKE '" + searchValue + "' ORDER BY book_id ASC";
                }
                else if (criteria == "All" | criteria == "")
                {
                    query = "SELECT * FROM book_bundle ORDER BY book_bundle_id ASC";
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
                        string[] row = new string[] { dataReader["book_bundle_id"].ToString(), dataReader["bundle_id"].ToString(), dataReader["customer_id"].ToString(), dataReader["book_id"].ToString()};
                        dataGridView1.Rows.Add(row);
                    }
                    dataReader.Close();
                    connect.closeConnection();
                }


            }

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            txtSearchVal.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bookBundleIdToUpdate!= "")
            {
                if (MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deleteQuery = "Delete from book_bundle where book_bundle_id='" + bookBundleIdToUpdate + "'";

                    conn connect = new conn();

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);


                            cmd.ExecuteNonQuery();

                            MessageBox.Show("data deleted successfully");

                            bookBundleIdToUpdate = "";
                            txtBookBundleId.Text = "";
                            cmbBookId.Text = "";
                            cmbCustomerId.Text = "";
                            cmbBundleId.Text = "";
                            txtBookBundleId.Focus();


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
