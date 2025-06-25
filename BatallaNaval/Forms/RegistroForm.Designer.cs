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
            txtUsuario = new TextBox();
            txtContrasena = new TextBox();
            btnRegistro = new Button();
            SuspendLayout();
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(50, 30);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Nuevo Usuario";
            txtUsuario.Size = new Size(200, 27);
            txtUsuario.TabIndex = 0;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(50, 70);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.PlaceholderText = "Contraseña";
            txtContrasena.Size = new Size(200, 27);
            txtContrasena.TabIndex = 1;
            // 
            // btnRegistro
            // 
            btnRegistro.Location = new Point(50, 110);
            btnRegistro.Name = "btnRegistro";
            btnRegistro.Size = new Size(200, 35);
            btnRegistro.TabIndex = 2;
            btnRegistro.Text = "Registrarse";
            btnRegistro.Click += btnRegistro_Click;
            // 
            // RegistroForm
            // 
            ClientSize = new Size(301, 256);
            Controls.Add(txtUsuario);
            Controls.Add(txtContrasena);
            Controls.Add(btnRegistro);
            Name = "RegistroForm";
            Text = "Registrarse";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
