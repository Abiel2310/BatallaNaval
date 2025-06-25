using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaNaval.Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }

        public Usuario() { }

        public Usuario(int id, string username, string pwd)
        {
            Id = id;
            Username = username;
            Pwd = pwd;
        }
    }
}
