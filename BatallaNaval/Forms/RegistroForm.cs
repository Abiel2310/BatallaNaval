using System;
using System.Windows.Forms;
using BatallaNaval.Modelos;
using BatallaNaval.PersistenciaC;
using System.Data.SQLite;

namespace BatallaNaval.Forms
{
    public partial class RegistroForm : Form
    {
        public RegistroForm()
        {
            InitializeComponent();
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string pwd = txtContrasena.Text;

            if (pwd.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.");
                return;
            }

            SQLiteCommand cmd = new("SELECT id FROM user WHERE username=@usuario");
            cmd.Parameters.Add(new SQLiteParameter("@usuario", usuario));
            cmd.Connection = Conexion.Connection;

            SQLiteDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Ese usuario ya está registrado.");
                return;
            }

            string hash = AuthHelper.Hashear(pwd);
            int id = Persistencia.Save(usuario, hash);

            Usuario u = new(id, usuario, pwd);
            Program.loggedIn = true;
            Program.usuarioActual = u;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
