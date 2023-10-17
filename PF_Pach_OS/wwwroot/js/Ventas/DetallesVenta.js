$(document).ready(function () {
    var tablaDetalles = $("#TablaDetalles");
    $(tablaDetalles).DataTable({
        searching: false,
        lengthChange: false,
        info: false,
        pageLength: 4,
        filters: false,
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json"
        }
    });

    // Contar las filas de la tabla
    var ContadorFilas = $('#TablaDetalles tbody tr').length;
    console.log('Número de filas en la tabla: ' + ContadorFilas);
});
