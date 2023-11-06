var cerrarSesion = document.getElementById("logoutForm")
cerrarSesion.addEventListener("click", ReporteDiario)
console.log("Estoy dentro del archivo")
function ReporteDiario(event) {
    event.preventDefault();

    $.ajax({
        url: "/Ventas/ReporteDiario",
        success: function (data) {
            let mensaje = "Dinero recaudado hasta el momento: " + data + ".\n¿Desea cerrar sesión ahora mismo?";
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