document.addEventListener('DOMContentLoaded', function () {

    var fomulario = document.getElementById('formCompra');

    var NumeroFactura = document.getElementById('NumeroFactura');
    var Proveedor = document.getElementById('Proveedor');

    var NumeroFeedback = document.getElementById('NumeroFeedback');
    var ProveedorFeedback = document.getElementById('ProveedorFeedback');

    const enlacesMenu = document.querySelectorAll('.links-modulos');


    


    function EnvioCompra(event) {
        event.preventDefault();

        // Validar si la tabla de detalles de compra está vacía

        if (fomulario.checkValidity() && !NumeroFactura.classList.contains('is-invalid') && !Proveedor.classList.contains('is-invalid')) {
            const tablaDetallesCompra = document.querySelector('.tablaDetallesCompra');
            if (tablaDetallesCompra.rows.length <= 1) {
                // Mostrar la SweetAlert de error
                Swal.fire({
                    title: 'Ups...',
                    timer: 2700,
                    text: 'Debes agregar al menos un insumo antes de confirmar la compra.',
                    icon: 'error',
                    showConfirmButton: false,
                });

                return;
            } else {
                Swal.fire({
                    title: '¡Éxito!',
                    text: 'Compra registrada',
                    timer: 2400,
                    icon: 'success',
                    showConfirmButton: false,
                }).then((result) => {
                    console.log('Formulario válido');
                    fomulario.removeEventListener('submit', EnvioCompra);
                    fomulario.submit();
                    localStorage.removeItem('NumeroFacturaL');
                    localStorage.removeItem('ProveedorL');
                });
            }
        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            console.log('Formulario inválido');
            validarNumeroFactura();
            validarProveedor();
        }
    }

    fomulario.addEventListener('submit', EnvioCompra);


    NumeroFactura.addEventListener('input', function () {
        validarNumeroFactura();
    });

    Proveedor.addEventListener('change', function () {
        validarProveedor();
    });

    function validarProveedor() {
        // Obtener el valor seleccionado del campo select
        var ProveedorSeleccion = Proveedor.value;

        // Restablecer los estilos y mensajes de validación
        Proveedor.classList.remove('is-invalid', 'is-valid');
        ProveedorFeedback.textContent = '';

        // Validar si no se ha seleccionado ninguna opción
        if (ProveedorSeleccion === '') {
            Proveedor.classList.add('is-invalid');
            ProveedorFeedback.textContent = 'Por favor, elige una opción.';
        } else {
            Proveedor.classList.add('is-valid');
        }
    }

    function validarNumeroFactura() {
        var numFactura = NumeroFactura.value;

        // Restablecer los estilos y mensajes de validación
        NumeroFactura.classList.remove('is-invalid', 'is-valid');
        NumeroFeedback.textContent = '';

        // Validar si el campo está vacío
        if (numFactura.trim() === '') {
            NumeroFactura.classList.add('is-invalid');
            NumeroFeedback.textContent = 'Por favor ingrese el código de la factura.';
        }
        // Validar que no tenga un espacio en blanco
        else if (/^\s/.test(numFactura)) {
            NumeroFactura.classList.add('is-invalid');
            NumeroFeedback.textContent = 'No se puede comenzar con un espacio en blanco.';
        }
        // Validar longitud del nombre
        else if (numFactura.length < 3 || numFactura.length > 20) {
            NumeroFactura.classList.add('is-invalid');
            NumeroFeedback.textContent = 'El código debe tener entre 3 y 20 caracteres.';
        } else if (/[^a-zA-Z0-9\sñÑ]/.test(numFactura)) {
            NumeroFactura.classList.add('is-invalid');
            NumeroFeedback.textContent = 'No se pueden ingresar caracteres especiales.';
        }
        else {
            NumeroFactura.classList.add('is-valid');

            // Validar si el número de factura está duplicado
            $.ajax({
                type: 'GET',
                url: '/Compras/NumeroFacturaDuplicado',
                data: { NumeroFactura: numFactura },
                success: function (result) {
                    if (result === true) {
                        NumeroFactura.classList.add('is-invalid');
                        NumeroFeedback.textContent = 'No se puede repetir el número de factura.';
                    } else {
                        NumeroFactura.classList.add('is-valid');
                    }
                },
                error: function () {
                    // Manejo de errores si la solicitud falla
                    console.log('Error en la solicitud AJAX');
                }
            });
        }
    }

    function actualizarReloj() {
        var ahora = new Date();
        var dia = ahora.getDate();
        var mes = ahora.getMonth() + 1;
        var año = ahora.getFullYear();
        var horas = ahora.getHours();
        var minutos = ahora.getMinutes();

        if (dia < 10) dia = '0' + dia;
        if (mes < 10) mes = '0' + mes;
        if (horas < 10) horas = '0' + horas;
        if (minutos < 10) minutos = '0' + minutos;

        var fechaHoraFormateada = dia + '/' + mes + '/' + año + ' ' + horas + ':' + minutos;

        document.getElementById('reloj').textContent = fechaHoraFormateada;
    }

    actualizarReloj();
    setInterval(actualizarReloj, 1000);

    // Manejar el paso de modulo mientras esta en una compra
    enlacesMenu.forEach(enlace => {
        enlace.addEventListener('click', e => {
            e.preventDefault();

            var href = e.currentTarget.href;

            Swal.fire({
                title: 'Advertencia',
                text: 'Si sales de esta página, perderás los cambios. ¿Estás seguro?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, salir',
                cancelButtonText: 'Cancelar'
            }).then(result => {
                if (result.isConfirmed) {
                    localStorage.removeItem('NumeroFacturaL');
                    localStorage.removeItem('ProveedorL');
                    EliminarCompra().then(() => {
                        window.location.href = href;
                    });
                }
            });
        });
    });

    function EliminarCompra() {
        IdCompra = document.getElementById("Item2_IdCompra").value;
        return $.ajax({
            url: '/Compras/DetallesSinConfirmar',
            type: 'POST',
            data: { IdCompra },
            success: function (response) {
                console.log("Todo fue bien");
            },
            error: function (xhr, status, error) {
                Swal.fire('Error', 'Ha ocurrido un error al enviar la solicitud', 'error');
            }
        });
    }
});

document.addEventListener('DOMContentLoaded', function () {

    var fomularioAgregar = document.getElementById('formAgregar');

    var InsumoCompra = document.getElementById('Item1_IdInsumo');
    var CantidadCompra = document.getElementById('CantidadCompra')
    var MedidaCompra = document.getElementById('MedidaCompra');
    var PrecioCompra = document.getElementById('PrecioCompra');

    var InsumoFeedback = document.getElementById('InsumoFeedback');
    var CantidadFeedback = document.getElementById('CantidadFeedback');
    var MedidaFeedback = document.getElementById('MedidaFeedback');
    var PrecioFeedback = document.getElementById('PrecioFeedback');

    var DivBotonAgregar = document.querySelector('.DivBotonAgregar');
    var divBotonInsumo = document.querySelector('.divBotonInsumo');




    function EnvioDetalle(event) {
        event.preventDefault();
        if (fomularioAgregar.checkValidity() && !InsumoCompra.classList.contains('is-invalid') && !CantidadCompra.classList.contains('is-invalid') && !MedidaCompra.classList.contains('is-invalid') && !PrecioCompra.classList.contains('is-invalid')) {
            console.log('Formulario válido');
            fomularioAgregar.removeEventListener('submit', EnvioDetalle);
            fomularioAgregar.submit();
            $('#Item1_IdInsumo').trigger('change');
        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            console.log('Formulario inválido');
            cambiarClaseBotonAgregar();
            cambiarClaseBotonInusumo();
            validarInsumoCompra();
            validarCantidadCompra();
            validarMedidaCompra();
            validarPrecioCompra();
        }
    }

    fomularioAgregar.addEventListener('submit', EnvioDetalle);

    function cambiarClaseBotonAgregar() {
        var DivBotonAgregar = document.querySelector('.DivBotonAgregar');
        if (DivBotonAgregar) {
            DivBotonAgregar.classList.remove('align-items-end');
            DivBotonAgregar.classList.add('align-items-center');
        }
    }

    function cambiarClaseBotonInusumo() {
        var divBotonInsumo = document.querySelector('.divBotonInsumo');
        if (divBotonInsumo) {
            divBotonInsumo.classList.remove('align-items-end');
            divBotonInsumo.classList.add('align-items-center');
        }
    }

    function restaurarClaseBotonAgregar() {
        var DivBotonAgregar = document.querySelector('.DivBotonAgregar');
        if (DivBotonAgregar) {
            DivBotonAgregar.classList.add('align-items-end');
            DivBotonAgregar.classList.remove('align-items-center');
        }
    }

    function restaurarClaseBotonInusumo() {
        var divBotonInsumo = document.querySelector('.divBotonInsumo');
        if (divBotonInsumo) {
            divBotonInsumo.classList.add('align-items-end');
            divBotonInsumo.classList.remove('align-items-center');
        }
    }

    InsumoCompra.addEventListener('change', function () {
        validarInsumoCompra();
    });

    

    function validarInsumoCompra() {
        // Obtener el valor seleccionado del campo select
        var InsumoSeleccion = InsumoCompra.value;

        // Restablecer los estilos y mensajes de validación
        InsumoCompra.classList.remove('is-invalid', 'is-valid');
        InsumoFeedback.textContent = '';

        if (InsumoSeleccion === '' ) {
            InsumoCompra.classList.add('is-invalid');
            InsumoFeedback.textContent = 'Por favor, Escoja una opcion.';
            cambiarClaseBotonInusumo();
        } else {
            if (!InsumoCompra.classList.contains('is-invalid') && !CantidadCompra.classList.contains('is-invalid')) {
                restaurarClaseBotonInusumo();
            }
            InsumoCompra.classList.add('is-valid');
        }
    }

    CantidadCompra.addEventListener('input', function () {
        validarCantidadCompra();
    });

    function validarCantidadCompra() {
        // Obtener el valor seleccionado del campo cantidad
        var Cantidad = CantidadCompra.value;

        // Restablecer los estilos y mensajes de validación
        CantidadCompra.classList.remove('is-invalid', 'is-valid');
        CantidadFeedback.textContent = '';

        var regex = /\./;

        if (Cantidad.trim() === '' ) {
            CantidadCompra.classList.add('is-invalid');
            CantidadFeedback.textContent = 'Por favor, ingrese la cantidad.';
            cambiarClaseBotonInusumo();
        }
        // Validar longitud de la cantidad
        else if (Cantidad <= 0) {
            CantidadCompra.classList.add('is-invalid');
            CantidadFeedback.textContent = 'Debe ser mayor a 0';
            cambiarClaseBotonInusumo();
        } else if (regex.test(Cantidad)) {
            CantidadCompra.classList.add('is-invalid');
            CantidadFeedback.textContent = 'No se pueden ingresar caracteres especiales.';
        }
        else if (Cantidad.length > 10) {
            CantidadCompra.classList.add('is-invalid');
            CantidadFeedback.textContent = 'No se pueden ingresar mas de 10 numeros.';
            cambiarClaseBotonInusumo();
        }
        else {
            if (!InsumoCompra.classList.contains('is-invalid') && !CantidadCompra.classList.contains('is-invalid')) {
                restaurarClaseBotonInusumo();
            }
            CantidadCompra.classList.add('is-valid');
        }
    }

    MedidaCompra.addEventListener('change', function () {
        validarMedidaCompra();
    });

    function validarMedidaCompra() {
        // Obtener el valor seleccionado del campo select
        var MedidaSeleccion = MedidaCompra.value;

        // Restablecer los estilos y mensajes de validación
        MedidaCompra.classList.remove('is-invalid', 'is-valid');
        MedidaFeedback.textContent = '';

        // Validar si no se ha seleccionado ninguna opción
        if (MedidaSeleccion === '') {
            MedidaCompra.classList.add('is-invalid');
            MedidaFeedback.textContent = 'Por favor, elige una opción.';
            cambiarClaseBotonAgregar();
        } else {
            // El campo es válido
            if (!MedidaCompra.classList.contains('is-invalid') && !PrecioCompra.classList.contains('is-invalid')) {
                restaurarClaseBotonAgregar();
            }
            MedidaCompra.classList.add('is-valid');
        }
    }


    PrecioCompra.addEventListener('input', function () {
        validarPrecioCompra();
    });

    function validarPrecioCompra() {
        // Obtener el valor seleccionado del campo precio
        var Precio = PrecioCompra.value;
        const esNumerico = /^[0-9]*$/.test(Precio);

        // Restablecer los estilos y mensajes de validación
        PrecioCompra.classList.remove('is-invalid', 'is-valid');
        PrecioFeedback.textContent = '';

        if (Precio.trim() === '') {
            PrecioCompra.classList.add('is-invalid');
            PrecioFeedback.textContent = 'Por favor, ingrese la precio del insumo';
            cambiarClaseBotonAgregar();
        } else if (/^\s/.test(Precio)) {
            PrecioCompra.classList.add('is-invalid');
            PrecioFeedback.textContent = 'No puede tener espacio en blanco.';
            cambiarClaseBotonAgregar();
        }
        else if (!esNumerico) {
            PrecioCompra.classList.add('is-invalid');
            PrecioFeedback.textContent = 'No se pueden ingresar letras';
            cambiarClaseBotonAgregar();
        }
        // Validar longitud del precio
        else if (Precio < 100) {
            PrecioCompra.classList.add('is-invalid');
            PrecioFeedback.textContent = 'Debe ser mayor a o igual a 100 pesos';
            cambiarClaseBotonAgregar();
        } else if (/[^a-zA-Z0-9\s]/.test(Precio)) {
            PrecioCompra.classList.add('is-invalid');
            PrecioFeedback.textContent = 'No se pueden ingresar caracteres especiales.';
            cambiarClaseBotonAgregar();
        } else if (Precio.length > 10) {
            PrecioCompra.classList.add('is-invalid');
            PrecioFeedback.textContent = 'No se pueden ingresar mas de 10 numeros.';
            cambiarClaseBotonAgregar();
        }
        else {
            if (!MedidaCompra.classList.contains('is-invalid') && !PrecioCompra.classList.contains('is-invalid')) {
                restaurarClaseBotonAgregar();
            }
            PrecioCompra.classList.add('is-valid');
        }
    }
    
});

window.addEventListener('load', function () {
    const cancelarCompraBtn = document.querySelector('.cancelarCompraBtn');

    // Agregar evento de clic al botón cancelar
    cancelarCompraBtn.addEventListener('click', function (event) {
        event.preventDefault();

        // Mostrar la SweetAlert de confirmación
        Swal.fire({
            title: '¿Estás seguro?',
            text: 'Esta acción cancelará la compra actual.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sí, cancelar',
            cancelButtonText: 'No, volver'
        }).then((result) => {
            if (result.isConfirmed) {
                // Si el usuario confirma, redirigir a la función "Delete" en el controlador de compras
                window.location.href = cancelarCompraBtn.getAttribute('href');
                localStorage.removeItem('NumeroFacturaL');
                localStorage.removeItem('EmpleadoL');
                localStorage.removeItem('ProveedorL');
            }
        });
    });
});

$('#Item1_IdInsumo').on('change', function () {
    var MedidaSeleccionada = $(this).find('option:selected').data('medida');
    updateMedidaOptions(MedidaSeleccionada);
});

function updateMedidaOptions(MedidaSeleccionada) {
    var Medida = $('#MedidaCompra');

    Medida.find('option').remove();

    Medida.append($('<option>', {
        value: '',
        text: 'Elige una opción',
        selected: true
    }));

    if (MedidaSeleccionada === 'Gramo') {
        Medida.append($('<option>', {
            value: 'Gramos',
            text: 'Gramos'
        }));
        Medida.append($('<option>', {
            value: 'Kilogramos',
            text: 'Kilogramos'
        }));
        Medida.append($('<option>', {
            value: 'Libras',
            text: 'Libras'
        }));
    } else if (MedidaSeleccionada === 'Mililitro') {
        Medida.append($('<option>', {
            value: 'Mililitro',
            text: 'Mililitro'
        }));
        Medida.append($('<option>', {
            value: 'Litros',
            text: 'Litros'
        }));
        Medida.append($('<option>', {
            value: 'Onza',
            text: 'Onza'
        }));
    } else if (MedidaSeleccionada === 'Unidad') {
        Medida.append($('<option>', {
            value: 'Unidad',
            text: 'Unidad'
        }));
    }
}

document.addEventListener('DOMContentLoaded', function () {

    $('#formAgregar').submit(function (event) {
        var NumeroFactura = $('#NumeroFactura').val();
        localStorage.setItem('NumeroFacturaL', NumeroFactura)

        var Empleado = $('#Empleado').val();
        localStorage.setItem('EmpleadoL', Empleado)

        var Proveedor = $('#Proveedor').val();
        localStorage.setItem('ProveedorL', Proveedor)

        console.log(NumeroFacturaLG)

        return true;
    })

    var NumeroFacturaLG = localStorage.getItem('NumeroFacturaL');
    var EmpleadoLG = localStorage.getItem('EmpleadoL');
    var ProveedorLG = localStorage.getItem('ProveedorL');

    if (NumeroFacturaLG !== null) {
        $('#NumeroFactura').val(NumeroFacturaLG);
    }
    if (EmpleadoLG !== null) {
        $('#Empleado').val(EmpleadoLG);
    }
    if (ProveedorLG !== null) {
        $('#Proveedor').val(ProveedorLG);
    }
});