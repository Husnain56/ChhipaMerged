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
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_clear_create_Click(object sender, EventArgs e)
        {
            txt_email.Clear();
            txt_pass.Clear();
            txt_email.Focus();
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if(txt_email.Text == "" || txt_pass.Text == "")
            {
                MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
