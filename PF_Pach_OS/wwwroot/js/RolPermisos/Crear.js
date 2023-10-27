var Permisos = [];
var nomRol;
$(document).ready(function () {


    $('#rol_NomRol').on('input', function () {
        nomRol = $('#rol_NomRol').val();
    });


    $("#btnEnviar").on("click", function () {
        $('input[name="ValoresSeleccionados"]:checked').each(function () {
            Permisos.push($(this).val());
        });

        console.log(nomRol)
        console.log(Permisos)

    });
    document.getElementById("formulario1").addEventListener("submit", function (event) {
      
        $.ajax({
            url: '/RolPermisos/Crear',
            type: 'POST',
            data: {
                permisos: Permisos,
                nomRol: nomRol
            },

            success: function (data) {
            }
        });
    });
});



