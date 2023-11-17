﻿
$(document).ready(function () {
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
});
//Obtenemos las compras mensuales 
$(document).ready(function () {
    $.ajax({
        url: '/Estadisticas/ObtenerComprasMensuales',
        type: 'GET',
        dataType: 'json',
        success: function (totalComprasMes) {

            if (!isNaN(totalComprasMes)) {
                var totalVentasFormateado = totalComprasMes.toLocaleString('es-CO', { style: 'currency', currency: 'COP' });

                $('#totalCompras').text(totalComprasFormateado);
            } else {
                $('#totalCompras').text('COP$0.00');
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
                    link.href = window.URL.createObjectURL(blob);
                    link.download = 'InformeVentas.pdf';
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
$(document).ready(function () {
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
$(document).ready(function () {
    function obtenerDatosTransferencias() {
        $.ajax({
            url: '/Estadisticas/ObtenerDatosTransferencias',
            method: 'GET',
            dataType: 'json',
            success: function (data) {
                var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });

                var transferenciasFormateadas = formatoColombiano.format(data.totalTransferencias);
                var efectivoFormateado = formatoColombiano.format(data.totalEfectivo);

                transferenciasFormateadas = transferenciasFormateadas.slice(0, -3);
                efectivoFormateado = efectivoFormateado.slice(0, -3);

                $('#totalEfectivo').text(efectivoFormateado);
                $('#totalTransferencias').text(transferenciasFormateadas);
            },
            error: function (error) {
                console.error('Error al obtener los datos de transferencias:', error);
            }
        });
    }

    obtenerDatosTransferencias();

    $('#btnEliminarVentas').click(function () {
        obtenerDatosTransferencias();
    });
});
$(document).ready(function () {
    $("#btnEliminarVentas").click(function () {

        Swal.fire({
            title: '¿Estás seguro?',
            text: '¡Esto eliminará todas las ventas?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: "/Estadisticas/EliminarTodasLasVentas",
                    success: function (response) {
                        Swal.fire({
                            title: '¡Eliminadas!',
                            text: 'Todas las ventas han sido eliminadas.',
                            icon: 'success',
                            timer: 2500,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = window.location.href;
                        });
                    },
                    error: function (error) {
                        console.log("Error en la llamada AJAX");
                        console.log(error);
                        Swal.fire({
                            title: 'Error',
                            text: 'Hubo un problema al eliminar las ventas.',
                            icon: 'error',
                            timer: 2500,
                            showConfirmButton: false
                        }).then(() => {
                            window.location.href = window.location.href;
                        });
                    }
                });
            }
        });
    });
});

$(document).ready(function () {
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
});


function cargarProductosMasVendidos() {
    $.ajax({
        url: '/Estadisticas/ObtenerProductosMasVendidos',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            let totalVentas = $('#input-totalVentas').val()
            var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });

            for (i = 0; i < 4; i++) {
                let totalVendido = data[i].totalVendido;
                let totalVendidoFormateado = formatoColombiano.format(totalVendido)
                totalVendidoFormateado = totalVendidoFormateado.slice(0, -3)

                $('#producto' + i).text(data[i].producto);
                $('#totalProducto' + i).text(totalVendidoFormateado + ' / ' + data[i].cantidadVendida);
                $('#progreso' + i).attr('max', totalVentas);
                $('#progreso' + i).attr('value', data[i].totalVendido);

            }
        },
        error: function () {
            console.log('Error al cargar productos más vendidos');
        }
    });
}