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
    public partial class frmCustomer : Form
    {
       string customerIdToUpdate;
        public frmCustomer()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            pnlCustomer.Left = (this.Width - pnlCustomer.Width) / 2;
            pnlCustomer.Top = (this.Height - pnlCustomer.Height) / 2;
            pnlDataGrid.Visible = false;
            customerIdToUpdate = "";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerId.Text) | string.IsNullOrWhiteSpace(txtFirstName.Text) | string.IsNullOrWhiteSpace(txtLastName.Text) | string.IsNullOrWhiteSpace(txtCustomerId.Text) |
                string.IsNullOrWhiteSpace(txtAddress.Text) | string.IsNullOrWhiteSpace(txtCode.Text) | string.IsNullOrWhiteSpace(txtAddress.Text) | string.IsNullOrWhiteSpace(txtPhone.Text) | string.IsNullOrWhiteSpace(cmbCity.Text) | string.IsNullOrWhiteSpace(cmbCountry.Text))
            {
                MessageBox.Show("Fill all fields");
               

            }
            else
            {
                try
                {
                    conn connect = new conn();


                    string insertQuery;

                    if (customerIdToUpdate== "")
                    {
                        insertQuery = "INSERT INTO customer(customer_id,first_name,last_name,phone_number,postal_address,postal_code,city,country) VALUES('" + txtCustomerId.Text + "','" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtPhone.Text + "','" + txtAddress.Text + "','" + txtCode.Text + "','" + cmbCity.Text+ "','" + cmbCountry.Text + "')";

                        if (connect.openConnection() == true)
                        {

                            try
                            {
                                MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);

                                command.ExecuteNonQuery();
                                MessageBox.Show("Customer Data Added successfully");

                                customerIdToUpdate = "";
                                txtCustomerId.Text = "";
                                txtFirstName.Text = "";
                                txtLastName.Text = "";
                                txtPhone.Text = "";
                                txtAddress.Text = "";
                                txtCode.Text = "";
                                cmbCity.Text = "";
                                cmbCountry.Text = "";
                                txtCustomerId.Focus();
                                connect.closeConnection();

                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show("Customer with ID " + txtCustomerId.Text.ToString() + " Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            if (connect.openConnection() == true)
                            {
                                insertQuery = "UPDATE customer SET customer_id='" + txtCustomerId.Text + "',first_name='" + txtFirstName.Text +"',last_name='"+ txtLastName.Text + "',phone_number='" + txtPhone.Text + "',postal_address='" + txtAddress.Text + "',postal_code='" + txtCode.Text + "',city='" + cmbCity.Text + "',country='" + cmbCountry.Text + "'where customer_id='" + customerIdToUpdate + "'";

                                MySqlCommand command = new MySqlCommand(insertQuery, connect.connection);

                                command.ExecuteNonQuery();
                                MessageBox.Show("Customer Data Updated successfully");

                                customerIdToUpdate = "";
                                txtCustomerId.Text = "";
                                txtFirstName.Text = "";
                                txtLastName.Text = "";
                                txtPhone.Text = "";
                                txtAddress.Text = "";
                                txtCode.Text = "";
                                cmbCity.Text = "";
                                cmbCountry.Text = "";
                                txtCustomerId.Focus();
                                connect.closeConnection();
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }

                    

                }
                catch(Exception ex)
                {
                 MessageBox.Show("Error "+ ex.Message);

                }
                {
                }
            }
        }

        private void txtcustomerId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            customerIdToUpdate = "";
            txtCustomerId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtCode.Text = "";
            cmbCity.Text = "";
            cmbCountry.Text = "";
            pnlDataGrid.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void pnlDataGrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
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

                if (criteria == "Customer Id")
                {
                    query = "SELECT * FROM customer WHERE customer_id LIKE '" + searchValue + "' ORDER BY customer_id ASC";
                }
                else if (criteria == "First Name")
                {
                    query = "SELECT * FROM customer WHERE first_name LIKE '" + searchValue + "' ORDER BY first_name ASC";
                }

                else if (criteria == "Last Name")
                {
                    query = "SELECT * FROM customer WHERE last_name LIKE '" + searchValue + "' ORDER BY last_name ASC";
                }
                else if (criteria == "Address")
                {
                    query = "SELECT * FROM customer WHERE postal_address LIKE '" + searchValue + "' ORDER BY postal_address ASC";
                }
                else if (criteria == "postal Code")
                {
                    query = "SELECT * FROM customer WHERE postal_code LIKE '" + searchValue + "' ORDER BY postal_code ASC";
                }
                else if (criteria == "City")
                {
                    query = "SELECT * FROM customer WHERE city LIKE '" + searchValue + "' ORDER BY city ASC";
                }
                else if (criteria == "Phone Number")
                {
                    query = "SELECT * FROM customer WHERE phone_number LIKE '" + searchValue + "' ORDER BY phone_number ASC";
                }
                else if (criteria == "Country")
                {
                    query = "SELECT * FROM customer WHERE country LIKE '" + searchValue + "' ORDER BY country ASC";
                }

                else if (criteria == "All")
                {
                    query = "SELECT * FROM customer ORDER BY customer_id ASC";
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
                        string[] row = new string[] { dataReader["customer_id"].ToString(), dataReader["first_name"].ToString(), dataReader["last_name"].ToString(), dataReader["phone_number"].ToString(), dataReader["postal_address"].ToString(), dataReader["postal_code"].ToString(), dataReader["city"].ToString(), dataReader["country"].ToString()};
                        dataGridView1.Rows.Add(row);
                    }
                    dataReader.Close();
                    connect.closeConnection();
                }


            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
            btnSave.Text= "Save";
            txtCustomerId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtCode.Text = "";
            cmbCity.Text = "";
            cmbCountry.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtCustomerId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
                txtFirstName.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
                txtLastName.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
                txtPhone.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
                txtAddress.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString();
                txtCode.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[5].Value.ToString();
                cmbCity.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString();
                cmbCountry.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[7].Value.ToString();
                pnlDataGrid.Visible = false;

                customerIdToUpdate = txtCustomerId.Text.ToString();
                btnSave.Text = "Update";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
           
            customerIdToUpdate = "";
            txtCustomerId.Clear();
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtCode.Text = "";
            cmbCity.Text = "";
            cmbCountry.Text = "";
            txtCustomerId.Focus();
            btnSave.Text = "Save";
            pnlDataGrid.Visible = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (customerIdToUpdate != "")
            {
                if (MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deleteQuery = "Delete from customer where customer_id='" + customerIdToUpdate + "'";

                    conn connect = new conn();

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);

                        
                            cmd.ExecuteNonQuery();
                        }
                        catch (MySqlException ex)
                        {
                            if (ex.Number == 1451 | ex.Number == 1452) // Check for the specific error number for foreign key constraint violation
                            {
                                MessageBox.Show("Cannot delete customer. There are associated Books,Book Bundles in the book,book bundle tables respectively.");
                            }
                            else
                            {
                                MessageBox.Show("Error: " + ex.Message);
                            }
                        }
                        customerIdToUpdate = "";
                        txtCustomerId.Text = "";
                        txtFirstName.Text = "";
                        txtLastName.Text = "";
                        txtPhone.Text = "";
                        txtAddress.Text = "";
                        txtCode.Text = "";
                        cmbCity.Text = "";
                        cmbCountry.Text = "";


                        connect.closeConnection();
                    }
                }

            }
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
