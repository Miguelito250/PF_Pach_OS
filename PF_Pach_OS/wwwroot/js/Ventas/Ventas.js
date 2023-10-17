function SumarDomicilio(subtotalProducto) {
    var pagoDomicilio = document.getElementById('domicilio').value;
    if (isNaN(pagoDomicilio) || pagoDomicilio == "" || pagoDomicilio < 0) {
        boton_domicilio.value = 0
        InsertarTotal(subtotalProducto)
    } else {
        pagoDomicilio = parseInt(pagoDomicilio)
        totalVenta = subtotalProducto + pagoDomicilio
        InsertarTotal(totalVenta)
    }
}

function InsertarTotal(totalVenta) {
    document.getElementById("totalVenta").innerHTML = totalVenta;
    document.getElementById("totalVenta-input").value = totalVenta;

}

function CalcularCambio() {
    let totalVenta = document.getElementById('totalVenta').textContent;
    var pago = document.getElementById('pago').value;

    var cambio = pago - totalVenta;

    // Formatear el valor de cambio a pesos colombianos
    var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });
    var cambioFormateado = formatoColombiano.format(cambio);

    InsertarCambio(cambioFormateado);
}


function InsertarCambio(cambio) {
    document.getElementById("texto-cambio").innerHTML = cambio;
}

function InsertarFecha() {
    var labelFecha = document.getElementById('fecha-hora');
    var inputFecha = document.getElementById('fecha-input');
    var fecha = new Date();
    var fechaHora = fecha.toLocaleString();

    labelFecha.innerHTML = fechaHora;
    inputFecha.value = fechaHora;
}

function CalcularSubtotal() {
    var subtotalProducto = 0

    var filasDetalles = document.querySelectorAll("#detalles-ventas tr");

    filasDetalles.forEach(function (fila) {
        var cantidad = parseInt(fila.cells[1].textContent);
        var precio = parseInt(fila.cells[2].textContent);
        var total = cantidad * precio;

        subtotalProducto += total;
        fila.querySelector("#texto-total").textContent = total.toFixed();
    });

    return subtotalProducto
}

const boton_domicilio = document.getElementById('domicilio')
const boton_pago = document.getElementById('pago')
const producto = document.getElementById('Item1_IdProducto')
const botonCerrar = document.getElementById("cerrar")

var subtotal = CalcularSubtotal()

InsertarTotal(subtotal);
InsertarFecha();

producto.addEventListener("change", function () {

    if (producto.value == 0) {
        var miModal = new bootstrap.Modal(document.getElementById('exampleModal'), {
            keyboard: false
        });
        $('#exampleModal').on('shown.bs.modal', function () {
            var modalBody = $("#modal-body");
            var url = "/Ventas/SaboresPizza";
            modalBody.empty();
            modalBody.load(url);
        });

        miModal.show();
    }
})

boton_domicilio.addEventListener('input', function () {
    SumarDomicilio(subtotal)
})
boton_pago.addEventListener('input', CalcularCambio)