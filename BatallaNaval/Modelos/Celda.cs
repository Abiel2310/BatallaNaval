using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaNaval.Modelos
{
    public class Celda
    {
        public int Id { get; set; }
        //public int NumeroCelda { get; set; }
        public bool ContieneBarco { get; set; }
        public int BarcoId{ get; set; }
        public bool Atacada { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
    }
}
