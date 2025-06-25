using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatallaNaval.Modelos;

namespace BatallaNaval.PersistenciaC
{
    internal class Registrar
    {
        public static void Registrarse()
        {
            Console.Clear();
            Console.WriteLine("REGISTRARSE");
            bool usuarioVerificado = false;
            string usuario = "";

            while (!usuarioVerificado)
            {
                Console.Write("Ingrese su usuario: ");
                var user = CrearUsuario();

                usuarioVerificado = user.verificado;
                usuario = user.usuario;
            }


            string pwd = "";
            while (pwd.Length < 6)
            {
                Console.Write("Ingrese su contrasena: ");
                string ingreso = Console.ReadLine();
                if (ingreso.Length >= 6)
                {
                    pwd = ingreso;
                    break;
                }

                Console.Write("Su contrasena debe tener por lo menos 6 caracteres. Por favor intente de nuevo...");
                Console.ReadKey(true);

                pwd = ingreso;
            }

            string hash = AuthHelper.Hashear(pwd);
            int id = Persistencia.Save(usuario, hash);

            Usuario u = new(id, usuario, pwd);

            // iniciar al usuario en el sistema
            Program.loggedIn = true;
            Program.usuarioActual = u;
        }

        public static (bool verificado, string? usuario) CrearUsuario()
        {
            string usuario = Console.ReadLine();
            bool verificado = true;

            if (string.IsNullOrEmpty(usuario))
            {
                Console.WriteLine("Su usuario no puede ser nulo. Por favor intente de nuevo...");
                Console.ReadKey(true);

                return (false, null);
            }

            SQLiteCommand cmd = new("SELECT id FROM user WHERE usuario=@usuario");
            cmd.Parameters.Add(new SQLiteParameter("@usuario", usuario));
            cmd.Connection = Conexion.Connection;

            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                verificado = false;
            }

            if (!verificado)
            {
                Console.WriteLine("Este usuario ya esta en uso. Por favor intente de nuevo...");
                Console.ReadKey(true);
            }


            return (verificado, usuario);
        }
    }
}
