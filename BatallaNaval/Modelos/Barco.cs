using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaNaval.Modelos
{
    internal class Barco
    {
        public int Id { get; set; }
        public int CantidadCeldas { get; set; }
        public string NombreBarco  { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool EnPosicion { get; set; }
        public List<Celda>? CeldasPosicion { get; set; }
        public string Direccion { get; set; }
        public List<int> OrdenRotacion { get; set; }


    }
}
