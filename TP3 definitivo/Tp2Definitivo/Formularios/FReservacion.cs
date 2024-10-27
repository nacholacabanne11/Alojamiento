using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tp2Definitivo.Clases;

namespace Tp2Definitivo
{
    public partial class FReservacion : Form
    {
        public FReservacion()
        {
            InitializeComponent();
        }
        Propiedad[] propiedades;
        Cliente[] clientes;
        bool lugar, pasajeros, disponibilidad, casa, hotel;
        int cantPasajeros, cantidadCasas, cantidadHoteles;
        DateTime desde, hasta;
        public FReservacion(Propiedad[] propiedades, Cliente[] clientes)
        {
            this.propiedades = propiedades;
            this.clientes = clientes;
            InitializeComponent();
            CargarPropiedades();
            CargarClientes();
            dtpDesde.Enabled = false;
            dtpHasta.Enabled = false;
            numPasajeros.Enabled = false;
            rbCasa.Enabled = false;
            rbHotel.Enabled = false;
            btnFiltrar.Enabled = false;
        }

        private void CargarClientes()
        {
            cbDni.Items.Clear();
            foreach (Cliente c in clientes)
            {
                cbDni.Items.Add(c.DNI);
            }
        }

        private void CargarPropiedades()
        {
            cbNro.Items.Clear();
            cantidadCasas = 0;
            cantidadHoteles = 0;
            foreach (Propiedad p in propiedades)
            {
                if(p is Casa)
                {
                    cantidadCasas++;
                }
                else
                {
                    cantidadHoteles++;
                }
                cbNro.Items.Add(p.Nro);
            }
        }

        private void CargarPropiedades(Propiedad[] filtradas)
        {
            cbNro.Items.Clear();
            foreach (Propiedad p in filtradas)
            {
                cbNro.Items.Add(p.Nro);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Reservaciones_Load(object sender, EventArgs e)
        {

        }

        private void cbPasajeros_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoFiltro();
        }

        private void cbDisponibilidad_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoFiltro();
        }

        private void cbNro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPropiedad();
        }

        public void CargarPropiedad()
        {
            int id = Convert.ToInt32(cbNro.SelectedItem);

            foreach (Propiedad p in propiedades)
            {
                if (p.Nro == id)
                {
                    if (p is Casa)
                    {
                        Casa c = (Casa)p;
                        MostrarDatosPropiedad(c);
                    }
                    else
                    {
                        Hotel h = (Hotel)p;
                        MostrarDatosPropiedad(h);
                    }
                    CargarImagenesPropiedad(p);
                }

            }
        }

        public void CargarImagenesPropiedad(Propiedad p)
        {
            Image[] fotos = p.fotos;
            imagen1.Image = fotos[0];
            imagen2.Image = fotos[1];
            imagen3.Image = fotos[2];
            imagen4.Image = fotos[3];
        }

        public void MostrarDatosPropiedad(Object o)
        {
            Type type = o.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            lbProp.Items.Clear(); // Limpia cualquier elemento existente en el ListBox

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(o);
                string propertyName = property.Name;
                if (propertyName == "Habitacion" && value is int)
                {
                    int habitacion = (int)value;
                    switch (habitacion)
                    {
                        case 1:
                            value = "Simple";
                            break;
                        case 2:
                            value = "Doble";
                            break;
                        default:
                            value = "Triple";
                            break;
                    }
                }
                if (propertyName == "TresEstrellas" && value is bool)
                {
                    bool tresEstrellas = (bool)value;
                    if (tresEstrellas)
                    {
                        propertyName = "Estrellas";
                        value = "Tres estrellas";
                    }
                    else
                    {
                        propertyName = "Estrellas";
                        value = "Dos estrellas";
                    }
                }
                if (propertyName == "Servicios" && value is string[])
                {
                    string[] servicios = (string[])value;
                    lbProp.Items.Add($"{propertyName}:");
                    foreach (string servicio in servicios)
                    {
                        lbProp.Items.Add($"{servicio}");
                    }
                }
                else
                {
                    lbProp.Items.Add($"{propertyName}:");
                    lbProp.Items.Add($"{value}");
                }

            }
        }

        private void cbDni_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Cliente c in clientes)
            {
                int dni = Convert.ToInt32(cbDni.SelectedItem.ToString());
                if (c.DNI == dni)
                {
                    lbCliente.Text = c.ToString();
                }
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void Filtrar()
        {

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            bool lugar = cbLugar.Checked;
            bool pasajeros = cbPasajeros.Checked;
            bool disponibilidad = cbDisponibilidad.Checked;

            casa = rbCasa.Checked ? true : false;
            //bool hotel = rbHotel.Checked;
            int cantPasajeros = Convert.ToInt32(numPasajeros.Value);
            DateTime desde = dtpDesde.Value;
            DateTime hasta = dtpHasta.Value;
            Propiedad[] filtradas = null;
            if (lugar)
            {
                filtradas = FiltrarPorLugar();
            }
            if (pasajeros)
            {
                filtradas = FiltrarPorPasajeros(cantPasajeros);
            }
            if (disponibilidad)
            {
                filtradas = FiltrarPorDisponibilidad(desde, hasta);
            }

            CargarPropiedades(filtradas);
        }

        private void btnReservar_Click(object sender, EventArgs e)
        {
            DateTime dateIn = Ingreso.SelectionStart;
            DateTime dateOut = Egreso.SelectionEnd;

            if (dateIn > dateOut)
            {
                MessageBox.Show("¡La fecha de egreso no puede ser anterior a la fecha de ingreso!","Fecha",MessageBoxButtons.OK,MessageBoxIcon.Warning);
               this.DialogResult = DialogResult.Cancel;
            }
        }

        private Propiedad[] FiltrarPorDisponibilidad(DateTime desde, DateTime hasta)
        {
            List<Propiedad> propiedadesFiltradas = new List<Propiedad>();
            DateTime[] filtro = ObtenerFechasOcupadas(desde, hasta);
            bool disponible;
            foreach (Propiedad propiedad in propiedades)
            {
                disponible = VerificarDisponibilidad(propiedad, filtro);
                if (disponible)
                {
                    propiedadesFiltradas.Add(propiedad);
                }
            }
            return propiedadesFiltradas.ToArray();
        }
        public DateTime[] ObtenerFechasOcupadas(DateTime desde, DateTime hasta)
        {
            List<DateTime> fechasOcupadas = new List<DateTime>();

            DateTime fechaInicio = desde;
            DateTime fechaFin = hasta;

            while (fechaInicio < fechaFin)
            {
                fechasOcupadas.Add(fechaInicio);
                fechaInicio = fechaInicio.AddDays(1);
            }

            return fechasOcupadas.ToArray();
        }
        private bool VerificarDisponibilidad(Propiedad propiedad, DateTime[]filtro)
        {
            bool disponible = true;
            DateTime[] fechasOcupadas = propiedad.ObtenerFechasOcupadas();
            foreach (DateTime fecha in filtro)
            {
                if (fechasOcupadas.Contains(fecha))
                {
                    // La fecha ya está ocupada
                    disponible = false;
                }
            }
            // No se encontraron coincidencias, la reserva se puede agregar
            return disponible;
        }

        private Propiedad[] FiltrarPorPasajeros(int cantPasajeros)
        {
            List<Propiedad> propiedadesFiltradas = new List<Propiedad>();

            foreach (Propiedad propiedad in propiedades)
            {
                if (propiedad.Pasajeros >= cantPasajeros)
                {
                    propiedadesFiltradas.Add(propiedad);
                }
            }

            Propiedad[] propPasajeros = propiedadesFiltradas.ToArray();

            return propPasajeros;
        }

        private Propiedad[] FiltrarPorLugar()
        {
            Hotel[] hoteles = new Hotel[cantidadHoteles];
            Casa[] casas = new Casa[cantidadCasas];
            int h = 0;
            int c = 0;

            try
            {
                //for (int i = 0; i < propiedades.Length; i++)
                foreach (Propiedad p in propiedades)
                    
                {
                    //if (propiedades[i] is Hotel)
                    if (p is Hotel)
                    {
                        hoteles[h] = (Hotel)p;
                        //hoteles[h] = (Hotel)propiedades[i];
                        h++;

                    }
                    else
                    {
                        casas[c] = (Casa)p;
                        //casas[c] = (Casa)propiedades[i];
                        c++;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            if (casa)
            {
                return casas;
            }
            else
            {
                return hoteles;
            }
        }

        private void cbLugar_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarEstadoFiltro();
        }

        private void ActualizarEstadoFiltro()
        {
            lugar = cbLugar.Checked;
            if (lugar)
            {
                rbCasa.Enabled = true;
                rbHotel.Enabled = true;
            }
            else
            {
                rbCasa.Enabled = false;
                rbHotel.Enabled = false;
            }

            disponibilidad = cbDisponibilidad.Checked;
            if (disponibilidad)
            {
                dtpDesde.Enabled = true;
                dtpHasta.Enabled = true;
            }
            else
            {
                dtpDesde.Enabled = false;
                dtpHasta.Enabled = false;
            }

            pasajeros = cbPasajeros.Checked;
            if (pasajeros)
            {
                numPasajeros.Enabled = true;
            }
            else
            {
                numPasajeros.Enabled = false;
            }

            if (pasajeros || disponibilidad || lugar)
            {
                btnFiltrar.Enabled = true;
            }
            else
            {
                btnFiltrar.Enabled = false;
            }
        }
    }
}
