$("#btnNuevo").click(function (eve) {
    console.log("Crear");
    $("#modal-content").load("/Proveedores/Create");
});

$(".btnEditar").click(function (eve) {
    console.log("Boton Editar");
    $("#modal-content").load("/Proveedores/Edit/" + $(this).data("id"));
});
$("#btnDetails").click(function (eve) {
    $("#modal-content").load("/Proveedores/Details/" + $(this).data("id"));
});
$("#btnDelete").click(function (eve) {
    var idProveedor = $(this).data("id");

    $.post('/Controllers/ProveedoresController/' + idProveedor, function (data) {
        if (data.success) {
            location.reload(); 
        } else {
        }
    });
});
$(document).ready(function () {
    $(".btnEditar").click(function () {
        var idProveedor = $(this).data("id");
        $("#modal-content").load("/Proveedores/Edit/" + idProveedor);
        $('#myModal').modal('show');
    });
});
document.getElementById("btnNuevo").addEventListener("click", function () {
    $('#myModal').modal('show');
    $('#myModal').on('hidden.bs.modal', function () {
        location.reload(); 
    });
});
function abrirModalEdicion() {
    // Cambiar el t�tulo de la modal a "Editar Proveedor"
    document.getElementById('modal-title').innerText = 'Editar Proveedor';

    // Resto del c�digo para abrir la modal...
}

// C�digo para abrir la modal de creaci�n
function abrirModalCreacion() {
    // Cambiar el t�tulo de la modal a "Crear Proveedor"
    document.getElementById('modal-title').innerText = 'Crear Proveedor';

    // Resto del c�digo para abrir la modal...
}