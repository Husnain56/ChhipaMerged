namespace Chhipa_Motors.GUI
{
    partial class Login
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
            txt_email = new SiticoneNetCoreUI.SiticoneTextBoxAdvanced();
            btn_confirm = new SiticoneNetCoreUI.SiticoneButton();
            btn_clear_create = new SiticoneNetCoreUI.SiticoneButton();
            txt_pass = new SiticoneNetCoreUI.SiticoneTextBoxAdvanced();
            label_credentials_acc = new SiticoneNetCoreUI.SiticoneShimmerLabel();
            SuspendLayout();
            // 
            // txt_email
            // 
            txt_email.BackColor = Color.Transparent;
            txt_email.BackgroundColor = Color.White;
            txt_email.BorderColor = Color.DarkGray;
            txt_email.FocusBorderColor = Color.DodgerBlue;
            txt_email.FocusImage = null;
            txt_email.ForeColor = SystemColors.WindowText;
            txt_email.HoverBorderColor = Color.Gray;
            txt_email.HoverImage = null;
            txt_email.IdleImage = null;
            txt_email.Location = new Point(164, 148);
            txt_email.Name = "txt_email";
            txt_email.PlaceholderColor = Color.Gray;
            txt_email.PlaceholderFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txt_email.PlaceholderText = "Email";
            txt_email.ReadOnlyColors.BackgroundColor = Color.FromArgb(245, 245, 245);
            txt_email.ReadOnlyColors.BorderColor = Color.FromArgb(200, 200, 200);
            txt_email.ReadOnlyColors.PlaceholderColor = Color.FromArgb(150, 150, 150);
            txt_email.ReadOnlyColors.TextColor = Color.FromArgb(100, 100, 100);
            txt_email.Size = new Size(260, 42);
            txt_email.TabIndex = 21;
            txt_email.TextColor = SystemColors.WindowText;
            txt_email.TextContent = "";
            txt_email.ValidationPattern = "";
            // 
            // btn_confirm
            // 
            btn_confirm.AccessibleDescription = "The default button control that accept input though the mouse, touch and keyboard";
            btn_confirm.AccessibleName = "Login";
            btn_confirm.AutoSizeBasedOnText = false;
            btn_confirm.BackColor = Color.Transparent;
            btn_confirm.BadgeBackColor = Color.Black;
            btn_confirm.BadgeFont = new Font("Segoe UI", 8F, FontStyle.Bold);
            btn_confirm.BadgeValue = 0;
            btn_confirm.BadgeValueForeColor = Color.White;
            btn_confirm.BorderColor = Color.FromArgb(213, 216, 220);
            btn_confirm.BorderWidth = 1;
            btn_confirm.ButtonBackColor = Color.FromArgb(245, 247, 250);
            btn_confirm.ButtonImage = null;
            btn_confirm.ButtonTextLeftPadding = 0;
            btn_confirm.CanBeep = true;
            btn_confirm.CanGlow = false;
            btn_confirm.CanShake = true;
            btn_confirm.ContextMenuStripEx = null;
            btn_confirm.CornerRadiusBottomLeft = 6;
            btn_confirm.CornerRadiusBottomRight = 6;
            btn_confirm.CornerRadiusTopLeft = 6;
            btn_confirm.CornerRadiusTopRight = 6;
            btn_confirm.CustomCursor = Cursors.Default;
            btn_confirm.DisabledTextColor = Color.FromArgb(150, 150, 150);
            btn_confirm.EnableLongPress = false;
            btn_confirm.EnableRippleEffect = true;
            btn_confirm.EnableShadow = false;
            btn_confirm.EnableTextWrapping = false;
            btn_confirm.Font = new Font("Segoe UI Semibold", 10.2F);
            btn_confirm.GlowColor = Color.FromArgb(100, 255, 255, 255);
            btn_confirm.GlowIntensity = 100;
            btn_confirm.GlowRadius = 20F;
            btn_confirm.GradientBackground = false;
            btn_confirm.GradientColor = Color.FromArgb(0, 227, 64);
            btn_confirm.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            btn_confirm.HintText = null;
            btn_confirm.HoverBackColor = Color.FromArgb(240, 240, 240);
            btn_confirm.HoverFontStyle = FontStyle.Regular;
            btn_confirm.HoverTextColor = Color.FromArgb(0, 0, 0);
            btn_confirm.HoverTransitionDuration = 250;
            btn_confirm.ImageAlign = ContentAlignment.MiddleLeft;
            btn_confirm.ImagePadding = 5;
            btn_confirm.ImageSize = new Size(16, 16);
            btn_confirm.IsRadial = false;
            btn_confirm.IsReadOnly = false;
            btn_confirm.IsToggleButton = false;
            btn_confirm.IsToggled = false;
            btn_confirm.Location = new Point(312, 276);
            btn_confirm.LongPressDurationMS = 1000;
            btn_confirm.Name = "btn_confirm";
            btn_confirm.NormalFontStyle = FontStyle.Regular;
            btn_confirm.ParticleColor = Color.FromArgb(200, 200, 200);
            btn_confirm.ParticleCount = 15;
            btn_confirm.PressAnimationScale = 0.97F;
            btn_confirm.PressedBackColor = Color.FromArgb(225, 227, 230);
            btn_confirm.PressedFontStyle = FontStyle.Regular;
            btn_confirm.PressTransitionDuration = 150;
            btn_confirm.ReadOnlyTextColor = Color.FromArgb(100, 100, 100);
            btn_confirm.RippleColor = Color.FromArgb(0, 0, 0);
            btn_confirm.RippleRadiusMultiplier = 0.6F;
            btn_confirm.ShadowBlur = 5;
            btn_confirm.ShadowColor = Color.FromArgb(30, 0, 0, 0);
            btn_confirm.ShadowOffset = new Point(0, 2);
            btn_confirm.ShakeDuration = 500;
            btn_confirm.ShakeIntensity = 5;
            btn_confirm.Size = new Size(112, 50);
            btn_confirm.TabIndex = 20;
            btn_confirm.Text = "Login";
            btn_confirm.TextAlign = ContentAlignment.MiddleCenter;
            btn_confirm.TextColor = Color.FromArgb(0, 0, 0);
            btn_confirm.TooltipText = null;
            btn_confirm.UseAdvancedRendering = true;
            btn_confirm.UseParticles = false;
            btn_confirm.Click += btn_confirm_Click;
            // 
            // btn_clear_create
            // 
            btn_clear_create.AccessibleDescription = "The default button control that accept input though the mouse, touch and keyboard";
            btn_clear_create.AccessibleName = "Clear";
            btn_clear_create.AutoSizeBasedOnText = false;
            btn_clear_create.BackColor = Color.Transparent;
            btn_clear_create.BadgeBackColor = Color.Black;
            btn_clear_create.BadgeFont = new Font("Segoe UI", 8F, FontStyle.Bold);
            btn_clear_create.BadgeValue = 0;
            btn_clear_create.BadgeValueForeColor = Color.White;
            btn_clear_create.BorderColor = Color.FromArgb(213, 216, 220);
            btn_clear_create.BorderWidth = 1;
            btn_clear_create.ButtonBackColor = Color.FromArgb(245, 247, 250);
            btn_clear_create.ButtonImage = null;
            btn_clear_create.ButtonTextLeftPadding = 0;
            btn_clear_create.CanBeep = true;
            btn_clear_create.CanGlow = false;
            btn_clear_create.CanShake = true;
            btn_clear_create.ContextMenuStripEx = null;
            btn_clear_create.CornerRadiusBottomLeft = 6;
            btn_clear_create.CornerRadiusBottomRight = 6;
            btn_clear_create.CornerRadiusTopLeft = 6;
            btn_clear_create.CornerRadiusTopRight = 6;
            btn_clear_create.CustomCursor = Cursors.Default;
            btn_clear_create.DisabledTextColor = Color.FromArgb(150, 150, 150);
            btn_clear_create.EnableLongPress = false;
            btn_clear_create.EnableRippleEffect = true;
            btn_clear_create.EnableShadow = false;
            btn_clear_create.EnableTextWrapping = false;
            btn_clear_create.Font = new Font("Segoe UI Semibold", 10.2F);
            btn_clear_create.GlowColor = Color.FromArgb(100, 255, 255, 255);
            btn_clear_create.GlowIntensity = 100;
            btn_clear_create.GlowRadius = 20F;
            btn_clear_create.GradientBackground = false;
            btn_clear_create.GradientColor = Color.FromArgb(0, 227, 64);
            btn_clear_create.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            btn_clear_create.HintText = null;
            btn_clear_create.HoverBackColor = Color.FromArgb(240, 240, 240);
            btn_clear_create.HoverFontStyle = FontStyle.Regular;
            btn_clear_create.HoverTextColor = Color.FromArgb(0, 0, 0);
            btn_clear_create.HoverTransitionDuration = 250;
            btn_clear_create.ImageAlign = ContentAlignment.MiddleLeft;
            btn_clear_create.ImagePadding = 5;
            btn_clear_create.ImageSize = new Size(16, 16);
            btn_clear_create.IsRadial = false;
            btn_clear_create.IsReadOnly = false;
            btn_clear_create.IsToggleButton = false;
            btn_clear_create.IsToggled = false;
            btn_clear_create.Location = new Point(451, 171);
            btn_clear_create.LongPressDurationMS = 1000;
            btn_clear_create.Name = "btn_clear_create";
            btn_clear_create.NormalFontStyle = FontStyle.Regular;
            btn_clear_create.ParticleColor = Color.FromArgb(200, 200, 200);
            btn_clear_create.ParticleCount = 15;
            btn_clear_create.PressAnimationScale = 0.97F;
            btn_clear_create.PressedBackColor = Color.FromArgb(225, 227, 230);
            btn_clear_create.PressedFontStyle = FontStyle.Regular;
            btn_clear_create.PressTransitionDuration = 150;
            btn_clear_create.ReadOnlyTextColor = Color.FromArgb(100, 100, 100);
            btn_clear_create.RippleColor = Color.FromArgb(0, 0, 0);
            btn_clear_create.RippleRadiusMultiplier = 0.6F;
            btn_clear_create.ShadowBlur = 5;
            btn_clear_create.ShadowColor = Color.FromArgb(30, 0, 0, 0);
            btn_clear_create.ShadowOffset = new Point(0, 2);
            btn_clear_create.ShakeDuration = 500;
            btn_clear_create.ShakeIntensity = 5;
            btn_clear_create.Size = new Size(112, 50);
            btn_clear_create.TabIndex = 19;
            btn_clear_create.Text = "Clear";
            btn_clear_create.TextAlign = ContentAlignment.MiddleCenter;
            btn_clear_create.TextColor = Color.FromArgb(0, 0, 0);
            btn_clear_create.TooltipText = null;
            btn_clear_create.UseAdvancedRendering = true;
            btn_clear_create.UseParticles = false;
            btn_clear_create.Click += btn_clear_create_Click;
            // 
            // txt_pass
            // 
            txt_pass.BackColor = Color.Transparent;
            txt_pass.BackgroundColor = Color.White;
            txt_pass.BorderColor = Color.DarkGray;
            txt_pass.FocusBorderColor = Color.DodgerBlue;
            txt_pass.FocusImage = null;
            txt_pass.HoverBorderColor = Color.Gray;
            txt_pass.HoverImage = null;
            txt_pass.IdleImage = null;
            txt_pass.Location = new Point(164, 207);
            txt_pass.Name = "txt_pass";
            txt_pass.PlaceholderColor = Color.Gray;
            txt_pass.PlaceholderFont = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            txt_pass.PlaceholderText = "Password";
            txt_pass.ReadOnlyColors.BackgroundColor = Color.FromArgb(245, 245, 245);
            txt_pass.ReadOnlyColors.BorderColor = Color.FromArgb(200, 200, 200);
            txt_pass.ReadOnlyColors.PlaceholderColor = Color.FromArgb(150, 150, 150);
            txt_pass.ReadOnlyColors.TextColor = Color.FromArgb(100, 100, 100);
            txt_pass.Size = new Size(260, 42);
            txt_pass.TabIndex = 18;
            txt_pass.TextColor = SystemColors.WindowText;
            txt_pass.TextContent = "";
            txt_pass.ValidationPattern = "";
            // 
            // label_credentials_acc
            // 
            label_credentials_acc.AutoReverse = false;
            label_credentials_acc.BackColor = Color.Black;
            label_credentials_acc.BaseColor = Color.White;
            label_credentials_acc.Direction = SiticoneNetCoreUI.ShimmerDirection.LeftToRight;
            label_credentials_acc.Font = new Font("Modern No. 20", 26.2499962F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_credentials_acc.IsAnimating = true;
            label_credentials_acc.IsPaused = false;
            label_credentials_acc.Location = new Point(128, 50);
            label_credentials_acc.Name = "label_credentials_acc";
            label_credentials_acc.PauseDuration = 0;
            label_credentials_acc.ShimmerColor = Color.Cyan;
            label_credentials_acc.ShimmerOpacity = 1F;
            label_credentials_acc.ShimmerSpeed = 50;
            label_credentials_acc.ShimmerWidth = 0.2F;
            label_credentials_acc.Size = new Size(357, 56);
            label_credentials_acc.TabIndex = 15;
            label_credentials_acc.Text = "Enter Your Credentials";
            label_credentials_acc.ToolTipText = "";
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            Controls.Add(txt_email);
            Controls.Add(btn_confirm);
            Controls.Add(btn_clear_create);
            Controls.Add(txt_pass);
            Controls.Add(label_credentials_acc);
            Name = "Login";
            Size = new Size(655, 456);
            ResumeLayout(false);
        }

        #endregion

        private SiticoneNetCoreUI.SiticoneTextBoxAdvanced txt_email;
        private SiticoneNetCoreUI.SiticoneButton btn_confirm;
        private SiticoneNetCoreUI.SiticoneButton btn_clear_create;
        private SiticoneNetCoreUI.SiticoneTextBoxAdvanced txt_pass;
        private SiticoneNetCoreUI.SiticoneShimmerLabel label_credentials_acc;
    }
}
