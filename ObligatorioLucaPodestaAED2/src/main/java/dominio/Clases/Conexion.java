package dominio.Clases;

import dominio.Estructuras.ArbolBinarioBusqueda;
import dominio.Estructuras.NodoAB;

import java.util.Objects;

public class Conexion {
    private String codigoAeropuertoOrigen;
    private String codigoAeropuertoDestino;
    private double kilometros;
    private ArbolBinarioBusqueda<Vuelo> ABBVuelos;

    public Conexion(String codigoAeropuertoOrigen, String codigoAeropuertoDestino, double kilometros,ArbolBinarioBusqueda<Vuelo> arbol) {
        this.codigoAeropuertoOrigen = codigoAeropuertoOrigen;
        this.codigoAeropuertoDestino = codigoAeropuertoDestino;
        this.kilometros = kilometros;
        this.ABBVuelos = arbol;
    }

    public Conexion() {
    }

    public boolean existeAerolinea(Aerolinea aerolinea) {
        if (ABBVuelos == null) {
            return false;
        }
        return buscarAerolineaEnArbol(ABBVuelos.getRaiz(), aerolinea);
    }

    private boolean buscarAerolineaEnArbol(NodoAB<Vuelo> nodo, Aerolinea aerolinea) {
        if (nodo == null) {
            return false;
        }

        if (nodo.getDato().getCodigoAerolinea().equals(aerolinea.getCodigo())) {
            return true;
        }
        if (buscarAerolineaEnArbol(nodo.getNodoIzq(), aerolinea)) {
            return true;
        }
        return buscarAerolineaEnArbol(nodo.getNodoDer(), aerolinea);
    }

    public double obtenerTiempoVueloMasRapido(String codigoAeropuertoOrigen, String codigoAeropuertoDestino) {
        if (ABBVuelos == null) {
            return -1;
        }
        return buscarTiempoVueloMasRapidoEnArbol(ABBVuelos.getRaiz(), codigoAeropuertoOrigen, codigoAeropuertoDestino);
    }

    private double buscarTiempoVueloMasRapidoEnArbol(NodoAB<Vuelo> nodo, String codigoAeropuertoOrigen, String codigoAeropuertoDestino) {
        if (nodo == null) {
            return -1;
        }

        if (nodo.getDato().getCodigoAeropuertoOrigen().equals(codigoAeropuertoOrigen) &&
                nodo.getDato().getCodigoAeropuertoDestino().equals(codigoAeropuertoDestino)) {
            return nodo.getDato().getMinutos();
        }

        double tiempoIzquierdo = buscarTiempoVueloMasRapidoEnArbol(nodo.getNodoIzq(), codigoAeropuertoOrigen, codigoAeropuertoDestino);
        double tiempoDerecho = buscarTiempoVueloMasRapidoEnArbol(nodo.getNodoDer(), codigoAeropuertoOrigen, codigoAeropuertoDestino);

        if (tiempoIzquierdo == -1) {
            return tiempoDerecho;
        } else if (tiempoDerecho == -1) {
            return tiempoIzquierdo;
        } else {
            return Math.min(tiempoIzquierdo, tiempoDerecho);
        }
    }


    public String getCodigoAeropuertoOrigen() {
        return codigoAeropuertoOrigen;
    }

    public void setCodigoAeropuertoOrigen(String codigoAeropuertoOrigen) {
        this.codigoAeropuertoOrigen = codigoAeropuertoOrigen;
    }

    public String getCodigoAeropuertoDestino() {
        return codigoAeropuertoDestino;
    }

    public void setCodigoAeropuertoDestino(String codigoAeropuertoDestino) {
        this.codigoAeropuertoDestino = codigoAeropuertoDestino;
    }

    public double getKilometros() {
        return kilometros;
    }

    public void setKilometros(double kilometros) {
        this.kilometros = kilometros;
    }

    public ArbolBinarioBusqueda<Vuelo> getABBVuelos() {
        return ABBVuelos;
    }

    public void setABBVuelos(ArbolBinarioBusqueda<Vuelo> ABBVuelos) {
        this.ABBVuelos = ABBVuelos;
    }


    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Conexion conexion = (Conexion) o;
        return Objects.equals(codigoAeropuertoOrigen, conexion.codigoAeropuertoOrigen) && Objects.equals(codigoAeropuertoDestino, conexion.codigoAeropuertoDestino);
    }

    @Override
    public int hashCode() {
        return Objects.hash(codigoAeropuertoOrigen, codigoAeropuertoDestino);
    }



}
