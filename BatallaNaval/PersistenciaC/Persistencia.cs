using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatallaNaval.Modelos;

namespace BatallaNaval.PersistenciaC
{
    internal class Persistencia
    {
        public static int Save(string usuario, string pwd)
        {
            SQLiteCommand cmd = new("INSERT INTO user (username, pwd) VALUES (@usuario, @pwd)");
            cmd.Connection = Conexion.Connection;

            cmd.Parameters.Add(new SQLiteParameter("@usuario", usuario));
            cmd.Parameters.Add(new SQLiteParameter("@pwd", pwd));

            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("SELECT last_insert_rowid()", Conexion.Connection);
            long lastId = (long)cmd.ExecuteScalar();
            return (int)lastId;
        }
        public static Usuario getByUsername(string usuario)
        {
            SQLiteCommand cmd = new("SELECT * FROM user WHERE username=@usuario");
            cmd.Connection = Conexion.Connection;

            cmd.Parameters.Add(new SQLiteParameter("@usuario", usuario));

            SQLiteDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new Usuario(
                    dr.GetInt32(0),
                    dr.GetString(1),
                    dr.GetString(2)
                );
            }
            return null;
        }
        public static Usuario getById(int id)
        {
            SQLiteCommand cmd = new("SELECT * FROM user WHERE id=@id");
            cmd.Connection = Conexion.Connection;

            cmd.Parameters.Add(new SQLiteParameter("@id", id));

            Usuario user = new();
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                user.Id = dr.GetInt32(0);
                user.Username = dr.GetString(1);
                user.Pwd = dr.GetString(2);
            }

            return user;
        }
    }
}
