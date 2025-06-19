using System.Linq;
using System.Windows.Forms;
using BatallaNaval.Controladores;
using BatallaNaval.Modelos;
using EnvDTE;

namespace BatallaNaval
{
    public partial class Main : Form
    {
        public static bool MoviendoBarco = false;
        public static List<Celda> celdasJuego = [];
        public static List<Panel> celdasPosicion = [];

        public static List<Celda> celdasEnemigo = [];
        public static List<Panel> panelesEnemigo = [];

        public static List<Barco> barcos = [];
        public static List<Barco> barcosEnemigo = [];

        public static List<Celda> celdasAtacadasJugador = [];
        public static List<Celda> celdasAtacadasEnemigo = [];

        static bool JuegoEmpezado = false;


        public Main()
        {
            InitializeComponent();
            this.KeyPreview = true;

            this.KeyDown += (s, e) =>
            {
                if (MoviendoBarco)
                {
                    Barcos.SalirModoUbicacion(instruccionesLabel, gridJuego);
                }
            };

            // esconder barra para estirar el panel  
            scMain.IsSplitterFixed = true;
            scMain.Panel1MinSize = 0;
            scMain.Panel2MinSize = 0;
            scMain.SplitterWidth = 1;


            Barco PortaAviones = new()
            {
                Id = 1,
                CantidadCeldas = 5,
                NombreBarco = "porta aviones",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };
            Barco BarcoGrando = new()
            {
                Id = 2,
                CantidadCeldas = 4,
                NombreBarco = "destructor",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };
            Barco BarcoUnPocoMasChico = new()
            {
                Id = 3,
                CantidadCeldas = 3,
                NombreBarco = "cruiser",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };
            Barco BarcoChico = new()
            {
                Id = 4,
                CantidadCeldas = 2,
                NombreBarco = "patrulla",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };
            Barco BarcoChiquito = new()
            {
                Id = 5,
                NombreBarco = "nave",
                CantidadCeldas = 1,
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha",
                Hundido = false
            };

            barcos = [PortaAviones, BarcoGrando, BarcoUnPocoMasChico, BarcoChico, BarcoChiquito];
            List<PictureBox> barcosEnTablero = [portaAviones, barcoGrande, barcoUnPocoMasChico, barcoChico, barcoChiquitito];

            int contador = 1;

            foreach (Control c in barcosEnTablero)
            {
                if (c is PictureBox pictureBox && pictureBox.Tag == null)
                {
                    pictureBox.Tag = pictureBox.Image.Clone();
                }
            }

            // inicializar celdas  
            for (int i = 0; i < gridJuego.RowCount; i++)
            {
                for (int j = 0; j < gridJuego.ColumnCount; j++)
                {
                    Celda celda = new()
                    {
                        Id = contador,
                        ContieneBarco = false,
                        Atacada = false,
                        Fila = i,
                        Columna = j
                    };
                    celdasJuego.Add(celda);

                    Panel p = new()
                    {
                        Dock = DockStyle.Fill,
                        Tag = celda.Id,
                        Margin = new Padding(0),
                    };

                    celdasPosicion.Add(p);

                    p.Click += (s, args) => clickSeleccionCelda(s, args, celda, p, barcos);

                    gridJuego.Controls.Add(p);
                    contador++;
                }
            }


            // inicializar celdas enemigas
            for (int i = 0; i < gridEnemigo.RowCount; i++)
            {
                for (int j = 0; j < gridEnemigo.ColumnCount; j++)
                {
                    Celda celda = new()
                    {
                        Id = contador,
                        ContieneBarco = false,
                        Atacada = false,
                        Fila = i,
                        Columna = j
                    };
                    celdasEnemigo.Add(celda);

                    Panel p = new()
                    {
                        Dock = DockStyle.Fill,
                        Tag = celda.Id,
                        Margin = new Padding(0),
                    };
                    panelesEnemigo.Add(p);

                    p.Click += (s, args) => ClickCeldaEnemigo(s, args, celda, p, barcosEnTablero);

                    gridEnemigo.Controls.Add(p);
                    contador++;
                }
            }

            foreach (Barco barco in barcos)
            {
                Control barcoInTablero = barcosEnTablero[barcos.IndexOf(barco)];
                PictureBox pb = (PictureBox)barcoInTablero;

                barcoInTablero.Click += (s, args) => Barcos.SeleccionarBarco(s, args, barco, instruccionesLabel, gridJuego, panelSeleccion, pb, btnRotar);
            }
        }

        private void ClickCeldaEnemigo(object sender, EventArgs e, Celda celda, Panel p, List<PictureBox> barcosEnTablero)
        {
            if (JuegoEmpezado && !celda.Atacada)
            {
                var seleccion = Juego.HacerSeleccion(celda, barcosEnTablero);
                if (seleccion.contieneBarco)
                {
                    p.BackColor = Color.Red;
                    celdasAtacadasJugador.Add(celda);

                    // fijarse si se hundio el barco completo
                    if (seleccion.barcoAtacado.CeldasPosicion.All(c => c.Atacada))
                    {
                        seleccion.barcoAtacado.Hundido = true;
                    }
                }
                else
                {
                    p.BackColor = Color.White;
                }

                p.Cursor = Cursors.Default;
                celda.Atacada = true;

                if (Juego.VerificarFin(celdasAtacadasJugador))
                {
                    MessageBox.Show("GANO EL JUEGO!!");
                    this.Close();
                    ResetGameState();
                    Computadora.ResetState();
                }
                else
                {
                    bool computadoraGano = Computadora.TurnoComputadora(instruccionesLabel, this, barcosEnTablero);
                    if (computadoraGano)
                    {
                        MessageBox.Show("PERDIO EL JUEGO!!");
                        this.Close();
                        ResetGameState();
                        Computadora.ResetState();
                    }
                }
            };
        }

        private void btnRotar_Click(object sender, EventArgs e)
        {
            Barcos.ClickRotar(sender, e, this);
        }

        private void empezarJuegoBtn_Click(object sender, EventArgs e)
        {
            empezarJuegoBtn.Visible = false;
            foreach (Control control in gridJuego.Controls)
            {
                if (control is Panel)
                {
                    control.Cursor = Cursors.Default;
                }
            }

            // poner posiciones de ia
            Computadora.HacerSeleccion(this, btnRotar);
            JuegoEmpezado = true;
            foreach (Panel panel in panelesEnemigo)
            {
                panel.Cursor = Cursors.Hand;
            }
            instruccionesLabel.Text = "Haga click en una celda del panel a la derecha";

        }

        private void clickSeleccionCelda(object sender, EventArgs e, Celda celda, Panel p, List<Barco> barcos)
        {
            if (MoviendoBarco)
            {
                bool listo = false;
                var lugar = Barcos.ElegirCelda(celda, p, this, btnRotar);
                if (lugar.direccion == null)
                {
                    MessageBox.Show("No hay lugar");
                    return;
                };

                Barcos.PosicionarBarco(lugar.direccion, celda, this, p);

                // mostrar button rotar
                btnRotar.Visible = true;

                foreach (Barco barco in barcos)
                {
                    if (barco.EnPosicion)
                    {
                        listo = true;
                    }
                    else listo = false;
                }

                if (listo)
                {
                    empezarJuegoBtn.Visible = true;
                }
            }
        }

        public static void ResetGameState()
        {
            celdasJuego = new List<Celda>();
            celdasPosicion = new List<Panel>();
            barcos = new List<Barco>();
            barcosEnemigo = new List<Barco>();
            celdasEnemigo = new List<Celda>();
            panelesEnemigo = new List<Panel>();
            celdasAtacadasEnemigo = new List<Celda>();

            // Also reset computer state
            Computadora.ResetState();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            ResetGameState();
            Computadora.ResetState();
        }
    }
}
