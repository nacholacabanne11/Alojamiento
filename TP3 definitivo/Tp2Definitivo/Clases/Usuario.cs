using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2Definitivo.Clases
{
    [Serializable]
    public class Usuario
    {

        public int Id { get; private set; }

        public string Pass { get; private set; }

        public int Tipo { get; private set; }

        public bool EsAdmin { get; private set; }
        public Usuario(int id, string pass, int tipo)
        {
            Id = id;
            Pass = pass;
            Tipo = tipo;
            AsignarRol();
        }

        private void AsignarRol()
        {
            switch (Tipo)
            {
                case 1:
                    EsAdmin = true;
                    break;
                default:
                    EsAdmin = false;
                    break;
            }
        }

        public bool CambiarPass(Usuario user, string pass)
        {
            bool exito = false;

            try
            {
                bool passNoVacia = pass != ""; //Evaluamos si la pass nueva está vacia
                bool passNueva = pass != Pass; //O si es igual a la anterior

                if (passNoVacia && passNueva) 
                {
                    user.Pass = pass;
                    exito = true;
                }
            }
            catch (Exception ex)
            {
                exito = false;
                throw;
            }

            return exito;
        }

        public bool ComprobarPass(string passUser)
        {
            bool exito = false;

            if(passUser == Pass)
            {
                exito = true;
            }

            return exito;
        }
    }
}
