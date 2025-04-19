using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Resenia:IComparable
    {
        #region Atributos Resenia
        public Periodista Periodista { get; set; }
        public DateTime fecha { get; set; }
        public Partido Partido { get; set; }
        public int idPartido { get; set; }
        public string titulo { get; set; }
        public string contenido { get; set; }

        #endregion
        #region Metodos Resenia
        public Resenia(Periodista pPeriodista,DateTime pFecha,Partido pPartido, string pTitulo, string pContenido)
        {
            this.Periodista = pPeriodista;
            this.fecha = pFecha;
            this.Partido = pPartido;
            this.titulo = pTitulo;
            this.contenido = pContenido;
        }
        public Resenia() { }
        #endregion


        public int CompareTo(Object obj)
        {
            Resenia resenia = (Resenia)obj;
            return this.fecha.CompareTo(resenia.fecha);
        }
    }
}
