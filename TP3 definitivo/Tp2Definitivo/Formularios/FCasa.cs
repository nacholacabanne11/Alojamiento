using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Drawing.Imaging;
using Tp2Definitivo.Clases;

namespace Tp2Definitivo
{
    public partial class CasasA : Form
    {
        Propietario[] propietarios;
        public CasasA(Propietario[] propietarios)
        {
            InitializeComponent();
            this.propietarios = propietarios;
        }

        public CasasA(int id, Propietario[] propietarios)
        {
            InitializeComponent();
            this.propietarios = propietarios;
            tbNroCasa.Text = id.ToString();
            tbNroCasa.Enabled = false;
            CargarPropietarios();
        }
        private void CargarPropietarios()
        {
            foreach (Propietario p in propietarios)
            {
                cbPropietarios.Items.Add(p.ObtenerCodigo());
            }
        }
        private void tbNroCasa_TextChanged(object sender, EventArgs e)
        {

        }

        private void imagen1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var a = GetCodecString();
            openFileDialog.Filter = a;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string urlArchivo = openFileDialog.FileName;
                imagen1.Load(urlArchivo);
            }
        }

        private void imagen2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var a = GetCodecString();
            openFileDialog.Filter = a;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string urlArchivo = openFileDialog.FileName;
                imagen2.Load(urlArchivo);
            }
        }

        private void imagen3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var a = GetCodecString();
            openFileDialog.Filter = a;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string urlArchivo = openFileDialog.FileName;
                imagen3.Load(urlArchivo);
            }
        }

        private void imagen4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var a = GetCodecString();
            openFileDialog.Filter = a;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string urlArchivo = openFileDialog.FileName;
                imagen4.Load(urlArchivo);
            }
        }
        private string GetCodecString()
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            var codecFilter = "Image Files|";
            foreach (var codec in codecs)
            {
                codecFilter += codec.FilenameExtension + ";";
            }
            return codecFilter;
        }
    }
}
