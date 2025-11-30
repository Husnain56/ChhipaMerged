using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;
using Chhipa_Motors.Factory;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI.Booking_Form
{
    public partial class BookingForm : Form
    {
        private CustomerBL _customerBL;
        private BookingBL _bookingBL;
        private CarProduct _carProduct;
        private UserDTO _userDTO;

        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel carDetailsPanel;
        private PictureBox pb_carImage;
        private Label lblCarName;
        private Label lblManufacturer;
        private Label lblPrice;
        private Panel customerDetailsPanel;
        private TextBox txt_fullName;
        private TextBox txt_email;
        private TextBox txt_phone;
        private TextBox txt_address;
        private TextBox txt_city;
        private Button btn_confirm;
        private Button btn_cancel;
        

        public BookingForm(CarProduct carProduct, Image carImage,UserDTO dto)
        {
            _customerBL = new CustomerBL();
            _bookingBL = new BookingBL();
            _carProduct = carProduct;

            _userDTO = dto;

            InitializeComponent();
            InitializeComponents();
            InitializeCustomComponents(carImage);
            LoadCarDetails();
        }

        private void InitializeComponents()
        {
            this.Text = "Book Your Dream Car - Chhipa Motors";
            this.Size = new Size(1100, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void InitializeCustomComponents(Image carImage)
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = Color.FromArgb(102, 126, 234)
            };
            headerPanel.Paint += HeaderPanel_Paint;

            lblTitle = new Label
            {
                Text = "Complete Your Booking",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 20)
            };

            lblSubtitle = new Label
            {
                Text = "Fill in your details to reserve this vehicle",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(40, 65)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            carDetailsPanel = new Panel
            {
                Location = new Point(40, 130),
                Size = new Size(1010, 320),
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            carDetailsPanel.Paint += CardPanel_Paint;

            pb_carImage = new PictureBox
            {
                Location = new Point(30, 30),
                Size = new Size(530, 250),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = carImage,
                BackColor = Color.FromArgb(248, 249, 252)
            };
            pb_carImage.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, pb_carImage.ClientRectangle,
                    Color.FromArgb(220, 220, 220), ButtonBorderStyle.Solid);
            };

            lblCarName = new Label
            {
                Text = "Loading...",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true,
                Location = new Point(590, 40),
                MaximumSize = new Size(380, 0)
            };

            lblManufacturer = new Label
            {
                Text = "Manufacturer",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.FromArgb(102, 126, 234),
                AutoSize = true,
                Location = new Point(590, 100)
            };

            lblPrice = new Label
            {
                Text = "$0",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 105, 30),
                AutoSize = true,
                Location = new Point(590, 150)
            };

            Label lblPriceDesc = new Label
            {
                Text = "Total Price",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(120, 120, 120),
                AutoSize = true,
                Location = new Point(590, 200)
            };

            carDetailsPanel.Controls.AddRange(new Control[] {
                pb_carImage, lblCarName, lblManufacturer, lblPrice, lblPriceDesc
            });

            customerDetailsPanel = new Panel
            {
                Location = new Point(40, 470),
                Size = new Size(1010, 220),
                BackColor = Color.White,
                Padding = new Padding(20)
            };
            customerDetailsPanel.Paint += CardPanel_Paint;

            Label lblCustomerTitle = new Label
            {
                Text = "Your Information",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = true,
                Location = new Point(30, 20)
            };

            Label lblFullName = new Label
            {
                Text = "Full Name *",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(30, 70)
            };

            txt_fullName = new TextBox
            {
                Location = new Point(30, 95),
                Size = new Size(300, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblEmail = new Label
            {
                Text = "Email Address *",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(360, 70)
            };

            txt_email = new TextBox
            {
                Location = new Point(360, 95),
                Size = new Size(300, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblPhone = new Label
            {
                Text = "Phone Number *",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(690, 70)
            };

            txt_phone = new TextBox
            {
                Location = new Point(690, 95),
                Size = new Size(280, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblAddress = new Label
            {
                Text = "Address *",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(30, 145)
            };

            txt_address = new TextBox
            {
                Location = new Point(30, 170),
                Size = new Size(630, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblCity = new Label
            {
                Text = "City *",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(690, 145)
            };

            txt_city = new TextBox
            {
                Location = new Point(690, 170),
                Size = new Size(280, 35),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };

            customerDetailsPanel.Controls.AddRange(new Control[] {
                lblCustomerTitle, lblFullName, txt_fullName, lblEmail, txt_email,
                lblPhone, txt_phone, lblAddress, txt_address, lblCity, txt_city
            });

            Panel buttonsPanel = new Panel
            {
                Location = new Point(40, 710),
                Size = new Size(1010, 60),
                BackColor = Color.Transparent
            };

            btn_cancel = new Button
            {
                Text = "✕ CANCEL",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(198, 40, 40),
                Location = new Point(0, 0),
                Size = new Size(180, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                BackColor = Color.White
            };
            btn_cancel.FlatAppearance.BorderColor = Color.FromArgb(198, 40, 40);
            btn_cancel.FlatAppearance.BorderSize = 2;
            btn_cancel.Click += Btn_cancel_Click;
            btn_cancel.MouseEnter += (s, e) => btn_cancel.BackColor = Color.FromArgb(255, 245, 245);
            btn_cancel.MouseLeave += (s, e) => btn_cancel.BackColor = Color.White;

            btn_confirm = new GradientButton
            {
                Text = "✓ CONFIRM BOOKING",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(800, 0),
                Size = new Size(220, 50),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btn_confirm.FlatAppearance.BorderSize = 0;
            btn_confirm.Click += Btn_confirm_Click;

            buttonsPanel.Controls.AddRange(new Control[] { btn_cancel, btn_confirm });

            this.Controls.AddRange(new Control[] {
                headerPanel, carDetailsPanel, customerDetailsPanel, buttonsPanel
            });
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

        private void CardPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(shadowBrush, 2, 2, panel.Width - 2, panel.Height - 2);
            }

            using (GraphicsPath path = GetRoundedRectPath(new Rectangle(0, 0, panel.Width - 1, panel.Height - 1), 12))
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

        private void LoadCarDetails()
        {
            try
            {
                lblCarName.Text = _carProduct.CarName;
                lblManufacturer.Text = _carProduct.Manufacturer;
                lblPrice.Text = $"${_carProduct.Price:N0}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading car details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to cancel this booking?",
                "Cancel Booking",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void Btn_confirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_fullName.Text) ||
                string.IsNullOrWhiteSpace(txt_email.Text) ||
                string.IsNullOrWhiteSpace(txt_phone.Text) ||
                string.IsNullOrWhiteSpace(txt_address.Text) ||
                string.IsNullOrWhiteSpace(txt_city.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidEmail(txt_email.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidPhone(txt_phone.Text))
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CustomerDTO customerDTO = new CustomerDTO
                {
                    CustomerID = _userDTO.Id, 
                    FullName = txt_fullName.Text,
                    Email = txt_email.Text,
                    PhoneNumber = txt_phone.Text,
                    Address = txt_address.Text,
                    City = txt_city.Text
                };
                int customerResult = _customerBL.addorUpdateCustomer(customerDTO);
                if (customerResult <= 0)
                {
                    MessageBox.Show("Failed to save customer information.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Saved customer information.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                BookingDTO bookingDTO = new BookingDTO
                {
                    UserID = _userDTO.Id,
                    CarID = _carProduct.CarID.ToString(),  
                    Status = "Pending"
                };

                int bookingResult = _bookingBL.CreateBooking(bookingDTO);
                if (bookingResult > 0)
                {
                    MessageBox.Show($"Booking confirmed successfully!\n\n" +
                        $"Vehicle: {_carProduct.CarName}\n" +      
                        $"Price: $${_carProduct.Price:N0}\n\n" +     
                        $"We will contact you shortly at {txt_phone.Text}",
                        "Booking Confirmed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create booking. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing booking: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}