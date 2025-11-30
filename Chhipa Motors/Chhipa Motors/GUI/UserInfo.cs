using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI
{
    public partial class UserInfo : UserControl
    {
        private CustomerBL _customerBL;
        private UserBL _userBL;
        private UserDTO _userDTO;
        private bool _isCustomer;

        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel profilePanel;
        private Panel updatePanel;

        private Label lblCurrentName;
        private Label lblCurrentEmail;
        private Label lblCurrentPhone;

        private TextBox txt_input;
        private TextBox txt_pass;

        private Button btn_up_name;
        private Button btn_up_email;
        private Button btn_up_phone;
        private Button btn_delete_account;
        private Button btn_confirm;
        private Button btn_cancel;

        private string currentUpdateField = "";

        public UserInfo(UserDTO dto)
        {
            _customerBL = new CustomerBL();
            _userBL = new UserBL();
            _userDTO = dto;

            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(245, 247, 250);

            CheckIfCustomer();
            InitializeCustomComponents();
            LoadUserInfo();
        }

        private void CheckIfCustomer()
        {
            try
            {
                _isCustomer = _customerBL.IsCustomer(_userDTO.Id);
            }
            catch
            {
                _isCustomer = false;
            }
        }

        private void InitializeCustomComponents()
        {
            this.Size = new Size(1100, 740);

            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(102, 126, 234)
            };
            headerPanel.Paint += HeaderPanel_Paint;

            lblTitle = new Label
            {
                Text = _isCustomer ? "Customer Profile" : "Welcome User!",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 25)
            };

            lblSubtitle = new Label
            {
                Text = _isCustomer ? "Manage your profile and contact information" : "Manage your account settings",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 85)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            profilePanel = new Panel
            {
                Location = new Point(40, 160),
                Size = _isCustomer ? new Size(480, 380) : new Size(480, 260),
                BackColor = Color.White,
                Padding = new Padding(30)
            };
            profilePanel.Paint += ProfilePanel_Paint;

            Label lblProfileTitle = new Label
            {
                Text = _isCustomer ? "Profile Details" : "Account Details",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true,
                Location = new Point(30, 20)
            };
            profilePanel.Controls.Add(lblProfileTitle);

            // Username Section (Always shown)
            CreateInfoSection("Username:", ref lblCurrentName, 80, profilePanel);
            btn_up_name = CreateUpdateButton("Edit", 380, 78);
            btn_up_name.Click += (s, e) => ShowUpdatePanel("Username");
            profilePanel.Controls.Add(btn_up_name);

            if (_isCustomer)
            {
                CreateInfoSection("Email Address:", ref lblCurrentEmail, 170, profilePanel);
                btn_up_email = CreateUpdateButton("Edit", 380, 168);
                btn_up_email.Click += (s, e) => ShowUpdatePanel("Email");
                profilePanel.Controls.Add(btn_up_email);

                CreateInfoSection("Phone Number:", ref lblCurrentPhone, 260, profilePanel);
                btn_up_phone = CreateUpdateButton("Edit", 380, 258);
                btn_up_phone.Click += (s, e) => ShowUpdatePanel("Phone");
                profilePanel.Controls.Add(btn_up_phone);
            }
            else
            {
                btn_delete_account = new Button
                {
                    Text = "🗑 Delete Account",
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(198, 40, 40),
                    Location = new Point(30, 180),
                    Size = new Size(200, 45),
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btn_delete_account.FlatAppearance.BorderSize = 0;
                btn_delete_account.Click += btn_delete_account_Click;
                btn_delete_account.MouseEnter += (s, e) => btn_delete_account.BackColor = Color.FromArgb(180, 30, 30);
                btn_delete_account.MouseLeave += (s, e) => btn_delete_account.BackColor = Color.FromArgb(198, 40, 40);
                profilePanel.Controls.Add(btn_delete_account);
            }

            updatePanel = new Panel
            {
                Location = new Point(560, 160),
                Size = new Size(480, 380),
                BackColor = Color.White,
                Padding = new Padding(30),
                Visible = false
            };
            updatePanel.Paint += ProfilePanel_Paint;

            Label lblUpdateTitle = new Label
            {
                Text = "Update Information",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true,
                Location = new Point(30, 20)
            };

            Label lblNewValue = new Label
            {
                Text = "New Value:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(30, 80)
            };

            txt_input = new TextBox
            {
                Location = new Point(30, 110),
                Size = new Size(420, 40),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblPassword = new Label
            {
                Text = "Confirm Password:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(30, 170)
            };

            txt_pass = new TextBox
            {
                Location = new Point(30, 200),
                Size = new Size(420, 40),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };

            Label lblInfo = new Label
            {
                Text = "Please enter your password to confirm changes",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.FromArgb(120, 120, 120),
                AutoSize = true,
                Location = new Point(30, 250)
            };

            btn_confirm = new GradientButton
            {
                Text = "✓ CONFIRM UPDATE",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(30, 300),
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn_confirm.FlatAppearance.BorderSize = 0;
            btn_confirm.Click += btn_confirm_Click;

            btn_cancel = new Button
            {
                Text = "✕ CANCEL",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(102, 126, 234),
                Location = new Point(250, 300),
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.White
            };
            btn_cancel.FlatAppearance.BorderColor = Color.FromArgb(102, 126, 234);
            btn_cancel.FlatAppearance.BorderSize = 2;
            btn_cancel.Click += btn_cancel_Click;
            btn_cancel.MouseEnter += (s, e) => btn_cancel.BackColor = Color.FromArgb(240, 243, 250);
            btn_cancel.MouseLeave += (s, e) => btn_cancel.BackColor = Color.White;

            updatePanel.Controls.AddRange(new Control[] {
                lblUpdateTitle, lblNewValue, txt_input, lblPassword, txt_pass,
                lblInfo, btn_confirm, btn_cancel
            });

            this.Controls.AddRange(new Control[] { headerPanel, profilePanel, updatePanel });
        }

        private void CreateInfoSection(string labelText, ref Label valueLabel, int yPosition, Panel parentPanel)
        {
            Label lblField = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(30, yPosition)
            };

            valueLabel = new Label
            {
                Text = "Loading...",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true,
                Location = new Point(30, yPosition + 25),
                MaximumSize = new Size(340, 0)
            };

            parentPanel.Controls.Add(lblField);
            parentPanel.Controls.Add(valueLabel);
        }

        private Button CreateUpdateButton(string text, int x, int y)
        {
            Button btn = new Button
            {
                Text = text,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(102, 126, 234),
                Location = new Point(x, y),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.White
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(102, 126, 234);
            btn.FlatAppearance.BorderSize = 1;
            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(240, 243, 250);
            btn.MouseLeave += (s, e) => btn.BackColor = Color.White;
            return btn;
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

        private void ProfilePanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(shadowBrush, 2, 2, panel.Width - 2, panel.Height - 2);
            }

            using (GraphicsPath path = GetRoundedRectPath(new Rectangle(0, 0, panel.Width - 1, panel.Height - 1), 8))
            {
                using (SolidBrush brush = new SolidBrush(panel.BackColor))
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

        private void LoadUserInfo()
        {
            try
            {
                var userInfo = _userBL.GetUserInfo(_userDTO.Id);
                if (userInfo != null)
                {
                    lblCurrentName.Text = userInfo.Username;

                    if (_isCustomer)
                    {
                        var customerInfo = _customerBL.GetCustomerInfo(_userDTO.Id);
                        if (customerInfo != null)
                        {
                            lblCurrentEmail.Text = customerInfo.Email;
                            lblCurrentPhone.Text = customerInfo.PhoneNumber;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user information: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowUpdatePanel(string fieldName)
        {
            currentUpdateField = fieldName;
            updatePanel.Visible = true;
            txt_input.Text = "";
            txt_pass.Text = "";
            txt_input.PlaceholderText = $"Enter new {fieldName.ToLower()}";
            txt_input.Focus();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            updatePanel.Visible = false;
            txt_input.Text = "";
            txt_pass.Text = "";
            currentUpdateField = "";
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_input.Text) || string.IsNullOrWhiteSpace(txt_pass.Text))
            {
                MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (currentUpdateField == "Username")
            {
                if (txt_input.Text.Length < 3 || txt_input.Text.Length > 20)
                {
                    MessageBox.Show("Username must be between 3 and 20 characters.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (_userBL.UsernameExists(txt_input.Text))
                {
                    MessageBox.Show("This username is already taken. Please choose another one.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (currentUpdateField == "Email" && !IsValidEmail(txt_input.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (currentUpdateField == "Phone" && !IsValidPhone(txt_input.Text))
            {
                MessageBox.Show("Please enter a valid phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _userDTO.Password = txt_pass.Text;
                bool passwordValid = _userBL.VerifyPassword(_userDTO);

                if (!passwordValid)
                {
                    MessageBox.Show("Incorrect password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int result = 0;
                switch (currentUpdateField)
                {
                    case "Username":
                        result = _userBL.UpdateUsername(_userDTO.Id, txt_input.Text);
                        break;
                    case "Email":
                        result = _customerBL.UpdateEmail(_userDTO.Id, txt_input.Text);
                        break;
                    case "Phone":
                        result = _customerBL.UpdatePhone(_userDTO.Id, txt_input.Text);
                        break;
                }

                if (result > 0)
                {
                    MessageBox.Show($"{currentUpdateField} updated successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUserInfo();
                    btn_cancel_Click(null, null);
                }
                else
                {
                    MessageBox.Show($"Failed to update {currentUpdateField.ToLower()}.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating information: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_delete_account_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to delete your account?\n\nThis action cannot be undone!",
                "Delete Account",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                var confirmResult = MessageBox.Show(
                    "Please confirm again. Your account will be permanently deleted.",
                    "Final Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        int deleteResult = _userBL.DeleteUser(_userDTO.Id);
                        if (deleteResult > 0)
                        {
                            MessageBox.Show("Your account has been deleted successfully.", "Account Deleted",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Exit();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete account. Please try again.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting account: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            string cleanPhone = phone.Replace(" ", "").Replace("-", "").Replace("+", "");
            return cleanPhone.All(char.IsDigit) && cleanPhone.Length >= 10;
        }
    }

    public class GradientButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color startColor = this.Enabled ? Color.FromArgb(102, 126, 234) : Color.Gray;
            Color endColor = this.Enabled ? Color.FromArgb(118, 75, 162) : Color.DarkGray;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, startColor, endColor, 45f))
            {
                e.Graphics.FillRoundedRectangle(brush, 0, 0, Width, Height, 8);
            }

            TextRenderer.DrawText(e.Graphics, this.Text, this.Font,
                this.ClientRectangle, this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }

    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush,
            float x, float y, float width, float height, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(x, y, radius, radius, 180, 90);
            path.AddArc(x + width - radius, y, radius, radius, 270, 90);
            path.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            path.AddArc(x, y + height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            graphics.FillPath(brush, path);
            path.Dispose();
        }
    }
}
