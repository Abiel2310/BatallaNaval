using System;
using System.Drawing;
using System.Windows.Forms;
using BatallaNaval.Modelos;
using BatallaNaval.PersistenciaC;

namespace BatallaNaval.Forms
{
    public partial class Autenticacion : Form
    {
        public Autenticacion()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            InicializarDoubleBuffering();
            CrearInterfaz();
        }

        private void InicializarDoubleBuffering()
        {
            HabilitarDoubleBuffering(this);
        }

        private void HabilitarDoubleBuffering(Control control)
        {
            control.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(control, true, null);

            foreach (Control child in control.Controls)
            {
                HabilitarDoubleBuffering(child);
            }
        }

        private void CrearInterfaz()
        {
            Label header = new()
            {
                Text = "Bienvenido a Batalla Naval",
                Font = new Font("Old English Text MT", 28),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 600,
                Height = 50,
                Top = 40,
                Left = (this.Width / 2) - 300,
                BackColor = Color.Transparent,
            };
            panelAuth.Controls.Add(header);

            Button btnLogin = new()
            {
                Text = "Iniciar Sesión",
                Font = new Font("Segoe UI Semibold", 10),
                Width = 200,
                Height = 40,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Top = 150,
                Left = (this.Width / 2) - 100,
            };
            btnLogin.Click += (s, e) => MostrarFormularioLogin();
            panelAuth.Controls.Add(btnLogin);

            Button btnRegister = new()
            {
                Text = "Registrarse",
                Font = new Font("Segoe UI Semibold", 10),
                Width = 200,
                Height = 40,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Top = 210,
                Left = (this.Width / 2) - 100,
            };
            btnRegister.Click += (s, e) => MostrarFormularioRegistro();
            panelAuth.Controls.Add(btnRegister);
        }

        private void MostrarFormularioLogin()
        {
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    this.Hide();
                    new Inicio().ShowDialog();
                    this.Close();
                }
            }
        }

        private void MostrarFormularioRegistro()
        {
            using (var registerForm = new RegistroForm())
            {
                if (registerForm.ShowDialog() == DialogResult.OK)
                {
                    this.Hide();
                    new Inicio().ShowDialog();
                    this.Close();
                }
            }
        }
    }
}

