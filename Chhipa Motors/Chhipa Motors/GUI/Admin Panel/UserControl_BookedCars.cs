using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;
using Chhipa_Motors.GUI.InputBox;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI.Admin_Panel
{
    public partial class UserControl_BookedCars : UserControl
    {
        private AdminBL _adminBL;
        private BookingDTO _bookDTO;
        private BookingContext _bookingContext;

        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private DataGridView dgvBookings;
        private Button btn_booking_status;
        private Button btn_booking_cancel;
        private Button btnRefresh;
        private Panel bottomPanel;

        public UserControl_BookedCars()
        {
            InitializeComponent();
            _adminBL = new AdminBL();
            _bookDTO = new BookingDTO();

            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScroll = true;
            InitializeCustomComponents();
            RefreshDataGridView();
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
                Text = "Booking Management",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 25)
            };

            // Subtitle
            lblSubtitle = new Label
            {
                Text = "Manage customer bookings and update order status",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 83)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            // DataGridView Container
            Panel dgvContainer = new Panel
            {
                Location = new Point(40, 130),
                Size = new Size(this.Width - 80, this.Height - 300),
                BackColor = Color.White,
                Padding = new Padding(15),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // DataGridView
            dgvBookings = new DataGridView
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

            // Header style
            dgvBookings.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(102, 126, 234),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            // Cell style
            dgvBookings.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5),
                Font = new Font("Segoe UI", 10)
            };

            // Alternating rows
            dgvBookings.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 252),
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5)
            };

            dgvBookings.SelectionChanged += dgv_booked_cars_SelectionChanged;
            dgvBookings.CellFormatting += DgvBookings_CellFormatting;
            dgvBookings.DataBindingComplete += DgvBookings_DataBindingComplete;

            dgvContainer.Controls.Add(dgvBookings);

            // Bottom Panel
            bottomPanel = new Panel
            {
                Height = 90,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(40, 20, 40, 20)
            };

            // Refresh Button
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
            btnRefresh.Click += BtnRefresh_Click;
            btnRefresh.MouseEnter += (s, e) => btnRefresh.BackColor = Color.FromArgb(240, 243, 250);
            btnRefresh.MouseLeave += (s, e) => btnRefresh.BackColor = Color.White;

            // Status Button
            btn_booking_status = new GradientButton
            {
                Text = "Update Status",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btn_booking_status.Location = new Point(this.Width - 240, 20);
            btn_booking_status.Anchor = AnchorStyles.Right;
            btn_booking_status.FlatAppearance.BorderSize = 0;
            btn_booking_status.Click += btn_booking_status_Click;

            // Cancel Button
            btn_booking_cancel = new CancelButton
            {
                Text = "✕ CANCEL BOOKING",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false,
                Visible = false
            };
            btn_booking_cancel.Location = new Point(this.Width - 460, 20);
            btn_booking_cancel.Anchor = AnchorStyles.Right;
            btn_booking_cancel.FlatAppearance.BorderSize = 0;
            btn_booking_cancel.Click += btn_booking_cancel_Click;

            bottomPanel.Controls.AddRange(new Control[] {
                btnRefresh, btn_booking_status, btn_booking_cancel
            });

            // Reposition buttons on resize
            this.Resize += (s, e) =>
            {
                btn_booking_status.Location = new Point(this.Width - 280, 20);
                btn_booking_cancel.Location = new Point(this.Width - 500, 20);
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

        private void DgvBookings_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvBookings.Columns.Count > 0)
            {
                // Customize columns
                if (dgvBookings.Columns.Contains("BookingID"))
                {
                    dgvBookings.Columns["BookingID"].HeaderText = "Booking #";
                    dgvBookings.Columns["BookingID"].Width = 100;
                }

                if (dgvBookings.Columns.Contains("UserID"))
                {
                    dgvBookings.Columns["UserID"].HeaderText = "Customer ID";
                    dgvBookings.Columns["UserID"].Width = 100;
                }

                if (dgvBookings.Columns.Contains("Username"))
                {
                    dgvBookings.Columns["Username"].HeaderText = "Customer";
                    dgvBookings.Columns["Username"].Width = 130;
                }

                if (dgvBookings.Columns.Contains("CarID"))
                {
                    dgvBookings.Columns["CarID"].HeaderText = "CarID";
                    dgvBookings.Columns["CarID"].Width = 130;
                }

                if (dgvBookings.Columns.Contains("Manufacturer"))
                {
                    dgvBookings.Columns["Manufacturer"].HeaderText = "Manufacturer";
                    dgvBookings.Columns["Manufacturer"].Width = 130;
                }

                if (dgvBookings.Columns.Contains("CarName"))
                {
                    dgvBookings.Columns["CarName"].HeaderText = "Car Model";
                    dgvBookings.Columns["CarName"].Width = 180;
                }

                if (dgvBookings.Columns.Contains("BookingDate"))
                {
                    dgvBookings.Columns["BookingDate"].HeaderText = "Booking Date";
                    dgvBookings.Columns["BookingDate"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    dgvBookings.Columns["BookingDate"].Width = 130;
                }

                if (dgvBookings.Columns.Contains("Status"))
                {
                    dgvBookings.Columns["Status"].HeaderText = "Status";
                    dgvBookings.Columns["Status"].Width = 120;
                }

                if (dgvBookings.Columns.Contains("UpdatedAt"))
                {
                    dgvBookings.Columns["UpdatedAt"].HeaderText = "Last Updated";
                    dgvBookings.Columns["UpdatedAt"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    dgvBookings.Columns["UpdatedAt"].Width = 130;
                }

                if (dgvBookings.Columns.Contains("AdminNote"))
                {
                    dgvBookings.Columns["AdminNote"].HeaderText = "Admin Notes";
                    dgvBookings.Columns["AdminNote"].Width = 200;
                }
            }
        }

        private void DgvBookings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvBookings.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();

                switch (status)
                {
                    case "Pending":
                        e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                        e.CellStyle.ForeColor = Color.FromArgb(176, 111, 0);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case "Processing":
                        e.CellStyle.BackColor = Color.FromArgb(227, 242, 253);
                        e.CellStyle.ForeColor = Color.FromArgb(1, 87, 155);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case "Shipping":
                    case "Shipped":
                        e.CellStyle.BackColor = Color.FromArgb(232, 234, 246);
                        e.CellStyle.ForeColor = Color.FromArgb(69, 90, 100);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case "Delivered":
                        e.CellStyle.BackColor = Color.FromArgb(220, 237, 200);
                        e.CellStyle.ForeColor = Color.FromArgb(51, 105, 30);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case "Cancelled":
                        e.CellStyle.BackColor = Color.FromArgb(255, 235, 238);
                        e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                }
            }
        }

        public void RefreshDataGridView()
        {
            LoadBookings(false);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings(true);
        }

        private void LoadBookings(bool showMessage)
        {
            try
            {
                dgvBookings.DataSource = _adminBL.GetBookedCars();

                if (showMessage)
                {
                    MessageBox.Show("Bookings refreshed!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgv_booked_cars_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBookings.SelectedRows.Count == 0) return;

            var row = dgvBookings.SelectedRows[0];
            int bookingId = Convert.ToInt32(row.Cells["BookingID"].Value);
            string currentStatus = row.Cells["Status"].Value.ToString();
            

            _bookingContext = new BookingContext(currentStatus)
            {
                BookingId = bookingId
            };

            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_bookingContext == null) return;

            var state = _bookingContext.CurrentState;

            btn_booking_status.Text = state.ButtonText;
            btn_booking_status.Enabled = state.GetNextState() != null;

            btn_booking_cancel.Visible = state.CanCancel;
            btn_booking_cancel.Enabled = state.CanCancel;
        }

        private void btn_booking_status_Click(object sender, EventArgs e)
        {
            if (_bookingContext == null) return;

            using (var inputDialog = new InputDialog("Enter admin note:"))
            {
                if (inputDialog.ShowDialog() == DialogResult.OK)
                {
                    string adminNote = inputDialog.InputText;

                    _bookingContext.TransitionToNextState();

                    UpdateBookingInDatabase(_bookingContext.BookingId,
                        _bookingContext.CurrentState.StateName, adminNote);

                    LoadBookings(false);
                    UpdateUI();

                    MessageBox.Show(
                        $"Status updated to {_bookingContext.CurrentState.StateName}",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private void btn_booking_cancel_Click(object sender, EventArgs e)
        {
            if (_bookingContext == null) return;

            try
            {
                var result = MessageBox.Show(
                    "Cancel this booking?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    using (var inputDialog = new InputDialog("Cancellation reason:"))
                    {
                        if (inputDialog.ShowDialog() == DialogResult.OK)
                        {
                            _bookingContext.Cancel();

                            UpdateBookingInDatabase(
                                _bookingContext.BookingId,
                                _bookingContext.CurrentState.StateName,
                                inputDialog.InputText
                            );

                            LoadBookings(false);
                            UpdateUI();
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateBookingInDatabase(int id, string status, string note)
        {
            _bookDTO.BookingID = id.ToString();
            _bookDTO.Status = status;
            _bookDTO.AdminNote = note;

            if (_adminBL.updateBookingStatus(_bookDTO) > 0)
            {
                MessageBox.Show("Booking updated successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Booking update failed.", "Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class CancelButton : Button
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color startColor = this.Enabled ?
                Color.FromArgb(220, 53, 69) : Color.Gray;
            Color endColor = this.Enabled ?
                Color.FromArgb(200, 35, 51) : Color.DarkGray;

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