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
            txtUsuario = new TextBox();
            txtContrasena = new TextBox();
            btnLogin = new Button();
            SuspendLayout();
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(61, 37);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.PlaceholderText = "Usuario";
            txtUsuario.Size = new Size(200, 27);
            txtUsuario.TabIndex = 0;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(61, 79);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.PlaceholderText = "Contraseña";
            txtContrasena.Size = new Size(200, 27);
            txtContrasena.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(61, 123);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(200, 33);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.Click += btnLogin_Click;
            // 
            // LoginForm
            // 
            ClientSize = new Size(332, 296);
            Controls.Add(txtUsuario);
            Controls.Add(txtContrasena);
            Controls.Add(btnLogin);
            Name = "LoginForm";
            Text = "Iniciar Sesión";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
