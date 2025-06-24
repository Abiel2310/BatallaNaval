namespace BatallaNaval.Forms
{
    partial class Autenticacion
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelAuth;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelAuth = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelAuth
            // 
            this.panelAuth.BackgroundImage = global::BatallaNaval.Properties.Resources.background;
            this.panelAuth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelAuth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAuth.Location = new System.Drawing.Point(0, 0);
            this.panelAuth.Name = "panelAuth";
            this.panelAuth.Size = new System.Drawing.Size(931, 538);
            this.panelAuth.TabIndex = 0;
            // 
            // Autenticacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 538);
            this.Controls.Add(this.panelAuth);
            this.Name = "Autenticacion";
            this.Text = "Autenticacion";
            this.ResumeLayout(false);
        }
    }
}
