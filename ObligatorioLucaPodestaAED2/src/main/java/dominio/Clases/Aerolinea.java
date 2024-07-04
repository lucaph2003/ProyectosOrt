package dominio.Clases;

public class Aerolinea implements Comparable<Aerolinea>{
    private String Codigo;
    private String Nombre;

    public Aerolinea(String codigo, String nombre) {
        Codigo = codigo;
        Nombre = nombre;
    }

    public Aerolinea() {
    }

    public String getCodigo() {
        return Codigo;
    }

    public void setCodigo(String codigo) {
        Codigo = codigo;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    @Override
    public int compareTo(Aerolinea o) {
        return this.Codigo.compareTo(o.getCodigo());
    }

    @Override
    public String toString() {
        return this.Codigo + ";" + this.Nombre ;
    }


}
