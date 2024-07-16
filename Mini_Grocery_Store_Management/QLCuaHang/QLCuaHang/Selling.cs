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
    public partial class Selling : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=GIALONGCHUAI\\SUCOBATNGO;Initial Catalog=QL_CuaHang;Integrated Security=True");
        private DataTable selectedProductsTable;
        DGVPrinter printer = new DGVPrinter();
        public Selling()
        {
            InitializeComponent();
            LoadData();
            LoadDanhMuc();
            InitializeSelectedProductsTable();
        }

        private void InitializeSelectedProductsTable()
        {
            // Tạo DataTable với các cột tương ứng
            selectedProductsTable = new DataTable();
            selectedProductsTable.Columns.Add("MaSanPham", typeof(int));
            selectedProductsTable.Columns.Add("TenSanPham", typeof(string));
            selectedProductsTable.Columns.Add("MoTaSanPham", typeof(string));
            selectedProductsTable.Columns.Add("GiaSanPham", typeof(decimal));
            selectedProductsTable.Columns.Add("SoLuong", typeof(int));
            selectedProductsTable.Columns.Add("ThanhTien", typeof(decimal));

            // Gắn DataTable với dataGridView2
            dataGridView2.DataSource = selectedProductsTable;
        }

        private void LoadData()
        {
            try
            {
                // Mở kết nối
                connection.Open();

                // Truy vấn SQL để lấy dữ liệu từ bảng SanPham
                string query = "SELECT TenSanPham, MoTaSanPham, GiaSanPham, SoLuongSanPham FROM SanPham";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Hiển thị dữ liệu lên dataGridView1
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối
                connection.Close();
            }
        }

        private void LoadDanhMuc()
        {
            try
            {
                // Mở kết nối
                connection.Open();

                // Truy vấn SQL để lấy danh sách tên danh mục từ bảng DanhMuc
                string query = "SELECT TenDanhMuc FROM DanhMuc";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Đọc dữ liệu từ SqlDataReader và thêm vào comboBox1
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["TenDanhMuc"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối
                connection.Close();
            }
        }

        private void LoadDataByDanhMuc(string tenDanhMuc)
        {
            try
            {
                // Mở kết nối
                connection.Open();

                // Truy vấn SQL để lấy sản phẩm theo danh mục
                string query = "SELECT SanPham.TenSanPham, SanPham.MoTaSanPham, SanPham.GiaSanPham " +
                               "FROM SanPham INNER JOIN DanhMuc ON SanPham.MaDanhMuc = DanhMuc.MaDanhMuc " +
                               "WHERE DanhMuc.TenDanhMuc = @TenDanhMuc";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenDanhMuc", tenDanhMuc);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Hiển thị dữ liệu lên dataGridView1
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối
                connection.Close();
            }
        }

        private void Selling_Load(object sender, EventArgs e)
        {
            // Cập nhật label1 với tên người dùng
            label1.Text = "Xin chào, " + Login.username;
            label5.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void LoadUserName()
        {
        }

        private void LoadProducts()
        {
        }

        private void LoadCategories()
        {
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gọi hàm để hiển thị sản phẩm tương ứng với danh mục đã chọn
            LoadDataByDanhMuc(comboBox1.SelectedItem.ToString());
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Vui lòng nhập số lượng sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Lấy số lượng sản phẩm trong kho từ cơ sở dữ liệu
            int soLuongKho = GetSoLuongSanPhamTrongKho(dataGridView1.CurrentRow.Cells["TenSanPham"].Value.ToString());

            // Lấy số lượng sản phẩm từ TextBox3
            int soLuongNhap = Convert.ToInt32(textBox3.Text);

            // Kiểm tra xem có đủ số lượng trong kho không
            if (soLuongNhap > soLuongKho)
            {
                MessageBox.Show("Số lượng sản phẩm không đủ trong kho.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Lấy dữ liệu từ dataGridView1
            string tenSanPham = dataGridView1.CurrentRow.Cells["TenSanPham"].Value.ToString();
            string moTaSanPham = dataGridView1.CurrentRow.Cells["MoTaSanPham"].Value.ToString();
            decimal giaSanPham = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["GiaSanPham"].Value);
            int soLuong = Convert.ToInt32(textBox3.Text);

            // Tính thành tiền
            decimal thanhTien = giaSanPham * soLuong;

            // Thêm sản phẩm vào DataTable
            DataRow newRow = selectedProductsTable.NewRow();
            newRow["MaSanPham"] = selectedProductsTable.Rows.Count + 1; // Mã sản phẩm tự động tăng
            newRow["TenSanPham"] = tenSanPham;
            newRow["MoTaSanPham"] = moTaSanPham;
            newRow["GiaSanPham"] = giaSanPham;
            newRow["SoLuong"] = soLuong;
            newRow["ThanhTien"] = thanhTien;

            selectedProductsTable.Rows.Add(newRow);

            // Cập nhật tổng thành tiền
            UpdateTotalAmount();
        }

        private int GetSoLuongSanPhamTrongKho(string tenSanPham)
        {
            // Hàm này lấy số lượng sản phẩm trong kho dựa trên tên sản phẩm
            int soLuongKho = 0;

            try
            {
                connection.Open();

                string query = "SELECT SoLuongSanPham FROM SanPham WHERE TenSanPham = @TenSanPham";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenSanPham", tenSanPham);

                soLuongKho = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return soLuongKho;
        }

        private void UpdateTotalAmount()
        {
            decimal totalAmount = 0;

            // Tính tổng thành tiền từ DataTable
            foreach (DataRow row in selectedProductsTable.Rows)
            {
                totalAmount += Convert.ToDecimal(row["ThanhTien"]);
            }

            // Hiển thị tổng thành tiền lên label6
            label6.Text = string.Format("Tổng thành tiền: {0}", totalAmount.ToString("C"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Gọi hàm để reset và hiển thị lại toàn bộ sản phẩm
            LoadData();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn một hàng hợp lệ chưa
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                // Lấy thông tin từ hàng đã chọn
                string tenSanPham = dataGridView1.Rows[e.RowIndex].Cells["TenSanPham"].Value.ToString();
                decimal giaSanPham = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["GiaSanPham"].Value);

                // Hiển thị thông tin lên textBox1 và textBox2
                textBox1.Text = tenSanPham;
                textBox2.Text = giaSanPham.ToString("C");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem dataGridView2 có dữ liệu không
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Không có sản phẩm trong hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Hiển thị hộp thoại xác nhận thanh toán
            DialogResult result = MessageBox.Show("Xác nhận thanh toán hóa đơn?", "Xác nhận",
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Thêm hóa đơn vào bảng HoaDon
            int maNguoiDung = GetMaNguoiDung(Login.username);
            DateTime ngayBan = DateTime.Now;
            decimal tongSoTien = CalculateTotalAmount();

            int maHoaDon = AddHoaDon(maNguoiDung, ngayBan, tongSoTien);

            // Thêm chi tiết hóa đơn vào bảng ChiTietHoaDon
            foreach (DataRow row in selectedProductsTable.Rows)
            {
                int maSanPham = GetMaSanPham(row["TenSanPham"].ToString());
                int soLuong = Convert.ToInt32(row["SoLuong"]);
                decimal giaBan = Convert.ToDecimal(row["GiaSanPham"]);

                AddChiTietHoaDon(maHoaDon, maSanPham, soLuong, giaBan);
                // Trừ số lượng sản phẩm trong kho
                UpdateSoLuongSanPhamTrongKho(maSanPham, soLuong);
            }

            // Cập nhật tổng thành tiền trong bảng HoaDon
            UpdateTongThanhTienHoaDon(maHoaDon, tongSoTien);

            // Hiển thị thông tin vào dataGridView3
            LoadDataToDataGridView3();
            LoadDataToDataGridView3(maHoaDon);
        }

        private void UpdateSoLuongSanPhamTrongKho(int maSanPham, int soLuongBan)
        {
            try
            {
                // Mở kết nối
                connection.Open();

                // Truy vấn SQL để cập nhật số lượng sản phẩm trong kho
                string query = "UPDATE SanPham SET SoLuongSanPham = SoLuongSanPham - @SoLuongBan WHERE MaSanPham = @MaSanPham";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SoLuongBan", soLuongBan);
                command.Parameters.AddWithValue("@MaSanPham", maSanPham);

                // Thực hiện câu lệnh SQL
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating product quantity: " + ex.Message);
            }
            finally
            {
                // Đóng kết nối
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }




        private void LoadDataToDataGridView3(int maHoaDon)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=GIALONGCHUAI\\SUCOBATNGO;Initial Catalog=QL_CuaHang;Integrated Security=True"))
                {
                    connection.Open();

                    string query = "SELECT NguoiDung.TenDangNhap, HoaDon.NgayBan, " +
               "SanPham.TenSanPham AS SanPhamDaMua, ChiTietHoaDon.SoLuong, " +
               "(ChiTietHoaDon.SoLuong * SanPham.GiaSanPham) AS TongSoTien " +
               "FROM HoaDon " +
               "INNER JOIN NguoiDung ON HoaDon.MaNguoiDung = NguoiDung.MaNguoiDung " +
               "INNER JOIN ChiTietHoaDon ON HoaDon.MaHoaDon = ChiTietHoaDon.MaHoaDon " +
               "INNER JOIN SanPham ON ChiTietHoaDon.MaSanPham = SanPham.MaSanPham " +
               "WHERE HoaDon.MaHoaDon = @MaHoaDon";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    decimal tongGiaSanPham = 0;

                    foreach (DataRow row in dataTable.Rows)
                    {
                        tongGiaSanPham += Convert.ToDecimal(row["TongSoTien"]);
                    }

                    DataRow tongRow = dataTable.NewRow();
                    tongRow["SanPhamDaMua"] = "Tổng";
                    tongRow["TongSoTien"] = tongGiaSanPham;

                    dataTable.Rows.Add(tongRow);

                    dataGridView3.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void InitializeDataGridView3()
        {
            // Tạo DataTable với các cột tương ứng
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MaHoaDon", typeof(int));
            dataTable.Columns.Add("TenDangNhap", typeof(string));
            dataTable.Columns.Add("NgayBan", typeof(DateTime));
            dataTable.Columns.Add("SanPhamDaMua", typeof(string));
            dataTable.Columns.Add("SoLuong", typeof(int));
            dataTable.Columns.Add("TongSoTien", typeof(decimal));

            // Gắn DataTable với dataGridView3
            dataGridView3.DataSource = dataTable;
        }


        private int GetMaNguoiDung(string tenDangNhap)
        {
            // Hàm này lấy mã người dùng dựa trên tên đăng nhập
            int maNguoiDung = 0;

            try
            {
                connection.Open();

                string query = "SELECT MaNguoiDung FROM NguoiDung WHERE TenDangNhap = @TenDangNhap";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);

                maNguoiDung = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return maNguoiDung;
        }

        private int AddHoaDon(int maNguoiDung, DateTime ngayBan, decimal tongSoTien)
        {
            int maHoaDon = 0;

            try
            {
                connection.Open();

                string query = "INSERT INTO HoaDon (NgayBan, TongSoTien, TrangThai, MaNguoiDung) " +
                               "VALUES (@NgayBan, @TongSoTien, @TrangThai, @MaNguoiDung);" +
                               "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NgayBan", ngayBan);
                command.Parameters.AddWithValue("@TongSoTien", tongSoTien);
                command.Parameters.AddWithValue("@TrangThai", "Đã thanh toán");
                command.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);

                maHoaDon = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return maHoaDon;
        }

        private void AddChiTietHoaDon(int maHoaDon, int maSanPham, int soLuong, decimal giaBan)
        {
            try
            {
                connection.Open();

                string query = "INSERT INTO ChiTietHoaDon (MaHoaDon, MaSanPham, SoLuong, GiaBan) " +
                               "VALUES (@MaHoaDon, @MaSanPham, @SoLuong, @GiaBan)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                command.Parameters.AddWithValue("@MaSanPham", maSanPham);
                command.Parameters.AddWithValue("@SoLuong", soLuong);
                command.Parameters.AddWithValue("@GiaBan", giaBan);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void UpdateTongThanhTienHoaDon(int maHoaDon, decimal tongSoTien)
        {
            // Hàm này cập nhật tổng thành tiền trong bảng HoaDon
            try
            {
                connection.Open();

                string query = "UPDATE HoaDon SET TongSoTien = @TongSoTien WHERE MaHoaDon = @MaHoaDon";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TongSoTien", tongSoTien);
                command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private decimal CalculateTotalAmount()
        {
            // Hàm này tính tổng thành tiền từ DataTable
            decimal totalAmount = 0;

            foreach (DataRow row in selectedProductsTable.Rows)
            {
                totalAmount += Convert.ToDecimal(row["ThanhTien"]);
            }

            return totalAmount;
        }

        private void LoadDataToDataGridView3()
        {
            // Hàm này hiển thị thông tin hóa đơn vào dataGridView3
            try
            {
                connection.Open();

                string query = "SELECT NguoiDung.TenDangNhap, HoaDon.NgayBan, HoaDon.TongSoTien " +
                               "FROM HoaDon INNER JOIN NguoiDung ON HoaDon.MaNguoiDung = NguoiDung.MaNguoiDung";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView3.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private int GetMaSanPham(string tenSanPham)
        {
        // Hàm này lấy mã sản phẩm dựa trên tên sản phẩm
        int maSanPham = 0;

        try
        {
            connection.Open();

            string query = "SELECT MaSanPham FROM SanPham WHERE TenSanPham = @TenSanPham";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TenSanPham", tenSanPham);

            maSanPham = (int)command.ExecuteScalar();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }

        return maSanPham;
    }

    private void label5_Click(object sender, EventArgs e)
    {

    }

    private void button5_Click(object sender, EventArgs e)
    {
        // Reset dataGridView2
        InitializeSelectedProductsTable();

        // Cập nhật tổng thành tiền
        UpdateTotalAmount();
    }

    private void button6_Click(object sender, EventArgs e)
    {
        // Hiển thị hộp thoại xác nhận
        DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
        printer.Title = "Hóa Đơn Thanh Toán";
        printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date);
        printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
        printer.PageNumbers = true;
        printer.PageNumberInHeader = false;
        printer.PorportionalColumns = true;
        printer.HeaderCellAlignment = StringAlignment.Near;
        printer.Footer = "Have a good day!";
        printer.FooterSpacing = 15;
        printer.printDocument.DefaultPageSettings.Landscape = true;
        printer.PrintDataGridView(dataGridView3);
    }

    }
}
