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

namespace Chhipa_Motors.GUI.Admin_Panel
{   
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            pnl_dynamic.AfterNavigate += pnl_AfterNavigate;
            pnl_dynamic.AddContentToView("Create Admin", new UserControl_CreateAdmin());
            pnl_dynamic.ContentBackColor = Color.FromArgb(8, 6, 16);
            pnl_dynamic.TitleSeparatorColor = Color.FromArgb(8, 6, 16);
            pnl_dynamic.TitleBackColor = Color.FromArgb(8, 6, 16);
            pnl_dynamic.Dock = DockStyle.None;
        }
        private void pnl_AfterNavigate(object sender, SiticoneContentPanel.NavigationEventArgs e)
        {
            pnl_dynamic.AddContentToView("Account", new UserControl_Account());
            pnl_dynamic.AddContentToView("View Users", new UserControl_Users());
            pnl_dynamic.AddContentToView("Create Admin", new UserControl_CreateAdmin());
            pnl_dynamic.AddContentToView("Booked Cars", new UserControl_BookedCars());
            pnl_dynamic.AddContentToView("View Cars", new UserControl_ViewCars());
            pnl_dynamic.AddContentToView("View Sales Record", new UserControl_SalesRecord());
        }
    }
}
