namespace BatallaNaval
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelInicio = new Panel();
            panelJuego = new Panel();
            panel1 = new Panel();
            instruccionesLabel = new Label();
            panelSeleccion = new Panel();
            btnRotar = new Button();
            textoInstruccion = new Label();
            scMain = new SplitContainer();
            gridJuego = new TableLayoutPanel();
            empezarJuegoBtn = new Button();
            barcoChiquitito = new PictureBox();
            barcoGrande = new PictureBox();
            barcoChico = new PictureBox();
            barcoUnPocoMasChico = new PictureBox();
            portaAviones = new PictureBox();
            gridEnemigo = new TableLayoutPanel();
            label1 = new Label();
            panelInicio.SuspendLayout();
            panelJuego.SuspendLayout();
            panel1.SuspendLayout();
            panelSeleccion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)scMain).BeginInit();
            scMain.Panel1.SuspendLayout();
            scMain.Panel2.SuspendLayout();
            scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)barcoChiquitito).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barcoGrande).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barcoChico).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barcoUnPocoMasChico).BeginInit();
            ((System.ComponentModel.ISupportInitialize)portaAviones).BeginInit();
            SuspendLayout();
            // 
            // panelInicio
            // 
            panelInicio.BackgroundImage = Properties.Resources.background;
            panelInicio.BackgroundImageLayout = ImageLayout.Stretch;
            panelInicio.Controls.Add(panelJuego);
            panelInicio.Dock = DockStyle.Fill;
            panelInicio.Location = new Point(0, 0);
            panelInicio.Name = "panelInicio";
            panelInicio.Size = new Size(1079, 609);
            panelInicio.TabIndex = 0;
            // 
            // panelJuego
            // 
            panelJuego.BackColor = SystemColors.GradientActiveCaption;
            panelJuego.BackgroundImage = Properties.Resources.background;
            panelJuego.Controls.Add(panel1);
            panelJuego.Controls.Add(scMain);
            panelJuego.Dock = DockStyle.Fill;
            panelJuego.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panelJuego.Location = new Point(0, 0);
            panelJuego.Name = "panelJuego";
            panelJuego.Size = new Size(1079, 609);
            panelJuego.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.None;
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(instruccionesLabel);
            panel1.Controls.Add(panelSeleccion);
            panel1.Location = new Point(65, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(948, 37);
            panel1.TabIndex = 6;
            // 
            // instruccionesLabel
            // 
            instruccionesLabel.Anchor = AnchorStyles.None;
            instruccionesLabel.AutoSize = true;
            instruccionesLabel.BackColor = SystemColors.GradientActiveCaption;
            instruccionesLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            instruccionesLabel.Location = new Point(16, 11);
            instruccionesLabel.Name = "instruccionesLabel";
            instruccionesLabel.Size = new Size(305, 25);
            instruccionesLabel.TabIndex = 4;
            instruccionesLabel.Text = "Haga click en un barco para seleccionarlo";
            instruccionesLabel.UseCompatibleTextRendering = true;
            // 
            // panelSeleccion
            // 
            panelSeleccion.Anchor = AnchorStyles.None;
            panelSeleccion.BackColor = Color.Transparent;
            panelSeleccion.Controls.Add(btnRotar);
            panelSeleccion.Controls.Add(textoInstruccion);
            panelSeleccion.Location = new Point(544, 0);
            panelSeleccion.Name = "panelSeleccion";
            panelSeleccion.Size = new Size(401, 37);
            panelSeleccion.TabIndex = 5;
            panelSeleccion.Visible = false;
            // 
            // btnRotar
            // 
            btnRotar.Anchor = AnchorStyles.None;
            btnRotar.BackColor = Color.Black;
            btnRotar.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRotar.ForeColor = SystemColors.Control;
            btnRotar.Location = new Point(307, 3);
            btnRotar.Name = "btnRotar";
            btnRotar.Size = new Size(94, 35);
            btnRotar.TabIndex = 1;
            btnRotar.Text = "Rotar";
            btnRotar.UseVisualStyleBackColor = false;
            btnRotar.Click += btnRotar_Click;
            // 
            // textoInstruccion
            // 
            textoInstruccion.Anchor = AnchorStyles.None;
            textoInstruccion.AutoSize = true;
            textoInstruccion.BackColor = SystemColors.GradientActiveCaption;
            textoInstruccion.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textoInstruccion.Location = new Point(0, 12);
            textoInstruccion.Name = "textoInstruccion";
            textoInstruccion.Size = new Size(49, 25);
            textoInstruccion.TabIndex = 0;
            textoInstruccion.Text = "label2";
            textoInstruccion.UseCompatibleTextRendering = true;
            // 
            // scMain
            // 
            scMain.Anchor = AnchorStyles.None;
            scMain.BackColor = SystemColors.GradientActiveCaption;
            scMain.Location = new Point(65, 37);
            scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            scMain.Panel1.BackColor = SystemColors.GradientActiveCaption;
            scMain.Panel1.Controls.Add(gridJuego);
            // 
            // scMain.Panel2
            // 
            scMain.Panel2.BackColor = SystemColors.GradientActiveCaption;
            scMain.Panel2.Controls.Add(empezarJuegoBtn);
            scMain.Panel2.Controls.Add(barcoChiquitito);
            scMain.Panel2.Controls.Add(barcoGrande);
            scMain.Panel2.Controls.Add(barcoChico);
            scMain.Panel2.Controls.Add(barcoUnPocoMasChico);
            scMain.Panel2.Controls.Add(portaAviones);
            scMain.Panel2.Controls.Add(gridEnemigo);
            scMain.Panel2.Controls.Add(label1);
            scMain.Size = new Size(948, 546);
            scMain.SplitterDistance = 550;
            scMain.TabIndex = 3;
            // 
            // gridJuego
            // 
            gridJuego.Anchor = AnchorStyles.Left;
            gridJuego.BackColor = SystemColors.ActiveCaption;
            gridJuego.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            gridJuego.ColumnCount = Program.tamano;
            for (int i = 0; i < gridJuego.ColumnCount; i++)
            {
                gridJuego.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
            }
            gridJuego.Location = new Point(16, 9);
            gridJuego.Name = "gridJuego";
            gridJuego.RowCount = Program.tamano;
            for (int i = 0; i < gridJuego.RowCount; i++)
            {
                gridJuego.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            }
            gridJuego.Size = new Size(521, 519);
            gridJuego.TabIndex = 0;
            // 
            // empezarJuegoBtn
            // 
            empezarJuegoBtn.Anchor = AnchorStyles.None;
            empezarJuegoBtn.BackColor = Color.Black;
            empezarJuegoBtn.ForeColor = SystemColors.Control;
            empezarJuegoBtn.Location = new Point(48, 277);
            empezarJuegoBtn.Name = "empezarJuegoBtn";
            empezarJuegoBtn.Size = new Size(223, 52);
            empezarJuegoBtn.TabIndex = 8;
            empezarJuegoBtn.Text = "Empezar juego";
            empezarJuegoBtn.UseVisualStyleBackColor = false;
            empezarJuegoBtn.Visible = false;
            empezarJuegoBtn.Click += empezarJuegoBtn_Click;
            // 
            // barcoChiquitito
            // 
            barcoChiquitito.Anchor = AnchorStyles.None;
            barcoChiquitito.Cursor = Cursors.Hand;
            barcoChiquitito.Image = Properties.Resources.ShipPatrolHull;
            barcoChiquitito.Location = new Point(311, 477);
            barcoChiquitito.Name = "barcoChiquitito";
            barcoChiquitito.Size = new Size(25, 51);
            barcoChiquitito.SizeMode = PictureBoxSizeMode.StretchImage;
            barcoChiquitito.TabIndex = 7;
            barcoChiquitito.TabStop = false;
            // 
            // barcoGrande
            // 
            barcoGrande.Anchor = AnchorStyles.None;
            barcoGrande.Cursor = Cursors.Hand;
            barcoGrande.Image = Properties.Resources.ShipBattleshipHull;
            barcoGrande.Location = new Point(128, 335);
            barcoGrande.Name = "barcoGrande";
            barcoGrande.Size = new Size(41, 193);
            barcoGrande.SizeMode = PictureBoxSizeMode.StretchImage;
            barcoGrande.TabIndex = 6;
            barcoGrande.TabStop = false;
            // 
            // barcoChico
            // 
            barcoChico.Anchor = AnchorStyles.None;
            barcoChico.Cursor = Cursors.Hand;
            barcoChico.Image = Properties.Resources.ShipDestroyerHull;
            barcoChico.Location = new Point(256, 429);
            barcoChico.Name = "barcoChico";
            barcoChico.Size = new Size(38, 99);
            barcoChico.SizeMode = PictureBoxSizeMode.StretchImage;
            barcoChico.TabIndex = 5;
            barcoChico.TabStop = false;
            // 
            // barcoUnPocoMasChico
            // 
            barcoUnPocoMasChico.Anchor = AnchorStyles.None;
            barcoUnPocoMasChico.Cursor = Cursors.Hand;
            barcoUnPocoMasChico.Image = Properties.Resources.ShipCruiserHull;
            barcoUnPocoMasChico.Location = new Point(195, 374);
            barcoUnPocoMasChico.Name = "barcoUnPocoMasChico";
            barcoUnPocoMasChico.Size = new Size(36, 154);
            barcoUnPocoMasChico.SizeMode = PictureBoxSizeMode.StretchImage;
            barcoUnPocoMasChico.TabIndex = 4;
            barcoUnPocoMasChico.TabStop = false;
            // 
            // portaAviones
            // 
            portaAviones.Anchor = AnchorStyles.None;
            portaAviones.Cursor = Cursors.Hand;
            portaAviones.Image = Properties.Resources.ShipCarrierHull;
            portaAviones.Location = new Point(35, 277);
            portaAviones.Name = "portaAviones";
            portaAviones.Size = new Size(64, 258);
            portaAviones.SizeMode = PictureBoxSizeMode.StretchImage;
            portaAviones.TabIndex = 3;
            portaAviones.TabStop = false;
            // 
            // gridEnemigo
            // 
            gridEnemigo.Anchor = AnchorStyles.Right;
            gridEnemigo.BackColor = SystemColors.ActiveCaption;
            gridEnemigo.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            gridEnemigo.ColumnCount = Program.tamano;
            for (int i = 0; i < gridEnemigo.ColumnCount; i++)
            {
                gridEnemigo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            }
            gridEnemigo.Location = new Point(48, 47);
            gridEnemigo.Name = "gridEnemigo";
            gridEnemigo.RowCount = Program.tamano;
            for (int i = 0; i < gridEnemigo.RowCount; i++)
            {
                gridEnemigo.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            }
            gridEnemigo.Size = new Size(223, 224);
            gridEnemigo.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.GradientActiveCaption;
            label1.Font = new Font("Old English Text MT", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(17, 9);
            label1.Name = "label1";
            label1.Size = new Size(284, 44);
            label1.TabIndex = 2;
            label1.Text = "Naves de contrincante";
            label1.UseCompatibleTextRendering = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1079, 609);
            Controls.Add(panelInicio);
            Name = "Main";
            Text = "Batalla Naval - Computadora";
            FormClosing += Main_FormClosing;
            panelInicio.ResumeLayout(false);
            panelJuego.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelSeleccion.ResumeLayout(false);
            panelSeleccion.PerformLayout();
            scMain.Panel1.ResumeLayout(false);
            scMain.Panel2.ResumeLayout(false);
            scMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)scMain).EndInit();
            scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)barcoChiquitito).EndInit();
            ((System.ComponentModel.ISupportInitialize)barcoGrande).EndInit();
            ((System.ComponentModel.ISupportInitialize)barcoChico).EndInit();
            ((System.ComponentModel.ISupportInitialize)barcoUnPocoMasChico).EndInit();
            ((System.ComponentModel.ISupportInitialize)portaAviones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelInicio;
        private Panel panelJuego;
        private TableLayoutPanel gridJuego;
        private Label label1;
        private TableLayoutPanel gridEnemigo;
        private SplitContainer scMain;
        private PictureBox portaAviones;
        private PictureBox barcoChico;
        private PictureBox barcoUnPocoMasChico;
        private PictureBox barcoChiquitito;
        private PictureBox barcoGrande;
        private Label instruccionesLabel;
        public Panel panelSeleccion;
        private Label label2;
        public Label textoInstruccion;
        private Button btnRotar;
        private Panel panel1;
        private Button empezarJuegoBtn;
    }
}
