var totalVenta = 0;
var domicilio = document.getElementById("domicilio").value;
var pago = document.getElementById("pago").value;
var cambio = document.getElementById("cambio").value;
const tablaDetalles = document.querySelector('.tabla-detalles'); // Obtén la tabla con la clase tabla-detalles

if (tablaDetalles) {
    const filas = tablaDetalles.querySelectorAll('tbody tr'); // Obtén todas las filas dentro del tbody de la tabla

    filas.forEach(function (fila) {
        const celdas = fila.querySelectorAll('td'); // Obtén todas las celdas dentro de la fila

        const cantidad = parseInt(celdas[1].textContent); // Valor de la celda de cantidad
        const precio = parseInt(celdas[2].textContent); // Valor de la celda de precio

        const total = cantidad * precio;
        totalVenta += total;

        const celdaTotal = document.createElement('td'); // Crea una nueva celda <td>
        celdaTotal.textContent = total.toFixed(); // Establece el contenido de la celda como el total calculado
        fila.appendChild(celdaTotal); // Agrega la nueva celda a la fila
    });
}
document.getElementById("totalVenta").innerHTML = totalVenta;
var textoCambiar = document.getElementById("texto-cambio").innerHTML = cambio;

//Calcular el cambio
domicilio.addEventListener('change', (e) => {
    console.log(e)
})
