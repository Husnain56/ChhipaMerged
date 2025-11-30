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
    public partial class CreateAccount : UserControl
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        private void btn_clear_create_Click(object sender, EventArgs e)
        {
            txt_create_pass.Clear();
            txt_create_email.Clear();
            txt_name_create.Clear();
            txt_phone_create.Clear();
            txt_name_create.Focus();
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if(txt_name_create.Text == "" || txt_create_email.Text == "" || txt_create_pass.Text == "" || txt_phone_create.Text == "")
            {
                MessageBox.Show("Please fill all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
