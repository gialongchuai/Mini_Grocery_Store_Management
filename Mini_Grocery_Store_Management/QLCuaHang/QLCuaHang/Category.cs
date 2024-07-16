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
    public partial class Category : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable table;
        private SqlCommandBuilder commandBuilder;
        private int selectedRowIndex = -1;
        public Category()
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
            string query = "SELECT * FROM DanhMuc";
            adapter = new SqlDataAdapter(query, connection);
            table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
            commandBuilder = new SqlCommandBuilder(adapter); // Used for updating changes back to the database
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Product productForm = new Product();
            productForm.Show();
            this.Hide();
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

        private void button1_Click(object sender, EventArgs e)
        {try
            {
                if (IsTextBoxEmpty())
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow row = table.NewRow();
                row["MaDanhMuc"] = textBox1.Text;
                row["TenDanhMuc"] = textBox2.Text;
                row["MoTaDanhMuc"] = textBox3.Text;

                if (table.Select(String.Format("MaDanhMuc = '{0}'", textBox1.Text)).Length > 0)
                {
                    MessageBox.Show("Mã danh mục đã tồn tại. Vui lòng nhập một mã danh mục khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        {// Enable editing in the TextBoxes
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
                    textBox1.Text = selectedRow.Cells["MaDanhMuc"].Value.ToString();
                    textBox2.Text = selectedRow.Cells["TenDanhMuc"].Value.ToString();
                    textBox3.Text = selectedRow.Cells["MoTaDanhMuc"].Value.ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {// Prompt user to select a row for deletion
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
        {try
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

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            LoadData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Category_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                textBox1.Text = selectedRow.Cells["MaDanhMuc"].Value.ToString();
                textBox2.Text = selectedRow.Cells["TenDanhMuc"].Value.ToString();
                textBox3.Text = selectedRow.Cells["MoTaDanhMuc"].Value.ToString();
            }
        }
        private bool IsTextBoxEmpty()
        {
            return string.IsNullOrWhiteSpace(textBox1.Text) ||
                   string.IsNullOrWhiteSpace(textBox2.Text) ||
                   string.IsNullOrWhiteSpace(textBox3.Text);
        }

        private void EnableTextBoxes(bool enable)
        {
            textBox1.ReadOnly = !enable;
            textBox2.ReadOnly = !enable;
            textBox3.ReadOnly = !enable;
        }
    }
}
