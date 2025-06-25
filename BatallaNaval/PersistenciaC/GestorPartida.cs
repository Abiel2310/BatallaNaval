using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BatallaNaval.Modelos;
using System.Text.Json;

namespace BatallaNaval.PersistenciaC
{
    internal class GestorPartida
    {
        private static string rutaArchivo = "partida_guardada.json";

        public static void GuardarPartida(JuegoGuardado estado)
        {
            var opciones = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(estado, opciones);
            File.WriteAllText(rutaArchivo, json);
        }

        public static JuegoGuardado? CargarPartida()
        {
            if (!File.Exists(rutaArchivo))
                return null;

            string json = File.ReadAllText(rutaArchivo);
            return JsonSerializer.Deserialize<JuegoGuardado>(json);
        }

        public static void BorrarPartida()
        {
            if (File.Exists(rutaArchivo))
                File.Delete(rutaArchivo);
        }
    }
}
