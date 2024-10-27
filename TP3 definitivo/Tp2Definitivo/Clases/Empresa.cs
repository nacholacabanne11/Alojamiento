using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using Tp2Definitivo.Clases;
using System.Drawing;

namespace Tp2Definitivo
{
    [Serializable]

    public class Empresa : IExportable
    {
        int cantidadReservas = 1;
        double costoDia;
        List<Cliente> clientes = new List<Cliente>();
        List<Propiedad> propiedades = new List<Propiedad>();
        List<Reserva> reservaciones = new List<Reserva>();
        List<Reserva> ticketsEmitidos = new List<Reserva>();
        List<Usuario> usuarios = new List<Usuario>();
        List<Propietario> propietarios = new List<Propietario>();
        private int ultimoID;
        public int UltimoID
        {
            get
            {
                return ultimoID;
            }
            private set
            {
                ultimoID = value;
            }
        }

        public int GenNro
        {
            get
            {
                return cantidadReservas;
            }
        }
        public int CantidadPropiedades
        {
            get { return propiedades.Count; }
        }
        public int CantidadReservas
        {
            get
            {
                return reservaciones.Count;
            }
        }
        public int CantidadClientes { get { return clientes.Count; } }
        public double CostoDia
        {
            get { return costoDia; }
        }
        public Propiedad[] VerPropiedades { get { return propiedades.ToArray<Propiedad>(); } }
        public Reserva[] VerReservaciones { get { return reservaciones.ToArray<Reserva>(); } }
        public Reserva[] VerTickets { get { return ticketsEmitidos.ToArray<Reserva>(); } }
        public Cliente[] VerClientes { get { return clientes.ToArray<Cliente>(); } }
        public Usuario[] VerUsuarios { get { return usuarios.ToArray<Usuario>(); } }

        public double PorcentajeCasas { get; set; }
        public double PorcentajeHoteles { get; set; }
        public int[] Pasajeros { get; set; }
        public Image Logo { get; set; }

        public Empresa(double costoDia)
        {
            this.costoDia = costoDia;
        }
        public Empresa()
        {

        }
        public bool VerificarReservas(DateTime fechaIngresoNueva, DateTime fechaEgresoNueva, int idPropiedad)
        {
            bool result = true;
            foreach (var reserva in reservaciones)
            {
                // Verificar si las fechas chocan
                if (idPropiedad == reserva.NroPropiedad)
                {
                    if ((fechaIngresoNueva >= reserva.FechaIngreso && fechaIngresoNueva < reserva.FechaOutgreso) ||
                        (fechaEgresoNueva > reserva.FechaIngreso && fechaEgresoNueva <= reserva.FechaOutgreso) ||
                        (fechaIngresoNueva <= reserva.FechaIngreso && fechaEgresoNueva >= reserva.FechaOutgreso))
                    {
                        // Existe un choque de fechas para la misma propiedad
                        result = false;
                    }
                }
            }

            // No hay choques de fechas para la misma propiedad
            return result;
        }
        public void AgregarCliente(Cliente nuevo)
        {
            clientes.Add(nuevo);
        }
        public void AgregarAlojamiento(Propiedad unaPropiedad)
        {
            propiedades.Add(unaPropiedad);
            ultimoID++;
        }
        public bool HacerReserva(int dni, int nro, DateTime ingreso, DateTime egreso)
        {
            bool exito = false;
            Cliente cliente = null;
            Propiedad propiedad = null;

            cliente = BuscarCliente(dni);
            propiedad = BuscarPropiedad(nro);
            
            Reserva nueva = new Reserva(cliente, propiedad, DateTime.Now, ingreso, egreso, cantidadReservas++, propiedad.Codigo);
            if (nueva != null)
            {
                double costoDia = propiedad.ObtenerPrecioBase();
                nueva.AsignarCostoPorDia(costoDia);
                double costoTotal = nueva.Dias * propiedad.PrecioCalcular();
                nueva.AsignarCostoTotal(costoTotal);
            }
            exito = propiedad.AgregarReserva(nueva); //Ahora las propiedades guardan las reservas
            Propietario propietario = ObtenerPropietario(propiedad.Codigo); //codigo del propietario
            propietario.AgregarReserva(nueva);
            reservaciones.Add(nueva); //Asi estaba antes.

            return exito;
        }

        public Cliente BuscarCliente(int dni)
        {
            Cliente cliente = null;
            for (int i = 0; i < clientes.Count; i++)
            {
                if (dni == clientes[i].DNI)
                {
                    cliente = clientes[i];
                }
            }

            return cliente;
        }

        public double CalcularTotal(int dni, int nro)
        {
            double total = 0;
            double pPropiedad = 0;
            for (int i = 0; i < reservaciones.Count; i++)
            {
                if (nro == reservaciones[i].NroPropiedad)
                {
                    pPropiedad = propiedades[i].PrecioCalcular();
                }
            }
            for (int i = 0; i < reservaciones.Count; i++)
            {
                if (dni == reservaciones[i].Dni)
                {
                    total = reservaciones[i].Dias * pPropiedad;
                }
            }
            return total;
        }

        public double ObtenerCostoPorDia(int dni, int nro)
        {
            double pPropiedad = 0;
            for (int i = 0; i < reservaciones.Count; i++)
            {
                if (nro == reservaciones[i].NroPropiedad)
                {
                    pPropiedad = propiedades[i].PrecioCalcular();
                }
            }
            return pPropiedad;
        }

        public double ObtenerCostoPorDia(int nroPropiedad)
        {
            double pPropiedad = 0;
            for (int i = 0; i < ticketsEmitidos.Count; i++)
            {
                if (nroPropiedad == ticketsEmitidos[i].NroPropiedad)
                {
                    pPropiedad = propiedades[i].PrecioCalcular();
                }
            }
            return pPropiedad;
        }

        public bool EliminarPropiedad(int nro)
        {
            bool exito = false;

            for (int i = 0; i < propiedades.Count; i++)
            {
                if (nro == propiedades[i].Nro)
                {
                    propiedades.RemoveAt(i);
                    exito = true;
                }
            }
            return exito;
        }
        public void RetirarReserva(Reserva reserva)
        {
            try
            {
                ticketsEmitidos.Add(reserva); //Lo agregamos a los ticketEmitidos
                EliminarReserva(reserva); //Reviento la reserva 
                Propiedad propiedad = BuscarPropiedad(reserva.NroPropiedad); //Busco la propiedad
                
                propiedad.EliminarReserva(reserva.Nro); //Elimino la reserva
                Propietario propietario = ObtenerPropietario(propiedad.Codigo);
                propietario.AgregarReserva(reserva); //Guardo copia de la reserva en propietario
            }
            catch (Exception ex)
            {
                throw new Exception("Error al retirar la reserva: " + ex.Message);
            }
           
        }
        public void EliminarReserva(int nro)
        {
            //reservaciones.RemoveAt(nro);
        }
        public void EliminarReserva(Reserva reserva)
        {
            reservaciones.Remove(reserva);
        }
        public void DarDeBajaCliente(int nro)
        {
            clientes.RemoveAt(nro);
        }

        public Propiedad BuscarPropiedad(int nroIdentificador)
        {
            Propiedad aux = new Casa("", 0, nroIdentificador, 0.00, "", "",""); //Creamos un objeto vacio con el NroIdentificador

            propiedades.Sort(); //Ordenamos las propiedades
            int index = propiedades.BinarySearch(aux); //Buscamos la posicion de la propiedad buscada
            if (index > -1)
            {
                aux = propiedades[index]; //Guardamos en aux la referencia en el indice 
            }

            return aux;
        }

        public Reserva BuscarReserva(int nroIdentificador)
        {
            Reserva aux = null;

            foreach (Reserva r in reservaciones)
            {
                if (r.Nro == nroIdentificador)
                {
                    aux = r;
                }
            }
            return aux;
        }

        public List<Propiedad> ObtenerPropiedades()
        {
            return propiedades;
        }

        #region Usuario
        public bool AgregarUsuario(int idUser, string passUser, int tipo)
        {
            bool exito = false;
            bool existeID = false;
            try
            {
                foreach (Usuario u in usuarios)
                {
                    if (idUser == u.Id) //Comprobamos que no exista un usuario con el id ingresado
                    {
                        existeID = true;
                    }
                }

                if (existeID)
                {
                    exito = false;
                }
                else
                {
                    Usuario nuevo = new Usuario(idUser, passUser, tipo);

                    usuarios.Add(nuevo);
                    exito = true;
                }
            }
            catch (Exception)
            {
                //Agregar tratamiento de excepcion - #Completar
            }

            return exito;


        }
        public Usuario BuscarUsuario(int idUser)
        {
            Usuario user = null;

            foreach (Usuario u in usuarios)
            {
                if (idUser == u.Id)
                {
                    user = u;
                }
            }

            return user;
        }

        public bool ModificarPass(int idUser, string pass)
        {
            bool modificado = false;
            Usuario user = BuscarUsuario(idUser);

            if (user != null)
            {
                modificado = user.CambiarPass(user, pass);
            }

            return modificado;
        }

        public Reserva[] ObtenerReservas()
        {
            List<Reserva> listaReservas = new List<Reserva>();

            foreach (Propiedad p in propiedades)
            {
                Reserva[] reservasPorPropiedad = p.ObtenerReservas();
                if (reservasPorPropiedad != null)
                {
                    foreach (Reserva r in reservasPorPropiedad)
                    {
                        listaReservas.Add(r);
                    }
                }

            }

            return listaReservas.ToArray();
        }

        public void CalcularEstadisticas()
        {
            Reserva[] reservas = ObtenerReservas();
            int totalReservas = reservas.Length;
            Pasajeros = new int[5];
            int countCasas = 0;
            int countHoteles = 0;

            foreach (Reserva r in reservas)
            {

                if (r.ObtenerTipoPropiedad() == "Casa")
                {
                    countCasas++;
                }
                else
                {
                    countHoteles++;
                }
                switch (r.ObtenerPasajeros())
                {
                    case 1:
                        Pasajeros[0]++;
                        break;
                    case 2:
                        Pasajeros[1]++;
                        break;
                    case 3:
                        Pasajeros[2]++;
                        break;
                    case 4:
                        Pasajeros[3]++;
                        break;
                    default:
                        Pasajeros[4]++;
                        break;
                }
            }

            // Calcular los porcentajes
            PorcentajeCasas = (double)countCasas / totalReservas * 100;
            PorcentajeHoteles = (double)countHoteles / totalReservas * 100;
        }

        public string ExportarCSV()
        {
            
            Cliente[] clientes = VerClientes;
            Reserva[] reservas = ObtenerReservas();

            List<string> csv = new List<string>();

            // Encabezado de clientes
            string encabezadoClientes = "DNI,Nombre,FechaNacimiento";
            csv.Add(encabezadoClientes);

            foreach (Cliente cliente in clientes)
            {
                string lineaCliente = $"{cliente.DNI},{cliente.Nombre},{cliente.FechaNacimiento.ToString("dd/MM/yyyy")}";
                csv.Add(lineaCliente);
            }

            // Encabezado de reservas
            string encabezadoReservas = "IdReserva,IdAlojamiento,IdCliente,CheckIn,CheckOut";
            csv.Add(encabezadoReservas);

            foreach (Reserva reserva in reservas)
            {
                string lineaReserva = $"{reserva.Nro},{reserva.NroPropiedad},{reserva.Dni},{reserva.FechaIngreso.ToString("dd/MM/yyyy")},{reserva.FechaOutgreso.ToString("dd/MM/yyyy")}";
                csv.Add(lineaReserva);
            }

            // Simulación de exportación: Unir las líneas en una cadena
            string csvContent = string.Join("\n", csv);


            return csvContent;

        }
        #endregion

        public int ObtenerCantidadReservas()
        {
            int cantidadReservas = 0;
            try
            {
                foreach (Propiedad p in propiedades)
                {
                    Reserva[] reservas = p.ObtenerReservas();
                    if (reservas != null)
                    {
                        foreach (Reserva r in reservas)
                        {
                            cantidadReservas++;
                        }
                    }
                    
                }
            }
            catch (Exception)
            {

            }
            return cantidadReservas;
        }

        public bool AgregarPropietario(Propietario propietario)
        {
            bool exito = false;
            
            if(propietario != null)
            {
                propietarios.Add(propietario);
                exito = true;
            }
            return exito;
        }
        public Propietario[] ObtenerPropietarios()
        {
            return propietarios.ToArray();
        }
        public Propietario ObtenerPropietario(string codigo)
        {
            Propietario buscado = null;
            foreach(Propietario p in propietarios)
            {
                if(p.ObtenerCodigo() == codigo)
                {
                    buscado = p;    
                }
            }

            return buscado;
        }

        public string VerResumen()
        {
            List<string> lista = new List<string>();
            foreach(Propietario propietario in propietarios)
            {
                double acumulado = 0;

                foreach(Reserva ticket in ticketsEmitidos)
                {
                    if (propietario.ObtenerCodigo() == ticket.ObtenerPropietario())
                    {
                        acumulado += ticket.ObtenerCostoTotal();
                    }
                }

                propietario.Recaudacion = acumulado;

                lista.Add(propietario.ToString());

            }

            string resumenUnido = string.Join("\n", lista);
            return resumenUnido;
        }


 //listar los propietarios


 //bucle de propietarios
 //     acumulado = 0

 //bucle de propietarios

 //     listar reservas

 //     bucle de reservas

	//	? propietario = reserva.propietario
 //           propiertario+=reserva.importe.




 // listar los propietarios
 //   agregando linea por propietario => codigo + acumulado



    }
}
