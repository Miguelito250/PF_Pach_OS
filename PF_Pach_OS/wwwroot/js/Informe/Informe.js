
$(document).ready(function () {
    $.ajax({
        url: '/Estadisticas/ObtenerVentasMensuales',
        type: 'GET',
        dataType: 'json',
        success: function (ventasMensuales) {
            console.log('Datos de ventas mensuales:', ventasMensuales);

            $.ajax({
                url: '/Estadisticas/ObtenerComprasMensuales', 
                type: 'GET',
                dataType: 'json',
                success: function (comprasMensuales) {
                    console.log('Datos de compras mensuales:', comprasMensuales);

                    var labels = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];
                    var datosVentas = {
                        labels: labels.slice(0, ventasMensuales.length),
                        datasets: [{
                            label: 'Ventas',
                            data: ventasMensuales,
                            backgroundColor: '#FF9200',
                            borderColor: '#FF9200',
                            borderWidth: 1
                        }, {
                            label: 'Compras',
                            data: comprasMensuales,
                            backgroundColor: '#FF0000', 
                            borderColor: '#FF0000',
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
                    alert('Error al cargar los datos de compras mensuales.');
                }
            });
        },
        error: function () {
            alert('Error al cargar los datos de ventas mensuales.');
        }
    });
});
//Obtenemos las compras mensuales 
$(document).ready(function () {
    $.ajax({
        url: '/Estadisticas/ObtenerComprasMensuales',
        type: 'GET',
        dataType: 'json',
        success: function (totalComprasMes) {
            console.log('Compras mensuales:', totalComprasMes);
            if (!isNaN(totalComprasMes)) {
                $('#totalCompras').text('$' + totalComprasMes.toFixed(2));
            } else {
                $('#totalCompras').text('$0.00'); 
            }
        },
        error: function () {
            alert('Error al cargar los datos de compras mensuales.');
        }
    });
});
//Funcion para obtener las diferencia del mes anterior
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
//Funcion para tener las ventas del dia
$(document).ready(function () {
    $("#fechaSeleccionada").change(function () {
        var fechaSeleccionada = $(this).val();

        $.ajax({
            url: '/Estadisticas/ObtenerVentasPorDia',
            type: 'GET',
            data: { fecha: fechaSeleccionada },
            dataType: 'json',
            success: function (ventasDelDia) {
                console.log(ventasDelDia)
                $('#ventasDelDia').html('Ventas para ' + fechaSeleccionada + ': $' + ventasDelDia.toFixed(2));
            },
            error: function () {
                alert('Error al cargar las ventas para el día seleccionado.');
            }
        });
    });
});

//FUncion para generar el informe
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
                    swal('Error', 'Error al generar el informe.', 'error');
                }
            };

            xhrPdf.send();
        } else if (xhr.status === 400) {
            /*alert('No se encontraron ventas en el rango seleccionado');*/
        }
        else {
            /*alert('Error al obtener los datos de ventas. Seleccione una fecha valida');*/
        }
    };

    xhr.send();
});

$(document).ready(function () {
    $.ajax({
        url: '/Estadisticas/ObtenerTotalVentasAnuales',
        type: 'GET',
        dataType: 'json',
        success: function (totalVentasAnuales) {
            console.log('Total de ventas anuales:', totalVentasAnuales);
            $('#totalVentasAnuales').text('$' + totalVentasAnuales.toFixed(2));
            $('#celdainsumos').text('$' + totalVentasAnuales.toFixed(2));

            $.ajax({
                url: '/Estadisticas/ObtenerTotalComprasAnuales',
                type: 'GET',
                dataType: 'json',
                success: function (totalComprasAnuales) {
                    console.log('Total de compras anuales:', totalComprasAnuales);
                    $('#totalComprasAnuales').text('$' + totalComprasAnuales.toFixed(2));
                    var diferencia = totalVentasAnuales - totalComprasAnuales;
                    console.log(diferencia)
                    $('#diferenciaVentasCompras').text('$' + diferencia.toFixed(2));
                },
                error: function () {
                    alert('Error al cargar el total de compras anuales.');
                }
            });
        },
        error: function () {
            alert('Error al cargar el total de ventas anuales.');
        }
    });
});
$(document).ready(function () {
    $('#generarInforme').on('click', function () {
        // Obtiene el tipo de informe seleccionado
        var tipoInforme = $('#tipoInforme').val();

        var fechaSeleccionada;
        if (tipoInforme === 'mensual') {
            var mes = $('#mes').val();
            var anio = $('#anio').val();
            fechaSeleccionada = anio + '-' + mes;
        } else {
            fechaSeleccionada = $('#anio').val();
        }
        console.log('Tipo de informe:', tipoInforme);
        console.log('Fecha seleccionada:', fechaSeleccionada);
        // Realiza la solicitud Ajax con la fecha seleccionada
        $.ajax({
            url: '/Estadisticas/ObtenerVentas',
            type: 'GET',
            data: { fechaSeleccionada: fechaSeleccionada, tipoInforme: tipoInforme },
            success: function (data) {
                if (data.error) {
                    // Hubo un error al generar el informe, muestra un mensaje de error
                    mostrarErrorSweetAlert();
                } else {
                    // El informe se generó con éxito, muestra una notificación SweetAlert
                    mostrarSweetAlert();
                }
            },
            error: function () {
                mostrarErrorSweetAlert();
            }
        });
    });

    function mostrarErrorSweetAlert(mensaje) {
        Swal.fire({
            title: 'Seleccione Fecha Valida, no existen ventas',
            text: mensaje,
            icon: 'error',
            timer: 2500,
            showConfirmButton: true
        });
    }

    function mostrarSweetAlert() {
        Swal.fire({
            title: 'Informe generado',
            text: 'El informe se ha generado exitosamente.',
            icon: 'success',
            timer: 2500,
            showConfirmButton: true
        });
    }
});









