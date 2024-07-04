package sistema;

import dominio.Estructuras.ArbolBinarioBusqueda;
import dominio.Estructuras.Grafo;
import dominio.Estructuras.RetornoTad;
import dominio.utils.TipoMedicionEnum;
import interfaz.*;
import dominio.Clases.*;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class ImplementacionSistema implements Sistema {

    protected int topeAeropuertos;
    protected int topeAerolineas;
    ArbolBinarioBusqueda<Aerolinea> ABBAerolineas;
    ArbolBinarioBusqueda<Aeropuerto> ABBAeropuertos;
    ArbolBinarioBusqueda<Pasajero> ABBPasajerosRegistrados;
    ArbolBinarioBusqueda<Pasajero> ABBPasajerosEstandar;
    ArbolBinarioBusqueda<Pasajero> ABBPasajerosPlatino;
    ArbolBinarioBusqueda<Pasajero> ABBPasajerosFrecuente;
    Grafo GAeropuertos;

    @Override
    public Retorno inicializarSistema(int maxAeropuertos, int maxAerolineas) {
        if(maxAeropuertos <= 5){
            topeAerolineas = 0;
            topeAeropuertos = 0;
            return Retorno.error1("La cantidad Maxima de Aeropuertos tiene que ser Mayor a 5");
        }
        if (maxAerolineas <= 3){
            topeAerolineas = 0;
            topeAeropuertos = 0;
            return  Retorno.error2("La cantidad de Aerolineas tiene que ser mayor a 3");
        }
        ABBAeropuertos = new ArbolBinarioBusqueda<Aeropuerto>();
        ABBPasajerosRegistrados = new ArbolBinarioBusqueda<Pasajero>();
        ABBAerolineas = new ArbolBinarioBusqueda<Aerolinea>();
        ABBPasajerosEstandar = new ArbolBinarioBusqueda<Pasajero>();
        ABBPasajerosFrecuente = new ArbolBinarioBusqueda<Pasajero>();
        ABBPasajerosPlatino = new ArbolBinarioBusqueda<Pasajero>();
        GAeropuertos = new Grafo(maxAeropuertos);


        topeAerolineas = maxAerolineas;
        topeAeropuertos = maxAerolineas;
        return Retorno.ok();
    }

    @Override
    public Retorno registrarPasajero(String cedula, String nombre, String telefono, Categoria categoria) {
        //Controlamos el error 1
        if ( cedula == null || cedula.equals("")  || nombre == null || telefono == null || nombre.equals("") || telefono.equals("") || categoria == null){
            return  Retorno.error1("Los campos de los parametrosa no deben ser nulos");
        }

        //Controlamos el Error 2
        String regexCedula = "^[1-9]\\d{0,2}(\\.\\d{3}){2}-\\d{1}$";
        Pattern pattern = Pattern.compile(regexCedula);
        Matcher matcher = pattern.matcher(cedula);
        if (!matcher.matches()) {
            return Retorno.error2("La cedula debe ser valida");
        }

        //Ya en este momento creamos el pasajero
        Pasajero pasajero = new Pasajero(cedula,nombre,telefono,categoria);

        //Controlameos el error 3
        if(ABBPasajerosRegistrados.ExisteRegistro(pasajero)){
            return Retorno.error3("El pasajero con esta cedula ya existe");
        }
        //Registramos
        ABBPasajerosRegistrados.Insertar(pasajero);
        if (pasajero.getCategoria() == Categoria.ESTANDAR){
            ABBPasajerosEstandar.Insertar(pasajero);
        }else if (pasajero.getCategoria() == Categoria.PLATINO){
            ABBPasajerosPlatino.Insertar(pasajero);
        }else if (pasajero.getCategoria() == Categoria.FRECUENTE){
            ABBPasajerosFrecuente.Insertar(pasajero);
        }
        return Retorno.ok();
    }

    @Override
    public Retorno buscarPasajero(String cedula) {
        if(cedula == null || cedula.equals("")){
            return  Retorno.error1("La cedula no puede ser vacia o nula");
        }

        String regexCedula = "^[1-9]\\d{0,2}(\\.\\d{3}){2}-\\d{1}$";
        Pattern pattern = Pattern.compile(regexCedula);
        Matcher matcher = pattern.matcher(cedula);
        if (!matcher.matches()) {
            return Retorno.error2("La cédula no tiene un formato válido");
        }

        Pasajero aux = new Pasajero();
        aux.setCedula(cedula);
        RetornoTad<Pasajero> pasajeroBuscado = ABBPasajerosRegistrados.BuscarDato(aux);
        if (pasajeroBuscado.getValor() == null) {
            return Retorno.error3("No existe un pasajero registrado con esa cédula");
        }

        return Retorno.ok(pasajeroBuscado.getElementosRecorridos(),pasajeroBuscado.getValor().toString());

    }

    @Override
    public Retorno listarPasajerosAscendente() {
        String lista = ABBPasajerosRegistrados.listarAscendentes();
        return Retorno.ok(lista);
    }

    @Override
    public Retorno listarPasajerosPorCategoria(Categoria categoria) {
        String lista = "";
        if(categoria == null){
            return Retorno.error1("La categoria no debe ser nula");
        }
        if(categoria == Categoria.ESTANDAR){
            lista = ABBPasajerosEstandar.listarAscendentes();
        }else if (categoria == Categoria.PLATINO){
            lista=ABBPasajerosPlatino.listarAscendentes();
        }else if (categoria == Categoria.FRECUENTE){
            lista=ABBPasajerosFrecuente.listarAscendentes();
        }
        return Retorno.ok(lista);
    }

    @Override
    public Retorno registrarAerolinea(String codigo, String nombre) {
        if(ABBAerolineas.contarElementos() >= topeAerolineas){
            return Retorno.error1("Ya esta el tope de Aerolineas registrado");
        }
        if(codigo == null || codigo.equals("") || nombre == null || nombre.equals("")){
            return Retorno.error2("Los campos de los parametros no pueden ser vacios o nulos");
        }

        Aerolinea aerolinea = new Aerolinea(codigo,nombre);
        if(ABBAerolineas.ExisteRegistro(aerolinea)){
            return Retorno.error3("Ya existe esta Aerolinea");
        }
        ABBAerolineas.Insertar(aerolinea);
        return Retorno.ok();
    }

    @Override
    public Retorno listarAerolineasDescendente() {
        String lista = ABBAerolineas.listarDescendente();
        return Retorno.ok(lista);
    }

    @Override
    public Retorno registrarAeropuerto(String codigo, String nombre) {
        if(GAeropuertos.getCantidad() >= this.topeAeropuertos){
            return Retorno.error1("Ya estan registrados los maximos de aeropuertos");
        }
        if(codigo == null || codigo.equals("") || nombre == null || nombre.equals("")){
            return Retorno.error2("Los campos de los parametros no pueden ser vacios o nulos");
        }
        Aeropuerto aeropuerto = new Aeropuerto(codigo,nombre);
        if(GAeropuertos.existeAeropuerto(aeropuerto)){
            return Retorno.error3("Ya existe un areopuerto con el codigo "+codigo);
        }

        GAeropuertos.agregarAeropuerto(aeropuerto);
        return Retorno.ok();
    }

    @Override
    public Retorno registrarConexion(String codigoAeropuertoOrigen, String codigoAeropuertoDestino, double kilometros) {
        if(kilometros <=0){
            return  Retorno.error1("Los kilometros tiene que ser mayor a 0");
        }

        if(codigoAeropuertoDestino == null || codigoAeropuertoDestino.equals("") || codigoAeropuertoOrigen == null || codigoAeropuertoOrigen.equals("") ){
            return Retorno.error2("Los campos de los parametros no deben ser nulos o vacios");
        }

        Aeropuerto aeroOrigen = new Aeropuerto();
        aeroOrigen.setCodigo(codigoAeropuertoOrigen);
        if(!GAeropuertos.existeAeropuerto(aeroOrigen)){
            return Retorno.error3("El Aeropuerto Origen no existe con codigo: " + codigoAeropuertoOrigen);
        }

        Aeropuerto aeroDestino = new Aeropuerto();
        aeroDestino.setCodigo(codigoAeropuertoDestino);
        if(!GAeropuertos.existeAeropuerto(aeroDestino)){
            return Retorno.error4("El Aeropuerto Destino no existe con codigo: " + codigoAeropuertoDestino);
        }

        if(GAeropuertos.obtenerConexion(aeroOrigen,aeroDestino) != null){
            return Retorno.error5("Ya existe la conexion");
        }

        GAeropuertos.agregarConexion(aeroOrigen,aeroDestino,kilometros,new ArbolBinarioBusqueda<>());

        return Retorno.ok();
    }

    @Override
    public Retorno registrarVuelo(String codigoCiudadOrigen, String codigoAeropuertoDestino, String codigoDeVuelo, double combustible, double minutos, double costoEnDolares, String codigoAerolinea) {
        if (combustible <= 0 || minutos <= 0 || costoEnDolares <= 0){
            return Retorno.error1("Los Parametros deben ser mayor  a 0");
        }
        if(codigoCiudadOrigen == null || codigoCiudadOrigen.equals("") || codigoAeropuertoDestino == null || codigoAeropuertoDestino.equals("") || codigoDeVuelo == null || codigoDeVuelo.equals("") ||codigoAerolinea == null || codigoAerolinea.equals("") ){
            return Retorno.error2("Los parametros  no pueden ser vacios o nulos");
        }

        Aeropuerto aeroOrigen = new Aeropuerto();
        aeroOrigen.setCodigo(codigoCiudadOrigen);
        if(!GAeropuertos.existeAeropuerto(aeroOrigen)){
            return Retorno.error3("El Aeropuerto Origen no existe con codigo: " + codigoCiudadOrigen);
        }

        Aeropuerto aeroDestino = new Aeropuerto();
        aeroDestino.setCodigo(codigoAeropuertoDestino);
        if(!GAeropuertos.existeAeropuerto(aeroDestino)){
            return Retorno.error4("El Aeropuerto Destino no existe con codigo: " + codigoAeropuertoDestino);
        }

        Aerolinea aerolinea = new Aerolinea();
        aerolinea.setCodigo(codigoAerolinea);
        if(!ABBAerolineas.ExisteRegistro(aerolinea)){
            return Retorno.error5("La aerolinea con ese codigo no existe");
        }
        Conexion conexion = GAeropuertos.obtenerConexion(aeroOrigen,aeroDestino);
        if( conexion == null){
            return Retorno.error6("No existe una conexion");
        }
        Vuelo vuelo = new Vuelo(codigoCiudadOrigen,codigoAeropuertoDestino,codigoDeVuelo,combustible,minutos,costoEnDolares,codigoAerolinea);
        if(conexion.getABBVuelos().ExisteRegistro(vuelo)){
            return Retorno.error7("Ya existe un vuelo con este codigo entre esta conexion de aeropuertos.");
        }
        conexion.getABBVuelos().Insertar(vuelo);
        GAeropuertos.borrarConexion(aeroOrigen,aeroDestino);
        GAeropuertos.agregarConexion(aeroOrigen,aeroDestino,conexion.getKilometros(),conexion.getABBVuelos());
        return Retorno.ok();
    }

    @Override
    public Retorno listadoAeropuertosCantDeEscalas(String codigoAeropuertoOrigen, int cantidad, String codigoAerolinea) {
        String lista = "";
        if(cantidad < 0 ){
            return Retorno.error1("La cantidad debe ser mayor a cero");
        }

        Aeropuerto aeroOrigen = new Aeropuerto();
        aeroOrigen.setCodigo(codigoAeropuertoOrigen);
        if(!GAeropuertos.existeAeropuerto(aeroOrigen)){
            return Retorno.error2("El Aeropuerto Origen no existe con codigo: " + codigoAeropuertoOrigen);
        }

        Aerolinea aerolinea = new Aerolinea();
        aerolinea.setCodigo(codigoAerolinea);
        if(!ABBAerolineas.ExisteRegistro(aerolinea)){
            return Retorno.error3("La aerolinea con ese codigo no existe");
        }
        lista = GAeropuertos.bfs(aeroOrigen,cantidad,aerolinea);

        return Retorno.ok(lista);
    }

    @Override
    public Retorno viajeCostoMinimoKilometros(String codigoCiudadOrigen, String codigoCiudadDestino) {
        if(codigoCiudadOrigen == null || codigoCiudadOrigen.equals("") || codigoCiudadDestino == null || codigoCiudadDestino.equals("")  ){
            return  Retorno.error1("Los parametros de entrada no pueden ser vacios");
        }

        Aeropuerto aeroOrigen = new Aeropuerto();
        aeroOrigen.setCodigo(codigoCiudadOrigen);
        Aeropuerto aeroDestino = new Aeropuerto();
        aeroDestino.setCodigo(codigoCiudadDestino);

        if(!GAeropuertos.existeAeropuerto(aeroOrigen)){
            return Retorno.error3("El Aeropuerto Origen no existe con codigo: " + codigoCiudadOrigen);
        }

        if(!GAeropuertos.existeAeropuerto(aeroDestino)){
            return Retorno.error4("El Aeropuerto Destino no existe con codigo: " + codigoCiudadDestino);
        }

        RetornoTad<String> result = GAeropuertos.dijkstra(aeroOrigen,aeroDestino,TipoMedicionEnum.TipoMedicion.KILOMETROS);
        if(result.getElementosRecorridos()== Integer.MAX_VALUE) {
            return Retorno.error2("No existe una conexion entre los aeropuertos");
        }
        return Retorno.ok(result.getElementosRecorridos(),result.getValor());
    }

    @Override
    public Retorno viajeCostoMinimoEnMinutos(String codigoAeropuertoOrigen, String codigoAeropuertoDestino) {
        if(codigoAeropuertoOrigen == null || codigoAeropuertoOrigen.equals("") || codigoAeropuertoDestino == null || codigoAeropuertoDestino.equals("") ){
            return  Retorno.error1("Los parametros de entrada no pueden ser vacios");
        }

        Aeropuerto aeroOrigen = new Aeropuerto();
        aeroOrigen.setCodigo(codigoAeropuertoOrigen);
        Aeropuerto aeroDestino = new Aeropuerto();
        aeroDestino.setCodigo(codigoAeropuertoDestino);


        if(!GAeropuertos.existeAeropuerto(aeroOrigen)){
            return Retorno.error3("El Aeropuerto Origen no existe con codigo: " + codigoAeropuertoOrigen);
        }

        if(!GAeropuertos.existeAeropuerto(aeroDestino)){
            return Retorno.error4("El Aeropuerto Destino no existe con codigo: " + codigoAeropuertoDestino);
        }

        RetornoTad<String> result = GAeropuertos.dijkstra(aeroOrigen,aeroDestino, TipoMedicionEnum.TipoMedicion.MINUTOS);
        if(result.getElementosRecorridos()== Integer.MAX_VALUE) {
            return Retorno.error2("No existe una conexion entre los aeropuertos");
        }
        return Retorno.ok(result.getElementosRecorridos(),result.getValor());
    }


}
