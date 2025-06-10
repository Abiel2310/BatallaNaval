using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatallaNaval.Forms
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();

            PantallaInicio();
        }
        private void PantallaInicio()
        {
            Label header = new()
            {
                Text = "Batalla Naval",
                Font = new Font("Old English Text MT", 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 500,
                Height = 50,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Transparent,
            };
            header.Left = (this.Width / 2) - (header.Width / 2);


            Button btnInicio = new()
            {
                Text = "Jugador v jugador",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Top = header.Bottom + 20,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White
            };

            btnInicio.Left = (this.Width / 2) - (btnInicio.Width / 2);
            btnInicio.Top = (this.Height / 2) - (btnInicio.Height) - header.Height;

            Button btnJuegoPC = new()
            {
                Text = "Computadora",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White

            };

            btnJuegoPC.Left = (this.Width / 2) - (btnJuegoPC.Width / 2);
            btnJuegoPC.Top = btnInicio.Bottom + 10;
            btnJuegoPC.Click += IniciarJuego;

            panelInicio.Controls.Add(header);
            panelInicio.Controls.Add(btnInicio);
            panelInicio.Controls.Add(btnJuegoPC);
        }

        private void IniciarJuego(object sender, EventArgs e)
        {
            this.Hide();
            Main mainForm = new Main();
            mainForm.FormClosed += (sender, args) => this.Show();
            mainForm.Show();
        }

    }
}
