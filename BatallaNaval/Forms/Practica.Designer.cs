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
            label1 = new Label();
            gridPractica = new TableLayoutPanel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(gridPractica);
            panel1.Location = new Point(97, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(833, 613);
            panel1.TabIndex = 0;
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
            // gridPractica
            // 
            gridPractica.Anchor = AnchorStyles.Left;
            gridPractica.BackColor = SystemColors.ActiveCaption;
            gridPractica.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            gridPractica.ColumnCount = 10;
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            gridPractica.Location = new Point(159, 74);
            gridPractica.Name = "gridPractica";
            gridPractica.RowCount = 10;
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            gridPractica.Size = new Size(521, 519);
            gridPractica.TabIndex = 1;
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
            Text = " ";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TableLayoutPanel gridPractica;
        private Label label1;
    }
}