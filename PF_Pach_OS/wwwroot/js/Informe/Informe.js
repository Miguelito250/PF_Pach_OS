
$(document).ready(function () {
    //--Datos mensuales--
    var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });
    let ventaMesActual = document.getElementById('input-totalVentas').value
    let compraMesActual = document.getElementById('input-totalCompras').value
    let resultadoResta

    if (ventaMesActual > compraMesActual) {
        icono.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="green" class="bi bi-graph-up-arrow mr-2" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M0 0h1v15h15v1H0zm10 3.5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 .5.5v4a.5.5 0 0 1-1 0V4.9l-3.613 4.417a.5.5 0 0 1-.74.037L7.06 6.767l-3.656 5.027a.5.5 0 0 1-.808-.588l4-5.5a.5.5 0 0 1 .758-.06l2.609 2.61L13.445 4H10.5a.5.5 0 0 1-.5-.5"/></svg>';
        resultadoResta = ventaMesActual - compraMesActual
        $('#Diferencias').addClass('text-success');
        $('#Diferencias').removeClass('text-danger');
    } else {
        icono.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="red" class="bi bi-graph-down-arrow" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M0 0h1v15h15v1H0zm10 11.5a.5.5 0 0 0 .5.5h4a.5.5 0 0 0 .5-.5v-4a.5.5 0 0 0-1 0v2.6l-3.613-4.417a.5.5 0 0 0-.74-.037L7.06 8.233 3.404 3.206a.5.5 0 0 0-.808.588l4 5.5a.5.5 0 0 0 .758.06l2.609-2.61L13.445 11H10.5a.5.5 0 0 0-.5.5"/></svg>';
        resultadoResta = compraMesActual - ventaMesActual
        $('#Diferencias').addClass('text-danger');
        $('#Diferencias').removeClass('text-success');
    }
    var cambioFormateadoD = formatoColombiano.format(resultadoResta);
    cambioFormateadoD = cambioFormateadoD.slice(0, -3);
    $('#Diferencias').text('' + cambioFormateadoD);

    cargarProductosMasVendidos();

    document.getElementById('tipoInforme').addEventListener('change', function () {
        var divMensual = document.querySelector('.div-mes');
        var divAnual = document.getElementById('divAnios');

        if (this.value === 'mensual') {
            divMensual.classList.remove('d-none');
            divAnual.classList.add('d-none');
        } else if (this.value === 'anual') {
            divMensual.classList.add('d-none');
            divAnual.classList.remove('d-none');
        }
    });

    //--Grafica--
    $.ajax({
        url: '/Estadisticas/ObtenerVentasMensuales',
        type: 'GET',
        dataType: 'json',
        success: function (ventasMensuales) {
            $.ajax({
                url: '/Estadisticas/ObtenerComprasMensuales',
                type: 'GET',
                dataType: 'json',
                success: function (comprasMensuales) {
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
                            backgroundColor: '#ff6361',
                            borderColor: '#ff6361',
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

    $.ajax({
        url: '/Estadisticas/ObtenerDiferenciaVentasMesAnterior',
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            var diferenciaSpan = $('#diferenciaVentasMesAnterior');
            var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });

            var diferenciaFormateada = formatoColombiano.format(data.diferencia);

            if (data.aumentoODisminucion === "aumento") {
                diferenciaSpan.addClass('text-success').removeClass('text-danger');
            } else if (data.aumentoODisminucion === "disminución") {
                diferenciaSpan.addClass('text-danger').removeClass('text-success');
            } else {
                diferenciaSpan.removeClass('text-success text-danger');
            }

            diferenciaSpan.text(diferenciaFormateada + ' (' + data.aumentoODisminucion.charAt(0).toUpperCase() + data.aumentoODisminucion.slice(1) + ')');
        },
        error: function () {
            alert('Error al cargar la diferencia de ventas desde el mes anterior.');
        }
    });

    $.ajax({
        url: '/Estadisticas/ObtenerTotalVentasAnuales',
        type: 'GET',
        dataType: 'json',
        success: function (totalVentasAnuales) {
            var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });
            var cambioFormateado = formatoColombiano.format(totalVentasAnuales);
            cambioFormateado = cambioFormateado.slice(0, -3);
            $('#totalVentasAnuales').text('' + cambioFormateado);
            $('#celdainsumos').text('' + cambioFormateado);
            $('#input-totalVentas').val(totalVentasAnuales);

            $.ajax({
                url: '/Estadisticas/ObtenerTotalComprasAnuales',
                type: 'GET',
                dataType: 'json',
                success: function (totalComprasAnuales) {
                    var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });
                    var cambioFormateado = formatoColombiano.format(totalComprasAnuales);
                    cambioFormateado = cambioFormateado.slice(0, -3);
                    $('#totalComprasAnuales').text('' + cambioFormateado);
                    var diferencia = totalVentasAnuales - totalComprasAnuales;
                    var diferenciaFormateada = formatoColombiano.format(diferencia);
                    diferenciaFormateada = diferenciaFormateada.slice(0, -3);
                    $('#diferenciaVentasCompras').text('' + diferenciaFormateada);
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
    $("#fechaSeleccionada").change(function () {
        var fechaSeleccionada = $(this).val();

        $.ajax({
            url: '/Estadisticas/ObtenerVentasPorDia',
            type: 'GET',
            data: { fecha: fechaSeleccionada },
            dataType: 'json',
            success: function (ventasDelDia) {
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

    var fechaSeleccionada = "";

    if (tipoInforme === "mensual") {
        var mesSeleccionado = document.getElementById('mes').value;
        fechaSeleccionada = mesSeleccionado;
    } else if (tipoInforme === "anual") {
        console.log("tambien ingreso al anual")
        var anioSeleccionado = document.getElementById('anio').value;
        fechaSeleccionada = anioSeleccionado;
    }

    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/Estadisticas/ObtenerVentas?fechaSeleccionada=' + fechaSeleccionada + '&tipoInforme=' + tipoInforme, true);
    xhr.responseType = 'json';

    xhr.onload = function () {
        if (xhr.status === 200) {
            var ventasDelInforme = xhr.response;

            var xhrPdf = new XMLHttpRequest();
            xhrPdf.open('GET', '/Estadisticas/ObtenerVentas?fechaSeleccionada=' + fechaSeleccionada + '&tipoInforme=' + tipoInforme, true);
            xhrPdf.responseType = 'blob';

            xhrPdf.onload = function () {
                if (xhrPdf.status === 200) {
                    var blob = new Blob([xhrPdf.response], { type: 'application/pdf' });
                    var link = document.createElement('a');
                    var nombreArchivo = '';
                    if (tipoInforme === 'mensual') {
                        var nombreMes = new Date(fechaSeleccionada + '-1').toLocaleDateString('es-ES', { month: 'long' });
                        nombreArchivo = 'Informe de ' + nombreMes + ' ' + new Date(fechaSeleccionada + '-1').getFullYear() + '.pdf';
                    } else if (tipoInforme === 'anual') {
                        nombreArchivo = 'Informe Anual ' + fechaSeleccionada + '.pdf';
                    }
                    link.href = window.URL.createObjectURL(blob);
                    link.download = nombreArchivo;
                    link.click();
                } else {
                    swal('Error', 'Error al generar el informe.', 'error');
                }
            };

            xhrPdf.send();
        } else if (xhr.status === 400) {
        }
        else {
        }
    };

    xhr.send();
});
//Obtenemos las ventas anuales

//Descargamos los informes
$(document).ready(function () {
    $('#generarInforme').on('click', function () {
        var tipoInforme = $('#tipoInforme').val();

        var fechaSeleccionada;
        if (tipoInforme === 'mensual') {
            var mes = $('#mes').val();
            fechaSeleccionada = mes;
        } else {
            fechaSeleccionada = $('#anio').val();
        }
        $.ajax({
            url: '/Estadisticas/ObtenerVentas',
            type: 'GET',
            data: { fechaSeleccionada: fechaSeleccionada, tipoInforme: tipoInforme },
            success: function (data) {
                if (data.error) {
                    mostrarErrorSweetAlert();
                } else {
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

function cargarProductosMasVendidos() {
    $.ajax({
        url: '/Estadisticas/ObtenerProductosMasVendidos',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            let totalVentas = $('#input-totalVentas').val()
            var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });

            for (i = 0; i < data.length; i++) {
                let totalVendido = data[i].totalVendido;
                let totalVendidoFormateado = formatoColombiano.format(totalVendido)
                totalVendidoFormateado = totalVendidoFormateado.slice(0, -3)

                $('#producto' + i).text(data[i].producto);
                $('#totalProducto' + i).text(totalVendidoFormateado + ' / ' + data[i].cantidadVendida);
                $('#progreso' + i).attr('aria-valuemax', totalVentas);
                $('#progreso' + i).attr('aria-valuemin', data[i].totalVendido);
                let porcentajeVentas = (totalVendido / totalVentas) * 100;

                $('#progreso' + i + ' .progress-bar').css('width', porcentajeVentas + '%');

            }
        },
        error: function () {
            console.log('Error al cargar productos más vendidos');
        }
    });
}