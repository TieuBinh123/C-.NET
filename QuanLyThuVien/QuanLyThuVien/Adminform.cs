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
using QuanLyThuVien.ChoPhi;

namespace QuanLyThuVien
{
    public partial class Adminform : Form
    {
        public Adminform()
        {
            InitializeComponent();
        }


        private void quảnLýĐộcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlithuvien qltv = new Quanlithuvien();
            qltv.ShowDialog();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qldocgia qldg = new qldocgia();
            qldg.ShowDialog();
        }

        private void Adminform_Load(object sender, EventArgs e)
        {
            about.Visible = false;
            thongke.Visible = false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about.Visible = true;
            pictureBox1.Visible = false;
            thongke.Visible = false;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            about.Visible = false;
            pictureBox1.Visible = true;
            thongke.Visible = false;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            about.Visible = false;
            pictureBox1.Visible = false;
            thongke.Visible = true;
            DienDuLieuVaoBieuDo();
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DangNhap f = new DangNhap();
            f.Show();
            this.Close();

        }
        private void DienDuLieuVaoBieuDo()
        {
            try
            {
                chart1.ChartAreas["ChartArea1"].AxisX.Title = "Mã sách";
                chart1.ChartAreas["ChartArea1"].AxisY.Title = "Số lượng";
                chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("Arial", 8);
                chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                SqlConnection conn = new SqlConnection(Properties.Settings.Default.sql);
                string query = "SELECT s.MaSach, ISNULL(SUM(ctpm.SoLuong), 0) AS SoLuong " +
                            "FROM Sach s " +
                            "LEFT JOIN ChiTietPhieuMuon ctpm ON s.MaSach = ctpm.MaSach " +
                            "LEFT JOIN PhieuMuon pm ON ctpm.MaPhieuMuon = pm.MaPhieuMuon AND pm.TinhTrang = N'Chấp Nhận' " +
                            "WHERE(pm.NgayMuon >= '2023-01-01' AND pm.NgayMuon <= '2023-03-31') OR pm.MaPhieuMuon IS NULL " +
                            "GROUP BY s.MaSach;";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open(); // Mở kết nối trước khi thực hiện truy vấn
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chart1.Series["Sách"].Points.AddXY(reader[0].ToString(), reader[1].ToString());
                        }
                    }
                    conn.Close(); // Đóng kết nối sau khi đọc xong dữ liệu
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }
}
