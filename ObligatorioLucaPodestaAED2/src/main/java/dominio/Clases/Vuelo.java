package dominio.Clases;

public class Vuelo implements Comparable<Vuelo> {
    public String codigoAeropuertoOrigen;
    public String codigoAeropuertoDestino;
    public String codigoDeVuelo;
    public double combustible;
    public double minutos;
    public double costoEnDolares;
    public String codigoAerolinea;

    public Vuelo(String codigoAeropuertoOrigen, String codigoAeropuertoDestino, String codigoDeVuelo, double combustible, double minutos, double costoEnDolares, String codigoAerolinea) {
        this.codigoAeropuertoOrigen = codigoAeropuertoOrigen;
        this.codigoAeropuertoDestino = codigoAeropuertoDestino;
        this.codigoDeVuelo = codigoDeVuelo;
        this.combustible = combustible;
        this.minutos = minutos;
        this.costoEnDolares = costoEnDolares;
        this.codigoAerolinea = codigoAerolinea;
    }

    public Vuelo() {
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

    public String getCodigoDeVuelo() {
        return codigoDeVuelo;
    }

    public void setCodigoDeVuelo(String codigoDeVuelo) {
        this.codigoDeVuelo = codigoDeVuelo;
    }

    public double getCombustible() {
        return combustible;
    }

    public void setCombustible(double combustible) {
        this.combustible = combustible;
    }

    public double getMinutos() {
        return minutos;
    }

    public void setMinutos(double minutos) {
        this.minutos = minutos;
    }

    public double getCostoEnDolares() {
        return costoEnDolares;
    }

    public void setCostoEnDolares(double costoEnDolares) {
        this.costoEnDolares = costoEnDolares;
    }

    public String getCodigoAerolinea() {
        return codigoAerolinea;
    }

    public void setCodigoAerolinea(String codigoAerolinea) {
        this.codigoAerolinea = codigoAerolinea;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        Vuelo vuelo = (Vuelo) o;

        if (!codigoAeropuertoOrigen.equals(vuelo.codigoAeropuertoOrigen)) return false;
        if (!codigoAeropuertoDestino.equals(vuelo.codigoAeropuertoDestino)) return false;
        return codigoDeVuelo.equals(vuelo.codigoDeVuelo);
    }

    @Override
    public int hashCode() {
        int result = codigoAeropuertoOrigen.hashCode();
        result = 31 * result + codigoAeropuertoDestino.hashCode();
        result = 31 * result + codigoDeVuelo.hashCode();
        return result;
    }
    @Override
    public int compareTo(Vuelo o) {
        return this.codigoDeVuelo.compareTo(o.getCodigoDeVuelo());
    }
}
