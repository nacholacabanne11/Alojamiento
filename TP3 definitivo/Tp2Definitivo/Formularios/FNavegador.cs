using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp2Definitivo.Formularios
{
    public partial class FNavegador : Form
    {
        public FNavegador()
        {
            InitializeComponent();
        }

        private void FNavegador_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://www.google.com");
            string directorioBase = AppDomain.CurrentDomain.BaseDirectory;
            string carpetaImagenes = Path.Combine(directorioBase, "Ayuda");
            string rutaNavegador = Path.Combine(carpetaImagenes, "ayuda.html");
            webBrowser1.Navigate(rutaNavegador);
        }
    }
}
