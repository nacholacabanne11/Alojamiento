using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Definitivo.Clases
{
    [Serializable]

    class Casa : Propiedad
    {
        double pBase;

        int cantCamas;
        public int CantCamas
        {
            get
            {
                return cantCamas;
            }
            set
            {
                cantCamas = value;
            }
        }

        public double PrecioBase
        {
            get
            {
                return pBase;

            }
            set
            {
                pBase = value;
            }
        }
        public Casa(string direccion, int cantCamas, int nroCasa, double pBase, string nombre, string localidad, string codigo) : base(direccion, nroCasa, nombre, localidad, codigo)
        {
            this.cantCamas = cantCamas;
            this.pBase = pBase;
        }
        public override double PrecioCalcular()
        {
            return (pBase * cantCamas);
        }
        public override string ToString()
        {
            return ("Direccion: " + Direccion + "Casa Nro: " + base.Nro);
        }

        public override double ObtenerPrecioBase()
        {
            return pBase;
        }

        public override void CalcularCantidadPasajeros()
        {
            Pasajeros = cantCamas; 
        }
    }
}
