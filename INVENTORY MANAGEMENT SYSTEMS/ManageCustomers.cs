using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace INVENTORY_MANAGEMENT_SYSTEMS
{
    public partial class ManageCustomers : Form
    {
        public ManageCustomers()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-4R8CEG0J\SQLEXPRESS;Initial Catalog=Inventorydb;Integrated Security=True");

        void populate()
        {
            try
            {
                connection.Open();
                string Myquery = "select * from CustomerTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, connection);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                CustomersScreen.DataSource = ds.Tables[0];
                connection.Close();


            }
            catch
            {



            }

        }
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into CustomerTbl values('" + CustomerId.Text + "','" + Ucustomername.Text + "','" + Ucustomerphone.Text + "')", connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Added");
                connection.Close();
                populate();
            }

            catch
            {


            }
        }

        private void ManageCustomers_Load(object sender, EventArgs e)
        {
            populate();

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (CustomerId.Text == "")
            {
                MessageBox.Show("Enter The Customer Phone Number");



            }
            else
            {
                connection.Open();
                string myquery = "delete from CustomerTbl where CustomerId='" + CustomerId.Text + "';";
                SqlCommand command = new SqlCommand(myquery, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Deleted");
                connection.Close();
                populate();



            }
        }

        private void CustomersScreen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = CustomersScreen.Rows[index];
            CustomerId.Text = selectedRow.Cells[0].Value.ToString();
            Ucustomername.Text = selectedRow.Cells[1].Value.ToString();
            Ucustomerphone.Text = selectedRow.Cells[2].Value.ToString();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("update CustomerTbl set Ucustomername= '" + Ucustomername.Text + "', Ucustomerphone='" + Ucustomerphone.Text  + "'", connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated");
                connection.Close();
                populate();
            }

            catch
            {


            }
        }
    }
}
