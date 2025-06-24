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
        public static Barco? barcoSeleccionado = null;
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
            if (!Main.JuegoEmpezado)
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

        public static (Barco? barco, string? direccion) ElegirCelda(Celda celda, Panel celdaPos, Main form, Button btnRotar, Barco barcoPc=null)
        {
            if (barcoPc != null)
            {
                barcoSeleccionado = barcoPc;
            }
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

            var posicion = EncontrarPosicion(celda, cantidadCeldas, "", (barcoPc != null) ? true : false);
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

            return (barcoSeleccionado, direccion);
        }

        public static (List<Celda> celdas, string direccion) EncontrarPosicion(Celda celda, int cantidadCeldas, string dirOmitir = "", bool turnoPc = false)
        {
            bool lugarEncontrado = false;

            List<Celda> celdasAOcupar = [];
            string direccion = "";

            List<Func<Celda, int, string, List<Celda>, (List<Celda>?, string?)>> lista = [BuscarDerecha, BuscarAbajo, BuscarIzquierda, BuscarArriba];
            var listaPosicionesBuscar = barcoSeleccionado.OrdenRotacion.Select(i => lista[i]).ToList();

            string[] direcciones = { "derecha", "abajo", "izquierda", "arriba" };
            int currentIndex = Array.IndexOf(direcciones, barcoSeleccionado.Direccion);
            bool estaRotando = !string.IsNullOrEmpty(dirOmitir);

            List<Celda> listaCeldas = turnoPc ? Main.celdasEnemigo : Main.celdasJuego;

            for (int i = 0; i < 4; i++)
            {
                int nextIndex = (currentIndex + i) % 4;
                string direccionAProbar = direcciones[nextIndex];

                if (direccionAProbar == dirOmitir)
                    continue;

                var buscarPosicion = lista[nextIndex];
                var resultadoPosicion = buscarPosicion(celda, cantidadCeldas, dirOmitir, listaCeldas);

                if (estaRotando && !turnoPc && resultadoPosicion.Item2 != null)
                {
                    // rotar imagen
                    barcoImg.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    barcoImg.Refresh();
                }

                if (resultadoPosicion.Item2 != null)
                {
                    lugarEncontrado = true;
                    direccion = resultadoPosicion.Item2;
                    celdasAOcupar = resultadoPosicion.Item1;
                    break;
                }

            }

            if (!lugarEncontrado) return (null, null);

            return (celdasAOcupar, direccion);
        }

        static (List<Celda>? celdasOcupar, string? direccion) BuscarDerecha(Celda celda, int cantidadCeldas, string dirOmitir, List<Celda> listaCeldas)
        {
            // se busca una posicion a la derecha de la celda en la que se hizo click
            Celda? celdaDerecha = listaCeldas.FirstOrDefault(c => c.Columna == celda.Columna + (cantidadCeldas - 1) && celda.Fila == c.Fila);

            if (celdaDerecha != null && dirOmitir != "derecha")
            {
                int start = listaCeldas.IndexOf(celda);
                int end = cantidadCeldas;

                List<Celda> celdasOcupar = listaCeldas.GetRange(start, end);
                if (!celdasOcupar.Any(c => c.ContieneBarco))
                {
                    return (celdasOcupar, "derecha");
                }
            }

            return (null, null);

        }

        static (List<Celda>? celdasOcupar, string? direccion) BuscarAbajo(Celda celda, int cantidadCeldas, string dirOmitir, List<Celda> listaCeldas)
        {
            Celda? celdaAbajo = listaCeldas.FirstOrDefault(c => c.Fila == celda.Fila + (cantidadCeldas - 1) && celda.Columna == c.Columna);

            if (celdaAbajo != null && dirOmitir != "abajo")
            {
                int start = listaCeldas.IndexOf(celda) - cantidadCeldas;
                int end = listaCeldas.IndexOf(celda);

                List<Celda> celdasOcupar = [];
                int i = celda.Fila;
                while (i < celda.Fila + cantidadCeldas)
                {
                    celdasOcupar.Add(listaCeldas.First(c => c.Fila == i && c.Columna == celda.Columna));
                    i++;
                }

                if (!celdasOcupar.Any(c => c.ContieneBarco))
                {
                    return (celdasOcupar, "abajo");
                }
            }
            return (null, null);
        }

        static (List<Celda>? celdasOcupar, string? direccion) BuscarIzquierda(Celda celda, int cantidadCeldas, string dirOmitir, List<Celda> listaCeldas)
        {
            Celda? celdaIzquierda = listaCeldas.FirstOrDefault(c => c.Columna == celda.Columna - (cantidadCeldas - 1) && celda.Fila == c.Fila);
            if (celdaIzquierda != null && dirOmitir != "izquierda")
            {
                // buscar entre medio
                int start = listaCeldas.IndexOf(celda) - cantidadCeldas + 1;
                int end = cantidadCeldas;

                List<Celda> celdasOcupar = listaCeldas.GetRange(start, end);
                if (!celdasOcupar.Any(c => c.ContieneBarco))
                {
                    return (celdasOcupar, "izquierda");
                }
            }
            return (null, null);
        }

        static (List<Celda>? celdasOcupar, string? direccion) BuscarArriba(Celda celda, int cantidadCeldas, string dirOmitir, List<Celda> listaCeldas)
        {
            Celda? celdaArriba = listaCeldas.FirstOrDefault(c => c.Fila == celda.Fila - (cantidadCeldas - 1) && celda.Columna == c.Columna);

            if (celdaArriba != null && dirOmitir != "arriba")
            {
                int start = listaCeldas.IndexOf(celda) - cantidadCeldas;
                int end = listaCeldas.IndexOf(celda);

                List<Celda> celdasOcupar = [];
                int i = celda.Fila - (cantidadCeldas - 1);
                while (i <= celda.Fila)
                {
                    celdasOcupar.Add(listaCeldas.First(c => c.Fila == i && c.Columna == celda.Columna));
                    i++;
                }

                if (!celdasOcupar.Any(c => c.ContieneBarco))
                {
                    return (celdasOcupar, "arriba");
                }
            }
            return (null, null);
        }

        public static void PosicionarBarco(string dir, Celda celdaInicio, Main form, Panel p, bool rotando = false)
        {
            // Verificar que no sea nulo
            if (barcoImg == null)
            {
                // Handle computer ships differently (no visual representation needed)
                if (barcoSeleccionado != null && barcoSeleccionado.Id > Main.barcos.Count)
                {
                    // el barco ya esta encontrado??
                    barcoSeleccionado.EnPosicion = true;
                    return;
                }
                else
                {
                    MessageBox.Show("Error: barcoImg es nulo");
                    return;
                }
            }
            
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
                        Celda c = Main.celdasJuego[Main.celdasJuego.IndexOf(celdaInicio) - (Program.tamano * num)];
                        Control p1 = Main.celdasPosicion[Main.celdasJuego.IndexOf(c)];

                        Point posIzq = p1.PointToScreen(Point.Empty);
                        Point pos = form.PointToClient(posIzq);
                        // cambiar el tamano del barco mas chiquito
                        if (barcoSeleccionado.Id == 5)
                        {
                            pos.X += 10;
                            barcoImg.Width = p.Width - 20;
                        }

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
                        // cambiar el tamano del barco mas chiquito
                        if (barcoSeleccionado.Id == 5)
                        {
                            pos.Y += 10;
                            barcoImg.Height = p.Height - 20;
                        }

                        barcoImg.Location = pos;
                        barcoImg.Refresh();
                        break;
                    }
               
                case "abajo":
                    {
                        barcoImg.Width = p.Width;
                        barcoImg.Height = p.Height * barcoSeleccionado.CantidadCeldas;

                        int num = barcoSeleccionado.CantidadCeldas - 1;
                        Celda c = Main.celdasJuego[Main.celdasJuego.IndexOf(celdaInicio) - num];
                        Control p1 = Main.celdasPosicion[Main.celdasJuego.IndexOf(c)];

                        Point posIzq = p1.PointToScreen(Point.Empty);
                        Point pos = form.PointToClient(posIzq);
                        // cambiar el tamano del barco mas chiquito
                        if (barcoSeleccionado.Id == 5)
                        {
                            pos.X += 10;
                            barcoImg.Width = p.Width - 20;
                            barcoImg.Location = pos;
                        }
                        barcoImg.Refresh();
                        break;
                    }
                case "derecha":
                    {
                        barcoImg.Height = p.Height;
                        barcoImg.Width = p.Width * barcoSeleccionado.CantidadCeldas;

                        int num = barcoSeleccionado.CantidadCeldas - 1;
                        Celda c = Main.celdasJuego[Main.celdasJuego.IndexOf(celdaInicio) - num];
                        Control p1 = Main.celdasPosicion[Main.celdasJuego.IndexOf(c)];

                        Point posIzq = p1.PointToScreen(Point.Empty);
                        Point pos = form.PointToClient(posIzq);

                        // cambiar el tamano del barco mas chiquito
                        if (barcoSeleccionado.Id == 5)
                        {
                            pos.Y += 10;
                            barcoImg.Height = p.Height - 20;
                            barcoImg.Location = pos;
                        }
                        barcoImg.Refresh();
                        break;
                    };
            }


            // agregar el barco 
            form.Controls.Add(barcoImg);

            barcoImg.BackColor = SystemColors.ActiveCaption;
            barcoImg.BringToFront();

            barcoSeleccionado.EnPosicion = true;
        }

        public static void ResetState()
        {
            //barcoSeleccionado = null;
            barcoImg = null;
            panelSeleccion = null;
        }
    }


}
