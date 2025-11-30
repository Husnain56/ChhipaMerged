using Chhipa_Motors.BL;
using Chhipa_Motors.DTO;
using Chhipa_Motors.Factory;
using Chhipa_Motors.GUI.Booking_Form;
using SiticoneNetCoreUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI.Car_Cards
{
    public partial class UserControl_Porsche : UserControl
    {
        CarBL _carBL;
        private PorscheCreator _porscheFactory;
        UserDTO _userDTO;
        public UserControl_Porsche(UserDTO dto)
        {
            InitializeComponent();
            _carBL = new CarBL();
            _porscheFactory = new PorscheCreator();
            LoadPorschePricesDirect();
            HandleButtonEvents();
            _userDTO = dto;
        }
        private void HandleButtonEvents()
        {;
            btn_book_TaycanTurbo.Click += btn_book_taycan_Click;
            btn_book_macan4.Click += btn_book_macan4_Click;
            btn_book_carrera4S.Click += btn_book_carrera4S_Click;
            btn_book_panamera.Click += btn_book_panamera_Click;
            btn_book_panamera4S.Click += btn_book_panamera4S_Click;
            btn_book_718Cayman.Click += btn_book_718Cayman_Click;   
        }
        private void btn_book_taycan_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _porscheFactory.CreateCar("Taycan Turbo GT", pb_taycan.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_carrera4S_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _porscheFactory.CreateCar("911 Carrera 4S", pb_carrera_4S.Image);
                BookingForm form = new BookingForm(car, car.CarImage,_userDTO);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_panamera_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _porscheFactory.CreateCar("Panamera", pb_panamera.Image);
                BookingForm form = new BookingForm(car, car.CarImage,_userDTO);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_panamera4S_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _porscheFactory.CreateCar("Panamera 4S E-Hybrid", pb_panamera_4S.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_book_macan4_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _porscheFactory.CreateCar("Macan 4 Electric", pb_macan4.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_book_718Cayman_Click(object sender, EventArgs e)
        {
            try
            {
                CarProduct car = _porscheFactory.CreateCar("718 Cayman GT4 RS", pb_718_cayman.Image);
                BookingForm form = new BookingForm(car, car.CarImage, _userDTO);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void UserControl_Porsche_Load(object sender, EventArgs e)
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
        }

        private void LoadPorschePricesDirect()
        {
            try
            {
                var porscheCars = _carBL.GetCarsByManufacturer("Porsche");

                var carLookup = new Dictionary<string, (SiticoneLabel priceLabel, SiticoneButtonAdvanced bookButton)>
        {
            { "Taycan Turbo GT", (lbl_p_TaycanTurboGT, btn_book_TaycanTurbo) },
            { "911 Carrera 4S", (lbl_p_911Carrera4S, btn_book_carrera4S) },
            { "Panamera", (lbl_p_Panamera, btn_book_panamera) },
            { "Panamera 4S E-Hybrid", (lbl_p_Panamera4SEHybrid, btn_book_panamera4S) },
            { "Macan 4 Electric", (lbl_p_Macan4Electric, btn_book_macan4) },
            { "718 Cayman GT4 RS", (lbl_p_718CaymanGT4RS, btn_book_718Cayman) }
        };

                foreach (var car in porscheCars)
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
                MessageBox.Show($"Error loading Porsche prices: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PorscheForm_Load(object sender, EventArgs e)
        {
            LoadPorschePricesDirect(); 
        }
    }
}
