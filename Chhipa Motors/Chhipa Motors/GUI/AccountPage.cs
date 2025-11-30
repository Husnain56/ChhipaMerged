using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chhipa_Motors.GUI
{
    public partial class AccountPage : UserControl
    {
        public AccountPage()
        {
            InitializeComponent();
            container_user_login.Hide();
        }

        public void LoadContent(UserControl page)
        {
            page.Dock = DockStyle.Fill;
            container_user_login.Controls.Clear();
            container_user_login.Controls.Add(page);
            container_user_login.Show();
        }
        private void btn_new_user_Click(object sender, EventArgs e)
        {
            LoadContent(new CreateAccount());
        }

        private void btn_exist_user_Click(object sender, EventArgs e)
        {
            LoadContent(new Login());
        }
    }
}
