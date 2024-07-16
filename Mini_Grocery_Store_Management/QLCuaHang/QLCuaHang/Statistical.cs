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
using DGVPrinterHelper;
namespace QLCuaHang
{
    public partial class Statistical : Form
    {
        private string connectionString = "Data Source=GIALONGCHUAI\\SUCOBATNGO;Initial Catalog=QL_CuaHang;Integrated Security=True";
        DGVPrinter printer = new DGVPrinter();
        public Statistical()
        {
            InitializeComponent();
            LoadData();
            LoadUsernames(); // Tải danh sách tên đăng nhập vào ComboBox
        }
        private void LoadData()
        {
            // Tính năng load dữ liệu vào DataGridView khi form được khởi tạo
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM SanPham";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void LoadUsernames()
        {
            // Tải danh sách tên đăng nhập vào ComboBox
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT DISTINCT TenDangNhap FROM NguoiDung";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Gán danh sách tên đăng nhập cho ComboBox
                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxTenDangNhap.Items.Add(row["TenDangNhap"].ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             // Xử lý sự kiện khi người dùng nhấn nút "Thống kê"
            string query = "SELECT sp.TenSanPham, chd.SoLuong, chd.GiaBan, hd.NgayBan, nd.HoTen AS NguoiBan " +
                           "FROM ChiTietHoaDon chd " +
                           "JOIN SanPham sp ON chd.MaSanPham = sp.MaSanPham " +
                           "JOIN HoaDon hd ON chd.MaHoaDon = hd.MaHoaDon " +
                           "JOIN NguoiDung nd ON hd.MaNguoiDung = nd.MaNguoiDung " +
                           "WHERE 1 = 1"; // Điều kiện mặc định

            try
            {
                if (!string.IsNullOrEmpty(txtThang.Text))
                {
                    int thang;
                    if (int.TryParse(txtThang.Text, out thang))
                    {
                        if (thang < 1 || thang > 12)
                        {
                            throw new ArgumentException("Tháng phải nằm trong khoảng từ 1 đến 12.");
                        }
                        query += String.Format(" AND MONTH(hd.NgayBan) = {0}", thang);
                    }
                    else
                    {
                        throw new ArgumentException("Vui lòng nhập một giá trị tháng hợp lệ.");
                    }
                }

                if (!string.IsNullOrEmpty(txtNam.Text))
                {
                    int nam;
                    if (int.TryParse(txtNam.Text, out nam))
                    {
                        query += String.Format(" AND YEAR(hd.NgayBan) = {0}", nam);
                    }
                    else
                    {
                        throw new ArgumentException("Vui lòng nhập một giá trị năm hợp lệ.");
                    }
                }

                if (comboBoxTenDangNhap.SelectedIndex != -1)
                {
                    string tenDangNhap = comboBoxTenDangNhap.SelectedItem.ToString();
                    query += String.Format(" AND nd.TenDangNhap = '{0}'", tenDangNhap);
                }


                // Thực hiện truy vấn và hiển thị kết quả trên DataGridView
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Lỗi: {0}", ex.Message), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        // Xử lý sự kiện khi người dùng nhấn nút "Làm mới"
            txtThang.Text = "";
            txtNam.Text = "";
            comboBoxTenDangNhap.SelectedIndex = -1;
            LoadData(); // Load lại dữ liệu ban đầu
        }

        private void Statistical_Load(object sender, EventArgs e)
        {
        
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtThang_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void txtNam_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Account accountForm = new Account();
            accountForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Đăng xuất", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Đăng xuất và mở form Login
                // Đóng form hiện tại
                this.Close();
                // Mở form Login
                Login loginForm = new Login();
                loginForm.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //We need DGVPrinter helper For PDF file
            printer.Title = "DANH SÁCH THỐNG KÊ";
            printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "Have a good day!";
            printer.FooterSpacing = 15;
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridView(dataGridView1);
        }

        private void btnPredict_Click(object sender, EventArgs e)
        {

        }
    }
}
