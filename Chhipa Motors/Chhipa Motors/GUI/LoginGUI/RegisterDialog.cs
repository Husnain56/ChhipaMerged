using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI.LoginGUI
{
    public partial class RegisterDialog : Form
    {
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirmPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnRegister;
        private Button btnCancel;

        private UserDTO _userDTO;
        private LoginBL _loginBL;

        public string Username => txtUsername.Text;
        public string Password => txtPassword.Text;

        public RegisterDialog()
        {
            InitializeComponent();
            InitializeComponents();
            _userDTO = new UserDTO();
            _loginBL = new LoginBL();
        }

        private void InitializeComponents()
        {
            this.Text = "Create New Account";
            this.Size = new Size(450, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            lblTitle = new Label
            {
                Text = "Create Account",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(102, 126, 234),
                Location = new Point(30, 30),
                AutoSize = true
            };

            lblUsername = new Label
            {
                Text = "Username",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(30, 100),
                AutoSize = true
            };

            txtUsername = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(30, 130),
                Size = new Size(380, 35),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblPassword = new Label
            {
                Text = "Password",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(30, 185),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(30, 215),
                Size = new Size(380, 35),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●'
            };

            lblConfirmPassword = new Label
            {
                Text = "Confirm Password",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(30, 270),
                AutoSize = true
            };

            txtConfirmPassword = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(30, 300),
                Size = new Size(380, 35),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●'
            };

            btnRegister = new GradientButton
            {
                Text = "CREATE ACCOUNT",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 360),
                Size = new Size(180, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            btnCancel = new Button
            {
                Text = "CANCEL",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Gray,
                Location = new Point(230, 360),
                Size = new Size(180, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.FromArgb(240, 240, 240)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] {
            lblTitle, lblUsername, txtUsername,
            lblPassword, txtPassword,
            lblConfirmPassword, txtConfirmPassword,
            btnRegister, btnCancel
        });
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            _userDTO.Username = username;
            _userDTO.Password = password;
            _userDTO.Role = "User";

            if (_loginBL.CreateUserAccount(_userDTO) > 0)
            {
                MessageBox.Show("User Created Successfully!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if(_loginBL.CreateUserAccount(_userDTO) == -1)
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Failed to create user account. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
