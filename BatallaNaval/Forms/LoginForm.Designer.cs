namespace BatallaNaval.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnLogin;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUsuario = new TextBox();
            this.txtContrasena = new TextBox();
            this.btnLogin = new Button();

            this.SuspendLayout();

            // txtUsuario
            txtUsuario.PlaceholderText = "Usuario";
            txtUsuario.Location = new System.Drawing.Point(50, 30);
            txtUsuario.Width = 200;

            // txtContrasena
            txtContrasena.PlaceholderText = "Contraseña";
            txtContrasena.Location = new System.Drawing.Point(50, 70);
            txtContrasena.Width = 200;
            txtContrasena.PasswordChar = '*';

            // btnLogin
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.Location = new System.Drawing.Point(50, 110);
            btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // LoginForm
            this.ClientSize = new System.Drawing.Size(300, 180);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(txtContrasena);
            this.Controls.Add(btnLogin);
            this.Text = "Iniciar Sesión";

            this.ResumeLayout(false);
        }
    }
}
