 //Miguel 20/10/2023
document.addEventListener('DOMContentLoaded', function () {
    //Variables para validar la venta
    var formulario = document.getElementById("formVentas")
    var pago = document.getElementById("pago")
    var pagoMensaje = document.getElementById("pagoMensaje")

    //Variables para validar detalles de venta
    var fomularioAgregar = document.getElementById('formAgregar');
    var producto = document.getElementById('Item1_IdProducto');
    var cantidad = document.getElementById('Item1_CantVendida');
    var domicilio = document.getElementById('domicilio');
    var productoMensaje = document.getElementById('productoMensaje');
    var cantidadMensaje = document.getElementById('cantidadMensaje');
    var domicilioMensaje = document.getElementById('domicilioMensaje');

    const enlacesMenu = document.querySelectorAll('#links-modulos');
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
    domicilio.addEventListener('input', ValidarDomicilio)

    //---------------Funciones para validar ventas------------------------
    //Miguel 22/10/2023: Función para validar los campos en tiempo real con SweetAlert2
    function EnvioVenta(event) {
        event.preventDefault();

        ValidarDomicilio()
        ValidarPago()
        if (formulario.checkValidity() && !pago.classList.contains('is-invalid')) {
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
                let tipoCuenta = document.getElementById("Mesa");
                valorTipoCuenta = tipoCuenta.value;
                var mensajeAlerta = valorTipoCuenta === "General" ? "Venta registrada como Pagada" : "Venta registrada como cuenta abierta";

                Swal.fire({
                    title: '¡Éxito!',
                    text: mensajeAlerta,
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
            ValidarPago();
            ValidarDomicilio();
        }

    }

    //Miguel 22/10/2023: Función para validar el campo de pago con todas sus posibilidades
    function ValidarPago() {
        var valorPago = pago.value;
        var totalVenta = document.getElementById("totalVenta-input").value;
        let metodoPago = document.getElementById("Item2_TipoPago").value
        totalVenta = parseInt(totalVenta);

        pago.classList.remove('is-invalid', 'is-valid');
        pagoMensaje.textContent = '';

        if (metodoPago == 'Cuenta abierta') {
            pago.classList.add('is-valid');
            return;
        }

        else if (/[^0-9]/.test(valorPago)) {
            pago.classList.add('is-invalid');
            pagoMensaje.textContent = 'El campo no puede contener caracteres especiales';
        }
        
        else if (valorPago > 999999999) {
            pago.classList.add('is-invalid');
            pagoMensaje.textContent = 'El campo no puede tener más de 10 caracteres';
        }
        else if (valorPago < totalVenta && metodoPago == "Efectivo") {
            let cambio = totalVenta - valorPago

            var formatoColombiano = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });
            var cambioFormateado = formatoColombiano.format(cambio);
            cambioFormateado = cambioFormateado.slice(0, -3);

            pago.classList.add('is-invalid');
            pagoMensaje.textContent = 'Falta por agregar ' + cambioFormateado;
        }
        else if (metodoPago == 'Transferencia' && valorPago > totalVenta) {
            pago.classList.add('is-invalid');
            pagoMensaje.textContent = 'El pago no debe ser mayor al total';

        } else if (metodoPago == 'Transferencia' && valorPago < totalVenta) {
            pago.classList.add('is-invalid');
            pagoMensaje.textContent = 'El pago no debe ser menor al total';
        }
        
        else {
            pago.classList.add('is-valid');
        }

    }

    //------------------------Funciones para validar detalles de venta------------------------
    //Miguel 22/10/2023: Función para validar los campos en tiempo real con SweetAlert2
    function EnvioDetalle(event) {
        ValidarProducto()
        event.preventDefault();
        if (fomularioAgregar.checkValidity() && !producto.classList.contains('is-invalid') && !cantidad.classList.contains('is-invalid')) {
            ValidarInsumos().then(function (data) {
                if (data) {
                    fomularioAgregar.removeEventListener('submit', EnvioDetalle);
                    fomularioAgregar.submit();
                    $(producto).trigger('change');
                } else {
                    Swal.fire({
                        title: 'Ups...',
                        timer: 2700,
                        text: 'No hay insumos suficientes para este producto.',
                        icon: 'error',
                        showConfirmButton: false,
                    });
                }
            }).catch(function (error) {
                console.log(error);
            });
        }
        else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            console.log('Formulario inválido');
            ValidarProducto();
            ValidarCantidad();

        }
    }

    //Miguel 22/10/2023: Función para validar que el campo de producto no esté vacío
    function ValidarProducto() {
        let valorProducto = producto.value

        producto.classList.remove('is-invalid', 'is-valid');
        productoMensaje.textContent = '';

        if (valorProducto === 'NA') {
            producto.classList.add('is-invalid');
            productoMensaje.textContent = 'El campo no puede estar vacío';
        }
        if (valorProducto <= 4) {
            producto.classList.add('is-invalid');
            productoMensaje.textContent = 'Este producto no es válido';
        }
        else {
            producto.classList.add('is-valid');
        }
        CambiarClaseBotonAgregar()
    }

    //Miguel 22/10/2023: Función para validar que el campo de cantidad no sea menor a 1
    function ValidarCantidad() {
        let valorCantidad = cantidad.value
        valorCantidad = parseInt(valorCantidad)

        cantidad.classList.remove('is-invalid', 'is-valid');
        cantidadMensaje.textContent = '';

        if (valorCantidad === 0) {
            cantidad.classList.add('is-invalid');
            cantidadMensaje.textContent = 'El campo no puede ser menor a 1';
        }

        if (valorCantidad > 999999999) {
            cantidad.classList.add('is-invalid');
            cantidadMensaje.textContent = 'El campo no puede tener mas de 10 caracteres';
        }

        if (!/^\d+$/.test(valorCantidad)) {
            cantidad.classList.add('is-invalid');
            cantidadMensaje.textContent = 'El campo no admite caracteres especiales';
        }

        if (isNaN(valorCantidad)) {
            cantidad.classList.add('is-invalid');
            cantidadMensaje.textContent = 'Por favor ingrese un número válido';
        }

        else {
            cantidad.classList.add('is-valid')
        }
        CambiarClaseBotonAgregar()
    }

    //Miguel 22/10/2023: Función para validar que haya insumos suficientes en el sistema antes de ingresar otro detalle
    function ValidarInsumos() {
        let productoConsultar = producto.value
        let cantidadConsultar = cantidad.value

        return new Promise(function (resolve, reject) {
            $.ajax({
                url: "/DetalleVentas/InsumosSuficientes",
                data: { idProducto: productoConsultar, cantidadVender: cantidadConsultar },
                success: function (data) {
                    resolve(data);
                },
                error: function (xhr, status, error) {
                    // Esta función se ejecutará si hay un error en la solicitud.
                    console.log("Error en la solicitud: " + error);
                }
            });
        });
    }

    //Miguel 06/11/2023: Función para validar el campo de domicilio
    function ValidarDomicilio() {
        let domicilioValor = domicilio.value
        domicilioValor = parseInt(domicilioValor)

        domicilio.classList.remove('is-invalid', 'is-valid');
        domicilioMensaje.textContent = '';

        if (domicilioValor > 999999999) {
            domicilio.classList.add('is-invalid');
            domicilioMensaje.textContent = 'El campo no puede tener mas de 10 caracteres';
        }

        if (!/^\d+$/.test(domicilioValor)) {
            domicilio.classList.add('is-invalid');
            domicilioMensaje.textContent = 'El campo no admite caracteres especiales';
        }

        else {
            domicilio.classList.add('is-valid')
        }


    }


    //Miguel 24/10/2023: Función para saber si hay detalles de venta sin confirmar en la cuenta
    function DetallesSinConfirmar() {
        const detallesTable = document.querySelector('.tablaDetalles-ventas');
        const filas = detallesTable.querySelectorAll('tr');

        for (let i = 0; i < filas.length; i++) {
            const columnaAcciones = filas[i].querySelector('td:last-child');

            if (columnaAcciones && columnaAcciones.querySelector('#btnEliminar')) {
                return true
            }
        }
    }

    //Miguel 24/10/2023: Función para enviar al controlador con AJAX los detalles a eliminar
    function EliminarDetalles() {
        return new Promise((resolve, reject) => {
            const IdVenta = document.getElementById("Item2_IdVenta").value;
            $.ajax({
                url: '/DetalleVentas/DetallesSinConfirmar',
                type: 'POST',
                data: { IdVenta },
                success: function (response) {
                    console.log("Todo fue bien");
                    resolve(response); // Resuelve la promesa cuando la solicitud es exitosa
                },
                error: function (xhr, status, error) {
                    console.error("Error en la solicitud: " + error);
                    reject(error); // Rechaza la promesa en caso de error
                }
            });
        });
    }

    //Miguel 25:10/2023: Función para que el boton de agregar no se mueva de su posición al tener un campo invalido
    function CambiarClaseBotonAgregar() {
        var DivBotonAgregar = document.querySelector('.DivBotonAgregar');
        if (cantidad.classList.contains('is-invalid') || producto.classList.contains('is-invalid')) {
            DivBotonAgregar.classList.remove('align-items-end');
            DivBotonAgregar.classList.add('align-items-center');
        } else {
            restaurarClaseBotonAgregar()
        }
    }

    //Miguel 25:10/2023: Función para que el boton de agregar que se posicione en su forma normal al tener el campo valido
    function restaurarClaseBotonAgregar() {
        var DivBotonAgregar = document.querySelector('.DivBotonAgregar');
        if (DivBotonAgregar) {
            DivBotonAgregar.classList.add('align-items-end');
            DivBotonAgregar.classList.remove('align-items-center');
        }
    }

    window.addEventListener('load', function () {
        const cancelarCompraBtn = document.querySelector('.cancelarCompraBtn');

        // Agregar evento de clic al botón
        if (cancelarCompraBtn != null) {

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
        }
    });

    //Foreach para recorrer las etiquetas 'a' del menu y lanzar una alerta para evitar que se salga al instante
    enlacesMenu.forEach(enlace => {
        enlace.addEventListener('click', e => {
            e.preventDefault();
            var href = e.currentTarget.href;

            if (DetallesSinConfirmar()) {
                Swal.fire({
                    title: 'Advertencia',
                    text: 'Si sales de esta página, perderás los cambios. ¿Estás seguro?',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, salir',
                    cancelButtonText: 'Cancelar'
                }).then(result => {
                    if (result.isConfirmed) {
                        EliminarDetalles().then(() => {
                            console.log(href)
                            window.location.href = href;
                        });
                    }
                });
            } else {
                console.log(href)
                window.location.href = href;
            }
        });
    });
});