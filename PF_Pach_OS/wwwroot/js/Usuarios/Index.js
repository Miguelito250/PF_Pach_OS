
﻿$('#searchInput').on('input', function () {
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

﻿$(document).on("click", "#DetalleUsuario", function () {

    var modalBody = $("#modal-body");
    var idRol = $(this).data("id");
    var url = "/AspNetUsers/Detalles?id=" + idRol;
    modalBody.empty();
    modalBody.load(url);
    


});

