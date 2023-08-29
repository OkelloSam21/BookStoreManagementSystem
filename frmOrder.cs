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
    public partial class frmOrder : Form
    {
        string orderIdToUpdate;
        public frmOrder()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            pnlOrder.Left = (this.Width - pnlOrder.Width) / 2;
            pnlOrder.Top = (this.Height - pnlOrder.Height) / 2;
            pnlDataGrid.Visible = false;
            orderIdToUpdate = "";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Do you want to close the form", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Dispose();
            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderId.Text) | string.IsNullOrWhiteSpace(txtOrderDate.Text) | string.IsNullOrWhiteSpace(txtOrderDate.Text))
            {
                MessageBox.Show("Fill All Fields", "BIMS ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               
                try
                {
                    conn connect = new conn();

                    string insertquery;

                    if (orderIdToUpdate == "")
                    {
                        insertquery = "INSERT INTO order1(order_id,order_date,order_price) VALUES('"+txtOrderId.Text.ToString().Trim()+"','"+txtOrderDate.Text.ToString().Trim()+"','"+txtOrderPrice.Text.ToString().Trim()+"');";


                        if (connect.openConnection() == true)
                            {
                                MySqlCommand command = new MySqlCommand(insertquery, connect.connection);


                                try
                                {
                                     command.ExecuteNonQuery();

                                    
                                        MessageBox.Show("Order Data inserted successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        orderIdToUpdate = "";
                                        txtOrderId.Text = "";
                                        txtOrderDate.Text = "";
                                        txtOrderPrice.Text = "";

                                        
                                  
                                }
                                catch (MySqlException ex)
                                {
                                   // MessageBox.Show("Auther with ID " + txtOrderId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    MessageBox.Show(ex.Message);

                              
                                }
                                connect.closeConnection();
                        }
                    }
                  

                  
                    else
                    {
                        insertquery = "UPDATE order1 set order_id='"+txtOrderId.Text.ToString().Trim()+"',order_date='"+txtOrderDate.Text.ToString().Trim()+"',order_price='"+txtOrderPrice.Text.ToString().Trim()+"'where order_id='"+orderIdToUpdate +"';";


                        if (connect.openConnection() == true)
                        {
                            MySqlCommand command = new MySqlCommand(insertquery, connect.connection);

                            try
                            {
                                command.ExecuteNonQuery();


                                MessageBox.Show("Order Data updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                orderIdToUpdate = "";
                                txtOrderId.Text = "";
                                txtOrderDate.Text = "";
                                txtOrderPrice.Text = "";
                                orderIdToUpdate = "";

                            }
                            catch (MySqlException ex)
                            {
                                // MessageBox.Show("Auther with ID " + txtOrderId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show(ex.Message);


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

                if (criteria == "Order Id")
                {
                    query = "SELECT * FROM order1 WHERE order_id LIKE '" + searchValue + "' ORDER BY order_id ASC";
                }
                else if (criteria == "Order Date")
                {
                    query = "SELECT * FROM order1 WHERE order_date LIKE '" + searchValue + "' ORDER BY order_date ASC";
                }

                else if (criteria == "Order Price")
                {
                    query = "SELECT * FROM order1 WHERE order_price LIKE '" + searchValue + "' ORDER BY order_price ASC";
                }

                else if (criteria == "All")
                {
                    query = "SELECT * FROM order1 ORDER BY order_id ASC";
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
                            string[] row = new string[] { dataReader["order_id"].ToString(), dataReader["order_date"].ToString(), dataReader["order_price"].ToString() };
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearchVal.Text = "";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtOrderId.Text = "";
            txtOrderDate.Text = "";
            txtOrderPrice.Text = "";
            orderIdToUpdate = "";
            pnlDataGrid.Visible = false;
            btnSave.Text = "Save";
            txtOrderId.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            orderIdToUpdate = "";
            txtOrderId.Text = "";
            txtOrderDate.Text = "";
            txtOrderPrice.Text = "";
            txtOrderId.Focus();
            
            pnlDataGrid.Visible = false;
            btnSave.Text = "Save";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            txtOrderId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtOrderDate.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            txtOrderPrice.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            pnlDataGrid.Visible = false;

           orderIdToUpdate = txtOrderId.Text.ToString();

            
            btnSave.Text = "Update";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (orderIdToUpdate!= "")
            {
                if (MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deleteQuery = "Delete from order1 where order_id='" + orderIdToUpdate + "'";

                    conn connect = new conn();

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);


                            cmd.ExecuteNonQuery();

                            MessageBox.Show("data deleted successfully");

                            orderIdToUpdate = "";
                            txtOrderId.Text = "";
                            txtOrderDate.Text = "";
                            txtOrderPrice.Text = "";
                           
                            btnSave.Text = "Save";


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
