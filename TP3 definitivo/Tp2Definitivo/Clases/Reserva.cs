using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp2Definitivo.Clases;

namespace Tp2Definitivo
{
    [Serializable]
    public class Reserva
    {     
        //datos de la reserva;
        DateTime reserva;
        DateTime fechaIngreso;
        DateTime fechaOutgreso;
        int nroDeReserva;
        private string nombrePropiedad;

        public int Nro 
        { 
            get 
            { 
                return nroDeReserva; 
            }
            set 
            { 
                nroDeReserva = value; 
            }
        }
        public int NroPropiedad { get{ return nroPropiedad; } }
        public DateTime FechaIngreso { get { return fechaIngreso; } set { fechaIngreso = value; } }
        public DateTime FechaOutgreso { get { return fechaOutgreso; } set { fechaOutgreso = value; } }
        public DateTime Reservacion { get { return reserva; } set { reserva = value; } }
        public int Dias 
        { 
            get 
            {
                TimeSpan tiempo = fechaOutgreso.Subtract(fechaIngreso);
                return tiempo.Days;
            } 
        }


        //datos de la propiedad;
        int nroPropiedad;
        string direccion;
        public string Direccion { get { return direccion; } }


        //datos del cliente
        int dni;
        string nombre;
        public string Nombre { get { return nombre; } }
        public int Dni { get { return dni; } }

        private string tipo;
        private int pasajeros;

        private Image fotoPropiedad;

        private double costoDia;
        private double costoTotal;
        private string servicios;
        private string propietario;

        public Reserva(Cliente unCliente,Propiedad unaPropiedad, DateTime reserva, DateTime fechaIngreso, DateTime fechaOutgreso, int nroReserva, string propietario)
        {
            //cliente;
            dni = unCliente.DNI;
            nombre = unCliente.Nombre;
            //propiedad;
            nroPropiedad = unaPropiedad.Nro;
            direccion = unaPropiedad.Direccion;
            this.tipo = unaPropiedad is Casa ? "Casa" : "Hotel";
            this.pasajeros = unaPropiedad.Pasajeros;
            //reserva;
            this.reserva = reserva;
            this.fechaIngreso = fechaIngreso;
            this.fechaOutgreso = fechaOutgreso;
            this.nroDeReserva = nroReserva;
            this.nombrePropiedad = unaPropiedad.Nombre;
            this.fotoPropiedad = unaPropiedad.fotos[0];
            this.costoDia = 0;
            this.costoTotal = 0;
            this.propietario = propietario;
            ActualizarServicios(unaPropiedad.Servicios);
        }

        public void ActualizarServicios(string[] servicios)
        {
            this.servicios = "";
            if (servicios.Length > 0)
            {
                this.servicios += servicios[0];
                for (int i = 1; i < servicios.Length; i++)
                {
                    this.servicios += ";" + servicios[i];
                }

            }
        }
        public string ObtenerServicios()
        {
            return servicios;
        }

        public DateTime[] ObtenerFechasOcupadas()
        {
            List<DateTime> fechasOcupadas = new List<DateTime>();

            DateTime fechaInicio = FechaIngreso;
            DateTime fechaFin = FechaOutgreso;

            while (fechaInicio < fechaFin)
            {
                fechasOcupadas.Add(fechaInicio);
                fechaInicio = fechaInicio.AddDays(1);
            }

            return fechasOcupadas.ToArray();
        }

        public string ObtenerTipoPropiedad()
        {
            return tipo;
        }
        public int ObtenerPasajeros()
        {
            return pasajeros;
        }

        public Image ObtenerFotoPropiedad()
        {
            return fotoPropiedad;
        }

        public void AsignarCostoPorDia(double costoPorDia)
        {
            this.costoDia = costoPorDia;
        }

        public void AsignarCostoTotal(double costoTotal)
        {
            this.costoTotal = costoTotal;
        }

        public double ObtenerCostoPorDia()
        {
            return costoDia;
        }

        public double ObtenerCostoTotal()
        {
            return costoTotal;
        }

        public string ObtenerNombrePropiedad()
        {
            return nombrePropiedad;
        }

        public string ObtenerPropietario()
        {
            return propietario;
        }
    }
}
