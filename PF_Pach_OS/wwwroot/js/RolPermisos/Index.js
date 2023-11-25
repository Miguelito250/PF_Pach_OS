
var strongElement = document.getElementById('titulo-modal').getElementsByTagName('strong')[0];
strongElement.textContent = 'Texto de ejemplo';
var recargar = true;
$(document).on("click", "#Crear-rol", function () {
    strongElement.textContent = 'Crear Rol';
    var modalBody = $("#modal-body");
    var url = "/RolPermisos/Crear";
    modalBody.empty();
    modalBody.load(url);
    recargar = false;


});
$(document).on("click", "#Detalle-rol", function () {
    strongElement.textContent = 'Detalles Rol';
    var modalBody = $("#modal-body");
    var idRol = $(this).data("id");
    var url = "/RolPermisos/Detalles?id=" + idRol;
    modalBody.empty();
    modalBody.load(url);

    recargar = false;


});
$(document).on("click", "#Editar-rol", function () {
    strongElement.textContent = 'Editar Rol';

    var modalBody = $("#modal-body");

    var idRol = $(this).data("id");
    var url = "/RolPermisos/Editar?id=" + idRol;
    modalBody.empty();
    modalBody.load(url);
    recargar = false;


});
if (!recargar) {
    for (var i = 0; i < 6; i++) {
        window.location.reload();
    }
}
document.addEventListener('DOMContentLoaded', function () {

    

    $(document).ready(function () {
        $('.deshabilitar, .habilitar').each(function () {
            $(this).on('click', function (event) {
                event.preventDefault();

                var title = '';
                var text = '';
                var confirmButtonText = '';

                if ($(this).hasClass('deshabilitar')) {
                    title = '¿Estás seguro de desactivar este rol?';
                    text = 'Si desactivas este rol, se desactivarán todos los usuarios asociados a este rol.';
                    confirmButtonText = 'Sí, desactivar';
                } else if ($(this).hasClass('habilitar')) {
                    title = '¿Estás seguro de activar este rol?';
                    text = 'Si activas este rol, se activarán todos los usuarios asociados a este rol.';
                    confirmButtonText = 'Sí, activar';
                }

                Swal.fire({
                    title: title,
                    text: text,
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: confirmButtonText,
                    cancelButtonText: 'No, volver'
                }).then((result) => {
                    if (result.isConfirmed) {
                        window.location.href = $(this).attr('href');
                    }
                });
            });
        });
    });


});



