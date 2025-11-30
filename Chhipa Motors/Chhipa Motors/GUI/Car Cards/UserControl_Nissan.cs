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
    public partial class UserControl_Nissan : UserControl
    {
        private CarBL _carBL;
        private NissanCreator _nissanFactory;
        private UserDTO _userDTO;
        public UserControl_Nissan(UserDTO dto)
        {
            InitializeComponent();
            _carBL = new CarBL();
            _nissanFactory = new NissanCreator();
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

        private void UserControl_Nissan_Load(object sender, EventArgs e)
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

            LoadNissanPricesDirect();
            AttachButtonEvents();
        }

        private void AttachButtonEvents()
        {
            // Attach click event handlers to booking buttons
            btn_book_GTR.Click += btn_book_GTR_Click;
            btn_book_Z.Click += btn_book_Z_Click;
            btn_book_leaf.Click += btn_book_leaf_Click;
            btn_book_frontier_PRO4X.Click += btn_book_frontier_PRO4X_Click;
        }

        private void LoadNissanPricesDirect()
        {
            try
            {
                var nissanCars = _carBL.GetCarsByManufacturer("Nissan");

                var carLookup = new Dictionary<string, (SiticoneLabel priceLabel, SiticoneButtonAdvanced bookButton)>
                {
                    { "Nissan GT-R", (lbl_p_GTR, btn_book_GTR) },
                    { "Nissan Z", (lbl_p_Z, btn_book_Z) },
                    { "Nissan Leaf", (lbl_p_leaf, btn_book_leaf) },
                    { "Frontier Pro-4X", (lbl_p_FrontierPro4X, btn_book_frontier_PRO4X) }
                };

                foreach (var car in nissanCars)
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
                MessageBox.Show($"Error loading Nissan prices: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========================
        // Booking Button Click Handlers using Factory Pattern
        // ========================

        private void btn_book_GTR_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _nissanFactory.CreateCar("Nissan GT-R", pb_GTR.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadNissanPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_Z_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _nissanFactory.CreateCar("Nissan Z", pb_NissanZ.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadNissanPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_leaf_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _nissanFactory.CreateCar("Nissan Leaf", pb_Leaf.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadNissanPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_frontier_PRO4X_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _nissanFactory.CreateCar("Frontier Pro-4X", pb_frontierpro.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadNissanPricesDirect();
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