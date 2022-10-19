using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataAccess dataAccess;
        List<Class1> listofall;
        public Form1()
        {
            InitializeComponent();
            dataAccess = new DataAccess();
            loadDataFromDB();
            
        }
        private void loadDataFromDB()
        {
            listofall = dataAccess.getAllsignup();
           fbGV.DataSource = listofall;
         }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void signupButton_Click(object sender, EventArgs e)
        {
           
            string emailText = emailTextBox.Text.ToString();
            if (emailText.Equals(""))
            { MessageBox.Show("Please Enter E-mail");
                return;
            }
            string passwordText = passwordTextBox.Text.ToString();
            if (passwordText.Equals(""))
            { MessageBox.Show("Please Enter Password");
                return;
            }
            string nameText = nameTextBox.Text.ToString();
            if (nameText.Equals(""))
            {
                MessageBox.Show("Please Enter Name");
                return;
            }

            string bd1Text = "";
            if (comboBox1.SelectedIndex > -1)
            {
                bd1Text = comboBox1.SelectedItem.ToString();
            }
            string bd2Text = "";
            if (comboBox2.SelectedIndex > -1)
            {
                bd2Text = comboBox2.SelectedItem.ToString();
            }
            string bd3Text = "";
            if (comboBox3.SelectedIndex > -1)
            {
                bd3Text = comboBox3.SelectedItem.ToString();
            }
            if(bd1Text.Equals("") || bd2Text.Equals("") || bd3Text.Equals(""))
            {
                MessageBox.Show("Please Enter Birthday");

            }

            FormGender genderText = FormGender.FEMALE;
            if (femaleRadioButton.Checked)
            {
                genderText = FormGender.FEMALE;
            }
            else if (maleRadioButton.Checked)
            {
                genderText = FormGender.MALE;
            }
            else
            {
                genderText = FormGender.CUSTOM; 
            }


           int recieved = dataAccess.InsertDataIntoFB( emailText, passwordText, nameText, bd1Text, bd2Text, bd3Text, genderText);
         
            if(recieved == 1)
            {
                MessageBox.Show("Data Successfully Recieved");
                loadDataFromDB();
            }
            dataAccess.InsertDataIntoLogIn(emailText, passwordText);



            try
            {
                var filename = string.Format("User-{0}.txt", emailTextBox.Text);
                FileStream fb = new FileStream(filename, FileMode.OpenOrCreate);
                using (StreamWriter adduserdatawrite = new StreamWriter(fb))
                {
                    adduserdatawrite.Write(nameTextBox.Text + "\r\n");
                    adduserdatawrite.Write(emailTextBox.Text + "\r\n");
                    adduserdatawrite.Write(passwordTextBox.Text + "\r\n");
                    adduserdatawrite.Write(comboBox1.Text + "\r\n");
                    adduserdatawrite.Write(comboBox2.Text + "\r\n");
                    adduserdatawrite.Write(comboBox3.Text + "\r\n");
                    if (femaleRadioButton.Checked)
                        {
                            adduserdatawrite.Write(femaleRadioButton.Text + "\r\n");

                        }
                        else if (maleRadioButton.Checked)
                        {
                                adduserdatawrite.Write(maleRadioButton.Text + "\r\n");

                            }
                        else if (customRadioButton.Checked)
                        {
                                    adduserdatawrite.Write(customRadioButton.Text + "\r\n");

                                }
                        else {
                                }
                                }
                            }
                catch (Exception)
            {
                //  messagebox.show(exc.message, "error", messageboxbuttons.ok, messageboxicon.error);
            }


        }

        private void loginButton_Click(object sender, EventArgs e)
        {

            string emailText1 = emailTextBox1.Text.ToString();
            if (emailText1.Equals(""))
            {
                MessageBox.Show("Please Enter E-mail");
                return;
            }

            string passwordText1 = passwordTextBox1.Text.ToString();
            if (passwordText1.Equals(""))
            {
                MessageBox.Show("Please Enter Password");
                return;
            }

            SqlConnection sqlcon = new SqlConnection(@"Data Source=DESKTOP-KJREPTE;Initial Catalog=facebook;Integrated Security=True");
                string query = "SELECT * FROM fblogin WHERE Email='" + emailTextBox1.Text + "' AND PAssword ='" + passwordTextBox1.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);

                if (dtbl.Rows.Count == 1)
                {
                    Form2 form2 = new Form2();
                    this.Hide();
                    form2.Show();

                }
               else
                { MessageBox.Show("Please Sign Up First"); }
            
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            emailTextBox1.Text = "";
            passwordTextBox1.Text = "";
            emailTextBox.Text = "";
            passwordTextBox.Text = "";
            nameTextBox.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            femaleRadioButton.Checked = true;
            maleRadioButton.Checked = false;
            customRadioButton.Checked = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //var filename = string.Format("User-{0}.txt", emailTextBox.Text);
            //FileStream fb = new FileStream(filename, FileMode.Open , FileAccess.Read);
            //StreamReader sr = new StreamReader(fb);
            //textBox2.Text = sr.ReadToEnd();
            //fb.Close();
            try
            {
                //Open file dialog, allows you to select a txt file
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", Multiselect = false, ValidateNames = true })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            textBox2.Text = await sr.ReadToEndAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

       
    }
}
