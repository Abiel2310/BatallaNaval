namespace BatallaNaval.Forms
{
    partial class Inicio
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelInicio = new Panel();
            SuspendLayout();
            // 
            // panelInicio
            // 
            panelInicio.BackgroundImage = Properties.Resources.background;
            panelInicio.BackgroundImageLayout = ImageLayout.Stretch;
            panelInicio.Dock = DockStyle.Fill;
            panelInicio.Location = new Point(0, 0);
            panelInicio.Name = "panelInicio";
            panelInicio.Size = new Size(931, 538);
            panelInicio.TabIndex = 0;
            // 
            // Inicio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(931, 538);
            Controls.Add(panelInicio);
            Name = "Inicio";
            Text = "Inicio";
            ResumeLayout(false);
        }

        #endregion

        private Panel panelInicio;
    }
}