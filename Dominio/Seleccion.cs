using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Seleccion :IComparable
    {
        #region Atributos Seleccion
        public Pais pais { get; set; }
        public List<Jugador> Jugadores { get; set; }
        #endregion

        #region Metodos Seleccion

        #region Constructores
        public Seleccion(Pais pPais)
        {
            this.pais = pPais;
            Jugadores = new List<Jugador>();
        }
        #endregion

        #region Metodos
        public void AgregarJugador(Jugador pJugador)
        {
            Jugadores.Add(pJugador);
        }

        public string VerNombre()
        {
            return pais.nombre;
        }
        #endregion

        #region Validaciones
        public void ValidarPais()
        {
            if(this.pais == null)
            {
                throw new Exception("Ingresa un pais que no este vacio");
            }
        }

        public void ValidarJugadores()
        {
            if(Jugadores.Count < 11 )
            {
                throw new Exception("Debe ingresar al menos 11 jugadores");
            }
        }
        
        public int CompareTo(Object obj)
        {
            Seleccion seleccion = (Seleccion)obj;
            return this.pais.nombre.CompareTo(seleccion.pais.nombre);

        }
        #endregion

        public override bool Equals(object obj)
        {
            Seleccion s = (Seleccion)obj;
            return this.pais.Equals(s.pais);
        }

        public override string ToString()
        {
            return this.pais.nombre;
        }
        #endregion
    }
}
