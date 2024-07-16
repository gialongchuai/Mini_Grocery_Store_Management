namespace QLCuaHang
{
    partial class Statistical
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.txtThang = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNam = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTenDangNhap = new System.Windows.Forms.ComboBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(197)))), ((int)(((byte)(222)))));
            this.button1.Location = new System.Drawing.Point(295, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 53);
            this.button1.TabIndex = 0;
            this.button1.Text = "Thực thi";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(471, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(824, 427);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(197)))), ((int)(((byte)(222)))));
            this.button2.Location = new System.Drawing.Point(63, 343);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 53);
            this.button2.TabIndex = 2;
            this.button2.Text = "Làm mới";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtThang
            // 
            this.txtThang.Location = new System.Drawing.Point(229, 184);
            this.txtThang.Name = "txtThang";
            this.txtThang.Size = new System.Drawing.Size(181, 26);
            this.txtThang.TabIndex = 3;
            this.txtThang.TextChanged += new System.EventHandler(this.txtThang_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(595, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "THỐNG KÊ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nhập số tháng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Nhập số năm";
            // 
            // txtNam
            // 
            this.txtNam.Location = new System.Drawing.Point(229, 230);
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(181, 26);
            this.txtNam.TabIndex = 11;
            this.txtNam.TextChanged += new System.EventHandler(this.txtNam_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 278);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Chọn tên người dùng";
            // 
            // comboBoxTenDangNhap
            // 
            this.comboBoxTenDangNhap.FormattingEnabled = true;
            this.comboBoxTenDangNhap.Location = new System.Drawing.Point(229, 278);
            this.comboBoxTenDangNhap.Name = "comboBoxTenDangNhap";
            this.comboBoxTenDangNhap.Size = new System.Drawing.Size(181, 28);
            this.comboBoxTenDangNhap.TabIndex = 8;
            this.comboBoxTenDangNhap.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(197)))), ((int)(((byte)(222)))));
            this.button8.Location = new System.Drawing.Point(11, 89);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(115, 53);
            this.button8.TabIndex = 35;
            this.button8.Text = "Tài khoản";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(197)))), ((int)(((byte)(222)))));
            this.button6.Location = new System.Drawing.Point(11, 436);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(115, 53);
            this.button6.TabIndex = 36;
            this.button6.Text = "Đăng xuất";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(197)))), ((int)(((byte)(222)))));
            this.button4.Location = new System.Drawing.Point(350, 436);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(115, 53);
            this.button4.TabIndex = 37;
            this.button4.Text = "In hóa đơn";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Statistical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 518);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtNam);
            this.Controls.Add(this.comboBoxTenDangNhap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThang);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "Statistical";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistical";
            this.Load += new System.EventHandler(this.Statistical_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtThang;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTenDangNhap;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button4;
    }
}