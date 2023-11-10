$(document).ready(function () {
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
    $(document).on("submit", "#botonEstado", function (event) {
        event.preventDefault();
        Toast.fire({
            icon: 'success',
            title: ' ¡Venta Pagada!'
        }).then(function () {
            // Envía el formulario después de que finalice la alerta
            document.getElementById('botonEstado').submit();
        });;
    });

    $('#slideThree').change(function () {
        var filtroSeleccionado = $(this).prop('checked');
        FiltroEstado(filtroSeleccionado);
    })
    $(document).on("click", "#boton-detalles", function () {
        var idVenta = $(this).data("idventa");
        var modalBody = $("#modal-body");
        var url = "/DetalleVentas/DetallesVentas?IdVenta=" + idVenta;
        modalBody.empty();

        modalBody.load(url);
    });
    var searchInput = $('#searchInput');

    $('#searchInput').on('input', function () {
        var searchText = $(this).val().trim().toLowerCase();

        // Filtrar las filas de la tabla que coincidan con la búsqueda
        $('tbody tr').each(function () {
            var fecha = $(this).find('td:eq(0)').text().trim().toLowerCase();
            var metodoPago = $(this).find('td:eq(1)').text().trim().toLowerCase();
            var total = $(this).find('td:eq(2)').text().trim().toLowerCase();

            if (
                fecha.includes(searchText) ||
                metodoPago.includes(searchText) ||
                total.includes(searchText)
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

    FiltroEstado(false);

});

//Miguel 24/10/2023: Función para filtrar las ventas por el estado 'Pagada' o 'Pendinete'
function FiltroEstado(filtroSeleccionado) {
    $(document).ready(function () {


        if (filtroSeleccionado == true) {
            estado = "Pagada"
        } else {
            estado = "Pendiente"
        }

        $('tbody tr').each(function () {
            var pagada = $(this).find('.boton-Estado').data('estado');
            if (pagada == estado) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        })

    });
}