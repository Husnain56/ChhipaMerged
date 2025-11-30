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
    public partial class UserControl_McLaren : UserControl
    {
        private CarBL _carBL;
        private McLarenCreator _mcLarenFactory;
        UserDTO _userDTO;
        public UserControl_McLaren(UserDTO dto)
        {
            InitializeComponent();
            _carBL = new CarBL();
            _mcLarenFactory = new McLarenCreator();
            HandleEvents();
            _userDTO = dto;
        }

        private void HandleEvents()
        {
            btn_book_GTS.Click += btn_book_GTS_Click;
            btn_book_750S.Click += btn_book_750S_Click;
            btn_book_765LT_Spider.Click += btn_book_765LT_Spider_Click;
            btn_book_ArturaSpider.Click += btn_book_ArturaSpider_Click;
            btn_book_750S_Spider.Click += btn_book_750S_Spider_Click;
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

        private void UserControl_McLaren_Load(object sender, EventArgs e)
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

            LoadMcLarenPricesDirect();
        }

        private void LoadMcLarenPricesDirect()
        {
            try
            {
                var mcLarenCars = _carBL.GetCarsByManufacturer("McLaren");

                var carLookup = new Dictionary<string, (SiticoneLabel priceLabel, SiticoneButtonAdvanced bookButton)>
                {
                    { "McLaren GTS", (lbl_p_GTS, btn_book_GTS) },
                    { "McLaren 750S", (lbl_p_750S, btn_book_750S) },
                    { "765LT Spider", (lbl_p_765LT_Spider, btn_book_765LT_Spider) },
                    { "Artura Spider", (lbl_p_ArturaSpider, btn_book_ArturaSpider) },
                    { "750S SPIDER", (lbl_p_750S_Spider, btn_book_750S_Spider) }
                };

                foreach (var car in mcLarenCars)
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
                MessageBox.Show($"Error loading McLaren prices: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========================
        // Booking Button Click Handlers using Factory Pattern
        // ========================

        private void btn_book_GTS_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _mcLarenFactory.CreateCar("McLaren GTS", pb_GTS.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMcLarenPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_750S_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _mcLarenFactory.CreateCar("McLaren 750S", pb_750S.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMcLarenPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_765LT_Spider_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _mcLarenFactory.CreateCar("765LT Spider", pb_765LT_Spider.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMcLarenPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_ArturaSpider_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _mcLarenFactory.CreateCar("Artura Spider", pb_artura_spider.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMcLarenPricesDirect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_750S_Spider_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _mcLarenFactory.CreateCar("750S SPIDER", pb_750S_Spider.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadMcLarenPricesDirect();
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