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
using QuanLyThuVien.ublites;

namespace QuanLyThuVien.ChoPhi
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        string connectionString = Properties.Settings.Default.sql;
        SqlConnection connection;
        public string Usernamee;
        private void button1_Click(object sender, EventArgs e)
        {
            string tk = textBox1.Text;
            connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM dbo.[User] WHERE TaiKhoan = @username AND MatKhau = @password", connection);
            command.Parameters.AddWithValue("@username", textBox1.Text);
            command.Parameters.AddWithValue("@password", password.GetMd5Hash(txtPassword.Text));
            //txtPassword.Text= password.Create_MD5(txtPassword.Text);
            //MessageBox.Show(textBox1.Text);
            //MessageBox.Show(txtPassword.Text);
            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {

                string role = reader["PhanLoai"].ToString();
                if (role == "admin")
                {
                    Form Adminform = new Adminform();
                    this.Hide();
                    Adminform.ShowDialog();
                    
                    this.Show();
                }
                else if (role == "user")
                {
                    tdn.username = tk;
                    MessageBox.Show("Dang nhap thanh cong");
                    FormUser d1 = new FormUser();
                    this.Hide();
                    d1.ShowDialog();
                    this.Show();
                }

            }
            else
            {
                // Đăng nhập thất bại
                reader.Close();
                connection.Close();
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }

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

        private void CBLUUDN_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && txtPassword.Text != "")
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

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPassword_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.User;
            txtPassword.Text = Properties.Settings.Default.Password;
            if (Properties.Settings.Default.User != "")
            {
                CBLUUDN.Checked = true;
            }
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
