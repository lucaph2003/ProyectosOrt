using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Operador : Usuario
    {
        //ver si hereda de persona o no
        public DateTime FechaContratado { get; set; }

        public const string Rol = "Operador";

        public Operador(string pNombre, string pApellido, string pContrasena, string pEmail , DateTime pFechaContratado):base(pNombre, pApellido, pContrasena,pEmail)
        {
            this.FechaContratado = pFechaContratado;
        }

        public override void Validar()
        {
            if (this.nombre.Length > 1 && this.apellido.Length > 1 && this.email.Length > 1 && this.contrasena.Length > 1)
            {
                ValidarEmail();
                ValidarPassword();
            }
            else
            {
                throw new Exception("Todos los campos deben estar llenos");
            }

        }
        public void ExisteEmail(List<Usuario> usuarios)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.email.Equals(this.email))
                {
                    throw new Exception("El mail ya existe");
                }
            }

        }
        public override void ValidarEmail()
        {
            if (!this.email.Contains("@") || this.email.StartsWith("@") || this.email.EndsWith("@"))
            {
                throw new Exception("El email debe contener @ y no puede estar en el principio ni final");
            }
        }

        public override void ValidarPassword()
        {
            if (this.contrasena.Length < 7)
            {
                throw new Exception("La contrasenia debe ser mayor a 8 caracteres");
            }
        }

        public override string ObtenerRol()
        {
            return Rol;
        }

    }
}
