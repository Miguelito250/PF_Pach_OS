﻿

$(document).ready(function () {

    var formularo_Permiso = document.getElementById('formulario1');

    var nombreRol = document.getElementById('nomrol');
    var Mensaje_nombre = document.getElementById('mensaje_nombre');

  

    function ValidarFormulario(event) {
        event.preventDefault();
        ValidarNombre();
        var permisos = [];
        $('input[name="ValoresSeleccionados"]:checked').each(function () {
            permisos.push($(this).val());
        });
        if (formularo_Permiso.checkValidity() && !nombreRol.classList.contains('is-invalid')) {
            if (permisos.length < 1) {
                Swal.fire({
                    title: 'Ups...',
                    timer: 2700,
                    text: 'Debes agregar al menos un permiso para crear un rol.',
                    icon: 'error',
                    showConfirmButton: false,
                });

                // Detener la ejecución del código o realizar alguna acción adicional si es necesario
                return;
            } else {
                Swal.fire({
                    title: '¡Éxito!',
                    text: 'El rol ha sido creado',
                    timer: 2400,
                    icon: 'success',
                    showConfirmButton: false,
                }).then((result) => {
                    console.log('Formulario válido');
                    $.ajax({
                        url: '/RolPermisos/Crear',
                        type: 'POST',
                        data: {
                            permisos: permisos,
                            nomRol: nombreRol.value
                        },
                        success: function (data) {
                            window.location.reload();
                        },
                        error: function (xhr, status, error) {
                            reject(error);
                        }
                    });

                    formularo_Permiso.removeEventListener('submit', ValidarFormulario);

                });
            }
        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            ValidarNombre();
        }

    }

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

    function ValidarNombre() {


        let ValorNombre = nombreRol.value;
        const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(ValorNombre);
        const hasNumber = /(?=.*\d)/.test(ValorNombre);
        nombreRol.classList.remove('is-invalid', 'is-valid');
        Mensaje_nombre.textContent = ''
        if (ValorNombre.trim() === '') {
            nombreRol.classList.add('is-invalid');
            Mensaje_nombre.textContent = 'El campo no puede estar vacio'

        } else if (ValorNombre.length < 4) {
            nombreRol.classList.add('is-invalid');
            Mensaje_nombre.textContent = 'El nombre debe tener almenos 4 caracteres';

        } else if (ValorNombre.length > 20) {
            nombreRol.classList.add('is-invalid');
            Mensaje_nombre.textContent = 'El nombre debe tener menos de 20 caracteres';
        } else if (hasSpecialChar) {
            nombreRol.classList.add('is-invalid');
            Mensaje_nombre.textContent = 'El nombre no puede tener caracteres especiales';

        } else if (hasNumber) {
            nombreRol.classList.add('is-invalid');
            Mensaje_nombre.textContent = 'El nombre no puede tener numeros';
        }
        else {
            nombreRol.classList.add('is-valid');

        }
        $.ajax({
            type: 'GET',
            url: '/RolPermisos/NombreDuplicado',
            data: { Nombre: ValorNombre },
            success: function (result) {
                if (result === true) {
                    nombreRol.classList.remove('is-valid');
                    nombreRol.classList.add('is-invalid');
                    Mensaje_nombre.textContent = 'No se puede repetir el nombre.';
                } 
            },
            error: function () {
                // Manejo de errores si la solicitud falla
                console.log('Error en la solicitud AJAX');
            }
        });

    }

    nombreRol.addEventListener('input', ValidarNombre)

    
    formularo_Permiso.addEventListener('submit', ValidarFormulario)

});
