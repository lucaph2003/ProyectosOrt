window.addEventListener("load", setup);

//Declaracion del ultimo div para volver a atras
let lastDiv = "";

function setup () {
    //Cargar pantalla inicial
    showWindow("inicial");

    //Volver al menu con el titulo
    document.querySelector("#tituloInicial").addEventListener("click", function(){showWindow("inicial")});

    //Iniciar y cerrar sesion
    document.querySelector("#link_nav_Local").addEventListener("click", resetLogin);
    document.querySelector("#link_nav_Usuario").addEventListener("click",resetLogin);

    //Ventana Inicio Local
    document.querySelector("#btnInicioLocal").addEventListener("click", function(){showWindow("local_login")});
    //Ventana Inicio y registro Usuario
    document.querySelector("#btnInicioUsuario").addEventListener("click", function(){showWindow("usuario_login")});
    document.querySelector("#btnRegistroCliente").addEventListener("click", function(){showWindow("usuario_register")});

    ////LOCAL////
    //Ventana Inicio Sesion Local

    //Menu del local
    document.querySelector("#btnLocalDisponibilidad").addEventListener("click", function(){showWindow("local_disponibilidad"),showAvaiable(loggedUser.id),show("menu_nav__Local")});
    document.querySelector("#btnLocalAdministrarReservas").addEventListener("click", function(){showWindow("local_administrar_reservas"), show("menu_nav__Local"),showReservesPendientesLocal()});
    document.querySelector("#btnLocalEstadoReservas").addEventListener("click", function(){showWindow("local_estado_reservas"),show("menu_nav__Local"),showCuposLocal()});
    document.querySelector("#btnLocalEstadisticas").addEventListener("click", function(){showWindow("local_estadisticas"),show("menu_nav__Local"),showStatusLocal()});
    
    //Boton volver Local
    document.querySelector("#btnVolverLocal").addEventListener("click", function(){backLocal(lastDiv)});
   
    ////Usuario////
    //Ventana Inicio Sesion Usuario
    document.querySelector("#enlaceRegistro").addEventListener("click", function(){showWindow("usuario_register")});
    //Ventana Registro Usuario
    document.querySelector("#enlaceLogin").addEventListener("click", function(){showWindow("usuario_login")});
    //Menu del Usuario
    document.querySelector("#btnUsuarioEleccionLocal").addEventListener("click", function(){showWindow("usuario_eleccion_local"),show("menu_nav__Usuario"),lastDiv="menu_nav__Usuario"});
    document.querySelector("#btnUsuarioReserva").addEventListener("click", function(){showWindow("usuario_reserva"),showStatusUser(),show("menu_nav__Usuario"),lastDiv="menu_nav__Usuario"});
    document.querySelector("#btnUsuarioEstadistica").addEventListener("click", function(){showWindow("usuario_estadistica"),showReservesStatus(),show("menu_nav__Usuario"),lastDiv="menu_nav__Usuario"});
    document.querySelector("#btnUsuarioEstadistica").addEventListener("click", function(){showWindow("usuario_estadistica"),showReservesPorcent(),show("menu_nav__Usuario"),lastDiv="menu_nav__Usuario"});
    //Ventana Opciones de Reserva
    document.querySelector("#btnRestaurantes").addEventListener("click", function(){showWindow("usuario_eleccion_restaurantes"),show("menu_nav__Usuario"),lastDiv="menu_nav__Usuario"});
    document.querySelector("#btnMuseos").addEventListener("click", function(){showWindow("usuario_eleccion_museo"),show("menu_nav__Usuario"),lastDiv="menu_nav__Usuario"});
    document.querySelector("#btnTeatros").addEventListener("click", function(){showWindow("usuario_eleccion_teatro"),show("menu_nav__Usuario"),lastDiv="menu_nav__Usuario"});
    //Ventana Opciones Restaurantes
    document.querySelector("#btnMcdonal").addEventListener("click", function(){showLocal(1),show("menu_nav__Usuario"),lastDiv="usuario_eleccion_restaurantes"});
    document.querySelector("#btnLapasiva").addEventListener("click", function(){showLocal(2),show("menu_nav__Usuario"),lastDiv="usuario_eleccion_restaurantes"});
    document.querySelector("#btnSubway").addEventListener("click", function(){showLocal(3),show("menu_nav__Usuario"),lastDiv="usuario_eleccion_restaurantes"});
    //Ventana Opciones Museos
    document.querySelector("#btnMuseoArtesVisuales").addEventListener("click", function(){showLocal(4),show("menu_nav__Usuario"),lastDiv="usuario_eleccion_museo"});
    document.querySelector("#btnMuseoHistoriaNatural").addEventListener("click", function(){showLocal(5),show("menu_nav__Usuario"),lastDiv="usuario_eleccion_museo"});
    //Ventana Opciones Teatros
    document.querySelector("#btnTeatroSolis").addEventListener("click", function(){showLocal(6),show("menu_nav__Usuario"),lastDiv="usuario_eleccion_teatro"});
    document.querySelector("#btnTeatroLife").addEventListener("click", function(){showLocal(7),show("menu_nav__Usuario"),lastDiv="usuario_eleccion_teatro"});

    //Volver para la ventana anterior usuario
    document.querySelector("#btnVolverUsuario").addEventListener("click", function(){backUser(lastDiv)});
}

function hideAll() {
    document.querySelector("#inicial").style.display = "none";
    document.querySelector("#local_login").style.display = "none";
    document.querySelector("#usuario_login").style.display = "none";
    document.querySelector("#usuario_register").style.display = "none";
    document.querySelector("#menu_nav__Local").style.display = "none";
    document.querySelector("#local_disponibilidad").style.display = "none";
    document.querySelector("#local_estado_reservas").style.display = "none";
    document.querySelector("#local_administrar_reservas").style.display = "none";
    document.querySelector("#local_estadisticas").style.display = "none";
    document.querySelector("#menu_nav__Usuario").style.display = "none";
    document.querySelector("#usuario_eleccion_local").style.display = "none";
    document.querySelector("#usuario_eleccion_restaurantes").style.display = "none";
    document.querySelector("#usuario_eleccion_museo").style.display = "none";
    document.querySelector("#usuario_eleccion_teatro").style.display = "none";
    document.querySelector("#usuario_reserva").style.display = "none";
    document.querySelector("#usuario_estadistica").style.display = "none";
    document.querySelector("#local").style.display = "none";
}

function showWindow(div) {
    hideAll();
    show(div);
}

function show(div) {
    document.querySelector("#"+div).style.display = "block";
}

function showLocal(pIdObjeto) {
    document.querySelector("#cantidad_Cupos_Reservar").innerHTML = "";
    hideAll();
    idLocalAReservar = pIdObjeto;
    let local = localesRegistrados[pIdObjeto];

    document.querySelector("#local").style.display = "block";
    document.querySelector("#imgLogo").src = local.logo;
    document.querySelector("#titulo_local_seleccionado").innerHTML = local.nombre;
    document.querySelector("#informacion_local").innerHTML = local.descripcion;
    document.querySelector("#img_ubicacion_local").src = local.imgUbicacion;
    document.querySelector("#direccion_local").innerHTML = local.direccion;
    document.querySelector("#cupos_disponibles_reserva").innerHTML = local.CuposRestantes();
    
    for(let i=1;i<=local.CuposRestantes();i++) {
        document.querySelector("#cantidad_Cupos_Reservar").innerHTML += "<option value="+i+">"+i+"</option>";
    }
}

function backUser(lastDiv) {
    showWindow(lastDiv);
    show("menu_nav__Usuario");
}

function backLocal(lastDiv) {
    showWindow(lastDiv);
    show("menu_nav__Local");
}

function showAvaiable(idLocal) {
    document.querySelector("#ActualizacionHabilitado").innerHTML = "";
    let isAvaiable = localesRegistrados[idLocal].habilitado;
    if (isAvaiable) {
        document.querySelector("#radioHabilitado").checked = true;
    } else {
        document.querySelector("#radioDeshabilitado").checked = true ;
    }
}

function resetLogin() {
    document.querySelector("#link_nav_Local").innerHTML = "";
    document.querySelector("#link_nav_Usuario").innerHTML = "";
    document.querySelector("#txtUserLocal").value = "";
    document.querySelector("#txtPasswordLocal").value = "";
    loggedUser = '';
    showWindow("inicial");
}

function showCuposLocal() {
    document.querySelector("#cupos_totales").innerHTML = loggedUser.cuposMaximos;
    document.querySelector("#mostrarCuposDisponibles").innerHTML = loggedUser.CuposRestantes();
    
}

function showStatusUser() {
    showReservesFinalizadasUser();
    showReservesCanceladasUser();
    showReservesPendientesUser();
}

function showStatusLocal() {
    document.querySelector("#Porcentaje_ocupacion").innerHTML = loggedUser.PorcentajeOcupacion();
    document.querySelector("#Reservas_totales_pendientes").innerHTML = loggedUser.reservasPendientes;
    document.querySelector("#Reservas_totales_finalizadas").innerHTML = loggedUser.reservasFinalizadas;
    document.querySelector("#Reservas_totales_canceladas").innerHTML = loggedUser.reservasCanceladas;
    document.querySelector("#Calificacion_Local").innerHTML = loggedUser.promedio;
    rankingLocal();
}