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
        private Celda celdaClick = null;
        private Panel panelClick = null;

        //public Label lab
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
                OrdenRotacion = [0, 1, 2, 3]
            };
            Barco BarcoGrando = new()
            {
                Id = 2,
                CantidadCeldas = 4,
                NombreBarco = "destructor",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3]
            };
            Barco BarcoUnPocoMasChico = new()
            {
                Id = 3,
                CantidadCeldas = 3,
                NombreBarco = "cruiser",
                EnPosicion = false,
                OrdenRotacion = [0, 1, 2, 3]
            };
            Barco BarcoChico = new()
            {
                Id = 4,
                CantidadCeldas = 2,
                NombreBarco = "patrulla",
                EnPosicion = false
            };
            Barco BarcoChiquito = new()
            {
                Id = 5,
                NombreBarco = "nave",
                CantidadCeldas = 1,
                OrdenRotacion = [0, 1, 2, 3]
            };

            List<Barco> barcos = [PortaAviones, BarcoGrando, BarcoUnPocoMasChico, BarcoChico, BarcoChiquito];
            List<Control> barcosEnTablero = [portaAviones, barcoGrande, barcoUnPocoMasChico, barcoChico, barcoChiquitito];

            int contador = 1;
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

                    celdaClick = celda;
                    panelClick = p;

                    celdasPosicion.Add(p);

                    p.Click += (s, args) =>
                    {
                        if (MoviendoBarco)
                        {
                            var seleccion = Barcos.ElegirCelda(s, args, celda, p);


                            if (seleccion.barco != null)
                            {
                                PictureBox pb = (PictureBox)barcosEnTablero[barcos.IndexOf(seleccion.barco)];

                                // reset the rotation to original
                                if (pb.Tag == null)
                                {
                                    pb.Tag = pb.Image.Clone(); // Save original
                                }
                                pb.Image = (Image)((Image)pb.Tag).Clone();
                                // Make sure the ship isn't already added to the form
                                if (pb.Parent != null)
                                    pb.Parent.Controls.Remove(pb);

                                Point screenPos = p.PointToScreen(Point.Empty);
                                Point formPos = this.PointToClient(screenPos);

                                pb.Location = formPos;


                                switch (seleccion.direccion)
                                {
                                    case "arriba":
                                        {
                                            pb.Width = p.Width;
                                            pb.Height = p.Height * seleccion.barco.CantidadCeldas;

                                            // conseguir el punto mas arriba??
                                            int num = seleccion.barco.CantidadCeldas - 1;
                                            Celda c = celdasJuego[celdasJuego.IndexOf(celda) - (10 * num)];
                                            Control p1 = celdasPosicion[celdasJuego.IndexOf(c)];

                                            Point posIzq = p1.PointToScreen(Point.Empty);
                                            Point pos = this.PointToClient(posIzq);

                                            pb.Location = pos;

                                            break;
                                        }

                                    case "izquierda":
                                        {
                                            pb.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                            pb.Height = p.Height;
                                            pb.Width = p.Width * seleccion.barco.CantidadCeldas;

                                            // conseguir el punto mas a izquierda
                                            int num = seleccion.barco.CantidadCeldas - 1;
                                            Celda c = celdasJuego[celdasJuego.IndexOf(celda) - num];
                                            Control p1 = celdasPosicion[celdasJuego.IndexOf(c)];

                                            Point posIzq = p1.PointToScreen(Point.Empty);
                                            Point pos = this.PointToClient(posIzq);

                                            pb.Location = pos;
                                            pb.Refresh();
                                            break;
                                        }
                                    case "abajo":
                                        {
                                            pb.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                            pb.Width = p.Width;
                                            pb.Height = p.Height * seleccion.barco.CantidadCeldas;
                                            pb.Refresh();
                                            break;
                                        }
                                    case "derecha":
                                        {
                                            pb.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                            pb.Height = p.Height;
                                            pb.Width = p.Width * seleccion.barco.CantidadCeldas;
                                            pb.Refresh();
                                            break;
                                        };
                                }

                                // Add the ship directly on the form
                                this.Controls.Add(pb);

                                pb.BackColor = SystemColors.GradientActiveCaption;


                                pb.BringToFront();
                                pb.Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No hay lugar");
                        }
                    };


                    gridJuego.Controls.Add(p);
                    contador++;
                }
            }

            foreach (Barco barco in barcos)
            {
                Control barcoInTablero = barcosEnTablero[barcos.IndexOf(barco)];

                PictureBox pb = (PictureBox)barcoInTablero;

                barcoInTablero.Click += (s, args) => Barcos.SeleccionarBarco(s, args, barco, instruccionesLabel, gridJuego, panelSeleccion, pb);
            }


        }

        private void btnRotar_Click(object sender, EventArgs e)
        {
            Barcos.ClickRotar(sender, e, this);
           
        }
    }
}
