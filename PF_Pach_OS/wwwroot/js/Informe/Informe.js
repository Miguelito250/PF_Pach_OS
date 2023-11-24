
var obtenerTotalVentasUrl = '@Url.Action("ObtenerTotalVentas", "EstadisticasController")';
    $(document).ready(function () {
        $.ajax({
            url: '/Estadisticas/ObtenerVentasMensuales',
            type: 'GET',
            dataType: 'json',
            success: function (ventasMensuales) {
                console.log('Datos de ventas mensuales:', ventasMensuales);

                var labels = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];
                var datosVentas = {
                    labels: labels.slice(0, ventasMensuales.length),
                    datasets: [{
                        label: 'Ventas',
                        data: ventasMensuales,
                        backgroundColor: '#FF9200',
                        borderColor: '#FF9200',
                        borderWidth: 1
                    }]
                };

                var ctx = document.getElementById('sales-chart').getContext('2d');
                var salesChart = new Chart(ctx, {
                    type: 'line',
                    data: datosVentas,
                    options: {
                        responsive: true,
                        maintainAspectRatio: false
                    }
                });
            },
            error: function () {
                alert('Error al cargar los datos de ventas mensuales.');
            }
        });
    });

$(document).ready(function () {
    $.ajax({
        url: '/Estadisticas/ObtenerTotalVentasAnuales',
        type: 'GET',
        dataType: 'json',
        success: function (totalVentasAnuales) {
            console.log('Total de ventas anuales:', totalVentasAnuales);


            $('#totalVentasAnuales').text('$' + totalVentasAnuales.toFixed(2)); 
        },
        error: function () {
            alert('Error al cargar el total de ventas anuales.');
        }
    });
});

$(document).ready(function () {
    $.ajax({
        url: '/Estadisticas/ObtenerDiferenciaVentasMesAnterior',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log('Diferencia desde ventas desde el mes anterior:', data.diferencia);

            var diferenciaSpan = $('#diferenciaVentasMesAnterior');

            if (data.aumentoODisminucion === "aumento") {
                diferenciaSpan.text(data.diferencia.toFixed(2) + ' (↑ Aumento)');
            } else if (data.aumentoODisminucion === "disminución") {
                diferenciaSpan.text(data.diferencia.toFixed(2) + ' (↓ Disminución)');
            } else {
                diferenciaSpan.text(data.diferencia.toFixed(2) + ' (Sin cambios)');
            }
        },
        error: function () {
            alert('Error al cargar la diferencia de ventas desde el mes anterior.');
        }
    });
});
$(document).ready(function () {
    // Escucha el evento de cambio en el campo de fecha
    $("#fechaSeleccionada").change(function () {
        var fechaSeleccionada = $(this).val();

        // Realiza una solicitud AJAX para cargar las ventas del día seleccionado
        $.ajax({
            url: '/Estadisticas/ObtenerVentasPorDia',
            type: 'GET',
            data: { fecha: fechaSeleccionada },
            dataType: 'json',
            success: function (ventasDelDia) {
                console.log(ventasDelDia)
                // Muestra las ventas en la vista, por ejemplo, en un div.
                $('#ventasDelDia').html('Ventas para ' + fechaSeleccionada + ': $' + ventasDelDia.toFixed(2));
            },
            error: function () {
                alert('Error al cargar las ventas para el día seleccionado.');
            }
        });
    });
});
//$(document).ready(function () {
//    $.ajax({
//        url: '/Estadisticas/ObtenerProductosMasYMenosVendidos',
//        type: 'GET',
//        dataType: 'json',
//        success: function (data) {
//            // Llenar la tabla con los datos
//            var masVendidoRow = '<tr><td>' + data.MasVendido.Producto + '</td><td>' + data.MasVendido.Precio + '</td><td>' + data.MasVendido.CantVendida + '</td><td>Más Vendido</td></tr>';
//            var menosVendidoRow = '<tr><td>' + data.MenosVendido.Producto + '</td><td>' + data.MenosVendido.Precio + '</td><td>' + data.MenosVendido.CantVendida + '</td><td>Menos Vendido</td></tr>';

//            // Agregar las filas a la tabla
//            $('#tablaProductos tbody').append(masVendidoRow);
//            $('#tablaProductos tbody').append(menosVendidoRow);
//        },
//        error: function () {
//            alert('Error al cargar los datos de productos más y menos vendidos.');
//        }
//    });
//});
document.getElementById('generarInforme').addEventListener('click', function () {
    var tipoInforme = document.getElementById('tipoInforme').value;
    console.log('Tipo de Informe: ' + tipoInforme);

    var fechaSeleccionada = "";

    if (tipoInforme === "mensual") {
        var mesSeleccionado = document.getElementById('mes').value;
        var anioSeleccionado = document.getElementById('anio').value;
        fechaSeleccionada = anioSeleccionado + '-' + mesSeleccionado;
    } else if (tipoInforme === "anual") {
        var anioSeleccionado = document.getElementById('anio').value;
        fechaSeleccionada = anioSeleccionado;
    }

    console.log('Fecha seleccionada: ' + fechaSeleccionada);

    // Realiza una solicitud AJAX para obtener los datos de ventas
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/Estadisticas/ObtenerVentas?fechaSeleccionada=' + fechaSeleccionada + '&tipoInforme=' + tipoInforme, true);
    xhr.responseType = 'json';

    xhr.onload = function () {
        if (xhr.status === 200) {
            var ventasDelInforme = xhr.response;
            console.log('Ventas del informe:');
            console.log(ventasDelInforme);
            var xhrPdf = new XMLHttpRequest();
            xhrPdf.open('GET', '/Estadisticas/ObtenerVentas?fechaSeleccionada=' + fechaSeleccionada + '&tipoInforme=' + tipoInforme, true);
            xhrPdf.responseType = 'blob';

            xhrPdf.onload = function () {
                if (xhrPdf.status === 200) {
                    var blob = new Blob([xhrPdf.response], { type: 'application/pdf' });
                    var link = document.createElement('a');
                    link.href = window.URL.createObjectURL(blob);
                    link.download = 'InformeVentas.pdf';
                    link.click();
                } else {
                    alert('Error al generar el informe.');
                }
            };

            xhrPdf.send();
        } else if (xhr.status === 400) {
            alert('No se encontraron ventas en el rango seleccionado');
        }
        else {
            alert('Error al obtener los datos de ventas. Seleccione una fecha valida');
        }
    };

    xhr.send();
});




// Escucha el evento "change" del campo de entrada de fecha
document.getElementById('fechaSeleccionada').addEventListener('change', function () {
    // Obten la fecha seleccionada (asegúrate de que tengas la fecha en el formato adecuado)
    var fechaSeleccionada = document.getElementById('fechaSeleccionada').value;

    // Realiza una solicitud AJAX para obtener los datos
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/Estadisticas/ObtenerVentas?fechaSeleccionada=' + fechaSeleccionada, true);
    xhr.responseType = 'json'; // Indica que esperas una respuesta JSON

    xhr.onload = function () {
        if (xhr.status === 200) {
            var data = xhr.response;
            console.log('Total de Ventas: ' + data.TotalVentas);

            // Imprime las ventas individuales
            data.VentasIndividuales.forEach(function (venta) {
                console.log('ID Venta: ' + venta.IdVenta);
                console.log('Fecha Venta: ' + venta.FechaVenta);
                console.log('Monto Total: ' + venta.TotalVenta);
            });
        } else {
            console.log('Error al obtener los datos de ventas.');
        }
    };

    xhr.send();
});






