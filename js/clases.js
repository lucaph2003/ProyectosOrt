//Clase Usuario
class Usuario {
    constructor(pId, pNombreCompleto,pUsername, pPassword) { 
    this.id = pId;
    this.nombreCompleto = pNombreCompleto;
    this.username = pUsername;
    this.password = pPassword;
    this.reserves = new Array();
    }
}

//Clase Local
class Local {
    constructor(pId, pNombre,pUsername, pPassword, pTipo, pDireccion, pCuposMaximos, pLogo, pHabilitado,pDescripcion,pImgUbicacion) {
    this.id = pId;
    this.nombre = pNombre;
    this.username = pUsername;
    this.password = pPassword;
    this.tipo = pTipo;
    this.direccion = pDireccion;
    this.cuposMaximos = pCuposMaximos;
    this.cuposDisponible = 0;
    this.logo = pLogo;
    this.habilitado = pHabilitado;
    this.descripcion = pDescripcion;
    this.imgUbicacion = pImgUbicacion;
    this.reservasPendientes = 0;
    this.reservasFinalizadas = 0;
    this.reservasCanceladas = 0;
    this.score = 0;
    this.promedio = 0;
    this.votos = 0;
    }   

    Score(puntaje) {
        this.votos++;
        this.score += puntaje;
        this.promedio = this.score / this.votos;
    }

    CuposRestantes() {
        let cuposRestantes = this.cuposMaximos - this.cuposDisponible;
        return cuposRestantes;
    }
    
    PorcentajeOcupacion() {
        let porcentaje = (100 * this.cuposDisponible) /this.cuposMaximos;
        porcentaje = Math.round(porcentaje);
        return porcentaje+"%";
    }
}

//Clase Reservas
//Estados de reserva: 0:Pendiente , 1:Finalizada , -1:Cancelada 
class Reserva {
    constructor(pId,pUsuario, pLocal, pCantCupos, pEstado) {
        this.id = pId;
        this.usuario = pUsuario;
        this.local = pLocal;
        this.cantCupos = pCantCupos;
        this.estado = pEstado
    }
}