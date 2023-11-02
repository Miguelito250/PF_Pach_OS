﻿$(document).ready(function () {
    //Clase de Detalle de venta para pasar al servidor 
    class DetalleVenta {
        constructor() {
            this.IdVenta = null;
            this.IdProducto = null;
            this.CantVendida = null;
        }
    };

    const maximoSaboresPermitidos = document.getElementById("maximoSabores");
    const checkboxes = document.querySelectorAll('input[type="checkbox"]');
    const cantidad = document.getElementById("CantVendida");
    const cantidadMensaje = document.getElementById("cantidadMensaje");

    const url = new URL(window.location.href);
    const parametros = new URLSearchParams(url.search);
    const idVenta = parametros.get('IdVenta');

    InsertarTextos("Escoger Sabores", "titulo-modal", "texto")

    cantidad.addEventListener('input', ValidarCantidad)
    LimitarSabores()
    $("#btnEnviar").on("click", function () {
        const cantidadVender = document.getElementById("CantVendida").value;
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

        if (cantidadVender > 0) {
            let detalleVenta = new DetalleVenta();

            detalleVenta.IdVenta = idVenta;
            detalleVenta.IdProducto = producto;
            detalleVenta.CantVendida = cantidadVender;

            var saboresSeleccionados = [];
            $('input[name="saborPizza"]:checked').each(function () {
                saboresSeleccionados.push($(this).val());
            });

            if (saboresSeleccionados.length > 0) {
                console.log("Toy dentro ")
                $.ajax({
                    url: "/Ventas/ConfirmarSabores",
                    type: "POST",
                    data: { sabores: saboresSeleccionados, detalleVenta: detalleVenta },
                    success: function (data) {
                        if (data) {
                            // Aquí puedes mostrar una alerta SweetAlert de éxito
                            Swal.fire({
                                title: '¡Éxito!',
                                text: 'Pizza agregada',
                                icon: 'success',
                                timer: 2400,
                                showConfirmButton: false
                            }).then(function () {
                                RecargarPagina();

                            });;
                        }
                        else {
                            Swal.fire({
                                title: 'Ups...',
                                text: 'No hay suficientes insumos para este producto.',
                                icon: 'error',
                                timer: 2700,
                                showConfirmButton: false
                            });
                        }

                    },
                    error: function (xhr, status, error) {
                        // Esta función se ejecutará si hay un error en la solicitud.
                        console.log("Error en la solicitud: " + error);
                    }
                });
            } else {
                // Mostrar una alerta SweetAlert de error si no se seleccionan sabores
                Swal.fire({
                    title: 'Ups...',
                    text: 'Debes seleccionar al menos un sabor de pizza antes de agregarla a la venta.',
                    icon: 'error',
                    timer: 2700,
                    showConfirmButton: false
                });
            }
        } else {
            // Mostrar una alerta SweetAlert de error si no se completan los campos
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            ValidarCantidad()
        }
    });
    //Funcion para insertar textos en etiquetas de esta misma pagina ya sean a inputs o a textos
    function InsertarTextos(valorInsertar, lugarCargar, etiqueta) {
        let valor = valorInsertar
        let idEtiqueta = document.getElementById(lugarCargar)
        if (etiqueta === "texto") {
            idEtiqueta.innerHTML = valor;
        } else if (etiqueta == "numerico") {
            idEtiqueta.value = valor
        } else {
            console.log("No se especifico una etiqueta valida")
        }
    }


    //Funcion para limitar la cantidad de sabores que puede vender segun el tamaño de la pizza
    function LimitarSabores() {
        checkboxes.forEach(checkbox => {
            checkbox.addEventListener("change", function () {
                const checkboxesSeleccionados = Array.from(checkboxes).filter(cb => cb.checked);

                if (checkboxesSeleccionados.length >= maximoSaboresPermitidos.value) {
                    checkboxes.forEach(cb => {
                        if (!cb.checked) {
                            cb.disabled = true;
                        }
                    });
                } else {
                    checkboxes.forEach(cb => {
                        cb.disabled = false;
                    });
                }
            });
        });
    }

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

    //Miguel 19/10/2023
    //Funcion para recargar la pagina al darle click al confirmar los sabores de las pizzas
    function RecargarPagina() {
        location.reload(true);
    }

});