document.addEventListener('DOMContentLoaded', function () {

    var tabla = document.getElementById("Datos");

    // Get all the table rows
    var filas = Array.from(tabla.getElementsByTagName("tr"));

    // Reverse the array of rows
    filas.reverse();

    // Append the reversed rows back to the table
    filas.forEach(function (row) {
        tabla.appendChild(row);
    });

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
                var totalCompra = $(this).find('td:eq(3)').text().trim().toLowerCase(); // Agregado
                var empleado = $(this).find('td:eq(4)').text().trim().toLowerCase(); // Agregado

                if (
                    purchaseDate.includes(searchText) ||
                    provider.includes(searchText) ||
                    employee.includes(searchText) ||
                    totalCompra.includes(searchText) ||  // Agregado
                    empleado.includes(searchText)  // Agregado
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
    });
}); // Aquí se agrega el paréntesis de cierre
