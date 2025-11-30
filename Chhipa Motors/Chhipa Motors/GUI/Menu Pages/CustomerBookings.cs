using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;

namespace Chhipa_Motors.GUI.Menu_Pages
{
    public partial class CustomerBookings : UserControl
    {
        CustomerBL _customerBL;
        UserDTO _userDTO;
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private DataGridView dgb_bookings;
        private Button btnCancelBooking;
        private Button btnRefresh;
        private Panel bottomPanel;

        public CustomerBookings(string id)
        {
            _customerBL = new CustomerBL();
            _userDTO = new UserDTO();
            _userDTO.Id = id;
            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoScroll = true;

            InitializeCustomComponents();
            LoadBookings();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "My Bookings - Chhipa Motors";
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
                Text = "Your Bookings",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 25)
            };

            lblSubtitle = new Label
            {
                Text = "Manage and track all your car bookings",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 83)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            Panel dgvContainer = new Panel
            {
                Location = new Point(40, 160),
                Size = new Size(1010, 450),
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            dgb_bookings = new DataGridView
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

            dgb_bookings.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(102, 126, 234),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            dgb_bookings.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5),
                Font = new Font("Segoe UI", 10)
            };

            dgb_bookings.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(248, 249, 252),
                ForeColor = Color.FromArgb(51, 51, 51),
                SelectionBackColor = Color.FromArgb(102, 126, 234),
                SelectionForeColor = Color.White,
                Padding = new Padding(10, 5, 10, 5)
            };

            dgb_bookings.SelectionChanged += Dgb_bookings_SelectionChanged;
            dgb_bookings.CellFormatting += Dgb_bookings_CellFormatting;
            dgb_bookings.DataBindingComplete += Dgb_bookings_DataBindingComplete;

            dgvContainer.Controls.Add(dgb_bookings);

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

            btnCancelBooking = new GradientButtonC
            {
                Text = "✕ CANCEL BOOKING",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(860, 10),
                Size = new Size(150, 45),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnCancelBooking.FlatAppearance.BorderSize = 0;
            btnCancelBooking.Click += BtnCancelBooking_Click;

            bottomPanel.Controls.AddRange(new Control[] { btnRefresh, btnCancelBooking });

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

        public void LoadBookings()
        {
            try
            {
                dgb_bookings.DataSource = _customerBL.fetchBookings(_userDTO);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dgb_bookings_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            if (dgb_bookings.Columns.Count > 0)
            {
                if (dgb_bookings.Columns.Contains("CarID"))
                {
                    dgb_bookings.Columns["CarID"].Visible = false;
                }

                if (dgb_bookings.Columns.Contains("BookingID"))
                {
                    dgb_bookings.Columns["BookingID"].HeaderText = "Booking ID";
                    dgb_bookings.Columns["BookingID"].Width = 100;
                }

                if (dgb_bookings.Columns.Contains("CarName"))
                {
                    dgb_bookings.Columns["CarName"].HeaderText = "Car Model";
                    dgb_bookings.Columns["CarName"].Width = 180;
                }

                if (dgb_bookings.Columns.Contains("Manufacturer"))
                {
                    dgb_bookings.Columns["Manufacturer"].HeaderText = "Manufacturer";
                    dgb_bookings.Columns["Manufacturer"].Width = 130;
                }

                if (dgb_bookings.Columns.Contains("BookingDate"))
                {
                    dgb_bookings.Columns["BookingDate"].HeaderText = "Booking Date";
                    dgb_bookings.Columns["BookingDate"].DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt";
                    dgb_bookings.Columns["BookingDate"].Width = 160;
                }

                if (dgb_bookings.Columns.Contains("Status"))
                {
                    dgb_bookings.Columns["Status"].HeaderText = "Status";
                    dgb_bookings.Columns["Status"].Width = 110;
                }

                if (dgb_bookings.Columns.Contains("UpdatedAt"))
                {
                    dgb_bookings.Columns["UpdatedAt"].HeaderText = "Last Updated";
                    dgb_bookings.Columns["UpdatedAt"].DefaultCellStyle.Format = "MMM dd, yyyy hh:mm tt";
                    dgb_bookings.Columns["UpdatedAt"].Width = 160;
                }

                if (dgb_bookings.Columns.Contains("AdminNote"))
                {
                    dgb_bookings.Columns["AdminNote"].HeaderText = "Notes";
                    dgb_bookings.Columns["AdminNote"].Width = 180;
                }

                int displayOrder = 0;

                if (dgb_bookings.Columns.Contains("BookingID"))
                    dgb_bookings.Columns["BookingID"].DisplayIndex = displayOrder++;

                if (dgb_bookings.Columns.Contains("CarName"))
                    dgb_bookings.Columns["CarName"].DisplayIndex = displayOrder++;

                if (dgb_bookings.Columns.Contains("Manufacturer"))
                    dgb_bookings.Columns["Manufacturer"].DisplayIndex = displayOrder++;

                if (dgb_bookings.Columns.Contains("BookingDate"))
                    dgb_bookings.Columns["BookingDate"].DisplayIndex = displayOrder++;

                if (dgb_bookings.Columns.Contains("Status"))
                    dgb_bookings.Columns["Status"].DisplayIndex = displayOrder++;

                if (dgb_bookings.Columns.Contains("UpdatedAt"))
                    dgb_bookings.Columns["UpdatedAt"].DisplayIndex = displayOrder++;

                if (dgb_bookings.Columns.Contains("AdminNote"))
                    dgb_bookings.Columns["AdminNote"].DisplayIndex = displayOrder++;
            }
        }

        private void Dgb_bookings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgb_bookings.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();

                switch (status)
                {
                    case "Pending":
                        e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                        e.CellStyle.ForeColor = Color.FromArgb(176, 111, 0);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case "Confirmed":
                        e.CellStyle.BackColor = Color.FromArgb(227, 242, 253);
                        e.CellStyle.ForeColor = Color.FromArgb(1, 87, 155);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case "Completed":
                        e.CellStyle.BackColor = Color.FromArgb(220, 237, 200);
                        e.CellStyle.ForeColor = Color.FromArgb(51, 105, 30);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    case "Cancelled":
                        e.CellStyle.BackColor = Color.FromArgb(255, 235, 238);
                        e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                    default:
                        e.CellStyle.BackColor = Color.FromArgb(240, 240, 240);
                        e.CellStyle.ForeColor = Color.FromArgb(100, 100, 100);
                        e.CellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                        break;
                }
            }
        }

        private void Dgb_bookings_SelectionChanged(object sender, EventArgs e)
        {
            if (dgb_bookings.SelectedRows.Count > 0)
            {
                var row = dgb_bookings.SelectedRows[0];

                if (row.Cells["Status"].Value != null)
                {
                    string status = row.Cells["Status"].Value.ToString();
                    btnCancelBooking.Enabled = (status == "Pending");
                }
                else
                {
                    btnCancelBooking.Enabled = false;
                }
            }
            else
            {
                btnCancelBooking.Enabled = false;
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadBookings();
            MessageBox.Show("Bookings refreshed!", "Refresh",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnCancelBooking_Click(object sender, EventArgs e)
        {
            if (dgb_bookings.SelectedRows.Count == 0) return;


            var result = MessageBox.Show(
                "Are you sure you want to cancel this booking?",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                var row = dgb_bookings.SelectedRows[0];
                int bookingId = Convert.ToInt32(row.Cells["BookingID"].Value);

                try
                {
                    BookingDTO bookingDTO = new BookingDTO
                    {
                        BookingID = bookingId.ToString(),
                        Status = "Cancelled",
                        AdminNote = "Cancelled by customer"
                    };

                    if (_customerBL.cancelBooking(bookingDTO) > 0)
                    {
                        MessageBox.Show("Booking cancelled successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookings();
                    }
                    else
                    {
                        MessageBox.Show("Booking cancellation failed!", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error cancelling booking: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
    public class GradientButtonC : Button
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

    public static class GraphicsExtensionsC
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