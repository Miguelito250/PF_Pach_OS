$(document).ready(function () {
    //Funcion para insertar textos en etiquetas de esta misma pagina ya sean a inputs o a textos
    function InsertarTextos(valorInsertar, lugarCargar, etiqueta) {
        let valor = valorInsertar
        let idEtiqueta = document.getElementById(lugarCargar)
        if (etiqueta === "texto") {
            idEtiqueta.innerHTML = valor;
        } else if (etiqueta == "numerico") {
            idEtiqueta.value = valor
        } else {
            console.log("No se especifico una etiqueta valida")
        }
    }

    //Funcion para consultar el tamaño a vender en la personalizacion de las pizzas
    const ConsultarTamano = async (productoTamano) => {
        var productoSeleccionado = $(productoTamano).val();
        return new Promise((resolve, reject) => {
            $.ajax({
                url: '/DetalleVentas/ConsultarMaximoSabores',
                type: 'GET',
                data: { IdProducto: productoSeleccionado },
                success: function (respuesta) {
                    resolve(respuesta);
                },
                error: function (xhr, status, error) {
                    reject(error);
                }
            });
        });
    }

    //Funcion para limitar la cantidad de sabores que puede vender segun el tamaño de la pizza
    function LimitarSabores() {
        checkboxes.forEach(checkbox => {
            checkbox.addEventListener("change", function () {
                const checkboxesSeleccionados = Array.from(checkboxes).filter(cb => cb.checked);

                if (checkboxesSeleccionados.length >= maximoSaboresPermitidos.value) {
                    checkboxes.forEach(cb => {
                        if (!cb.checked) {
                            cb.disabled = true;
                        }
                    });
                } else {
                    checkboxes.forEach(cb => {
                        cb.disabled = false;
                    });
                }
            });
        });
    }

    const maximoSaboresPermitidos = document.getElementById("maximoSabores");
    const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    var productoTamano = document.getElementById("ProductoEscoger")

    InsertarTextos("Escoger Sabores", "titulo-modal", "texto")

    //Escuchador de eventos para ir cambiando el valor de los sabores maximos
    productoTamano.addEventListener("change", async function () {
        var maximoSaboresConsulta = await ConsultarTamano(productoTamano);
        let maximoSaboresPermitidos = maximoSaboresConsulta.maximoSabores;
        InsertarTextos(maximoSaboresPermitidos, "maximoSabores", "numerico");

        checkboxes.forEach(checkbox => {
            checkbox.checked = false;
            checkbox.disabled = false; 
        });
    })

    LimitarSabores()

    $("#btnEnviar").on("click", function () {
        var saboresSeleccionados = [];
        $('input[name="saborPizza"]:checked').each(function () {

            saboresSeleccionados.push($(this).val());
        });

        console.log(saboresSeleccionados)

        $.ajax({
            url: "/Ventas/ConfirmarSabores",
            type: "POST",
            data: { sabores: saboresSeleccionados }
        });

    });
});