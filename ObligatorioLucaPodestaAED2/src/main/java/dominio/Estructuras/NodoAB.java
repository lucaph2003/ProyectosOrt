package dominio.Estructuras;

import dominio.Clases.Pasajero;

public class NodoAB<T extends Comparable<T>> {
    private T dato;
    private NodoAB<T> izq;
    private NodoAB<T> der;

    public NodoAB(T pDato){
        this.dato = pDato;
    }

    public NodoAB(T pDato,NodoAB<T> pNodoABizq,NodoAB<T> pNodoABder){
        this.dato = pDato;
        this.der = pNodoABder;
        this.izq = pNodoABizq;
    }

    public T getDato(){
        return this.dato;
    }

    public NodoAB<T> getNodoDer(){
        return this.der;
    }
    public NodoAB<T> getNodoIzq(){
        return this.izq;
    }
    public void setDato(T dato) {
        this.dato = dato;
    }
    public void setNodoDer(NodoAB<T> pNodo){
        this.der = pNodo;
    }
    public void setNodoIzq(NodoAB<T> pNodo){
        this.izq = pNodo;
    }
}
