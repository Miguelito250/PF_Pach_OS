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

//Filtro de estado
$(document).ready(function () {
    FiltroEstado(false);
    $('#slideThree').change(function () {
        var filtroSeleccionado = $(this).prop('checked');
        FiltroEstado(filtroSeleccionado);
    })
});

//Funciones con AJAX
$(document).on("click", "#boton-detalles", function () {
    var idVenta = $(this).data("idventa");
    var modalBody = $("#modal-body");
    var url = "/Ventas/DetallesVentas?IdVenta=" + idVenta;
    modalBody.empty();

    modalBody.load(url);
});
