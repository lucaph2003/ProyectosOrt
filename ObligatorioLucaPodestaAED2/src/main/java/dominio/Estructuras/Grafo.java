package dominio.Estructuras;

import dominio.Clases.Aerolinea;
import dominio.Clases.Aeropuerto;
import dominio.Clases.Conexion;
import dominio.Clases.Vuelo;
import dominio.utils.TipoMedicionEnum;


public class Grafo {
    private Aeropuerto[] Aeropuertos;
    private Conexion[][] Conexions;
    private final int maxAeropuertos;
    private int cantidad ;

    public Grafo(int maxAeropuertos) {
        this.maxAeropuertos = maxAeropuertos;
        Aeropuertos = new Aeropuerto[maxAeropuertos];
        Conexions = new Conexion[maxAeropuertos][maxAeropuertos];
        this.cantidad = 0;
    }

    public void agregarAeropuerto(Aeropuerto a) {
        if (cantidad < maxAeropuertos) {
            int posLibre = obtenerPosLibre();
            Aeropuertos[posLibre] = a;
            cantidad++;
        }
    }

    public void agregarConexion(Aeropuerto aInicial, Aeropuerto aFinal, double kilometros,ArbolBinarioBusqueda<Vuelo> vuelos) {
        int posVInicial = buscarPos(aInicial);
        int posVFinal = buscarPos(aFinal);

        Conexions[posVInicial][posVFinal] = new Conexion(aInicial.getCodigo(),aFinal.getCodigo(),kilometros,vuelos);
    }

    public void borrarConexion(Aeropuerto aInicial, Aeropuerto aFinal) {
        int posVInicial = buscarPos(aInicial);
        int posVFinal = buscarPos(aFinal);

        Conexions[posVInicial][posVFinal] = null;
    }

    public Conexion obtenerConexion(Aeropuerto aInicial, Aeropuerto aFinal) {
        int posVInicial = buscarPos(aInicial);
        int posVFinal = buscarPos(aFinal);

        return Conexions[posVInicial][posVFinal];
    }

    public boolean existeAeropuerto(Aeropuerto aeropuerto){
        int posABuscar= buscarPos(aeropuerto);
        return posABuscar>=0;
    }


    private int buscarPos(Aeropuerto v) {
        for (int i = 0; i < Aeropuertos.length; i++) {
            if (Aeropuertos[i] != null && Aeropuertos[i].equals(v)) {
                return i;
            }
        }
        return -1;
    }

    private int obtenerPosLibre() {
        for (int i = 0; i < Aeropuertos.length; i++) {
            if (Aeropuertos[i] == null) {
                return i;
            }
        }
        return -1;
    }

    public int getCantidad(){
        return this.cantidad;
    }


    //Algoritmos de busqueda


    public String bfs(Aeropuerto aeropuerto, int cantidad,Aerolinea aerolinea) {
        String lista = "";
        int posV = buscarPos(aeropuerto);
        boolean[] visitados = new boolean[this.maxAeropuertos];
        Cola<Integer> cola = new Cola<>();
        cola.encolar(posV);
        visitados[posV] = true;
        ArbolBinarioBusqueda<Aeropuerto> arbolAeropuertos = new ArbolBinarioBusqueda<>();
        int escalas = 0;
        while (!cola.esVacia() && escalas <= cantidad) {
            int pos = cola.desencolar();
            arbolAeropuertos.Insertar(Aeropuertos[pos]);
            for (int i = 0; i < Conexions.length; i++) {
                if (Conexions[pos][i] != null && !visitados[i] && escalas < cantidad && Conexions[pos][i].existeAerolinea(aerolinea)) {
                    cola.encolar(i);
                    visitados[i] = true;
                }
            }
            if (!cola.esVacia() && escalas < cantidad) {
                lista += "|";
            }
            escalas++;
        }
        lista = arbolAeropuertos.listarAscendentes();
        return lista;
    }
    public RetornoTad dijkstra(Aeropuerto aeropuertoOrigen, Aeropuerto aeroDestino, TipoMedicionEnum.TipoMedicion TipoMedicion) {
        int posVOrigen = buscarPos(aeropuertoOrigen);
        int posVDestino = buscarPos(aeroDestino);

        boolean[] visitados = new boolean[this.maxAeropuertos];
        int[] costos = new int[this.maxAeropuertos];
        int[] vengo = new int[this.maxAeropuertos];

        for (int i = 0; i < this.maxAeropuertos; i++) {
            costos[i] = Integer.MAX_VALUE;
            vengo[i] = -1;
            visitados[i] = false;
        }

        costos[posVOrigen] = 0;

        for (int v = 0; v < this.maxAeropuertos; v++) {
            int pos = obtenerSiguenteVerticeNoVisitadoDeMenorCosto(costos, visitados);

            if (pos != -1) {
                visitados[pos] = true;

                for (int i = 0; i < Conexions.length; i++) {
                    if (Conexions[pos][i] != null && !visitados[i]) {
                        int unidadmedida;
                        if(TipoMedicion== TipoMedicionEnum.TipoMedicion.KILOMETROS){
                            unidadmedida = costos[pos] + (int) Conexions[pos][i].getKilometros();
                        }else{
                            unidadmedida = costos[pos] + (int) Conexions[pos][i].obtenerTiempoVueloMasRapido(Conexions[pos][i].getCodigoAeropuertoOrigen(),Conexions[pos][i].getCodigoAeropuertoDestino());
                        }
                        if (unidadmedida < costos[i]) {
                            costos[i] = unidadmedida;
                            vengo[i] = pos;
                        }
                    }
                }
            }
        }

        StringBuilder camino = new StringBuilder();
        int posActual = posVDestino;
        camino.append(Aeropuertos[posActual].toString());

        while (posActual != posVOrigen && vengo[posActual] != -1) {
            posActual = vengo[posActual];
            camino.insert(0, "|").insert(0, Aeropuertos[posActual].toString());
        }

        RetornoTad<String> result = new RetornoTad<>(camino.toString(),costos[posVDestino]);
        return result;
    }


    private int obtenerSiguenteVerticeNoVisitadoDeMenorCosto(int[] costos, boolean[] visitados) {
        int posMin = -1;
        int min=Integer.MAX_VALUE;
        for (int i = 0; i < this.maxAeropuertos; i++) {
            if(!visitados[i] && costos[i]<min){
                min=costos[i];
                posMin=i;
            }
        }
        return posMin;
    }

}
