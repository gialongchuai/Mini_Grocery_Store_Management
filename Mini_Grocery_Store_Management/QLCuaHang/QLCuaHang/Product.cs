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

namespace QLCuaHang
{
    public partial class Product : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable table;
        private SqlCommandBuilder commandBuilder;
        public Product()
        {
            InitializeComponent();
            string connectionString = "Data Source=GIALONGCHUAI\\SUCOBATNGO;Initial Catalog=QL_CuaHang;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            LoadData();
            LoadComboBox();
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.ReadOnly = true; // Make the DataGridView read-only by default
        }

        private void LoadData()
        {
            connection.Open();
            string query = "SELECT * FROM SanPham";
            adapter = new SqlDataAdapter(query, connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
            commandBuilder = new SqlCommandBuilder(adapter); // Used for updating changes back to the database
        }

        private void LoadComboBox()
        {
            connection.Open();
            string query = "SELECT TenDanhMuc FROM DanhMuc";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
            }
            connection.Close();
        }

        private void Product_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            LoadData();
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
             try
            {
                if (IsTextBoxEmpty())
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = table.NewRow();
                row["MaSanPham"] = textBox1.Text;
                row["TenSanPham"] = textBox2.Text;
                row["MoTaSanPham"] = textBox3.Text;
                row["GiaSanPham"] = textBox4.Text;
                row["SoLuongSanPham"] = textBox5.Text;
                row["MaDanhMuc"] = textBox6.Text;

                if (table.Select(String.Format("MaSanPham = '{0}'", textBox1.Text)).Length > 0)
                {
                    MessageBox.Show("Mã sản phẩm đã tồn tại. Vui lòng nhập một mã sản phẩm khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                table.Rows.Add(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Enable editing in the TextBoxes
            EnableTextBoxes(true);

            // Enable editing in the DataGridView
            dataGridView1.ReadOnly = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           // Prompt user to select a row for deletion
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một dòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                row.Selected = false; // Deselect the row
                table.Rows[row.Index].Delete();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsTextBoxEmpty())
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                adapter.Update(table); // Update changes to the database
                dataGridView1.ReadOnly = true; // Disable editing after saving changes

                MessageBox.Show("Dữ liệu đã được lưu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsTextBoxEmpty()
        {
            return string.IsNullOrWhiteSpace(textBox1.Text) ||
                   string.IsNullOrWhiteSpace(textBox2.Text) ||
                   string.IsNullOrWhiteSpace(textBox3.Text) ||
                   string.IsNullOrWhiteSpace(textBox4.Text) ||
                   string.IsNullOrWhiteSpace(textBox5.Text) ||
                   string.IsNullOrWhiteSpace(textBox6.Text);
        }

        private void EnableTextBoxes(bool enable)
        {
            textBox1.ReadOnly = !enable;
            textBox2.ReadOnly = !enable;
            textBox3.ReadOnly = !enable;
            textBox4.ReadOnly = !enable;
            textBox5.ReadOnly = !enable;
            textBox6.ReadOnly = !enable;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                string selectedCategory = comboBox1.SelectedItem.ToString();
                LoadProductsByCategory(selectedCategory);
            }
        }

        private void LoadProductsByCategory(string tenDanhMuc)
        {
           try
            {
                connection.Open();

                string query = "SELECT SanPham.MaSanPham, SanPham.TenSanPham, SanPham.MoTaSanPham, SanPham.GiaSanPham, SanPham.SoLuongSanPham, SanPham.MaDanhMuc " +
                                "FROM SanPham INNER JOIN DanhMuc ON SanPham.MaDanhMuc = DanhMuc.MaDanhMuc " +
                                "WHERE DanhMuc.TenDanhMuc = @TenDanhMuc";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TenDanhMuc", tenDanhMuc);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Category categoryForm = new Category();
            categoryForm.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Account accountForm = new Account();
            accountForm.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                textBox1.Text = selectedRow.Cells["MaSanPham"].Value.ToString();
                textBox2.Text = selectedRow.Cells["TenSanPham"].Value.ToString();
                textBox3.Text = selectedRow.Cells["MoTaSanPham"].Value.ToString();
                textBox4.Text = selectedRow.Cells["GiaSanPham"].Value.ToString();
                textBox5.Text = selectedRow.Cells["SoLuongSanPham"].Value.ToString();
                textBox6.Text = selectedRow.Cells["MaDanhMuc"].Value.ToString();
            }
        }
    }
}
