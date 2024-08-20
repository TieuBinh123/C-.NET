using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;
using QuanLyThuVien.ublites;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyThuVien
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connectionString = Properties.Settings.Default.sql;
        SqlConnection connection;

        private void checkShow_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkShow.Checked)
            {
                txtPassword.PasswordChar = (char)0;
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }

        }
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
        public string Usernamee;
       
        private void button1_Click(object sender, EventArgs e)
        {
            string tk = textBox1.Text;
            connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM dbo.[User] WHERE TaiKhoan = @username AND MatKhau = @password", connection);
            command.Parameters.AddWithValue("@username", textBox1.Text);
            command.Parameters.AddWithValue("@password", GetMd5Hash(txtPassword.Text));
            //txtPassword.Text= password.Create_MD5(txtPassword.Text);
            
            SqlDataReader reader = command.ExecuteReader();
            
            
            if (reader.Read())
            {
                
                string role = reader["PhanLoai"].ToString();
                if(role == "admin")
                {
                    //
                }    
                else if(role == "user")
                {
                    tdn.username = tk;
                    MessageBox.Show("Dang nhap thanh cong");
                    FormUser d1 = new FormUser();
                    d1.ShowDialog();
                }    
                
            }
            else
            {
                // Đăng nhập thất bại
                reader.Close();
                connection.Close();
                MessageBox.Show("False");
            }

           // Usernamee = textBox1.Text;

        }

       
        

        private void CBLUUDN_CheckedChanged(object sender, EventArgs e)
        {
            /*if (textBox1.Text != "" && txtPassword.Text != "")
            {
                if (CBLUUDN.Checked == true)
                {
                    string User = textBox1.Text;
                    string Password = txtPassword.Text;
                    Properties.Settings.Default.User = User;
                    Properties.Settings.Default.Password = Password;
                    Properties.Settings.Default.Save();

                }
                else
                {
                    Properties.Settings.Default.Reset();
                }

            }*/
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            /*textBox1.Text = Properties.Settings.Default.User;
            txtPassword.Text = Properties.Settings.Default.Password;
            if (Properties.Settings.Default.User != "")
            {
                CBLUUDN.Checked = true;
            }*/
        }
    }
}
