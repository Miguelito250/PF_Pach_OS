$(document).ready(function () {
    function InsertarTitulo() {
        var titulo = "Escoger Sabores"
        var modal_header = document.getElementById("titulo-modal")
        tituloModal = titulo
        modal_header.innerHTML = tituloModal;
    }


    const consultarTamano = async () => {
        try {
            productoValor = producto.value;
            const url = "https://localhost:7229/DetalleVentas/ConsultarTamano?IdProducto=" + productoValor;
            return axios.get(url)
        } catch (error) {
            console.error(error);
        }
    }





    InsertarTitulo()

    tamano.addEventListener('change', async function () {
        var maximoSabor = await consultarTamano()

    })

    var tamano = document.getElementById("IdProducto")


    $("#btnEnviar").on("click", function () {
        var saboresSeleccionados = [];

        $('input[name="saborPizza"]:checked').each(function () {
            saboresSeleccionados.push($(this).val());
        });

        console.log(saboresSeleccionados)

        $.ajax({
            url: "/Ventas/ConfirmarSabores",
            type: "POST",
            data: { sabores: saboresSeleccionados },

        });

    });
});