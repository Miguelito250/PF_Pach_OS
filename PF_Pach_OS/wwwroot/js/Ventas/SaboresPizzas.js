$(document).ready(function () {
    //Clase de Detalle de venta para pasar al servidor 
    class DetalleVenta {
        constructor() {
            this.IdVenta = null;
            this.IdProducto = null;
            this.CantVendida = null;
        }
    };

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

    const url = new URL(window.location.href);
    const parametros = new URLSearchParams(url.search);
    const idVenta = parametros.get('IdVenta');

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
        const producto = document.getElementById("ProductoEscoger").value;
        const cantidadVender = document.getElementById("CantVendida").value;

        let detalleVenta = new DetalleVenta();

        detalleVenta.IdVenta = idVenta;
        detalleVenta.IdProducto = producto;
        detalleVenta.CantVendida = cantidadVender

        var saboresSeleccionados = [];
        $('input[name="saborPizza"]:checked').each(function () {

            saboresSeleccionados.push($(this).val());
        });


        $.ajax({
            url: "/Ventas/ConfirmarSabores",
            type: "POST",
            data: { sabores: saboresSeleccionados, detalleVenta: detalleVenta },
            success: function (data) {
                console.log("Todo fue bien ")
            }, error: function (xhr, status, error) {
                // Esta función se ejecutará si hay un error en la solicitud.
                console.log("Error en la solicitud:"+ error);
            }
        });

    });
});