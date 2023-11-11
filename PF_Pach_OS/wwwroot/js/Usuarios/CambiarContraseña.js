document.addEventListener("DOMContentLoaded", function () {

    const formulario_Contraseña = document.getElementById('CambioContraseña');

    const AntiguaContraseña = document.getElementById("AntiguaContraseña");
    const mensaje_AntiguaContraseña = document.getElementById("mensaje_AntiguaContraseña");

    const NuevaContraseña = document.getElementById("NuevaContraseña");
    const mensaje_NuevaContraseña = document.getElementById("mensaje_NuevaContraseña");

    const ConfirmarContraseña = document.getElementById("ConfirmarContraseña");
    const mensaje_ConfirmarContraseña = document.getElementById("mensaje_ConfirmarContraseña");

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

    AntiguaContraseña.addEventListener('input', ValidarAntiguaCon);
    NuevaContraseña.addEventListener('input', ValidarNuevaCon)
    ConfirmarContraseña.addEventListener('input', ValidarConfirmarCon)

    formulario_Contraseña.addEventListener('submit', EnviarFormulario)

    function EnviarFormulario(event) {
        event.preventDefault();
        ValidarAntiguaCon();
        ValidarNuevaCon();
        ValidarConfirmarCon();
        if (formulario_Contraseña.checkValidity()) {

            ajax().then(function (resultado) {
                if (resultado) {
                    Swal.fire({
                        title: '¡Éxito!',
                        text: 'La contraseña se cambio con exito',
                        timer: 2400,
                        icon: 'success',
                        showConfirmButton: false,
                    }).then((result) => {

                        formulario_Contraseña.removeEventListener('submit', EnviarFormulario)
                        formulario_Contraseña.submit();
                    });
                    console.log("Contraseña cambiada con éxito");
                } else {
                    Swal.fire({
                        title: 'Ups...',
                        timer: 2700,
                        text: 'La contraseña anterior es Erronea',
                        icon: 'error',
                        showConfirmButton: false,
                    });
                    console.log("Error al cambiar la contraseña");
                }
            }).catch(function (error) {
                // La solicitud AJAX falló, puedes manejar el error aquí.
                console.log("Error en la solicitud AJAX: " + error);
            });


        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            ValidarAntiguaCon();
            ValidarNuevaCon();
            ValidarConfirmarCon();
        }


    }
    function ajax() {
        return new Promise(function (resolve, reject) {

            $.ajax({
                url: '/AspNetUsers/CambiarContraseña',
                type: 'POST',
                data: {
                    AntiguaContraseña: AntiguaContraseña.value,
                    NuevaContraseña: NuevaContraseña.value
                },
                success: function (data) {
                    resolve(data)
                },
                error: function (xhr, status, error) {
                    reject(error);
                }
            });
        });
    }

    function ValidarAntiguaCon() {
        const AntiguoValor = AntiguaContraseña.value;


        AntiguaContraseña.classList.remove('is-invalid', 'is-valid');
        mensaje_AntiguaContraseña.textContent = "";
        if (AntiguoValor.trim() === '') {
            AntiguaContraseña.classList.add('is-invalid');
            mensaje_AntiguaContraseña.textContent = 'El campo no puede estar vacio';
        }
        else {
            AntiguaContraseña.classList.add('is-valid');

        }
    }
    function ValidarNuevaCon() {
        const NuevoValor = NuevaContraseña.value;

        const minLength = 8;
        const hasUppercase = /[A-Z]/.test(NuevoValor);
        const hasLowercase = /[a-z]/.test(NuevoValor);
        const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(NuevoValor);
        const hasNoSpaces = !/\s/.test(NuevoValor);
        const hasNumber = /(?=.*\d)/.test(NuevoValor);
        NuevaContraseña.classList.remove('is-invalid', 'is-valid');
        mensaje_NuevaContraseña.textContent = "";
        if (NuevoValor.trim() === '') {
            NuevaContraseña.classList.add('is-invalid');
            mensaje_NuevaContraseña.textContent = 'El campo no puede estar vacio';
        }
        if (NuevoValor.length < minLength) {
            NuevaContraseña.classList.add('is-invalid');
            mensaje_NuevaContraseña.textContent = 'El campo debe terne al menos 8 caracteres';
        } else if (!hasUppercase) {
            NuevaContraseña.classList.add('is-invalid');
            mensaje_NuevaContraseña.textContent = 'El campo debe terne al menos una mayuscula';
        } else if (!hasLowercase) {
            NuevaContraseña.classList.add('is-invalid');
            mensaje_NuevaContraseña.textContent = 'El campo debe terne al menos una minuscula';
        } else if (!hasSpecialChar) {
            NuevaContraseña.classList.add('is-invalid');
            mensaje_NuevaContraseña.textContent = 'El campo debe terne al menos un caracter especial';
        } else if (!hasNoSpaces) {
            NuevaContraseña.classList.add('is-invalid');
            mensaje_NuevaContraseña.textContent = 'El campo no debe tener espacios';
        } else if (!hasNumber) {
            NuevaContraseña.classList.add('is-invalid');
            mensaje_NuevaContraseña.textContent = 'El campo debe tener al menos un numero';
        }
        else {
            NuevaContraseña.classList.add('is-valid');

        }

    }
    function ValidarConfirmarCon() {
        const ConfirmarValor = ConfirmarContraseña.value;

        const minLength = 8;
        const hasUppercase = /[A-Z]/.test(ConfirmarValor);
        const hasLowercase = /[a-z]/.test(ConfirmarValor);
        const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(ConfirmarValor);
        const hasNoSpaces = !/\s/.test(ConfirmarValor);
        const hasNumber = /(?=.*\d)/.test(ConfirmarValor);


        ConfirmarContraseña.classList.remove('is-invalid', 'is-valid');
        mensaje_ConfirmarContraseña.textContent = "";
        if (ConfirmarValor.trim() === '') {
            ConfirmarContraseña.classList.add('is-invalid');
            mensaje_ConfirmarContraseña.textContent = 'El campo no puede estar vacio';
        }
        if (ConfirmarValor.length < minLength) {
            ConfirmarContraseña.classList.add('is-invalid');
            mensaje_ConfirmarContraseña.textContent = 'El campo debe terne al menos 8 caracteres';
        } else if (!hasUppercase) {
            ConfirmarContraseña.classList.add('is-invalid');
            mensaje_ConfirmarContraseña.textContent = 'El campo debe terne al menos una mayuscula';
        } else if (!hasLowercase) {
            ConfirmarContraseña.classList.add('is-invalid');
            mensaje_ConfirmarContraseña.textContent = 'El campo debe terne al menos una minuscula';
        } else if (!hasSpecialChar) {
            ConfirmarContraseña.classList.add('is-invalid');
            mensaje_ConfirmarContraseña.textContent = 'El campo debe terne al menos un caracter especial';
        } else if (!hasNoSpaces) {
            ConfirmarContraseña.classList.add('is-invalid');
            mensaje_ConfirmarContraseña.textContent = 'El campo no debe tener espacios';
        } else if (!hasNumber) {
            ConfirmarContraseña.classList.add('is-invalid');
            mensaje_ConfirmarContraseña.textContent = 'El campo debe tener al menos un numero';
        } else if (ConfirmarValor != NuevaContraseña.value) {
            ConfirmarContraseña.classList.add('is-invalid');
            mensaje_ConfirmarContraseña.textContent = 'Las contraseñas no coinciden';
        }
        else {
            ConfirmarContraseña.classList.add('is-valid');

        }
    }



});