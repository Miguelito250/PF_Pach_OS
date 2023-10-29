document.addEventListener('DOMContentLoaded', function () {

    var recargar = true;
    $(document).on("click", "#Crear-rol", function () {

        var modalBody = $("#modal-body");
        var url = "/RolPermisos/Crear";
        modalBody.empty();
        modalBody.load(url);
        recargar = false;


    });
    $(document).on("click", "#Editar-rol", function () {

        var modalBody = $("#modal-body");

        var idRol = $(this).data("id");
        var url = "/RolPermisos/Editar?id=" + idRol;
        modalBody.empty();
        modalBody.load(url);
        recargar = false;


    });
    if (!recargar) {
        for (var i = 0; i < 2; i++) {
            window.location.reload();
        }
    }

    window.addEventListener('load', function () {
        const deshabilitar = document.querySelector('.deshabilitar');
       




        deshabilitar.addEventListener('click', function (event) {
            event.preventDefault();
            console.log('Clic detectado en la etiqueta a');

            Swal.fire({
                title: '¿Estás seguro?',
                text: 'Si desactivas este rol se desactivaran todos los usuarios asocidos a este rol',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sí, desactivar',
                cancelButtonText: 'No, volver'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Si el usuario confirma, redirigir a la función "Delete" en el controlador de ventas
                    window.location.href = deshabilitar.getAttribute('href');
                }
            });
        });

        const habilitar = document.querySelector('.habilitar');


        habilitar.addEventListener('click', function (event) {
            event.preventDefault();
            console.log('Clic detectado en la etiqueta a');

            Swal.fire({
                title: '¿Estás seguro?',
                text: 'Si activa este rol se ativaran todos los usuarios asocidos a este rol',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sí, activar',
                cancelButtonText: 'No'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Si el usuario confirma, redirigir a la función "Delete" en el controlador de ventas
                    window.location.href = habilitar.getAttribute('href');
                }
            });
        });

    });
    
    
});



