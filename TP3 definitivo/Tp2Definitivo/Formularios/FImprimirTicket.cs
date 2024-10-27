using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Tp2Definitivo.Formularios
{
    public partial class FImprimirTicket : Form
    {
        Reserva[] ticketsEmitidos;
        Reserva ticket;
        Empresa miEmpresa;

        string linea;    // línea que se está imprimiendo
        Brush pincel;   // Relleno con el que se pintarán las letras
        Pen lapiz;       // Borde con el que se dibujará el marco de página
        Font fuente;
        bool conCopia = true;
        int contadorLinea;
        int numeroPagina;
        Image logo;
        string leyenda = "Código honesto";
        Image fotoPropiedad;
        private bool imprimirOriginal = true;

        public FImprimirTicket(Empresa miEmpresa)
        {
            InitializeComponent();
            this.miEmpresa = miEmpresa;
            this.ticketsEmitidos = miEmpresa.VerTickets;
            contadorLinea = 0;
            numeroPagina = 1;
            ConfigurarGrilla();
            RefrescarGrillaReservas();
            if (miEmpresa.Logo == null)
            {
                //Obtiene la ruta de la imagen de la empresa
                string directorioBase = AppDomain.CurrentDomain.BaseDirectory;
                string carpetaImagenes = Path.Combine(directorioBase, "Imagenes");
                string rutaImagen = Path.Combine(carpetaImagenes, "logoEmpresa.png");
                logo = Image.FromFile(rutaImagen);
                miEmpresa.Logo = logo;
            }
            else
            {
                logo = miEmpresa.Logo;
            }      
        }

        private void ConfigurarGrilla()
        {
            dgvTickets.Columns.Clear();
            var propiedades = typeof(Reserva).GetProperties();

            int anchoTotal = dgvTickets.ClientSize.Width;
            foreach (var propiedad in propiedades)
            {
                var columna = new DataGridViewTextBoxColumn();

                columna.Name = propiedad.Name;
                columna.HeaderText = propiedad.Name;
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                columna.FillWeight = (float)anchoTotal / propiedades.Length;
                dgvTickets.Columns.Add(columna);
            }
        }

        private void RefrescarGrillaReservas()
        {
            dgvTickets.Rows.Clear();

            var propiedades = typeof(Reserva).GetProperties();

            foreach (Reserva t in ticketsEmitidos)
            {
                DataGridViewRow fila = new DataGridViewRow();
                foreach (var propiedad in propiedades)
                {
                    object valor = propiedad.GetValue(t);

                    if (valor is DateTime)
                    {
                        fila.Cells.Add(new DataGridViewTextBoxCell { Value = ((DateTime)valor).ToString("dd/MM/yyyy") });
                    }
                    else
                    {
                        fila.Cells.Add(new DataGridViewTextBoxCell { Value = valor });
                    }
                }
                dgvTickets.Rows.Add(fila);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarVistaPrevia();
        }

        private void MostrarVistaPrevia()
        {
            int idTicket = ObtenerIndiceSeleccionado();
            ticket = ObtenerTicket(idTicket);
            if (ticket != null)
            {
                ActualizarLista(ticket);
                fotoPropiedad = ticket.ObtenerFotoPropiedad();
                if (fotoPropiedad == null)
                {
                    fotoPropiedad = Image.FromFile(@"E:\UTN\Laboratorio 2\Proyectos\TP3\Logo empresa\fotoPropiedad.png");
                }
                imprimirDocumento.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169); // Tamaño A4 en orientación horizontal
                imprimirDocumento.DefaultPageSettings.Landscape = true; // Cambiar a orientación horizontal

                vistaPrevia.Document = imprimirDocumento;
                vistaPrevia.ShowDialog();
            }

        }

        private void ActualizarLista(Reserva ticket)
        {
            lbTicket.Items.Clear();
            lbTicket.Items.Add("Nº Reserva: " + ticket.Nro);
            lbTicket.Items.Add("Nombre: " + ticket.Nombre);
            lbTicket.Items.Add("Dni: " + ticket.Dni);
            lbTicket.Items.Add("");

            lbTicket.Items.Add("Fecha de registro de reserva: " + ticket.Reservacion);
            lbTicket.Items.Add("Fecha de ingreso: " + ticket.FechaIngreso.ToShortDateString());
            lbTicket.Items.Add("Fecha de salida: " + ticket.FechaOutgreso.ToShortDateString());
            lbTicket.Items.Add("Total de dias que se hospedo: " + ticket.Dias + " dias");
            lbTicket.Items.Add("Cantidad de pasajeros: " + ticket.ObtenerPasajeros());
            lbTicket.Items.Add("");

            lbTicket.Items.Add("Numero de propiedad: " + ticket.NroPropiedad);
            lbTicket.Items.Add("Nombre de propiedad: " + ticket.ObtenerNombrePropiedad());
            lbTicket.Items.Add("Direccion: " + ticket.Direccion);
            lbTicket.Items.Add("Servicios:");
            string[] servicios = ticket.ObtenerServicios().Split(';');
            foreach (string s in servicios)
            {
                lbTicket.Items.Add(s);
            }
            lbTicket.Items.Add("");

            lbTicket.Items.Add(string.Format("Costo x dia: {0,8:C}", ticket.ObtenerCostoPorDia()));
            //lbTicket.Items.Add(string.Format("Costo total: {0,8:C}", miEmpresa.CalcularTotal(ticket.Dni, ticket.NroPropiedad)));
            lbTicket.Items.Add(string.Format("Costo total: {0,8:C}", ticket.ObtenerCostoTotal()));
        }

        private Reserva ObtenerTicket(int idTicket)
        {
            Reserva ticket = null;
            foreach (Reserva t in ticketsEmitidos)
            {
                if (t.Nro == idTicket)
                {
                    ticket = t;
                }
            }

            return ticket;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirDocumento();
        }

        private void ImprimirDocumento()
        {
            PrintDialog dialogoImpresion = new PrintDialog();
            dialogoImpresion.Document = imprimirDocumento;
            imprimirDocumento.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169); // Tamaño A4 en orientación horizontal
            imprimirDocumento.DefaultPageSettings.Landscape = true; // Cambiar a orientación horizontal

            if (dialogoImpresion.ShowDialog() == DialogResult.OK)
            {
                imprimirDocumento.Print();
            }
        }

        private int ObtenerIndiceSeleccionado()
        {
            int indice = -1;

            int selectedrowindex = dgvTickets.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvTickets.Rows[selectedrowindex];
            DataGridViewCell selectedCell = selectedRow.Cells["Nro"];
            indice = Convert.ToInt32(selectedCell.Value);

            return indice;
        }

        private void imprimirDocumento_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Seteamos el lapiz y la fuente
            fuente = new Font("Consolas", 12);
            pincel = new SolidBrush(Color.Black);
        }

        private void imprimirDocumento_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            fuente.Dispose();
            pincel.Dispose();
        }

        private void imprimirDocumento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
                        
            float yPos = 0f;
            int count = 0;

            float leftMargin = e.MarginBounds.Left; // Espacio en blanco desde el borde izquierdo de la página
            float rightMargin = e.MarginBounds.Right; // Espacio en blanco desde el borde derecho de la página
            float topMargin = e.MarginBounds.Top; // Espacio en blanco desde el tope de la página
            float bottomMargin = e.MarginBounds.Bottom;
            string line = null; // Con esta variable escribimos la línea
            float linesPerPage = e.MarginBounds.Height / fuente.GetHeight(e.Graphics);
            float anchoPagina = e.MarginBounds.Width;
            float altoPagina = e.MarginBounds.Height;

            string pagina = imprimirOriginal ? "Original" : "Copia"; // Cambiar entre "Original" y "Copia"
            int ancho = 62;
            int alto = 80;
            int espacioEntreCopias = 20; // Espacio entre las copias

            // Calcular el espacio para una sola copia
            float anchoCopia = anchoPagina/2;
            float medioCopia = anchoCopia / 2;

            // Imprimir dos copias, una al lado de la otra
            for (int i = 0; i < 2; i++)
            {
                e.Graphics.DrawImage(logo, (anchoCopia) + i * (anchoCopia), topMargin, ancho, alto); // 125x160
                //e.Graphics.DrawString(leyenda, fuente, pincel, (anchoCopia) + i * (anchoCopia) + ancho / 2, topMargin + alto + linesPerPage);

                pagina = i == 1 ? "Copia" : "Original";

                e.Graphics.DrawString(pagina, fuente, pincel, (anchoCopia) + i * (anchoCopia), bottomMargin - linesPerPage);
                while (count <= linesPerPage && contadorLinea < lbTicket.Items.Count)
                {
                    line = lbTicket.Items[contadorLinea].ToString();
                    yPos = topMargin + count * fuente.GetHeight(e.Graphics); // Avanza en el eje Y a medida que recorre el listbox con los textos
                    e.Graphics.DrawString(line, fuente, pincel, new PointF(leftMargin + i * (anchoCopia + espacioEntreCopias), yPos));

                    count++;
                    contadorLinea++;
                }

                e.Graphics.DrawImage(fotoPropiedad, medioCopia + (i * 2) * (medioCopia), yPos + alto, ancho, alto);
                count=0;
                contadorLinea=0;
            }

            // Verifica si estamos imprimiendo la copia
            if (contadorLinea >= lbTicket.Items.Count)
            {
                if (imprimirOriginal)
                {
                    contadorLinea = 0;
                    imprimirOriginal = false; // Cambia a imprimir la copia
                    e.HasMorePages = true;
                }
                else
                {
                    imprimirOriginal = true; // Cambia de nuevo a imprimir el original
                    e.HasMorePages = false;
                }
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        private void dgvTickets_SelectionChanged(object sender, EventArgs e)
        {
            int idTicket = ObtenerIndiceSeleccionado();
            ticket = ObtenerTicket(idTicket);
            if (ticket != null)
            {
                ActualizarLista(ticket);
            }
        }
    }
}
