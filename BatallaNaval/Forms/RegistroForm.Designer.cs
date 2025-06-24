namespace BatallaNaval.Forms
{
    partial class RegistroForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnRegistro;

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
            this.btnRegistro = new Button();

            this.SuspendLayout();

            // txtUsuario
            txtUsuario.PlaceholderText = "Nuevo Usuario";
            txtUsuario.Location = new System.Drawing.Point(50, 30);
            txtUsuario.Width = 200;

            // txtContrasena
            txtContrasena.PlaceholderText = "Contraseña";
            txtContrasena.Location = new System.Drawing.Point(50, 70);
            txtContrasena.Width = 200;
            txtContrasena.PasswordChar = '*';

            // btnRegistro
            btnRegistro.Text = "Registrarse";
            btnRegistro.Location = new System.Drawing.Point(50, 110);
            btnRegistro.Click += new System.EventHandler(this.btnRegistro_Click);

            // RegistroForm
            this.ClientSize = new System.Drawing.Size(300, 180);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(txtContrasena);
            this.Controls.Add(btnRegistro);
            this.Text = "Registrarse";

            this.ResumeLayout(false);
        }
    }
}
