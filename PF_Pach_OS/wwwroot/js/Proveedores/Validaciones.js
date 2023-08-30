
    $('#NomLocal').on('input', function () {
        validatenomLocal();
    });
    function validatenomLocal() {
        var nomlocal = $('#NomLocal').val();
        if (nomlocal === '') {
            $('#NomLocal').removeClass('is-valid').addClass('is-invalid');
        } else {
            $('#NomLocal').removeClass('is-invalid').addClass('is-valid');
        }
    }
    $('#Correo').on('input', function () {
        validateCorreo();
    });

    function validateCorreo() {
        var correo = $('#Correo').val();
        var correoFormato = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

        if (correo.trim() === '' || correoFormato.test(correo)) {
            $('#Correo').removeClass('is-invalid').addClass('is-valid');
        } else {
            $('#Correo').removeClass('is-valid').addClass('is-invalid');
        }
    }

$('#Direccion').on('input', function () {
    validateDireccion();
});

$('#Telefono').on('input', function () {
    validateTelefono();
});


function validateDireccion() {
    var direccion = $('#Direccion').val();
    if (direccion.trim() === '') {
        $('#Direccion').removeClass('is-valid').addClass('is-invalid');
    } else {
        $('#Direccion').removeClass('is-invalid').addClass('is-valid');
    }
}

function validateTelefono() {
    var telefono = $('#Telefono').val();
    if (telefono.trim() === '') {
        $('#Telefono').removeClass('is-valid').addClass('is-invalid');
    } else {
        $('#Telefono').removeClass('is-invalid').addClass('is-valid');
    }
}

$('#formulario').submit(function (event) {

    validateNit();
    validatenomLocal();
    validateDireccion();
    validateTelefono();
    validateCorreo();

    if ($('.form-control.is-invalid').length > 0) {
        event.preventDefault();
    }
});