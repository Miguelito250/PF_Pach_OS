var idPizza = 1;

$(document).ready(function () {
    var tamano = $('#Producto_IdCategoria').val();
    if (tamano == idPizza) {
        console.log("1")
        MostrarCampoOculto();
    }
    function MostrarCampoOculto() {
        var campoOculto = document.getElementById('Tamano_Oculto');
        campoOculto.classList.remove('d-none')
    }
})
