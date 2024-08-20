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
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Reflection;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using ZXing;

namespace QuanLyThuVien
{
    public partial class Quanlithuvien : Form
    {
        public Quanlithuvien()
        {
            InitializeComponent();
            SetTabControlBackground();
        }
        string sql = Properties.Settings.Default.sql;
        SqlConnection conn = new SqlConnection();
        private void Quanlithuvien_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyThuVienDataSet2.TacGia' table. You can move, or remove it, as needed.
            this.tacGiaTableAdapter.Fill(this.quanLyThuVienDataSet2.TacGia);
            // TODO: This line of code loads data into the 'quanLyThuVienDataSet1.Sach' table. You can move, or remove it, as needed.
            this.sachTableAdapter.Fill(this.quanLyThuVienDataSet1.Sach);

        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            loadtablesach();
            combobox();
            comboboxtg();
            comboboxcd();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            loadtablesach();
            cleartest();
        }

        public void combobox()
        {
            conn = new SqlConnection(sql);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select MaNXB from dbo.NhaXuatBan", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            ts_nxb.Items.Clear();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string cd = reader.GetString(0);
                    ts_nxb.Items.Add(cd);
                }
            }
            else
            {
                reader.Close();
                conn.Close();
                MessageBox.Show("False");
            }
            conn.Close();
        }
        public void comboboxtg()
        {
            conn = new SqlConnection(sql);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("select MaTacGia from dbo.TacGia", conn);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            ts_tg.Items.Clear();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    string cd = reader1.GetString(0);
                    ts_tg.Items.Add(cd);
                }
            }
            else
            {
                reader1.Close();
                conn.Close();
                MessageBox.Show("False");
            }
            conn.Close();
        }
        public void comboboxcd()
        {
            conn = new SqlConnection(sql);
            conn.Open();
            SqlCommand cmd1 = new SqlCommand("select MaChuDe from dbo.ChuDe", conn);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            ts_cd.Items.Clear();
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    string cd = reader1.GetString(0);
                    ts_cd.Items.Add(cd);
                }
            }
            else
            {
                reader1.Close();
                conn.Close();
                MessageBox.Show("False");
            }
            conn.Close();
        }
        private void insert_Click(object sender, EventArgs e)
        {
            string maSach = ts_ms.Text;
            string tenSach = ts_ts.Text;
            string theLoai = ts_cd.Text;
            string nhaXuatBan = ts_nxb.Text;
            DateTime ngayXuatBan = ts_time.Value;
            string tacGia = ts_tg.Text;
            int soLuong = Convert.ToInt32(ts_sl.Text);
            decimal giaTien = Convert.ToDecimal(ts_gt.Text);
            string tg = ts_tg.Text;
            AddBookToDatabase(maSach, tenSach, theLoai, nhaXuatBan, ngayXuatBan, tacGia, soLuong, giaTien, tg);
            loadtablesach();
            cleartest();
        }
        private void AddBookToDatabase(string maSach, string tenSach, string theLoai, string nhaXuatBan, DateTime ngayXuatBan, string tacGia, int soLuong, decimal giaTien, string tg)
        {
            try
            {
                using (conn = new SqlConnection(sql))
                {
                    conn.Open();

                    string query = "INSERT INTO Sach (MaSach, TenSach, MaChuDe, MaNXB, NgayXuatBan, SoLuongTon, GiaTien) VALUES (@MaSach," +
                        "N'" + tenSach + "', @MaChuDe, @MaNXB, @NgayXuatBan, @SoLuongTon, @GiaTien)";


                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@MaSach", maSach);
                        command.Parameters.AddWithValue("@MaChuDe", theLoai);
                        command.Parameters.AddWithValue("@MaNXB", nhaXuatBan);
                        command.Parameters.AddWithValue("@NgayXuatBan", ngayXuatBan);
                        command.Parameters.AddWithValue("@SoLuongTon", soLuong);
                        command.Parameters.AddWithValue("@GiaTien", giaTien);

                        command.ExecuteNonQuery();
                    }
                    string query1 = "INSERT INTO dbo.ThamGia (MaSach, MaTacGia, VaiTro, ViTri) VALUES (@MaSach, @MaTacGia, @VaiTro, @ViTri)";

                    using (SqlCommand command = new SqlCommand(query1, conn))
                    {
                        command.Parameters.AddWithValue("@MaSach", maSach);
                        command.Parameters.AddWithValue("@MaTacGia", tg);
                        command.Parameters.AddWithValue("@VaiTro", "Tác giả");
                        command.Parameters.AddWithValue("@ViTri", "1");

                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sách vào CSDL: " + ex.Message);
            }
        }
        public void cleartest()
        {
            ts_ms.Text = string.Empty;
            ts_cd.Text = string.Empty;
            ts_ts.Text = string.Empty;
            ts_time.Text = string.Empty;
            ts_nxb.Text = string.Empty;
            ts_tg.Text = string.Empty;
            ts_sl.Text = string.Empty;
            ts_gt.Text = string.Empty;
            insert.Enabled = true;
        }
        public void loadtablesach()
        {
            conn = new SqlConnection(sql);
            conn.Open();
            SqlCommand command = new SqlCommand("SELECT a.MaSach, a.TenSach, b.TenChuDe, a.MaNXB, a.NgayXuatBan, c.MaTacGia,a.SoLuongTon, a.GiaTien FROM dbo.Sach a, dbo.ChuDe b, dbo.ThamGia c " +
                "WHERE a.MaChuDe = b.MaChuDe and c.MaSach = a.MaSach", conn);



            // Thực thi câu lệnh truy vấn
            SqlDataReader reader = command.ExecuteReader();
            //reader.Read();
            dstruyen.Items.Clear();
            // Kiểm tra kết quả
            if (reader.HasRows)
            {
                int i = 1;
                while (reader.Read())
                {
                    string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[3].ToString(), reader[2].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[4].ToString() };
                    ListViewItem a1 = new ListViewItem(cd);
                    dstruyen.Items.Add(a1);
                    i++;
                }
                conn.Close();
            }
            else
            {
                reader.Close();
                conn.Close();
                MessageBox.Show("False");
            }

        }
        private void tk_Click(object sender, EventArgs e)
        {
            if (tk_ms.Checked == true)
            {
                string a = ts_text.Text;
                conn = new SqlConnection(sql);
                conn.Open();
                int i = 1;
                using (SqlCommand command = new SqlCommand("SELECT a.MaSach, a.TenSach, b.TenChuDe, a.MaNXB, a.NgayXuatBan, a.MaSach, " +
                    "a.SoLuongTon, a.GiaTien FROM dbo.Sach a, dbo.ChuDe b " +
                    "WHERE a.MaChuDe = b.MaChuDe and a.MaSach = @masach", conn))
                {
                    command.Parameters.AddWithValue("@masach", a);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dstruyen.Items.Clear();
                        if (reader.Read()) // Kiểm tra xem có dữ liệu hay không
                        {
                            string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[3].ToString(), reader[2].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[4].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            dstruyen.Items.Add(a1);
                        }
                    }
                }
            }
            else if (tk_nxb.Checked == true)
            {
                string maNXB = ts_text.Text;
                conn = new SqlConnection(sql);
                conn.Open();
                int i = 1;
                using (SqlCommand command = new SqlCommand("SELECT a.MaSach, a.TenSach, b.TenChuDe, a.MaNXB, a.NgayXuatBan, a.SoLuongTon, a.GiaTien " +
                                                            "FROM dbo.Sach a INNER JOIN dbo.ChuDe b ON a.MaChuDe = b.MaChuDe " +
                                                            "WHERE a.MaNXB = @MaNXB", conn))
                {
                    command.Parameters.AddWithValue("@MaNXB", maNXB);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dstruyen.Items.Clear();
                        while (reader.Read()) // Kiểm tra xem có dữ liệu hay không
                        {
                            string[] cd = { i.ToString(), reader["MaSach"].ToString(), reader["TenSach"].ToString(), reader["MaNXB"].ToString(), reader["TenChuDe"].ToString(), reader["SoLuongTon"].ToString(), reader["GiaTien"].ToString(), reader["NgayXuatBan"].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            dstruyen.Items.Add(a1);
                            i++;
                        }
                    }
                }
            }

            else if (tk_tl.Checked == true)
            {
                string tl = ts_text.Text;
                conn = new SqlConnection(sql);
                conn.Open();
                int i = 1;
                using (SqlCommand command = new SqlCommand("SELECT a.MaSach, a.TenSach, b.TenChuDe, a.MaNXB, a.NgayXuatBan, a.SoLuongTon, a.GiaTien " +
                    "FROM dbo.Sach a INNER JOIN dbo.ChuDe b ON a.MaChuDe = b.MaChuDe  " +
                    "WHERE b.TenChuDe = N'" + tl + "'", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dstruyen.Items.Clear();
                        while (reader.Read()) // Kiểm tra xem có dữ liệu hay không
                        {
                            string[] cd = { i.ToString(), reader["MaSach"].ToString(), reader["TenSach"].ToString(), reader["MaNXB"].ToString(), reader["TenChuDe"].ToString(), reader["SoLuongTon"].ToString(), reader["GiaTien"].ToString(), reader["NgayXuatBan"].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            dstruyen.Items.Add(a1);
                            i++;
                        }
                    }
                }
            }
        }
        private void dstruyen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dstruyen.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = dstruyen.SelectedItems[0];

                // Lấy dữ liệu từ dòng được chọn và hiển thị lên TextBox
                ts_ms.Text = selectedItem.SubItems[1].Text;
                ts_ts.Text = selectedItem.SubItems[2].Text;
                string cd = selectedItem.SubItems[4].Text;
                ts_cd.Text = macd(cd);
                ts_nxb.Text = selectedItem.SubItems[3].Text;

                // Lưu ý: Kiểm tra kiểu và xử lý ngoại lệ cho phần chuyển đổi kiểu dữ liệu
                DateTime ngayXuatBan;
                if (DateTime.TryParse(selectedItem.SubItems[8].Text, out ngayXuatBan))
                {
                    ts_time.Value = ngayXuatBan;
                }

                ts_tg.Text = selectedItem.SubItems[5].Text;
                ts_sl.Text = selectedItem.SubItems[6].Text;
                ts_gt.Text = selectedItem.SubItems[7].Text;
                delete.Enabled = true;
                update.Enabled = true;
                insert.Enabled = false;
            }
        }
        public string macd(string cd)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sql))
                {
                    conn.Open();

                    string query = "SELECT MaChuDe FROM dbo.ChuDe WHERE TenChuDe = @TenChuDe";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Sử dụng SqlParameter để tránh SQL injection
                        command.Parameters.AddWithValue("@TenChuDe", cd);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Kiểm tra xem có dữ liệu trước khi đọc
                                string maChuDe = reader.GetString(0);
                                return maChuDe;
                            }
                            // Không tìm thấy dữ liệu
                            return "a";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn CSDL: " + ex.Message);
            }

            return "a";

        }
        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sql))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("XoaSachVaLienQuan", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaSach", ts_ms.Text);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sách và dữ liệu liên quan: " + ex.Message);
            }
            loadtablesach();
            cleartest();
        }

        private void update_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sql))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand("CapNhatThongTinSach", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MaSach", ts_ms.Text);
                        command.Parameters.AddWithValue("@TenSach", ts_ts.Text);
                        command.Parameters.AddWithValue("@MaChuDe", ts_cd.Text);
                        command.Parameters.AddWithValue("@MaNXB", ts_nxb.Text);
                        command.Parameters.AddWithValue("@NgayXuatBan", ts_time.Text);
                        command.Parameters.AddWithValue("@MaTacGia", ts_tg.Text);
                        command.Parameters.AddWithValue("@SoLuong", ts_sl.Text);
                        command.Parameters.AddWithValue("@GiaTien", ts_gt.Text);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin sách: " + ex.Message);
            }
            loadtablesach();
            cleartest();
        }



        //Phiếu mượn:
        private void tabPage3_Enter(object sender, EventArgs e)
        {
            phieumuon();
            PhieuCho();
        }
        private string maPhieuMuonSelected;
        public void phieumuon()
        {
            try
            {
                conn = new SqlConnection(sql);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT a.MaUser, a.HoTen, d.TenSach, b.NgayMuon, b.TinhTrang, b.PhiPhat " +
                    "FROM dbo.[User] a " +
                    "INNER JOIN dbo.PhieuMuon b ON a.MaUser = b.MaUser " +
                    "INNER JOIN dbo.ChiTietPhieuMuon c ON b.MaPhieuMuon = c.MaPhieuMuon " +
                    "INNER JOIN dbo.Sach d ON c.MaSach = d.MaSach", conn);

                SqlDataReader reader = cmd.ExecuteReader();
                pm_all.Items.Clear();

                if (reader.HasRows)
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        DateTime dt = reader.GetDateTime(3);
                        dt = dt.AddDays(20);
                        string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), dt.ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString() };
                        ListViewItem a2 = new ListViewItem(cd);
                        pm_all.Items.Add(a2);
                        i++;
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public void PhieuCho()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sql))
                {
                    conn.Open();
                    string query = "SELECT a.MaUser, a.HoTen, d.TenSach, b.TinhTrang FROM dbo.[User] a " +
                                   "INNER JOIN dbo.PhieuMuon b ON a.MaUser = b.MaUser " +
                                   "INNER JOIN dbo.ChiTietPhieuMuon c ON b.MaPhieuMuon = c.MaPhieuMuon " +
                                   "INNER JOIN dbo.Sach d ON c.MaSach = d.MaSach " +
                                   "WHERE b.TinhTrang = N'Đang Chờ'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            pm_dc.Items.Clear();

                            if (reader.HasRows)
                            {
                                int i = 1;
                                while (reader.Read())
                                {
                                    string[] row = {
                                i.ToString(),
                                reader["MaUser"].ToString(),
                                reader["HoTen"].ToString(),
                                reader["TenSach"].ToString(),
                                reader["TinhTrang"].ToString()
                            };

                                    ListViewItem listItem = new ListViewItem(row);
                                    pm_dc.Items.Add(listItem);
                                    i++;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Không có dữ liệu phù hợp.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message);
            }
        }
        private void pm_dc_SelectedIndexChanged(object sender, EventArgs e)
        {
            pm_accept.Enabled = true;
            pm_cancel.Enabled = true;
            if (pm_dc.SelectedItems.Count > 0)
            {
                // Lấy dòng được chọn từ ListView
                ListViewItem selectedRow = pm_dc.SelectedItems[0];

                // Lấy giá trị từ các cột của dòng được chọn
                string madocgia = selectedRow.SubItems[1].Text;
                string tensach = selectedRow.SubItems[3].Text; // Giả sử MaUser nằm ở cột thứ 1
                //MessageBox.Show(tensach);

                // Thực hiện truy vấn để lấy mã phiếu mượn từ bảng PhieuMuon
                conn = new SqlConnection(sql);
                conn.Open();

                // Sử dụng tham số để tránh SQL Injection
                SqlCommand cmd = new SqlCommand("SELECT b.MaPhieuMuon FROM dbo.PhieuMuon b " +
                    "INNER JOIN dbo.ChiTietPhieuMuon c ON b.MaPhieuMuon = c.MaPhieuMuon " +
                    "INNER JOIN dbo.Sach d ON c.MaSach = d.MaSach " +
                    "WHERE b.TinhTrang = N'Đang Chờ' AND d.TenSach = N'" + tensach + "' AND b.MaUser = @MaUser", conn);


                cmd.Parameters.AddWithValue("@MaUser", madocgia);

                // Thực hiện truy vấn và lấy giá trị
                try
                {
                    // Thực hiện truy vấn và lấy giá trị
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // Lưu mã phiếu mượn vào biến toàn cục
                        maPhieuMuonSelected = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã phiếu mượn cho mã user này.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi truy vấn: " + ex.Message);
                }
                conn.Close();
            }
            else
            {
                pm_accept.Enabled = false;
                pm_cancel.Enabled = false;
            }
        }


        private void pm_accept_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(sql);
                conn.Open();

                // Sử dụng câu truy vấn UPDATE để cập nhật dữ liệu
                string query = "UPDATE dbo.PhieuMuon SET TinhTrang = @TinhTrang WHERE MaPhieuMuon = @MaPhieuMuon";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm tham số vào câu truy vấn để tránh tình trạng SQL Injection
                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuonSelected);
                cmd.Parameters.AddWithValue("@TinhTrang", "Chấp nhận");

                // Thực hiện câu truy vấn UPDATE
                int rowsAffected = cmd.ExecuteNonQuery();
                phieumuon();
                PhieuCho();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối sau khi sử dụng
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void pm_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(sql);
                conn.Open();

                // Sử dụng câu truy vấn UPDATE để cập nhật dữ liệu
                string query = "UPDATE dbo.PhieuMuon SET TinhTrang = @TinhTrang, LDTC = N'" + pm_ldtc.Text + "' WHERE MaPhieuMuon = @MaPhieuMuon";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Thêm tham số vào câu truy vấn để tránh tình trạng SQL Injection
                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuonSelected);
                cmd.Parameters.AddWithValue("@TinhTrang", "Từ Chối");

                // Thực hiện câu truy vấn UPDATE
                int rowsAffected = cmd.ExecuteNonQuery();
                phieumuon();
                PhieuCho();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối sau khi sử dụng
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void pm_ldtc_TextChanged(object sender, EventArgs e)
        {
            if (pm_ldtc.Text == null)
                pm_accept.Enabled = true;
            else
                pm_accept.Enabled = false;
        }
        //Tg va Nxb
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            t_p_tg.Visible = false;
            pn_tg.Visible = false;
            pn_thamgia2.Visible = false;
            pn_thamgia1.Visible = false;
            if (nxb.Checked)
            {
                LoadDataToListView();
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            t_p_nxb.Visible = true;
            t_p_tg.Visible = false;
            pn_nxb.Visible = true;
            pn_tg.Visible = false;
            pn_thamgia2.Visible = false;
            pn_thamgia1.Visible = false;
            LoadDataToListView();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            t_p_tg.Visible = true;
            t_p_nxb.Visible = false;
            pn_nxb.Visible = false;
            pn_tg.Visible = true;
            pn_thamgia2.Visible = false;
            pn_thamgia1.Visible = false;
            LoadDataTacgia();
        }
        private void thamgia_CheckedChanged(object sender, EventArgs e)
        {
            t_p_tg.Visible = false;
            t_p_nxb.Visible = false;
            pn_nxb.Visible = false;
            pn_tg.Visible = false;
            pn_thamgia2.Visible = true;
            pn_thamgia1.Visible = true;
            LoadDataThamGia();
        }
        private void txb_hienthi_Click(object sender, EventArgs e)
        {
            if (nxb.Checked)
            {
                LoadDataToListView();
            }
            else if (tg.Checked)
            {
                LoadDataTacgia();
                textcleartg();
            }    
            else if (thamgia.Checked)
            {
                LoadDataThamGia();
                cleartestthamgia();
            }    
        }
        private void txb_them_Click(object sender, EventArgs e)
        {
            if (nxb.Checked)
            {
                ThemNhaXuatBan(nxb_manxb.Text, nxb_nxb.Text, nxb_dc.Text, nxb_dt.Text);
            }
            else if (tg.Checked)
            {
                ThemTacGia(tg_mtg.Text, tg_tentg.Text, tg_dc.Text, tg_tieusu.Text, tg_sdt.Text);
                loadtablesach();
                textcleartg();
            }
            else if (thamgia.Checked)
                ThemDuLieuVaoBangThamGia(thamgia_ms.Text, test, thamgia_vaitro.Text, thamgia_vt.Text);
        }

        private void txb_xoa_Click(object sender, EventArgs e)
        {
            if (nxb.Checked)
            {
                XoaNhaXuatBan(nxb_manxb.Text);
                LoadDataToListView();
            }
            else if (tg.Checked)
            {
                XoaTacGia(tg_mtg.Text);
                LoadDataTacgia();
                textcleartg();
            }
            else if (thamgia.Checked)
                Xoathamgia(thamgia_ms.Text, thamgia_mtg.Text);
        }

        private void txb_sua_Click(object sender, EventArgs e)
        {
            if (nxb.Checked)
            {
                SuaNhaXuatBan(nxb_manxb.Text, nxb_nxb.Text, nxb_dc.Text, nxb_dt.Text);
                LoadDataToListView();
            }
            else if (tg.Checked)
            {
                SuaTacGia(tg_mtg.Text, tg_tentg.Text, tg_dc.Text, tg_tieusu.Text, tg_sdt.Text);
                LoadDataTacgia();
                textcleartg();
            }
            else if (thamgia.Checked)
                Suathamgia(thamgia_ms.Text, thamgia_mtg.Text, thamgia_vaitro.Text, thamgia_vt.Text);
        }
        private void bt_nxb_tg_tk_Click(object sender, EventArgs e)
        {
            if (nxb.Checked)
            {
                tknxb(tg_nxb_tk.Text);
            }
            else if (tg.Checked)
            {
                TimKiemTacGia(tg_nxb_tk.Text);
            }
            else if (thamgia.Checked)
                TimKiemThamGia(tg_nxb_tk.Text);
        }
        //NXB
        private void LoadDataToListView()
        {
            lv_nxb.Items.Clear(); // Xóa các mục cũ trước khi thêm mới
            txb_them.Enabled = true;
            txb_xoa.Enabled = false;
            txb_sua.Enabled = false;
            textclearnxb();
            using (SqlConnection conn = new SqlConnection(sql))
            {
                conn.Open();
                lv_nxb.Items.Clear();
                string query = "SELECT * FROM dbo.NhaXuatBan";
                int i = 1;
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            lv_nxb.Items.Add(a1);
                            i++;
                        }
                        conn.Close();
                    }
                }
            }
        }
        private void ThemNhaXuatBan(string maNXB, string tenNXB, string diaChi, string dienThoai)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(sql))
                {
                    conn.Open();

                    string query = "INSERT INTO dbo.NhaXuatBan (MaNXB, TenNXB, DiaChi, DienThoai) " +
                                   "VALUES (@maNXB, N'" + tenNXB + "', N'" + diaChi + "', @dienThoai)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@maNXB", maNXB);
                        command.Parameters.AddWithValue("@dienThoai", dienThoai);

                        command.ExecuteNonQuery();
                    }
                }
                LoadDataToListView();
                textclearnxb();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ: hiển thị thông báo lỗi
                MessageBox.Show("Lỗi trùng mã nhà xuất bảng");
            }
        }

        private void lv_nxb_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_them.Enabled = false;
            txb_xoa.Enabled = true;
            txb_sua.Enabled = true;
            if (lv_nxb.SelectedItems.Count > 0)
            {
                // Lấy dòng được chọn
                ListViewItem selectedItem = lv_nxb.SelectedItems[0];

                // Lấy giá trị từ các cột của dòng được chọn
                string maNXB = selectedItem.SubItems[1].Text;
                string tenNXB = selectedItem.SubItems[2].Text;
                string diaChi = selectedItem.SubItems[3].Text;
                string dienThoai = selectedItem.SubItems[4].Text;

                // Đặt giá trị vào các TextBox
                nxb_manxb.Text = maNXB;
                nxb_nxb.Text = tenNXB;
                nxb_dc.Text = diaChi;
                nxb_dt.Text = dienThoai;
            }
        }
        public void textclearnxb()
        {
            nxb_manxb.Text = string.Empty;
            nxb_nxb.Text = string.Empty;
            nxb_dc.Text = string.Empty;
            nxb_dt.Text = string.Empty;
        }
        private void XoaNhaXuatBan(string maNXB)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                // Kiểm tra xem có sách nào thuộc NXB này không
                string queryCheckSach = "SELECT COUNT(*) FROM dbo.Sach WHERE MaNXB = @maNXB";
                using (SqlCommand commandCheckSach = new SqlCommand(queryCheckSach, conn))
                {
                    commandCheckSach.Parameters.AddWithValue("@maNXB", maNXB);
                    int count = (int)commandCheckSach.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Không thể xóa Nhà Xuất Bản này vì có sách thuộc NXB này.");
                        return;
                    }
                }

                // Xóa Nhà Xuất Bản
                string queryXoaNXB = "DELETE FROM dbo.NhaXuatBan WHERE MaNXB = @maNXB";
                using (SqlCommand commandXoaNXB = new SqlCommand(queryXoaNXB, conn))
                {
                    commandXoaNXB.Parameters.AddWithValue("@maNXB", maNXB);
                    commandXoaNXB.ExecuteNonQuery();
                }

                MessageBox.Show("Đã xóa Nhà Xuất Bản.");
                conn.Close();
            }
        }
        private void SuaNhaXuatBan(string maNXB, string tenNXB, string diaChi, string dienThoai)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                /*// Kiểm tra xem có sách nào thuộc NXB này không (nếu có thay đổi tên NXB)
                string queryCheckSach = "SELECT COUNT(*) FROM dbo.Sach WHERE MaNXB = @maNXB";
                using (SqlCommand commandCheckSach = new SqlCommand(queryCheckSach, conn))
                {
                    commandCheckSach.Parameters.AddWithValue("@maNXB", maNXB);
                    int count = (int)commandCheckSach.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Không thể thay đổi tên Nhà Xuất Bản này vì có sách thuộc NXB này.");
                        return;
                    }
                }*/

                // Sửa thông tin Nhà Xuất Bản
                string querySuaNXB = "UPDATE dbo.NhaXuatBan SET TenNXB = N'" + tenNXB + "', DiaChi = N'" + diaChi + "', DienThoai = @dienThoai WHERE MaNXB = @maNXB";
                using (SqlCommand commandSuaNXB = new SqlCommand(querySuaNXB, conn))
                {
                    commandSuaNXB.Parameters.AddWithValue("@maNXB", maNXB);
                    commandSuaNXB.Parameters.AddWithValue("@dienThoai", dienThoai);

                    commandSuaNXB.ExecuteNonQuery();
                }

                MessageBox.Show("Đã cập nhật thông tin Nhà Xuất Bản.");
                conn.Close();
            }
        }
        public void tknxb(string maNXB)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string query = "select * from NhaXuatBan WHERE MaNXB = @maNXB";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@maNXB", maNXB);
                    lv_nxb.Items.Clear();
                    int i = 1;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            lv_nxb.Items.Add(a1);
                            i++;
                        }
                        conn.Close();
                    }
                }
                conn.Close();
            }
        }
        //TG
        private void LoadDataTacgia()
        {
            lv_tg.Items.Clear(); // Xóa các mục cũ trước khi thêm mới
            txb_them.Enabled = true;
            txb_xoa.Enabled = false;
            txb_sua.Enabled = false;
            textclearnxb();
            using (SqlConnection conn = new SqlConnection(sql))
            {
                conn.Open();
                lv_nxb.Items.Clear();
                string query = "SELECT * FROM dbo.TacGia";
                int i = 1;
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            lv_tg.Items.Add(a1);
                            i++;
                        }
                        conn.Close();
                    }
                }
            }

        }
        private void SuaTacGia(string maTacGia, string tenTacGia, string diaChi, string tieuSu, string dienThoai)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                // Kiểm tra xem Mã Tác Giả đã tồn tại chưa (trong trường hợp sửa)
                string queryCheckExists = "SELECT COUNT(*) FROM dbo.TacGia WHERE MaTacGia = @maTacGia";
                using (SqlCommand commandCheckExists = new SqlCommand(queryCheckExists, conn))
                {
                    commandCheckExists.Parameters.AddWithValue("@maTacGia", maTacGia);
                    int count = (int)commandCheckExists.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Mã Tác Giả không tồn tại. Vui lòng kiểm tra lại.");
                        return;
                    }
                }

                // Sửa thông tin Tác Giả
                string querySuaTacGia = "UPDATE dbo.TacGia SET TenTacGia = N'" + tenTacGia + "', DiaChi = N'" + diaChi + "', TieuSu = N'" + tieuSu + "'," +
                    " DienThoai = @dienThoai WHERE MaTacGia = @maTacGia";
                using (SqlCommand commandSuaTacGia = new SqlCommand(querySuaTacGia, conn))
                {
                    commandSuaTacGia.Parameters.AddWithValue("@maTacGia", maTacGia);
                    commandSuaTacGia.Parameters.AddWithValue("@dienThoai", dienThoai);

                    commandSuaTacGia.ExecuteNonQuery();
                    MessageBox.Show("Sửa thông tin Tác Giả thành công.");
                }
                conn.Close();
            }
        }
        private void ThemTacGia(string maTacGia, string tenTacGia, string diaChi, string tieuSu, string dienThoai)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                // Kiểm tra xem Mã Tác Giả đã tồn tại chưa
                string queryCheckExists = "SELECT COUNT(*) FROM dbo.TacGia WHERE MaTacGia = @maTacGia";
                using (SqlCommand commandCheckExists = new SqlCommand(queryCheckExists, conn))
                {
                    commandCheckExists.Parameters.AddWithValue("@maTacGia", maTacGia);
                    int count = (int)commandCheckExists.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Mã Tác Giả đã tồn tại. Vui lòng chọn mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Thêm Tác Giả mới
                string queryThemTacGia = "INSERT INTO dbo.TacGia (MaTacGia, TenTacGia, DiaChi, TieuSu, DienThoai) " +
                    "VALUES (@maTacGia, N'" + tenTacGia + "', N'" + diaChi + "', N'" + tieuSu + "', @dienThoai)";
                using (SqlCommand commandThemTacGia = new SqlCommand(queryThemTacGia, conn))
                {
                    commandThemTacGia.Parameters.AddWithValue("@maTacGia", maTacGia);
                    commandThemTacGia.Parameters.AddWithValue("@dienThoai", dienThoai);

                    commandThemTacGia.ExecuteNonQuery();
                    MessageBox.Show("Thêm Tác Giả thành công.");
                }
                conn.Close();
            }
        }
        private void lv_tg_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_them.Enabled = false;
            txb_xoa.Enabled = true;
            txb_sua.Enabled = true;
            if (lv_tg.SelectedItems.Count > 0)
            {
                // Lấy dòng được chọn
                ListViewItem selectedItem = lv_tg.SelectedItems[0];

                // Lấy giá trị từ các cột của dòng được chọn
                string maNXB = selectedItem.SubItems[1].Text;
                string tenNXB = selectedItem.SubItems[2].Text;
                string diaChi = selectedItem.SubItems[3].Text;
                string tieusu = selectedItem.SubItems[4].Text;
                string dienThoai = selectedItem.SubItems[5].Text;
                // Đặt giá trị vào các TextBox
                tg_mtg.Text = maNXB;
                tg_tentg.Text = tenNXB;
                tg_dc.Text = diaChi;
                tg_sdt.Text = dienThoai;
                tg_tieusu.Text = tieusu;
            }
        }
        public void textcleartg()
        {
            tg_mtg.Text = string.Empty;
            tg_tentg.Text = string.Empty;
            tg_dc.Text = string.Empty;
            tg_sdt.Text = string.Empty;
            tg_tieusu.Text = string.Empty;
        }
        private void XoaTacGia(string maTacGia)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                // Kiểm tra xem có sách nào liên quan đến tác giả không
                string queryCheckSach = "SELECT COUNT(*) FROM dbo.ThamGia WHERE MaTacGia = @maTacGia";
                using (SqlCommand commandCheckSach = new SqlCommand(queryCheckSach, conn))
                {
                    commandCheckSach.Parameters.AddWithValue("@maTacGia", maTacGia);
                    int count = (int)commandCheckSach.ExecuteScalar();

                    if (count > 0)
                    {
                        DialogResult result = MessageBox.Show("Tác Giả có sách liên quan. Bạn có muốn xóa tất cả sách của Tác Giả này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            // Xóa các sách của Tác Giả từ bảng ThamGia
                            string queryXoaThamGia = "DELETE FROM dbo.ThamGia WHERE MaTacGia = @maTacGia";
                            using (SqlCommand commandXoaThamGia = new SqlCommand(queryXoaThamGia, conn))
                            {
                                commandXoaThamGia.Parameters.AddWithValue("@maTacGia", maTacGia);
                                commandXoaThamGia.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            return; // Người dùng không muốn xóa
                        }
                    }
                }

                // Xóa Tác Giả
                string queryXoaTacGia = "DELETE FROM dbo.TacGia WHERE MaTacGia = @maTacGia";
                using (SqlCommand commandXoaTacGia = new SqlCommand(queryXoaTacGia, conn))
                {
                    commandXoaTacGia.Parameters.AddWithValue("@maTacGia", maTacGia);
                    commandXoaTacGia.ExecuteNonQuery();
                    MessageBox.Show("Xóa Tác Giả thành công.");
                }
                conn.Close();
            }
        }
        private void TimKiemTacGia(string maTacGia)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string query = "select * from TacGia WHERE MaTacGia = @matg";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@matg", maTacGia);
                    lv_tg.Items.Clear();
                    int i = 1;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            lv_tg.Items.Add(a1);
                            i++;
                        }
                        conn.Close();
                    }
                }
                conn.Close();
            }
        }
        //Tham gia
        private void LoadMaSachToComboBox()
        {
            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(sql))
            {
                conn.Open();

                // Tạo câu truy vấn để lấy danh sách mã sách từ bảng Sach
                string query = "SELECT MaSach FROM dbo.Sach";

                // Thực hiện truy vấn
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Sử dụng SqlDataReader để đọc dữ liệu từ truy vấn
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Xóa tất cả các mục cũ trong ComboBox
                        thamgia_ms.Items.Clear();

                        // Đọc từng dòng dữ liệu và thêm vào ComboBox
                        while (reader.Read())
                        {
                            // Đọc giá trị từ cột "MaSach"
                            string maSach = reader["MaSach"].ToString();

                            // Thêm vào ComboBox
                            thamgia_ms.Items.Add(maSach);
                        }
                        conn.Close();
                    }
                }
            }
        }
        private void LoadMaTacGiaToComboBox()
        {
            // Kết nối đến cơ sở dữ liệu
            using (SqlConnection conn = new SqlConnection(sql))
            {
                conn.Open();

                // Tạo câu truy vấn để lấy danh sách mã tác giả từ bảng TacGia
                string query = "SELECT TenTacGia FROM dbo.TacGia";

                // Thực hiện truy vấn
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    // Sử dụng SqlDataReader để đọc dữ liệu từ truy vấn
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Xóa tất cả các mục cũ trong ComboBox
                        thamgia_mtg.Items.Clear();

                        // Đọc từng dòng dữ liệu và thêm vào ComboBox
                        while (reader.Read())
                        {
                            // Đọc giá trị từ cột "MaTacGia"
                            string maTacGia = reader["TenTacGia"].ToString();

                            // Thêm vào ComboBox
                            thamgia_mtg.Items.Add(maTacGia);
                        }
                        conn.Close();
                    }
                }
            }
        }
        private void LoadDataThamGia()
        {
            using (SqlConnection conn = new SqlConnection(sql))
            {
                conn.Open();

                string query = "SELECT ThamGia.MaSach, ThamGia.MaTacGia, TacGia.TenTacGia, ThamGia.ViTri, ThamGia.VaiTro " +
                               "FROM dbo.ThamGia INNER JOIN dbo.TacGia ON ThamGia.MaTacGia = TacGia.MaTacGia";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Xóa tất cả các cột và hàng cũ trong ListView
                        thamgia_lv.Items.Clear();
                        int i = 1;
                        // Đọc từng dòng dữ liệu và thêm vào ListView
                        while (reader.Read())
                        {
                            string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            thamgia_lv.Items.Add(a1);
                            i++;
                        }
                        conn.Close();
                    }
                }
                LoadMaSachToComboBox();
                LoadMaTacGiaToComboBox();
            }
        }
        private void ThemDuLieuVaoBangThamGia(string maSach, string maTacGia, string vaiTro, string viTri)
        {
            using (SqlConnection conn = new SqlConnection(sql))
            {
                conn.Open();

                // Kiểm tra xem đã tồn tại dữ liệu trùng lặp chưa
                string queryKiemTra = "SELECT COUNT(*) FROM dbo.ThamGia WHERE MaSach = @maSach AND MaTacGia = @maTacGia";
                using (SqlCommand commandKiemTra = new SqlCommand(queryKiemTra, conn))
                {
                    commandKiemTra.Parameters.AddWithValue("@maSach", maSach);
                    commandKiemTra.Parameters.AddWithValue("@maTacGia", maTacGia);

                    int count = (int)commandKiemTra.ExecuteScalar();

                    if (count > 0)
                    {
                        // Dữ liệu đã tồn tại, báo lỗi hoặc xử lý theo ý bạn
                        MessageBox.Show("Dữ liệu đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                
                // Nếu không trùng lặp, thực hiện thêm dữ liệu
                string queryThem = "INSERT INTO dbo.ThamGia (MaSach, MaTacGia, VaiTro, ViTri) VALUES (@maSach, @maTacGia, N'" + vaiTro + "', @viTri)";
                using (SqlCommand commandThem = new SqlCommand(queryThem, conn))
                {
                    commandThem.Parameters.AddWithValue("@maSach", maSach);
                    commandThem.Parameters.AddWithValue("@maTacGia", maTacGia);
                    commandThem.Parameters.AddWithValue("@viTri", viTri);

                    int rowsAffected = commandThem.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Sau khi thêm dữ liệu, có thể gọi lại hàm load dữ liệu vào ListView
                        LoadDataToListView();
                    }
                    else
                    {
                        MessageBox.Show("Thêm dữ liệu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                cleartestthamgia();
                LoadDataThamGia();
                conn.Close();
            }
        }
        public void cleartestthamgia()
        {
            thamgia_ms.Text = string.Empty;
            thamgia_mtg.Text = string.Empty;
            thamgia_vt.Text = string.Empty;
            thamgia_vaitro.Text = string.Empty;
        }
        private void thamgia_lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            txb_them.Enabled = false;
            txb_xoa.Enabled = true;
            txb_sua.Enabled = true;
            if (thamgia_lv.SelectedItems.Count > 0)
            {
                // Lấy dòng được chọn
                ListViewItem selectedItem = thamgia_lv.SelectedItems[0];

                // Lấy giá trị từ các cột của dòng được chọn
                string ms = selectedItem.SubItems[1].Text;
                string mtg = selectedItem.SubItems[2].Text;
                string vitri = selectedItem.SubItems[4].Text;
                string vaitro = selectedItem.SubItems[5].Text;


                // Đặt giá trị vào các TextBox
                thamgia_ms.Text = ms;
                thamgia_mtg.Text = mtg;
                thamgia_vt.Text = vitri;
                thamgia_vaitro.Text = vaitro;
            }
        }

        private void Xoathamgia(string ms, string mtg)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string queryKiemTra = "SELECT COUNT(*) FROM dbo.ThamGia WHERE [MaSach] = @MaSach AND [MaTacGia] = @MaTacGia";
                using (SqlCommand commandKiemTra = new SqlCommand(queryKiemTra, conn))
                {
                    commandKiemTra.Parameters.AddWithValue("@MaSach", ms);
                    commandKiemTra.Parameters.AddWithValue("@MaTacGia", mtg);

                    int rowCount = (int)commandKiemTra.ExecuteScalar();

                    if (rowCount == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để xóa.");
                        conn.Close();
                        return;
                    }
                }

                string queryXoaThamGia = "DELETE FROM dbo.ThamGia WHERE [MaSach] = @MaSach AND [MaTacGia] = @MaTacGia";
                using (SqlCommand commandXoaThamGia = new SqlCommand(queryXoaThamGia, conn))
                {
                    commandXoaThamGia.Parameters.AddWithValue("@MaSach", ms);
                    commandXoaThamGia.Parameters.AddWithValue("@MaTacGia", mtg);
                    commandXoaThamGia.ExecuteNonQuery();
                }

                MessageBox.Show("Đã xóa dữ liệu Tham Gia.");
                conn.Close();
                cleartestthamgia();
                LoadDataThamGia();
            }
        }
        private void Suathamgia(string ms, string mtg, string vaitro, string vitri)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string queryKiemTra = "SELECT COUNT(*) FROM dbo.ThamGia WHERE [MaSach] = @MaSach AND [MaTacGia] = @MaTacGia";
                using (SqlCommand commandKiemTra = new SqlCommand(queryKiemTra, conn))
                {
                    commandKiemTra.Parameters.AddWithValue("@MaSach", ms);
                    commandKiemTra.Parameters.AddWithValue("@MaTacGia", mtg);

                    int rowCount = (int)commandKiemTra.ExecuteScalar();

                    if (rowCount == 0)
                    {
                        MessageBox.Show("Không có dữ liệu để sửa.");
                        conn.Close();
                        return;
                    }
                }

                string querySuaThamGia = "UPDATE dbo.ThamGia SET [VaiTro] = @VaiTro, [ViTri] = @ViTri WHERE [MaSach] = @MaSach AND [MaTacGia] = @MaTacGia";
                using (SqlCommand commandSuaThamGia = new SqlCommand(querySuaThamGia, conn))
                {
                    commandSuaThamGia.Parameters.AddWithValue("@MaSach", ms);
                    commandSuaThamGia.Parameters.AddWithValue("@MaTacGia", mtg);
                    commandSuaThamGia.Parameters.AddWithValue("@VaiTro", vaitro);
                    commandSuaThamGia.Parameters.AddWithValue("@ViTri", vitri);

                    commandSuaThamGia.ExecuteNonQuery();
                }

                MessageBox.Show("Đã sửa dữ liệu Tham Gia.");
                conn.Close();
                cleartestthamgia();
                LoadDataThamGia();
            }
        }
        private void TimKiemThamGia(string MaTacGia)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string query = "select a.MaSach, a.MaTacGia, b.TenTacGia, a.ViTri, a.VaiTro from ThamGia a, TacGia b " +
                    "where a.MaTacGia = b.MaTacGia and a.MaTacGia = @matg";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@matg", MaTacGia);
                    thamgia_lv.Items.Clear();
                    int i = 1;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string[] cd = { i.ToString(), reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString() };
                            ListViewItem a1 = new ListViewItem(cd);
                            thamgia_lv.Items.Add(a1);
                            i++;
                        }
                        conn.Close();
                    }
                }
                conn.Close();
            }
        }
        private void SetTabControlBackground()
        {/*
            Color darkGreen = Color.SkyBlue;
            tabControl1.BackColor = darkGreen;

            // Sự kiện DrawItem để vẽ nền cho mỗi tab
            tabControl1.DrawItem += (sender, e) =>
            {
                // Lấy màu nền của TabControl
                Color backgroundColor = tabControl1.BackColor;

                // Vẽ hình chữ nhật màu nền cho tab
                using (Brush brush = new SolidBrush(backgroundColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                // Vẽ tiêu đề của tab
                TextRenderer.DrawText(e.Graphics, tabControl1.TabPages[e.Index].Text, e.Font, e.Bounds, e.ForeColor);
            };

            // Thiết lập màu nền cho từng TabPage nếu cần
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                tabPage.BackColor = darkGreen; // Màu nền của từng tab
            }*/
            Image backgroundImage = Properties.Resources.background_book_sach_nen_bau_troi_xanh_trong;

            // Sự kiện DrawItem để vẽ nền cho mỗi tab
            tabControl1.DrawItem += (sender, e) =>
            {
                // Vẽ hình nền cho tab
                e.Graphics.DrawImage(backgroundImage, e.Bounds);

                // Vẽ tiêu đề của tab
                TextRenderer.DrawText(e.Graphics, tabControl1.TabPages[e.Index].Text, e.Font, e.Bounds, e.ForeColor);
            };

            // Thay đổi kích thước của mỗi tab để phù hợp với kích thước của hình nền
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                tabPage.BackgroundImage = backgroundImage;
                tabPage.BackgroundImageLayout = ImageLayout.Stretch; // Chọn kiểu căn chỉnh hình ảnh
            }

            // Thay đổi kích thước của TabControl để phù hợp với kích thước của hình nền
            tabControl1.Size = new Size(backgroundImage.Width, backgroundImage.Height); ;
            /*p_ts.BackColor = Color.WhiteSmoke;
            p_pl3.BackColor = Color.WhiteSmoke;*/
        }
        //USER MUON SACH:
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice captureDevice;

        private void tabPage4_Enter(object sender, EventArgs e)
        {
            FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in FilterInfoCollection)
            {
                camera.Items.Add(filterInfo.Name);
            }
            camera.SelectedIndex = 0;
            loadmauser();
        }

        private void Quanlithuvien_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            captureDevice = new VideoCaptureDevice(FilterInfoCollection[camera.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();
            timer1.Start();
        }
        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            camera_pb.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (camera_pb.Image != null)
            {
                BarcodeReader barcodeReader = new BarcodeReader();
                Result result = barcodeReader.Decode((Bitmap)camera_pb.Image);
                if (result != null)
                {
                    matruyen_dg.Text = result.ToString();
                    timer1.Stop();

                    // Dừng thiết bị chụp hình một cách an toàn
                    StopCaptureDevice();
                }
            }
        }
        private void StopCaptureDevice()
        {
            if (captureDevice != null && captureDevice.IsRunning)
            {
                captureDevice.SignalToStop();
                captureDevice.WaitForStop();
            }
        }

        private void matruyen_dg_TextChanged(object sender, EventArgs e)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string query = "select s.TenSach, c.TenChuDe  from dbo.Sach s, dbo.ChuDe c " +
                    "where s.MaChuDe = c.MaChuDe and MaSach = @masach";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@masach", matruyen_dg.Text);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tentruyen_dg.Text = reader.GetString(0);
                            tl_docgia.Text = reader.GetString(1);
                        }
                        conn.Close();
                    }
                }
                conn.Close();
            }
        }
        public void loadmauser()
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();
                string query = "select MaUser from dbo.[User] where PhanLoai = 'user'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                mauser.Items.Clear();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string cd = reader.GetString(0);
                        mauser.Items.Add(cd);
                    }
                }
                else
                {
                    reader.Close();
                    conn.Close();
                    MessageBox.Show("False");
                }
                conn.Close();
            }
        }

        private void tabPage4_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (captureDevice.IsRunning)
            {
                captureDevice.Stop();
            }
        }
        private void dgmt_Click(object sender, EventArgs e)
        {
            string chuoiketnoi = Properties.Settings.Default.sql;
            string maphieumuon = SinhMaPhieuMuon(chuoiketnoi);
            ThemDuLieuPhieuMuon(chuoiketnoi, maphieumuon, mauser.Text, matruyen_dg.Text);
        }
        static string SinhMaPhieuMuon(string connectionString)
        {
            string newMaPhieuMuon = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tạo đối tượng SqlCommand để thực hiện truy vấn
                    using (SqlCommand command = new SqlCommand("SELECT TOP 1 MaPhieuMuon FROM PhieuMuon ORDER BY MaPhieuMuon DESC", connection))
                    {
                        // Sử dụng ExecuteScalar để lấy mã phiếu mượn cuối cùng trong cơ sở dữ liệu
                        object lastMaPhieuMuon = command.ExecuteScalar();

                        if (lastMaPhieuMuon != null)
                        {
                            // Nếu có mã phiếu mượn cuối cùng, tạo mã mới dựa trên nó
                            string lastNumberPart = lastMaPhieuMuon.ToString().Substring(2);
                            int nextNumber = int.Parse(lastNumberPart) + 1;
                            newMaPhieuMuon = "PM" + nextNumber.ToString("D2");
                        }
                        else
                        {
                            // Nếu không có mã phiếu mượn trong cơ sở dữ liệu, tạo mã đầu tiên
                            newMaPhieuMuon = "PM01";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return newMaPhieuMuon;
        }
        static void ThemDuLieuPhieuMuon(string connectionString, string maPhieuMuon, string MaUser, string MaSach)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Bắt đầu một giao dịch
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Thêm dữ liệu vào bảng PhieuMuon
                            using (SqlCommand commandPhieuMuon = new SqlCommand("INSERT INTO PhieuMuon (MaPhieuMuon, NgayMuon, PhiPhat, TinhTrang, MaUser) " +
                                "VALUES (@MaPhieuMuon, @NgayMuon, 1, N'Chấp Nhận', @MaUser)", connection, transaction))
                            {
                                commandPhieuMuon.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                                commandPhieuMuon.Parameters.AddWithValue("@NgayMuon", DateTime.Now.Date);
                                commandPhieuMuon.Parameters.AddWithValue("@MaUser", MaUser);
                                commandPhieuMuon.ExecuteNonQuery();
                            }


                            // Thêm dữ liệu vào bảng ChiTietPhieuMuon
                            using (SqlCommand commandChiTietPhieuMuon = new SqlCommand("INSERT INTO ChiTietPhieuMuon (MaPhieuMuon, MaSach, SoLuong, DonGia) " +
                                "VALUES (@MaPhieuMuon, @MaSach, @SoLuong, @DonGia)", connection, transaction))
                            {
                                // Thay đổi thông tin dữ liệu dựa trên yêu cầu của bạn
                                commandChiTietPhieuMuon.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                                commandChiTietPhieuMuon.Parameters.AddWithValue("@MaSach", MaSach);
                                commandChiTietPhieuMuon.Parameters.AddWithValue("@SoLuong", 1);
                                commandChiTietPhieuMuon.Parameters.AddWithValue("@DonGia", 50000);
                                commandChiTietPhieuMuon.ExecuteNonQuery();
                            }


                            // Hoàn thành giao dịch
                            transaction.Commit();
                            MessageBox.Show("Mượn Truyện Thành Công !!!");
                        }
                        catch (Exception ex)
                        {
                            // Nếu có lỗi, hủy bỏ giao dịch
                            transaction.Rollback();
                            Console.WriteLine("Lỗi: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                Console.WriteLine("Lỗi: " + ex.Message);
            }
        }
        string test;
        private void thamgia_mtg_TextChanged(object sender, EventArgs e)
        {
            test = xuatma(thamgia_mtg.Text);
        }
        public string xuatma(string tentacgia)
        {
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string query = "select MaTacGia from TacGia where TenTacGia = N'"+ tentacgia + "'";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                        conn.Close();
                    }
                }
                conn.Close();
            }
            return "a";

        }
    }
}
