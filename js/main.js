window.addEventListener("load", setup);

//Declaracion de Arrays
let usuariosRegistrados = [];
let localesRegistrados = [];

//Declaracion de variables
let idLocalAReservar = 0;
let idReserva = 1;
let loggedUser = '';

function setup() {
    preLoadData();
   
    //Botones de inicio de sesion y loggeos
    document.querySelector("#btnIniciarSesionLocal").addEventListener("click",loginLocal);
    document.querySelector("#btnIniciarSesionUsuario").addEventListener("click",loginUsuario);
    document.querySelector("#btnRegistroSesion").addEventListener("click",registerUsuario);

    //Botones del local
    document.querySelector("#btnAcutalizarEstado").addEventListener("click",changeAvaiable);
    document.querySelector("#btnAgregarCupos").addEventListener("click", addCupos);
    document.querySelector("#btnRestarCupos").addEventListener("click",restarCupos);

    //Bototones del cliente
    document.querySelector("#btnReservar").addEventListener("click",function(){newReserve(loggedUser.id,idLocalAReservar, obtenerValueNumerico("cantidad_Cupos_Reservar"))});
    document.querySelector("#btn_Buscador").addEventListener("click", showReservesPendientesLocal);
}

//Funcion para precargar datos de la aplicacion
function preLoadData() {
    //Precarga de Usuarios
    /////Locales/////
    addLocal("Admin", "admin", "1", 1, "18 de Julio 1351", 100, 'img/mcdonald.png',"administrador", 'img/ubicacionMC.png');
    //3 Restaurantes
    addLocal("McDonald's", "mcdonaldR", "mC12345", 1, "18 de Julio 1351", 100, 'img/mcdonald.png',"Es una red de restaurantes de comida rápida alrededor del mundo especializada en la venta de hamburguesas, papas fritas y bebidas efervescentes que, además adhieren platillos según el entorno en donde se encuentre.", 'img/ubicacionMC.png');
    addLocal("LaPasiva", "lapasivaR", "lP12345", 1, "Gral. Flores 3295", 100, 'img/lapasiva.png',"La pasiva es un restaurante de panchos muy conocido en uruguay , reconocido por su cerveza y mostaza , aunque actualmente cuenta con un amplio y variado menu de alimentos", 'img/ubicacionLP.png');
    addLocal("Subway", "subwayR", "suB12345", 1, "Constituyente 1550", 100, 'img/subway.png',"Subway es una red de restaurantes de comida rapida donde tu mismo puedes preparar tus sandwiches a gusto elijiendo cada uno de los ingredientes que quieras que contenga.", 'img/ubicacionSW.png');
    //2 Museos
    addLocal("Museo de Artes Visuales", "artesvisualesM", "arT12345", 2, "Tomas Garibaldi 2283", 100, 'img/artesVisuales.png',"El Museo Nacional de Artes Visuales alberga la mayor colección pública de pintura y escultura de Uruguay, así como una destacada selección de arte extranjero.",'img/ubicacionAV.png');
    addLocal("Museo de Historia Nacional", "historiaNacionalM", "hiS12345", 2, "Miguelete 1825", 100,'img/historiaNacional.png',"El Museo Nacional de Historia Natural es la primera institución científica y museológica del Uruguay.",'img/ubicacionHN.png');
    //2 Teatros
    addLocal("Teatro Solis", "solisT", "soL12345", 3, "Reconquista S / N", 100,'img/solis.png',"El Teatro Solís es un teatro de ópera de Uruguay, ubicado en el casco histórico de la ciudad de Montevideo. Sus obras comenzaron en 1842 —apenas 17 años después de la independencia de Uruguay— y fue inaugurado en 1856.",'img/ubicacionTS.png');
    addLocal("Teatro Life", "lifeT", "liF12345", 3, "Giannattasio Km. 21", 100,'img/life.png',"En febrero de 2012 abre el primer teatro de Ciudad de la Costa, dentro del complejo de Cines Costa Urbana y auspiciada por Patricia (FNC). Se trata de una sala polifuncional, con equipamiento técnico de última generación y mobiliario ajustable según el tipo de evento. Esta sala de espectáculos en vivo.",'img/ubicacionTL.png');

    /////Usuarios/////
    addUser("admin","admin","admin");
    addUser("Luca Podesta","lucaph03","Lucap2003");
    addUser("Agustin Cairus","agus2020","Cairus22");
    addUser("Diego Pajares","diegoUY","Diego3434");
    addUser("Agustin Padia","agustinp04","Agusagus123");
    addUser("Lu Roman","lu004","Lulu1234");
    addUser("Cami Padia","cami13","Camicami123");

    //Precarga de Reservas

    //Reserva en estado pend
    addReserve(1,1,3);
    addReserve(2,1,13);
    addReserve(3,1,9);
    addReserve(3,2,4);
    addReserve(4,1,2);

    addReserve(1,2,39);
    addReserve(1,3,03);
    addReserve(1,4,13);
    addReserve(1,5,23);
    addReserve(1,7,33);
    
    //reserva en estado Finalizado
    usuariosRegistrados[2].reserves.push(new Reserva(idReserva++,usuariosRegistrados[2],localesRegistrados[6],32,1));
    usuariosRegistrados[1].reserves.push(new Reserva(idReserva++,usuariosRegistrados[1],localesRegistrados[1],13,1));
    usuariosRegistrados[1].reserves.push(new Reserva(idReserva++,usuariosRegistrados[1],localesRegistrados[2],13,1));
    usuariosRegistrados[1].reserves.push(new Reserva(idReserva++,usuariosRegistrados[1],localesRegistrados[2],13,1));

    //Reserva en estado Cancelado
    usuariosRegistrados[2].reserves.push(new Reserva(idReserva++,usuariosRegistrados[2],localesRegistrados[6],32,-1));
    usuariosRegistrados[1].reserves.push(new Reserva(idReserva++,usuariosRegistrados[1],localesRegistrados[2],13,-1));
}


//Metodo para añadir usuarios
function addUser(pNombreCompleto,pUserName,pPassword) {
    let usuario = new Usuario(usuariosRegistrados.length,pNombreCompleto,pUserName,pPassword);
    usuariosRegistrados.push(usuario);
}

//Metodo para añadir Locales
function addLocal(pNombre,pUsername, pPassword, pTipo, pDireccion, pCuposMaximos, pLogo,pDescripcion,pImgUbicacion) {
    let local = new Local(localesRegistrados.length,pNombre,pUsername, pPassword, pTipo, pDireccion, pCuposMaximos, pLogo, true,pDescripcion,pImgUbicacion);
    localesRegistrados.push(local);
}

// //Metodo para obtener valor de las etiquetas x id
function obtenerValue(pId) {
    return document.querySelector("#"+pId).value;
}

// //Metodo para obtener valor numerico de las etiquetas x id
function obtenerValueNumerico(pId) {
    return Number(document.querySelector("#"+pId).value);
}

//Metodos de inicio de sesion , registro ,etc
//Login Local
function loginLocal() {
    let user = obtenerValue("txtUserLocal");
    let password = obtenerValue("txtPasswordLocal");
    let error = true;
    
    for(let i=0; i<localesRegistrados.length; i++) {
        if (user.toLowerCase() === localesRegistrados[i].username.toLowerCase() && password === localesRegistrados[i].password) {
            alert("INICIO DE SESION EXITOSO! ! !");
            showWindow("menu_nav__Local");
            lastDiv = "menu_nav__Local";
            error = false;
            document.querySelector("#link_nav_Local").innerHTML = localesRegistrados[i].username;
            document.querySelector("#link_nav_Usuario").innerHTML = "Cerrar Sesion";
            loggedUser = localesRegistrados[i];
            document.querySelector("#txtUserLocal").value = "";
            document.querySelector("#txtPasswordLocal").value = "";
            break;
        }
    }
    if(error === true) {
        document.querySelector("#error_login_Local").innerHTML = "Credenciales incorrectas";
    }
}

//Login Usuario
function loginUsuario() {
    let user = obtenerValue("txtUserUsuario");
    let password = obtenerValue("txtPasswordUsuario");
    let error = true;
    
    for(let i=0; i<usuariosRegistrados.length; i++) {
        if (user.toLowerCase() === usuariosRegistrados[i].username.toLowerCase() && password === usuariosRegistrados[i].password){
            alert("INICIASTE SESION CORRECTAMENTE! ! !"); 
            showWindow("menu_nav__Usuario");
            lastDiv = "menu_nav__Usuario";
            error = false;
            document.querySelector("#link_nav_Usuario").innerHTML = usuariosRegistrados[i].username;
            document.querySelector("#link_nav_Local").innerHTML = "Cerrar Sesion";
            loggedUser = usuariosRegistrados[i];
            document.querySelector("#txtUserUsuario").value = "";
            document.querySelector("#txtPasswordUsuario").value = "";
            break;
        }
    }
    if(error === true)
    {
        document.querySelector("#error_login_usuario").innerHTML = "Credenciales incorrectas";
    }
}

//Funcion de registro de usuario
function registerUsuario()
{
    let parrafo = document.querySelector("#error_register_usuario");
    let name = obtenerValue("txtFirstName");
    let user = obtenerValue("txtUsername");
    let password = obtenerValue("txtPasswordRegistro");
    let conPassword = obtenerValue("txtConfirmPassword");

    if (name != "" && user != "" && password !="") {
        if(existUsername(user)) {
            parrafo.innerHTML = "Usuario ya existente";
        }

        if(validacionPassword(password,conPassword) && !existUsername(user) && verificarEspacio(user) && verificarEspacio(password)) {
            addUser(name,user,password);
            showWindow("usuario_login");
            document.querySelector("#txtFirstName").value = "";
            document.querySelector("#txtUsername").value = "";
            document.querySelector("#txtPasswordRegistro").value = "";
            document.querySelector("#txtConfirmPassword").value = "";
        } else if(!validacionPassword(password,conPassword)) {
            parrafo.innerHTML = "Contraseñas incorrecta, Las contraseñas deben contar con al menos una mayuscula, una minuscula , un numero y un largo de 6 caracteres";
        } else {
            parrafo.innerHTML = "Los usuarios y contraseñas no pueden contar con espacios";
        }
    } else {
            parrafo.innerHTML = "Debes Completar todos los campos";
        }
}

//Funcion para validar la contraseña
function validacionPassword(pPassword,pConPassword) 
{
    let existeMayuscula = false;
    let existeMinuscula = false;
    let existeNumero = false;
    let cumple = false;

    if(pPassword === pConPassword && pPassword.length >= 7){
        for(let i= 0; i<pPassword.length;i++) {
            if (!isNaN(pPassword.charAt(i))) {
                existeNumero = true;
            } else {
                if(pPassword.charAt(i) === pPassword.charAt(i).toUpperCase()) {
                    existeMayuscula = true;
                } else { 
                    existeMinuscula = true;
                }
            }
        }

        if(existeMayuscula && existeMinuscula && existeNumero) {
            cumple = true;
        }
    }
    return cumple;
}

//Funcion para validar si exite el username del usuario
function existUsername(pUsername) {
    let exist=false;

    for(let i=0;i<usuariosRegistrados.length && !exist;i++) {
        if(pUsername.toLowerCase() === usuariosRegistrados[i].username.toLowerCase()) {
            exist = true;
        }
    }
    
    if (!exist) {
        for(let i=0;i< localesRegistrados.length && !exist;i++) {
            if(pUsername.toLowerCase() == localesRegistrados[i].username.toLowerCase()) {
                exist = true;
            }
        }
    }
    return exist;
}

function verificarEspacio(pTexto) {
    let noEspacio = true;

    for (let i = 0; i < pTexto.length && noEspacio;i++) {
        if (pTexto.charAt(i) == " ") {
            noEspacio = false;
        }
    }
    return noEspacio;
}

//Metodos Local ///////
//Cambiar Disponibilidad
function changeAvaiable() {
    let habilitado = document.querySelector("#radioHabilitado");
    let parrafo = document.querySelector("#ActualizacionHabilitado");

    if(obtenerReservasPendientesLocal(loggedUser).length == 0) {
        if (habilitado.checked) {
            localesRegistrados[loggedUser.id].habilitado = true;
        } else {
            localesRegistrados[loggedUser.id].habilitado = false;
        }  
        parrafo.innerHTML = "Actualizado con exito";  
    } else {
        parrafo.innerHTML = "No puedes tener reservas pendienes";    
    }
    
}

//Buscar Reserva
function searchReserve(pLista) {
    document.querySelector("#userReserva").innerHTML = "";
    let nameToSearch = document.querySelector("#txtNombre_Buscador").value;
    nameToSearch = nameToSearch.toLowerCase();
    let exist = false;
    let parrafoError = document.querySelector("#errorBuscador");

    for(let i=0;i<pLista.length;i++){
        let nombre = pLista[i].usuario.nombreCompleto.toLowerCase();
        if(nombre.indexOf(nameToSearch) != -1) {
            document.querySelector("#userReserva").innerHTML += '<tr><td>'+pLista[i].usuario.nombreCompleto+'</td><td>'+pLista[i].cantCupos+'</td><td><input type="button" value="Finalizar" id="'+pLista[i].usuario.id+"-"+pLista[i].id+'" class="btnFinalizar" title="Finalizar"></td></tr>'; 
            exist = true;
        }   
    }

    if ( pLista.length > 0 && exist) {
        let btnFinalizar = document.querySelectorAll(".btnFinalizar"); 

        for(let i=0; i<btnFinalizar.length;i++) {
            btnFinalizar[i].addEventListener("click", endReserve);
        }
    } else {
        parrafoError.innerHTML = "No hay reservas disponibles con ese nombre";
    }
}

//Funcion para mostrar las reservas
function showReservesPendientesLocal()
{
    document.querySelector("#userReserva").innerHTML = "";
    document.querySelector("#errorBuscador").innerHTML = "";
    let reservas = obtenerReservasLocal(loggedUser);
    let reservasPendientes = [];

    for (let i=0; i< reservas.length;i++ ) {
        if(reservas[i].local === loggedUser && reservas[i].estado == 0) {
            document.querySelector("#userReserva").innerHTML += '<tr><td>'+reservas[i].usuario.nombreCompleto+'</td><td>'+reservas[i].cantCupos+'</td><td><input type="button" value="Finalizar" id="'+reservas[i].usuario.id+"-"+reservas[i].id+'" class="btnFinalizar" title="Finalizar"></td></tr>';
            reservasPendientes.push(reservas[i]);
        }
    }

    if ( reservas.length > 0) {
        let btnFinalizar = document.querySelectorAll(".btnFinalizar") 
        for(let i=0; i<btnFinalizar.length;i++) {
            btnFinalizar[i].addEventListener("click", endReserve);
        }
        document.querySelector("#btn_Buscador").addEventListener("click",function(){searchReserve(reservasPendientes)});
    } else {
        document.querySelector("#errorBuscador").innerHTML = "No hay reservas pendientes";
    }

}

//obtiene todas las reservas de un local
function obtenerReservasLocal(pLocal) {
    let reservas = [];

    for (let i=0; i< usuariosRegistrados.length;i++ ){
        for(let j = 0; j < usuariosRegistrados[i].reserves.length; j++) {
            if(usuariosRegistrados[i].reserves[j].local === pLocal) {
                let reserva = usuariosRegistrados[i].reserves[j];
                reservas.push(reserva);
            }
        }
    }
    return reservas;
}

//Funcion para obtener las reservas pendientes de un local
function obtenerReservasPendientesLocal(pLocal) {
    let reservas = obtenerReservasLocal(pLocal);
    let reservasPendientes=[];

    for(let i=0;i<reservas.length;i++) {
        if(reservas[i].estado == 0) {
            reservasPendientes.push(reservas[i]);
        }
    }
    return reservasPendientes;
}

//Finalizar Reserva
function endReserve() 
{
    let parrafo = document.querySelector("#errorBuscador");
    parrafo.innerHTML ="";
    let btnId = this.id;
    let userId = btnId.substring(0, btnId.indexOf("-"));
    let usuario = usuariosRegistrados[userId];
    let reserveId = btnId.substring(btnId.indexOf("-")+1);

    for(let i=0; i<usuario.reserves.length;i++)
    {
        if (usuario.reserves[i].id == reserveId) 
        {
            usuario.reserves[i].estado = 1;
            loggedUser.cuposDisponible = loggedUser.cuposDisponible - usuario.reserves[i].cantCupos;
            loggedUser.reservasFinalizadas++;
            parrafo.innerHTML = "Reserva Finalizada con exito!!!"
            break;
        }
    }

    showWindow("local_administrar_reservas");
    show("menu_nav__Local");
    showReservesPendientesLocal();

}

//Metodo para mostrar todas las reservas de un local sin importar el estado
function showAllReservesLocal() {
    document.querySelector("#userReserva").innerHTML = "";
    for (let i=0; i< usuariosRegistrados.length;i++) {
        for(let j = 0; j < usuariosRegistrados[i].reserves.length; j++) {
            if(usuariosRegistrados[i].reserves[j].local === loggedUser) {
                document.querySelector("#userReserva").innerHTML += "<tr><td>"+usuariosRegistrados[i].nombreCompleto+"</td><td>"+usuariosRegistrados[i].reserves[j].cantCupos+"</td><td>"+usuariosRegistrados[i].reserves[j].estado+"</td></tr>";
            }
        }
    }
}

//Funcion para agregar cupos
function addCupos()
{
    if (loggedUser.cuposDisponible == 0) {
        let nuevosCupos = Number(document.querySelector("#agrearCuposTotales").value);
        localesRegistrados[loggedUser.id].cuposMaximos +=nuevosCupos;
        document.querySelector("#cupos_totales").innerHTML = loggedUser.cuposMaximos;
        document.querySelector("#mostrarCuposDisponibles").innerHTML = loggedUser.CuposRestantes();
    } else {
        alert("Para aumentar sus cupos maximos no debe tener ninguna reserva en pendiente ! ! ");
    }
    document.querySelector("#agrearCuposTotales").value = "";
}

//Funcion para restar los cupos
function restarCupos()
{
    if (loggedUser.cuposDisponible == 0) {
        let nuevosCupos = Number(document.querySelector("#agrearCuposTotales").value);
        localesRegistrados[loggedUser.id].cuposMaximos -= nuevosCupos;
        document.querySelector("#cupos_totales").innerHTML = loggedUser.cuposMaximos;
        document.querySelector("#mostrarCuposDisponibles").innerHTML = loggedUser.CuposRestantes();
    } else {
        alert("Para restar sus cupos maximos no debe tener ninguna reserva en pendiente ! ! ");
    }
    document.querySelector("#agrearCuposTotales").value = "";
}

//Funcion para verificar cupos
function verificarCupos(local) 
{
    let cuposMaximos = local.cuposMaximos;
    let cuposDisponible = local.cuposDisponible;

    if (cuposDisponible >= cuposMaximos )
    {
        local.habilitado = false;
    }else {
        local.habilitado = true;
    }
}

//Funcion para mostrar un ranking del local
function rankingLocal()
{
    let tabla = document.querySelector("#Ranking_Locales");
    tabla.innerHTML = "";
    for(let i = 1; i < localesRegistrados.length; i++) {
        tabla.innerHTML += "<tr><td>"+localesRegistrados[i].nombre+"</td><td>" + localesRegistrados[i].promedio +"</td></tr>"
    }
}

//Metodos CLIENTE ///
//Agregar Reserva
function addReserve(pIdUsuario,pIdLocal,pCantidadCupos) 
{
    let usuario = usuariosRegistrados[pIdUsuario];
    let local = localesRegistrados[pIdLocal];
    let add = true;

    if(local.habilitado) {
        if(noExistReserve(usuario,local)){
            let reserva = new Reserva(idReserva++, usuario,local,pCantidadCupos,0);
            usuario.reserves.push(reserva);
            local.cuposDisponible += pCantidadCupos;
            local.reservasPendientes++;
            verificarCupos(local);
        }else {
            alert("El usuario ya tiene una reserva en el local! ! !");
            add = false;
        }       
    }
    else {
        alert("El local no esta habilitado para reservar! ! !");
        add = false;
    }    
    return add;
}

function newReserve(pIdUsuario,pIdLocal,pCantidadCupos) 
{
    if(addReserve(pIdUsuario, pIdLocal, pCantidadCupos)){
        backUser(lastDiv);
    }
}

function noExistReserve(pUser,pLocal)
{
    let noExist = true;
    for(let  i=0; i<pUser.reserves.length && noExist;i++ ){
        if (pUser.reserves[i].local.id == pLocal.id && pUser.reserves[i].estado == 0){
            noExist = false;
            break;
        }
    }
    return noExist;
}

//Metodo para mostrar reservas pendientes del usuario
function showReservesPendientesUser() 
{
    let tabla = document.querySelector("#tab2_Body");
    tabla.innerHTML = "";
    let reservas = loggedUser.reserves;
    let contador = 0;

    for(let i = 0; i < reservas.length;i++) {
        if (reservas[i].estado == 0){
            tabla.innerHTML += '<tr><td><img src="' + reservas[i].local.logo + '" class="imgTablas"></img></td><td>' + reservas[i].local.nombre + '</td><td>' + reservas[i].cantCupos + '</td><td><input type="button" value="X" id="'+loggedUser.username+"-"+reservas[i].id+'" class="btnCancelar" title="Cancelar"></td></tr>';
            contador++;
        }
    }

    if ( contador> 0)
    {
        let btnCancelar = document.querySelectorAll(".btnCancelar") 
        for(let i=0; i<btnCancelar.length;i++) {
            btnCancelar[i].addEventListener("click", removeReserve);
        }
    }
}

//Metodo para cancelar reserva
function removeReserve() {
    let btnId = this.id;
    let reserveId = btnId.substring(btnId.indexOf("-")+1);
    for(let i=0; i<loggedUser.reserves.length;i++){
        if (loggedUser.reserves[i].id == reserveId){
            let local = localesRegistrados[loggedUser.reserves[i].local.id];
            loggedUser.reserves[i].estado = -1;
            local.cuposDisponible = local.cuposDisponible - loggedUser.reserves[i].cantCupos;
            verificarCupos(local);
            local.reservasCanceladas++;
            break;
        }
    }
    showWindow("usuario_reserva");
    showStatusUser();
    show("menu_nav__Usuario");
}
                
//Metodo para mostrar reservas finalizadas del usuario
function showReservesFinalizadasUser()
{
    let tabla = document.querySelector("#tab3_Body");
    tabla.innerHTML = "";
    let reservas = loggedUser.reserves;
    let contador = 0;
    
    for(let i = 0; i < reservas.length;i++)
    {
        if (reservas[i].estado == 1)
        {
            tabla.innerHTML += '<tr><td><img src="' + reservas[i].local.logo + '" class="imgTablas"></img></td><td>' + reservas[i].local.nombre + '</td><td><select id ="SelectVote'+contador+'-'+reservas[i].local.id +'""><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option></select><input type="button" value="✔" id="btnVote'+contador+'-'+reservas[i].local.id +'" class="btnVotes" title="Votar"></td></tr>';
            contador++;
        }
    }
    let buttons = document.querySelectorAll(".btnVotes");
    for(let i = 0; i < buttons.length; i++ ){
        buttons[i].addEventListener("click", votar);
    }
}

//Metodo para Mostrar reservas canceladas del udsu 
function showReservesCanceladasUser()
{
    let tabla = document.querySelector("#tab1_Body");
    tabla.innerHTML = "";
    let reservas = loggedUser.reserves;
    for(let i = 0; i < reservas.length;i++)
    {
        if (reservas[i].estado == -1)
        {
            tabla.innerHTML += '<tr><td><img src="' + reservas[i].local.logo + '" class="imgTablas"></img></td><td>' + reservas[i].local.nombre + '</td><tr>';
        }
    }
}

//Funcion para votar locales 
function votar()
{   
    btnId = this.id;
    let localId = btnId.substring(9);
    let idSelect = "SelectVote"+btnId.substring(7);
    let voto = obtenerValueNumerico(idSelect);
    localesRegistrados[localId].Score(voto);
    document.querySelector("#"+idSelect).disabled = true;
    document.querySelector("#"+btnId).disabled = true;
}

function contadorReserva(pUsuario){
    let reservas = loggedUser.reserves;
    let locales = new Array();

    let restaurante1 = 0;
    let restaurante2 = 0;
    let restaurante3 = 0;
    let museo1 = 0;
    let museo2 = 0;
    let teatro1 = 0;
    let teatro2 = 0;

    for(let i = 0; i < reservas.length;i++)
    {
        if (reservas[i].local.id == 1) {
            restaurante1 ++;
        } else if (reservas[i].local.id == 2) {
            restaurante2 ++;
        } else if (reservas[i].local.id == 3) {
            restaurante3 ++;
        } else if (reservas[i].local.id == 4) {
            museo1++;
        } else if (reservas[i].local.id == 5) {
            museo2++;
        } else if (reservas[i].local.id == 6) {
            teatro1++;
        } else if (reservas[i].local.id == 7) {
            teatro2++;
        } else { 

        }
    }

    locales.push(restaurante1,restaurante2,restaurante3,museo1,museo2,teatro1,teatro2);
    return locales;
}

//Funcion para ver la cantidad de veces que reservaste un local
function showReservesStatus()
{
    let tabla  = document.querySelector("#tab5_Body");
    tabla.innerHTML = "";
    let locales = contadorReserva(loggedUser);
   
    tabla.innerHTML =   "<tr><td>"+localesRegistrados[1].nombre+"</td><td>"+locales[0]+"</td></tr>" +
                        "<tr><td>"+localesRegistrados[2].nombre+"</td><td>"+locales[1]+"</td></tr>" +
                        "<tr><td>"+localesRegistrados[3].nombre+"</td><td>"+locales[2]+"</td></tr>" +
                        "<tr><td>"+localesRegistrados[4].nombre+"</td><td>"+locales[3]+"</td></tr>" +
                        "<tr><td>"+localesRegistrados[5].nombre+"</td><td>"+locales[4]+"</td></tr>" +
                        "<tr><td>"+localesRegistrados[6].nombre+"</td><td>"+locales[5]+"</td></tr>" +
                        "<tr><td>"+localesRegistrados[7].nombre+"</td><td>"+locales[6]+"</td></tr>" ;
}

//Metodo para mostrar el porcentaje de reservas por local
function showReservesPorcent()
{
    let tabla  = document.querySelector("#tab4_Body");
    tabla.innerHTML = "";
    let locales = contadorReserva(loggedUser);

    let masPuntuado =  0;
    let masPuntuadoLocal = localesRegistrados[0];
   
    if (locales[0] > masPuntuado ) {
       masPuntuado = locales[0];
       masPuntuadoLocal = localesRegistrados[1]; 
    }
    else if(locales[1] > locales[0])  {
        masPuntuado = locales[1];
        masPuntuadoLocal = localesRegistrados[2];
    }
    else if(locales[2] > locales[1]) {
        masPuntuado = locales[2];
        masPuntuadoLocal = localesRegistrados[3];
    }
    else if(locales[3] > locales[2]) {
        masPuntuado = locales[3];
        masPuntuadoLocal = localesRegistrados[6];
    }
    else if(locales[4] > locales[3]) {
        masPuntuado = locales[4];
        masPuntuadoLocal = localesRegistrados[7];
    }
    else if(locales[5] > locales[4]) {
        masPuntuado = locales[5];
        masPuntuadoLocal = localesRegistrados[4];
    }
    else if(locales[6] > locales[5]) {
        masPuntuado = locales[6];
        masPuntuadoLocal = localesRegistrados[5];
    }

    if (masPuntuado != 0) {
        let totalReservas = locales[0] + locales[1] + locales[2] + locales[3] + locales[4] + locales[5] + locales[6];
        let porcentajeReserva = 100 * masPuntuado / totalReservas;
        porcentajeReserva = Math.round(porcentajeReserva);
        
        tabla.innerHTML =   "<tr><td>"+masPuntuadoLocal.nombre+"</td><td>" + porcentajeReserva + "%" +"</td></tr>";    
    }
}
