using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Partido 
    {
        #region Atributos Partido
        public int id { get; set; }
        public Seleccion seleccion1 { get; set; }
        public Seleccion seleccion2 { get; set; }
        public DateTime fechaHora { get; set; }
        public bool esFinalizada { get; set; }
        public int resultado1 { get; set; }
        public int resultado2 { get; set; }
        public string resultadoFinal { get;  set; }
        public List<Incidencia> Incidencias { get; set; }
        public static int contador = 1;

        #endregion
        #region Metodos Partido

        #region Constructores
        public Partido(Seleccion pSeleccion1,Seleccion pSeleccion2,DateTime pFechaHora)
        {
            Incidencias = new List<Incidencia>();
            this.seleccion1 = pSeleccion1;
            this.seleccion2 = pSeleccion2;
            this.fechaHora = pFechaHora;
            this.esFinalizada = false;
            this.id = contador++;
            this.resultadoFinal = "Pendiente";
        }

        public Partido()
        {
            Incidencias = new List<Incidencia>();
            this.esFinalizada = false;
            this.id = contador++;
            this.resultadoFinal = "Pendiente";
        }

        #endregion

        #region Metodos
        public void AgregarIncidencia(Incidencia incidencia)
        {
            Incidencias.Add(incidencia);
        }

        //?
        public int CantidadIncidencia()
        {
            int cant = 0;
            for (int i = 0; i < Incidencias.Count; i++)
            {
                cant++;
            }
            return cant;
        }

        //?
        public string verIncidencias()
        {
            string incidencias = "";
            foreach (Incidencia i in Incidencias)
            {
                incidencias += i.ToString() + " \n";
            }
            return incidencias;
        }

        //Obtenemos los goles de la seleccion , en caso de no tener devuelve 0
        public virtual int obtenerGolesSeleccion(Seleccion seleccion)
        {
            int cantGoles = 0;
            foreach (Incidencia i in Incidencias)
            {
                if (i.jugador.pais.Equals(seleccion.pais) && i.incidencia == "Gol") { cantGoles++; }
            }
            return cantGoles;
        }

        public int ObtenerAmonestacionesSeleccion(Seleccion seleccion)
        {
            int cantAmarillas = 0;
            foreach(Incidencia i in Incidencias)
            {
                if(i.jugador.pais.Equals(seleccion.pais) && i.incidencia == "Amarilla") { cantAmarillas++; }
            }
            return cantAmarillas;
        }
        public int ObtenerExpulsionesSeleccion(Seleccion seleccion)
        {
            int cantRojas = 0;
            foreach (Incidencia i in Incidencias)
            {
                if (i.jugador.pais.Equals(seleccion.pais) && i.incidencia == "Roja") { cantRojas++; }
            }
            return cantRojas;
        }

        

        //Metodo para validar el resultado
        public virtual void ValidarResultado() { }

        //Metodo para finalizar el partido
        public virtual void finalizarPartido(){}
        #endregion

        #region Validaciones
        //Valida que las selecciones no sean vacias, ni que una seleccion se enfrente asi misma
        public void ValidarSelecciones()
        {
            if(this.seleccion1 == null || this.seleccion2 == null || this.seleccion1.Equals(this.seleccion2))
            {
                throw new Exception("Debe cargar correctamente las dos selecciones! ! !");
            }
        }

        //Valida que las fechas esten comprendidas en el tiempo estipulado 
        public void ValidarFecha()
        {
            DateTime fechaInicio = new DateTime(2022, 11, 20);
            DateTime fechaFinal = new DateTime(2022, 12, 18);
            if(this.fechaHora < fechaInicio || this.fechaHora > fechaFinal)
            {
                throw new Exception("Debe ingresar fecha entre el 20/11/2022 hasta 18/12/2022");
            }
        }

        #endregion
        #region Override y Compare
        public override bool Equals(object obj)
        {
            Partido p = (Partido)obj;
            return this.id == p.id;
        }
        public override string ToString()
        {
            return $"Fecha: {this.fechaHora}" + " " + $"{this.seleccion1.VerNombre()}" + " VS " + $"{this.seleccion2.VerNombre()}" + " " + "Cantidad Incidencias: " + $"{this.Incidencias.Count}"; 
        }

        public virtual string ObtenerTipo()
        {
            return "SIN_TIPO";
        }

        //ESTE METODO EN FASE ELIMINATORIA DEVUELVE LA ETAPA Y EN FASE DE GRUPO DEVUELVE EL GRUPO
        public virtual string ObtenerEtapa()
        {
            return "SIN_ETAPA";
        }
        #endregion

        #endregion
    }
}
