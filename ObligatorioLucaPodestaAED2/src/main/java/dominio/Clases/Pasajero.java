package dominio.Clases;

import interfaz.Categoria;

import java.util.Objects;

public class Pasajero implements Comparable<Pasajero>{
    private String Cedula;
    private String Nombre;
    private String Telefono;
    private Categoria categoria;

    public Pasajero(String cedula, String nombre, String telefono, Categoria categoria) {
        Cedula = cedula;
        Nombre = nombre;
        Telefono = telefono;
        this.categoria = categoria;
    }

    public Pasajero() {
    }

    public String getCedula() {
        return Cedula;
    }

    public void setCedula(String cedula) {
        Cedula = cedula;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public String getTelefono() {
        return Telefono;
    }

    public void setTelefono(String telefono) {
        Telefono = telefono;
    }

    public Categoria getCategoria() {
        return categoria;
    }

    public void setCategoria(Categoria categoria) {
        this.categoria = categoria;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Pasajero aux = (Pasajero) o;
        return Cedula == aux.getCedula();
    }

    @Override
    public int compareTo(Pasajero o) {
        return this.Cedula.compareTo(o.getCedula());
    }

    @Override
    public String toString() {
        return Cedula + ";" + Nombre + ";" + Telefono + ";" + categoria.getTexto();
    }

    @Override
    public int hashCode() {
        return Objects.hash(Cedula);
    }
}
