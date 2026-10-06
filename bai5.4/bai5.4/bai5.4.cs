using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Bai5_4
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmManager());
        }
    }

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public string Department { get; set; }
    }

    public class FrmManager : Form
    {
        TreeView tvDepartments;
        ListView lsvEmployees;
        ComboBox cboView;
        ImageList imageList;

        List<Employee> employees;

        public FrmManager()
        {
            Text = "TRÌNH QUẢN LÝ NHÂN VIÊN";
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;

            employees = new List<Employee>
            {
                new Employee
                {
                    Id = "NV001",
                    Name = "Nguyễn Văn An",
                    Position = "Trưởng phòng",
                    StartDate = new DateTime(2020, 5, 10),
                    Department = "Phòng Kinh Doanh"
                },

                new Employee
                {
                    Id = "NV002",
                    Name = "Trần Văn Bình",
                    Position = "Nhân viên",
                    StartDate = new DateTime(2022, 3, 15),
                    Department = "Phòng Kinh Doanh"
                },

                new Employee
                {
                    Id = "NV003",
                    Name = "Lê Thị Hoa",
                    Position = "Nhân viên",
                    StartDate = new DateTime(2021, 7, 20),
                    Department = "Phòng Kinh Doanh"
                },

                new Employee
                {
                    Id = "NV004",
                    Name = "Phạm Văn Nam",
                    Position = "Trưởng phòng",
                    StartDate = new DateTime(2019, 2, 12),
                    Department = "Phòng Kỹ Thuật"
                },

                new Employee
                {
                    Id = "NV005",
                    Name = "Hoàng Minh Đức",
                    Position = "Lập trình viên",
                    StartDate = new DateTime(2023, 8, 1),
                    Department = "Phòng Kỹ Thuật"
                },

                new Employee
                {
                    Id = "NV006",
                    Name = "Nguyễn Thị Lan",
                    Position = "Lập trình viên",
                    StartDate = new DateTime(2022, 11, 5),
                    Department = "Phòng Kỹ Thuật"
                },

                new Employee
                {
                    Id = "NV007",
                    Name = "Đỗ Văn Hùng",
                    Position = "Trưởng nhóm",
                    StartDate = new DateTime(2020, 9, 18),
                    Department = "Phòng Nhân Sự"
                },

                new Employee
                {
                    Id = "NV008",
                    Name = "Vũ Thị Mai",
                    Position = "Nhân viên",
                    StartDate = new DateTime(2023, 1, 10),
                    Department = "Phòng Nhân Sự"
                }
            };

            imageList = new ImageList();
            imageList.Images.Add(
                SystemIcons.Application.ToBitmap());
            imageList.Images.Add(
                SystemIcons.Information.ToBitmap());

            tvDepartments = new TreeView
            {
                Dock = DockStyle.Fill,
                ImageList = imageList
            };

            TreeNode company =
                new TreeNode("Công ty ABC", 0, 0);

            TreeNode kinhDoanh =
                new TreeNode("Phòng Kinh Doanh", 1, 1);

            TreeNode kyThuat =
                new TreeNode("Phòng Kỹ Thuật", 1, 1);

            TreeNode nhanSu =
                new TreeNode("Phòng Nhân Sự", 1, 1);

            kinhDoanh.Nodes.Add(
                new TreeNode("Nhóm Kinh Doanh 1", 1, 1));

            kinhDoanh.Nodes.Add(
                new TreeNode("Nhóm Kinh Doanh 2", 1, 1));

            kyThuat.Nodes.Add(
                new TreeNode("Nhóm Lập Trình", 1, 1));

            kyThuat.Nodes.Add(
                new TreeNode("Nhóm Hỗ Trợ", 1, 1));

            nhanSu.Nodes.Add(
                new TreeNode("Nhóm Tuyển Dụng", 1, 1));

            company.Nodes.Add(kinhDoanh);
            company.Nodes.Add(kyThuat);
            company.Nodes.Add(nhanSu);

            tvDepartments.Nodes.Add(company);
            company.Expand();

            tvDepartments.AfterSelect +=
                TvDepartments_AfterSelect;

            lsvEmployees = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                LargeImageList = imageList,
                SmallImageList = imageList
            };

            lsvEmployees.Columns.Add("Mã NV", 100);
            lsvEmployees.Columns.Add("Họ Tên", 200);
            lsvEmployees.Columns.Add("Chức vụ", 180);
            lsvEmployees.Columns.Add("Ngày vào làm", 150);

            cboView = new ComboBox
            {
                Dock = DockStyle.Top,
                Height = 35,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboView.Items.AddRange(new object[]
            {
                "Details",
                "SmallIcon",
                "LargeIcon",
                "Tile"
            });

            cboView.SelectedIndex = 0;
            cboView.SelectedIndexChanged +=
                CboView_SelectedIndexChanged;

            SplitContainer split =
                new SplitContainer
                {
                    Dock = DockStyle.Fill,
                    SplitterDistance = 300
                };

            split.Panel1.Controls.Add(tvDepartments);
            split.Panel2.Controls.Add(lsvEmployees);
            split.Panel2.Controls.Add(cboView);

            Controls.Add(split);

            ShowEmployees("Công ty ABC");
        }

        private void TvDepartments_AfterSelect(
            object sender,
            TreeViewEventArgs e)
        {
            ShowEmployees(e.Node.Text);
        }

        private void ShowEmployees(string nodeName)
        {
            lsvEmployees.Items.Clear();

            string department = "";

            if (nodeName.Contains("Kinh Doanh"))
                department = "Phòng Kinh Doanh";

            else if (nodeName.Contains("Kỹ Thuật") ||
                     nodeName.Contains("Lập Trình") ||
                     nodeName.Contains("Hỗ Trợ"))
                department = "Phòng Kỹ Thuật";

            else if (nodeName.Contains("Nhân Sự") ||
                     nodeName.Contains("Tuyển Dụng"))
                department = "Phòng Nhân Sự";

            foreach (Employee emp in employees)
            {
                if (string.IsNullOrEmpty(department) ||
                    emp.Department == department)
                {
                    ListViewItem item =
                        new ListViewItem(emp.Id, 1);

                    item.SubItems.Add(emp.Name);
                    item.SubItems.Add(emp.Position);
                    item.SubItems.Add(
                        emp.StartDate.ToString("dd/MM/yyyy"));

                    lsvEmployees.Items.Add(item);
                }
            }
        }

        private void CboView_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            switch (cboView.SelectedItem.ToString())
            {
                case "Details":
                    lsvEmployees.View = View.Details;
                    break;

                case "SmallIcon":
                    lsvEmployees.View = View.SmallIcon;
                    break;

                case "LargeIcon":
                    lsvEmployees.View = View.LargeIcon;
                    break;

                case "Tile":
                    lsvEmployees.View = View.Tile;
                    break;
            }
        }
    }
}