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

namespace StudentEntryApp
{
    public partial class StudentEntry : Form
    {
        public StudentEntry()
        {
            InitializeComponent();
        }

        

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Take user input
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string email = emailAddressTextBox.Text;
            string phone = phoneTextBox.Text;

            

            // Connect with database
            // 1. connectionString
            string connectionString = @"Data Source = LAB2\SQLEXPRESS; Database = UniversityDB; Integrated Security = true";

            // 2. Build a connection with connection string
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // Insert data into database
            // 1. Write down query
            //string query = "INSERT INTO tStudents VALUES('" + name + "','" + email + "','" + address + "','" + phone + "')";

            string query = string.Format("INSERT INTO tStudents VALUES('{0}','{1}','{2}','{3}')", name, email, address, phone);
            
            // 2. Execute query throught connection
            SqlCommand command = new SqlCommand(query,connection);
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowAffected > 0)
            {
                MessageBox.Show("Successfully Saved!");
            }
            else
            {
                MessageBox.Show("Couldn't saved data!");
            }

            testClear();
            
        }

        private void testClear()
        {
            nameTextBox.Text = string.Empty;
            addressTextBox.Text = string.Empty;
            emailAddressTextBox.Text = string.Empty;
            phoneTextBox.Text = string.Empty;
        }
    }
}
