document.addEventListener('DOMContentLoaded', function () {

    $(document).on("click", "#boton-detalles", function () {
        var IdCompra = $(this).data("idcompra");
        var modalBody = $("#modal-body");
        var url = "/Compras/DetalleCompra?IdCompra=" + IdCompra;
        modalBody.empty();

        modalBody.load(url);
    });

    $(document).ready(function () {
        // Manejar el evento de cambio en el campo de búsqueda mientras se escribe
        $('#searchInput').on('input', function () {
            var searchText = $(this).val().trim().toLowerCase();

            // Filtrar las filas de la tabla que coincidan con la búsqueda
            $('tbody tr').each(function () {
                var purchaseDate = $(this).find('td:eq(1)').text().trim().toLowerCase();
                var provider = $(this).find('td:eq(0)').text().trim().toLowerCase();
                var employee = $(this).find('td:eq(2)').text().trim().toLowerCase();
                var totalCompra = $(this).find('td:eq(3)').text().trim().toLowerCase();
                var empleado = $(this).find('td:eq(4)').text().trim().toLowerCase();

                if (
                    purchaseDate.includes(searchText) ||
                    provider.includes(searchText) ||
                    employee.includes(searchText) ||
                    totalCompra.includes(searchText) ||  
                    empleado.includes(searchText) 
                ) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            });
        });
        // Restablecer el placeholder cuando se borre el contenido o se pierda el foco
        $(document).ready(function () {
            // Restablecer el placeholder cuando se borre el contenido o se pierda el foco
            $('#searchInput').on('blur', function () {
                if ($(this).val().trim() === '') {
                    $(this).addClass('empty');
                }
            });

            $('#searchInput').on('focus', function () {
                $(this).removeClass('empty');
            });
        });
    });
});
