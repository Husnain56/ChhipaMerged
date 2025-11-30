namespace Chhipa_Motors.GUI.Admin_Panel
{
    partial class UserControl_Users
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)dgv_users).BeginInit();
            SuspendLayout();
          
            // 
            // UserControl_Users
            // 
            AccessibleDescription = "";
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Indigo;
            Controls.Add(dgv_users);
            Controls.Add(radioButton_Customers);
            Controls.Add(radioButton_Users);
            Controls.Add(radioButton_Admins);
            Controls.Add(btn_deleteUser);
            Name = "UserControl_Users";
            Size = new Size(1157, 720);
            ((System.ComponentModel.ISupportInitialize)dgv_users).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
