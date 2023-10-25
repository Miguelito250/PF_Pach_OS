var recargar = true;
$(document).on("click", "#Crear-rol", function () {

    var modalBody = $("#modal-body");
    var url = "/RolPermisos/Crear";
    modalBody.empty();
    modalBody.load(url);
    recargar = false;


});
$(document).on("click", "#Editar-rol", function () {

    var modalBody = $("#modal-body");

    var idRol = $(this).data("id");
    var url = "/RolPermisos/Editar?id=" + idRol;
    modalBody.empty();
    modalBody.load(url);
    recargar = false;


});
if (!recargar) {
    for (var i = 0; i < 2; i++) {
        window.location.reload();
    }
}


