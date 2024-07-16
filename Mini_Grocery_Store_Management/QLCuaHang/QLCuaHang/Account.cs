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
    public partial class Account : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable table;
        private SqlCommandBuilder commandBuilder;
        private int selectedRowIndex = -1;
        public Account()
        {
            InitializeComponent();
            string connectionString = "Data Source=GIALONGCHUAI\\SUCOBATNGO;Initial Catalog=QL_CuaHang;Integrated Security=True";
            connection = new SqlConnection(connectionString);
            LoadData();
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.ReadOnly = true;
        }

        private void LoadData()
        {
            connection.Open();
            string query = "SELECT * FROM NguoiDung";
            adapter = new SqlDataAdapter(query, connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
            commandBuilder = new SqlCommandBuilder(adapter); // Used for updating changes back to the database
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Category categoryForm = new Category();
            categoryForm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Product productForm = new Product();
            productForm.Show();
            this.Hide();
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsTextBoxEmpty() || comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin và chọn vai trò.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = table.NewRow();
                row["MaNguoiDung"] = textBox1.Text;
                row["HoTen"] = textBox2.Text;
                row["Email"] = textBox3.Text;
                row["SoDienThoai"] = textBox4.Text;
                row["DiaChi"] = textBox5.Text;
                row["TenDangNhap"] = textBox6.Text;
                row["MatKhau"] = textBox7.Text;
                row["VaiTro"] = comboBox1.SelectedItem.ToString();

                if (table.Select(String.Format("MaNguoiDung = '{0}'", textBox1.Text)).Length > 0)
                {
                    MessageBox.Show("Mã người dùng đã tồn tại. Vui lòng nhập một mã người dùng khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (table.Select(String.Format("Email = '{0}'", textBox3.Text)).Length > 0)
                {
                    MessageBox.Show("Email đã tồn tại. Vui lòng nhập một email khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (table.Select(String.Format("TenDangNhap = '{0}'", textBox6.Text)).Length > 0)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng nhập một tên đăng nhập khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (table.Select(String.Format("SoDienThoai = '{0}'", textBox4.Text)).Length > 0)
                {
                    MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập một số điện thoại khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                table.Rows.Add(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Enable editing in the TextBoxes
            EnableTextBoxes(true);

            // Enable editing in the DataGridView
            dataGridView1.ReadOnly = false;

            // Get the selected row index
            if (dataGridView1.SelectedCells.Count > 0)
            {
                selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                // Populate TextBoxes with selected row data
                if (selectedRowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];
                    textBox1.Text = selectedRow.Cells["MaNguoiDung"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["HoTen"].Value.ToString();
                    textBox3.Text = selectedRow.Cells["Email"].Value.ToString();
                    textBox4.Text = selectedRow.Cells["SoDienThoai"].Value.ToString();
                    textBox5.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                    textBox6.Text = selectedRow.Cells["TenDangNhap"].Value.ToString();
                    textBox7.Text = selectedRow.Cells["MatKhau"].Value.ToString();
                    comboBox1.SelectedItem = selectedRow.Cells["VaiTro"].Value.ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // Save changes to the database
                adapter.Update(table);
                dataGridView1.ReadOnly = true; // Disable editing after saving changes

                MessageBox.Show("Dữ liệu đã được lưu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Account_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                textBox1.Text = selectedRow.Cells["MaNguoiDung"].Value.ToString();
                textBox2.Text = selectedRow.Cells["HoTen"].Value.ToString();
                textBox3.Text = selectedRow.Cells["Email"].Value.ToString();
                textBox4.Text = selectedRow.Cells["SoDienThoai"].Value.ToString();
                textBox5.Text = selectedRow.Cells["DiaChi"].Value.ToString();
                textBox6.Text = selectedRow.Cells["TenDangNhap"].Value.ToString();
                textBox7.Text = selectedRow.Cells["MatKhau"].Value.ToString();
                comboBox1.SelectedItem = selectedRow.Cells["VaiTro"].Value.ToString();
            }
        }
         private bool IsTextBoxEmpty()
        {
            return string.IsNullOrWhiteSpace(textBox1.Text) ||
                   string.IsNullOrWhiteSpace(textBox2.Text) ||
                   string.IsNullOrWhiteSpace(textBox3.Text) ||
                   string.IsNullOrWhiteSpace(textBox4.Text) ||
                   string.IsNullOrWhiteSpace(textBox5.Text) ||
                   string.IsNullOrWhiteSpace(textBox6.Text) ||
                   string.IsNullOrWhiteSpace(textBox7.Text);
        }

        private void EnableTextBoxes(bool enable)
        {
            textBox1.ReadOnly = !enable;
            textBox2.ReadOnly = !enable;
            textBox3.ReadOnly = !enable;
            textBox4.ReadOnly = !enable;
            textBox5.ReadOnly = !enable;
            textBox6.ReadOnly = !enable;
            textBox7.ReadOnly = !enable;
            comboBox1.Enabled = enable;
        }

        private void button10_Click(object sender, EventArgs e)
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

        private void button8_Click(object sender, EventArgs e)
        {
            Statistical statisticalForm = new Statistical();
            statisticalForm.Show();
            this.Hide();
        }
    }
}
