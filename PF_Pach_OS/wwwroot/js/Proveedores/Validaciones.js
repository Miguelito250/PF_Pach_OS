var formulario = document.getElementById('formularioProveedor');
var tipoDocumento = document.getElementById('TipoDocumento');
var nit = document.getElementById('Nit')
var nomLocal = document.getElementById('NomLocal');
var direccion = document.getElementById('Direccion');
var telefono = document.getElementById('Telefono');
var correo = document.getElementById('Correo');

var tipoDocumentoMensaje = document.getElementById('TipoDocumentoMensaje');
var nitMensaje = document.getElementById('NitMensaje')
var nomLocalMensaje = document.getElementById('NomLocalMensaje');
var direccionMensaje = document.getElementById('DireccionMensaje');
var telefonoMensaje = document.getElementById('TelefonoMensaje');
var correoMensaje = document.getElementById('CorreoMensaje');

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 1700,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
});

function EnviarProveedor(event) {
    event.preventDefault();
    if (formulario.checkValidity() && !tipoDocumento.classList.contains('is-invalid') && !nit.classList.contains('is-invalid')
            && !nomLocal.classList.contains('is-invalid') && !direccion.classList.contains('is-invalid') && !telefono.classList.contains('is-invalid')
            && !correo.classList.contains('is-invalid')) {
        Swal.fire({
            title: '\u00A1\u00C9xito!',
            text: 'Proveedor Registrado Exitosamente',
            timer: 1500,
            icon: 'success',
            showConfirmButton: false,
        }).then((result) => {
            formulario.removeEventListener('submit', EnviarProveedor);
            formulario.submit();
            location.reload();
        })
    } else {
        Toast.fire({
            icon: 'error',
            title: 'Formulario inv\u00E1lido'
        });
        ValidarNit();
        ValidarNomLocal();
        ValidarDireccion();
        ValidarTelefono();
        ValidarCorreo()
    }
}

formulario.addEventListener('submit', EnviarProveedor);

nit.addEventListener('input', function () {
    ValidarNit();
});

nomLocal.addEventListener('input', function () {
    ValidarNomLocal();
});

direccion.addEventListener('input', function () {
    ValidarDireccion();
});

telefono.addEventListener('input', function () {
    ValidarTelefono();
});

correo.addEventListener('input', function () {
    ValidarCorreo();
});

function ValidarNit() {
    let nitValor = nit.value;
    tipoDocumento.classList.add('is-valid')
    const esNumerico = /^[0-9]*$/.test(nitValor);
    // Restablecer los estilos y mensajes de validación
    nit.classList.remove('is-invalid', 'is-valid');
    nitMensaje.textContent = '';

    // Validar si el campo está vacío
    if (nitValor.trim() === '') {
        nit.classList.add('is-invalid');
        nitMensaje.textContent = 'Por favor ingrese el numero de documento.';

    } else if (/^\s/.test(nitValor)) {
        nit.classList.add('is-invalid');
        nitMensaje.textContent = 'No se puede comenzar con un espacio en blanco.';
    }
    // Validar longitud del nombre
    else if (nitValor.length < 3 || nitValor.length > 11) {
        nit.classList.add('is-invalid');
        nitMensaje.textContent = 'El documento debe tener entre 3 y 11 caracteres.';
    } else if (/[^a-zA-Z0-9\s]/.test(nitValor)) {
        nit.classList.add('is-invalid');
        nitMensaje.textContent = 'No se pueden ingresar caracteres especiales.';
    } else if (!esNumerico) {
        nit.classList.add('is-invalid');
        nitMensaje.textContent = 'No se pueden ingresar letras';
    }
    else {
        // El campo es válido
        nit.classList.add('is-valid');
    }

    $.ajax({
        type: 'GET',
        url: '/Proveedores/NitRepetido',
        data: { numeroDocumento: nitValor },
        success: function (result) {
            if (result === true) {
                nit.classList.add('is-invalid');
                nitMensaje.textContent = 'No se puede repetir un numero de documento.';
            } else {
                nit.classList.add('is-valid');
            }
        },
        error: function () {
            // Manejo de errores si la solicitud falla
            console.log('Error en la solicitud AJAX');
        }
    });
}
function ValidarNomLocal() {
    // Obtener el valor seleccionado del campo select
    let nomLocalValor = nomLocal.value;

    // Restablecer los estilos y mensajes de validación
    nomLocal.classList.remove('is-invalid', 'is-valid');
    nomLocalMensaje.textContent = '';

    if (nomLocalValor.trim() === '') {
        nomLocal.classList.add('is-invalid');
        nomLocalMensaje.textContent = 'Por favor ingrese el nombre del local.';

    } else if (/^\s/.test(nomLocalValor)) {
        nomLocal.classList.add('is-invalid');
        nomLocalMensaje.textContent = 'No se puede comenzar con un espacio en blanco.';
    }
    // Validar longitud del nombre
    else if (nomLocalValor.length < 3 || nomLocalValor.length > 20) {
        nomLocal.classList.add('is-invalid');
        nomLocalMensaje.textContent = 'El nombre debe tener entre 3 y 20 caracteres.';
    } else if (/[^a-zA-Z0-9\s]/.test(nomLocalValor)) {
        nomLocal.classList.add('is-invalid');
        nomLocalValor.textContent = 'No se pueden ingresar caracteres especiales.';
    }
    else {
        // El campo es válido
        nomLocal.classList.add('is-valid');
    }

    $.ajax({
        type: 'GET',
        url: '/Proveedores/NomLocalRepetido',
        data: { nombreLocal: nomLocalValor },
        success: function (result) {
            if (result === true) {
                nomLocal.classList.add('is-invalid');
                nomLocalMensaje.textContent = 'No se puede repetir un nombre de local.';
            } else {
                nomLocal.classList.add('is-valid');
            }
        },
        error: function () {
            // Manejo de errores si la solicitud falla
            console.log('Error en la solicitud AJAX');
        }
    });
}
function ValidarDireccion() {
    // Obtener el valor seleccionado del campo select
    var direccionValor = direccion.value;

    // Restablecer los estilos y mensajes de validación
    direccion.classList.remove('is-invalid', 'is-valid');
    direccionMensaje.textContent = '';

    if (direccionValor.trim() === '') {
        direccion.classList.add('is-invalid');
        direccionMensaje.textContent = 'Por favor ingrese la direccion.';

    } else if (/^\s/.test(direccionValor)) {
        direccion.classList.add('is-invalid');
        direccionMensaje.textContent = 'No se puede comenzar con un espacio en blanco.';
    }
    // Validar longitud del nombre
    else if (direccionValor.length < 3 || direccionValor.length > 30) {
        direccion.classList.add('is-invalid');
        direccionMensaje.textContent = 'La direccion debe tener entre 3 y 30 caracteres.';

    }
    else {
        // El campo es válido
        direccion.classList.add('is-valid');
    }
}
function ValidarTelefono() {
    let telefonoValor = telefono.value;
    const esNumerico = /^[0-9]*$/.test(telefonoValor);
    // Restablecer los estilos y mensajes de validación
    telefono.classList.remove('is-invalid', 'is-valid');
    telefonoMensaje.textContent = '';

    // Validar si el campo está vacío
    if (telefonoValor.trim() === '') {
        telefono.classList.add('is-invalid');
        telefonoMensaje.textContent = 'Por favor ingrese el telefono.';

    } else if (/^\s/.test(telefonoValor)) {
        telefono.classList.add('is-invalid');
        telefonoMensaje.textContent = 'No se puede comenzar con un espacio en blanco.';
    }
    // Validar longitud del nombre
    else if (telefonoValor.length < 3 || telefonoValor.length > 12) {
        telefono.classList.add('is-invalid');
        telefonoMensaje.textContent = 'El telefono debe tener entre 3 y 12 caracteres.';
    } else if (/[^a-zA-Z0-9\s]/.test(telefonoValor)) {
        telefono.classList.add('is-invalid');
        telefonoMensaje.textContent = 'No se pueden ingresar caracteres especiales.';
    } else if (!esNumerico) {
        telefono.classList.add('is-invalid');
        telefonoMensaje.textContent = 'No se pueden ingresar letras';
    }
    else {
        // El campo es válido
        telefono.classList.add('is-valid');
    }

    $.ajax({
        type: 'GET',
        url: '/Proveedores/TelefonoRepetido',
        data: { telefono: telefonoValor },
        success: function (result) {
            if (result === true) {
                telefono.classList.add('is-invalid');
                telefonoMensaje.textContent = 'No se puede repetir un numero de telefono.';
            } else {
                telefono.classList.add('is-valid');
            }
        },
        error: function () {
            // Manejo de errores si la solicitud falla
            console.log('Error en la solicitud AJAX');
        }
    });
}
function ValidarCorreo() {
    let correoValor = correo.value;
    const esValido = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(correoValor);
    // Restablecer los estilos y mensajes de validación
    correo.classList.remove('is-invalid', 'is-valid');
    correoMensaje.textContent = '';

    // Validar si el campo está vacío
    if (correoValor.trim() === '') {
        correo.classList.add('is-invalid');
        correoMensaje.textContent = `Por favor, escriba un un correo eletr\u00F3nico`;
    }
    // Validar si el correo electrónico es válido
    else if (!esValido) {
        correo.classList.add('is-invalid');
        correoMensaje.textContent = `Por favor, escribe un correo eletr\u00F3nico v\u00E1lido.`;
    }
    else {
        // El campo es válido
        correo.classList.add('is-valid');
    }

    $.ajax({
        type: 'GET',
        url: '/Proveedores/CorreoRepetido',
        data: { correo: correoValor },
        contentType: 'application/json; charset=utf-8',
        dataType: 'text',
        success: function (result) {
            if (result === true) {
                correo.classList.add('is-invalid');
                correoMensaje.textContent = 'No se puede repetir un correo.';
            } else {
                correo.classList.add('is-valid');
            }
        },
        error: function () {
            // Manejo de errores si la solicitud falla
            console.log('Error en la solicitud AJAX');
        }
    });
}
