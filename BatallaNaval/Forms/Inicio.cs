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
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
            InicializarDoubleBuffering();

            PantallaInicio();
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
            var config = new Form();

            config.Text = "Configuracion";
            config.Width = 600;
            config.Height = 400;
            config.StartPosition = FormStartPosition.CenterScreen;


            Label header = new()
            {
                Text = "Configuracion de grid",
                Font = new Font("Segoe UI semibold", 15),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Anchor = AnchorStyles.Top,
            };

            Label labelInput = new()
            {
                Text = "Ingrese la cantidad de filas y columnas con las que quiere jugar",
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Anchor = AnchorStyles.Top,
                Top = header.Bottom + 30
            };

            TextBox valorTamano = new()
            {
                Width = 200,
                Height = 29,
                Top = labelInput.Bottom + 10
            };

            Button confirmarBtn = new()
            {
                Text = "Comenzar juego",
                Font = new Font("Segoe UI Semibold", 9),
                Width = 200,
                Height = 40,
                Anchor = AnchorStyles.Top,
                BackColor = Color.Black,
                ForeColor = Color.White,
                Top = valorTamano.Bottom + 10
            };

            config.Controls.Add(header);
            config.Controls.Add(labelInput);
            config.Controls.Add(valorTamano);
            config.Controls.Add(confirmarBtn);
            config.PerformLayout();

            header.Left = (config.ClientSize.Width - header.Width) / 2;
            labelInput.Left = (config.ClientSize.Width - labelInput.Width) / 2;
            valorTamano.Left = (config.ClientSize.Width - valorTamano.Width) / 2;
            confirmarBtn.Left = (config.ClientSize.Width - confirmarBtn.Width) / 2;

            confirmarBtn.Click += (s, args) =>
            {
                int valor;
                if (int.TryParse(valorTamano.Text, out valor))
                { 
                    if (valor >= 5 && valor <= 15)
                    {
                        Program.Tamano = valor;
                        this.Hide();
                        Main mainForm = new Main();
                        mainForm.FormClosed += (sender, args) =>
                        {
                            config.Close();
                            this.Show();
                        };
                        mainForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar un numero entre 5 y 15");
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar un numero");
                }
            };

            config.ShowDialog();
            
        }

    }
}
