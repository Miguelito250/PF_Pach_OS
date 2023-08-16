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

    // Realizar aquí la lógica correspondiente para deshabilitar el proveedor
    // Puedes enviar una solicitud AJAX al servidor para deshabilitar el proveedor

    // Por ejemplo, si estás usando jQuery y una ruta de controlador "DisableProveedor" para deshabilitar:
    $.post('/Controllers/ProveedoresController/' + idProveedor, function (data) {
        // Lógica para manejar la respuesta del servidor
        if (data.success) {
            // Actualizar la vista si es necesario, por ejemplo, cambiando el estado "Deshabilitado"
            // También puedes cerrar el modal si lo deseas
            location.reload(); // Ejemplo: recargar la página
        } else {
            // Manejar el caso de error si es necesario
        }
    });
});