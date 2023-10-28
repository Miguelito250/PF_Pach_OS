
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
                        backgroundColor: '#007bff',
                        borderColor: '#007bff',
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
            console.log('Diferencia de ventas desde el mes anterior:', data.diferencia);

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
$(document).ready(function () {
    $.ajax({
        url: '/Estadisticas/ObtenerProductosMasYMenosVendidos',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var ctx = document.getElementById('productosMasYMenosVendidos').getContext('2d');
            var chart = new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['Más Vendido', 'Menos Vendido'],
                    datasets: [{
                        data: [data.MasVendido.TotalVendido, data.MenosVendido.TotalVendido],
                        backgroundColor: ['rgba(54, 162, 235, 0.6)', 'rgba(255, 99, 132, 0.6)']
                    }]
                }
            });
        },
        error: function () {
            alert('Error al cargar los datos de productos más y menos vendidos.');
        }
    });
});