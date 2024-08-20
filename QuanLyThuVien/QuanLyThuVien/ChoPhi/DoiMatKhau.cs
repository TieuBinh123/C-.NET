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
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        string connectionString = Properties.Settings.Default.sql;
        SqlConnection connection;
        private void button1_Click(object sender, EventArgs e)
        {
            //string taikhoanhientai = textBox4.Text;
            string matKhauHienTai = textBox1.Text;
            string matKhauMoi = textBox2.Text;
            string matKhauNhapLai = textBox3.Text;
            if (matKhauHienTai == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại");
                return;
            }
            else if (matKhauMoi == "")
            {
                MessageBox.Show("Vui lòng nhập vào mật khẩu mới");
                return;
            }
            else if (matKhauNhapLai == "")
            {
                MessageBox.Show("Vui lòng nhập lại pass");
                return;
            }
            else if (matKhauMoi != matKhauNhapLai)
            {
                MessageBox.Show("Mật khẩu mới và mật khẩu nhập lại không khớp. Vui lòng nhập lại.");
                return;
            }
            matKhauMoi = password.Create_MD5(matKhauMoi);




            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE dbo.[User] SET Matkhau = @password WHERE TaiKhoan = @username", connection);
            command.Parameters.AddWithValue("@password", matKhauMoi);
            command.Parameters.AddWithValue("@username", tdn.username);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();


            MessageBox.Show("Đổi mật khẩu thành công.");


            this.Close();
        }

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.z4953088472735_f2f11e36b6a2c7f4b656c8225376d9f6;
            this.BackgroundImageLayout = ImageLayout.Tile;
        }
    }
}
