var cerrarSesion = document.getElementById("logoutForm")
cerrarSesion.addEventListener("click", ReporteDiario)
function ReporteDiario(event) {
    event.preventDefault();

    $.ajax({
        url: "/Ventas/ReporteDiario",
        success: function (data) {

            var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });

            var reporteVenta = formatoColombiano.format(data);
            reporteVenta = reporteVenta.slice(0, -3);
            let mensaje = "Dinero recaudado hasta el momento: " + reporteVenta + ".\n¿Desea cerrar sesión ahora mismo?";
            Swal.fire({
                position: 'top-end',
                icon: 'info',
                title: 'Cierre de caja',
                text: mensaje,
                showCancelButton: true,
                confirmButtonText: 'Sí, salir',
                cancelButtonText: 'No, Cancelar',
            }).then((result) => {
                if (result.isConfirmed) {
                    cerrarSesion.submit()
                }
            });

        },
        error: function (xhr, status, error) {
            // Esta función se ejecutará si hay un error en la solicitud.
            console.log("Error en la solicitud: " + error);
        }
    });
}