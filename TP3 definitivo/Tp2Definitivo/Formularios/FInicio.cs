using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tp2Definitivo.Clases;

namespace Tp2Definitivo.Formularios
{
    public partial class FInicio : Form
    {
        Empresa miEmpresa;
        FInicioSesion fInicioSesion; //Formulario para iniciar sesion
        FReservacion fReservacion;
        FCalendario fCalendario;
        FTicket fTicket;
        FImprimirTicket fImprimirTicket;
        FUsuario fUsuario; //Formulario para añadir usuarios
        FNavegador fNavegador; //Para mostrar la ayuda
        int idUser;
        string passUser;
        bool esAdmin; //Bandera para saber si el usuario es admin o no
        bool esUser;
        string ruta; //Ruta donde se encuentra el archivo para deserializar
        string archivo;
        FileStream fileStream;
        BinaryFormatter bf;
        StreamReader reader;
        StreamWriter writer;
        public FInicio()
        {
            InitializeComponent();
            InicializarVariables();
            VerificarInicio();
            CargarFormulario();
        }
        #region Persistencia
        private void SerializarArchivo()
        {
            try
            {
                fileStream = new FileStream(archivo, FileMode.Create, FileAccess.Write);
                bf = new BinaryFormatter();
                bf.Serialize(fileStream, miEmpresa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al serializar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }
        private void CargarFormulario()
        {
            lbUserId.Text = "Usuario: " + idUser;

            if (esAdmin)
            {
                lbUserTipo.Text = "Administrador";
                //En caso de ser Administrador puede hacer altas y bajas de propiedades, además puede crear nuevos usuarios y anular reservas.
                ActualizarListadoUsuarios();
                RefrescarGrilla();
                ActualizarListadoClientes();
                RefrescarGrillaReservas();

            }
            else if (esUser)
            {
                lbUserTipo.Text = "Empleado";
                //En caso de ser empleado, sólo podrá cambiar su contraseña y realizar reservas y respaldos de archivos CSV solicitados
                gbPropiedades.Enabled = false;
                gbReservas.Controls["btnModificarReserva"].Enabled = false;
                gbReservas.Controls["btnEliminarReserva"].Enabled = false;
                gbUsuarios.Controls["btnAgregarUsuario"].Enabled = false;
                gbClientes.Enabled = false;
                ActualizarListadoUsuarios();
                RefrescarGrillaReservas();
            }
            else
            {
                //this.Enabled = false;
            }


        }
        private void InicializarVariables()
        {
            ruta = Application.StartupPath;
            archivo = Path.Combine(ruta, "datosempresa.dat");
            fileStream = null;
            DeserializarArchivo();
            fInicioSesion = new FInicioSesion();
            fReservacion = new FReservacion();
            fCalendario = new FCalendario();
            fUsuario = new FUsuario();
            esAdmin = false;
            esUser = false;
            idUser = -1;
            passUser = "";
            timer.Start();
            ConfigurarGrillas();


        }
        private void DeserializarArchivo()
        {
            try
            {
                //Si se rompe la serializacion hay que comentar el if else y crear una nueva empresa, generar un admin y volver a crear los usuarios
                //miEmpresa = new Empresa();

                if (File.Exists(archivo))
                {
                    fileStream = new FileStream(archivo, FileMode.Open, FileAccess.Read);

                    bf = new BinaryFormatter();
                    miEmpresa = (Empresa)bf.Deserialize(fileStream);
                }
                else
                {
                    miEmpresa = new Empresa();
                    MessageBox.Show("No se pudo encontrar el archivo, se generó una nueva empresa", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error De Deserializacion: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }

                //RefrescarGrilla();

            }
        }
        private string ObtenerRutaImportarcion()
        {
            string rutaArchivo = "";
            ofdImportar = new OpenFileDialog();

            ofdImportar.Filter = "Archivos CSV|*.csv";
            ofdImportar.Title = "Selecciona un archivo CSV";

            if (ofdImportar.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = ofdImportar.FileName;
            }

            return rutaArchivo;
        }

        private bool ProcesarArchivoImportacion(string rutaArchivo)
        {
            bool exito = false;
            string linea;
            try
            {
                if (!string.IsNullOrEmpty(rutaArchivo))
                {
                    linea = "";
                    fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(fileStream);

                    while (!reader.EndOfStream)
                    {
                        linea = reader.ReadLine();
                        string[] datos = linea.Split(',');
                        if (datos.Length == 6)
                        {
                            exito = ProcesarLineaImportacion(datos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }

                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            return exito;
        }

        private bool ProcesarLineaImportacion(string[] datos)
        {
            bool exito = false;

            try
            {
                int dni = Convert.ToInt32(datos[0]);
                int nroPropiedad = Convert.ToInt32(datos[1]);
                DateTime reservacion = Convert.ToDateTime(datos[2]);
                DateTime fechaIngreso = Convert.ToDateTime(datos[3]);
                DateTime fechaEgreso = Convert.ToDateTime(datos[4]);
                string propietario = datos[6];
                int nroReserva = Convert.ToInt32(datos[5]);
                Cliente encontrado = miEmpresa.BuscarCliente(dni);
                Propiedad propiedad = miEmpresa.BuscarPropiedad(nroPropiedad);
                Reserva importada = new Reserva(encontrado, propiedad, reservacion, fechaIngreso, fechaEgreso, nroReserva, propietario);
                exito = propiedad.AgregarReserva(importada);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return exito;
        }
        #endregion
        #region Usuarios
        private void VerificarInicio()
        {
            fInicioSesion.ShowDialog();
            DialogResult resultado = fInicioSesion.DialogResult;

            switch (resultado)
            {
                case DialogResult.OK: //Entrar como user
                    idUser = Convert.ToInt32(fInicioSesion.tbId.Text);
                    passUser = fInicioSesion.tbPass.Text;
                    IniciarSesion();
                    break;
                case DialogResult.Yes: //Entrar como admin
                    idUser = 1;
                    passUser = "admin";
                    IniciarSesion();
                    break;
                case DialogResult.No: //Entrar como empleado
                    idUser = 2;
                    passUser = "empleado";
                    IniciarSesion();
                    break;
                case DialogResult.Cancel: //Mostrar acerca
                    MostrarAcerca();
                    break;

            }
        }
        
        private void IniciarSesion()
        {
            Usuario user = miEmpresa.BuscarUsuario(idUser); //Buscamos en la empresa un usuario con el ID especificado
            if (user != null)
            {
                esUser = user.ComprobarPass(passUser); //Preguntamos al usuario por su contraseña

                if (esUser) //Si la contraseña dada es correcta, preguntamos por el perfil del usuario
                {
                    esAdmin = user.EsAdmin;
                }
            }
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            if (AgregarUsuario())
            {
                MessageBox.Show("¡Se añadió un nuevo usuario!", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ActualizarListadoUsuarios();
            }
            else
            {
                MessageBox.Show("No se añadió un usuario", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ActualizarListadoUsuarios()
        {
            lbUsers.Items.Clear();
            Usuario[] usuarios = miEmpresa.VerUsuarios;
            foreach (Usuario u in usuarios)
            {
                string userID = u.Id.ToString();
                string userTipo = u.Tipo == 1 ? "Administrador" : "Empleado";
                int longitudTotal = 20;

                string textoFormateado = $"{userID.PadRight(longitudTotal)}{userTipo}";
                if (esAdmin)
                {
                    lbUsers.Items.Add(textoFormateado);
                }
                else
                {
                    if (u.Id == idUser)
                    {
                        lbUsers.Items.Add(textoFormateado);
                    }
                }
            }
        }

        private bool AgregarUsuario()
        {
            bool agregado = false;

            try
            {
                DialogResult resultado = fUsuario.ShowDialog();
                if (resultado == DialogResult.OK)
                {
                    int id = Convert.ToInt32(fUsuario.tbId.Text);
                    string pass = fUsuario.tbPass.Text;
                    int tipo;
                    if (fUsuario.rbAdmin.Checked)
                    {
                        tipo = 1;
                    }
                    else
                    {
                        tipo = 2;
                    }

                    agregado = miEmpresa.AgregarUsuario(id, pass, tipo);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                fUsuario.Dispose();
            }

            return agregado;
        }

        private void btnCambiarPass_Click(object sender, EventArgs e)
        {
            bool userSeleccionado = lbUsers.SelectedItem != null;

            if (userSeleccionado)
            {
                if (ModificarPass())
                {
                    MessageBox.Show("¡Contraseña modificada!", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ActualizarListadoUsuarios();
                }
                else
                {
                    MessageBox.Show("No se modificó la contraseña", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("¡Debe seleccionar un usuario!", "Usuario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private bool ModificarPass()
        {
            bool modificado = false;
            string user = lbUsers.SelectedItem.ToString();
            string[] datosUser = user.Split('A', 'E');
            string idUser = datosUser[0].Trim();
            Usuario aux = miEmpresa.BuscarUsuario(Convert.ToInt32(idUser));

            fUsuario = new FUsuario(aux);

            DialogResult resultado = fUsuario.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                int id = Convert.ToInt32(fUsuario.tbId.Text);
                string pass = fUsuario.tbPass.Text;

                modificado = aux.CambiarPass(aux, pass);
            }
            return modificado;
        }
        #endregion
        #region Cerrar Aplicacion
        private void FInicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ConfirmarCerrar())
            {
                if (ConfirmarRespaldo())
                {
                    SerializarArchivo();
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        private bool ConfirmarRespaldo()
        {
            bool confirmarRespaldo = false;
            DialogResult resultado = MessageBox.Show("¿Desea respaldar los cambios realizados?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                confirmarRespaldo = true;
            }

            return confirmarRespaldo;
        }

        private bool ConfirmarCerrar()
        {
            bool cerrarAplicacion = false;
            DialogResult resultado = MessageBox.Show("¿Desea salir de la aplicación?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                cerrarAplicacion = true;
            }

            return cerrarAplicacion;
        }
        #endregion
        #region Grilla
        private void ConfigurarGrillas()
        {
            dgvReservas.Columns.Clear();
            dgvPropiedades.Columns.Clear();
            // Obtener las propiedades de la clase Reserva (asumo que Reserva es una clase)
            var propiedades = typeof(Reserva).GetProperties();

            int anchoTotal = dgvReservas.ClientSize.Width;
            // Agregar columnas al DataGridView basado en las propiedades del objeto
            foreach (var propiedad in propiedades)
            {
                // Crear una nueva columna
                var columna = new DataGridViewTextBoxColumn();

                columna.Name = propiedad.Name;
                // Asignar el nombre de la propiedad como encabezado de la columna
                columna.HeaderText = propiedad.Name;

                // Configurar AutoSizeMode para que la columna ajuste su ancho automáticamente
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // Calcular el ancho de la columna basado en el ancho total disponible
                columna.FillWeight = (float)anchoTotal / propiedades.Length;

                // Agregar la columna al DataGridView
                dgvReservas.Columns.Add(columna);
            }

            propiedades = typeof(Propiedad).GetProperties();

            anchoTotal = dgvPropiedades.ClientSize.Width;
            // Agregar columnas al DataGridView basado en las propiedades del objeto
            foreach (var propiedad in propiedades)
            {
                if (propiedad.Name == "Servicios")
                {
                    var serviciosColumn = new DataGridViewComboBoxColumn();
                    serviciosColumn.Name = "Servicios";
                    serviciosColumn.HeaderText = "Servicios";
                    serviciosColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    serviciosColumn.FillWeight = (float)anchoTotal / propiedades.Length;
                    dgvPropiedades.Columns.Add(serviciosColumn);
                }
                else
                {
                    var columna = new DataGridViewTextBoxColumn();
                    columna.Name = propiedad.Name;
                    columna.HeaderText = propiedad.Name;
                    columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    columna.FillWeight = (float)anchoTotal / propiedades.Length;
                    dgvPropiedades.Columns.Add(columna);
                }
            }
        }

        private DataGridViewComboBoxCell CargarServicios(string[] serviciosPropiedad)
        {
            DataGridViewComboBoxCell servicios = new DataGridViewComboBoxCell();
            foreach (string s in serviciosPropiedad)
            {
                servicios.Items.Add(s);

            }
            return servicios;
        }
        private void RefrescarGrilla()
        {
            dgvPropiedades.Rows.Clear(); //Limpiamos la grilla

            foreach (Propiedad p in miEmpresa.ObtenerPropiedades()) //Recorremos cada propiedad

            {
                DataGridViewComboBoxCell servicios = CargarServicios(p.Servicios);
                int nro = p.Nro;
                string tipo = ""; //Si p is Hotel o p is Casa
                string direccion = "";
                string codigo = "";
                double precio = 0.00;
                string nombre = "";
                string localidad = "";
                int pasajeros = 0;
                if (p is Hotel)
                {
                    Hotel h = (Hotel)p;
                    tipo = "Habitación de hotel";
                    direccion = h.Direccion;
                    precio = h.ObtenerPrecioBase();
                    localidad = h.Localidad;
                    nombre = h.Nombre;
                    h.CalcularCantidadPasajeros();
                    pasajeros = h.Pasajeros;
                    codigo = h.Codigo;
                }
                else
                {
                    Casa c = (Casa)p;
                    tipo = "Casa";
                    direccion = c.Direccion;
                    precio = c.ObtenerPrecioBase();
                    localidad = c.Localidad;
                    nombre = c.Nombre;
                    c.CalcularCantidadPasajeros();
                    pasajeros = c.Pasajeros;
                    codigo = c.Codigo;
                }

                dgvPropiedades.Rows.Add(nro,codigo, nombre, direccion, localidad, pasajeros, precio);
                int rowIndex = dgvPropiedades.Rows.Count - 2;
                dgvPropiedades.Rows[rowIndex].Cells["Servicios"] = servicios;
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            lbHora.Text = dateTime.ToString("HH:mm:ss");
        }
        #endregion
        #region Casa
        private void btnAgregarCasa_Click(object sender, EventArgs e)
        {
            if (AgregarCasa())
            {
                MessageBox.Show("¡Propiedad agregada con éxito!", "Casa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("No se agregó una casa", "Casa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool AgregarCasa()
        {
            bool exito = false;
            int id = miEmpresa.UltimoID; //Obtenemos  el último ID
            id++; //Incremento en 1
            CasasA aCasa = new CasasA(id, miEmpresa.ObtenerPropietarios());
            try
            {
                if (aCasa.ShowDialog() == DialogResult.OK)
                {
                    int nroCasa = Convert.ToInt32(aCasa.tbNroCasa.Text);
                    string nombre = aCasa.tbNombre.Text;
                    string codProp = aCasa.cbPropietarios.Text;
                    string direccion = aCasa.tbDireccion.Text;
                    string localidad = aCasa.tbLocalidad.Text;
                    int nroCamas = Convert.ToInt32(aCasa.tbCamas.Text);
                    double pBase = Convert.ToDouble(aCasa.tbPrecio.Text);

                    Casa nuevo = new Casa(direccion, nroCamas, nroCasa, pBase, nombre, localidad, codProp);
                    nuevo.fotos[0] = (Image)aCasa.imagen1.Image.Clone();
                    nuevo.fotos[1] = (Image)aCasa.imagen2.Image.Clone();
                    nuevo.fotos[2] = (Image)aCasa.imagen3.Image.Clone();
                    nuevo.fotos[3] = (Image)aCasa.imagen4.Image.Clone();

                    string[] serviciosSpliteados = serviciosCasa(aCasa);
                    if (serviciosSpliteados.Length > 0)
                    {
                        nuevo.Servicios = serviciosSpliteados;
                    }
                    Propietario propietario = miEmpresa.ObtenerPropietario(codProp);
                    propietario.AgregarPropiedad(nuevo);
                    miEmpresa.AgregarAlojamiento(nuevo);

                    //reservas.cboxNro.Items.Add(nroCasa);

                    exito = true;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("El formato que ingreso no es correcto.", ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                aCasa.Dispose();
            }

            return exito;
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
        private bool EditarCasa(Casa c)
        {
            bool exito = false;

            CasasA casaA = new CasasA(c.Nro,miEmpresa.ObtenerPropietarios()); //Creo un nuevo formulario

            //Cargamos los campos con el objeto que viene para modificar
            casaA.tbNombre.Text = c.Nombre;
            casaA.tbLocalidad.Text = c.Localidad;
            
            casaA.tbDireccion.Text = c.Direccion;
            casaA.tbCamas.Text = c.CantCamas.ToString();
            casaA.tbPrecio.Text = c.ObtenerPrecioBase().ToString();

            casaA.imagen1.Image = c.fotos[0];
            casaA.imagen2.Image = c.fotos[1];
            casaA.imagen3.Image = c.fotos[2];
            casaA.imagen4.Image = c.fotos[3];
            //cbLimpieza.Checked cbPileta.Checked cbWifi.Checked cbLimpieza.Checked cbDesayuno.Checked cbMascotas.Checked
            foreach (string i in c.Servicios)
            {
                if (i == "Cochera")
                {
                    casaA.cbCochera.Checked = true;
                }
                if (i == "Pileta")
                {
                    casaA.cbPileta.Checked = true;
                }
                if (i == "Wifi")
                {
                    casaA.cbWifi.Checked = true;
                }
                if (i == "Limpieza")
                {
                    casaA.cbLimpieza.Checked = true;
                }
                if (i == "Desayuno")
                {
                    casaA.cbDesayuno.Checked = true;
                }
                if (i == "Mascotas")
                {
                    casaA.cbMascotas.Checked = true;
                }

            }

            //Si dan a aceptar
            if (casaA.ShowDialog() == DialogResult.OK)
            {
                c.Nombre = casaA.tbNombre.Text;
                c.Direccion = casaA.tbDireccion.Text;
                c.CantCamas = Convert.ToInt32(casaA.tbCamas.Text);
                c.PrecioBase = Convert.ToDouble(casaA.tbPrecio.Text);
                c.Localidad = casaA.tbLocalidad.Text;

                c.fotos[0] = (Image)casaA.imagen1.Image.Clone();
                c.fotos[1] = (Image)casaA.imagen2.Image.Clone();
                c.fotos[2] = (Image)casaA.imagen3.Image.Clone();
                c.fotos[3] = (Image)casaA.imagen4.Image.Clone();
                string[] serviciosEditados = new string[6];
                serviciosEditados = serviciosCasa(casaA);
                c.Servicios = serviciosEditados;
                exito = true;
            }

            return exito;
        }
        private void agregarCasaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AgregarCasa())
            {
                MessageBox.Show("¡Propiedad agregada con éxito!", "Casa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("No se agregó una casa", "Casa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
        #region Propiedad
        private void btnEliminarPropiedad_Click(object sender, EventArgs e)
        {
            try
            {
                if (EliminarPropiedad())
                {
                    MessageBox.Show("¡Propiedad eliminada con éxito!", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    RefrescarGrilla();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar propiedad: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private bool EliminarPropiedad()
        {
            bool exito = false;
            int indice;
            indice = ObtenerIndiceSeleccionado();
            Propiedad propiedad = BuscarPropiedad(indice);
            if (propiedad != null)
            {
                if (VerificarLibreReservas(propiedad))
                {
                    DialogResult resultado = MessageBox.Show("¿Desea eliminar la propiedad: " + propiedad.Nro + "?", "Confirmar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        exito = miEmpresa.EliminarPropiedad(indice);
                    }
                }
                else
                {
                    throw new InvalidOperationException("La propiedad tiene reservas pendientes y no se puede eliminar.");
                }

            }
            return exito;
        }
        private void btnModificarPropiedad_Click(object sender, EventArgs e)
        {
            if (EditarPropiedad())
            {
                MessageBox.Show("Propiedad editada con éxito", "Propiedad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("No se realizó la modificación", "Propiedad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool EditarPropiedad()
        {
            try
            {

                bool exito = false;
                int indice;
                indice = ObtenerIndiceSeleccionado();

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
                MessageBox.Show("Ocurrió un error al editar la propiedad: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void modificarPropiedadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditarPropiedad())
            {
                MessageBox.Show("Propiedad editada con éxito", "Propiedad", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("No se realizó la modificación", "Propiedad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private Propiedad BuscarPropiedad(int indice)
        {

            Propiedad propiedad;
            propiedad = miEmpresa.BuscarPropiedad(indice);

            return propiedad;
        }
        private void eliminarPropiedadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (EliminarPropiedad())
                {
                    MessageBox.Show("¡Propiedad eliminada con éxito!", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    RefrescarGrilla();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar propiedad: " + ex.Message, "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private int ObtenerIndiceSeleccionado()
        {
            int indice = -1;

            int selectedrowindex = dgvPropiedades.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvPropiedades.Rows[selectedrowindex];
            DataGridViewCell selectedCell = selectedRow.Cells["Nro"];
            indice = Convert.ToInt32(selectedCell.Value);

            return indice;
        }
        #endregion
        #region Hotel
        private bool EditarHotel(Hotel h)
        {
            bool exito = false;
            HotelesA hotelesA = new HotelesA(h.Nro); //Creamos el formulario

            //Cargamos los datos
            hotelesA.tbDireccion.Text = h.Direccion;
            hotelesA.tbNroHabitacion.Text = h.Nro.ToString();
            hotelesA.tbNroHabitacion.Enabled = false;
            hotelesA.tbPrecio.Text = h.ObtenerPrecioBase().ToString();
            hotelesA.imagen1.Image = h.fotos[0];
            hotelesA.imagen2.Image = h.fotos[1];
            hotelesA.imagen3.Image = h.fotos[2];
            hotelesA.imagen4.Image = h.fotos[3];


            foreach (string i in h.Servicios)
            {
                if (i == "Cochera")
                {
                    hotelesA.cbCochera.Checked = true;
                }
                if (i == "Pileta")
                {
                    hotelesA.cbPileta.Checked = true;
                }
                if (i == "Wifi")
                {
                    hotelesA.cbWifi.Checked = true;
                }
                if (i == "Limpieza")
                {
                    hotelesA.cbLimpieza.Checked = true;
                }
                if (i == "Desayuno")
                {
                    hotelesA.cbDesayuno.Checked = true;
                }
                if (i == "Mascotas")
                {
                    hotelesA.cbMascotas.Checked = true;
                }

            }


            //Preguntamos por las estrellas
            if (h.ObtenerEstrellas())
            {
                hotelesA.radioButton2.Checked = true;
                hotelesA.radioButton3.Checked = false;
            }
            else
            {
                hotelesA.radioButton2.Checked = false;
                hotelesA.radioButton3.Checked = true;
            }

            //Preguntamos por las piezas
            switch (h.ObtenerPiezas())
            {
                case 1:
                    hotelesA.rbSimple.Checked = true;
                    hotelesA.rbDoble.Checked = false;
                    hotelesA.rbTriple.Checked = false;
                    break;
                case 2:
                    hotelesA.rbSimple.Checked = false;
                    hotelesA.rbDoble.Checked = true;
                    hotelesA.rbTriple.Checked = false;
                    break;
                default:
                    hotelesA.rbSimple.Checked = false;
                    hotelesA.rbDoble.Checked = false;
                    hotelesA.rbTriple.Checked = true;
                    break;
            }

            //Si le dan a aceptar
            if (hotelesA.ShowDialog() == DialogResult.OK)
            {
                //Actualizamos los datos
                h.Nombre = hotelesA.tbNombre.Text;
                h.Localidad = hotelesA.tbLocalidad.Text;
                h.Direccion = hotelesA.tbDireccion.Text;
                h.PrecioBase = Convert.ToDouble(hotelesA.tbPrecio.Text);
                h.fotos[0] = (Image)hotelesA.imagen1.Image.Clone();
                h.fotos[1] = (Image)hotelesA.imagen2.Image.Clone();
                h.fotos[2] = (Image)hotelesA.imagen3.Image.Clone();
                h.fotos[3] = (Image)hotelesA.imagen4.Image.Clone();
                string[] serviciosEditados = new string[6];
                serviciosEditados = serviciosHotel(hotelesA);
                h.Servicios = serviciosEditados;

                if (hotelesA.rbSimple.Checked)
                {
                    h.Habitacion = 1;
                }
                else if (hotelesA.rbDoble.Checked)
                {
                    h.Habitacion = 2;
                }
                else if (hotelesA.rbTriple.Checked)
                {
                    h.Habitacion = 3;
                }

                if (hotelesA.radioButton3.Checked)
                {
                    h.TresEstrellas = false;
                }
                else
                {
                    h.TresEstrellas = true;
                }



                exito = true;
            }
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

        private void btnAgregarHotel_Click(object sender, EventArgs e)
        {
            if (AgregarHotel())
            {
                MessageBox.Show("Creaste un Hotel capo");
                RefrescarGrilla();
            }
            else
            {
                MessageBox.Show("NT");
            }
        }

        private void agregarHotelToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (AgregarHotel())
            {
                MessageBox.Show("Propiedad agregada con éxito!", "Casa");
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
            int id = miEmpresa.UltimoID; //Obtenemos  el último ID
            id++; //Incremento en 1
            HotelesA aHotel = new HotelesA(id);
            try
            {
                if (aHotel.ShowDialog() == DialogResult.OK)
                {
                    int nroHabitacion = Convert.ToInt32(aHotel.tbNroHabitacion.Text);
                    string nombre = aHotel.tbNombre.Text;
                    string codProp = aHotel.cbPropietario.Text;
                    string direccion = aHotel.tbDireccion.Text;
                    string localidad = aHotel.tbLocalidad.Text;
                    double pBase = Convert.ToDouble(aHotel.tbPrecio.Text);
                    int habitaciones = 0;
                    bool tresEstrellas = false;
                    if (aHotel.rbSimple.Checked)
                    {
                        habitaciones = 1;
                    }
                    else if (aHotel.rbDoble.Checked)
                    {
                        habitaciones = 2;
                    }
                    else if (aHotel.rbTriple.Checked)
                    {
                        habitaciones = 3;
                    }

                    if (aHotel.radioButton3.Checked)
                    {
                        tresEstrellas = false;
                    }
                    else
                    {
                        tresEstrellas = true;
                    }
                    Hotel nuevo = new Hotel(direccion, habitaciones, nroHabitacion, tresEstrellas, pBase, nombre, localidad, codProp);
                    nuevo.fotos[0] = (Image)aHotel.imagen1.Image.Clone();
                    nuevo.fotos[1] = (Image)aHotel.imagen2.Image.Clone();
                    nuevo.fotos[2] = (Image)aHotel.imagen3.Image.Clone();
                    nuevo.fotos[3] = (Image)aHotel.imagen4.Image.Clone();

                    string[] serviciosSpliteados = serviciosHotel(aHotel);


                    if (serviciosSpliteados.Length > 0)
                    {
                        nuevo.Servicios = serviciosSpliteados;
                    }
                    miEmpresa.AgregarAlojamiento(nuevo);
                    //reservas.cboxNro.Items.Add(nroHabitacion);

                    exito = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al agregar hotel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                aHotel.Dispose();
            }

            return exito;
        }
        #endregion
        #region Reservas
        private bool VerificarLibreReservas(Propiedad propiedad)
        {
            bool tieneReservas = true;
            Reserva[] reservas = propiedad.ObtenerReservas();

            if (reservas != null)
            {
                tieneReservas = false;
            }
            return tieneReservas;
        }

        private void btnAgregarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                bool hayPropiedades = miEmpresa.ObtenerPropiedades().Count > 0;
                bool hayClientes = miEmpresa.CantidadClientes > 0;

                if (hayPropiedades && hayClientes)
                {
                    AgregarReserva();
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

        private void AgregarReserva()
        {
            bool reservado = false;
            fReservacion = new FReservacion(miEmpresa.VerPropiedades, miEmpresa.VerClientes);
            DialogResult resultado = fReservacion.ShowDialog();
            if (resultado == DialogResult.OK)
            {

                int dni = Convert.ToInt32(fReservacion.cbDni.Text);
                int nro = Convert.ToInt32(fReservacion.cbNro.Text);
                DateTime ingreso = fReservacion.Ingreso.SelectionStart;
                DateTime egreso = fReservacion.Egreso.SelectionEnd;

                reservado = miEmpresa.HacerReserva(dni, nro, ingreso, egreso);

                if (reservado)
                {
                    fReservacion.Ingreso.AddBoldedDate(ingreso);
                    fReservacion.Egreso.AddBoldedDate(egreso);
                    fCalendario.calendariaVer.AddBoldedDate(ingreso);
                    fCalendario.calendariaVer.AddBoldedDate(egreso);

                    fCalendario.calendarioVerS.AddBoldedDate(ingreso);
                    fCalendario.calendarioVerS.AddBoldedDate(egreso);
                }

                else
                {
                    MessageBox.Show("Detectamos una reserva Activa para la propiedad: " + nro + "\n" + " Entre la fecha: " + ingreso.ToString("dd/mm/yyyy") + "-" + egreso.ToString("dd/mm/yyyy"));
                }
            }

            RefrescarGrillaReservas();
        }

        private void RefrescarGrillaReservas()
        {

            // Limpiar las filas existentes (si es necesario)
            dgvReservas.Rows.Clear();

            // Obtener las propiedades del objeto
            //Reserva[] reservas = miEmpresa.VerReservaciones;
            Reserva[] reservas = miEmpresa.ObtenerReservas();

            // Obtener las propiedades de la clase Reserva (asumo que Reserva es una clase)
            var propiedades = typeof(Reserva).GetProperties();

            foreach (Reserva r in reservas)
            {
                // Agregar fila con los valores del objeto
                DataGridViewRow fila = new DataGridViewRow();
                foreach (var propiedad in propiedades)
                {
                    object valor = propiedad.GetValue(r);

                    if (valor is DateTime)
                    {
                        // Formatear el valor DateTime como DD/MM/YYYY
                        fila.Cells.Add(new DataGridViewTextBoxCell { Value = ((DateTime)valor).ToString("dd/MM/yyyy") });
                    }
                    else
                    {
                        // Para otras propiedades, simplemente mostrar el valor tal como está
                        fila.Cells.Add(new DataGridViewTextBoxCell { Value = valor });
                    }
                }
                // Agregar la fila al DataGridView
                dgvReservas.Rows.Add(fila);
            }

            ActualizarGraficos();
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

            int selectedrowindex = dgvReservas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvReservas.Rows[selectedrowindex];

            int idReserva = Convert.ToInt32(selectedRow.Cells["Nro"].Value); //Busco la reserva por su id
            int idPropiedad = Convert.ToInt32(selectedRow.Cells["NroPropiedad"].Value); //Busco la propiedad por su id

            Propiedad propiedad = miEmpresa.BuscarPropiedad(idPropiedad);
            Reserva reserva = propiedad.BuscarReserva(idReserva);

            if (reserva != null)
            {
                DialogResult resultado = MessageBox.Show("¿Desea eliminar la reserva: " + reserva.Nro + "?", "Si", MessageBoxButtons.YesNoCancel);
                if (resultado == DialogResult.Yes)
                {
                    miEmpresa.EliminarReserva(reserva); //Esto ya estaba habria que eliminarlo
                    exito = propiedad.EliminarReserva(idReserva);
                }
            }
            return exito;
        }

        private void clientesYReservasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string csv = miEmpresa.ExportarCSV();
            ruta = Application.StartupPath;
            string nombreArchivo = "datos.csv";
            string rutacsv = Path.Combine(ruta, nombreArchivo);

            string filePath = "datos.csv";

            try
            {
                // Escribe el contenido en el archivo
                File.WriteAllText(filePath, csv);

                Console.WriteLine($"Archivo CSV creado exitosamente en {filePath}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al crear el archivo CSV: {ex.Message}");
            }
        }

        private void calendarioDeReservasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (ExportarCalendarioReservar())
                {
                    MessageBox.Show("¡Calendario de reservas exportado con éxito!", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("¡No se pudo exportar!", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al exportar: " + ex.Message, "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool ExportarCalendarioReservar()
        {
            bool exito = false;
            int indice;
            indice = ObtenerIndiceSeleccionado();
            Propiedad propiedad = BuscarPropiedad(indice);
            Reserva[] reservas = propiedad.ObtenerReservas();
            ruta = Application.StartupPath;
            string nombreArchivo = "Calendario_Reservas_" + DateTime.Now.ToString("dd-MM-yyyy");
            string extension = ".csv";
            writer = new StreamWriter(ruta + nombreArchivo + extension);
            try
            {
                //public Reserva(Cliente unCliente,Propiedad unaPropiedad, DateTime reserva, DateTime fechaIngreso, DateTime fechaOutgreso, int nroReserva)
                foreach (Reserva r in reservas)
                {
                    string linea = $"{r.Dni},{r.NroPropiedad},{r.Reservacion},{r.FechaIngreso},{r.FechaOutgreso},{r.Nro}";

                    writer.WriteLine(linea);
                }

                exito = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                writer.Close();
                writer.Dispose();
            }

            return exito;
        }

        private void calendarioDeReservasToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (ImportarCalendario())
                {
                    MessageBox.Show("¡Calendario de reservas importado con éxito!", "Importar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    RefrescarGrillaReservas();
                }
                else
                {
                    MessageBox.Show("¡No se pudo importar!", "Importar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al importar: " + ex.Message, "Importar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModificarReserva())
                {
                    MessageBox.Show("¡Reserva modificada!", "Reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarGrillaReservas();
                }
                else
                {
                    MessageBox.Show("No se pudo modificó la reserva", "Reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrió un error al modificar la reserva: " + ex.Message, "Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ModificarReserva()
        {
            bool exito = false;

            int selectedrowindex = dgvReservas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvReservas.Rows[selectedrowindex];

            int idReserva = Convert.ToInt32(selectedRow.Cells["Nro"].Value); //Busco la reserva por su id
            int idPropiedad = Convert.ToInt32(selectedRow.Cells["NroPropiedad"].Value); //Busco la propiedad por su id

            Propiedad propiedad = miEmpresa.BuscarPropiedad(idPropiedad);
            Reserva reserva = propiedad.BuscarReserva(idReserva);




            if (reserva != null)
            {
                exito = EditarReserva(reserva, propiedad);
            }

            return exito;
        }
        private bool EditarReserva(Reserva reserva, Propiedad propiedad)
        {
            bool exito = false;

            try
            {
                fReservacion = new FReservacion(miEmpresa.VerPropiedades, miEmpresa.VerClientes);
                fReservacion.cbDni.Text = reserva.Dni.ToString();
                fReservacion.cbDni.Enabled = false;
                fReservacion.cbNro.Text = reserva.Nro.ToString();
                fReservacion.cbNro.Enabled = false;
                fReservacion.Ingreso.SelectionStart = reserva.FechaIngreso;
                fReservacion.Egreso.SelectionEnd = reserva.FechaOutgreso;
                fReservacion.MostrarDatosPropiedad(propiedad);
                fReservacion.CargarImagenesPropiedad(propiedad);
                fReservacion.btnReservar.Text = "Modificar";
                if (fReservacion.ShowDialog() == DialogResult.OK)
                {
                    reserva.FechaIngreso = fReservacion.Ingreso.SelectionStart;
                    reserva.FechaOutgreso = fReservacion.Egreso.SelectionEnd;

                    exito = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ");
            }

            return exito;
        }
        private bool ImportarCalendario()
        {
            bool exito = false;
            string rutaArchivo = string.Empty;

            try
            {
                rutaArchivo = ObtenerRutaImportarcion();
                exito = ProcesarArchivoImportacion(rutaArchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return exito;
        }
        private void modificarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ModificarReserva())
                {
                    MessageBox.Show("¡Reserva modificada!", "Reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarGrillaReservas();
                }
                else
                {
                    MessageBox.Show("No se pudo modificó la reserva", "Reserva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ocurrió un error al modificar la reserva: " + ex.Message, "Reserva", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarReservaToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void agregarReservaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                bool hayPropiedades = miEmpresa.ObtenerPropiedades().Count > 0;
                bool hayClientes = miEmpresa.CantidadClientes > 0;

                if (hayPropiedades && hayClientes)
                {
                    AgregarReserva();
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

        private void agregarTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (EmitirTicket())
                {
                    DialogResult resultado = MessageBox.Show("¡Ticket emitido con éxito!\n¿Desea ir al menú de impresión?", "Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (resultado == DialogResult.Yes)
                    {
                        fImprimirTicket = new FImprimirTicket(miEmpresa);
                        fImprimirTicket.ShowDialog();
                    }
                    RefrescarGrillaReservas();
                }
                else
                {
                    MessageBox.Show("¡No se pudo emitir el ticket!", "Ticket", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al emitir ticket: " + ex.Message, "Ticket", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Graficos
        private void ActualizarGraficos()
        {
            miEmpresa.CalcularEstadisticas();
            bool hayReservas = miEmpresa.ObtenerCantidadReservas() > 0 ? true : false;
            if (hayReservas)
            {
                ActualizarGraficoTorta();
                ActualizarGraficoBarras();
            }
        }

        private void ActualizarGraficoBarras()
        {
            int[] datos = miEmpresa.Pasajeros;
            int totalWidth = pbHuesped.Width; // Usar el ancho del PictureBox
            int totalHeight = pbHuesped.Height; // Usar el alto del PictureBox
            int barCount = datos.Length;
            int barWidth = totalWidth / (barCount * 2); // Ancho de la barra ajustado
            int spacing = totalWidth / (barCount * 2); // Espacio entre las barras ajustado

            Bitmap bmp = new Bitmap(totalWidth, totalHeight);

            using (Graphics ga = Graphics.FromImage(bmp))
            {
                ga.Clear(Color.White); // Fondo blanco

                for (int i = 0; i < barCount; i++)
                {
                    int barHeight = totalHeight * datos[i] / datos.Max(); // Altura ajustada
                    if (i % 2 == 0)
                    {
                        ga.FillRectangle(Brushes.Gold, i * (barWidth + spacing), totalHeight - barHeight, barWidth, barHeight);
                    }
                    else
                    {
                        ga.FillRectangle(Brushes.Blue, i * (barWidth + spacing), totalHeight - barHeight, barWidth, barHeight);

                    }
                }
            }

            pbHuesped.Image = bmp; // Asignar el Bitmap al PictureBox
        }

        private void ActualizarGraficoTorta()
        {
            // Calcular los ángulos en grados para cada porcentaje
            double anguloCasas = miEmpresa.PorcentajeCasas * 3.6;  // Cada 1% es igual a 3.6 grados
            double anguloHoteles = miEmpresa.PorcentajeHoteles * 3.6;

            // Crear una nueva imagen y un objeto Graphics para dibujar en ella
            Bitmap image = new Bitmap(298, 267);
            Graphics g = Graphics.FromImage(image);

            // Dibujar una cuña que representa el porcentaje de casas en rojo
            Brush redBrush = new SolidBrush(Color.Gold);
            g.FillPie(redBrush, 0, 0, image.Width, image.Height, 0, (float)anguloCasas);

            // Dibujar una cuña que representa el porcentaje de hoteles en azul
            Brush blueBrush = new SolidBrush(Color.Blue);
            g.FillPie(blueBrush, 0, 0, image.Width, image.Height, (float)anguloCasas, (float)anguloHoteles);

            // Liberar los recursos del objeto Graphics
            g.Dispose();

            // Asignar la imagen al PictureBox
            pbTipo.Image = image;
        }
        #endregion
        #region Cliente
        private void btnAgregaCliente_Click(object sender, EventArgs e)
        {
            if (AgregarCliente())
            {
                MessageBox.Show("¡Agregado con éxito!");
                ActualizarListadoClientes();
            }
            else
            {
                MessageBox.Show("No se pudo agregar cliente :(");
            }
        }

        private void ActualizarListadoClientes()
        {
            lbClientes.Items.Clear();
            Cliente[] c = miEmpresa.VerClientes;
            for (int i = 0; i < miEmpresa.CantidadClientes; i++)
            {
                lbClientes.Items.Add(c[i].ToString());
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
                    exito = true;
                }
                aCliente.Dispose();


            }
            catch (Exception ex)
            {
                MessageBox.Show("¡Ocurrió un error al agregar cliente: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return exito;
        }
        private void agregarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AgregarCliente())
            {
                MessageBox.Show("¡Agregado con éxito!");
                ActualizarListadoClientes();
            }
            else
            {
                MessageBox.Show("No se pudo agregar cliente :(");
            }
        }

        #endregion
        #region Ticket
        private void btnEmitirTicket_Click(object sender, EventArgs e)
        {
            try
            {
                if (EmitirTicket())
                {
                    DialogResult resultado = MessageBox.Show("¡Ticket emitido con éxito!\n¿Desea ir al menú de impresión?", "Ticket", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (resultado == DialogResult.Yes)
                    {
                        fImprimirTicket = new FImprimirTicket(miEmpresa);
                        fImprimirTicket.ShowDialog();
                    }
                    RefrescarGrillaReservas();
                }
                else
                {
                    MessageBox.Show("¡No se pudo emitir el ticket!", "Ticket", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al emitir ticket: " + ex.Message, "Ticket", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool EmitirTicket()
        {
            bool emitido = false;

            int selectedrowindex = dgvReservas.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgvReservas.Rows[selectedrowindex];
            DataGridViewCell selectedCell = selectedRow.Cells["Nro"];
            int nroReserva = Convert.ToInt32(selectedCell.Value);

            Reserva reserva = miEmpresa.BuscarReserva(nroReserva);

            if (reserva != null)
            {
                fTicket = new FTicket();
            }
            try
            {

                fTicket.lbTicket.Items.Add("Nº Reserva: " + reserva.Nro);
                fTicket.lbTicket.Items.Add("Nombre: " + reserva.Nombre);
                fTicket.lbTicket.Items.Add("Dni: " + reserva.Dni);
                fTicket.lbTicket.Items.Add("");

                fTicket.lbTicket.Items.Add("Fecha de registro de reserva: " + reserva.Reservacion);
                fTicket.lbTicket.Items.Add("Fecha de ingreso: " + reserva.FechaIngreso.ToShortDateString());
                fTicket.lbTicket.Items.Add("Fecha de salida: " + reserva.FechaOutgreso.ToShortDateString());
                fTicket.lbTicket.Items.Add("Total de dias que se hospedo: " + reserva.Dias + " dias");
                fTicket.lbTicket.Items.Add("Cantidad de pasajeros: " + reserva.ObtenerPasajeros());
                fTicket.lbTicket.Items.Add("");

                fTicket.lbTicket.Items.Add("Numero de propiedad: " + reserva.NroPropiedad);
                fTicket.lbTicket.Items.Add("Nombre de propiedad: " + reserva.ObtenerNombrePropiedad());
                fTicket.lbTicket.Items.Add("Direccion: " + reserva.Direccion);
                fTicket.lbTicket.Items.Add("Servicios:");
                string[] servicios = reserva.ObtenerServicios().Split(';');
                foreach (string s in servicios)
                {
                    fTicket.lbTicket.Items.Add(s);
                }
                fTicket.lbTicket.Items.Add("");

                fTicket.lbTicket.Items.Add(string.Format("Costo x dia: {0,8:C}", reserva.ObtenerCostoPorDia()));
                //lbTicket.Items.Add(string.Format("Costo total: {0,8:C}", miEmpresa.CalcularTotal(ticket.Dni, ticket.NroPropiedad)));
                fTicket.lbTicket.Items.Add(string.Format("Costo total: {0,8:C}", reserva.ObtenerCostoTotal()));

                fTicket.ShowDialog();
                miEmpresa.RetirarReserva(reserva);
                emitido = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al emitir el ticket: " + ex.Message);
            }
            finally
            {
                if (fTicket != null)
                {
                    fTicket.Dispose();
                }
            }
            return emitido;
        }

        private void tsmImprimir_Click(object sender, EventArgs e)
        {
            fImprimirTicket = new FImprimirTicket(miEmpresa);
            fImprimirTicket.ShowDialog();
        }

        #endregion
        #region Acerca de y Ayuda
        private void MostrarAcerca()
        {
            string[] integrantes = new string[6] { "Altamirano Martin", "Caceres Micaela", "Johnston Tomas", "Lacabane Ignacio", "Onores Matias", "Russo Barbagelata Valentino" };
            string grupo = "";
            foreach (string s in integrantes)
            {
                grupo += s;
            }
            MessageBox.Show(grupo + "Laboratorio de computación II\nUTN FRP - 2023", "Desarrollado por", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarAcerca();
        }

        private void ayudaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MostrarAyuda();
        }

        private void MostrarAyuda()
        {
            fNavegador = new FNavegador();
            fNavegador.ShowDialog();
        }
        #endregion

        private void btnAgregarPropietario_Click(object sender, EventArgs e)
        {
            try
            {
                if (AgregarPropietario())

                {
                    MessageBox.Show("Agregado");
                }
                else
                {
                    MessageBox.Show("No agregado");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            
        }

        private bool AgregarPropietario()
        {
            bool exito = false;
            try
            {
                FPropietario fPropietario = new FPropietario();
                DialogResult resultado = fPropietario.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    string nombre = fPropietario.tbNombre.Text;
                    string codigo = fPropietario.tbCodigo.Text;

                    Propietario propietario = new Propietario(nombre, codigo);
                    exito = miEmpresa.AgregarPropietario(propietario);
                    ActualizarGrillaPropietarios();
                    
                }
            }
            catch (Exception ex)
            {

            }
            return exito;
        }

        private void ActualizarGrillaPropietarios()
        {
            Propietario[] propietarios = miEmpresa.ObtenerPropietarios();
            if (propietarios.Length > 0)
            {
                foreach(Propietario p in propietarios)
                {
                    lbPropietarios.Items.Clear();
                    lbPropietarios.Items.Add(p.Nombre);
                }
            }
        }

        private void btnVerResumen_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerResumen())

                {
                    MessageBox.Show("Emitido");
                }
                else
                {
                    MessageBox.Show("No emitido");
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show("Error al emitir: " + ex.Message);
            }
        }

        private bool VerResumen()
        {
            string resumenUnido = miEmpresa.VerResumen();
            ruta = Application.StartupPath;
            string nombreArchivo = "resumen.csv";
            string rutacsv = Path.Combine(ruta, nombreArchivo);

            try
            {
                // Escribe el contenido en el archivo
                File.WriteAllText(rutacsv, resumenUnido);

                Console.WriteLine($"Archivo CSV creado exitosamente en {rutacsv}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al crear el archivo CSV: {ex.Message}");
            }

            return true;
            //Nombre del propietario, codigo, las propiedades, lo recudado por propiedad, total recaudado por prop.
        }

        private void resumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerResumen())

                {
                    MessageBox.Show("Emitido");
                }
                else
                {
                    MessageBox.Show("No emitido");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al emitir: " + ex.Message);
            }
        }

    }

    
}
