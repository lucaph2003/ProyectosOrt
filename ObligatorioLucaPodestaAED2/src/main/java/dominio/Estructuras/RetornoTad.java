package dominio.Estructuras;

public class RetornoTad <T> {

    private T valor;
    private int elementosRecorridos;

    public RetornoTad(T valor, int elementosRecorridos) {
        this.valor = valor;
        this.elementosRecorridos = elementosRecorridos;
    }

    public RetornoTad() {

    }

    public T getValor() {
        return valor;
    }

    public int getElementosRecorridos() {
        return elementosRecorridos;
    }
}
