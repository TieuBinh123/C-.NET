﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using QuanLyThuVien.ublites;
using System.Collections;
using System.Timers;

namespace QuanLyThuVien
{
    public partial class FormUser : Form
    {
        public FormUser()
        {
            InitializeComponent();
            
            

            // Tạo một truy vấn SQL để lấy dữ liệu từ cơ sở dữ liệu
            

        }

       
        string connectionString = Properties.Settings.Default.sql;
        SqlConnection connection;
        public void HienThiTruyen()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "select * from Sach";
            SqlCommand command = new SqlCommand(query, connection);

            // Dùng vòng lặp để duyệt qua kết quả của truy vấn SQL và thêm mỗi dòng dữ liệu vào ListView
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Thêm mỗi dòng dữ liệu vào ListView
                listView1.Items.Add(
                    new ListViewItem(
                        new string[] {
                            reader["MaSach"].ToString(),
                            reader["TenSach"].ToString(),
                            reader["GiaTien"].ToString(),
                            reader["MoTa"].ToString(),
                            reader["NgayXuatBan"].ToString(),
                            reader["SoLuongTon"].ToString(),
                            reader["MaChuDe"].ToString(),
                            reader["MaNXB"].ToString(),
                        }
                    )
                );
            }
            connection.Close();
        }
        public class RandomNumberGenerator
        {
            private static Random random = new Random();

            public static int GenerateRandomNumber(int minValue, int maxValue)
            {
                return random.Next(minValue, maxValue + 1);
            }
        }
        public void HienThiPhieuMuon()
        {
            
            connection = new SqlConnection(connectionString);
            connection.Open();
            

            // Tạo một truy vấn SQL để lấy dữ liệu từ cơ sở dữ liệu
            string query = "select * from PhieuMuon";
            SqlCommand command = new SqlCommand(query, connection);

            // Dùng vòng lặp để duyệt qua kết quả của truy vấn SQL và thêm mỗi dòng dữ liệu vào ListView
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Thêm mỗi dòng dữ liệu vào ListView
                listView2.Items.Add(
                    new ListViewItem(
                        new string[] {
                            reader["MaPhieuMuon"].ToString(),
                            reader["NgayMuon"].ToString(),
                            reader["PhiPhat"].ToString(),
                            reader["TinhTrang"].ToString(),
                            reader["MaUser"].ToString(),
                            reader["LDTC"].ToString(),
                            
                        }
                    )
                );
            }
            connection.Close();
        }
        public void HienThiPhieuMuonChuaDuocChapNhan()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();


            // Tạo một truy vấn SQL để lấy dữ liệu từ cơ sở dữ liệu
            string query = "select * from PhieuMuon";
            SqlCommand command = new SqlCommand(query, connection);

            // Dùng vòng lặp để duyệt qua kết quả của truy vấn SQL và thêm mỗi dòng dữ liệu vào ListView
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Kiểm tra nếu TinhTrang là Đang_chờ thì thêm dòng dữ liệu vào ListView
                if (reader["TinhTrang"].ToString() == "Đang Chờ")
                {
                    listView3.Items.Add(
                        new ListViewItem(
                            new string[] {
                        reader["MaPhieuMuon"].ToString(),
                        reader["NgayMuon"].ToString(),
                        reader["PhiPhat"].ToString(),
                        reader["TinhTrang"].ToString(),
                        reader["MaUser"].ToString(),
                        reader["LDTC"].ToString(),
                            }
                        )
                    );
                }
            }
            connection.Close();
        }

        public void HienThiChiTietPhieuMuon()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();


            // Tạo một truy vấn SQL để lấy dữ liệu từ cơ sở dữ liệu
            string sql = "SELECT ChiTietPhieuMuon.*, Sach.TenSach, Sach.MaNXB " +
                 "FROM ChiTietPhieuMuon " +
                 "INNER JOIN Sach ON ChiTietPhieuMuon.MaSach = Sach.MaSach";
            SqlCommand command = new SqlCommand(sql, connection);

            // Dùng vòng lặp để duyệt qua kết quả của truy vấn SQL và thêm mỗi dòng dữ liệu vào ListView
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Thêm mỗi dòng dữ liệu vào ListView
                listView4.Items.Add(
                    new ListViewItem(
                        new string[] {
                            reader["MaPhieuMuon"].ToString(),
                            reader["MaSach"].ToString(),
                            reader["SoLuong"].ToString(),
                            reader["DonGia"].ToString(),
                            reader["TenSach"].ToString(),
                            reader["MaNXB"].ToString(),
                        }
                    )
                );
            }
            connection.Close();
        }
        private void doiMatKhauToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ThongTinUser d1 = new ThongTinUser();
            d1.ShowDialog();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoiMatKhau d1 = new DoiMatKhau();
            d1.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 d1 = new Form1();
            this.Hide();
            d1.ShowDialog();
            this.Show();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            textBox11.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox12.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox13.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox14.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (radioTenSach.Checked == true)
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Sach WHERE TenSach = N'" + txtSearch.Text + "'", connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        // Xóa các item cũ trong ListView trước khi thêm mới
                        listView1.Items.Clear();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Thêm mỗi dòng dữ liệu vào ListView
                                listView1.Items.Add(
                                    new ListViewItem(
                                        new string[] {
                                    reader["MaSach"].ToString(),
                                    reader["TenSach"].ToString(),
                                    reader["GiaTien"].ToString(),
                                    reader["MoTa"].ToString(),
                                    reader["NgayXuatBan"].ToString(),
                                    reader["SoLuongTon"].ToString(),
                                    reader["MaChuDe"].ToString(),
                                    reader["MaNXB"].ToString(),
                                            // Thêm các cột khác tương ứng
                                        }
                                    )
                                );
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu.");
                        }
                    }
                }
                else
                {
                    using (SqlCommand command1 = new SqlCommand("SELECT s.* FROM Sach s WHERE s.MaSach = (SELECT tg.MaSach FROM ThamGia tg " +
                        "WHERE MaTacGia = (SELECT tacg.MaTacGia FROM TacGia tacg WHERE tacg.TenTacGia = @tentacgia))", connection))
                    {
                        command1.Parameters.AddWithValue("@tentacgia", txtSearch.Text);
                        SqlDataReader reader = command1.ExecuteReader();

                        // Xóa các item cũ trong ListView trước khi thêm mới
                        listView1.Items.Clear();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Thêm mỗi dòng dữ liệu vào ListView
                                listView1.Items.Add(
                                    new ListViewItem(
                                        new string[] {
                                    reader["MaSach"].ToString(),
                                    reader["TenSach"].ToString(),
                                    reader["GiaTien"].ToString(),
                                    reader["MoTa"].ToString(),
                                    reader["NgayXuatBan"].ToString(),
                                    reader["SoLuongTon"].ToString(),
                                    reader["MaChuDe"].ToString(),
                                    reader["MaNXB"].ToString(),
                                            // Thêm các cột khác tương ứng
                                        }
                                    )
                                );
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu.");
                            HienThiTruyen();
                        }
                    }
                }
            }
        }
        
        private void tabControl1_Enter(object sender, EventArgs e)
        {
            HienThiPhieuMuon();
            HienThiPhieuMuonChuaDuocChapNhan();
            HienThiChiTietPhieuMuon();
            button3.Enabled = false;
            
        }
        private Random random = new Random();
        public int GenerateRandomNumber()
        {
            // Tạo số ngẫu nhiên và trả về nó
            return random.Next(50, 300); // Số ngẫu nhiên từ 1 đến 100
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            SqlConnection connection = new SqlConnection(connectionString);

            

            // Xây dựng câu lệnh SQL
            string sql = "INSERT INTO PhieuMuon (MaPhieuMuon, NgayMuon, PhiPhat, TinhTrang, MaUser, LDTC) VALUES (@Maphieumuon, @Ngaymuon, @Phiphat, @Tinhtrang, @Mauser, @Ldtc)";

            // Thực thi câu lệnh SQL
            connection.Open();
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@MaPhieuMuon", "PM" + GenerateRandomNumber());
            command.Parameters.AddWithValue("NgayMuon", DateTime.Today.AddDays(20));
            command.Parameters.AddWithValue("@Phiphat",1 );
            command.Parameters.AddWithValue("@Tinhtrang", "Đang Chờ");
            command.Parameters.AddWithValue("@Mauser",GetUserMaUserByEmail(tdn.username));
            command.Parameters.AddWithValue("@Ldtc", "NULL");
            command.ExecuteNonQuery();
            connection.Close();


            // Thông báo thêm dữ liệu thành công
            MessageBox.Show("Thêm dữ liệu thành công!");

            
            //listView2.Items.Clear();
            //HienThiPhieuMuon();
        }

        
        private bool IsBookAvailable(string maSach)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            // Kiểm tra xem sách có sẵn hay không (thay thế bằng logic thích hợp)
            // Trả về true nếu sách có sẵn, ngược lại false
            string query = "SELECT COUNT(*) FROM Sach WHERE MaSach = @MaSach";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaSach", maSach);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            
        }

        private bool IsUserExists(string email)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            // Kiểm tra xem người dùng có tồn tại hay không (thay thế bằng logic thích hợp)
            // Trả về true nếu người dùng tồn tại, ngược lại false
            string query = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }

          
        }

        private string GenerateMaPhieuMuon()
        {
            // Triển khai logic tạo mã phieu muon duy nhất
            // Ví dụ đơn giản: "PM" + số tự tăng
            return "PM" + GetNextID();
        }

        private int GetNextID()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            // Triển khai logic lấy số tự tăng
            // Ví dụ đơn giản: lấy max ID + 1 từ bảng PhieuMuon
            
            string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(MaPhieuMuon, 3, LEN(MaPhieuMuon)) AS INT)), 0) + 1 FROM PhieuMuon Where ISNUMERIC(SUBSTRING(MaPhieuMuon, 3, LEN(MaPhieuMuon))) = 1";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        
        }

        private int GetBookGiaTien(string maSach)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            // Triển khai logic lấy giá tiền của sách từ bảng Sach
            // Giả sử sách có sẵn và có giá tiền là 10000
            string query = "SELECT GiaTien FROM Sach WHERE MaSach = @MaSach";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaSach", maSach);
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private string GetUserMaUserByEmail(string email)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            // Triển khai logic lấy MaUser từ bảng User dựa trên email
            // Giả sử lấy MaUser từ bảng User
            string query = "SELECT MaUser FROM [User] WHERE Email = @Email";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                object result = command.ExecuteScalar();
                return result != null ? result.ToString() : null;
            }
        
        }
        public decimal giamuon(string masach)
        {
            connection = new SqlConnection(connectionString);
            string query = "select GiaTien from dbo.Sach where MaSach = @Masach";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Masach", masach);
                connection.Open();
                object giaTienObject = sqlCommand.ExecuteScalar();
                connection.Close();
                if (giaTienObject != null)
                {
                    // Chuyển đổi giá trị sang kiểu decimal (hoặc kiểu dữ liệu tương ứng)
                    decimal giaTien = Convert.ToDecimal(giaTienObject);
                    MessageBox.Show(giaTien.ToString());
                    return giaTien;
                }
                else
                {
                    return 0;
                }
            }
               // return 0;
        }
        //public string tenuser()
        //{
        //    string a;
        //    connection = new SqlConnection(connectionString);
        //    string query = "select MaUser from dbo.[User] where TaiKhoan =  @Tentaikhoan";
        //    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
        //    {
        //        sqlCommand.Parameters.AddWithValue("@Tentaikhoan", tdn.username);
        //        connection.Open();
        //        a =  sqlCommand.ExecuteReader().ToString();
        //        connection.Close();
                
        //    }
        //    return a;
        //}

        
        private void FormUser_Load(object sender, EventArgs e)
        {
            thongTinTaiKhoanToolStripMenuItem.Text = tdn.username;
            HienThiTruyen();
            textBox4.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox13.Enabled = false;
            textBox14.Enabled = false;
            textBox3.Enabled = false;
            button2.Enabled = false;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            textBox4.Text = listView2.SelectedItems[0].SubItems[0].Text;
            textBox1.Text = listView2.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView2.SelectedItems[0].SubItems[3].Text;
            
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView2.selec != -1)
            //{
            //    // Hiển thị button
            //    btnEdit.Visible = true;
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                try
                {
                    // Tạo kết nối SQL
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Tạo truy vấn SQL DELETE
                        string deleteQuery = "DELETE FROM PhieuMuon WHERE MaPhieuMuon = @PrimaryKeyValue";

                        // Tạo đối tượng SqlCommand và thêm tham số
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@PrimaryKeyValue", textBox4.Text);

                            // Thực thi truy vấn DELETE
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Dữ liệu đã được xóa thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy dữ liệu để xóa!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập giá trị khóa chính hoặc điều kiện xác định dữ liệu cần xóa!");
            }
            listView2.Items.Clear();
            listView3.Items.Clear();
            //listView4.Items.Clear();
            connection.Close();

            //stView2.Clear();
            HienThiPhieuMuonChuaDuocChapNhan();
            HienThiPhieuMuon();
        }

        private void listView3_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            textBox4.Text = listView3.SelectedItems[0].SubItems[0].Text;
            textBox1.Text = listView3.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView3.SelectedItems[0].SubItems[3].Text;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
