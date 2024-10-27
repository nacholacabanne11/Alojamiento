using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tp2Definitivo
{
    using System;

    [Serializable]
    public class Cliente
    {
        int dni;
        string nombre;
        DateTime fechaNacimiento;

        public int DNI
        {
            get { return dni; }
            set
            {
                if (value < 0)
                {
                    throw new DNIInvalidoException();
                }
                dni = value;
            }
        }
        public string Nombre { get { return nombre; } }
        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new FechaNacimientoFuturoException();
                }
                if (CalcularEdad(value) < 18)
                {
                    throw new MenorDeEdadException();
                }
                fechaNacimiento = value;
            }
        }

        public Cliente(int dni, string nombre, DateTime fechaNacimiento)
        {
            this.DNI = dni;
            this.nombre = nombre;
            this.FechaNacimiento = fechaNacimiento;
        }

        public int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime hoy = DateTime.Now;
            int edad = hoy.Year - fechaNacimiento.Year;
            if (hoy < fechaNacimiento.AddYears(edad))
            {
                edad--;
            }
            return edad;
        }

        public override string ToString()
        {
            return string.Format("Cliente: {0} / Dni: {1} / Edad: {2}", nombre, dni, CalcularEdad(FechaNacimiento));
        }
    }

    public class FechaNacimientoFuturoException : Exception
    {
        public FechaNacimientoFuturoException() : base("La fecha de nacimiento no puede ser en el futuro.")
        {
        }
    }

    public class MenorDeEdadException : Exception
    {
        public MenorDeEdadException() : base("El cliente no puede ser menor de 18 años.")
        {
        }
    }

    public class DNIInvalidoException : Exception
    {
        public DNIInvalidoException() : base("El DNI no puede ser un número negativo.")
        {
        }
    }

}
