using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MVC
{
    public partial class fmr_login : Form
    {
        public fmr_login()
        {
            InitializeComponent();
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void fmr_login_Load(object sender, EventArgs e)
        {

        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            usuarios cls_usuarios = new usuarios();
            login datosLogin = new login { Contrasenia = txt_contrasenia.Text, NombreUsuario = txt_usuario.Text };

            Usuarios dtousuario = new Usuarios();

            dtousuario = cls_usuarios.Login(datosLogin);

            var usuario = txt_usuario.Text;
            var constrasenia = txt_contrasenia.Text;
            if (usuario == dtousuario.NombreUsuario && constrasenia == dtousuario.Contrasenia)
            {
                MessageBox.Show("Bienvenido al sistema");
                this.Hide();
                frm_cuadro_mando frm_Cuadro_Mando = new frm_cuadro_mando();
                frm_Cuadro_Mando.Show();
            }
            else
            {
                MessageBox.Show("El usuario o la contrasenia son incorrectos");
            }
        }
    }
}