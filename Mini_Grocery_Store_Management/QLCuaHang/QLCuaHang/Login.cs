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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true; // Mặc định che dấu mật khẩu
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = !checkBox1.Checked;
        }
        public static string username;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một vai trò!");
                return;
            }

            string connectionString = "Data Source=GIALONGCHUAI\\SUCOBATNGO;Initial Catalog=QL_CuaHang;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM NguoiDung WHERE TenDangNhap = @username AND MatKhau = @password AND VaiTro = @role";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", textBox1.Text);
                    command.Parameters.AddWithValue("@password", textBox2.Text);
                    command.Parameters.AddWithValue("@role", comboBox1.SelectedItem.ToString() == "Quản trị viên" ? "Quản trị viên" : "Người dùng");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            this.Hide(); // Đóng form Login
                            if (comboBox1.SelectedItem.ToString() == "Quản trị viên")
                            {
                                Product productForm = new Product();
                                productForm.Show();
                            }
                            else
                            {
                                username = textBox1.Text;
                                Selling sellingForm = new Selling();
                                sellingForm.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng, hoặc vai trò không phù hợp.");
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
