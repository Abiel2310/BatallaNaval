namespace BatallaNaval.Forms
{
    partial class Practica
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
            panel1 = new Panel();
            gridJuego = new TableLayoutPanel();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(gridJuego);
            panel1.Location = new Point(97, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(833, 613);
            panel1.TabIndex = 0;
            // 
            // gridJuego
            // 
            gridJuego.Anchor = AnchorStyles.Left;
            gridJuego.BackColor = SystemColors.ActiveCaption;
            gridJuego.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            gridJuego.ColumnCount = 10;
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridJuego.Location = new Point(159, 74);
            gridJuego.Name = "gridJuego";
            gridJuego.RowCount = 10;
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridJuego.Size = new Size(521, 519);
            gridJuego.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.GradientActiveCaption;
            label1.Font = new Font("Old English Text MT", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(279, 22);
            label1.Name = "label1";
            label1.Size = new Size(284, 44);
            label1.TabIndex = 3;
            label1.Text = "Naves de contrincante";
            label1.UseCompatibleTextRendering = true;
            // 
            // Practica
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.background;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1020, 646);
            Controls.Add(panel1);
            Name = "Practica";
            Text = "Practica";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel gridJuego;
        private Label label1;
    }
}