document.addEventListener('DOMContentLoaded', function () {
    //Informacion Producto
    var formulario_producto = document.getElementById("formulario1");
    var idPizza = 1;

    var nombre = document.getElementById("Nombre");
    var mensaje_nombre = document.getElementById("mensaje_nombre")

    var precio = document.getElementById("precio");
    var mensaje_precio = document.getElementById("mensaje_precio");

    var categoria = document.getElementById("Producto_IdCategoria");
    var mensaje_categoria = document.getElementById("mensaje_categoria");

    var tamano = document.getElementById("tamano");
    var mensaje_tamano = document.getElementById("mensaje_tamano");
    //Informacion Receta


    var insumo = document.getElementById('insumo');
    var mensaje_insumo = document.getElementById('mensaje_Insumo');

    var cantidadInsumo = document.getElementById('CantidadInsumo');
    var mensaje_CantInsumo = document.getElementById('mensaje_Cantinsumo');


    const enlacesMenu = document.querySelectorAll('.links-modulos');
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

    //Escuchadores para productos
    formulario_producto.addEventListener('submit', EnvioProducto)
    nombre.addEventListener('input', ValidarNombre)
    precio.addEventListener('input', ValidarPrecio)
    categoria.addEventListener('change', ValidarCategoria)
    tamano.addEventListener('change', ValidarTamano)

    //Escuchadores de receta
    insumo.addEventListener('change', ValidarInsumo);
    cantidadInsumo.addEventListener('input', ValidarCantInsumo);

    //validar el campo nombre del producto
    function ValidarNombre() {
        var valorNombre = nombre.value;
        var caracterMinimo = 4;
        var caracterMAximo = 30;

        nombre.classList.remove('is-invalid', 'is-valid');
        mensaje_nombre.textContent = '';
        if (valorNombre.trim() === '') {
            nombre.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El campo no puede ir vacio';
        } else if (valorNombre.length < caracterMinimo) {
            nombre.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El nombre debe tener más de 4 caracteres';
        } else if (valorNombre.length > caracterMAximo) {
            nombre.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El nombre debe tener menos de 30 caracteres';
        } else {
            nombre.classList.add('is-valid');
        }
    }
    //validar el campo precio del producto
    function ValidarPrecio() {
        var valorPrecio = precio.value;
        var precioMinimo = 100;
        var precioMaximo = 6000000;
        precio.classList.remove('is-invalid', 'is-valid');
        mensaje_precio.textContent = '';

        if (valorPrecio.trim() === '') {
            precio.classList.add('is-invalid');
            mensaje_precio.textContent = 'El campo no puede ir vacio';

        } else if (valorPrecio < precioMinimo) {
            precio.classList.add('is-invalid');
            mensaje_precio.textContent = 'El precio debe ser de minimo 100 pesos';
        } else if (valorPrecio > precioMaximo) {
            precio.classList.add('is-invalid');
            mensaje_precio.textContent = 'El precio debe ser de maximo 6.000.000 pesos';
        } else {
            precio.classList.add('is-valid');
        }
    }
    //validar el campo categoria del producto
    function ValidarCategoria() {
        var ValorCategoria = categoria.value;

        categoria.classList.remove('is-invalid', 'is-valid');
        mensaje_categoria.textContent = '';
        if (ValorCategoria.trim() === '') {
            categoria.classList.add('is-invalid');
            mensaje_categoria.textContent = 'El campo no puede estar vacio';

        } else {
            categoria.classList.add('is-valid');

        }
        if (ValorCategoria == idPizza) {
            MostrarCampoOculto();
        } else {
            OcultaCampoOculto();
        }
    }
    //validar el campo Tamaño del producto

    function ValidarTamano() {
        var ValorCategoria = categoria.value;
        var ValorTamano = tamano.value;
        tamano.classList.remove('is-invalid', 'is-valid');
        mensaje_tamano.textContent = '';
        if (ValorCategoria == idPizza) {
            if (ValorTamano.trim() === '') {
                tamano.classList.add('is-invalid');
                mensaje_tamano.textContent = 'El campo no puede ir vacio si la categoria es Pizza'
            } else {
                tamano.classList.add('is-valid');

            }
        }
    }


    //muestra el campo Tamaño
    function MostrarCampoOculto() {
        var campoOculto = document.getElementById('Tamano_Oculto');
        campoOculto.classList.remove('d-none')
    }

    //oculta el campo Tamaño
    function OcultaCampoOculto() {
        var campoOculto = document.getElementById('Tamano_Oculto');
        campoOculto.classList.add('d-none')
    }
    function EnvioProducto(event) {
        event.preventDefault();
        InterfazProducto();
        if (formulario_producto.checkValidity()) {
            const tablaReceta = document.querySelector('#Tabla1');
            if (tablaReceta.rows.length < 1) {
                // Mostrar la SweetAlert de error
                Swal.fire({
                    title: 'Ups...',
                    timer: 2700,
                    text: 'Debes agregar al menos un insumo antes de confirmar el producto.',
                    icon: 'error',
                    showConfirmButton: false,
                });
                // Detener la ejecución del código o realizar alguna acción adicional si es necesario
                return;
            } else {
                Swal.fire({
                    title: '¡Éxito!',
                    text: 'Producto Registrado Correctamente',
                    timer: 2400,
                    icon: 'success',
                    showConfirmButton: false,
                }).then((result) => {
                    console.log('Formulario válido');
                    formulario_producto.removeEventListener('submit', EnvioProducto);
                    formulario_producto.submit();
                });
            }
        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            InterfazProducto();
        }
    }
    function InterfazProducto() {
        ValidarNombre();
        ValidarPrecio();
        ValidarTamano();
        ValidarCategoria();
    }



    window.addEventListener('load', function () {
        const cancelarBtn = document.querySelector('.cancelarBtn');
        if (cancelarBtn != null) {
            cancelarBtn.addEventListener('click', function (event) {
                event.preventDefault();
                Swal.fire({
                    title: '¿Estás seguro?',
                    text: 'Desea descartar el producto',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, cancelar',
                    cancelButtonText: 'No, volver'
                }).then((result) => {
                    if (result.isConfirmed) {
                        // Si el usuario confirma, redirigir a la función "Delete" en el controlador de ventas
                        window.location.href = cancelarBtn.getAttribute('href');
                    }
                });
            });
        }
    });
    enlacesMenu.forEach(enlace => {
        enlace.addEventListener('click', e => {

            e.preventDefault();

            if (DetallesSinConfirmar()) {
                Swal.fire({
                    title: 'Advertencia',
                    text: 'Si sales de esta página, perderás los cambios. ¿Estás seguro?',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, salir',
                    cancelButtonText: 'Cancelar'
                }).then(result => {
                    if (result.isConfirmed) {
                        EliminarDetalles();

                        window.location.href = e.target.href;

                    }
                });
            } else {
                window.location.href = e.target.href;
            }
        });
    });
});
//validar el campo Insumos del recetas
function ValidarInsumo() {
    var insumo = document.getElementById('insumo');
    var mensaje_insumo = document.getElementById('mensaje_Insumo');
    var ValorInsumo = insumo.value;

    insumo.classList.remove('is-invalid', 'is-valid');
    mensaje_insumo.textContent = ''
    if (ValorInsumo.trim() === '') {
        insumo.classList.add('is-invalid')
        mensaje_insumo.textContent = 'El campo no puede estar vacio para crear una receta'
    } else {
        insumo.classList.add('is-valid')
    }
}

//validar el campo cantidad del recetas
function ValidarCantInsumo() {
    var cantidadInsumo = document.getElementById('CantidadInsumo');
    var mensaje_CantInsumo = document.getElementById('mensaje_Cantinsumo');
    var ValorCantInsumo = cantidadInsumo.value;


    cantidadInsumo.classList.remove('is-invalid', 'is-valid');
    mensaje_CantInsumo.textContent = ''

    if (ValorCantInsumo.trim() === '') {
        cantidadInsumo.classList.add('is-invalid');
        mensaje_CantInsumo.textContent = 'El campo no puede estar vacio para crear una receta'
    } else if (ValorCantInsumo < 1) {
        cantidadInsumo.classList.add('is-invalid');
        mensaje_CantInsumo.textContent = 'la cantidad de insumos deben ser mayores a 1'
    } else {
        cantidadInsumo.classList.add('is-valid');
    }
}

function EnvioReceta(event) {
    ValidarInsumo();
    ValidarCantInsumo();
}
var Nuevas_Recetas_ID = [];
var Nuevas_Recetas_Cant = [];
var Id_Producto;
var Recetas_Actualizadas_ID = [];
var Recetas_Actualizadas_Cant = [];
var Eliminar_Receta = [];

document.getElementById("Formurario_Modal").addEventListener("submit", function (event) {


    var Idproducto_Exportar = $('#IdSeleccionado').val()
    var Idproducto_Exportar = parseInt(Idproducto_Exportar);
    var filas = document.querySelectorAll("#Tabla1 tr");

    $.ajax({
        type: "GET",
        url: "/Productos/ConsultarRecetas",
        data: { Id_PRoducto: Idproducto_Exportar },
        success: function (data) {
            console.log(data)
            for (var i = 0; i < data.length; i++) {
                var receta = data[i];
                var encontrado = false;

                // Itera sobre las filas existentes para verificar si el insumo ya está en la tabla
                filas.forEach(function (fila) {
                    var idInsumo_tabla = parseInt(fila.cells[0].textContent);

                    if (receta.idInsumo === idInsumo_tabla) {
                        var total = receta.cantInsumo;
                        fila.querySelector("#CantInsumo").textContent = total;

                        var IdReceta = parseInt(fila.cells[5].textContent);
                        var NuevaCantidad = total;

                        if (IdReceta === 0) {
                            for (var j = 0; j < Nuevas_Recetas_ID.length; j++) {
                                if (Nuevas_Recetas_ID[j] === receta.idInsumo) {
                                    Nuevas_Recetas_Cant[j] = total;
                                }
                            }
                        } else {
                            Atualizar_Recetas(IdReceta, NuevaCantidad);
                        }

                        encontrado = true;
                    }
                });

                if (!encontrado) {
                    // El insumo no se encontró en las filas existentes, así que insertamos una nueva fila
                    var tabla = document.getElementById("Tabla1");
                    var nuevaFila = tabla.insertRow(-1);

                    var idInsumo = nuevaFila.insertCell(0);
                    var nomInsumo = nuevaFila.insertCell(1);
                    var cantInsumo = nuevaFila.insertCell(2);
                    var medida = nuevaFila.insertCell(3);
                    var acciones = nuevaFila.insertCell(4);
                    var IdReceta = nuevaFila.insertCell(5);

                    idInsumo.classList.add('d-none');
                    IdReceta.classList.add('d-none');

                    idInsumo.innerHTML = receta.idInsumo;
                    nomInsumo.innerHTML = receta.nomInsumo;
                    cantInsumo.id = 'CantInsumo';
                    cantInsumo.innerHTML = receta.cantInsumo;
                    medida.innerHTML = receta.medida;
                    IdReceta.innerHTML = 0;

                    var boton = document.createElement('button');
                    boton.className = 'btn btn-outline-danger';
                    boton.textContent = 'Remover';
                    boton.addEventListener('click', function () {
                        ocultarFila(this);
                    });
                    acciones.appendChild(boton);

                    var id_producto_texto = document.getElementById("Producto_IdProducto").value;
                    var id_producto = parseInt(id_producto_texto);
                    Agregar_Nuevas_Recetas(receta.cantInsumo, id_producto, receta.idInsumo);
                }
            }
        },
        error: function (xhr, status, error) {

            console.log("Error en la solicitud: " + error);
        }
    });
    $("#myModal").modal("hide");

    event.preventDefault();

});
(function () {
    'use strict';
    window.addEventListener('load', function () {

        var forms = document.getElementsByClassName('needs-validation');

        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();


document.getElementById("miFormulario").addEventListener("submit", function (event) {
    event.preventDefault();
    
    EnvioReceta();


    var campo_idInsumo = $('#insumo').val()
    var campo_cantidadInsumo = $('#CantidadInsumo').val()
    var idInsumo_Input = parseInt(campo_idInsumo);
    var cantidadInsumo = parseInt(campo_cantidadInsumo);

    var encontrado = false;
    var NoExiste = false;
    console.log(cantidadInsumo)
    console.log(idInsumo_Input)



    var filas = document.querySelectorAll("#Tabla1 tr");
    filas.forEach(function (fila) {
        var Idinsumo_tabla = parseInt(fila.cells[0].textContent);
        var IdREceta = parseInt(fila.cells[5].textContent);
        if (idInsumo_Input == Idinsumo_tabla) {
            var Cantidad_Existente = parseInt(fila.cells[2].textContent);
            var total = Cantidad_Existente + cantidadInsumo;
            fila.querySelector("#CantInsumo").textContent = total;

            var IdReceta = IdREceta;
            var NuevaCantidad = total;
            if (IdREceta == 0) {
                for (var i = 0; i < Nuevas_Recetas_ID.length; i++) {
                    if (Nuevas_Recetas_ID[i] == idInsumo_Input) {

                        Nuevas_Recetas_Cant[i] = total;
                    }
                }
            } else {

                Atualizar_Recetas(IdReceta, NuevaCantidad)
            }
            encontrado = true;
        }


    });
    if (!encontrado) {
        NoExiste = true;
    }
    if (NoExiste) {
        $.ajax({
            url: '/Productos/ConsultarNomInsumo',
            type: 'GET',
            data: { Idinsumo: idInsumo_Input },
            success: function (data) {
                var Insumo = data[0];


                if (typeof Insumo !== 'undefined' && Insumo !== null) {
                    var tabla = document.getElementById("Tabla1");
                    var nuevaFila = tabla.insertRow(-1);
                    var idInsumo = nuevaFila.insertCell(0);
                    var nomInsumo = nuevaFila.insertCell(1);
                    var cantInsumo = nuevaFila.insertCell(2);
                    var medida = nuevaFila.insertCell(3);
                    var acciones = nuevaFila.insertCell(4);
                    var IdReceta = nuevaFila.insertCell(5);


                    idInsumo.classList.add('d-none');
                    IdReceta.classList.add('d-none');

                    idInsumo.innerHTML = Insumo.idInsumo;
                    nomInsumo.innerHTML = Insumo.nomInsumo;
                    cantInsumo.id = 'CantInsumo';
                    cantInsumo.innerHTML = cantidadInsumo;
                    medida.innerHTML = Insumo.medida;
                    IdReceta.innerHTML = 0;

                    var boton = document.createElement('button');
                    boton.className = 'btn btn-outline-danger';
                    boton.textContent = 'Remover';
                    boton.addEventListener('click', function () {
                        ocultarFila(this);
                    });
                    acciones.appendChild(boton);
                    var id_producto_texto = document.getElementById("Producto_IdProducto").value
                    var id_producto = parseInt(id_producto_texto)

                    Agregar_Nuevas_Recetas(cantidadInsumo, id_producto, Insumo.idInsumo);
                } else {
                    console.log("Insumo es undefined o nulo.");
                }
            },
            error: function (xhr, status, error) {
                reject(error);
            }

        });

    }

});
function ocultarFila(button) {
    var fila = button.closest('tr');
    var IdREceta = parseInt(fila.cells[5].textContent);
    if (IdREceta == 0) {
        Sacar_de_Crear()
    } else {
        Agregar_Eliminar(IdREceta)
    }
    fila.remove();
}
function Agregar_Eliminar(idreceta) {
    Eliminar_Receta.push(idreceta);
    console.log(Eliminar_Receta);
}
function Sacar_de_Crear() {
    var filas = document.querySelectorAll("#Tabla1 tr");
    filas.forEach(function (fila, index) {
        var Idinsumo_tabla = parseInt(fila.cells[0].textContent);
        Nuevas_Recetas_ID = Nuevas_Recetas_ID.filter(function (Elemento) {
            return Elemento !== Idinsumo_tabla;
        });
    });
    console.log(Nuevas_Recetas_ID);
}
function Agregar_Nuevas_Recetas(cantidadInsumo, id_producto, idInsumo) {
    Nuevas_Recetas_ID.push(idInsumo);
    Nuevas_Recetas_Cant.push(cantidadInsumo);
    Id_Producto = id_producto;



}
function Atualizar_Recetas(IdReceta, NuevaCantidad) {
    Recetas_Actualizadas_ID.push(IdReceta);
    Recetas_Actualizadas_Cant.push(NuevaCantidad);


}

document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("formulario1").addEventListener("submit", function (event) {
        $.ajax({
            url: '/Productos/Interfaz',
            type: 'POST',
            data: {
                Actualizar_Id: Recetas_Actualizadas_ID,
                Actualizar_Cantidad: Recetas_Actualizadas_Cant,
                Crear_id: Nuevas_Recetas_ID,
                Crear_Cantidad: Nuevas_Recetas_Cant,
                id_producto: Id_Producto,
                Eliminar: Eliminar_Receta
            },

            success: function (data) {
            }
        });
    });
});
