using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatallaNaval.Modelos;
using static System.Windows.Forms.AxHost;

namespace BatallaNaval.Controladores
{
    internal class Barcos
    {
        static Barco barcoSeleccionado = null;
        static PictureBox barcoImg = null;
        static Panel panelSeleccion = null;

        private static string ObtenerDireccionSiguiente(string actual)
        {
            return actual switch
            {
                "derecha" => "abajo",
                "abajo" => "izquierda",
                "izquierda" => "arriba",
                "arriba" => "derecha",
                _ => "derecha"
            };
        }

        private static int ObtenerSiguienteInt(int actual)
        {
            return actual switch
            {
                0 => 1,
                1 => 2,
                2 => 3,
                3 => 0,
                _ => 0
            };
        }


        public static void ClickRotar(object sender, EventArgs e, Main form)
        {
            if (barcoSeleccionado == null)
            {
                MessageBox.Show("No hay barco");
                return;
            }
            int cantidadCeldas = barcoSeleccionado.CantidadCeldas;
            Celda? celdaInicio = Main.celdasJuego.FirstOrDefault(c => c.Fila == barcoSeleccionado.Fila && c.Columna == barcoSeleccionado.Columna);
            int indiceCeldaInicio = Main.celdasJuego.IndexOf(celdaInicio);
            Panel p = (Panel)Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaInicio)];

            // resetear las celdas
            foreach (Celda cld in Main.celdasJuego)
            {
                if (cld.ContieneBarco && cld.BarcoId == barcoSeleccionado.Id)
                {
                    cld.ContieneBarco = false;
                    cld.BarcoId = 0;
                }
            }

            List<Celda> celdasAOcupar = [];

            var posicion = EncontrarPosicion(celdaInicio, cantidadCeldas, barcoSeleccionado.Direccion);

            if (posicion.direccion == null)
            {
                MessageBox.Show("No se puede rotar");
                return;
            }

            celdasAOcupar = posicion.celdas;
            barcoSeleccionado.Fila = celdaInicio.Fila;
            barcoSeleccionado.Columna = celdaInicio.Columna;
            barcoSeleccionado.Direccion = posicion.direccion;



            foreach (Celda cld in celdasAOcupar)
            {
                //Main.celdasPosicion[Main.celdasJuego.IndexOf(cld)].BackColor = Color.Blue;
                cld.ContieneBarco = true;
                cld.BarcoId = barcoSeleccionado.Id;
            }
            Point screenPos = p.PointToScreen(Point.Empty);
            Point formPos = form.PointToClient(screenPos);

            barcoImg.Location = formPos;


            switch (posicion.direccion)
            {
                case "arriba":
                    {
                        barcoImg.Width = p.Width;
                        barcoImg.Height = p.Height * barcoSeleccionado.CantidadCeldas;

                        // conseguir el punto mas arriba??
                        int num = barcoSeleccionado.CantidadCeldas - 1;
                        Celda c = Main.celdasJuego[Main.celdasJuego.IndexOf(celdaInicio) - (10 * num)];
                        Control p1 = Main.celdasPosicion[Main.celdasJuego.IndexOf(c)];

                        Point posIzq = p1.PointToScreen(Point.Empty);
                        Point pos = form.PointToClient(posIzq);

                        barcoImg.Location = pos;

                        break;
                    }

                case "izquierda":
                    {
                        barcoImg.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        barcoImg.Height = p.Height;
                        barcoImg.Width = p.Width * barcoSeleccionado.CantidadCeldas;

                        // conseguir el punto mas a izquierda
                        int num = barcoSeleccionado.CantidadCeldas - 1;
                        Celda c = Main.celdasJuego[Main.celdasJuego.IndexOf(celdaInicio) - num];
                        Control p1 = Main.celdasPosicion[Main.celdasJuego.IndexOf(c)];

                        Point posIzq = p1.PointToScreen(Point.Empty);
                        Point pos = form.PointToClient(posIzq);

                        barcoImg.Location = pos;
                        barcoImg.Refresh();
                        break;
                    }
                case "abajo":
                    {
                        barcoImg.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        barcoImg.Width = p.Width;
                        barcoImg.Height = p.Height * barcoSeleccionado.CantidadCeldas;
                        barcoImg.Refresh();
                        break;
                    }
                case "derecha":
                    {
                        barcoImg.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        barcoImg.Height = p.Height;
                        barcoImg.Width = p.Width *  barcoSeleccionado.CantidadCeldas;
                        barcoImg.Refresh();
                        break;
                    };
            }


            barcoImg.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            barcoImg.Refresh();

        }

        public static void SeleccionarBarco(object sender, EventArgs e, Barco barco, Label instruccion, TableLayoutPanel gridJuego, Panel panel, PictureBox barcoPb)
        {
            barcoSeleccionado = barco;
            barcoImg = barcoPb;
            Main.MoviendoBarco = true;

            instruccion.Text = "Haga click en una celda, o aprete esc para salir";
            instruccion.Visible = true;

            panel.Visible = true;
            panel.Controls[1].Text = $"Barco seleccionado: {barcoSeleccionado.NombreBarco}";

            panelSeleccion = panel;

            foreach (Control control in gridJuego.Controls)
            {
                if (control is Panel)
                {
                    control.Cursor = Cursors.Hand;
                }
            }
        }

        public static void SalirModoUbicacion(Label instruccion, TableLayoutPanel gridJuego)
        {
            Main.MoviendoBarco = false;
            instruccion.Visible = false;
            barcoSeleccionado = null;
            panelSeleccion.Visible = false;


            foreach (Control control in gridJuego.Controls)
            {
                if (control is Panel)
                {
                    control.Cursor = Cursors.Default;
                }
            }

        }

        public static (Panel panel, Barco barco, string direccion) ElegirCelda(object sender, EventArgs e, Celda celda, Panel celdaPos, bool rotate=false)
        {
            int cantidadCeldas = barcoSeleccionado.CantidadCeldas;
            Panel panel = null;
            string direccion = "";
            List<Celda> celdasAOcupar = [];


            // resetea las celdas
            foreach (Celda cld in Main.celdasJuego)
            {
                if (cld.ContieneBarco && cld.BarcoId == barcoSeleccionado.Id)
                {
                    cld.ContieneBarco = false;
                    cld.BarcoId = 0;
                }
            }

            var posicion = EncontrarPosicion(celda, cantidadCeldas);

            // no se encontro la posicion
            if (posicion.direccion == null)
                return (null, null, null);

            celdasAOcupar = posicion.celdas;
            panel = posicion.panel;
            direccion = posicion.direccion;

            foreach (Celda celdaAOcupar in celdasAOcupar)
            {
                celdaAOcupar.ContieneBarco = true;
                celdaAOcupar.BarcoId = barcoSeleccionado.Id;
                //Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaAOcupar)].BackColor = Color.Blue;
            }

            barcoSeleccionado.Fila = celda.Fila;
            barcoSeleccionado.Columna = celda.Columna;
            barcoSeleccionado.Direccion = direccion;



            return (panel, barcoSeleccionado, direccion);
        }

        static (Panel panel, List<Celda> celdas, string direccion) EncontrarPosicion(Celda celda, int cantidadCeldas, string dirOmitir="")
        {
            Celda? celdaDerecha = Main.celdasJuego.FirstOrDefault(c => c.Columna == celda.Columna + (cantidadCeldas - 1) && celda.Fila == c.Fila);
            Celda? celdaIzquierda = Main.celdasJuego.FirstOrDefault(c => c.Columna == celda.Columna - (cantidadCeldas - 1) && celda.Fila == c.Fila);
            Celda? celdaArriba = Main.celdasJuego.FirstOrDefault(c => c.Fila == celda.Fila - (cantidadCeldas - 1) && celda.Columna == c.Columna);
            Celda? celdaAbajo = Main.celdasJuego.FirstOrDefault(c => c.Fila == celda.Fila + (cantidadCeldas - 1) && celda.Columna == c.Columna);
            bool lugarEncontrado = false;

            List<Celda> celdasAOcupar = [];
            string direccion = "";
            Panel panel = null;

            List<Func<Celda, int, string, (List<Celda>?, string?, Panel?)>> listaPosicionesBuscar = [BuscarDerecha, BuscarAbajo, BuscarIzquierda, BuscarArriba];

            foreach(var action in listaPosicionesBuscar)
            {
                if (!lugarEncontrado)
                {
                    var result = action(celda, cantidadCeldas, dirOmitir);
                    if (result.Item2 != null)
                    {
                        lugarEncontrado = true;
                        direccion = result.Item2;
                        celdasAOcupar = result.Item1;
                        panel = result.Item3;

                        // hay rotacion
                        if (!string.IsNullOrEmpty(dirOmitir))
                        {
                            int limit = 4;
                            barcoSeleccionado.
                        }

                        break;
                    }
                }
            }

            if (!lugarEncontrado) return (null, null, null);

            return (panel, celdasAOcupar, direccion);
        }

        static (List<Celda>? celdasOcupar, string? direccion, Panel? panel) BuscarDerecha(Celda celda, int cantidadCeldas, string dirOmitir)
        {
            Celda? celdaDerecha = Main.celdasJuego.FirstOrDefault(c => c.Columna == celda.Columna + (cantidadCeldas - 1) && celda.Fila == c.Fila);
            Panel panel = null;

            if (celdaDerecha != null && dirOmitir != "derecha")
            {
                int start = Main.celdasJuego.IndexOf(celda);
                int end = cantidadCeldas;

                List<Celda> celdasOcupar = Main.celdasJuego.GetRange(start, end);


                if (!celdasOcupar.Any(c => c.ContieneBarco))
                {
                    panel = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaDerecha)];
                    return (celdasOcupar, "derecha", panel);
                }
            }

            return (null, null, null);

        }

        static (List<Celda>? celdasOcupar, string? direccion, Panel? panel) BuscarAbajo(Celda celda, int cantidadCeldas, string dirOmitir)
        {
            Celda? celdaAbajo = Main.celdasJuego.FirstOrDefault(c => c.Fila == celda.Fila + (cantidadCeldas - 1) && celda.Columna == c.Columna);

            if (celdaAbajo != null && dirOmitir != "abajo")
            {
                int start = Main.celdasJuego.IndexOf(celda) - cantidadCeldas;
                int end = Main.celdasJuego.IndexOf(celda);

                List<Celda> celdasOcupar = [];
                int i = celda.Fila;
                while (i < celda.Fila + cantidadCeldas)
                {
                    celdasOcupar.Add(Main.celdasJuego.First(c => c.Fila == i && c.Columna == celda.Columna));
                    i++;
                }

                if (!celdasOcupar.Any(c => c.ContieneBarco))
                {
                    return (celdasOcupar, "abajo", Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaAbajo)]);
                }
            }
            return (null, null, null);
        }

        static (List<Celda>? celdasOcupar, string? direccion, Panel? panel) BuscarIzquierda(Celda celda, int cantidadCeldas, string dirOmitir)
        {
            Celda? celdaIzquierda = Main.celdasJuego.FirstOrDefault(c => c.Columna == celda.Columna - (cantidadCeldas - 1) && celda.Fila == c.Fila);
            if (celdaIzquierda != null && dirOmitir != "izquierda")
            {
                // buscar entre medio
                int start = Main.celdasJuego.IndexOf(celda) - cantidadCeldas + 1;
                int end = cantidadCeldas;

                List<Celda> celdasOcupar = Main.celdasJuego.GetRange(start, end);
                if (!celdasOcupar.Any(c => c.ContieneBarco))
                {
                    return (celdasOcupar, "izquierda", Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaIzquierda)]);
                }
            }
            return (null, null, null);
        }

        static (List<Celda>? celdasOcupar, string? direccion, Panel? panel) BuscarArriba(Celda celda, int cantidadCeldas, string dirOmitir)
        {
            Celda? celdaArriba = Main.celdasJuego.FirstOrDefault(c => c.Fila == celda.Fila - (cantidadCeldas - 1) && celda.Columna == c.Columna);

            if (celdaArriba != null && dirOmitir != "arriba")
            {
                int start = Main.celdasJuego.IndexOf(celda) - cantidadCeldas;
                int end = Main.celdasJuego.IndexOf(celda);

                List<Celda> celdasOcupar = [];
                int i = celda.Fila - (cantidadCeldas - 1);
                while (i <= celda.Fila)
                {
                    celdasOcupar.Add(Main.celdasJuego.First(c => c.Fila == i && c.Columna == celda.Columna));
                    i++;
                }

                if (!celdasOcupar.Any(c => c.ContieneBarco))
                {
                    return (celdasOcupar, "arriba", Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaArriba)]);
                }
            }
            return (null, null, null);
        }
    }


}
