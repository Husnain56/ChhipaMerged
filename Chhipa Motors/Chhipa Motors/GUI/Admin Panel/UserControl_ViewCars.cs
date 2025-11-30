using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Chhipa_Motors.DTO;
using Chhipa_Motors.BL;
using Chhipa_Motors.GUI.InputBox;

namespace Chhipa_Motors.GUI.Admin_Panel
{
    public partial class UserControl_ViewCars : UserControl
    {
        private AdminBL _adminBL;
        private CarDTO _carDTO;

        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private DataGridView dgv_cars;
        private Button btn_update_price;
        private Button btn_update_stock;
        private Button btn_change_status;
        private Button btnRefresh;
        private Panel bottomPanel;

        public UserControl_ViewCars()
        {
            InitializeComponent();
            _adminBL = new AdminBL();
            _carDTO = new CarDTO();
            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScroll = true;

            InitializeCustomComponents();
            LoadCars();
        }

        private void InitializeCustomComponents()
        {
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Dock = DockStyle.Fill;

            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(102, 126, 234)
            };
            headerPanel.Paint += HeaderPanel_Paint;

            lblTitle = new Label
            {
                Text = "Manage Cars",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 25)
            };
            lblSubtitle = new Label
            {
                Text = "Update pricing, stock levels, and availability status",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 85)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            Panel dgvContainer = new Panel
            {
                Location = new Point(40, 160),
                Size = new Size(this.Width - 80, this.Height - 300),
                BackColor = Color.White,
                Padding = new Padding(15),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            dgv_cars = new DataGridView
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

            dgv_cars.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(102, 126, 234),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            dgv_cars.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5),
                Font = new Font("Segoe UI", 10)
            };

            dgv_cars.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 252),
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5)
            };

            dgv_cars.SelectionChanged += dgv_cars_SelectionChanged;
            dgv_cars.DataBindingComplete += Dgv_cars_DataBindingComplete;
            dgv_cars.CellFormatting += Dgv_cars_CellFormatting;

            dgvContainer.Controls.Add(dgv_cars);

            bottomPanel = new Panel
            {
                Height = 90,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(40, 20, 40, 20)
            };

            btnRefresh = new Button
            {
                Text = "🔄 REFRESH",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(102, 126, 234),
                Size = new Size(150, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.White,
                Dock = DockStyle.Left
            };
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(102, 126, 234);
            btnRefresh.FlatAppearance.BorderSize = 2;
            btnRefresh.Click += (s, e) => { LoadCars(); MessageBox.Show("Cars list refreshed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); };
            btnRefresh.MouseEnter += (s, e) => btnRefresh.BackColor = Color.FromArgb(240, 243, 250);
            btnRefresh.MouseLeave += (s, e) => btnRefresh.BackColor = Color.White;

            btn_update_price = new GradientButton
            {
                Text = "💰 UPDATE PRICE",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(180, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn_update_price.Location = new Point(this.Width - 590, 40);
            btn_update_price.Anchor = AnchorStyles.Right;
            btn_update_price.FlatAppearance.BorderSize = 0;
            btn_update_price.Click += btn_update_price_Click;

            btn_update_stock = new GradientButton
            {
                Text = "📦 UPDATE STOCK",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(180, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn_update_stock.Location = new Point(this.Width - 390, 40);
            btn_update_stock.Anchor = AnchorStyles.Right;
            btn_update_stock.FlatAppearance.BorderSize = 0;
            btn_update_stock.Click += btn_update_stock_Click;

            btn_change_status = new StatusButton
            {
                Text = "⏸ PAUSE AVAILABILITY",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(180, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn_change_status.Location = new Point(this.Width - 190, 40);
            btn_change_status.Anchor = AnchorStyles.Right;
            btn_change_status.FlatAppearance.BorderSize = 0;
            btn_change_status.Click += btn_change_status_Click;

            bottomPanel.Controls.AddRange(new Control[] {
                btnRefresh, btn_update_price, btn_update_stock, btn_change_status
            });

            this.Resize += (s, e) =>
            {
                btn_update_price.Location = new Point(this.Width - 630, 30);
                btn_update_stock.Location = new Point(this.Width - 430, 30);
                btn_change_status.Location = new Point(this.Width - 230, 30);
            };

            this.Controls.AddRange(new Control[] { headerPanel, dgvContainer, bottomPanel });
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

        private void Dgv_cars_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgv_cars.Columns.Count > 0)
            {
                if (dgv_cars.Columns.Contains("CarID"))
                {
                    dgv_cars.Columns["CarID"].HeaderText = "Car ID";
                    dgv_cars.Columns["CarID"].Width = 80;
                }

                if (dgv_cars.Columns.Contains("Manufacturer"))
                {
                    dgv_cars.Columns["Manufacturer"].HeaderText = "Manufacturer";
                    dgv_cars.Columns["Manufacturer"].Width = 130;
                }

                if (dgv_cars.Columns.Contains("CarName"))
                {
                    dgv_cars.Columns["CarName"].HeaderText = "Car Model";
                    dgv_cars.Columns["CarName"].Width = 150;
                }

                if (dgv_cars.Columns.Contains("ModelYear"))
                {
                    dgv_cars.Columns["ModelYear"].HeaderText = "Year";
                    dgv_cars.Columns["ModelYear"].Width = 80;
                }

                if (dgv_cars.Columns.Contains("Price"))
                {
                    dgv_cars.Columns["Price"].HeaderText = "Price";
                    dgv_cars.Columns["Price"].DefaultCellStyle.Format = "C0";
                    dgv_cars.Columns["Price"].Width = 120;
                }

                if (dgv_cars.Columns.Contains("Stock"))
                {
                    dgv_cars.Columns["Stock"].HeaderText = "Stock";
                    dgv_cars.Columns["Stock"].Width = 80;
                }

                if (dgv_cars.Columns.Contains("Active"))
                {
                    dgv_cars.Columns["Active"].HeaderText = "Status";
                    dgv_cars.Columns["Active"].Width = 100;
                }
            }
        }

        private void Dgv_cars_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgv_cars.Columns[e.ColumnIndex].Name == "Active" && e.Value != null)
            {
                string status = e.Value.ToString();

                if (status == "True")
                {
                    e.Value = "Active";
                    e.CellStyle.BackColor = Color.FromArgb(220, 237, 200);
                    e.CellStyle.ForeColor = Color.FromArgb(51, 105, 30);
                    e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
                else
                {
                    e.Value = "Paused";
                    e.CellStyle.BackColor = Color.FromArgb(255, 235, 238);
                    e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                    e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }
            }

            if (dgv_cars.Columns[e.ColumnIndex].Name == "Stock" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int stock))
                {
                    if (stock == 0)
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                    else if (stock <= 5)
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(176, 111, 0);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                }
            }
        }

        public void LoadCars()
        {
            try
            {
                dgv_cars.DataSource = _adminBL.GetAllCars();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading cars: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_update_price_Click(object sender, EventArgs e)
        {
            if (dgv_cars.CurrentRow == null)
            {
                MessageBox.Show("Please select a car first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _carDTO.CarID = dgv_cars.CurrentRow.Cells["CarID"].Value.ToString();
            string currentPrice = dgv_cars.CurrentRow.Cells["Price"].Value.ToString();
            string carName = dgv_cars.CurrentRow.Cells["CarName"].Value.ToString();

            using (InputDialog input = new InputDialog($"Update price for {carName}\nCurrent Price: {currentPrice}\n\nEnter New Price:"))
            {
                if (input.ShowDialog() == DialogResult.OK)
                {
                    string userInput = input.InputText.Trim();

                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        MessageBox.Show("Price cannot be empty.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!decimal.TryParse(userInput, out decimal newPrice) || newPrice <= 0)
                    {
                        MessageBox.Show("Please enter a valid positive number.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _carDTO.Price = userInput;

                    try
                    {
                        int row_affected = _adminBL.updateCarPrice(_carDTO);
                        if (row_affected > 0)
                        {
                            MessageBox.Show($"Price updated successfully to {newPrice:C0}!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCars();
                        }
                        else
                        {
                            MessageBox.Show("Price update failed.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating price: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_update_stock_Click(object sender, EventArgs e)
        {
            if (dgv_cars.CurrentRow == null)
            {
                MessageBox.Show("Please select a car first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _carDTO.CarID = dgv_cars.CurrentRow.Cells["CarID"].Value.ToString();
            string currentStock = dgv_cars.CurrentRow.Cells["Stock"].Value.ToString();
            string carName = dgv_cars.CurrentRow.Cells["CarName"].Value.ToString();

            using (InputDialog input = new InputDialog($"Update stock for {carName}\nCurrent Stock: {currentStock}\n\nEnter New Stock:"))
            {
                if (input.ShowDialog() == DialogResult.OK)
                {
                    string userInput = input.InputText.Trim();

                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        MessageBox.Show("Stock cannot be empty.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!int.TryParse(userInput, out int newStock) || newStock < 0)
                    {
                        MessageBox.Show("Please enter a valid non-negative number.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _carDTO.Stock = userInput;

                    try
                    {
                        int row_affected = _adminBL.updateCarStock(_carDTO);
                        if (row_affected > 0)
                        {
                            MessageBox.Show($"Stock updated successfully to {newStock} units!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadCars();
                        }
                        else
                        {
                            MessageBox.Show("Stock update failed.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating stock: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_change_status_Click(object sender, EventArgs e)
        {
            if (dgv_cars.CurrentRow == null)
            {
                MessageBox.Show("Please select a car first.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string currentStatus = dgv_cars.CurrentRow.Cells["Status"].Value.ToString();
            string carName = dgv_cars.CurrentRow.Cells["CarName"].Value.ToString();
            _carDTO.CarID = dgv_cars.CurrentRow.Cells["CarID"].Value.ToString();

            DialogResult result;

            if (currentStatus == "False")
            {
                result = MessageBox.Show(
                    $"Resume availability for {carName}?\n\nThis will make the car visible to customers.",
                    "Resume Availability",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                _carDTO.Status = "True";
            }
            else
            {
                result = MessageBox.Show(
                    $"Pause availability for {carName}?\n\nThis will hide the car from customers.",
                    "Pause Availability",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                _carDTO.Status = "False";
            }

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (_adminBL.changeCarStatus(_carDTO) > 0)
                    {
                        string statusText = currentStatus == "False" ? "resumed" : "paused";
                        MessageBox.Show($"Availability {statusText} successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadCars();
                    }
                    else
                    {
                        MessageBox.Show("Failed to change status.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error changing status: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgv_cars_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_cars.CurrentRow == null) return;

            string currentStatus = dgv_cars.CurrentRow.Cells["Status"].Value.ToString();

            if (currentStatus == "False")
            {
                btn_change_status.Text = "▶ RESUME AVAILABILITY";
            }
            else
            {
                btn_change_status.Text = "⏸ PAUSE AVAILABILITY";
            }
        }
    }

    public class StatusButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color startColor = this.Enabled ?
                Color.FromArgb(255, 152, 0) : Color.Gray;
            Color endColor = this.Enabled ?
                Color.FromArgb(251, 192, 45) : Color.DarkGray;

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

}