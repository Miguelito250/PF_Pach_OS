$("#btnNuevo").click(function (eve) {
    $("#modal-content").load("/Proveedores/Create");
});

$("#btnEditar").click(function (eve) {
    $("#modal-content").load("/Proveedores/Edit/" + $(this).data("id"));
});
$("#btnDetails").click(function (eve) {
    $("#modal-content").load("/Proveedores/Details/" + $(this).data("id"));
});
$("#btnDelete").click(function (eve) {
    $("#modal-content").load("/Proveedores/Delete/" + $(this).data("id"));
});