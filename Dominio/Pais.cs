using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Pais : IValidable
    {
        #region Atributos Pais
        public int id { get; set; }
        public string nombre { get; set; }
        public string codigo { get; set; }
        public static int contador { get; set; } = 0;
        #endregion

        #region Metodos Pais

        #region Constructores
        public Pais(string pNombre, string pCodigo)
        {
            this.id = contador++;
            this.nombre = pNombre;
            this.codigo = pCodigo;
        }
        public Pais()
        {
            this.id = 0;
            this.nombre = "Sin Definir";
            this.codigo = "AAA";
        }
        #endregion

        #region Validaciones
        public void Validar()
        {
            try
            {
                ValidarPais();
                ValidarCodigo();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void ValidarPais()
        {
            if (this.nombre.Length == 0)
            {
                throw new Exception("El nombre del pais no puede estar vacio");
            }
        }

        public bool ValidarCodigo()
        {
            bool esCorrecto = false;
            if (this.codigo.Length == 3)
            {
                esCorrecto = true;
            }
            return esCorrecto;
        }
        #endregion

        public override bool Equals(object obj)
        {
            Pais pais = (Pais)obj;
            return this.id == pais.id;
        }
        #endregion
    }
}
