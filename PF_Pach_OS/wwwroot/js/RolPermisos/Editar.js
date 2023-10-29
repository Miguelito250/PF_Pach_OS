var Permisos = [];
var nomRol;
var idrol;

$(document).ready(function () {
    var mensaje_permiso = document.getElementById("mensaje_permiso");

    idrol = $("#idrol").val();


    $("#btnEnviar").on("click", function () {
        $('input[name="ValoresSeleccionados"]:checked').each(function () {
            Permisos.push($(this).val());
            mensaje_permiso.textContent = ''
        });
        console.log(idrol);
        console.log(nomRol)
        console.log(Permisos)

    });

});

document.addEventListener('DOMContentLoaded', function () {

    var formulario_rol = document.getElementById("formulario1");

    var nombreRol = document.getElementById("nombreRol");
    var mensaje_nombre = document.getElementById("mensaje_nombre");

    var mensaje_permiso = document.getElementById("mensaje_permiso");

    const enlacesMenu = document.querySelectorAll('.links-modulos');
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

    //Escuchadores para rol
    formulario_rol.addEventListener('submit', EnvioRol);
    nombreRol.addEventListener('input', ValidarNombre);

    function ValidarNombre() {
        var ValorNombre = nombreRol.value;
        var caracterMinimo = 3;
        var caracterMAximo = 20;

        nombreRol.classList.remove('is-invalid', 'is-valid');
        mensaje_nombre.textContent = ''

        if (ValorNombre.trim() === '') {
            nombreRol.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El campo no puede estar vacio';
        } else if (ValidarNombre < caracterMinimo) {
            nombreRol.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El nombre debe tener más de 3 caracteres';
        } else if (ValidarNombre > caracterMAximo) {
            nombreRol.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El nombre debe tener menos de 30 caracteres';
        } else {
            nombreRol.classList.add('is-valid');
            nomRol = ValorNombre;
        }

    }
    function EnvioRol(event) {
        event.preventDefault();
        ValidarNombre();
        if (Permisos.length < 1) {
            mensaje_permiso.textContent = 'Debe agrgar almenos un permiso'
        } else {
            if (formulario_producto.checkValidity()) {
                Swal.fire({
                    title: '¡Éxito!',
                    text: 'Rol Registrado Correctamente',
                    timer: 2400,
                    icon: 'success',
                    showConfirmButton: false,
                }).then((result) => {
                    $.ajax({
                        url: '/RolPermisos/Editar',
                        type: 'POST',
                        data: {
                            id: idrol,
                            permisos: Permisos,
                            nomRol: nomRol
                        },

                        success: function (data) {
                            console.log(id)
                            console.log(Permisos)
                            console.log(nomRol)

                        },
                        error: function (xhr, status, error) {
                            console.log("Error en la solicitud: " + error);
                        }
                    });
                    formulario_rol.addEventListener('submit', EnvioRol);
                    formulario_rol.submit();
                });
            }

        }
    }

});