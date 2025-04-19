using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class PartidoFaseDeGrupo : Partido
    {
        #region Atributos PartidoFaseDeGrupo
        public char grupo { get; set; }
        
        #endregion

        #region Metodos PartidoFaseDeGrupo
        public PartidoFaseDeGrupo(Seleccion pSeleccion1,Seleccion pSeleccion2,DateTime pFechaHora,char pGrupo) : base(pSeleccion1, pSeleccion2, pFechaHora)
        {
            this.grupo = pGrupo;
        }

        public override void ValidarResultado()
        {
            if (this.resultado1 > this.resultado2)
            {
                this.resultadoFinal = "Ganador: " + this.seleccion1.pais.nombre;
            }
            else if (this.resultado1 < this.resultado2)
            {
                this.resultadoFinal = "Ganador: " + this.seleccion2.pais.nombre;
            }
            else
            {
                this.resultadoFinal = "Empate";
            }
        }

        public override void finalizarPartido()
        {
            if (!esFinalizada) 
            {
                this.resultado1 = obtenerGolesSeleccion(this.seleccion1);
                this.resultado2 = obtenerGolesSeleccion(this.seleccion2);
                ValidarResultado();
                esFinalizada = true;
            }
            else
            {
                throw new Exception("El partido ya esta finalizado");
            }
        }

        public override string ObtenerTipo()
        {
            return "FaseGrupo";
        }

        public override string ObtenerEtapa()
        {
            return this.grupo.ToString();
        }

        #endregion
    }
}
