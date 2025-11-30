using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI.Admin_Panel
{
    public partial class UserControl_Users : UserControl
    {
        AdminBL _adminBL;
        UserDTO _userDTO;
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel filterPanel;
        private RadioButton radioButton_Admins;
        private RadioButton radioButton_Users;
        private RadioButton radioButton_Customers;
        private DataGridView dgv_users;
        private Button btn_deleteUser;
        private Button btn_refresh;
        private Panel bottomPanel;
        private Label lblTotalCount;

        public UserControl_Users()
        {
            _adminBL = new AdminBL();
            _userDTO = new UserDTO();

            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = Color.FromArgb(245, 247, 250);

            InitializeCustomComponents();
            LoadAdmins();
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
                Text = "User Management",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 25)
            };

            lblSubtitle = new Label
            {
                Text = "Manage admins, users, and customer accounts",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 83)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            filterPanel = new Panel
            {
                Location = new Point(40, 140),
                Size = new Size(1010, 70),
                BackColor = Color.White
            };
            filterPanel.Paint += FilterPanel_Paint;

            Label lblFilter = new Label
            {
                Text = "Filter by Type:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(30, 23)
            };

            radioButton_Admins = CreateRadioButton("👨‍💼 Admins", 180, 20, true);
            radioButton_Admins.CheckedChanged += radioButton_Admins_CheckedChanged;

            radioButton_Users = CreateRadioButton("👤 Users", 350, 20, false);
            radioButton_Users.CheckedChanged += radioButton_Users_CheckedChanged;

            radioButton_Customers = CreateRadioButton("🛍️ Customers", 500, 20, false);
            radioButton_Customers.CheckedChanged += radioButton_Customers_CheckedChanged;

            lblTotalCount = new Label
            {
                Text = "Total: 0",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(102, 126, 234),
                AutoSize = true,
                Location = new Point(880, 23)
            };

            filterPanel.Controls.AddRange(new Control[] {
                lblFilter, radioButton_Admins, radioButton_Users, radioButton_Customers, lblTotalCount
            });

            Panel dgvContainer = new Panel
            {
                Location = new Point(40, 230),
                Size = new Size(1010, 400),
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            dgvContainer.Paint += DgvContainer_Paint;

            dgv_users = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 45,
                RowTemplate = { Height = 40 },
                EnableHeadersVisualStyles = false,
                GridColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Segoe UI", 10)
            };

            dgv_users.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(102, 126, 234),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            dgv_users.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5),
                Font = new Font("Segoe UI", 10)
            };

            dgv_users.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 252),
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5)
            };

            dgv_users.SelectionChanged += Dgv_users_SelectionChanged;
            dgv_users.DataBindingComplete += Dgv_users_DataBindingComplete;

            dgvContainer.Controls.Add(dgv_users);

            bottomPanel = new Panel
            {
                Location = new Point(40, 650),
                Size = new Size(1010, 60),
                BackColor = Color.Transparent
            };

            btn_refresh = new Button
            {
                Text = "🔄 REFRESH",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(102, 126, 234),
                Location = new Point(0, 10),
                Size = new Size(150, 45),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.White
            };
            btn_refresh.FlatAppearance.BorderColor = Color.FromArgb(102, 126, 234);
            btn_refresh.FlatAppearance.BorderSize = 2;
            btn_refresh.Click += Btn_refresh_Click;
            btn_refresh.MouseEnter += (s, e) => btn_refresh.BackColor = Color.FromArgb(240, 243, 250);
            btn_refresh.MouseLeave += (s, e) => btn_refresh.BackColor = Color.White;

            btn_deleteUser = new DeleteButton
            {
                Text = "🗑️ DELETE USER",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(860, 10),
                Size = new Size(150, 45),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false,
                Visible = false
            };
            btn_deleteUser.FlatAppearance.BorderSize = 0;
            btn_deleteUser.Click += btn_deleteUser_Click;

            bottomPanel.Controls.AddRange(new Control[] { btn_refresh, btn_deleteUser });

            this.Controls.AddRange(new Control[] { headerPanel, filterPanel, dgvContainer, bottomPanel });
        }

        private RadioButton CreateRadioButton(string text, int x, int y, bool isChecked)
        {
            RadioButton rb = new RadioButton
            {
                Text = text,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true,
                Location = new Point(x, y),
                Checked = isChecked,
                Cursor = Cursors.Hand
            };
            return rb;
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

        private void FilterPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

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

        private void DgvContainer_Paint(object sender, PaintEventArgs e)
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

        private void LoadAdmins()
        {
            dgv_users.DataSource = _adminBL.getAdmins();
            btn_deleteUser.Visible = false;
            UpdateTotalCount();
        }

        private void LoadUsers()
        {
            dgv_users.DataSource = _adminBL.getUsers();
            btn_deleteUser.Visible = true;
            UpdateTotalCount();
        }

        private void LoadCustomers()
        {
            dgv_users.DataSource = _adminBL.getCustomers();
            btn_deleteUser.Visible = false;
            UpdateTotalCount();
        }

        private void UpdateTotalCount()
        {
            int count = dgv_users.Rows.Count;
            string type = radioButton_Admins.Checked ? "Admins" :
                         radioButton_Users.Checked ? "Users" : "Customers";
            lblTotalCount.Text = $"Total {type}: {count}";
        }

        private void Dgv_users_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgv_users.Columns.Count > 0)
            {
                if (dgv_users.Columns.Contains("Password"))
                {
                    dgv_users.Columns["Password"].Visible = false;
                }

                if (dgv_users.Columns.Contains("UserID"))
                {
                    dgv_users.Columns["UserID"].HeaderText = "User ID";
                    dgv_users.Columns["UserID"].Width = 100;
                }

                if (dgv_users.Columns.Contains("CustomerID"))
                {
                    dgv_users.Columns["CustomerID"].HeaderText = "Customer ID";
                    dgv_users.Columns["CustomerID"].Width = 120;
                }

                if (dgv_users.Columns.Contains("Username"))
                {
                    dgv_users.Columns["Username"].HeaderText = "Username";
                    dgv_users.Columns["Username"].Width = 150;
                }

                if (dgv_users.Columns.Contains("FullName"))
                {
                    dgv_users.Columns["FullName"].HeaderText = "Full Name";
                    dgv_users.Columns["FullName"].Width = 180;
                }

                if (dgv_users.Columns.Contains("Email"))
                {
                    dgv_users.Columns["Email"].HeaderText = "Email";
                    dgv_users.Columns["Email"].Width = 200;
                }

                if (dgv_users.Columns.Contains("PhoneNumber"))
                {
                    dgv_users.Columns["PhoneNumber"].HeaderText = "Phone";
                    dgv_users.Columns["PhoneNumber"].Width = 130;
                }

                if (dgv_users.Columns.Contains("Address"))
                {
                    dgv_users.Columns["Address"].HeaderText = "Address";
                    dgv_users.Columns["Address"].Width = 200;
                }

                if (dgv_users.Columns.Contains("City"))
                {
                    dgv_users.Columns["City"].HeaderText = "City";
                    dgv_users.Columns["City"].Width = 120;
                }

                if (dgv_users.Columns.Contains("Role"))
                {
                    dgv_users.Columns["Role"].HeaderText = "Role";
                    dgv_users.Columns["Role"].Width = 100;
                }
            }

            UpdateTotalCount();
        }

        private void Dgv_users_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButton_Users.Checked && dgv_users.SelectedRows.Count > 0)
            {
                btn_deleteUser.Enabled = true;
            }
            else
            {
                btn_deleteUser.Enabled = false;
            }
        }

        private void radioButton_Admins_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Admins.Checked)
            {
                LoadAdmins();
            }
        }

        private void radioButton_Users_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Users.Checked)
            {
                LoadUsers();
            }
        }

        private void radioButton_Customers_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_Customers.Checked)
            {
                LoadCustomers();
            }
        }

        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            if (radioButton_Admins.Checked)
            {
                LoadAdmins();
            }
            else if (radioButton_Users.Checked)
            {
                LoadUsers();
            }
            else if (radioButton_Customers.Checked)
            {
                LoadCustomers();
            }

            MessageBox.Show("User list refreshed!", "Refresh",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_deleteUser_Click(object sender, EventArgs e)
        {
            if (dgv_users.CurrentRow == null) return;

            _userDTO.Id = dgv_users.CurrentRow.Cells["UserID"].Value.ToString();

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this user?\n\nThis action cannot be undone.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_adminBL.DeleteUser(_userDTO) > 0)
                    {
                        MessageBox.Show("User deleted successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (radioButton_Users.Checked)
                        {
                            LoadUsers();
                        }
                        else if (radioButton_Customers.Checked)
                        {
                            LoadCustomers();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete user. Please try again.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public class DeleteButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color startColor = this.Enabled ?
                Color.FromArgb(220, 53, 69) : Color.Gray;
            Color endColor = this.Enabled ?
                Color.FromArgb(200, 35, 51) : Color.DarkGray;

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