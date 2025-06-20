using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatallaNaval.Modelos;

namespace BatallaNaval.Controladores
{
    internal class Juego
    {
        public static (bool contieneBarco, Barco? barcoAtacado, PictureBox? barcoImg) HacerSeleccion(Celda celda, List<PictureBox> imagenesBarco, bool turnoPc = false)
        {
            celda.Atacada = true;

            if (celda.ContieneBarco)
            {
                List<Barco> listaBarcos = turnoPc ? Main.barcos : Main.barcosEnemigo;
                Barco? barcoAtacado = listaBarcos.FirstOrDefault(b => b.Id == celda.BarcoId);
                if (barcoAtacado == null)
                {
                    MessageBox.Show("No se encontro el barco");
                    return (false, null, null);
                }

                PictureBox barcoImg = imagenesBarco[listaBarcos.IndexOf(barcoAtacado)];
                return (true, barcoAtacado, barcoImg);

            }
            return (false, null, null);
        }

        public static bool VerificarFin(List<Celda> celdasAtacadas, List<Barco> barcosObjetivo)
        {
            int totalShipCells = barcosObjetivo.Sum(barco => barco.CantidadCeldas);
            int actualHits = celdasAtacadas.Count(c => c.ContieneBarco);
            if (actualHits >= totalShipCells)
            {
                MessageBox.Show(actualHits + " " + totalShipCells);
            }
            return actualHits >= totalShipCells;
        }
    }
}
