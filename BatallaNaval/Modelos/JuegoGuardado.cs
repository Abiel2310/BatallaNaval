using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaNaval.Modelos
{
    internal class JuegoGuardado
    {
        public List<Celda> CeldasJugador { get; set; }
        public List<Celda> CeldasComputadora { get; set; }

        public List<Barco> BarcosJugador { get; set; }
        public List<Barco> BarcosComputadora { get; set; }

        public List<int> CeldasAtacadasJugadorIds { get; set; }
        public List<int> CeldasAtacadasComputadoraIds { get; set; }

        public bool TurnoComputadora { get; set; }

        public string ProximaDireccion { get; set; }
    }
}
