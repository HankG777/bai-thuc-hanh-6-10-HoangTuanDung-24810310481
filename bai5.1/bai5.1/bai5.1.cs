using System;
using System.Drawing;
using System.Windows.Forms;

namespace bai5_1
{ 
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmRegister());
        }
    }

    public class FrmRegister : Form
    {
        TextBox txtUsername, txtPassword, txtConfirm;
        DateTimePicker dtpBirth;
        RadioButton radMale, radFemale;
        CheckBox chkTerms;
        Button btnRegister, btnReset;
        ErrorProvider epCheck;

        public FrmRegister()
        {
            Text = "ĐĂNG KÝ TÀI KHOẢN";
            Size = new Size(520, 500);
            StartPosition = FormStartPosition.CenterScreen;

            epCheck = new ErrorProvider();

            GroupBox gbPersonal = new GroupBox
            {
                Text = "Thông tin cá nhân",
                Location = new Point(20, 20),
                Size = new Size(455, 180)
            };

            Label lblUser = new Label
            {
                Text = "Tên đăng nhập:",
                Location = new Point(20, 30),
                AutoSize = true
            };

            txtUsername = new TextBox
            {
                Location = new Point(150, 27),
                Width = 250
            };

            Label lblPass = new Label
            {
                Text = "Mật khẩu:",
                Location = new Point(20, 70),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Location = new Point(150, 67),
                Width = 250,
                UseSystemPasswordChar = true
            };

            Label lblConfirm = new Label
            {
                Text = "Xác nhận mật khẩu:",
                Location = new Point(20, 110),
                AutoSize = true
            };

            txtConfirm = new TextBox
            {
                Location = new Point(150, 107),
                Width = 250,
                UseSystemPasswordChar = true
            };

            gbPersonal.Controls.AddRange(new Control[]
            {
                lblUser, txtUsername,
                lblPass, txtPassword,
                lblConfirm, txtConfirm
            });

            GroupBox gbMore = new GroupBox
            {
                Text = "Thông tin bổ sung",
                Location = new Point(20, 215),
                Size = new Size(455, 150)
            };

            Label lblBirth = new Label
            {
                Text = "Ngày sinh:",
                Location = new Point(20, 30),
                AutoSize = true
            };

            dtpBirth = new DateTimePicker
            {
                Location = new Point(150, 27),
                Width = 250,
                Format = DateTimePickerFormat.Short
            };

            Label lblGender = new Label
            {
                Text = "Giới tính:",
                Location = new Point(20, 70),
                AutoSize = true
            };

            radMale = new RadioButton
            {
                Text = "Nam",
                Location = new Point(150, 67),
                Checked = true
            };

            radFemale = new RadioButton
            {
                Text = "Nữ",
                Location = new Point(230, 67)
            };

            chkTerms = new CheckBox
            {
                Text = "Tôi đồng ý với điều khoản dịch vụ",
                Location = new Point(150, 105),
                AutoSize = true
            };

            gbMore.Controls.AddRange(new Control[]
            {
                lblBirth, dtpBirth,
                lblGender, radMale, radFemale,
                chkTerms
            });

            btnRegister = new Button
            {
                Text = "Đăng Ký",
                Location = new Point(140, 390),
                Size = new Size(100, 35)
            };

            btnReset = new Button
            {
                Text = "Làm Mới",
                Location = new Point(260, 390),
                Size = new Size(100, 35)
            };

            btnRegister.Click += BtnRegister_Click;
            btnReset.Click += BtnReset_Click;

            Controls.Add(gbPersonal);
            Controls.Add(gbMore);
            Controls.Add(btnRegister);
            Controls.Add(btnReset);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            epCheck.Clear();

            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                epCheck.SetError(txtUsername, "Tên đăng nhập không được để trống!");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                epCheck.SetError(txtPassword, "Mật khẩu không được để trống!");
                valid = false;
            }

            if (txtPassword.Text != txtConfirm.Text)
            {
                epCheck.SetError(txtConfirm, "Mật khẩu nhập lại không khớp!");
                valid = false;
            }

            int age = DateTime.Today.Year - dtpBirth.Value.Year;

            if (dtpBirth.Value.Date > DateTime.Today.AddYears(-age))
                age--;

            if (age < 18)
            {
                epCheck.SetError(
                    dtpBirth,
                    "Người đăng ký phải đủ 18 tuổi!"
                );
                valid = false;
            }

            if (!chkTerms.Checked)
            {
                epCheck.SetError(
                    chkTerms,
                    "Bạn phải đồng ý với điều khoản dịch vụ!"
                );
                valid = false;
            }

            if (valid)
            {
                MessageBox.Show(
                    "Đăng ký tài khoản thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirm.Clear();

            dtpBirth.Value = DateTime.Today;
            radMale.Checked = true;
            chkTerms.Checked = false;

            epCheck.Clear();
        }
    }
}