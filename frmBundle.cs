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
    public partial class frmBundle : Form
    {

        string bundleIdToUpdate;
        public frmBundle()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
        }

        private void frmBundle_Load(object sender, EventArgs e)
        {
            pnlBundle.Left = (this.Width - pnlBundle.Width) / 2;
            pnlBundle.Top = (this.Height - pnlBundle.Height) / 2;
            pnlDataGrid.Visible = false;
            bundleIdToUpdate = "";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBundleId.Text) | string.IsNullOrWhiteSpace(txtTotalPrice.Text) | string.IsNullOrWhiteSpace(txtBundleName.Text) | string.IsNullOrWhiteSpace(txtSalesEnd.Text) | string.IsNullOrWhiteSpace(txtSalesStart.Text))
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

                    if(bundleIdToUpdate=="")
                     {
                        
                        insertquery= "INSERT INTO bundle(bundle_id,total_price,sales_start_date,sales_end_date,bundle_name) VALUES('"+txtBundleId.Text.ToString().Trim()+"','"+txtTotalPrice.Text.ToString().Trim()+"','"+txtSalesStart.Text.ToString().Trim()+"','"+txtSalesEnd.Text.ToString().Trim()+"','"+txtBundleName.Text.ToString().Trim()+"')";

                        if (connect.openConnection() == true)
                        {
                        MySqlCommand command = new MySqlCommand(insertquery, connect.connection);

                        try
                        {
                            command.ExecuteNonQuery();

                            MessageBox.Show("Bundle Data inserted successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            bundleIdToUpdate = "";
                            txtBundleId.Text = "";
                            txtTotalPrice.Text = "";
                            txtSalesStart.Text = "";
                            txtSalesEnd.Text = "";
                            txtBundleName.Text = "";
                            txtBundleId.Focus();
                             
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.Message);
                            //MessageBox.Show(txtBundleId.Text.ToString() + ", Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }


                    }
                        
                    }
                       
                    else
                    {
                        insertquery = "UPdate bundle set bundle_id='" + txtBundleId.Text.ToString().Trim() + "',total_price='" + txtTotalPrice.Text.ToString().Trim() + "',sales_start_date='" + txtSalesStart.Text.ToString().Trim() + "',sales_end_date='" + txtSalesEnd.Text.ToString().Trim() +"',bundle_name='"+ txtBundleName.Text.ToString().Trim() + "'where bundle_id='"+bundleIdToUpdate+"'";

                        if (connect.openConnection() == true)
                        {
                            MySqlCommand command = new MySqlCommand(insertquery, connect.connection);

                            try
                            {
                                command.ExecuteNonQuery();

                                MessageBox.Show("Bundle Data Updated successfully!", "BIMS Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bundleIdToUpdate = "";
                                txtBundleId.Text = "";
                                txtTotalPrice.Text = "";
                                txtSalesStart.Text = "";
                                txtSalesEnd.Text = "";
                                txtBundleName.Text = "";
                                txtBundleId.Focus();

                            }
                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                                //MessageBox.Show(txtBundleId.Text.ToString() + ", Already exist", "BIMS Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        }
                        
                    }
                }

                catch (Exception ex)
                {

                    MessageBox.Show(" " + ex);
                }
            }
        }

        private void txtSalesStart_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
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
                searchValue = "%" + txtSearchVal.Text.ToString() + "%";

                if (criteria == "Bundle Id")
                {
                    query = "SELECT * FROM bundle WHERE bundle_id LIKE '" + searchValue + "' ORDER BY bundle_id ASC";
                }
                else if (criteria == "Bundle Name")
                {
                    query = "SELECT * FROM bundle WHERE bundle_name LIKE '" + searchValue + "' ORDER BY bundle_name ASC";
                }
                else if (criteria == "Total Price")
                {
                    query = "SELECT * FROM bundle WHERE total_price LIKE '" + searchValue + "' ORDER BY total_price ASC";
                }

                else if (criteria == "Sales Start Date")
                {
                    query = "SELECT * FROM bundle WHERE sales_start_date LIKE '" + searchValue + "' ORDER BY sales_start_date ASC";
                }
                else if (criteria == "Sales End Date")
                {
                    query = "SELECT * FROM bundle WHERE sales_end_date LIKE '" + searchValue + "' ORDER BY sales_end_date ASC";
                }

                else if (criteria == "All")
                {
                    query = "SELECT * FROM bundle ORDER BY bundle_id ASC";
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
                            string[] row = new string[] { dataReader["bundle_id"].ToString(), dataReader["total_price"].ToString(), dataReader["sales_start_date"].ToString(), dataReader["sales_end_date"].ToString(),dataReader["bundle_name"].ToString()};
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

        private void pnlBundle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = true;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            txtSearchVal.Text = "";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            pnlDataGrid.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtBundleId.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtTotalPrice.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            txtSalesStart.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            txtSalesEnd.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString();
            txtBundleName.Text= this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[4].Value.ToString();
            pnlDataGrid.Visible = false;

            bundleIdToUpdate = txtBundleId.Text;

            btnSave.Text = "Update";
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bundleIdToUpdate != "")
            {
                if (MessageBox.Show("are sure you want to delete this record?", "BIMS Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string deleteQuery = "Delete from bundle where bundle_id='" + bundleIdToUpdate + "'";

                    conn connect = new conn();

                    if (connect.openConnection() == true)
                    {
                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(deleteQuery, connect.connection);


                            cmd.ExecuteNonQuery();

                            MessageBox.Show("data deleted successfully");

                            bundleIdToUpdate = "";
                            txtBundleId.Text = "";
                            txtBundleName.Text = "";
                            txtSalesEnd.Text= "";
                            txtSalesStart.Text= "";
                            txtTotalPrice.Text = "";


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

        private void btnReset_Click(object sender, EventArgs e)
        {
            bundleIdToUpdate = "";
            txtBundleId.Text = "";
            txtBundleName.Text = "";
            txtSalesEnd.Text = "";
            txtSalesStart.Text = "";
            txtTotalPrice.Text = "";
            txtBundleId.Focus();
            btnSave.Text = "Save";
            pnlDataGrid.Visible = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            bundleIdToUpdate = "";
            txtBundleId.Text = "";
            txtBundleName.Text = "";
            txtSalesEnd.Text = "";
            txtSalesStart.Text = "";
            txtTotalPrice.Text = "";
            txtBundleId.Focus();
            btnSave.Text = "Save";
            pnlDataGrid.Visible = false;
        }
    }
}
