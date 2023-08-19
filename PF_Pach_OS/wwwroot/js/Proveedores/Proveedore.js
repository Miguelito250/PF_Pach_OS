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
    var idProveedor = $(this).data("id");

    // Realizar aqu� la l�gica correspondiente para deshabilitar el proveedor
    // Puedes enviar una solicitud AJAX al servidor para deshabilitar el proveedor

    // Por ejemplo, si est�s usando jQuery y una ruta de controlador "DisableProveedor" para deshabilitar:
    $.post('/Controllers/ProveedoresController/' + idProveedor, function (data) {
        // L�gica para manejar la respuesta del servidor
        if (data.success) {
            // Actualizar la vista si es necesario, por ejemplo, cambiando el estado "Deshabilitado"
            // Tambi�n puedes cerrar el modal si lo deseas
            location.reload(); // Ejemplo: recargar la p�gina
        } else {
            // Manejar el caso de error si es necesario
        }
    });
});