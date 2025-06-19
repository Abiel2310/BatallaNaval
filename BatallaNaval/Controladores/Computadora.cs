using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BatallaNaval.Modelos;

namespace BatallaNaval.Controladores
{
    internal class Computadora
    {
        private static Celda? ultimaAtacada = null;

        private static Barco? ultimoBarcoAtacado = null;
        static string proximaDireccionBuscar = "";

        public static void HacerSeleccion(Main form, Button btnRotar)
        {
            foreach (Barco barco in Main.barcos)
            {
                // setear barcos
                Barco barcoEnemigo = new()
                {
                    Id = barco.Id + Main.barcos.Count,
                    CantidadCeldas = barco.CantidadCeldas,
                    NombreBarco = barco.NombreBarco,
                    EnPosicion = false,
                    OrdenRotacion = [0, 1, 2, 3],
                    Direccion = "derecha",
                    Hundido = false
                };

                Main.barcosEnemigo.Add(barcoEnemigo);

                // encontar panel
                Random r = new();


                bool encontrado = false;
                int intentos = 0;
                int intentosMaximos = 100;

                while (!encontrado && intentos < intentosMaximos)
                {
                    List<Celda> celdasLibres = Main.celdasEnemigo.Where(c => !c.ContieneBarco).ToList();
                    Celda celda = celdasLibres[r.Next(celdasLibres.Count)];
                    Panel panelSeleccionado = Main.panelesEnemigo[Main.celdasEnemigo.IndexOf(celda)];

                    var lugar = Barcos.ElegirCelda(celda, panelSeleccionado, form, btnRotar, barcoEnemigo);

                    if (lugar.direccion != null)
                        encontrado = true;

                    intentos++;
                }

                if (!encontrado)
                {
                    MessageBox.Show("No se pudo ubicar el barco enemigo tras múltiples intentos.");
                    return;
                }

            }
        }

        public static bool TurnoComputadora(Label instruccionesLabel, Main form, List<PictureBox> barcosImg)
        {
            instruccionesLabel.Text = "Turno de computadora...";

            // si hay ataques enemigos que acertaron, setear la ultima atacada
            if (Main.celdasAtacadasEnemigo.Count > 0)
            {
                ultimaAtacada = Main.celdasAtacadasEnemigo[^1];
            }


            var seleccionComputadora = ElegirCelda(ultimaAtacada, proximaDireccionBuscar);
            proximaDireccionBuscar = seleccionComputadora.proximaDireccion;


            var seleccion = Juego.HacerSeleccion(seleccionComputadora.celdaElegida, barcosImg, true);
            if (seleccion.contieneBarco)
            {
                CrearImagenFuego(form, seleccionComputadora.panelCelda, seleccion.barcoImg);

                Main.celdasAtacadasEnemigo.Add(seleccionComputadora.celdaElegida);
                ultimoBarcoAtacado = seleccion.barcoAtacado;
            }
            else
            {
                CrearImagenAgua(form, seleccionComputadora.panelCelda);
            }

            seleccionComputadora.celdaElegida.Atacada = true;

            // fijarse si se hundio el barco completo
            if (seleccion.barcoAtacado != null && seleccion.barcoAtacado.CeldasPosicion.All(c => c.Atacada))
            {
                seleccion.barcoAtacado.Hundido = true; 
                proximaDireccionBuscar = "derecha";
            }

            instruccionesLabel.Text = "Haga click en una celda del panel a la derecha";

            // verificar fin de juego
            if (Juego.VerificarFin(Main.celdasAtacadasEnemigo))
            {
                //MessageBox.Show
                return true;
            }
            return false;
        }



        static (Celda celdaElegida, Panel panelCelda, string proximaDireccion) ElegirCelda(Celda ultimaAtacada, string direccionBuscar)
        {
            if (string.IsNullOrEmpty(direccionBuscar)) direccionBuscar = "derecha";

            if (direccionBuscar == "seguirDerecha")
            {
                Celda? celdaDerecha = Main.celdasJuego.FirstOrDefault(celda => celda.Fila == ultimaAtacada.Fila && celda.Columna == ultimaAtacada.Columna + 1);
                if (celdaDerecha == null)
                {
                    // buscar a la izquierda si estamos al final de la fila
                    Celda celdaIzquierda = Main.celdasJuego.First(celda => celda.Fila == ultimaAtacada.Fila && celda.Columna == ultimaAtacada.Columna - 1);
                    Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaIzquierda)];
                    return (celdaIzquierda, panelCelda, "seguirIzquierda");
                }
                else
                {
                    // el barco que estabamos buscando no estaba ahi
                    if (!celdaDerecha.ContieneBarco || celdaDerecha.BarcoId != ultimaAtacada.BarcoId)
                    {
                        // hay que buscar a la izquierda, pero desde la celda en que se hizo click originalmente
                        // recorrer desde la actual hasta la izquierda donde no hay ataque
                        int Columna = celdaDerecha.Columna;
                        Celda? celda = null;
                        while (true)
                        {
                            Columna--;
                            celda = Main.celdasJuego.First(c => c.Fila == celdaDerecha.Fila && c.Columna == Columna);

                            if (!celda.Atacada) break;
                        }

                        Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celda)];
                        return (celda, panelCelda, "seguirIzquierda");
                    }
                    else
                    {
                        Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaDerecha)];
                        return (celdaDerecha, panelCelda, "seguirDerecha");
                    }
                }
            }

            if (direccionBuscar == "seguirIzquierda")
            {
                Celda? celdaIzquierda = Main.celdasJuego.FirstOrDefault(celda => celda.Fila == ultimaAtacada.Fila && celda.Columna == ultimaAtacada.Columna - 1);
                if (celdaIzquierda == null)
                {
                    // buscar a la derecha si estamos al comienzo de la fila
                    Celda celdaDerecha = Main.celdasJuego.First(celda => celda.Fila == ultimaAtacada.Fila && celda.Columna == ultimaAtacada.Columna + 1);
                    Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaDerecha)];
                    return (celdaDerecha, panelCelda, "seguirDerecha");
                }
                else
                {
                    // el barco que estabamos buscando no estaba ahi
                    if (!celdaIzquierda.ContieneBarco || celdaIzquierda.BarcoId != ultimaAtacada.BarcoId)
                    {
                        // hay que buscar a la derecha, pero desde la celda en que se hizo click originalmente
                        // recorrer desde la actual hasta la derecha donde no hay ataque
                        int Columna = celdaIzquierda.Columna;
                        Celda? celda = null;
                        while (true)
                        {
                            celda = Main.celdasJuego.First(c => c.Fila == celdaIzquierda.Fila && c.Columna == Columna);

                            if (!celda.Atacada) break;
                            Columna++;
                        }

                        Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celda)];
                        return (celda, panelCelda, "seguirIzquierda");
                    }
                    else
                    {
                        Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaIzquierda)];
                        return (celdaIzquierda, panelCelda, "seguirIzquierda");
                    }
                }
            }

            if (direccionBuscar == "seguirAbajo")
            {
                Celda? celdaAbajo = Main.celdasJuego.FirstOrDefault(celda => celda.Fila == ultimaAtacada.Fila + 1 && celda.Columna == ultimaAtacada.Columna);
                if (celdaAbajo == null)
                {
                    // buscar a hacia arriba si estamos al final de la columna
                    Celda celdaArriba = Main.celdasJuego.First(celda => celda.Fila == ultimaAtacada.Fila - 1 && celda.Columna == ultimaAtacada.Columna);
                    Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaArriba)];
                    return (celdaArriba, panelCelda, "seguirArriba");
                }
                else
                {
                    // el barco que estabamos buscando no estaba ahi
                    if (!celdaAbajo.ContieneBarco || celdaAbajo.BarcoId != ultimaAtacada.BarcoId)
                    {
                        // hay que buscar hacia arriba, pero desde la celda en que se hizo click originalmente
                        // recorrer desde la actual hasta la derecha donde no hay ataque
                        int Fila = celdaAbajo.Fila;
                        Celda? celda = null;
                        while (true)
                        {
                            Fila--;
                            celda = Main.celdasJuego.First(c => c.Fila == Fila && c.Columna == celdaAbajo.Columna);

                            if (!celda.Atacada) break;
                        }

                        Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celda)];
                        return (celda, panelCelda, "seguirArriba");
                    }
                    else
                    {
                        Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaAbajo)];
                        return (celdaAbajo, panelCelda, "seguirAbajo");
                    }
                }
            }

            if (direccionBuscar == "seguirArriba")
            {
                Celda? celdaArriba = Main.celdasJuego.FirstOrDefault(celda => celda.Fila == ultimaAtacada.Fila - 1 && celda.Columna == ultimaAtacada.Columna);
                if (celdaArriba == null)
                {
                    // buscar a abajo si estamos al comienzo de la columna
                    Celda celdaAbajo = Main.celdasJuego.First(celda => celda.Fila == ultimaAtacada.Fila + 1 && celda.Columna == ultimaAtacada.Columna);
                    Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaAbajo)];
                    return (celdaAbajo, panelCelda, "seguirAbajo");
                }
                else
                {
                    // el barco que estabamos buscando no estaba ahi
                    if (!celdaArriba.ContieneBarco || celdaArriba.BarcoId != ultimaAtacada.BarcoId)
                    {
                        // hay que buscar hacia arriba, pero desde la celda en que se hizo click originalmente
                        // recorrer desde la actual hasta la derecha donde no hay ataque
                        int Fila = celdaArriba.Fila;
                        Celda? celda = null;
                        while (true)
                        {
                            Fila++;
                            celda = Main.celdasJuego.First(c => c.Fila == Fila && c.Columna == celdaArriba.Columna);

                            if (!celda.Atacada) break;
                        }

                        Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celda)];
                        return (celda, panelCelda, "seguirAbajo");
                    }
                    else
                    {
                        Panel panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaArriba)];
                        return (celdaArriba, panelCelda, "seguirArriba");
                    }
                }
            }


            // si el ultimo ataque no existe o el ultimo barco fue hundido retornar random
            if (ultimoBarcoAtacado == null || ultimoBarcoAtacado?.Hundido == true)
            {
                Random r = new();
                List<Celda> listaCeldas = Main.celdasJuego.Where(celda => !celda.Atacada).ToList();
                Celda celda = listaCeldas[r.Next(listaCeldas.Count)];
                Panel panelCelda = Main.celdasPosicion.First(panel => (int)panel.Tag == celda.Id);
                return (celda, panelCelda, "");
            }

            // si el ultimo ataque tiene barco
            else
            {
                Celda? celdaProbar = null;
                Panel? panelCelda = null;


                bool encontrada = false;
                if (!encontrada && direccionBuscar == "derecha")
                {
                    celdaProbar = Main.celdasJuego.FirstOrDefault(c => c.Fila == ultimaAtacada.Fila && !c.Atacada && c.Columna == ultimaAtacada.Columna + 1);
                    encontrada = true;
                }

                if (!encontrada && direccionBuscar == "abajo")
                {
                    celdaProbar = Main.celdasJuego.FirstOrDefault(c => c.Fila == ultimaAtacada.Fila + 1 && !c.Atacada && c.Columna == ultimaAtacada.Columna);
                    encontrada = true;
                }

                if (!encontrada && direccionBuscar == "izquierda")
                {
                    celdaProbar = Main.celdasJuego.FirstOrDefault(c => c.Fila == ultimaAtacada.Fila && !c.Atacada && c.Columna == ultimaAtacada.Columna - 1);
                    encontrada = true;
                }

                if (!encontrada && direccionBuscar == "arriba")
                {
                    celdaProbar = Main.celdasJuego.FirstOrDefault(c => c.Fila == ultimaAtacada.Fila - 1 && !c.Atacada && c.Columna == ultimaAtacada.Columna);
                    encontrada = true;
                }



                if (celdaProbar != null)
                {
                    if (!celdaProbar.ContieneBarco)
                    {
                        panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaProbar)];
                        string dir = ProximaDireccion(direccionBuscar);
                        return (celdaProbar, panelCelda, dir);
                    }
                    else
                    {
                        panelCelda = Main.celdasPosicion[Main.celdasJuego.IndexOf(celdaProbar)];
                        Barco barcoEnCelda = Main.barcos.First(b => b.Id == celdaProbar.BarcoId);
                        return (celdaProbar, panelCelda, "seguir" + char.ToUpper(barcoEnCelda.Direccion[0]) + barcoEnCelda.Direccion.Substring(1));
                    }
                }
                else
                {
                    Random r = new();
                    List<Celda> listaCeldas = Main.celdasJuego.Where(celda => !celda.Atacada).ToList();
                    Celda celda = listaCeldas[r.Next(listaCeldas.Count)];
                    Panel panel = Main.celdasPosicion.First(p => (int)p.Tag == celda.Id);
                    return (celda, panel, "");
                }
            }
        }

        static void CrearImagenFuego(Main form, Panel panelCelda, PictureBox barcoImg)
        {
            int tamanoExtra = 13;
            PictureBox fuego = new();
            fuego.Image = Image.FromFile("../../../Resources/explosion.png");

            fuego.SizeMode = PictureBoxSizeMode.StretchImage;
            fuego.Width = panelCelda.Width + tamanoExtra;
            fuego.Height = panelCelda.Height + tamanoExtra;
            fuego.BackColor = Color.Transparent;

            Point posicionPanel = panelCelda.PointToScreen(Point.Empty);
            Point pos = barcoImg.PointToClient(posicionPanel);
            pos.X -= tamanoExtra / 2;
            pos.Y -= (tamanoExtra / 2) + 10;

            fuego.Parent = barcoImg;
            fuego.Location = pos;
            barcoImg.Controls.Add(fuego);
            fuego.BringToFront();
        }
        static void CrearImagenAgua(Main form, Panel panelCelda)
        {
            int tamanoExtra = 13;
            PictureBox agua = new();
            agua.Image = Image.FromFile("../../../Resources/agua.png");

            agua.SizeMode = PictureBoxSizeMode.StretchImage;
            agua.Width = panelCelda.Width - tamanoExtra;
            agua.Height = panelCelda.Height - tamanoExtra;
            agua.BackColor = SystemColors.ActiveCaption;
            agua.Anchor = AnchorStyles.None;

            Point posicionPanel = panelCelda.PointToScreen(Point.Empty);
            Point pos = form.PointToClient(posicionPanel);
            pos.X += (tamanoExtra / 3) + 2;

            agua.Location = pos;

            form.Controls.Add(agua);
            agua.BringToFront();
        }
        private static string ProximaDireccion(string direccion)
        {
            return direccion switch
            {
                "derecha" => "abajo",
                "abajo" => "izquierda",
                "izquierda" => "arriba",
                "arriba" => "derecha",
                _ => "derecha"
            };
        }

        public static void ResetState()
        {
            ultimaAtacada = null;
            ultimoBarcoAtacado = null;
            proximaDireccionBuscar = "";
        }
    }
}
