using System;
using System.Windows.Forms;
using BatallaNaval.Modelos;
using BatallaNaval.PersistenciaC;

namespace BatallaNaval.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string pwd = txtContrasena.Text;

            Usuario u = Login.VerificarUsuario(usuario);

            if (u == null)
            {
                MessageBox.Show("Usuario no encontrado.");
                return;
            }

            if (!AuthHelper.Verificar(pwd, u.Pwd))
            {
                MessageBox.Show("Contraseña incorrecta.");
                return;
            }

            Program.loggedIn = true;
            Program.usuarioActual = u;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
