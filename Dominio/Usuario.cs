using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Usuario : IValidable 
    {
        #region Atributos Usuario

        public string nombre { get; set; }
        public string apellido { get; set; }

        public string contrasena { get; set; }

        public string email { get; set; }

        #endregion

        #region Metodos Usuario
        #region Constructores
        public Usuario(string pNombre, string pApellido, string pContrasena,string pEmail )
        {
            this.nombre = pNombre;
            this.apellido = pApellido;
            this.contrasena = pContrasena;
            this.email = pEmail;
        }
        public Usuario()
        {
            this.nombre = "Sin Definir";
            this.apellido = "Unkown";
            this.contrasena = "No Existe";
            this.email = "000@000";
        }
        #endregion

        #region Validaciones
        public virtual void Validar() 
        {
            try
            {
                ValidarEmail();
                ValidarPassword();
            }catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual void ValidarEmail()
        {
            if (!this.email.Contains("@") || this.email.StartsWith("@") || this.email.EndsWith("@"))
            {
                throw new Exception("El email debe contener @ y no puede estar en el principio ni final");
            }
        }

        public virtual void ValidarPassword()
        {
            if (this.contrasena.Length < 7)
            {
                throw new Exception("La contrasenia debe ser mayor a 8 caracteres");
            }
        }
        public virtual string ObtenerRol()
        {
            return "SIN_ROL";
        }
       

        #endregion

        #endregion
    }
}
