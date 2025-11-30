using SiticoneNetCoreUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Chhipa_Motors.BL;
using Chhipa_Motors.Factory;
using Chhipa_Motors.GUI.Booking_Form;
using Chhipa_Motors.DTO;

namespace Chhipa_Motors.GUI.Car_Cards
{
    public partial class UserControl_Lamborghini : UserControl
    {
        private CarBL _carBL;
        private LamborghiniCreator _lamborghiniFactory;
        UserDTO _userDTO;

        public UserControl_Lamborghini(UserDTO dto)
        {
            InitializeComponent();
            _carBL = new CarBL();
            _lamborghiniFactory = new LamborghiniCreator();
            HandleEvents();
            _userDTO = dto;
        }

        private void HoverEnter(object sender, EventArgs e)
        {
            SiticoneContainer container = null;
            if (sender is SiticoneContainer c)
                container = c;
            else if (sender is Control child && child.Parent is SiticoneContainer parent)
                container = parent;
            if (container != null)
            {
                container.BorderColor1 = Color.White;
                container.BorderColor2 = Color.White;
                container.BorderWidth = 2;
            }
        }

        private void HoverLeave(object sender, EventArgs e)
        {
            SiticoneContainer container = null;
            if (sender is SiticoneContainer c)
                container = c;
            else if (sender is Control child && child.Parent is SiticoneContainer parent)
                container = parent;
            if (container != null)
            {
                container.BorderColor1 = Color.Black;
                container.BorderColor2 = Color.Black;
                container.BorderWidth = 0;
            }
        }

        private void UserControl_Lamborghini_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is SiticoneContainer container)
                {
                    container.MouseEnter += HoverEnter;
                    container.MouseLeave += HoverLeave;

                    foreach (Control child in container.Controls)
                    {
                        child.MouseEnter += HoverEnter;
                        child.MouseLeave += HoverLeave;
                    }
                }
            }

            LoadLamborghiniPricesDirect();
        }

        private void LoadLamborghiniPricesDirect()
        {
            try
            {
                var lamborghiniCars = _carBL.GetCarsByManufacturer("Lamborghini");

                var carLookup = new Dictionary<string, (SiticoneLabel priceLabel, SiticoneButtonAdvanced bookButton)>
                {
                    { "Urus SE", (lbl_p_UrusSE, btn_book_UrusSE) },
                    { "Urus Performance", (lbl_p_UrusPerformance, btn_book_UrusP) },
                    { "Temerario", (lbl_p_Temerario, btn_book_Temerario) },
                    { "Revuelto", (btn_p_revuelto, btn_book_Revuelto) }
                };

                foreach (var car in lamborghiniCars)
                {
                    if (carLookup.ContainsKey(car.CarName))
                    {
                        var (priceLabel, bookButton) = carLookup[car.CarName];

                        decimal price = decimal.Parse(car.Price);
                        priceLabel.Text = $"{price:N0}";
                        priceLabel.Tag = car.CarID;

                        bookButton.Tag = car.CarID;

                        int stock = int.Parse(car.Stock);

                        bool isActive = false;
                        if (!string.IsNullOrEmpty(car.Status))
                        {
                            string status = car.Status.Trim().ToLower();
                            isActive = status == "active" || status == "1" || status == "true";
                        }

                        bool shouldEnable = (stock > 0 && isActive);

                        bookButton.Enabled = shouldEnable;
                        bookButton.Cursor = shouldEnable ? Cursors.Hand : Cursors.No;

                        if (shouldEnable)
                        {
                            bookButton.Text = "Book Vehicle";
                        }
                        else if (stock <= 0)
                        {
                            bookButton.Text = "Out of Stock";
                        }
                        else if (!isActive)
                        {
                            bookButton.Text = "Unavailable";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Lamborghini prices: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========================
        // Booking Button Click Handlers using Factory Pattern
        // ========================
        public void HandleEvents()
        {
            btn_book_UrusSE.Click += btn_book_UrusSE_Click;
            btn_book_UrusP.Click += btn_book_UrusP_Click;
            btn_book_Temerario.Click += btn_book_Temerario_Click;
            btn_book_Revuelto.Click += btn_book_Revuelto_Click;
        }
        private void btn_book_UrusSE_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _lamborghiniFactory.CreateCar("Urus SE", pb_urus_se.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Refresh prices after booking
                    LoadLamborghiniPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_UrusP_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _lamborghiniFactory.CreateCar("Urus Performance", pb_urus_p.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadLamborghiniPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_Temerario_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _lamborghiniFactory.CreateCar("Temerario", pb_temerario.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadLamborghiniPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_Revuelto_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _lamborghiniFactory.CreateCar("Revuelto", pb_revuelto.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadLamborghiniPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}