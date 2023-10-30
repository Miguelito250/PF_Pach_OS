
$('#NomLocal').on('input', function () {
    validatenomLocal();
}); $('#Correo').on('input', function () {
    validateCorreo();
});
$('#Direccion').on('input', function () {
    validateDireccion();
});
$('#Telefono').on('input', function () {
    validateTelefono();
});
function validatenomLocal() {
    var nomlocal = $('#NomLocal').val();
    if (nomlocal === '') {
        $('#NomLocal').removeClass('is-valid').addClass('is-invalid');
        $('#btnEditar').prop('disabled', true);
    } else {
        $('#NomLocal').removeClass('is-invalid').addClass('is-valid');
        $('#btnEditar').prop('disabled', false);
    }
}
function validateDireccion() {
    var direccion = $('#Direccion').val();
    if (direccion.trim() === '') {
        $('#Direccion').removeClass('is-valid').addClass('is-invalid');
        $('#btnEditar').prop('disabled', true);
    } else {
        $('#Direccion').removeClass('is-invalid').addClass('is-valid');
        $('#btnEditar').prop('disabled', false);
    }
}
function validateTelefono() {
    var telefono = $('#Telefono').val();
    if (telefono.trim() === '') {
        $('#Telefono').removeClass('is-valid').addClass('is-invalid');
        $('#btnEditar').prop('disabled', true);
    } else {
        $('#Telefono').removeClass('is-invalid').addClass('is-valid');
        $('#btnEditar').prop('disabled', false);
    }
}
function validateCorreo() {
    var correo = $('#Correo').val();
    var correoFormato = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (correo.trim() === '' || !correoFormato.test(correo)) {
        $('#Correo').removeClass('is-valid').addClass('is-invalid');
        $('#btnEditar').prop('disabled', true);
    } else {
        $('#Correo').removeClass('is-invalid').addClass('is-valid');
        $('#btnEditar').prop('disabled', false);
    }
}

