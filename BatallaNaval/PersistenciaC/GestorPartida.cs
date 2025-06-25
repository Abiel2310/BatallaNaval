using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BatallaNaval.Modelos;
using System.Text.Json;
using System.Data.SQLite;

namespace BatallaNaval.PersistenciaC
{
    internal class GestorPartida
    {
        
        public static void GuardarPartida(JuegoGuardado estado)
        {
            for (int i = 0; i < estado.CeldasJugador.Count; i++)
            {
                SQLiteCommand cmd = new("INSERT INTO celda (id, contieneBarco, barcoId, atacada, fila, columna, userId, jugador) VALUES (@id, @contieneBarco, @barcoId, @atacada, @fila, @columna, @userId, @jugador)");
                cmd.Connection = Conexion.Connection;
                cmd.Parameters.Add(new SQLiteParameter("@id", estado.CeldasJugador[i].Id));
                cmd.Parameters.Add(new SQLiteParameter("@contieneBarco", estado.CeldasJugador[i].ContieneBarco));
                cmd.Parameters.Add(new SQLiteParameter("@barcoId", estado.CeldasJugador[i].BarcoId));
                cmd.Parameters.Add(new SQLiteParameter("@atacada", estado.CeldasJugador[i].Atacada));
                cmd.Parameters.Add(new SQLiteParameter("@fila", estado.CeldasJugador[i].Fila));
                cmd.Parameters.Add(new SQLiteParameter("@columna", estado.CeldasJugador[i].Columna));
                cmd.Parameters.Add(new SQLiteParameter("@userId", Program.usuarioActual.Id));
                cmd.Parameters.Add(new SQLiteParameter("@jugador", "true"));

                cmd.ExecuteNonQuery();
            }
            for (int i = 0; i < estado.CeldasComputadora.Count; i++)
            {
                SQLiteCommand cmd = new("INSERT INTO celda (id, contieneBarco, barcoId, atacada, fila, columna, userId, jugador) VALUES (@id, @contieneBarco, @barcoId, @atacada, @fila, @columna, @userId, @jugador)");
                cmd.Connection = Conexion.Connection;
                cmd.Parameters.Add(new SQLiteParameter("@id", estado.CeldasComputadora[i].Id));
                cmd.Parameters.Add(new SQLiteParameter("@contieneBarco", estado.CeldasComputadora[i].ContieneBarco));
                cmd.Parameters.Add(new SQLiteParameter("@barcoId", estado.CeldasComputadora[i].BarcoId));
                cmd.Parameters.Add(new SQLiteParameter("@atacada", estado.CeldasComputadora[i].Atacada));
                cmd.Parameters.Add(new SQLiteParameter("@fila", estado.CeldasComputadora[i].Fila));
                cmd.Parameters.Add(new SQLiteParameter("@columna", estado.CeldasComputadora[i].Columna));
                cmd.Parameters.Add(new SQLiteParameter("@userId", Program.usuarioActual.Id));
                cmd.Parameters.Add(new SQLiteParameter("@jugador", "false"));

                cmd.ExecuteNonQuery();
            }
            for (int i = 0; i < estado.BarcosJugador.Count; i++)
            {
                SQLiteCommand cmd = new("INSERT INTO barco (id, cantidadCeldas, nombre, fila, columna, hundido, userId) VALUES (@id, @cantidadCeldas, @nombre, @fila, @columna, @hundido, @userId)");
                cmd.Connection = Conexion.Connection;
                cmd.Parameters.Add(new SQLiteParameter("@id", estado.BarcosJugador[i].Id));
                cmd.Parameters.Add(new SQLiteParameter("@cantidadCeldas", estado.BarcosJugador[i].CantidadCeldas));
                cmd.Parameters.Add(new SQLiteParameter("@nombre", estado.BarcosJugador[i].NombreBarco));
                cmd.Parameters.Add(new SQLiteParameter("@fila", estado.BarcosJugador[i].Fila));
                cmd.Parameters.Add(new SQLiteParameter("@columna", estado.BarcosJugador[i].Columna));
                cmd.Parameters.Add(new SQLiteParameter("@hundido", "false"));
                cmd.Parameters.Add(new SQLiteParameter("@userId", Program.usuarioActual.Id));

                cmd.ExecuteNonQuery();
            }
            for (int i = 0; i < estado.BarcosComputadora.Count; i++)
            {
                SQLiteCommand cmd = new("INSERT INTO barco (id, cantidadCeldas, nombre, fila, columna, hundido, userId) VALUES (@id, @cantidadCeldas, @nombre, @fila, @columna, @hundido, @userId)");
                cmd.Connection = Conexion.Connection;
                cmd.Parameters.Add(new SQLiteParameter("@id", estado.BarcosComputadora[i].Id));
                cmd.Parameters.Add(new SQLiteParameter("@cantidadCeldas", estado.BarcosComputadora[i].CantidadCeldas));
                cmd.Parameters.Add(new SQLiteParameter("@nombre", estado.BarcosComputadora[i].NombreBarco));
                cmd.Parameters.Add(new SQLiteParameter("@fila", estado.BarcosComputadora[i].Fila));
                cmd.Parameters.Add(new SQLiteParameter("@columna", estado.BarcosComputadora[i].Columna));
                cmd.Parameters.Add(new SQLiteParameter("@hundido", "false"));
                cmd.Parameters.Add(new SQLiteParameter("@userId", Program.usuarioActual.Id));

                cmd.ExecuteNonQuery();
            }
        }

        public static JuegoGuardado? CargarPartida()
        {
            List<Celda> celdasJ = new();
            List<Celda> celdasC = new();
            List<Barco> barcosJ = new();
            List<Barco> barcosC = new();
            List<int> celAtJ = new();
            List<int> celAtC = new();


            SQLiteCommand cmd = new("SELECT * FROM celda WHERE userId = @userId");
            cmd.Connection = Conexion.Connection;
            cmd.Parameters.Add(new SQLiteParameter("@userId", Program.usuarioActual.Id));

            SQLiteCommand cmd2 = new("SELECT * FROM barco WHERE userId = @userId");
            cmd2.Connection = Conexion.Connection;
            cmd2.Parameters.Add(new SQLiteParameter("@userId", Program.usuarioActual.Id));

            Celda celdas = new();
            Barco barcos = new();


            SQLiteDataReader dr = cmd.ExecuteReader();
            SQLiteDataReader dr2 = cmd2.ExecuteReader();
            while (dr.Read()) 
            {
                celdas.Id = dr.GetInt32(0);
                celdas.ContieneBarco = dr.GetBoolean(1);
                celdas.BarcoId = dr.GetInt32(2);
                celdas.Atacada = dr.GetBoolean(3);
                celdas.Fila = dr.GetInt32(4);
                celdas.Columna = dr.GetInt32(5);

                if (dr.GetString(7) == "true")
                {
                    celdasJ.Add(celdas);
                }
                else
                {
                    celdasC.Add(celdas);
                }
            }
            while (dr2.Read())
            {
                barcos.Id = dr2.GetInt32(0);
                barcos.CantidadCeldas = dr2.GetInt32(1);
                barcos.NombreBarco = dr2.GetString(2);
                barcos.Fila = dr2.GetInt32(3);
                barcos.Columna = dr2.GetInt32(4);
                barcos.Hundido = dr2.GetBoolean(5);

                if (dr.GetString(7) == "true")
                {
                    barcosJ.Add(barcos);
                }
                else
                {
                    barcosC.Add(barcos);
                }
            }
            for (int i = 0; i < celdasJ.Count; i++)
            {
                if (celdasJ[i].Atacada == true)
                {
                    celAtJ.Add(celdasJ[i].Id);
                }
            }
            for (int i = 0; i < celdasC.Count; i++)
            {
                if (celdasC[i].Atacada == true)
                {
                    celAtC.Add(celdasC[i].Id);
                }
            }
            JuegoGuardado partida = new()
            {
                CeldasJugador = celdasJ,
                CeldasComputadora = celdasC,
                BarcosJugador = barcosJ,
                BarcosComputadora = barcosC,
                CeldasAtacadasJugadorIds = celAtJ,
                CeldasAtacadasComputadoraIds = celAtC,
                TurnoComputadora = false,
                ProximaDireccion = "derecha"
            };
            return partida;
        }

        public static void BorrarPartida()
        {
            SQLiteCommand cmd = new("DELETE FROM celda WHERE userId = @userId");
            cmd.Connection = Conexion.Connection;
            cmd.Parameters.Add(new SQLiteParameter("@userId", Program.usuarioActual.Id));
            cmd.ExecuteNonQuery();

            SQLiteCommand cmd2 = new("DELETE FROM barco WHERE userId = @userId");
            cmd2.Connection = Conexion.Connection;
            cmd2.Parameters.Add(new SQLiteParameter("@userId", Program.usuarioActual.Id));
            cmd2.ExecuteNonQuery();
        }
    }
}
