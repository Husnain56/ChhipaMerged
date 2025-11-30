using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;

namespace Chhipa_Motors.GUI.Menu_Pages
{
    public partial class PurchasedCars : UserControl
    {
        CustomerBL _customerBL;
        AdminBL _adminBL;
        UserDTO _userDTO;
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private DataGridView dgv_PurchasedCars;
        private Button btnRefresh;
        private Button btnViewDetails;
        private Panel bottomPanel;
        private Label lblTotalPurchases;

        public PurchasedCars(string userId)
        {
            _customerBL = new CustomerBL();
            _adminBL = new AdminBL();
            _userDTO = new UserDTO();
            _userDTO.Id = userId;

            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScroll = true;

            InitializeCustomComponents();
            LoadPurchasedCars();
        }

        private void InitializeCustomComponents()
        {
            this.Size = new Size(1100, 740);
            this.BackColor = Color.FromArgb(245, 247, 250);

            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(102, 126, 234)
            };
            headerPanel.Paint += HeaderPanel_Paint;

            lblTitle = new Label
            {
                Text = "My Purchased Vehicles",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 25)
            };

            lblSubtitle = new Label
            {
                Text = "View and manage all your purchased vehicles",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 83)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            Panel statsPanel = new Panel
            {
                Location = new Point(40, 140),
                Size = new Size(1010, 50),
                BackColor = Color.White
            };
            statsPanel.Paint += StatsPanel_Paint;

            lblTotalPurchases = new Label
            {
                Text = "Total Purchases: 0",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(102, 126, 234),
                AutoSize = true,
                Location = new Point(20, 13)
            };

            statsPanel.Controls.Add(lblTotalPurchases);

            Panel dgvContainer = new Panel
            {
                Location = new Point(40, 210),
                Size = new Size(1010, 400),
                BackColor = Color.White,
                Padding = new Padding(15)
            };
            dgvContainer.Paint += DgvContainer_Paint;

            dgv_PurchasedCars = new DataGridView
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

            dgv_PurchasedCars.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(102, 126, 234),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            dgv_PurchasedCars.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5),
                Font = new Font("Segoe UI", 10)
            };

            dgv_PurchasedCars.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 252),
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5)
            };

            dgv_PurchasedCars.SelectionChanged += Dgv_PurchasedCars_SelectionChanged;
            dgv_PurchasedCars.CellFormatting += Dgv_PurchasedCars_CellFormatting;
            dgv_PurchasedCars.DataBindingComplete += Dgv_PurchasedCars_DataBindingComplete;

            dgvContainer.Controls.Add(dgv_PurchasedCars);

            bottomPanel = new Panel
            {
                Location = new Point(40, 630),
                Size = new Size(1010, 60),
                BackColor = Color.Transparent
            };

            btnRefresh = new Button
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
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(102, 126, 234);
            btnRefresh.FlatAppearance.BorderSize = 2;
            btnRefresh.Click += BtnRefresh_Click;
            btnRefresh.MouseEnter += (s, e) => btnRefresh.BackColor = Color.FromArgb(240, 243, 250);
            btnRefresh.MouseLeave += (s, e) => btnRefresh.BackColor = Color.White;

            btnViewDetails = new GradientButton
            {
                Text = "📋 VIEW DETAILS",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(860, 10),
                Size = new Size(150, 45),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnViewDetails.FlatAppearance.BorderSize = 0;
            btnViewDetails.Click += BtnViewDetails_Click;

            bottomPanel.Controls.AddRange(new Control[] { btnRefresh, btnViewDetails });

            this.Controls.AddRange(new Control[] { headerPanel, statsPanel, dgvContainer, bottomPanel });
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

        private void StatsPanel_Paint(object sender, PaintEventArgs e)
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

        public void LoadPurchasedCars()
        {
            try
            {
                dgv_PurchasedCars.DataSource = _customerBL.fetchPurchasedCars(_userDTO);
                UpdateTotalPurchases();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchased vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateTotalPurchases()
        {
            int totalCount = dgv_PurchasedCars.Rows.Count;
            lblTotalPurchases.Text = $"Total Purchases: {totalCount}";
        }

        private void Dgv_PurchasedCars_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgv_PurchasedCars.Columns.Count > 0)
            {
                if (dgv_PurchasedCars.Columns.Contains("CarID"))
                {
                    dgv_PurchasedCars.Columns["CarID"].Visible = false;
                }

                if (dgv_PurchasedCars.Columns.Contains("SaleID"))
                {
                    dgv_PurchasedCars.Columns["SaleID"].HeaderText = "Sale #";
                    dgv_PurchasedCars.Columns["SaleID"].Width = 100;
                }

                if (dgv_PurchasedCars.Columns.Contains("Manufacturer"))
                {
                    dgv_PurchasedCars.Columns["Manufacturer"].HeaderText = "Manufacturer";
                    dgv_PurchasedCars.Columns["Manufacturer"].Width = 150;
                }

                if (dgv_PurchasedCars.Columns.Contains("CarName"))
                {
                    dgv_PurchasedCars.Columns["CarName"].HeaderText = "Vehicle Model";
                    dgv_PurchasedCars.Columns["CarName"].Width = 250;
                }

                if (dgv_PurchasedCars.Columns.Contains("TotalAmount"))
                {
                    dgv_PurchasedCars.Columns["TotalAmount"].HeaderText = "Purchase Price";
                    dgv_PurchasedCars.Columns["TotalAmount"].DefaultCellStyle.Format = "C0";
                    dgv_PurchasedCars.Columns["TotalAmount"].Width = 150;
                }

                if (dgv_PurchasedCars.Columns.Contains("SaleDate"))
                {
                    dgv_PurchasedCars.Columns["SaleDate"].HeaderText = "Purchase Date";
                    dgv_PurchasedCars.Columns["SaleDate"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    dgv_PurchasedCars.Columns["SaleDate"].Width = 150;
                }
            }

            UpdateTotalPurchases();
        }

        private void Dgv_PurchasedCars_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv_PurchasedCars.Columns[e.ColumnIndex].Name == "TotalAmount" && e.Value != null)
            {
                e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(51, 105, 30);
            }
            if (dgv_PurchasedCars.Columns[e.ColumnIndex].Name == "SaleDate" && e.Value != null)
            {
                if (DateTime.TryParse(e.Value.ToString(), out DateTime saleDate))
                {
                    TimeSpan timeSincePurchase = DateTime.Now - saleDate;

                    if (timeSincePurchase.TotalDays <= 30)
                    {
                        e.CellStyle.BackColor = Color.FromArgb(220, 237, 200);
                        e.CellStyle.ForeColor = Color.FromArgb(51, 105, 30);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                }
            }
        }

        private void Dgv_PurchasedCars_SelectionChanged(object sender, EventArgs e)
        {
            btnViewDetails.Enabled = dgv_PurchasedCars.SelectedRows.Count > 0;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadPurchasedCars();
            MessageBox.Show("Purchased vehicles refreshed!", "Refresh",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgv_PurchasedCars.SelectedRows.Count == 0) return;

            var row = dgv_PurchasedCars.SelectedRows[0];

            string details = "Vehicle Purchase Details\n\n";

            foreach (DataGridViewCell cell in row.Cells)
            {
                if (dgv_PurchasedCars.Columns[cell.ColumnIndex].Visible)
                {
                    string columnName = dgv_PurchasedCars.Columns[cell.ColumnIndex].HeaderText;
                    string value = cell.Value?.ToString() ?? "N/A";
                    details += $"{columnName}: {value}\n";
                }
            }

            MessageBox.Show(details, "Purchase Details",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }

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