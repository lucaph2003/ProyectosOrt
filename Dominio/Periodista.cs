using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Periodista : Usuario, IComparable
    {
        #region Atributos Periodista

        public static int Contador = 1;

        public int id { get; set; }

        public const string Rol = "Periodista";
        #endregion

        #region Metodos Periodista

        #region Constructores
        public Periodista(string pNombre, string pApellido, string pContrasena,string pEmail) :base(pNombre, pApellido, pContrasena,pEmail)
        {
            this.id = Contador++;
        }
        public Periodista() {
            this.id = Contador++;
        }
        #endregion

        #region Validaciones

       

        #endregion

        #region Override y Compare
        public override string ToString()
        {
            return $"Nombre: {this.nombre}" + "\n" + $"Correo Electronico: {this.email}";
        }
        public override string ObtenerRol()
        {
            return Rol;
        }
        public int CompareTo(Object obj)
        {
            Periodista periodista = (Periodista)obj;
            int ordenado = this.apellido.CompareTo(periodista.apellido);
            if(ordenado == 0)
            {
                ordenado = this.nombre.CompareTo(periodista.nombre);
            }
            return ordenado;
        }

        #endregion
        #endregion
    }
}
