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
    public partial class frmBookOrder : Form
    {

       string bookkOrderIdToUpdate;
        public frmBookOrder()
        {
            InitializeComponent();
            PopulateComboxBoxBookId();
            PopulateComboxBoxCustomerId();
            PopulateComboxBoxOrderId();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
           
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmBookOrder_Load(object sender, EventArgs e)
        {
            pnlBookOrder.Left = (this.Width - pnlBookOrder.Width) / 2;
            pnlBookOrder.Top = (this.Height - pnlBookOrder.Height) / 2;
            pnlDataGrid.Visible = false;
            bookkOrderIdToUpdate = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBookOrderId.Text) | string.IsNullOrWhiteSpace(cmbBookId.Text) | string.IsNullOrWhiteSpace(cmbCustomerId.Text) | string.IsNullOrWhiteSpace(cmbOrderId.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
              
                try
                {
                    conn connect = new conn();

                    string insertQuery;

                    if (bookkOrderIdToUpdate == "")
                    {

                        insertQuery = "INSERT INTO book_order(book_order_id,book_id,order_id,customer_id) VALUES('" + txtBookOrderId.Text.ToString().Trim() +"','"+cmbBookId.Text+ "','" + cmbOrderId.Text + "','" + cmbCustomerId.Text + "')";



                        if (connect.openConnection() == true)
                        {

                            MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);
                            try
                            {
                                command.ExecuteNonQuery();

                                MessageBox.Show("Book Order Data inserted successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                bookkOrderIdToUpdate = "";
                                txtBookOrderId.Text = "";
                                cmbBookId.Text = "";
                                cmbCustomerId.Text = "";
                                cmbOrderId.Text = "";
                                txtBookOrderId.Focus();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                // MessageBox.Show("Book Author with ID " + txtBookOrderId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBookOrderId.Focus();
                            }
                        }
                    }
                    else
                    {
                        insertQuery = "Update book_order set book_order_id='" + txtBookOrderId.Text.ToString().Trim()+"',book_id='"+cmbBookId.Text + "',order_id='" + cmbOrderId.Text + "',customer_id='" + cmbCustomerId.Text + "'where book_order_id='"+bookkOrderIdToUpdate+"'";



                        if (connect.openConnection() == true)
                        {

                            MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);
                            try
                            {
                                command.ExecuteNonQuery();

                                MessageBox.Show("Book Order Data updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                bookkOrderIdToUpdate = "";
                                txtBookOrderId.Text = "";
                                cmbBookId.Text = "";
                                cmbCustomerId.Text = "";
                                cmbOrderId.Text = "";
                                txtBookOrderId.Focus();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                // MessageBox.Show("Book Author with ID " + txtBookOrderId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtBookOrderId.Focus();
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

        
        private void PopulateComboxBoxOrderId()
        {
            try
            {
                conn connect = new conn();

                string query = "SELECT order_id FROM order1";

                if (connect.openConnection() == true)
                {
                    MySqlCommand command = new MySqlCommand(query, connect.connection);
                    MySqlDataReader dataReader = command.ExecuteReader();
                    this.cmbOrderId.Items.Clear();

                    while (dataReader.Read())
                    {
                        cmbOrderId.Items.Add(dataReader["order_id"].ToString());
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

        private void label7_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
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

                if (criteria == "Book Order Id")
                {
                    query = "SELECT * FROM book_order WHERE book_order_id LIKE '" + searchValue + "' ORDER BY book_order_id ASC";
                }
                else if (criteria == "Order Id")
                {
                    query = "SELECT * FROM book_order WHERE order_id LIKE '" + searchValue + "' ORDER BY order_id ASC";
                }
                else if (criteria == "Book Id")
                {
                    query = "SELECT * FROM book_order WHERE book_id LIKE '" + searchValue + "' ORDER BY book_id ASC";
                }

                else if (criteria == "Customer Id")
                {
                    query = "SELECT * FROM book_order WHERE customer_id LIKE '" + searchValue + "' ORDER BY customer_id ASC";
                }

                else if (criteria == "All")
                {
                    query = "SELECT * FROM book_order ORDER BY book_order_id ASC";
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
                            string[] row = new string[] { dataReader["book_order_id"].ToString(), dataReader["book_id"].ToString(), dataReader["order_id"].ToString(),dataReader["customer_id"].ToString()};
                            dataGridView1.Rows.Add(row);
                        }
                        dataReader.Close();
                        connect.closeConnection();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cmbSearchVal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bookkOrderIdToUpdate = "";
            txtBookOrderId.Text = "";
            cmbBookId.Text = "";
            cmbCustomerId.Text = "";
            cmbOrderId.Text = "";
            txtBookOrderId.Focus();
            pnlDataGrid.Visible = false;
            btnSave.Text = "Save";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            bookkOrderIdToUpdate = "";
            txtBookOrderId.Text = "";
            cmbBookId.Text = "";
            cmbCustomerId.Text = "";
            cmbOrderId.Text = "";
            txtBookOrderId.Focus();
            btnSave.Text = "Save";
            pnlDataGrid.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBookOrderId.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            cmbBookId.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            cmbOrderId.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            cmbCustomerId.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            
            pnlDataGrid.Visible = false;

            bookkOrderIdToUpdate = txtBookOrderId.Text;

            btnSave.Text = "Update";
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bookkOrderIdToUpdate != "")
            {
                if (MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deleteQuery = "Delete from book_order where book_order_id='" + bookkOrderIdToUpdate + "'";

                    conn connect = new conn();

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);


                            cmd.ExecuteNonQuery();

                            MessageBox.Show("data deleted successfully");

                            bookkOrderIdToUpdate = "";
                            txtBookOrderId.Text = "";
                            cmbBookId.Text = "";
                            cmbCustomerId.Text = "";
                            cmbOrderId.Text = "";
                            txtBookOrderId.Focus();


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
