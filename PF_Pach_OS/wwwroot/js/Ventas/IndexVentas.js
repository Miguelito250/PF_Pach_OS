//Funciones con AJAX
$(document).on("click", "#boton-detalles", function () {
    var idVenta = $(this).data("idventa");
    var modalBody = $("#modal-body");
    var url = "/Ventas/DetallesVentas?IdVenta=" + idVenta;
    modalBody.empty();

    modalBody.load(url);
});