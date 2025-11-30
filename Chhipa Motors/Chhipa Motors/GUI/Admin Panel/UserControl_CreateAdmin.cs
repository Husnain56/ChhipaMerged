using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI.Admin_Panel
{
    public partial class UserControl_CreateAdmin : UserControl
    {
        UserDTO _userDTO;
        AdminBL _adminBL;

        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel formPanel;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txt_username;
        private TextBox txt_password;
        private Button btn_create;
        private Button btn_clear;
        private PictureBox iconAdmin;

        public UserControl_CreateAdmin()
        {
            InitializeComponent();
            _userDTO = new UserDTO();
            _adminBL = new AdminBL();
            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScroll = true;

            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Dock = DockStyle.Fill;

            // Header Panel
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(102, 126, 234)
            };
            headerPanel.Paint += HeaderPanel_Paint;

            // Title
            lblTitle = new Label
            {
                Text = "Create Admin Account",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 25)
            };

            // Subtitle
            lblSubtitle = new Label
            {
                Text = "Add a new administrator to manage the system",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 70)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            // Form Panel (Centered Card)
            formPanel = new Panel
            {
                Size = new Size(500, 480),
                BackColor = Color.White,
                Location = new Point((this.Width - 500) / 2, 180)
            };
            formPanel.Paint += FormPanel_Paint;

            // Admin Icon
            iconAdmin = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(210, 30),
                BackColor = Color.FromArgb(102, 126, 234),
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            iconAdmin.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, iconAdmin.Width, iconAdmin.Height);
                    iconAdmin.Region = new Region(path);
                }

                // Draw admin icon (simple user silhouette)
                using (SolidBrush brush = new SolidBrush(Color.White))
                {
                    // Head
                    e.Graphics.FillEllipse(brush, 28, 15, 24, 24);
                    // Body
                    e.Graphics.FillEllipse(brush, 20, 35, 40, 35);
                }
            };

            // Username Label
            lblUsername = new Label
            {
                Text = "Username",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(50, 140),
                AutoSize = true
            };

            // Username TextBox
            txt_username = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(50, 170),
                Size = new Size(400, 35),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250)
            };
            txt_username.GotFocus += (s, e) => txt_username.BackColor = Color.White;
            txt_username.LostFocus += (s, e) => txt_username.BackColor = Color.FromArgb(250, 250, 250);

            // Password Label
            lblPassword = new Label
            {
                Text = "Password",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                Location = new Point(50, 230),
                AutoSize = true
            };

            // Password TextBox
            txt_password = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(50, 260),
                Size = new Size(400, 35),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250),
                PasswordChar = '●'
            };
            txt_password.GotFocus += (s, e) => txt_password.BackColor = Color.White;
            txt_password.LostFocus += (s, e) => txt_password.BackColor = Color.FromArgb(250, 250, 250);

            // Create Button
            btn_create = new GradientButton
            {
                Text = "✓ CREATE ADMIN",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(50, 340),
                Size = new Size(400, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn_create.FlatAppearance.BorderSize = 0;
            btn_create.Click += btn_create_Click;

            // Clear Button
            btn_clear = new Button
            {
                Text = "CLEAR",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(102, 126, 234),
                Location = new Point(50, 405),
                Size = new Size(400, 45),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.White
            };
            btn_clear.FlatAppearance.BorderColor = Color.FromArgb(102, 126, 234);
            btn_clear.FlatAppearance.BorderSize = 2;
            btn_clear.Click += btn_clear_Click;
            btn_clear.MouseEnter += (s, e) => btn_clear.BackColor = Color.FromArgb(240, 243, 250);
            btn_clear.MouseLeave += (s, e) => btn_clear.BackColor = Color.White;

            formPanel.Controls.AddRange(new Control[] {
                iconAdmin, lblUsername, txt_username,
                lblPassword, txt_password, btn_create, btn_clear
            });

            this.Controls.Add(headerPanel);
            this.Controls.Add(formPanel);

            // Center form panel on resize
            this.Resize += (s, e) =>
            {
                formPanel.Location = new Point((this.Width - formPanel.Width) / 2, 180);
            };
        }

        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                headerPanel.ClientRectangle,
                Color.FromArgb(102, 126, 234),
                Color.FromArgb(118, 75, 162),
                45f))
            {
                e.Graphics.FillRectangle(brush, headerPanel.ClientRectangle);
            }
        }

        private void FormPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw shadow
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(shadowBrush, 3, 3, formPanel.Width - 3, formPanel.Height - 3);
            }

            // Draw rounded card
            using (GraphicsPath path = GetRoundedRectPath(
                new Rectangle(0, 0, formPanel.Width - 1, formPanel.Height - 1), 15))
            {
                using (SolidBrush brush = new SolidBrush(formPanel.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.FromArgb(220, 220, 220), 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_password.Clear();
            txt_username.Clear();
            txt_username.Focus();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txt_username.Text) || string.IsNullOrWhiteSpace(txt_password.Text))
            {
                MessageBox.Show(
                    "All fields are required.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (txt_username.Text.Length < 3)
            {
                MessageBox.Show(
                    "Username must be at least 3 characters.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txt_username.Focus();
                return;
            }

            if (txt_password.Text.Length < 6)
            {
                MessageBox.Show(
                    "Password must be at least 6 characters.",
                    "Validation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txt_password.Focus();
                return;
            }

            try
            {
                _userDTO.Username = txt_username.Text.Trim();
                _userDTO.Password = txt_password.Text;
                _userDTO.Role = "Admin";

                if (_adminBL.CreateAdmin(_userDTO) > 0)
                {
                    MessageBox.Show(
                        $"Admin account '{txt_username.Text}' created successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    btn_clear.PerformClick();
                }
                else
                {
                    MessageBox.Show(
                        "Failed to create admin account. Username may already exist.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"An error occurred: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

    // Custom Gradient Button
    public class GradientButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color startColor = this.Enabled ?
                Color.FromArgb(102, 126, 234) : Color.Gray;
            Color endColor = this.Enabled ?
                Color.FromArgb(118, 75, 162) : Color.DarkGray;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                startColor,
                endColor,
                45f))
            {
                e.Graphics.FillRoundedRectangle(brush, 0, 0, Width, Height, 8);
            }

            TextRenderer.DrawText(e.Graphics, this.Text, this.Font,
                this.ClientRectangle, this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}