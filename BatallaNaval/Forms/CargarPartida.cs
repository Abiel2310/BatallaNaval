using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatallaNaval.Controladores;
using BatallaNaval.PersistenciaC;

namespace BatallaNaval.Forms
{
    public partial class CargarPartida : Form
    {
        public CargarPartida()
        {
            InitializeComponent();
            CargarBotones();
        }
        private void CargarBotones() 
        {
            Button cargarPartidaButton = new Button
            {
                Text = "Cargar Partida",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White, 
            };
            cargarPartidaButton.Location = new Point(((this.Width / 2) - (cargarPartidaButton.Width / 2)), ((this.Height / 2) - (cargarPartidaButton.Height / 2) - 50));

            Button NuevaPartida = new Button
            {
                Text = "Nueva Partida",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            NuevaPartida.Location = new Point(((this.Width / 2) - (NuevaPartida.Width / 2)), ((this.Height / 2) - (NuevaPartida.Height / 2) - 100));

            // Agregar los botones al formulario
            this.Controls.Add(cargarPartidaButton);
            this.Controls.Add(NuevaPartida);
        }
    }
}
