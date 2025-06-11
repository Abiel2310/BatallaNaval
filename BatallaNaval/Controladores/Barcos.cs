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


        public static void ClickRotar(object sender, EventArgs e, Main form)
        {
            if (!barcoSeleccionado.EnPosicion)
            {
                MessageBox.Show("Barco no posicionado");
                return;
            }
            int cantidadCeldas = barcoSeleccionado.CantidadCeldas;
            Celda? celdaInicio = Main.celdasJuego.FirstOrDefault(c => c.Fila == barcoSeleccionado.Fila && c.Columna == barcoSeleccionado.Columna);
            int indiceCeldaInicio = Main.celdasJuego.IndexOf(celdaInicio);
            Panel p = (Panel)Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaInicio)];

            // resetear las celdas
            foreach (Celda cld in barcoSeleccionado.CeldasPosicion)
            {
                cld.ContieneBarco = false;
                cld.BarcoId = 0;
            }

            var posicion = EncontrarPosicion(celdaInicio, cantidadCeldas, barcoSeleccionado.Direccion);

            if (posicion.direccion == null)
            {
                MessageBox.Show("No se puede rotar");
                return;
            }

            barcoSeleccionado.CeldasPosicion = posicion.celdas;
            barcoSeleccionado.Fila = celdaInicio.Fila;
            barcoSeleccionado.Columna = celdaInicio.Columna;
            barcoSeleccionado.Direccion = posicion.direccion;



            foreach (Celda cld in barcoSeleccionado.CeldasPosicion)
            {
                cld.ContieneBarco = true;
                cld.BarcoId = barcoSeleccionado.Id;
            }

            PosicionarBarco(posicion.direccion, celdaInicio, form, p, rotando: true);
        }

        public static void SeleccionarBarco(object sender, EventArgs e, Barco barco, Label instruccion, TableLayoutPanel gridJuego, Panel panel, PictureBox barcoPb, Button btnRotar)
        {
            barcoSeleccionado = barco;
            barcoImg = barcoPb;
            Main.MoviendoBarco = true;

            instruccion.Text = "Haga click en una celda, o aprete esc para salir";
            instruccion.Visible = true;

            panel.Visible = true;
            panel.Controls[1].Text = $"Barco seleccionado: {barcoSeleccionado.NombreBarco}";

            panelSeleccion = panel;

            btnRotar.Visible = barcoSeleccionado.EnPosicion;

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
            instruccion.Text = "Haga click en un barco para seleccionarlo";
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

        public static (Barco? barco, string? direccion) ElegirCelda(Celda celda, Panel celdaPos, Main form, Button btnRotar)
        {
            int cantidadCeldas = barcoSeleccionado.CantidadCeldas;

            // resetea las celdas
            if (barcoSeleccionado.CeldasPosicion != null)
            {
                foreach (Celda cld in barcoSeleccionado.CeldasPosicion)
                {
                    cld.ContieneBarco = false;
                    cld.BarcoId = 0;
                }
            }

            var posicion = EncontrarPosicion(celda, cantidadCeldas);
            if (posicion.direccion == null) return (null, null);

            barcoSeleccionado.CeldasPosicion = posicion.celdas;
            string direccion = posicion.direccion;

            foreach (Celda celdaAOcupar in barcoSeleccionado.CeldasPosicion)
            {
                celdaAOcupar.ContieneBarco = true;
                celdaAOcupar.BarcoId = barcoSeleccionado.Id;
            }

            barcoSeleccionado.Fila = celda.Fila;
            barcoSeleccionado.Columna = celda.Columna;
            barcoSeleccionado.Direccion = direccion;


            PosicionarBarco(direccion, celda, form, celdaPos);

            // mostrar button rotar
            barcoSeleccionado.EnPosicion = true;
            btnRotar.Visible = true;

            return (barcoSeleccionado, direccion);
        }

        static (Panel panel, List<Celda> celdas, string direccion) EncontrarPosicion(Celda celda, int cantidadCeldas, string dirOmitir = "")
        {
            Celda? celdaDerecha = Main.celdasJuego.FirstOrDefault(c => c.Columna == celda.Columna + (cantidadCeldas - 1) && celda.Fila == c.Fila);
            Celda? celdaIzquierda = Main.celdasJuego.FirstOrDefault(c => c.Columna == celda.Columna - (cantidadCeldas - 1) && celda.Fila == c.Fila);
            Celda? celdaArriba = Main.celdasJuego.FirstOrDefault(c => c.Fila == celda.Fila - (cantidadCeldas - 1) && celda.Columna == c.Columna);
            Celda? celdaAbajo = Main.celdasJuego.FirstOrDefault(c => c.Fila == celda.Fila + (cantidadCeldas - 1) && celda.Columna == c.Columna);
            bool lugarEncontrado = false;

            List<Celda> celdasAOcupar = [];
            string direccion = "";
            Panel panel = null;

            List<Func<Celda, int, string, (List<Celda>?, string?, Panel?)>> lista = [BuscarDerecha, BuscarAbajo, BuscarIzquierda, BuscarArriba];
            var listaPosicionesBuscar = barcoSeleccionado.OrdenRotacion.Select(i => lista[i]).ToList();

            string[] direcciones = { "derecha", "abajo", "izquierda", "arriba" };
            int currentIndex = Array.IndexOf(direcciones, barcoSeleccionado.Direccion);
            bool estaRotando = !string.IsNullOrEmpty(dirOmitir);

            for (int offset = 0; offset < 4; offset++)
            {
                int nextIndex = (currentIndex + offset) % 4;
                string tryDir = direcciones[nextIndex];

                if (tryDir == dirOmitir)
                    continue;

                if (estaRotando)
                {
                    // rotar imagen
                    barcoImg.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    barcoImg.Refresh();
                }

                var buscarPosicion = lista[nextIndex];
                var result = buscarPosicion(celda, cantidadCeldas, dirOmitir);

                if (result.Item2 != null)
                {
                    lugarEncontrado = true;
                    direccion = result.Item2;
                    celdasAOcupar = result.Item1;
                    panel = result.Item3;
                    break;
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

        static void PosicionarBarco(string dir, Celda celdaInicio, Main form, Panel p, bool rotando = false)
        {
            // eliminar el barco
            if (barcoImg.Parent != null)
                barcoImg.Parent.Controls.Remove(barcoImg);

            // la posicion del barco empieza en la ubicacion del panel en el que se hizo click
            Point formPos = form.PointToClient(p.PointToScreen(Point.Empty));
            barcoImg.Location = formPos;
          
            // logica de rotacion de la imagen cuando no se apreta el boton de rotar
            if (!rotando && barcoImg.Tag is Image originalImg)
            {
                Image nuevaImagen = (Image)originalImg.Clone();

                switch(dir)
                {
                    case "derecha":
                        nuevaImagen.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case "abajo":
                        nuevaImagen.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case "izquierda":
                        nuevaImagen.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    case "arriba":
                        break;
                }
                barcoImg.Image = nuevaImagen;
                barcoImg.Refresh();
            }

            // poner la posicion de la ultima celda del barco
            switch (dir)
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
                        barcoImg.Width = p.Width;
                        barcoImg.Height = p.Height * barcoSeleccionado.CantidadCeldas;
                        barcoImg.Refresh();
                        break;
                    }
                case "derecha":
                    {
                        barcoImg.Height = p.Height;
                        barcoImg.Width = p.Width * barcoSeleccionado.CantidadCeldas;
                        barcoImg.Refresh();
                        break;
                    };
            }

            // agregar el barco 
            form.Controls.Add(barcoImg);

            barcoImg.BackColor = SystemColors.ActiveCaption;
            barcoImg.BringToFront();
        }
    }


}
