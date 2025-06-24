using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatallaNaval.Modelos;
using BCrypt.Net;
using System.Data.SQLite;

namespace BatallaNaval.PersistenciaC
{
    internal class Login
    {
        public static (bool verificado, Usuario current) Main()
        {
            Console.Clear();
            Console.WriteLine("INICIAR SESION");
            Usuario usuario = new();
            bool verificado = false;

            while (!verificado)
            {
                Console.Write("Ingrese su usuario: ");
                string ingreso = Console.ReadLine();

                Usuario u = VerificarUsuario(ingreso);
                if (u != null)
                {
                    usuario = u;
                    verificado = true;
                }
                else
                {
                    Console.WriteLine("Este usuario no existe. Por favor intente de nuevo...");
                    Console.ReadKey(true);
                }
            }

            bool pwdVerificado = false;
            while (!pwdVerificado)
            {
                Console.Write("Ingrese su contraseña: ");
                string pwd = Console.ReadLine();

                if (AuthHelper.Verificar(pwd, usuario.Pwd))
                {
                    pwdVerificado = true;
                }
                else
                {
                    Console.WriteLine("Su contraseña es incorrecta. Por favor intente de nuevo...");
                    Console.ReadKey(true);
                }
            }

            // iniciar al usuario en el sistema
            Program.loggedIn = true;
            Program.usuarioActual = usuario;

            return (true, usuario);
        }

        public static Usuario VerificarUsuario(string usuario)
        {
            Usuario user = Persistencia.getByUsername(usuario);
            if (user == null)
            {
                return null;
            }
            else { return user; }
        }
    }
}
