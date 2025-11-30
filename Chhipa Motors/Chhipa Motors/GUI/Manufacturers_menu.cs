using Chhipa_Motors.Properties;
using SiticoneNetCoreUI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI
{
    public partial class Manufacturers_menu : UserControl
    {
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private FlowLayoutPanel manufacturersPanel;

        public Manufacturers_menu()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.AutoSize = false;
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.FromArgb(245, 247, 250);

            HideDesignerPictureBoxes();

            InitializeCustomComponents();
        }

        private void HideDesignerPictureBoxes()
        {
            if (pb_porschelogo != null)
                pb_porschelogo.Visible = false;

            if (pb_mcLarenLogo != null)
                pb_mcLarenLogo.Visible = false;

            if (pb_lambologo != null)
                pb_lambologo.Visible = false;

            if (pb_nissanlogo != null)
                pb_nissanlogo.Visible = false;
        }

        private void InitializeCustomComponents()
        {
            headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = Color.FromArgb(102, 126, 234)
            };
            headerPanel.Paint += HeaderPanel_Paint;

            lblTitle = new Label
            {
                Text = "Our Manufacturers",
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 35)
            };

            lblSubtitle = new Label
            {
                Text = "Browse vehicles by your favorite automotive brands",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(50, 100)
            };

            headerPanel.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });

            manufacturersPanel = new FlowLayoutPanel
            {
                Location = new Point(30, 180),
                Size = new Size(this.Width - 60, this.Height - 210),
                AutoScroll = true,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };

            CreateManufacturerCards();

            this.Controls.Add(headerPanel);
            this.Controls.Add(manufacturersPanel);

            this.Resize += (s, e) =>
            {
                manufacturersPanel.Size = new Size(this.Width - 60, this.Height - 210);
            };
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

        private void CreateManufacturerCards()
        { 
            CreateManufacturerCard(pb_porschelogo?.Image, "Porsche", "porsche");
            CreateManufacturerCard(pb_mcLarenLogo?.Image, "McLaren", "mclaren");
            CreateManufacturerCard(pb_lambologo?.Image, "Lamborghini", "lamborghini");
            CreateManufacturerCard(pb_nissanlogo?.Image, "Nissan", "nissan");
        }

        private void CreateManufacturerCard(Image logoImage, string manufacturerName, string tag)
        {
            Panel cardPanel = new Panel
            {
                Width = 250,
                Height = 280,
                BackColor = Color.White,
                Margin = new Padding(15),
                Cursor = Cursors.Hand,
                Tag = tag
            };
            cardPanel.Paint += CardPanel_Paint;

            SiticonePictureBox pictureBox = new SiticonePictureBox
            {
                Size = new Size(180, 180),
                Location = new Point(35, 30),
                Image = logoImage,
                BackColor = Color.White,
                BorderWidth = 0,
                Tag = tag
            };

            pictureBox.MouseEnter += pb_mf_MouseEnter;
            pictureBox.MouseLeave += pb_mf_MouseLeave;
            pictureBox.Click += ManufacturerCard_Click;

            Label lblName = new Label
            {
                Text = manufacturerName,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 51, 51),
                AutoSize = false,
                Size = new Size(250, 30),
                Location = new Point(0, 220),
                TextAlign = ContentAlignment.MiddleCenter,
                Tag = tag
            };
            lblName.Click += ManufacturerCard_Click;

            Label lblViewCars = new Label
            {
                Text = "View Models →",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(102, 126, 234),
                AutoSize = false,
                Size = new Size(250, 25),
                Location = new Point(0, 245),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                Tag = tag
            };
            lblViewCars.Click += ManufacturerCard_Click;

            cardPanel.Controls.Add(pictureBox);
            cardPanel.Controls.Add(lblName);
            cardPanel.Controls.Add(lblViewCars);

            cardPanel.MouseEnter += (s, e) =>
            {
                cardPanel.BackColor = Color.FromArgb(248, 249, 252);
                lblViewCars.ForeColor = Color.FromArgb(118, 75, 162);
            };

            cardPanel.MouseLeave += (s, e) =>
            {
                cardPanel.BackColor = Color.White;
                lblViewCars.ForeColor = Color.FromArgb(102, 126, 234);
            };

            cardPanel.Click += ManufacturerCard_Click;

            manufacturersPanel.Controls.Add(cardPanel);
        }

        private void CardPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(shadowBrush, 3, 3, panel.Width - 3, panel.Height - 3);
            }

            using (GraphicsPath path = GetRoundedRectPath(new Rectangle(0, 0, panel.Width - 1, panel.Height - 1), 12))
            {
                using (SolidBrush brush = new SolidBrush(panel.BackColor))
                {
                    e.Graphics.FillPath(brush, path);
                }

                using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 1))
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

        private void pb_mf_MouseEnter(object sender, EventArgs e)
        {
            var pic = sender as SiticonePictureBox;
            pic.BorderColor = Color.FromArgb(102, 126, 234);
            pic.BorderWidth = 3;
        }

        private void pb_mf_MouseLeave(object sender, EventArgs e)
        {
            var pic = sender as SiticonePictureBox;
            pic.BorderColor = Color.Transparent;
            pic.BorderWidth = 0;
        }

        private void ManufacturerCard_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            string manufacturer = control?.Tag?.ToString();

            if (!string.IsNullOrEmpty(manufacturer))
            {
                MessageBox.Show($"Loading {manufacturer} models...", "Manufacturer Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}