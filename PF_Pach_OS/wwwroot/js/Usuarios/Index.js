$(document).ready(function () {

    $('#searchInput').on('input', function () {
        var searchText = $(this).val().trim().toLowerCase();

        // Filtrar las filas de la tabla que coincidan con la búsqueda
        $('tbody tr').each(function () {
            var nombre = $(this).find('td:eq(0)').text().trim().toLowerCase();
            var correo = $(this).find('td:eq(1)').text().trim().toLowerCase();
            var roll = $(this).find('td:eq(2)').text().trim().toLowerCase();

            if (
                nombre.includes(searchText) ||
                correo.includes(searchText) ||
                roll.includes(searchText)
            ) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });


    $(document).on("click", "#DetalleUsuario", function () {

        var modalBody = $("#modal-body");
        var idRol = $(this).data("id");

        var url = "/AspNetUsers/Detalles?id=" + idRol;
        modalBody.empty();
        modalBody.load(url);



    });
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
    $(document).on("click", "#Deshabilitar", function (event) {
        event.preventDefault();
        var enlace = $(this);
        Toast.fire({
            icon: 'success',
            title: ' Usuario Deshabilitado'
        }).then(function () {
            // Envía el formulario después de que finalice la alerta
            window.location.href = enlace.attr("href");
        });;
    });
    $(document).on("click", "#Habilitar", function (event) {
        event.preventDefault();
        var enlace = $(this);
        var idusario = $(this).data("id");
        $.ajax({
            url: '/AspNetUsers/Rol',
            type: 'GET',
            data: { idUsario: idusario },
            success: function (response) {

                if (response === false) {
                    Toast.fire({
                        icon: 'success',
                        title: ' Usuario Habilitado'
                    }).then(function () {
                        // Envía el formulario después de que finalice la alerta
                        window.location.href = enlace.attr("href");
                    });;

                } else {
                    Toast.fire({
                        icon: 'error',
                        title: 'El rol se encuentra deshabilitado'
                    }).then(function () {

                    });;

                }
            },
            error: function (xhr, status, error) {
                // Imprimir el error completo en la consola
                console.error('Error en la solicitud AJAX:', xhr, status, error);
            }
        });

    });


});



