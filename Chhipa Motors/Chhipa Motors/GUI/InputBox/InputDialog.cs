using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chhipa_Motors.GUI.InputBox
{
    public class InputDialog:Form
    {
        private Label lblMessage;
        private TextBox txtInput;
        private Button btnOK;
        private Button btnCancel;

        public string InputText => txtInput.Text;

        public InputDialog(string message, string title = "Input Required")
        {
            this.Text = title;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Width = 350;
            this.Height = 170;

            lblMessage = new Label()
            {
                Left = 15,
                Top = 10,
                Width = 300,
                Text = message
            };
            this.Controls.Add(lblMessage);

            txtInput = new TextBox()
            {
                Left = 15,
                Top = 40,
                Width = 300
            };
            this.Controls.Add(txtInput);

            btnOK = new Button()
            {
                Text = "OK",
                Left = 150,
                Width = 75,
                Top = 80,
                DialogResult = DialogResult.OK
            };
            this.Controls.Add(btnOK);

            btnCancel = new Button()
            {
                Text = "Cancel",
                Left = 240,
                Width = 75,
                Top = 80,
                DialogResult = DialogResult.Cancel
            };
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
    }
}
