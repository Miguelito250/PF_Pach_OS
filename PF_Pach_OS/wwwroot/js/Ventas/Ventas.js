//------------------------------Miguel 24/10/2023------------------------------
const boton_domicilio = document.getElementById('domicilio')
const boton_pago = document.getElementById('pago')
const producto = document.getElementById('Item1_IdProducto')
const mesa = document.getElementById("Mesa")

var subtotal = document.getElementById("totalVenta-input").value;

InsertarFecha();
InsertarTotal(subtotal);
OcultarDomicilio();

$(producto).select2({
    theme: "bootstrap-5",
    width: $(this).data('width') ? $(this).data('width') : $(this).hasClass('w-100') ? '100%' : 'style',
    placeholder: $(this).data('placeholder'),
});
$('#Item1_IdProducto').on('change', function () {
    var Valor = $(this).val();
    console.log(Valor);

    if (Valor <= 4) {
        let tamanoPizza = Valor;
        var miModal = new bootstrap.Modal(document.getElementById('exampleModal'), {
            backdrop: 'static',
            keyboard: false
        });
        $('#exampleModal').on('shown.bs.modal', function () {
            var modalBody = $("#modal-body");
            var url = "/Ventas/SaboresPizza?tamanoPizza=" + tamanoPizza;
            modalBody.empty();
            modalBody.load(url);
        });

        miModal.show();
    }
});

$(document).ready(function () {
    $('.dropdown-toggle').click(function () {
        let detalleVentaId = $(this).attr('data-iddetalleVenta');
        let dropdownMenu = $(this).next('.dropdown-menu');

        if (dropdownMenu.is(':hidden')) {
            $.ajax({
                type: "GET",
                url: "/Ventas/DetallesSabores",
                data: { idDetalleVenta: detalleVentaId },
                success: function (data) {
                    dropdownMenu.html(data);
                    dropdownMenu.show();
                }
            });
        } else {
            dropdownMenu.hide();
        }
    });
});

boton_domicilio.addEventListener('input', function () {
    SumarDomicilio(subtotal)
})
boton_pago.addEventListener('input', CalcularCambio)
mesa.addEventListener("input", OcultarDomicilio)

//Función para sumar en tiempo real si el domicilio tiene algun valor
function SumarDomicilio(subtotalProducto) {
    var pagoDomicilio = document.getElementById('domicilio').value;
    if (isNaN(pagoDomicilio) || pagoDomicilio === "" || pagoDomicilio < 0) {
        boton_domicilio.value = 0;
        totalVenta = parseFloat(subtotalProducto);
    } else {
        pagoDomicilio = parseFloat(pagoDomicilio);
        totalVenta = parseFloat(subtotalProducto) + pagoDomicilio;
    }
    InsertarTotal(totalVenta);
}

//Función para insertar el total en la factura de la venta con su formato en pesos
function InsertarTotal(totalVenta) {
    var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });
    var totalVentaFormateado = formatoColombiano.format(totalVenta);
    totalVentaFormateado = totalVentaFormateado.slice(0, -3);

    document.getElementById("totalVenta").innerHTML = totalVentaFormateado;
    document.getElementById("totalVenta-input").value = totalVenta;

}

//Función para calcular cuanto se le devolverá al cliente
function CalcularCambio() {
    let totalVenta = document.getElementById('totalVenta-input').value;
    var pago = document.getElementById('pago').value;
    let cambio;
    if (pago > totalVenta) {
        cambio = pago - totalVenta;
    } else {
        cambio = 0;
    }

    // Formatear el valor de cambio a pesos colombianos
    var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });
    var cambioFormateado = formatoColombiano.format(cambio);
    cambioFormateado = cambioFormateado.slice(0, -3);

    InsertarCambio(cambioFormateado);
}

//Función para insertar el valor del cambio en la parte en la que se mostrará
function InsertarCambio(cambio) {
    document.getElementById("texto-cambio").innerHTML = cambio;
}

//Función para para insertar la fecha del dia de la venta en el input y en la parte superior de la factura
function InsertarFecha() {
    var labelFecha = document.getElementById('fecha-hora');
    var inputFecha = document.getElementById('fecha-input');
    var fecha = new Date();
    var fechaHora = fecha.toLocaleString();

    labelFecha.innerHTML = fechaHora;
    inputFecha.value = fechaHora;
}

//Función para ocultar el campo de domicilio en caso de que la mesa sea diferente de 'General'
function OcultarDomicilio() {
    let divDomicilio = document.getElementById("campo-domicilio")
    if (mesa.value == "General") {
        divDomicilio.style.display = "";
    } else {
        divDomicilio.style.display = "none";
        boton_domicilio.value = 0
    }
}