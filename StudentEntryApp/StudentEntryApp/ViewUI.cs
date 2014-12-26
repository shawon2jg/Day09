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
    public partial class ViewUI : Form
    {
        public ViewUI()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string inputId = searchIdTextBox.Text;
            string connectionString = @"Data Source = LAB2\SQLEXPRESS; Database = UniversityDB; Integrated Security = true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM tStudents";

            if (!string.IsNullOrEmpty(inputId))
            {
                query = "SELECT * FROM tStudents WHERE Id = '" + inputId + "'";
            }

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            resultListView.Items.Clear();

            while (reader.Read())
            {
                List<Student> studentList = new List<Student>();
                Student aStudent = new Student();

                aStudent.id =  (int) reader["Id"];
                aStudent.name = reader["Name"].ToString();
                aStudent.address = reader["Address"].ToString();
                aStudent.phone = reader["PhoneNumber"].ToString();
                aStudent.email = reader["EmailAddress"].ToString();

                string[] row ={aStudent.id.ToString(), aStudent.name, aStudent.address, aStudent.phone, aStudent.email};
                var listViewItem = new ListViewItem(row);
                resultListView.Items.Add(listViewItem);

                listViewItem.Tag = aStudent;
            }
            connection.Close();
        }

        private void resultListView_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem seletedItem = resultListView.SelectedItems[0];
            Student selectedStudent = (Student) seletedItem.Tag;

            idLabel.Text = selectedStudent.id.ToString();
            nameTextBox.Text = selectedStudent.name;
            emailAddressTextBox.Text = selectedStudent.email;
            addressTextBox.Text = selectedStudent.address;
            phoneTextBox.Text = selectedStudent.phone;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            string inputId = Convert.ToString(idLabel.Text);
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string email = emailAddressTextBox.Text;
            string phone = phoneTextBox.Text;

            string connectionString = @"Data Source = LAB2\SQLEXPRESS; Database = UniversityDB; Integrated Security = true";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string updateQuery = "Update tStudents SET Name='" + name + "',EmailAddress='" + email + "',Address='" + address + "',PhoneNumber='" + phone + "' WHERE Id = '"+inputId+"'" ;
                                  


            SqlCommand command = new SqlCommand(updateQuery, connection);
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();

            if (rowAffected > 0)
            {
                MessageBox.Show("Successfully Updated!");
            }
            else
            {
                MessageBox.Show("Couldn't Updated data!");
            }

            
            
        }

        
    }
}
