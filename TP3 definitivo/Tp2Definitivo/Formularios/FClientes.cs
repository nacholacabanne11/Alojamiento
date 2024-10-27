using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp2Definitivo
{
    public partial class ClientesA : Form
    {
        public ClientesA()
        {
            InitializeComponent();
        }

        private void ClientesA_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool nombreVacio = tbNombre.Text == "";

            if (nombreVacio)
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbNombre_TextChanged(object sender, EventArgs e)
        {
            btnAgregar.Enabled = !string.IsNullOrWhiteSpace(tbNombre.Text);
        }

        private void dtpNacimiento_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechaNacimiento = dtpNacimiento.Value;
            DateTime fechaActual = DateTime.Now;

            int edad = fechaActual.Year - fechaNacimiento.Year;

            // Ajustar la edad si aún no ha tenido su cumpleaños este año
            if (fechaNacimiento > fechaActual.AddYears(-edad))
            {
                edad--;
            }

            lbEdad.Text = edad.ToString();
            if (edad < 18)
            {
                lbEdad.ForeColor = Color.Red;
            }
        }
    }
}
