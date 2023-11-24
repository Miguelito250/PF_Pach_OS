document.addEventListener('DOMContentLoaded', (event) => {
    var formulario = document.getElementById('formCambiarContrasena');
    var correo = document.getElementById('Input_Email');
    var passwordInput = document.getElementById('Input_Password');
    var errorSpanContrasena = document.getElementById('contrasenaMensaje');
    var confirmPasswordInput = document.getElementById('Input_ConfirmPassword');
    var errorSpan = document.getElementById('confirmarContrasenaMensaje');

    formulario.addEventListener('submit', EnvioRecuperarContraseña)
    correo.addEventListener('input', ValidarCorreo)
    passwordInput.addEventListener('input', ValidarContrasena)
    confirmPasswordInput.addEventListener('input', ValidarConfirmarContrasena)
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
    function EnvioRecuperarContraseña(event) {
        event.preventDefault();
        ValidarCorreo()
        ValidarContrasena()
        ValidarConfirmarContrasena()
        if (formulario.checkValidity() && !correo.classList.contains('is-invalid') && !passwordInput.classList.contains('is-invalid') && !confirmPasswordInput.classList.contains('is-invalid')) {
            Swal.fire({
                title: '¡Éxito!',
                text: '¡Cambio de contraseña exitoso!',
                timer: 2400,
                icon: 'success',
                showConfirmButton: false,
            }).then((result) => {
                formulario.removeEventListener('submit', EnvioRecuperarContraseña);
                formulario.submit();
            });

        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            ValidarCorreo()
            ValidarContrasena()
            ValidarConfirmarContrasena()
        }

    }

    function ValidarCorreo() {
        let correoValor = correo.value
        var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        let correoValido = re.test(String(correoValor).toLowerCase());

        correo.classList.remove('is-invalid', 'is-valid');
        correoMensaje.textContent = '';

        if (!correoValido) {
            correo.classList.add('is-invalid')
            correoMensaje.textContent = 'Correo electrónico no válido';
        } else {
            correo.classList.add('is-valid')
        }
    }

    function ValidarContrasena() {
        var password = passwordInput.value;

        passwordInput.classList.remove('is-invalid', 'is-valid');
        errorSpanContrasena.textContent = '';

        if (password.length < 6) {
            passwordInput.classList.add('is-invalid');
            errorSpanContrasena.textContent = "La contraseña debe tener al menos 6 caracteres";
        } else if (password.length > 50) {
            passwordInput.classList.add('is-invalid');
            errorSpanContrasena.textContent = "La contraseña no puede tener más de 50 caracteres";
        } else if (!/[0-9]/.test(password)) {
            passwordInput.classList.add('is-invalid');
            errorSpanContrasena.textContent = "La contraseña debe contener un número";
        } else if (!/[a-z]/.test(password)) {
            passwordInput.classList.add('is-invalid');
            errorSpanContrasena.textContent = "La contraseña debe contener una letra minúscula";
        } else if (!/[A-Z]/.test(password)) {
            passwordInput.classList.add('is-invalid');
            errorSpanContrasena.textContent = "La contraseña debe contener una letra mayúscula";
        } else if (!/[!@#$%^&*.]/.test(password)) {
            passwordInput.classList.add('is-invalid');
            errorSpanContrasena.textContent = "La contraseña debe contener un carácter especial";
        } else {
            passwordInput.classList.add('is-valid');
        }
    }
    function ValidarConfirmarContrasena() {
        confirmPasswordInput.classList.remove('is-invalid', 'is-valid');
        errorSpan.textContent = '';

        if (confirmPasswordInput.value === '') {
            confirmPasswordInput.classList.add('is-invalid');
            errorSpan.textContent = "El campo no puede ir vacío";
        }
        else if (passwordInput.value !== confirmPasswordInput.value) {
            confirmPasswordInput.classList.add('is-invalid');
            errorSpan.textContent = "Las contraseñas no coinciden";
        } else {
            confirmPasswordInput.classList.add('is-valid')
        }
    }
})