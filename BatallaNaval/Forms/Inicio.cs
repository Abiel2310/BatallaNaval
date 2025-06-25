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
    public partial class Inicio : Form
    {
        private TextBox textBox;
        private Label labelResultado;

        public Inicio()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeComponent();
            InicializarDoubleBuffering();
            PantallaInicio();

            //this.FormClosed
        }

        private void InicializarDoubleBuffering()
        {
            // Habilitar double buffering para todos los controles
            HabilitarDoubleBuffering(this);
        }

        private void HabilitarDoubleBuffering(Control control)
        {
            // Habilitar double buffering para el control
            control.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(control, true, null);

            // Aplicar a todos los componentes recursivamente
            foreach (Control child in control.Controls)
            {
                HabilitarDoubleBuffering(child);
            }
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
                Text = "Practica",
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
            btnInicio.Click += ConfiguracionPractica;

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
            btnJuegoPC.Click += CargarPartida;

            panelInicio.Controls.Add(header);
            panelInicio.Controls.Add(btnInicio);
            panelInicio.Controls.Add(btnJuegoPC);
        }
        private void CargarPartida(object sender, EventArgs e) 
        {
            //this.Hide();
            //CargarPartida cargarPartidaForm = new CargarPartida();
            //cargarPartidaForm.Show();
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
            cargarPartidaButton.Click +=  Main.btnCargarPartida_Click;
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
            NuevaPartida.Click += Configuracion;

            // Agregar los botones al panel
            panelInicio.Controls.Clear();
            panelInicio.Controls.Add(cargarPartidaButton);
            panelInicio.Controls.Add(NuevaPartida);
        }
        private void Configuracion(object sender, EventArgs e)
        {
            panelInicio.Controls.Clear();
            textBox = new TextBox();
            textBox.Location = new Point(((this.Width / 2) - (textBox.Width / 2)), ((this.Height / 2) - (textBox.Height / 2)));
            textBox.Name = "inputTamano";
            panelInicio.Controls.Add(textBox);

            // Crear botón para guardar el número ingresado
            Button btnGuardarNumero = new ()
            {
                Text = "Guardar Número",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            btnGuardarNumero.Location = new Point(((this.Width / 2) - (btnGuardarNumero.Width / 2)), ((this.Height / 2) - (btnGuardarNumero.Height / 2) - 50));
            btnGuardarNumero.Click += btnGuardarNumero_Click;
            panelInicio.Controls.Add(btnGuardarNumero);

            //Crear el boton para volver a la pantalla de inicio
            Button btnVolverInicio = new()
            {
                Text = "Volver al Inicio",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            btnVolverInicio.Location = new Point(50,50);
            btnVolverInicio.Click += (s, args) =>
            {
                panelInicio.Controls.Clear();
                PantallaInicio();
            };
            panelInicio.Controls.Add(btnVolverInicio);
        }
        private void ConfiguracionPractica(object sender, EventArgs e)
        {
            panelInicio.Controls.Clear();
            textBox = new TextBox();
            textBox.Location = new Point(((this.Width / 2) - (textBox.Width / 2)), ((this.Height / 2) - (textBox.Height / 2)));
            textBox.Name = "inputTamano";
            panelInicio.Controls.Add(textBox);

            // Crear botón para guardar el número ingresado
            Button btnGuardarNumero = new()
            {
                Text = "Guardar Número",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            btnGuardarNumero.Location = new Point(((this.Width / 2) - (btnGuardarNumero.Width / 2)), ((this.Height / 2) - (btnGuardarNumero.Height / 2) - 50));
            btnGuardarNumero.Click += btnGuardarNumero_ClickPractica;
            panelInicio.Controls.Add(btnGuardarNumero);

            //Crear el boton para volver a la pantalla de inicio
            Button btnVolverInicio = new()
            {
                Text = "Volver al Inicio",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White
            };
            btnVolverInicio.Location = new Point(50,50);
            btnVolverInicio.Click += (s, args) =>
            {
                panelInicio.Controls.Clear();
                PantallaInicio();
            };
            panelInicio.Controls.Add(btnVolverInicio);
        }
        private void btnGuardarNumero_Click(object sender, EventArgs e)
        {
            bool esNumero = int.TryParse(textBox.Text, out Program.tamano);

            if (esNumero && Program.tamano < 11 && Program.tamano > 4)
            {
                IniciarJuego();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido (entre 5 y 10).");
            }
        }
        private void btnGuardarNumero_ClickPractica(object sender, EventArgs e)
        {
            bool esNumero = int.TryParse(textBox.Text, out Program.tamano);

            if (esNumero && Program.tamano < 11 && Program.tamano > 4)
            {
                btnInicio_Click();
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número válido (entre 5 y 10).");
            }
        }
        private void IniciarJuego()
        {
            this.Hide();
            Main mainForm = new Main();

            mainForm.FormClosed += (sender, args) => this.Show();
            mainForm.Show();
        }
        private void btnInicio_Click()
        {
            this.Hide();
            Practica practicaForm = new Practica();

            practicaForm.FormClosed += (sender, args) => this.Show();

            practicaForm.Show();
            
        }

    }
}
