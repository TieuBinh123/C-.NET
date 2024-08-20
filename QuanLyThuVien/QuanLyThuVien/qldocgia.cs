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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;
using System.Collections;
using QuanLyThuVien.ublites;

namespace QuanLyThuVien
{
    public partial class qldocgia : Form
    {
        private DataTable dataTable;
        public qldocgia()
        {
            InitializeComponent();
            SetTabControlBackground();
        }
        string sql = Properties.Settings.Default.sql;
        private SqlConnection conn = new SqlConnection();
        private void ql_tk_Enter(object sender, EventArgs e)
        {
            ql_tk.Text = "";
            ql_tk.ForeColor = System.Drawing.Color.Black;
        }

        private void ql_tk_Leave(object sender, EventArgs e)
        {
            if (ql_tk.Text == "")
            {
                ql_tk.Text = "Tìm kiếm dựa trên mã người dùng";
                ql_tk.ForeColor = Color.DimGray;
            }
        }
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            InitializeDataGridView();
            ttdocgia_sua.Enabled = false;
            ttdocgia_them.Enabled = true;
            ttdocgia_xoa.Enabled = false;
            btdocgia_dmk.Enabled = false;
        }
        private void InitializeDataGridView()
        {
            conn = new SqlConnection(sql);
            conn.Open();
            /*dataGridView1.AutoGenerateColumns = false;
            dataTable = new DataTable();
            string query = "SELECT MaUser, HoTen, NgaySinh, GioiTinh, DienThoai, TaiKhoan, Email, DiaChi FROM dbo.[User] WHERE PhanLoai = 'user'";
            //Chỉ hiển thị các cột cần thiết trong DataGridView
            dataGridView1.Columns.Add("MaUser", "Mã User");
            dataGridView1.Columns.Add("HoTen", "Họ Tên");
            dataGridView1.Columns.Add("NgaySinh", "Ngày Sinh");
            dataGridView1.Columns.Add("GioiTinh", "Giới Tính");
            dataGridView1.Columns.Add("DienThoai", "Điện Thoại");
            dataGridView1.Columns.Add("TaiKhoan", "Tài Khoản");
            dataGridView1.Columns.Add("Email", "Email");
            dataGridView1.Columns.Add("DiaChi", "Địa Chỉ");

            try
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter1 = new SqlDataAdapter(command);
                adapter1.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            connection.Close();*/
            string query = "SELECT MaUser, HoTen, NgaySinh, GioiTinh, DienThoai, TaiKhoan, Email, DiaChi FROM dbo.[User] WHERE PhanLoai = 'user'";

            SqlCommand command = new SqlCommand(query,conn);
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            conn.Close();
            dataGridView1.DataSource = data;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ttdocgia_them.Enabled = false;
            ttdocgia_xoa.Enabled = true;
            ttdocgia_sua.Enabled = true;
            btdocgia_dmk.Enabled = true;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy dòng được chọn
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Lấy giá trị từ các ô trong dòng được chọn
                string taiKhoan = selectedRow.Cells["TaiKhoan"].Value.ToString();
                string mauser = selectedRow.Cells["MaUser"].Value.ToString();
                string hoTen = selectedRow.Cells["HoTen"].Value.ToString();
                string ngaySinh = selectedRow.Cells["NgaySinh"].Value.ToString();
                string gioiTinh = selectedRow.Cells["GioiTinh"].Value.ToString();
                string dienThoai = selectedRow.Cells["DienThoai"].Value.ToString();
                string email = selectedRow.Cells["Email"].Value.ToString();
                string diaChi = selectedRow.Cells["DiaChi"].Value.ToString();

                // Hiển thị giá trị trong các TextBox
                ttdocgia_tk.Text = taiKhoan;
                ttdocgia_hoten.Text = hoTen;
                ttdocgia_ns.Text = ngaySinh;
                ttdocgia_gt.Text = gioiTinh;
                ttdocgia_dt.Text = dienThoai;
                ttdocgia_email.Text = email;
                ttdocgia_diachi.Text = diaChi;
                ttdocgia_us.Text = mauser;
                ttdocgia_mk.Text = mk(mauser);
            }
        }
        public string mk(string mauser)
        {
            try
            {
                using (conn = new SqlConnection(sql))
                {
                    conn.Open();

                    string query = "SELECT MatKhau FROM dbo.[User] WHERE MaUser = @mauser";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@mauser", mauser);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read() && reader[0] != DBNull.Value)
                            {
                                string matkhau = reader[0].ToString();
                                return matkhau;
                            }
                            else
                            {
                                return "a";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sách vào CSDL: " + ex.Message);
                return "a";
            }
        }

        private void ttdocgia_hienthi_Click(object sender, EventArgs e)
        {
            InitializeDataGridView();
            textclear();
            ttdocgia_them.Enabled = true;
            ttdocgia_xoa.Enabled = false;
            ttdocgia_sua.Enabled = false;
            btdocgia_dmk.Enabled = false;
        }
        private void ttdocgia_timkiem_Click(object sender, EventArgs e)
        {
            try
            {
                using (conn = new SqlConnection(sql))
                {
                    conn.Open();

                    string query = "SELECT MaUser, HoTen, NgaySinh, GioiTinh, DienThoai, TaiKhoan, Email, DiaChi FROM dbo.[User] " +
                                   "WHERE PhanLoai = 'user' AND MaUser = @Mauser";

                    // Khai báo và thiết lập tham số
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Mauser", ql_tk.Text);

                    // Sử dụng SqlDataReader để đọc dữ liệu
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            DataTable data = new DataTable();
                            data.Load(reader); // Load dữ liệu từ SqlDataReader vào DataTable
                            dataGridView1.DataSource = data;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy độc giả có mã số: " + ql_tk.Text);
                        }
                    }

                    // Đóng kết nối
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        public void textclear()
        {
            ttdocgia_tk.Text = string.Empty;
            ttdocgia_hoten.Text = string.Empty;
            ttdocgia_ns.Text = string.Empty;
            ttdocgia_gt.Text = string.Empty;
            ttdocgia_dt.Text = string.Empty;
            ttdocgia_email.Text = string.Empty;
            ttdocgia_diachi.Text = string.Empty;
            ttdocgia_us.Text = string.Empty;
            ttdocgia_mk.Text = string.Empty;
        }

        private void bt_on_Click(object sender, EventArgs e)
        {
            bt_on.Visible = false;
            bt_off.Visible = true;
            ttdocgia_mk.PasswordChar = '\0';
        }

        private void bt_off_Click(object sender, EventArgs e)
        {
            bt_off.Visible = false;
            bt_on.Visible = true;
            ttdocgia_mk.PasswordChar = '*';
        }
        private bool isMouseOver = false;
        private void bt_off_MouseEnter(object sender, EventArgs e)
        {
            bt_off.BackColor = Color.Gainsboro;
        }

        private void bt_off_MouseLeave(object sender, EventArgs e)
        {
            bt_off.BackColor = Color.Transparent;
        }

        private void bt_on_MouseEnter(object sender, EventArgs e)
        {
            bt_on.BackColor = Color.Gainsboro;
        }

        private void bt_on_MouseLeave(object sender, EventArgs e)
        {
            bt_on.BackColor = Color.Transparent;
        }

        private void ttdocgia_sua_Click(object sender, EventArgs e)
        {
            try
            {
                using (conn = new SqlConnection(sql))
                {
                    conn.Open();

                    string query = "UPDATE dbo.[User] SET HoTen = N'" + ttdocgia_hoten.Text + "', NgaySinh = @ngaySinh, GioiTinh = N'"+ ttdocgia_gt.Text + "'" +
                        ", DienThoai = @dienThoai, TaiKhoan = @taiKhoan, Email = @email, DiaChi = N'"+ ttdocgia_diachi.Text + "'" +
                                   "WHERE MaUser = @maUser and PhanLoai = 'user'";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@maUser", ttdocgia_us.Text);
                        command.Parameters.AddWithValue("@ngaySinh", ttdocgia_ns.Text);
                        command.Parameters.AddWithValue("@dienThoai", ttdocgia_dt.Text);
                        command.Parameters.AddWithValue("@taiKhoan", ttdocgia_tk.Text);
                        command.Parameters.AddWithValue("@email", ttdocgia_email.Text);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thông tin người dùng đã được cập nhật thành công.");
                            InitializeDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Không có dữ liệu nào được cập nhật.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật thông tin người dùng: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void btdocgia_dmk_Click(object sender, EventArgs e)
        {
            // Lấy mật khẩu cũ từ TextBox
            string matKhauCu = ttdocgia_mk.Text;

            // Kiểm tra mật khẩu cũ có đúng hay không
            // Nhập mật khẩu mới từ MessageBox
            string matKhauMoi = ShowInputDialog("Đổi mật khẩu", "Nhập mật khẩu mới: ");

            // Kiểm tra xem người dùng đã nhập mật khẩu mới hay chưa
            if (!string.IsNullOrEmpty(matKhauMoi))
            {
                // Thực hiện cập nhật mật khẩu mới vào CSDL
                bool a = CapNhatMatKhau(ttdocgia_us.Text, password.Create_MD5(matKhauMoi));

                if(a)
                    MessageBox.Show("Đổi mật khẩu thành công!");
            }
            else
            {
                MessageBox.Show("Không thực hiện đổi mật khẩu do bạn chưa nhập mật khẩu mới.");
            }
        }
        private bool CapNhatMatKhau(string Mauser,string matKhauMoi)
        {
            bool a = false;
            // Thực hiện cập nhật mật khẩu mới vào CSDL
            try
            {
                using (conn = new SqlConnection(sql))
                {
                    conn.Open();

                    string query = "UPDATE dbo.[User] SET MatKhau = @matKhauMoi WHERE MaUser = @maUser";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@maUser", Mauser);
                        command.Parameters.AddWithValue("@matKhauMoi", matKhauMoi);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật mật khẩu thành công.");
                            a = true;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy mã user hoặc có lỗi xảy ra khi cập nhật mật khẩu.");
                            a = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            return a;
        }
        private string ShowInputDialog(string title, string prompt)
        {
            Form promptForm = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label label = new Label() { Left = 50, Top = 20, Text = prompt };
            System.Windows.Forms.TextBox inputTextBox = new System.Windows.Forms.TextBox();
            inputTextBox.Left = 50;
            inputTextBox.Top = 50;
            inputTextBox.Width = 400;

            System.Windows.Forms.Button confirmationButton = new System.Windows.Forms.Button();
            confirmationButton.Text = "OK";
            confirmationButton.Left = 350;
            confirmationButton.Width = 100;
            confirmationButton.Top = 70;
            confirmationButton.DialogResult = DialogResult.OK;
            confirmationButton.Click += (sender, e) => { promptForm.Close(); };
            promptForm.Controls.Add(inputTextBox);
            promptForm.Controls.Add(confirmationButton);
            promptForm.Controls.Add(label);
            promptForm.AcceptButton = confirmationButton;

            return promptForm.ShowDialog() == DialogResult.OK ? inputTextBox.Text : "";
        }

        private void ttdocgia_xoa_Click(object sender, EventArgs e)
        {
            string maUser = ttdocgia_us.Text;
            try
            {
                // Kiểm tra xem user có liên quan đến Phiếu Mượn hay không
                bool hasPhieuMuon = KiemTraUserCoPhieuMuon(maUser);

                if (hasPhieuMuon)
                {
                    DialogResult result = MessageBox.Show("User này đang có Phiếu Mượn. Bạn có chắc chắn muốn xóa không?",
                                                          "Xác nhận xóa",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        // Xóa user và các liên kết có thể cần thiết
                        XoaUserVaPhieuMuon(maUser);

                        MessageBox.Show("Xóa thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Hủy xóa.");
                    }
                }
                else
                {
                    // User không liên quan đến Phiếu Mượn, xóa ngay lập tức
                    XoaUser(maUser);
                    MessageBox.Show("Xóa thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            InitializeDataGridView();
            textclear();
        }
        private bool KiemTraUserCoPhieuMuon(string maUser)
        {
            // Kiểm tra xem user có Phiếu Mượn hay không
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM dbo.PhieuMuon WHERE MaUser = @maUser";
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@maUser", maUser);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        private void XoaUserVaPhieuMuon(string maUser)
        {
            // Xóa user và các liên kết có thể cần thiết (cần triển khai thêm logic xóa các liên kết khác)
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                string queryXoactPhieuMuon = "DELETE FROM dbo.ChiTietPhieuMuon" +
                    " WHERE MaPhieuMuon IN (SELECT MaPhieuMuon FROM dbo.PhieuMuon WHERE MaUser = @maUser)";
                using (SqlCommand commandXoaPhieuMuon = new SqlCommand(queryXoactPhieuMuon, conn))
                {
                    commandXoaPhieuMuon.Parameters.AddWithValue("@maUser", maUser);
                    commandXoaPhieuMuon.ExecuteNonQuery();
                }
                string queryXoaPhieuMuon = "DELETE FROM dbo.PhieuMuon WHERE MaUser = @maUser";
                using (SqlCommand commandXoaPhieuMuon = new SqlCommand(queryXoaPhieuMuon, conn))
                {
                    commandXoaPhieuMuon.Parameters.AddWithValue("@maUser", maUser);
                    commandXoaPhieuMuon.ExecuteNonQuery();
                }
                // Xóa user
                string queryXoaUser = "DELETE FROM dbo.[User] WHERE MaUser = @maUser";
                using (SqlCommand commandXoaUser = new SqlCommand(queryXoaUser, conn))
                {
                    commandXoaUser.Parameters.AddWithValue("@maUser", maUser);
                    commandXoaUser.ExecuteNonQuery();
                }
                
                conn.Close();
            }
        }
        private void XoaUser(string maUser)
        {
            // Xóa user và các liên kết có thể cần thiết (cần triển khai thêm logic xóa các liên kết khác)
            using (conn = new SqlConnection(sql))
            {
                conn.Open();

                // Xóa user
                string queryXoaUser = "DELETE FROM dbo.[User] WHERE MaUser = @maUser";
                using (SqlCommand commandXoaUser = new SqlCommand(queryXoaUser, conn))
                {
                    commandXoaUser.Parameters.AddWithValue("@maUser", maUser);
                    commandXoaUser.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        private void SetTabControlBackground()
        {
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
            /*Image backgroundImage = Properties.Resources.udql;

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
            tabControl1.Size = new Size(backgroundImage.Width, backgroundImage.Height);;*/
            /*p_ts.BackColor = Color.White;
            p_pl3.BackColor = Color.White;*/
        }
    }
}
