using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatallaNaval.Modelos;

namespace BatallaNaval.Controladores
{
    internal class Computadora
    {
        public static void HacerSeleccion(Main form, Button btnRotar)
        {
            foreach (Barco barco in Main.barcos)
            {
                // setear barcos
                Barco barcoEnemigo = new();

                barcoEnemigo.Id = barco.Id + Main.barcos.Count;
                barcoEnemigo.CantidadCeldas = barco.CantidadCeldas;
                barcoEnemigo.NombreBarco = barco.NombreBarco;
                barcoEnemigo.EnPosicion = false;
                barcoEnemigo.OrdenRotacion = [0, 1, 2, 3];
                barcoEnemigo.Direccion = "derecha";

                Main.barcosEnemigo.Add(barcoEnemigo);

                // encontar panel
                Random r = new();


                bool encontrado = false;
                int intentos = 0;
                int intentosMaximos = 100;

                while (!encontrado && intentos < intentosMaximos)
                {
                    List<Celda> celdasLibres = Main.celdasEnemigo.Where(c => !c.ContieneBarco).ToList();
                    Celda celda = Main.celdasEnemigo[r.Next(celdasLibres.Count)];
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

                foreach (Celda c in barcoEnemigo.CeldasPosicion)
                {
                    Panel pn = Main.panelesEnemigo[Main.celdasEnemigo.IndexOf(c)];
                    pn.BackColor = Color.Red;
                }

            }
        }
    }
}
