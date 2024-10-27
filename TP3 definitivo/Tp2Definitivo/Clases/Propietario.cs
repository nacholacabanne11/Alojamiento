using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Definitivo.Clases
{
    [Serializable]
    public class Propietario
    {
        private string nombre;
        private string codigo;

        public string Nombre
        {
            get { return nombre; }
            
        }
        public double Recaudacion { get; set; }
        public List<Reserva> reservas { get; set; }
        public List<Propiedad> propiedades { get; set; }

        public Propietario(string nombre, string codigo)
        {
            this.nombre = nombre;
            this.codigo = codigo;
            reservas = new List<Reserva>();
            propiedades = new List<Propiedad>();
            Recaudacion = 0;
        }

        public string ObtenerCodigo()
        {
            return codigo;
        }

        public void AgregarReserva(Reserva reserva)
        {
            reservas.Add(reserva);
            Recaudacion += reserva.ObtenerCostoTotal();
        }
        public void AgregarPropiedad(Propiedad propiedad)
        {
            propiedades.Add(propiedad);
        }

        public double ObtenerTotal()
        {
            double total = 0;
            if (reservas.Count>0)
            {
                foreach (Reserva r in reservas)
                {
                    total += r.ObtenerCostoTotal();
                }
            }
            return total;
        }
        public string ObtenerPropiedades()
        {
            string props = "";
            if (propiedades.Count > 0)
            {
                for (int i = 0; i < propiedades.Count -1; i++)
                {
                    props += propiedades[i].ToString() + ";";
                }
                props += propiedades[propiedades.Count - 1].Nro;
            }
            return props;
        }

        public override string ToString()
        {
            //return nombre + ";" + codigo + ";" + ObtenerPropiedades() + ";" + ObtenerTotal().ToString(); 
            return nombre + ";" + codigo + ";" + ObtenerPropiedades() + ";" + Recaudacion;
        }
    }
}
