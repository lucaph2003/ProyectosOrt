window.addEventListener("load",setup);
const api = "https://calcount.develotion.com";

function setup(){
    //registrarPersona();
    //iniciarSesion();
    obtenerPaises();
    // obtenerUsuariosPorPais();
    // obtenerRegistros();
    // agregarRegistro();
    // eliminarRegistro();
    // obtenerAlimentos();
}

//////REGISTRO
const registrarPersona = () => {
    const datos = {
        "usuario": "LucaPrueba3",
        "password": "LucaPrueba1",
        "idPais": 235,
        "caloriasDiarias": 2000
    };
    
    // Configuración de la solicitud
    const opciones = {
        method: 'POST', // Método HTTP
        headers: {
            'Content-Type': 'application/json' // Tipo de contenido del cuerpo de la solicitud
        },
        body: JSON.stringify(datos) // Convertir los datos a formato JSON
    };

    fetch(api + "/usuarios.php", opciones)
        .then(response=>{
            if(!response.ok){
                throw new Error("Error al registrar: " + response.status);
            }
            return response.json();
        })
        .then(datos => {
            localStorage.setItem('apiKey', datos.apiKey);
            localStorage.setItem('usuarioId', datos.id);
            console.log(datos);
            console.log("La apiKEy es :" + localStorage.getItem('apiKey'));
            console.log("El usuario id es: " + localStorage.getItem('usuarioId'));
        })
        .catch(error => {
            console.error('Error:', error); // Manejar errores de red u otros errores
        })
}


////////L<OGINN
const iniciarSesion = () => {
    const datosLogin = {
        "usuario": "LucaPrueba1",
        "password": "LucaPrueba1"
    };
    const opciones = {
        method: 'POST', // Método HTTP
        headers: {
            'Content-Type': 'application/json' // Tipo de contenido del cuerpo de la solicitud
        },
        body: JSON.stringify(datosLogin) // Convertir los datos a formato JSON
    };

    fetch(api + "/login.php", opciones)
    .then(r => r.json())
    .then(datos => {
        //Guardar en el local storage
        localStorage.setItem('apiKey', datos.apiKey);
        localStorage.setItem('usuarioId', datos.id);
        console.log(datos);
        console.log("La apiKEy es :" + localStorage.getItem('apiKey'));
        console.log("El usuario id es: " + localStorage.getItem('usuarioId'));
    })
}

//PAISES
const obtenerPaises = () => {
    fetch(api + "/paises.php")
    .then(r => r.json())
    .then(datos => {
        console.log(datos);
        datos.paises.forEach(pais => {
            console.log(pais.name);
        });
    })
}

const obtenerUsuariosPorPais = () => {
    const opciones = {
        method: 'GET', // Método HTTP
        headers: {
            'Content-Type': 'application/json' ,
           'apikey' : ''+localStorage.getItem('apiKey')+'',
            'iduser' : ''+localStorage.getItem('usuarioId')+''
        }
      };

    fetch(api + "/usuariosPorPais.php",opciones)
    .then(r => r.json())
    .then(datos => {
        console.log(datos);
    })
}


/////////////REGISTROS
const obtenerRegistros = () => {
    const opciones = {
        method: 'GET', // Método HTTP
        headers: {
            'Content-Type': 'application/json' ,
           'apikey' : ''+localStorage.getItem('apiKey')+'',
            'iduser' : ''+localStorage.getItem('usuarioId')+''
        }
      };

    fetch(api + "/registros.php?idUsuario="+localStorage.getItem('usuarioId'),opciones)
    .then(r => r.json())
    .then(datos => {
        console.log(datos)
    })
}

const agregarRegistro = () => {
    const datos = {
        "idAlimento": 8,
        "idUsuario": localStorage.getItem('usuarioId'),
        "cantidad": 200,
        "fecha": "2023-09-21"
    };

    const opciones = {
        method: 'POST', // Método HTTP
        headers: {
            'Content-Type': 'application/json', // Tipo de contenido del cuerpo de la solicitud
            'apikey' : ''+localStorage.getItem('apiKey')+'',
            'iduser' : ''+localStorage.getItem('usuarioId')+''
        },
        body: JSON.stringify(datos) // Convertir los datos a formato JSON
    };


    fetch(api + "/registros.php",opciones)
    .then(r => r.json())
    .then(datos => {
        console.log(datos)
    })
}

const eliminarRegistro = () => {
    const opciones = {
        method: 'DELETE', // Método HTTP
        headers: {
            'Content-Type': 'application/json' ,
           'apikey' : ''+localStorage.getItem('apiKey')+'',
            'iduser' : ''+localStorage.getItem('usuarioId')+''
        }
      };

    fetch(api + "/registros.php?idRegistro="+4150,opciones)
    .then(r => r.json())
    .then(datos => {
        console.log(datos)
    })
}


/////////////ALIMENTOS
const obtenerAlimentos = () => {
    const opciones = {
        method: 'GET', // Método HTTP
        headers: {
            'Content-Type': 'application/json' ,
           'apikey' : ''+localStorage.getItem('apiKey')+'',
            'iduser' : ''+localStorage.getItem('usuarioId')+''
        }
      };

    fetch(api + "/alimentos.php",opciones)
    .then(r => r.json())
    .then(datos => {
        console.log(datos)
    })
}