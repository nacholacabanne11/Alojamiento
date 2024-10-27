using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Security.Cryptography;
using Tp2Definitivo.Clases;

namespace Tp2Definitivo
{
    public partial class Form1 : Form
    {
        Empresa miEmpresa;
        FReservacion reservas = new FReservacion();
        FCalendario calend = new FCalendario();
        public Form1()
        {
            InitializeComponent();
            miEmpresa = new Empresa();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ltbxClientes.Items.Clear();
            ltbxPropiedades.Items.Clear();
            ConfigurarGrillaReservas();

            //string path = Application.StartupPath;
            //string nombre = Path.Combine(path, "sys.dat");

            //FileStream fs = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.Read);
            //BinaryFormatter bf = new BinaryFormatter();

            //miEmpresa = (Empresa)bf.Deserialize(fs);
        }

        private void ConfigurarGrillaReservas()
        {
            dgvReservas.Columns.Clear();

            // Obtener las propiedades de la clase Reserva (asumo que Reserva es una clase)
            var propiedades = typeof(Reserva).GetProperties();

            int anchoTotal = dgvReservas.ClientSize.Width;
            // Agregar columnas al DataGridView basado en las propiedades del objeto
            foreach (var propiedad in propiedades)
            {
                // Crear una nueva columna
                var columna = new DataGridViewTextBoxColumn();

                // Asignar el nombre de la propiedad como encabezado de la columna
                columna.HeaderText = propiedad.Name;

                // Configurar AutoSizeMode para que la columna ajuste su ancho automáticamente
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Calcular el ancho de la columna basado en el ancho total disponible
                columna.FillWeight = (float)anchoTotal / propiedades.Length;

                // Agregar la columna al DataGridView
                dgvReservas.Columns.Add(columna);
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                string path = Application.StartupPath;
                string nombre = Path.Combine(path, "sys.dat");

                FileStream archivo = new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter formateador = new BinaryFormatter();

                formateador.Serialize(archivo, miEmpresa);
                archivo.Close();
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show("No se puede serialiar un objeto nulo", ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ");
            }
        }

        private void btnAgregarCasa_Click(object sender, EventArgs e)
        {
            if (AgregarCasa())
            {
                MessageBox.Show("Agregaste una casa con Exito");
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("Error al agregar Casa");
            }
        }

        private bool AgregarCasa()
        {
            bool exito = false;

            //CasasA aCasa = new CasasA();
            //try
            //{
            //    if (aCasa.ShowDialog() == DialogResult.OK)
            //    {
            //        string direccion = aCasa.tbDireccion.Text;
            //        int nroCamas = Convert.ToInt32(aCasa.tbCamas.Text);
            //        int nroCasa = Convert.ToInt32(aCasa.tbNroCasa.Text);
            //        double pBase = Convert.ToDouble(aCasa.tbPrecio.Text);
                    
            //        Casa nuevo = new Casa(direccion, nroCamas, nroCasa, pBase);
            //        nuevo.fotos[0] = aCasa.imagen1;
            //        nuevo.fotos[1] = aCasa.imagen2;
            //        nuevo.fotos[2] = aCasa.imagen3;
            //        nuevo.fotos[3] = aCasa.imagen4;

            //        string[] serviciosSpliteados = serviciosCasa(aCasa);
            //        if (serviciosSpliteados.Length > 0)
            //        {
            //            nuevo.Servicios = serviciosSpliteados;
            //        }
            //        miEmpresa.AgregarAlojamiento(nuevo);
                    
            //        reservas.cboxNro.Items.Add(nroCasa);

            //        exito = true;
            //    }
            //    ltbxPropiedades.Items.Clear();
            //    Propiedad[] p = miEmpresa.VerPropiedades;
            //    for (int i = 0; i < p.Length; i++)
            //    {
            //        ltbxPropiedades.Items.Add(p[i].ToString());
            //    }


            //}
            //catch (FormatException ex)
            //{
            //    MessageBox.Show("El formato que ingreso no es correcto.", ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error: ");
            //}
            //finally
            //{
            //    aCasa.Dispose();
            //}

            return exito;
        }

        private void btnAgregarHotel_Click(object sender, EventArgs e)
        {
            if (AgregarHotel())
            {
                MessageBox.Show("Creaste un coso capo");
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("NT");
            }
        }

        private bool AgregarHotel()
        {
            bool exito = false;

            //HotelesA aHotel = new HotelesA();
            //try
            //{
            //    if (aHotel.ShowDialog() == DialogResult.OK)
            //    {
            //        string direccion = aHotel.tbDireccion.Text;
            //        int nroHabitacion = Convert.ToInt32(aHotel.tbNroHabitacion.Text);
            //        double pBase = Convert.ToDouble(aHotel.tbPrecio.Text);
            //        int habitaciones = 0;
            //        bool tresEstrellas = false;
            //        if (aHotel.rbSimple.Checked)
            //        {
            //            habitaciones = 1;
            //        }
            //        else if (aHotel.rbDoble.Checked)
            //        {
            //            habitaciones = 2;
            //        }
            //        else if (aHotel.rbTriple.Checked)
            //        {
            //            habitaciones = 3;
            //        }

            //        if (aHotel.radioButton3.Checked)
            //        {
            //            tresEstrellas = false;
            //        }
            //        else
            //        {
            //            tresEstrellas = true;
            //        }
            //        //Hotel nuevo = new Hotel(direccion, habitaciones, nroHabitacion, tresEstrellas, miEmpresa.CostoDia);
            //        Hotel nuevo = new Hotel(direccion, habitaciones, nroHabitacion, tresEstrellas, pBase);
            //        nuevo.fotos[0] = aHotel.imagen1;
            //        nuevo.fotos[1] = aHotel.imagen2;
            //        nuevo.fotos[2] = aHotel.imagen3;
            //        nuevo.fotos[3] = aHotel.imagen4;

            //        string[] serviciosSpliteados = serviciosHotel(aHotel);


            //        if (serviciosSpliteados.Length > 0)
            //        {
            //            nuevo.Servicios = serviciosSpliteados;
            //        }


            //        miEmpresa.AgregarAlojamiento(nuevo);
            //        reservas.cboxNro.Items.Add(nroHabitacion);

            //        exito = true;
            //    }
            //    ltbxPropiedades.Items.Clear();
            //    Propiedad[] p = miEmpresa.VerPropiedades;
            //    for (int i = 0; i < p.Length; i++)
            //    {
            //        ltbxPropiedades.Items.Add(p[i].ToString());
            //    }


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error: ");
            //}
            //finally
            //{
            //    aHotel.Dispose();
            //}

            return exito;
        }
        public string[] serviciosHotel(HotelesA aHotel)
        {
            string servicios = "";
            if (aHotel.cbCochera.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Cochera";
                }
            }
            if (aHotel.cbPileta.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Pileta";
                }
                else
                {
                    servicios += ";Pileta";
                }
            }
            if (aHotel.cbWifi.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Wifi";
                }
                else
                {
                    servicios += ";Wifi";
                }
            }
            if (aHotel.cbLimpieza.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Limpieza";
                }
                else
                {
                    servicios += ";Limpieza";
                }
            }
            if (aHotel.cbDesayuno.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Desayuno";
                }
                else
                {
                    servicios += ";Desayuno";
                }
            }
            if (aHotel.cbMascotas.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Mascotas";
                }
                else
                {
                    servicios += ";Mascotas";
                }
            }
            return servicios.Split(';');
        }

        public string[] serviciosCasa(CasasA aCasa)
        {
            string servicios = "";
            if (aCasa.cbCochera.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Cochera";
                }
            }
            if (aCasa.cbPileta.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Pileta";
                }
                else
                {
                    servicios += ";Pileta";
                }
            }
            if (aCasa.cbWifi.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Wifi";
                }
                else
                {
                    servicios += ";Wifi";
                }
            }
            if (aCasa.cbLimpieza.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Limpieza";
                }
                else
                {
                    servicios += ";Limpieza";
                }
            }
            if (aCasa.cbDesayuno.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Desayuno";
                }
                else
                {
                    servicios += ";Desayuno";
                }
            }
            if (aCasa.cbMascotas.Checked)
            {
                if (servicios == "")
                {
                    servicios += "Mascotas";
                }
                else
                {
                    servicios += ";Mascotas";
                }
            }
            return servicios.Split(';');
        }

        private void btnEditarPropiedad_Click(object sender, EventArgs e)
        {
            if (EditarPropiedad())
            {
                MessageBox.Show("Edita3");
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("No pudo");
            }
        }

        private void RefrescarGrilla()
        {
            dgvPropiedades.Rows.Clear(); //Limpiamos la grilla

            foreach (Propiedad p in miEmpresa.ObtenerPropiedades()) //Recorremos cada propiedad

            {
                //ComboBox servicios = CargarServicios();
                DataGridViewComboBoxCell servicios = CargarServicios(p.Servicios);
                int nro = p.Nro;
                string tipo = ""; //Si p is Hotel o p is Casa
                string direccion = "";
                double precio = 0.00;
                //string servicios = "";
                if (p is Hotel)
                {
                    Hotel h = (Hotel)p;
                    tipo = "Habitación de hotel";
                    direccion = h.Direccion;
                    precio = h.PrecioBase;
                    //servicios = CargarServicios();
                }
                else
                {
                    Casa c = (Casa)p;
                    tipo = "Casa";
                    direccion = c.Direccion;
                    precio = c.PrecioBase;
                    //servicios = CargarServicios();
                }
                //dgvPropiedades.Rows.Add(nro, tipo, direccion, precio, servicios);
                //((DataGridViewComboBoxColumn)dgvPropiedades.Columns["Servicios"]).DataSource = servicios.Items;

                dgvPropiedades.Rows.Add(nro, tipo, direccion, precio);
                int rowIndex = dgvPropiedades.Rows.Count - 2;
                dgvPropiedades.Rows[rowIndex].Cells["Servicios"] = servicios;
            }
        }

        private DataGridViewComboBoxCell CargarServicios(string[] serviciosPropiedad)
        {
            DataGridViewComboBoxCell servicios = new DataGridViewComboBoxCell();
            foreach(string s in serviciosPropiedad)
            {
                servicios.Items.Add(s);

            }
       

            return servicios;
        }

        private bool EditarPropiedad()
        {
            try
            {

            bool exito = false;
            int indice;
            int selectedrowindex = dgvPropiedades.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPropiedades.Rows[selectedrowindex];
            indice = Convert.ToInt32(selectedRow.Cells["Nro"].Value);

            //Levantar de la row el Nro de la propiedad

            Propiedad propiedad = BuscarPropiedad(indice);
            if (propiedad != null)
            {
                if (propiedad is Casa)
                {
                    Casa c = (Casa)propiedad;
                    exito = EditarCasa(c);
                }
                else
                {
                    Hotel h = (Hotel)propiedad;
                    exito = EditarHotel(h);
                }
            }

            return exito;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool EditarHotel(Hotel h)
        {
            bool exito = false;
            //HotelesA hotelesA = new HotelesA(); //Creamos el formulario
            
            ////Cargamos los datos
            //hotelesA.tbDireccion.Text = h.Direccion;
            //hotelesA.tbNroHabitacion.Text = h.NroIdentificador.ToString();
            //hotelesA.tbNroHabitacion.Enabled = false;
            //hotelesA.tbPrecio.Text = h.ObtenerPrecioBase().ToString();

            //Image image1 = h.fotos[0].Image;
            //Image image2 = h.fotos[1].Image;
            //Image image3 = h.fotos[2].Image;
            //Image image4 = h.fotos[3].Image;
            //hotelesA.imagen1.Image = image1;
            //hotelesA.imagen2.Image = image2;
            //hotelesA.imagen3.Image = image3;
            //hotelesA.imagen4.Image = image4;


            //foreach (string i in h.Servicios)
            //{
            //    if (i == "Cochera")
            //    {
            //        hotelesA.cbCochera.Checked = true;
            //    }
            //    if (i == "Pileta")
            //    {
            //        hotelesA.cbPileta.Checked = true;
            //    }
            //    if (i == "Wifi")
            //    {
            //        hotelesA.cbWifi.Checked = true;
            //    }
            //    if (i == "Limpieza")
            //    {
            //        hotelesA.cbLimpieza.Checked = true;
            //    }
            //    if (i == "Desayuno")
            //    {
            //        hotelesA.cbDesayuno.Checked = true;
            //    }
            //    if (i == "Mascotas")
            //    {
            //        hotelesA.cbMascotas.Checked = true;
            //    }

            //}


            ////Preguntamos por las estrellas
            //if (h.ObtenerEstrellas())
            //{
            //    hotelesA.radioButton2.Checked = true;
            //    hotelesA.radioButton3.Checked = false;
            //}
            //else
            //{
            //    hotelesA.radioButton2.Checked = false;
            //    hotelesA.radioButton3.Checked = true;
            //}

            ////Preguntamos por las piezas
            //switch (h.ObtenerPiezas())
            //{
            //    case 1:
            //        hotelesA.rbSimple.Checked = true;
            //        hotelesA.rbDoble.Checked = false;
            //        hotelesA.rbTriple.Checked = false;
            //        break;
            //    case 2:
            //        hotelesA.rbSimple.Checked = false;
            //        hotelesA.rbDoble.Checked = true;
            //        hotelesA.rbTriple.Checked = false;
            //        break;
            //    default:
            //        hotelesA.rbSimple.Checked = false;
            //        hotelesA.rbDoble.Checked = false;
            //        hotelesA.rbTriple.Checked = true;
            //        break;
            //}

            ////Si le dan a aceptar
            //if (hotelesA.ShowDialog() == DialogResult.OK)
            //{
            //    //Actualizamos los datos
            //    h.Direccion = hotelesA.tbDireccion.Text;
            //    h.PrecioBase = Convert.ToDouble(hotelesA.tbPrecio.Text);
            //    h.fotos[0] = hotelesA.imagen1;
            //    h.fotos[1] = hotelesA.imagen2;
            //    h.fotos[2] = hotelesA.imagen3;
            //    h.fotos[3] = hotelesA.imagen4;
            //    string[] serviciosEditados = new string[6];
            //    serviciosEditados = serviciosHotel(hotelesA);
            //    h.Servicios = serviciosEditados;

            //    if (hotelesA.rbSimple.Checked)
            //    {
            //        h.NroPiezas = 1;
            //    }
            //    else if (hotelesA.rbDoble.Checked)
            //    {
            //        h.NroPiezas = 2;
            //    }
            //    else if (hotelesA.rbTriple.Checked)
            //    {
            //        h.NroPiezas = 3;
            //    }

            //    if (hotelesA.radioButton3.Checked)
            //    {
            //        h.TresEstrellas = false;
            //    }
            //    else
            //    {
            //        h.TresEstrellas = true;
            //    }



            //    exito = true;
            //}
            return exito;
        }

        private bool EditarCasa(Casa c)
        {
            bool exito = false;

            //CasasA casaA = new CasasA(); //Creo un nuevo formulario

            ////Cargamos los campos con el objeto que viene para modificar
            //casaA.tbNroCasa.Text = c.NroIdentificador.ToString();
            //casaA.tbNroCasa.Enabled = false; //Para que no se pueda editar el Nro
            //casaA.tbDireccion.Text = c.Direccion;
            //casaA.tbCamas.Text = c.CantCamas.ToString();
            //casaA.tbPrecio.Text = c.ObtenerPrecioBase().ToString();

            //Image image1 = c.fotos[0].Image;
            //Image image2 = c.fotos[1].Image;
            //Image image3 = c.fotos[2].Image;
            //Image image4 = c.fotos[3].Image;
            //casaA.imagen1.Image = image1;
            //casaA.imagen2.Image = image2;
            //casaA.imagen3.Image = image3;
            //casaA.imagen4.Image = image4;
            ////cbLimpieza.Checked cbPileta.Checked cbWifi.Checked cbLimpieza.Checked cbDesayuno.Checked cbMascotas.Checked
            //foreach (string i in c.Servicios)
            //{
            //    if (i == "Cochera")
            //    {
            //        casaA.cbCochera.Checked = true;
            //    }
            //    if (i == "Pileta")
            //    {
            //        casaA.cbPileta.Checked = true;
            //    }
            //    if (i == "Wifi")
            //    {
            //        casaA.cbWifi.Checked = true;
            //    }
            //    if (i == "Limpieza")
            //    {
            //        casaA.cbLimpieza.Checked = true;
            //    }
            //    if (i == "Desayuno")
            //    {
            //        casaA.cbDesayuno.Checked = true;
            //    }
            //    if (i == "Mascotas")
            //    {
            //        casaA.cbMascotas.Checked = true;
            //    }
              
            //}

            ////Si dan a aceptar
            //if (casaA.ShowDialog() == DialogResult.OK)
            //{
            //    c.Direccion = casaA.tbDireccion.Text;
            //    c.CantCamas = Convert.ToInt32(casaA.tbCamas.Text);
            //    c.PrecioBase = Convert.ToDouble(casaA.tbPrecio.Text);
            //    c.fotos[0] = casaA.imagen1;
            //    c.fotos[1] = casaA.imagen2;
            //    c.fotos[2] = casaA.imagen3;
            //    c.fotos[3] = casaA.imagen4;
            //    string[] serviciosEditados = new string[6];
            //    serviciosEditados = serviciosCasa(casaA);
            //    c.Servicios = serviciosEditados;
            //    exito = true;
            //}

            return exito;
        }

        private Propiedad BuscarPropiedad(int indice)
        {

            Propiedad propiedad;
            propiedad = miEmpresa.BuscarPropiedad(indice);

            return propiedad;
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if (AgregarCliente())
            {
                MessageBox.Show("¡Agregado con éxito!");
            }
            else
            {
                MessageBox.Show("No se pudo agregar cliente :(");
            }
        }

        private bool AgregarCliente()
        {
            bool exito = false;

            ClientesA aCliente = new ClientesA();
            try
            {
                int dni = 1;
                string nombre;
                DateTime fechaNacimiento;
                if (aCliente.ShowDialog() == DialogResult.OK)
                {
                    dni = Convert.ToInt32(aCliente.tbDNI.Text);
                    nombre = aCliente.tbNombre.Text;
                    fechaNacimiento = aCliente.dtpNacimiento.Value;
                    Cliente nuevo = new Cliente(dni, nombre, fechaNacimiento);
                    miEmpresa.AgregarCliente(nuevo);
                    reservas.cbDni.Items.Add(dni);
                    exito = true;
                }
                aCliente.Dispose();

                ltbxClientes.Items.Clear();
                Cliente[] c = miEmpresa.VerClientes;
                for (int i = 0; i < miEmpresa.CantidadClientes; i++)
                {
                    ltbxClientes.Items.Add(c[i].ToString());
                }
                if (dni <= 0) throw new Exception("Debe ingresar un numero mayor a 0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ");
            }

            return exito;
        }

        private void btnEliminarPropiedad_Click(object sender, EventArgs e)
        {
            if (EliminarPropiedad())
            {
                MessageBox.Show("Eliminada");
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar");
            }
        }

        private bool EliminarPropiedad()
        {
            bool exito = false;
            int indice;
            int selectedrowindex = dgvPropiedades.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPropiedades.Rows[selectedrowindex];
            indice = Convert.ToInt32(selectedRow.Cells["Nro"].Value);

            //Levantar de la row el Nro de la propiedad

            Propiedad propiedad = BuscarPropiedad(indice);
            if (propiedad != null)
            {
                DialogResult resultado = MessageBox.Show("¿Desea eliminar la propiedad: " + propiedad.Nro + "?", "Si", MessageBoxButtons.YesNoCancel);
                if (resultado == DialogResult.Yes)
                {
                    miEmpresa.EliminarPropiedad(indice);
                    exito = true;
                }
            }
            return exito;
        }

        private void btnAgregarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                bool hayPropiedades = miEmpresa.ObtenerPropiedades().Count > 0;
                bool hayClientes = miEmpresa.CantidadClientes > 0;

                if (hayPropiedades && hayClientes)
                {
                   // reservas = CargarFormularioReservas(); 
                    if (reservas.ShowDialog() == DialogResult.OK)
                    {

                        int dni = Convert.ToInt32(reservas.cbDni.Text);
                        int nro = Convert.ToInt32(reservas.cbNro.Text);
                        DateTime ingreso = reservas.Ingreso.SelectionStart;
                        DateTime egreso = reservas.Egreso.SelectionEnd;
                        bool resultReserva = miEmpresa.VerificarReservas(ingreso, egreso, nro);
                        if (resultReserva)
                        {

                        miEmpresa.HacerReserva(dni, nro, ingreso, egreso);


                        reservas.Ingreso.AddBoldedDate(ingreso);
                        reservas.Egreso.AddBoldedDate(egreso);
                        calend.calendariaVer.AddBoldedDate(ingreso);
                        calend.calendariaVer.AddBoldedDate(egreso);

                        calend.calendarioVerS.AddBoldedDate(ingreso);
                        calend.calendarioVerS.AddBoldedDate(egreso);
                        } else
                        {
                            MessageBox.Show("Detectamos una reserva Activa para la propiedad: " + nro + "\n" + " Entre la fecha: " + ingreso.ToString("dd/mm/yyyy") + "-" + egreso.ToString("dd/mm/yyyy"));
                        }
                    }

                    RefrescarGrillaReservas();
                    
                }
                else
                {
                    MessageBox.Show("Se necesita cargar una propiedad y un cliente para poder realizar una reserva");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ");
            }
          
        }
       



        public void RefrescarGrillaReservas()
        {
            // Limpiar las filas existentes (si es necesario)
            dgvReservas.Rows.Clear();

            // Obtener las propiedades del objeto
            Reserva[] reservas = miEmpresa.VerReservaciones;

            // Obtener las propiedades de la clase Reserva (asumo que Reserva es una clase)
            var propiedades = typeof(Reserva).GetProperties();

            foreach (Reserva r in reservas)
            {
                // Agregar fila con los valores del objeto
                DataGridViewRow fila = new DataGridViewRow();
                foreach (var propiedad in propiedades)
                {
                    fila.Cells.Add(new DataGridViewTextBoxCell { Value = propiedad.GetValue(r) });
                }
                // Agregar la fila al DataGridView
                dgvReservas.Rows.Add(fila);
            }
        }

        private void btnModificarReserva_Click(object sender, EventArgs e)
        {
            bool hayReservas = miEmpresa.VerReservaciones.Length> 0;

            if (hayReservas)
            {
                if (ModificarReserva())
                {
                    MessageBox.Show("¡Reserva modificada!");
                    RefrescarGrillaReservas();
                }
                else
                {
                    MessageBox.Show("No se pudo modificar reserva");
                }
            }
            else
            {
                MessageBox.Show("¡No hay reservas registradas!");
            }
            
        }

        private bool ModificarReserva()
        {
            bool exito = false;
            int indice;
            int selectedrowindex = dgvReservas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPropiedades.Rows[selectedrowindex];
            indice = Convert.ToInt32(selectedRow.Cells["Nro"].Value);

            //Levantar de la row el Nro de la propiedad

            Reserva reserva = BuscarReserva(indice);
            if (reserva != null)
            {
                exito = EditarReserva(reserva);
            }

            return exito;
        }

        private bool EditarReserva(Reserva reserva)
        {
            bool exito = false;

            try
            {
                Reserva[] rs = miEmpresa.VerReservaciones;

                reservas.cbDni.Text = reserva.Dni.ToString();
                reservas.cbDni.Enabled = false;
                reservas.cbNro.Text = reserva.Nro.ToString();
                reservas.cbNro.Enabled = false;
                reservas.Ingreso.SelectionStart = reserva.FechaIngreso;
                reservas.Egreso.SelectionEnd = reserva.FechaOutgreso;

                if (reservas.ShowDialog() == DialogResult.OK)
                {
                    reserva.FechaIngreso = reservas.Ingreso.SelectionStart;
                    reserva.FechaOutgreso = reservas.Egreso.SelectionEnd;

                    exito = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ");
            }
          
            return exito;
        }

        private Reserva BuscarReserva(int indice)
        {
            Reserva buscada = null;
            
            buscada = miEmpresa.BuscarReserva(indice);

            return buscada;
        }

        private void btnEliminarReserva_Click(object sender, EventArgs e)
        {
            bool hayReservas = miEmpresa.VerReservaciones.Length > 0;

            if (hayReservas)
            {
                if (EliminarReserva())
                {
                    MessageBox.Show("¡Reserva eliminada!");
                    RefrescarGrillaReservas();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar reserva");
                }
            }
            else
            {
                MessageBox.Show("¡No hay reservas registradas!");
            }
        }

        private bool EliminarReserva()
        {
            bool exito = false;
            int indice;
            int selectedrowindex = dgvPropiedades.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPropiedades.Rows[selectedrowindex];
            indice = Convert.ToInt32(selectedRow.Cells["Nro"].Value);

            Reserva reserva = BuscarReserva(indice);
            if (reserva != null)
            {
                DialogResult resultado = MessageBox.Show("¿Desea eliminar la reserva: " + reserva.Nro + "?", "Si", MessageBoxButtons.YesNoCancel);
                if (resultado == DialogResult.Yes)
                {    
                    miEmpresa.EliminarReserva(indice);
                    exito = true;
                }
            }
            return exito;
        }

        private void btnVerTicket_Click(object sender, EventArgs e)
        {
            MostrarTicket mt = new MostrarTicket();
            FTicket tk = new FTicket();
            Reserva[] reservs = miEmpresa.VerReservaciones;
            bool hayReserva = false;
            bool hayClientes = miEmpresa.VerClientes.Length > 0; //Comprobamos que haya clientes antes de mostrar el modal 
            if (hayClientes)
            {
                mt = CargarClientes();
            }
            try
            {
                if (mt.ShowDialog() == DialogResult.OK)
                {
                    int dni = Convert.ToInt32(mt.cboxDNI.Text);

                    for (int i = 0; i < miEmpresa.CantidadClientes; i++) //Recorremos el listado de clientes
                    {
                        if (reservs[i].Dni == dni) //Buscamos una coincidencia con las reservas cargadas
                        {
                            hayReserva = true;

                            tk.lbTicket.Items.Add("Nombre: " + reservs[i].Nombre);
                            tk.lbTicket.Items.Add("Dni: " + reservs[i].Dni);
                            tk.lbTicket.Items.Add("");

                            tk.lbTicket.Items.Add("Fecha de ingreso: " + reservs[i].FechaIngreso.ToShortDateString());
                            tk.lbTicket.Items.Add("Fecha de salida: " + reservs[i].FechaOutgreso.ToShortDateString());
                            tk.lbTicket.Items.Add("Hora que se realizo la reserva: " + reservs[i].Reservacion);
                            tk.lbTicket.Items.Add("Total de dias que se hospedo: " + reservs[i].Dias + " dias");
                            tk.lbTicket.Items.Add("");

                            tk.lbTicket.Items.Add("Direccion: " + reservs[i].Direccion);
                            tk.lbTicket.Items.Add("Numero de propiedad: " + reservs[i].NroPropiedad);
                            tk.lbTicket.Items.Add("");

                            //tk.listBox1.Items.Add(string.Format("Costo x dia: {0,8:C}", miEmpresa.CostoDia));
                            tk.lbTicket.Items.Add(string.Format("Costo x dia: {0,8:C}", miEmpresa.ObtenerCostoPorDia(reservs[i].Dni, reservs[i].NroPropiedad)));
                            tk.lbTicket.Items.Add(string.Format("Costo total: {0,8:C}", miEmpresa.CalcularTotal(reservs[i].Dni, reservs[i].NroPropiedad)));

                            if (tk.ShowDialog() == DialogResult.OK)
                            { }
                            miEmpresa.RetirarReserva(reservs[i]);
                        }
                        
                    }

                    if (!hayReserva)
                    {
                        MessageBox.Show("No se encontró una reserva para ese cliente");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ");
            }
            finally
            {
                tk.Dispose();
                mt.Dispose();
            }
        }

        private MostrarTicket CargarClientes()
        {
            MostrarTicket mt = new MostrarTicket();
            foreach(Cliente c in miEmpresa.VerClientes)
            {
                mt.cboxDNI.Items.Add(c.DNI.ToString());
            }

            return mt;
        }

        private void btnVerCalendario_Click(object sender, EventArgs e)
        {
            if (calend.ShowDialog() == DialogResult.OK)
            {

            }
        }
    }
}
