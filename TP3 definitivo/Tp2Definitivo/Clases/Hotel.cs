using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Definitivo.Clases
{
    [Serializable]

    class Hotel : Propiedad
    {
        bool tresEstrellas;
        double precioBase;

        int numeroPiezas;
        public int Habitacion
        {
            get
            {
                return numeroPiezas;
            }
            set
            {
                numeroPiezas = value;
            }
        }
        public double PrecioBase
        {
            get
            {
                return precioBase;
            }
            set
            {
                precioBase = value;
            }
        }
        public bool TresEstrellas
        {
            get
            {
                return tresEstrellas;
            }
            set
            {
                tresEstrellas = value;
            }
        }


        public Hotel(string direccion, int numeroPiezas, int nroHabitacion, bool tresEstrellas, double precioBase, string nombre, string localidad, string codigo) : base(direccion, nroHabitacion,nombre,localidad, codigo)
        {
            this.numeroPiezas = numeroPiezas;
            this.tresEstrellas = tresEstrellas;
            this.precioBase = precioBase;
        }
        public override double PrecioCalcular()
        {
            switch (numeroPiezas)
            {
                case 1:
                    precio = precioBase;
                    break;
                case 2:
                    precio = precioBase * 0.8;
                    break;
                case 3:
                    precio = precioBase * 1.5;
                    break;
            }
            if (tresEstrellas)
            {
                precio += 0.4;
            }
            return precio;
        }
        public override string ToString()
        {
            return ("Direccion: " + Direccion + "Hotel, Habitacion Nro: " + base.Nro);
        }

        public override double ObtenerPrecioBase()
        {
            return precioBase;
        }

        public bool ObtenerEstrellas()
        {
            return tresEstrellas;
        }

        public int ObtenerPiezas()
        {
            return numeroPiezas;
        }

        public override void CalcularCantidadPasajeros()
        {
            Pasajeros = Habitacion;
        }
    }
}
