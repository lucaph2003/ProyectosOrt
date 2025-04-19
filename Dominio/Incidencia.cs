using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Incidencia
    {
        #region Atributos
        public string incidencia { get; set; }

        //Los goles de penales son representados en el minuto 121
        public int minuto { get; set; } 
        public Jugador jugador { get; set; }
        public Seleccion seleccion { get; set; }
        #endregion

        #region Metodos Incidencia

        #region Constructores
        public Incidencia(string pIncidencia,int pMinuto,Jugador pJugador )
        {
            this.incidencia = pIncidencia;
            this.minuto = pMinuto;
            this.jugador = pJugador;
        }
        public Incidencia()
        {
            this.incidencia = "Sin definir";
            this.minuto = 0;
            this.jugador = null;
        }
        #endregion

        #region Override y Compare
        public override string ToString()
        {
            return $"Incidencia: {incidencia}" + "\n" + $"Minuto: {minuto}" + "\n" + $"Nombre: {jugador.nombreCompleto}";
        }
        #endregion

        #endregion
    }
}
