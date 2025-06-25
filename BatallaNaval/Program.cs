using BatallaNaval.Forms;
using BatallaNaval.Modelos;
using BatallaNaval.PersistenciaC;

namespace BatallaNaval
{
    internal static class Program
    {
        public static bool loggedIn = false;
        public static Usuario usuarioActual = new();
        public static int tamano = 0;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]

        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Conexion.OpenConnection();
            Application.Run(new Autenticacion());
        }
    }
}