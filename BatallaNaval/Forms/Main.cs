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
                Direccion = "derecha"
            };
            Barco BarcoGrando = new()
            {
                Id = 2,
                CantidadCeldas = 4,
                NombreBarco = "destructor",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha"
            };
            Barco BarcoUnPocoMasChico = new()
            {
                Id = 3,
                CantidadCeldas = 3,
                NombreBarco = "cruiser",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha"
            };
            Barco BarcoChico = new()
            {
                Id = 4,
                CantidadCeldas = 2,
                NombreBarco = "patrulla",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha"
            };
            Barco BarcoChiquito = new()
            {
                Id = 5,
                NombreBarco = "nave",
                CantidadCeldas = 1,
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3],
                Direccion = "derecha"
            };

            barcos = [PortaAviones, BarcoGrando, BarcoUnPocoMasChico, BarcoChico, BarcoChiquito];
            List<Control> barcosEnTablero = [portaAviones, barcoGrande, barcoUnPocoMasChico, barcoChico, barcoChiquitito];

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

        private void btnRotar_Click(object sender, EventArgs e)
        {
            Barcos.ClickRotar(sender, e, this);
        }


        private void empezarJuegoBtn_Click(object sender, EventArgs e)
        {
            foreach (Control control in gridJuego.Controls)
            {
                if (control is Panel)
                {
                    control.Cursor = Cursors.Default;
                }
            }

            // poner posiciones de ia
            Computadora.HacerSeleccion(this, btnRotar);


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
    }
}
