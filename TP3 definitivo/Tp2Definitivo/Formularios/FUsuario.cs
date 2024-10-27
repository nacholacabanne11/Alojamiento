using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tp2Definitivo.Clases;

namespace Tp2Definitivo.Formularios
{
    public partial class FUsuario : Form
    {
        public FUsuario() //Este formulario aparece cuando creamos un nuevo usuario
        {
            InitializeComponent();
        }
        public FUsuario(Usuario user) //Cuando queremos modificar la pass de un usuario abrimos con este
        {
            InitializeComponent();
            tbId.Text = user.Id.ToString();
            tbId.Enabled = false;
            tbPass.Text = user.Pass;
            
            if (user.Tipo ==1)
            {
                rbAdmin.Checked = true;
                rbAdmin.Enabled = false;
                rbEmpleado.Checked = false;
                rbEmpleado.Enabled = false;
            }
            else
            {
                rbAdmin.Checked = false;
                rbAdmin.Enabled = false;
                rbEmpleado.Checked = true;
                rbEmpleado.Enabled = false;
            }
            btnNuevoUsuario.Text = "Cambiar contraseña";
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            bool faltaID = (tbId.Text == "");
            bool faltaPass = (tbPass.Text == "");
            string passUser = tbPass.Text;
            if (faltaID && faltaPass)
            {
                MessageBox.Show("¡No deje los campos en blanco!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }

            else if (faltaID || faltaPass)
            {
                if (faltaID)
                {
                    MessageBox.Show("¡Debe ingresar un ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                }
                if (faltaPass)
                {
                    MessageBox.Show("¡Debe ingresar una contraseña!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                }
            }
        }

        private void tbId_TextChanged(object sender, EventArgs e)
        {
            if (tbId.Text != "")
            {
                if (!int.TryParse(tbId.Text, out int numero))
                {
                    MessageBox.Show("Por favor, ingrese un valor numérico válido."); // Si no se puede analizar como número, muestra un mensaje de error
                    tbId.Clear(); // Limpia el TextBox
                }
            }
        }
    }
}
