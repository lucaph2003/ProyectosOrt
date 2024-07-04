package dominio.Clases;

public class Aeropuerto implements Comparable<Aeropuerto>{
    public String Codigo;
    public String Nombre;

    public Aeropuerto(String codigo, String nombre) {
        Codigo = codigo;
        Nombre = nombre;
    }

    public Aeropuerto() {
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
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        Aeropuerto that = (Aeropuerto) o;

        return Codigo.equals(that.Codigo);
    }

    @Override
    public int hashCode() {
        return Codigo.hashCode();
    }

    @Override
    public int compareTo(Aeropuerto o) {
        return this.Codigo.compareTo(o.getCodigo());
    }

    @Override
    public String toString() {
        return this.Codigo + ';' + this.Nombre ;
    }
}
