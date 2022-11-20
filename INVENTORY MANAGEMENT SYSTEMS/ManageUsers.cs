using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace INVENTORY_MANAGEMENT_SYSTEMS
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-4R8CEG0J\SQLEXPRESS;Initial Catalog=Inventorydb;Integrated Security=True");
        

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void populate()
        {
            try
            {
                connection.Open();
                string Myquery = "select * from UserTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, connection);
                SqlCommandBuilder builder= new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                UsersScreen.DataSource = ds.Tables[0];
                connection.Close();
                

            }
            catch
            {



            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
          
            try 
            {
                connection.Open();
                SqlCommand command = new SqlCommand("insert into UserTbl values('" + UserName.Text + "','" + Ufullname.Text + "','" + Upassword.Text + "','" + Uphone.Text + "')", connection);
                command.ExecuteNonQuery();
                MessageBox.Show("User Successfully Added");
                connection.Close();
                populate();
            }

            catch
            {


            }
        }
        private void ManageUsers_Load(object sender, EventArgs e)
        {
            populate();

        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if(Uphone.Text=="")
            {
                MessageBox.Show("Enter The Users Phone Number");



            }
            else
            {
                connection.Open();
                string myquery = "delete from UserTbl where Uphone='" + Uphone.Text + "';";
                SqlCommand command = new SqlCommand(myquery, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("User Successfully Deleted");
                connection.Close();
                populate();



            }
        }

        private void UsersScreen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index=e.RowIndex;
            DataGridViewRow selectedRow = UsersScreen.Rows[index];
            UserName.Text = selectedRow.Cells[0].Value.ToString();
            Ufullname.Text = selectedRow.Cells[1].Value.ToString();
            Upassword.Text = selectedRow.Cells[2].Value.ToString();
            Uphone.Text = selectedRow.Cells[3].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("update UserTbl set UserName= '" + UserName.Text + "', Ufullname='" + Ufullname.Text + "',Upassword='"+ Upassword.Text+ "'where Uphone='"+Uphone.Text+"'", connection);
                command.ExecuteNonQuery();
                MessageBox.Show("User Successfully Updated");
                connection.Close();
                populate();
            }

            catch
            {


            }
        }
    }
}
