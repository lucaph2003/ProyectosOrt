package dominio.Estructuras;

public class ArbolBinarioBusqueda<T extends Comparable<T>> {
    private NodoAB<T> raiz;

    public NodoAB<T> getRaiz(){
        return this.raiz;
    }

    public void Insertar(T dato) {
        raiz = Insertar(dato, raiz);
    }

    private NodoAB<T> Insertar(T dato, NodoAB<T> nodo) {
        if (nodo == null) {
            return new NodoAB<>(dato);
        }

        if (dato.compareTo(nodo.getDato()) < 0) {
            nodo.setNodoIzq(Insertar(dato, nodo.getNodoIzq()));
        } else if (dato.compareTo(nodo.getDato()) > 0) {
            nodo.setNodoDer(Insertar(dato, nodo.getNodoDer()));
        }

        return nodo;
    }

    public boolean ExisteRegistro(T dato){
        return ExisteRegistro(raiz, dato);
    }

    private boolean ExisteRegistro(NodoAB<T> nodo, T dato) {
        if (nodo != null) {
            if (nodo.getDato().compareTo(dato) == 0) {
                return true;
            } else if (dato.compareTo(nodo.getDato()) < 0) {
                return ExisteRegistro(nodo.getNodoIzq(), dato);
            } else {
                return ExisteRegistro(nodo.getNodoDer(), dato);
            }
        } else {
            return false;
        }
    }

    public RetornoTad<T> BuscarDato(T dato) {
        return BuscarDato(raiz, dato,0);
    }

    private RetornoTad<T> BuscarDato(NodoAB<T> nodo, T dato,int elementosRecorridos) {
        if (nodo != null) {
            T valor = nodo.getDato();
            if (valor.compareTo(dato) == 0) {
                return new RetornoTad<T>(valor, elementosRecorridos);
            } else if (valor.compareTo(dato) > 0) {
                return BuscarDato(nodo.getNodoIzq(), dato,++elementosRecorridos);
            } else {
                return BuscarDato(nodo.getNodoDer(), dato,++elementosRecorridos);
            }
        }
        return new RetornoTad<T>(null, elementosRecorridos);
    }


    public String listarAscendentes() {
        return listarAscendentes(this.raiz);
    }

    private String listarAscendentes(NodoAB<T> nodo) {
        if (nodo != null) {
            if(nodo.getNodoIzq() == null && nodo.getNodoDer() == null){
                return nodo.getDato().toString();
            }
            if(nodo.getNodoIzq() == null){
                return nodo.getDato() + "|" + listarAscendentes(nodo.getNodoDer());
            }
            if(nodo.getNodoDer() == null){
                return listarAscendentes(nodo.getNodoIzq()) + "|" + nodo.getDato();
            }
            return listarAscendentes(nodo.getNodoIzq()) + "|" + nodo.getDato() + "|" + listarAscendentes(nodo.getNodoDer());
        } else {
            return "";
        }
    }

    public int contarElementos() {
        return contarElementos(this.raiz);
    }

    private int contarElementos(NodoAB<T> nodo) {
        if (nodo == null) {
            return 0;
        } else {
            int contIzq = contarElementos(nodo.getNodoIzq());
            int contDer = contarElementos(nodo.getNodoDer());
            return contIzq + contDer + 1;
        }
    }


    public String listarDescendente() {
        return listarDescendente(this.raiz);
    }

    private String listarDescendente(NodoAB<T> nodo) {
        if (nodo != null) {
            if(nodo.getNodoDer() == null && nodo.getNodoIzq() == null){
                return nodo.getDato().toString();
            }
            if(nodo.getNodoDer() == null){
                return nodo.getDato() + "|" + listarDescendente(nodo.getNodoIzq());
            }
            if(nodo.getNodoIzq() == null){
                return listarDescendente(nodo.getNodoDer()) + "|" + nodo.getDato();
            }
            return listarDescendente(nodo.getNodoDer()) + "|" + nodo.getDato() + "|" + listarDescendente(nodo.getNodoIzq());
        } else {
            return "";
        }
    }


}

