//Miguel 20/10/2023
document.addEventListener('DOMContentLoaded', function () {
    //Variables para validar la venta
    var formulario = document.getElementById("formVentas")
    var pago = document.getElementById("pago")
    var pagoMensaje = document.getElementById("pagoMensaje")

    //Variables para validar detalles de venta
    var fomularioAgregar = document.getElementById('formAgregar');
    var producto = document.getElementById('Item1_IdProducto');
    var cantidad = document.getElementById('Item1_CantVendida')
    var productoMensaje = document.getElementById('productoMensaje');
    var cantidadMensaje = document.getElementById('cantidadMensaje');
    var DivBotonAgregar = document.querySelector('.DivBotonAgregar');

    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 1700,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    });

    //Escuchadores para ventas
    formulario.addEventListener('submit', EnvioVenta)
    pago.addEventListener('input', ValidarPago)

    //Escuchadores para detalle de venta
    fomularioAgregar.addEventListener('submit', EnvioDetalle);
    producto.addEventListener('change', ValidarProducto)
    cantidad.addEventListener('input', ValidarCantidad)

    //Funciones para validar ventas
    //Funcion para validar los campos en tiempo real con SweetAlert2
    function EnvioVenta(event) {
        event.preventDefault();

        if (formulario.checkValidity() && !pago.classList.contains('is-invalid')) {

            console.log(pago.classList.contains('is-invalid'))
            const tablaDetallesVenta = document.querySelector('.tablaDetalles-ventas');
            if (tablaDetallesVenta.rows.length <= 1) {
                // Mostrar la SweetAlert de error
                Swal.fire({
                    title: 'Ups...',
                    timer: 2700,
                    text: 'Debes agregar al menos un producto antes de confirmar la venta.',
                    icon: 'error',
                    showConfirmButton: false,
                });

                // Detener la ejecución del código o realizar alguna acción adicional si es necesario
                return;
            }
            else {

                Swal.fire({
                    title: '¡Éxito!',
                    text: 'Venta registrada',
                    timer: 2400,
                    icon: 'success',
                    showConfirmButton: false,
                }).then((result) => {
                    console.log('Formulario válido');
                    formulario.removeEventListener('submit', EnvioVenta);
                    formulario.submit();
                });
            }
        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            console.log('Formulario inválido');
            ValidarPago();
        }

    }

    //Funcion para validar el campo de pago con todas sus posibilidades
    function ValidarPago() {
        var valorPago = pago.value;
        var totalVenta = document.getElementById("totalVenta-input").value;
        totalVenta = parseInt(totalVenta);

        pago.classList.remove('is-invalid', 'is-valid');
        pagoMensaje.textContent = '';

        if (valorPago.trim() === '') {
            pago.classList.add('is-invalid');
            pagoMensaje.textContent = 'El campo no puede estar vacío';

        }
        else if (valorPago < 100) {
            pago.classList.add('is-invalid');
            pagoMensaje.textContent = 'El pago no puede ser menor a 100 pesos ';
        }
        else if (valorPago < totalVenta) {
            pago.classList.add('is-invalid');
            pagoMensaje.textContent = 'El pago no debe de ser menor al total';

        } else {
            pago.classList.add('is-valid');
        }
    }

    //Funciones para validar detalles de venta
    //Funcion para validar los campos en tiempo real con SweetAlert2
    function EnvioDetalle(event) {
        event.preventDefault();
        if (fomularioAgregar.checkValidity() && !producto.classList.contains('is-invalid') && !cantidad.classList.contains('is-invalid')) {
            console.log('Formulario válido');
            fomularioAgregar.removeEventListener('submit', EnvioDetalle);
            fomularioAgregar.submit();
            $(producto).trigger('change');

        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            console.log('Formulario inválido');
            CambiarClaseBotonAgregar();
            ValidarProducto();
            ValidarCantidad();
        }
    }
    
    //Funcion para validar que el campo de producto no esté vacío 
    function ValidarProducto() {
        let valorProducto = producto.value

        producto.classList.remove('is-invalid', 'is-valid');
        productoMensaje.textContent = '';

        if (valorProducto === 'NA') {
            producto.classList.add('is-invalid');
            productoMensaje.textContent = 'El campo no puede estar vacío';
        } else {
            producto.classList.add('is-valid');
        }
    }

    //Funcion para validar que el campo de cantidad no sea menor a 1 
    function ValidarCantidad() {
        let valorCantidad = cantidad.value

        cantidad.classList.remove('is-invalid', 'is-valid');
        cantidadMensaje.textContent = '';

        if (valorCantidad <= 0) {
            cantidad.classList.add('is-invalid');
            cantidadMensaje.textContent = 'El campo no puede ser menor a 1';
        } else {
            cantidad.classList.add('is-valid')
        }

    }
    function CambiarClaseBotonAgregar() {
        var DivBotonAgregar = document.querySelector('.DivBotonAgregar');
        if (DivBotonAgregar) {
            DivBotonAgregar.classList.remove('align-items-end');
            DivBotonAgregar.classList.add('align-items-center');
        }
    }

    window.addEventListener('load', function () {
        const cancelarCompraBtn = document.querySelector('.cancelarCompraBtn');

        // Agregar evento de clic al botón
        cancelarCompraBtn.addEventListener('click', function (event) {
            event.preventDefault(); // Evitar que el enlace se siga ejecutando

            // Mostrar la SweetAlert de confirmación
            Swal.fire({
                title: '¿Estás seguro?',
                text: 'Esta acción cancelará la venta actual.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sí, cancelar',
                cancelButtonText: 'No, volver'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Si el usuario confirma, redirigir a la función "Delete" en el controlador de ventas
                    window.location.href = cancelarCompraBtn.getAttribute('href');
                }
            });
        });
    });
});