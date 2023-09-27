console.log("toy dentro")

function CalcularCambio() {
    let totalVenta = document.getElementById('totalVenta').textContent;
    var pago = document.getElementById('pago').textContent;

    var cambio = pago - totalVenta
    InsertarCambio(cambio)
}

function InsertarCambio(cambio) {
    document.getElementById("texto-cambio").innerHTML = cambio;
}

function InsertarId() {
    var idVenta = document.getElementById("IdVenta").textContent;
    var modal_header = document.getElementById("titulo-modal")
    tituloModal = "Registro de Venta Nro: " + idVenta;
    modal_header.innerHTML = tituloModal;
}

CalcularCambio();
InsertarId();