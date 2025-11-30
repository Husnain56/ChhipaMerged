namespace Chhipa_Motors.GUI.Admin_Panel
{
    partial class UserControl_Account
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
            siticoneActivityButton1 = new SiticoneNetCoreUI.SiticoneActivityButton();
            SuspendLayout();
            // 
            // siticoneActivityButton1
            // 
            siticoneActivityButton1.ActivityDuration = 2000;
            siticoneActivityButton1.ActivityIndicatorColor = Color.White;
            siticoneActivityButton1.ActivityIndicatorSize = 4;
            siticoneActivityButton1.ActivityIndicatorSpeed = 100;
            siticoneActivityButton1.ActivityText = "Processing...";
            siticoneActivityButton1.AnimationEasing = SiticoneNetCoreUI.SiticoneActivityButton.AnimationEasingType.EaseOutQuad;
            siticoneActivityButton1.BackColor = Color.Transparent;
            siticoneActivityButton1.BorderColor = Color.FromArgb(10, 10, 10, 50);
            siticoneActivityButton1.BorderWidth = 2;
            siticoneActivityButton1.CornerRadiusBottomLeft = 5;
            siticoneActivityButton1.CornerRadiusBottomRight = 5;
            siticoneActivityButton1.CornerRadiusTopLeft = 5;
            siticoneActivityButton1.CornerRadiusTopRight = 5;
            siticoneActivityButton1.DisabledColor = Color.FromArgb(160, 160, 160);
            siticoneActivityButton1.Elevation = 2F;
            siticoneActivityButton1.Font = new Font("Segoe UI", 9F);
            siticoneActivityButton1.HoverAnimationDuration = 200;
            siticoneActivityButton1.HoverColor = Color.FromArgb(66, 165, 245);
            siticoneActivityButton1.Location = new Point(313, 135);
            siticoneActivityButton1.Name = "siticoneActivityButton1";
            siticoneActivityButton1.PressAnimationDuration = 150;
            siticoneActivityButton1.PressedColor = Color.FromArgb(21, 101, 192);
            siticoneActivityButton1.PressedElevation = 1F;
            siticoneActivityButton1.RippleColor = Color.FromArgb(128, 255, 255, 255);
            siticoneActivityButton1.RippleDuration = 1800;
            siticoneActivityButton1.RippleSize = 5;
            siticoneActivityButton1.ShowActivityText = true;
            siticoneActivityButton1.Size = new Size(190, 50);
            siticoneActivityButton1.TabIndex = 0;
            siticoneActivityButton1.Text = "siticoneActivityButton1";
            siticoneActivityButton1.TextColor = Color.White;
            siticoneActivityButton1.UseAnimation = true;
            siticoneActivityButton1.UseElevation = false;
            siticoneActivityButton1.UseRippleEffect = true;
            // 
            // UserControl_Account
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(siticoneActivityButton1);
            Name = "UserControl_Account";
            Size = new Size(948, 592);
            ResumeLayout(false);
        }

        #endregion

        private SiticoneNetCoreUI.SiticoneActivityButton siticoneActivityButton1;
    }
}
