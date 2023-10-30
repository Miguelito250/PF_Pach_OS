var Permisos = [];
var nomRol;
var idrol;

$(document).ready(function () {

    nomRol = $("#rol_NomRol").val();
    $('#rol_NomRol').on('input', function () {
        nomRol = $('#rol_NomRol').val();
    });
    
    idrol = $("#idrol").val();
    

    $("#btnEnviar").on("click", function () {
        $('input[name="ValoresSeleccionados"]:checked').each(function () {
            Permisos.push($(this).val());
        });
        console.log(idrol);
        console.log(nomRol)
        console.log(Permisos)

    });
    
   
    document.getElementById("formulario1").addEventListener("submit", function (event) {
     
        $.ajax({
            url: '/RolPermisos/Editar',
            type: 'POST',
            data: {
                id: idrol,
                permisos: Permisos,
                nomRol: nomRol
            },

            success: function (data) {
                console.log(id)
                console.log(Permisos)
                console.log(nomRol)

            },
            error: function (xhr, status, error) {
                console.log("Error en la solicitud: " + error);
            }
        });
    });
});

