using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using System.Windows.Forms;

namespace Tp2Definitivo
{
    [Serializable]

    public abstract class Propiedad : IComparable
    {
        int nroIdentificador;
        string direccion;
        protected double precio;
        public Image[] fotos = new Image[4];

        string[] servicios;
        public int Nro { get { return nroIdentificador; } }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        public string Localidad { get; set; }
        public int Pasajeros { get; set; }
        public double PrecioBase {
            get { return precio; }
            set { precio = value; }
        }
        public string[] Servicios
        {
            get { return servicios; }
            set { servicios = value; }
        }

        private List<Reserva> reservas;
        public Propiedad(string direccion, int nro, string nombre, string localidad, string codigo)
        {
            this.nroIdentificador = nro;
            this.direccion = direccion;
            Nombre = nombre;
            Localidad = localidad;
            reservas = new List<Reserva>();
            Codigo = codigo;
        }
        public abstract double PrecioCalcular();

        public abstract double ObtenerPrecioBase();

        public abstract void CalcularCantidadPasajeros();

       
        public int CompareTo(object obj)
        {
            return this.Nro.CompareTo(((Propiedad)obj).Nro);
        }

        public DateTime[] ObtenerFechasOcupadas()
        {
            List<DateTime> fechasOcupadas = new List<DateTime>();

            foreach (Reserva reserva in reservas)
            {
                // Verifica si la reserva corresponde a esta propiedad
                if (reserva.NroPropiedad == this.Nro)
                {
                    DateTime fechaInicio = reserva.FechaIngreso;
                    DateTime fechaFin = reserva.FechaOutgreso;

                    // Agrega todas las fechas ocupadas entre fechaInicio y fechaFin
                    while (fechaInicio < fechaFin)
                    {
                        fechasOcupadas.Add(fechaInicio);
                        fechaInicio = fechaInicio.AddDays(1);
                    }
                }
            }

            return fechasOcupadas.ToArray();
        }

        public bool AgregarReserva(Reserva nueva)
        {
            bool exito;
            // Verificar si la nueva reserva coincide con fechas ocupadas
            DateTime[] fechasAReservar = nueva.ObtenerFechasOcupadas();
            DateTime[] fechaOcupada = ObtenerFechasOcupadas();

            bool disponible = ComprobarDisponibilidad(fechasAReservar, fechaOcupada);

            if (disponible)
            {
                reservas.Add(nueva);
                exito = true;
            }
            else
            {
                exito = false;
            }
            
            return exito;
        }

        private bool ComprobarDisponibilidad(DateTime[] fechasAReservar, DateTime[] fechaOcupada)
        {
            bool disponible = true;
            foreach (DateTime fecha in fechasAReservar)
            {
                if (fechaOcupada.Contains(fecha))
                {
                    // La fecha ya está ocupada, no se puede realizar la reserva
                    disponible = false;
                }
            }
            // No se encontraron coincidencias, la reserva se puede agregar
            return disponible;
        }

        public bool EliminarReserva(int nroReserva)
        {
            bool exito = false;
            Reserva reservaEncontrada = BuscarReserva(nroReserva);

            if (reservaEncontrada != null)
            {
                reservas.Remove(reservaEncontrada);
                exito = true;
            }

            return exito;
        }

        public Reserva BuscarReserva(int nroReserva)
        {
            Reserva encontrada = null;
            foreach(Reserva r in reservas)
            {
                if(r.Nro == nroReserva)
                {
                    encontrada = r;
                }
            }

            return encontrada;
        }

        public Reserva[] ObtenerReservas()
        {
            Reserva[] listaReserva = null;
                if (reservas.Count > 0)
                {
                    listaReserva = reservas.ToArray();
                }
            
            return listaReserva;
        }
    }
    
    
}
