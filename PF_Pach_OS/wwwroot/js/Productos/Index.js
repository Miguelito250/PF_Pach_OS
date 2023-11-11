$(document).ready(function () {
    var searchInput = $('#searchInput');

    // Manejar el evento de cambio en el campo de búsqueda mientras se escribe
    $('#searchInput').on('input', function () {
        var searchText = $(this).val().trim().toLowerCase();

        // Filtrar las filas de la tabla que coincidan con la búsqueda
        $('tbody tr').each(function () {
            var purchaseDate = $(this).find('td:eq(1)').text().trim().toLowerCase();
            var provider = $(this).find('td:eq(0)').text().trim().toLowerCase();
            if (
                purchaseDate.includes(searchText) ||
                provider.includes(searchText)
            ) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });
    // Restablecer el placeholder cuando se borre el contenido o se pierda el foco
    searchInput.on('blur', function () {
        if ($(this).val().trim() === '') {
            $(this).addClass('empty');
        }
    });

    searchInput.on('focus', function () {
        $(this).removeClass('empty');
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
            title: ' Producto Deshabilitado'
        }).then(function () {
            // Envía el formulario después de que finalice la alerta
            window.location.href = enlace.attr("href");
        });;
    });
    $(document).on("click", "#Habilitar", function (event) {
        event.preventDefault();
        var enlace = $(this);
        Toast.fire({
            icon: 'success',
            title: ' Producto Habilitado'
        }).then(function () {
            // Envía el formulario después de que finalice la alerta
            window.location.href = enlace.attr("href");
        });;
    });
});

   