function SumarDomicilio() {
    var totalSinCambio = document.getElementById('totalVenta').textContent;
    var pagoDomicilio = document.getElementById('domicilio').value;

    totalSinCambio = parseInt(totalSinCambio)
    pagoDomicilio = parseInt(pagoDomicilio)

    totalVenta = totalSinCambio + pagoDomicilio

    InsertarTotal(totalVenta)
}

function InsertarTotal(totalVenta) {
    document.getElementById("totalVenta").innerHTML = totalVenta;
    document.getElementById("totalVenta-input").value = totalVenta;

}

function CalcularCambio() {
    let totalVenta = document.getElementById('totalVenta').textContent;
    var pago = document.getElementById('pago').value;

    var cambio = pago - totalVenta 
    InsertarCambio(cambio)
}

function InsertarCambio(cambio){
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
//Scripts para la pagina de ventas

var boton_domicilio = document.getElementById('domicilio')
var boton_pago = document.getElementById('pago')

boton_domicilio.addEventListener('change', SumarDomicilio)
boton_pago.addEventListener('change', CalcularCambio)

var subtotalProducto = 0

// Obtener todas las filas de detalles
var filasDetalles = document.querySelectorAll("#detalles-ventas tr");
        
filasDetalles.forEach(function(fila) {
    var cantidad = parseInt(fila.cells[1].textContent);
    var precio = parseInt(fila.cells[2].textContent);
    var total = cantidad * precio;

    subtotalProducto += total; 
    fila.querySelector("#texto-total").textContent = total.toFixed();
});

InsertarTotal(subtotalProducto);
InsertarFecha();
