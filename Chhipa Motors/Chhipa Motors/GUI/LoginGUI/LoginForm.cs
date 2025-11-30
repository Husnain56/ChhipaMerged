using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;
using Chhipa_Motors.GUI.Car_Cards;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Chhipa_Motors.GUI.Admin_Panel;

namespace Chhipa_Motors.GUI.LoginGUI
{
    public partial class LoginForm : Form
    {
        private Panel leftPanel;
        private Panel rightPanel;
        private Label lblLogo;
        private Label lblWelcome;
        private Label lblSubtitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private RadioButton rbUser;
        private RadioButton rbAdmin;
        private Label lblUserType;
        private LinkLabel lnkCreateAccount;

        private UserDTO _userDTO;
        private LoginBL _loginBL;

        public LoginForm()
        {
            InitializeComponent();
            InitializeButton();
            _userDTO = new UserDTO();
            _loginBL = new LoginBL();
        }
        private void InitializeButton()
        {
            this.Text = "Chhipa Motors - Login";
            this.Size = new Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            leftPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 400,
                BackColor = Color.FromArgb(102, 126, 234)
            };
            leftPanel.Paint += LeftPanel_Paint;

            lblLogo = new Label
            {
                Text = "🚗",
                Font = new Font("Segoe UI", 60, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(150, 80)
            };

            lblWelcome = new Label
            {
                Text = "Chhipa Motors",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(80, 180)
            };

            lblSubtitle = new Label
            {
                Text = "Your trusted partner in finding\nthe perfect vehicle",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(75, 240),
                TextAlign = ContentAlignment.MiddleCenter
            };

            leftPanel.Controls.AddRange(new Control[] { lblLogo, lblWelcome, lblSubtitle });

            rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(60, 80, 60, 80)
            };

            Label lblTitle = new Label
            {
                Text = "Welcome Back!",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true,
                Location = new Point(60, 50)
            };

            Label lblDesc = new Label
            {
                Text = "Please login to continue",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(60, 95)
            };

            lblUserType = new Label
            {
                Text = "Login as:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(60, 140),
                AutoSize = true
            };

            rbUser = new RadioButton
            {
                Text = "Customer",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(102, 126, 234),
                Location = new Point(60, 170),
                AutoSize = true,
                Checked = true
            };

            rbAdmin = new RadioButton
            {
                Text = "Administrator",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(102, 126, 234),
                Location = new Point(180, 170),
                AutoSize = true
            };

            lblUsername = new Label
            {
                Text = "Username",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(60, 220),
                AutoSize = true
            };

            txtUsername = new RoundedTextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(60, 250),
                Size = new Size(360, 40),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.None,
                Padding = new Padding(10)
            };

            lblPassword = new Label
            {
                Text = "Password",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(60, 310),
                AutoSize = true
            };

            txtPassword = new RoundedTextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(60, 340),
                Size = new Size(360, 40),
                BackColor = Color.FromArgb(245, 245, 245),
                BorderStyle = BorderStyle.None,
                PasswordChar = '●',
                Padding = new Padding(10)
            };

            btnLogin = new GradientButton
            {
                Text = "LOGIN",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(60, 400),
                Size = new Size(360, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            lnkCreateAccount = new LinkLabel
            {
                Text = "Don't have an account? Create one",
                Font = new Font("Segoe UI", 10),
                LinkColor = Color.FromArgb(102, 126, 234),
                Location = new Point(110, 465),
                AutoSize = true
            };
            lnkCreateAccount.Click += LnkCreateAccount_Click;

            rightPanel.Controls.AddRange(new Control[] {
            lblTitle, lblDesc, lblUserType, rbUser, rbAdmin,
            lblUsername, txtUsername, lblPassword, txtPassword,
            btnLogin, lnkCreateAccount
        });

            this.Controls.Add(rightPanel);
            this.Controls.Add(leftPanel);

            Button btnClose = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(this.Width - 50, 10),
                Size = new Size(40, 40),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            btnClose.MouseEnter += (s, e) => btnClose.ForeColor = Color.Red;
            btnClose.MouseLeave += (s, e) => btnClose.ForeColor = Color.FromArgb(200, 200, 200);
            btnClose.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnClose);
            btnClose.BringToFront();
        }

        private void LeftPanel_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                leftPanel.ClientRectangle,
                Color.FromArgb(102, 126, 234),
                Color.FromArgb(118, 75, 162),
                45f))
            {
                e.Graphics.FillRectangle(brush, leftPanel.ClientRectangle);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _userDTO.Username = username;
            _userDTO.Password = password;
            _userDTO.Role = rbAdmin.Checked ? "Admin" : "User"; 

            UserDTO verifiedUser = _loginBL.VerifyUser(_userDTO); 
            

            if (verifiedUser != null)
            {
                if (rbAdmin.Checked)
                {
                    AdminDashboard dashboard = new AdminDashboard();
                    dashboard.FormClosed += (s, args) => this.Show();
                    this.Hide();
                    dashboard.ShowDialog();
                }
                else
                {
                    MainForm mainForm = new MainForm(verifiedUser.Id); 
                    mainForm.FormClosed += (s, args) => this.Show();
                    this.Hide();
                    mainForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LnkCreateAccount_Click(object sender, EventArgs e)
        {
            using (RegisterDialog registerDialog = new RegisterDialog())
            {
                if (registerDialog.ShowDialog() == DialogResult.OK)
                {
                    return;
                }
            }
        }
    }

    public class RoundedTextBox : TextBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0xF)
            {
                using (Graphics g = Graphics.FromHwnd(Handle))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (Pen pen = new Pen(Color.FromArgb(224, 224, 224), 2))
                    {
                        g.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                    }
                }
            }
        }
    }

    public class GradientButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(102, 126, 234),
                Color.FromArgb(118, 75, 162),
                45f))
            {
                e.Graphics.FillRoundedRectangle(brush, 0, 0, Width, Height, 10);
            }

            TextRenderer.DrawText(e.Graphics, this.Text, this.Font,
                this.ClientRectangle, this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush,
            int x, int y, int width, int height, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, radius, radius, 180, 90);
            path.AddArc(x + width - radius, y, radius, radius, 270, 90);
            path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            path.AddArc(x, y + height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            graphics.FillPath(brush, path);
        }
    }
}