$(document).on("click", "#DetalleUsuario", function () {

    var modalBody = $("#modal-body");
    var idRol = $(this).data("id");
    var url = "/AspNetUsers/Detalles?id=" + idRol;
    modalBody.empty();
    modalBody.load(url);
    


});