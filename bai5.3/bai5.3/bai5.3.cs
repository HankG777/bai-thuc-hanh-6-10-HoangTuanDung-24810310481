using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Bai5_3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmProducts());
        }
    }

    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }

    public class FrmProducts : Form
    {
        TextBox txtId, txtName, txtPrice, txtQuantity, txtSearch;
        ComboBox cboCategory;
        Button btnAdd, btnEdit, btnDelete, btnSearch;
        DataGridView dgvProducts;

        List<Product> products;
        BindingSource bindingSource;

        public FrmProducts()
        {
            Text = "QUẢN LÝ DANH SÁCH SẢN PHẨM";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            products = new List<Product>();
            bindingSource = new BindingSource();

            GroupBox gbInfo = new GroupBox
            {
                Text = "Thông tin sản phẩm",
                Location = new Point(20, 20),
                Size = new Size(940, 150)
            };

            Label lblId = MakeLabel("Mã SP:", 20, 30);
            txtId = MakeTextBox(100, 27);

            Label lblName = MakeLabel("Tên SP:", 320, 30);
            txtName = MakeTextBox(400, 27);

            Label lblPrice = MakeLabel("Đơn giá:", 20, 75);
            txtPrice = MakeTextBox(100, 72);

            Label lblQuantity = MakeLabel("Số lượng:", 320, 75);
            txtQuantity = MakeTextBox(400, 72);

            Label lblCategory = MakeLabel("Danh mục:", 620, 30);

            cboCategory = new ComboBox
            {
                Location = new Point(700, 27),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboCategory.Items.AddRange(new object[]
            {
                "Điện thoại",
                "Laptop",
                "Phụ kiện",
                "Thiết bị khác"
            });

            cboCategory.SelectedIndex = 0;

            gbInfo.Controls.AddRange(new Control[]
            {
                lblId, txtId,
                lblName, txtName,
                lblPrice, txtPrice,
                lblQuantity, txtQuantity,
                lblCategory, cboCategory
            });

            GroupBox gbFunction = new GroupBox
            {
                Text = "Chức năng",
                Location = new Point(20, 185),
                Size = new Size(940, 70)
            };

            btnAdd = MakeButton("Thêm", 20, 25);
            btnEdit = MakeButton("Sửa", 120, 25);
            btnDelete = MakeButton("Xóa", 220, 25);

            txtSearch = new TextBox
            {
                Location = new Point(500, 25),
                Width = 250
            };

            btnSearch = MakeButton("Tìm kiếm", 760, 22);

            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSearch.Click += BtnSearch_Click;

            gbFunction.Controls.AddRange(new Control[]
            {
                btnAdd, btnEdit, btnDelete,
                txtSearch, btnSearch
            });

            dgvProducts = new DataGridView
            {
                Location = new Point(20, 275),
                Size = new Size(940, 310),
                AutoGenerateColumns = true,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill
            };

            dgvProducts.CellClick += DgvProducts_CellClick;

            Controls.Add(gbInfo);
            Controls.Add(gbFunction);
            Controls.Add(dgvProducts);

            LoadData();
        }

        Label MakeLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true
            };
        }

        TextBox MakeTextBox(int x, int y)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Width = 200
            };
        }

        Button MakeButton(string text, int x, int y)
        {
            return new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(85, 35)
            };
        }

        void LoadData()
        {
            bindingSource.DataSource = null;
            bindingSource.DataSource = products;
            dgvProducts.DataSource = bindingSource;
        }

        private bool GetProductFromInput(Product p)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            decimal price;
            int quantity;

            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Đơn giá không hợp lệ!");
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out quantity))
            {
                MessageBox.Show("Số lượng không hợp lệ!");
                return false;
            }

            p.ProductId = txtId.Text;
            p.ProductName = txtName.Text;
            p.UnitPrice = price;
            p.Quantity = quantity;
            p.Category = cboCategory.Text;

            return true;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Product p = new Product();

            if (!GetProductFromInput(p))
                return;

            if (products.Any(x => x.ProductId == p.ProductId))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!");
                return;
            }

            products.Add(p);
            LoadData();
            ClearInput();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
                return;

            Product p =
                dgvProducts.CurrentRow.DataBoundItem as Product;

            if (p == null)
                return;

            if (GetProductFromInput(p))
            {
                LoadData();
                ClearInput();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
                return;

            Product p =
                dgvProducts.CurrentRow.DataBoundItem as Product;

            if (p == null)
                return;

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa sản phẩm này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                products.Remove(p);
                LoadData();
                ClearInput();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string keyword =
                txtSearch.Text.Trim().ToLower();

            var result = products
                .Where(p =>
                    p.ProductName.ToLower()
                    .Contains(keyword))
                .ToList();

            bindingSource.DataSource = result;
            dgvProducts.DataSource = bindingSource;
        }

        private void DgvProducts_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            Product p =
                dgvProducts.Rows[e.RowIndex]
                .DataBoundItem as Product;

            if (p == null)
                return;

            txtId.Text = p.ProductId;
            txtName.Text = p.ProductName;
            txtPrice.Text = p.UnitPrice.ToString();
            txtQuantity.Text = p.Quantity.ToString();
            cboCategory.Text = p.Category;
        }

        private void ClearInput()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            cboCategory.SelectedIndex = 0;
        }
    }
}