using Chhipa_Motors.GUI.Admin_Panel;
using Chhipa_Motors.GUI.Car_Cards;
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
using Chhipa_Motors.GUI.Menu_Pages;
using Chhipa_Motors.DTO;

namespace Chhipa_Motors.GUI
{
    public partial class MainForm : Form
    {
        UserDTO _userDTO;
        public MainForm(string ID)
        {
            InitializeComponent();
            setStates();
            _userDTO = new UserDTO();
            _userDTO.Id = ID;
            InitializeMenuCloseButton();
        }
        private void InitializeMenuCloseButton()
        {
            Button btnCloseMenu = new Button();
            btnCloseMenu.Name = "btn_close_menu";
            btnCloseMenu.Size = new Size(35, 35);
            btnCloseMenu.Location = new Point(220, 10);
            btnCloseMenu.Text = "✕";
            btnCloseMenu.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            btnCloseMenu.ForeColor = Color.White;
            btnCloseMenu.BackColor = Color.Transparent;
            btnCloseMenu.FlatStyle = FlatStyle.Flat;
            btnCloseMenu.FlatAppearance.BorderSize = 0;
            btnCloseMenu.Cursor = Cursors.Hand;
            btnCloseMenu.TabStop = false;
            btnCloseMenu.MouseEnter += (s, e) =>
            {
                btnCloseMenu.BackColor = Color.FromArgb(50, 255, 255, 255);
                btnCloseMenu.FlatAppearance.BorderColor = Color.White;
                btnCloseMenu.FlatAppearance.BorderSize = 1;
            };

            btnCloseMenu.MouseLeave += (s, e) =>
            {
                btnCloseMenu.BackColor = Color.Transparent;
                btnCloseMenu.FlatAppearance.BorderSize = 0;
            };

            btnCloseMenu.Click += btn_close_menu_Click_1;
            container_menu.Panel1.Controls.Add(btnCloseMenu);
            btnCloseMenu.BringToFront();
        }
        public void setStates()
        {
            pb_wallpaper.BringToFront();
            pb_porsche_logo.BringToFront();
            pb_porsche_mf.BringToFront();
            pb_lambo_mf.BringToFront();
            pb_MacLaren_mf.BringToFront();
            pb_nissan_mf.BringToFront();
            lbl_msg.BringToFront();
            pb_menu.BringToFront();
            container_menu.BringToFront();
            lbl_test_drive.BringToFront();
            

            container_menu.Hide();

            pb_Acc.Parent = pb_wallpaper;
            lbl_msg.Parent = pnl_main;
            pb_menu.Parent = pb_wallpaper;
            container_menu.Parent = pb_wallpaper;

            container_menu.SplitterDistance = 40;

        }

        private void LoadContent(UserControl page)
        {
            //page.Dock = DockStyle.Fill;
            pnl_dynamic_menu.Controls.Clear();
            pnl_dynamic_menu.Controls.Add(page);
        }

        private void pb_mf_MouseEnter(object sender, EventArgs e)
        {
            var pic = sender as SiticonePictureBox;
            pic.BorderColor = Color.Silver;
            pic.BorderWidth = 2;

        }

        private void pb_mf_MouseLeave(object sender, EventArgs e)
        {
            var pic = sender as SiticonePictureBox;
            pic.BorderWidth = 0;
        }
        private void pnl_AfterNavigate(object sender, SiticoneContentPanel.NavigationEventArgs e)
        {
            pnl_dynamic_menu.AddContentToView("Manufacturers", new Manufacturers_menu());
            pnl_dynamic_menu.AddContentToView("Purchases", new PurchasedCars(_userDTO.Id));
            pnl_dynamic_menu.AddContentToView("Bookings", new CustomerBookings(_userDTO.Id));
            pnl_dynamic_menu.AddContentToView("Account Settings", new UserInfo(_userDTO));
        }
        private void btn_menu_Click(object sender, EventArgs e)
        {
            container_menu.Show();
            pb_menu.Hide();
            pnl_main.AutoScroll = false;
            pnl_dynamic_menu.AfterNavigate += pnl_AfterNavigate;
            navbar_menu.SelectedItem = navbar_menu.Items[0];
            pnl_dynamic_menu.AddContentToView("Manufacturers", new Manufacturers_menu());
            pb_Acc.Hide();
        }

        private void pb_acc_Click(object sender, EventArgs e)
        {
            pb_Acc.Hide();
            container_menu.Show();
            pb_menu.Hide();
            pnl_main.AutoScroll = false;
            pnl_dynamic_menu.AfterNavigate += pnl_AfterNavigate;
            pnl_dynamic_menu.AddContentToView("Account Settings", new UserInfo(_userDTO));
            navbar_menu.SelectedItem = navbar_menu.Items[3];
        }

        private void pb_blur_screen_Click(object sender, EventArgs e)
        {
            container_menu.Hide();
            pb_menu.Show();
            pb_Acc.Show();
            pnl_main.AutoScroll = true;
            this.pb_wallpaper.Focus();
        }

        private void btn_close_menu_Click_1(object sender, EventArgs e)
        {
            container_menu.Hide();
            pb_menu.Show();
            pb_Acc.Show();
            pnl_main.AutoScroll = true;
            pb_wallpaper.Focus();
        }

        private void btn_menu_manufacturer_list_Click(object sender, EventArgs e)
        {
            LoadContent(new Manufacturers_menu());
        }

        private void btn_menu_acc_Click(object sender, EventArgs e)
        {
            LoadContent(new UserInfo(_userDTO));
        }
        private void manufacturerCard_Click(object sender, EventArgs e)
        {
            string manufacturer = ((Control)sender).Tag.ToString();
            TestForm manufacturerForm = new TestForm(manufacturer, _userDTO);
            manufacturerForm.FormClosed += (s, args) => this.Show();
            this.Hide();
            manufacturerForm.ShowDialog();
        }

        private void lbl_test_drive_Click(object sender, EventArgs e)
        {
            Chhipa_Motors.Game TestDrive = new Chhipa_Motors.Game();
            TestDrive.FormClosed += (s, args) => this.Show();
            this.Hide();
            TestDrive.ShowDialog();
        }

        //private void pb_menu_btn_MouseEnter(object sender, EventArgs e)
        //{
        //    btn_close_menu.BorderColor = Color.White;
        //    btn_close_menu.BorderWidth = 2;

        //}

        //private void pb_menu_btn_MouseLeave(object sender, EventArgs e)
        //{
        //    btn_close_menu.BorderColor = Color.Transparent; 
        //    btn_close_menu.BorderWidth = 0;
        //}
    }
}
