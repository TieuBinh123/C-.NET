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
    public partial class ThongTinUser : Form
    {
        public ThongTinUser()
        {
            InitializeComponent();
            HienThiThongTin();
        }
        string connectionString = Properties.Settings.Default.sql;
        SqlConnection connection;
        public void HienThiThongTin()
        {
            string username = tdn.username;
            connection.Open();
            using (SqlCommand command = new SqlCommand("Select * from dbo.[User] Where TaiKhoan = @username", connection))
            {
                command.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Hiển thị tên tài khoản
                        textBox1.Text = reader["MaUser"].ToString();

                        // Hiển thị email
                        textBox2.Text = reader["HoTen"].ToString();

                        // Hiển thị số điện thoại
                        textBox3.Text = reader["NgaySinh"].ToString();

                        textBox4.Text = reader["GioiTinh"].ToString();

                        textBox5.Text = reader["DienThoai"].ToString();

                        textBox6.Text = reader["DiaChi"].ToString();

                    }
                }
            }



            connection.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime.Now.ToString(textBox3.Text);
            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("UPDATE dbo.[User] SET HoTen = @hoten, NgaySinh = @ngaysinh, GioiTinh = @gioitinh, DienThoai = @dienthoai, DiaChi = @diachi WHERE TaiKhoan = @username", connection);
            command.Parameters.AddWithValue("@hoten", textBox2.Text);
            command.Parameters.AddWithValue("@ngaysinh", DateTime.Now.ToString(textBox3.Text));
            command.Parameters.AddWithValue("@gioitinh", textBox4.Text);
            command.Parameters.AddWithValue("@dienthoai", textBox5.Text);
            command.Parameters.AddWithValue("@diachi", textBox6.Text);
            command.Parameters.AddWithValue("@username", tdn.username);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();


            MessageBox.Show("Đổi thông tin của cá nhân thành công");


            this.Close();
            ThongTinUser d1 = new ThongTinUser();
            d1.ShowDialog();
        }
    }
}
