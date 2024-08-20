using QuanLyThuVien.ublites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string connectionString = Properties.Settings.Default.sql;
        SqlConnection connection;


        private void label12_Click(object sender, EventArgs e)
        {
            ThongTinUser f1 = new ThongTinUser();
            this.Hide();
            f1.ShowDialog(); 
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            

            string taikhoanhientai = textBox4.Text;
            string matKhauHienTai = textBox1.Text;
            string matKhauMoi = textBox2.Text;
            string matKhauNhapLai = textBox3.Text;
            if (matKhauHienTai == "") MessageBox.Show("Vui lòng nhập mật khẩu hiện tại");
            else if (matKhauMoi == "") MessageBox.Show("Vui lòng nhập vào mật khẩu mới");
            else if (matKhauNhapLai == "") MessageBox.Show("Vui lòng nhập lại pass");
            else if (taikhoanhientai == "") MessageBox.Show("Vui lòng nhập vào tài khoản cần đổi mật khẩu");
            if (matKhauMoi != matKhauNhapLai)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu nhập lại không khớp. Vui lòng nhập lại.");
                return;
            }
            matKhauMoi = password.Create_MD5(matKhauMoi);




            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE dbo.[User] SET Matkhau = @password WHERE TaiKhoan = @username", connection);
            command.Parameters.AddWithValue("@password", matKhauMoi);
            command.Parameters.AddWithValue("@username", textBox4.Text);

            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            
            MessageBox.Show("Đổi mật khẩu thành công.");

           
            this.Close();
        }
    }
}
