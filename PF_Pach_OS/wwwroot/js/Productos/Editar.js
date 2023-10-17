$(document).ready(function () {

    var categoria = $('#Producto_IdCategoria').val();
    var idPizza = 1;
    if (categoria == idPizza) {
        Mostrar_Campo_Oculto();

    } else if (categoria != idPizza) {
        Ocultar_Campo_Oculto();
    }

    $('#Producto_NomProducto').on('input', function () {
        validateNomProducto();
        var id = $('#Producto_IdProducto').val();
        console.log(id)
    });

    // Manejar el evento de cambio en el campo de PrecioVenta
    $('#Producto_PrecioVenta').on('input', function () {
        validatePrecioVenta();
    });

    // Manejar el evento de cambio en el campo de IdTamano
    $('#Producto_IdTamano').on('input', function () {
        validateTamano();
    });

    // Manejar el evento de cambio en el campo de IdCategoria
    $('#Producto_IdCategoria').on('change', function () {
        validateCategoria();
    });

    // Manejar el evento de cambio en el campo de CantInsumo
    $('#Receta_CantInsumo').on('input', function () {
        validateCantInsumo();
    });

    // Manejar el evento de cambio en el campo de IdInsumo
    $('#Receta_IdInsumo').on('change', function () {
        validateIdInsumo();
    });
    $('#miFormulario').submit(function (event) {
        // Tu código para llenar el campo aquí
        var NomProducto = $('#Producto_NomProducto').val();
        var llenar_Nombre = document.getElementById("nom_Pro")
        llenar_Nombre.value = NomProducto;

        var precio = $('#Producto_PrecioVenta').val();
        var llenar_Precio = document.getElementById("pre_Pro")
        llenar_Precio.value = precio;

        var categoria = $('#Producto_IdCategoria').val();
        var llenar_Categoria = document.getElementById("cat_Pro")
        llenar_Categoria.value = categoria;

        var tamano = $('#Producto_IdTamano').val();
        var llenar_tamano = document.getElementById("tam_Pro")
        llenar_tamano.value = tamano;


        return true;
    });
    function validateNomProducto() {
        var maxCaracteres = 30;
        var minCaracteres = 4;
        var validacion = false;

        var NomProducto = $('#Producto_NomProducto').val();
        var contarNombre = NomProducto.length;

        console.log(NomProducto)
        if (contarNombre == 0) {
            validacion = true;
            mostrarError('Por favor ingrese un nombre al producto.');
        } else if (contarNombre < minCaracteres) {
            validacion = true;
            mostrarError('El nombre del producto debe tener al menos 4 caracteres.', '#Producto_NomProducto');
        } else if (contarNombre > maxCaracteres) {
            validacion = true;
            mostrarError('El nombre del producto no puede exceder los 30 caracteres.', '#Producto_NomProducto');
        }
        resultado(validacion, '#Producto_NomProducto');

    }



    function validatePrecioVenta() {
        var precio = $('#Producto_PrecioVenta').val();
        var minVenta = 100;
        var validacion = false;

        if (precio == 0) {
            validacion = true;
            mostrarError('Por favor ingrese un Precio', '#Producto_PrecioVenta');
        }
        else if (isNaN(precio)) {
            validacion = true;
            mostrarError('Por favor ingrese un Precio', '#Producto_PrecioVenta');
        } else if (precio < minVenta) {
            validacion = true;
            mostrarError('El presio minimo del producto debe ser 100 pesos', '#Producto_PrecioVenta');
        }
        resultado(validacion, '#Producto_PrecioVenta');



    }

    function validateCategoria() {
        var categoria = $('#Producto_IdCategoria').val();
        var idPizza = 1;
        var validacion = false;

        if (categoria == 0) {
            mostrarError('Por favor seleccione una categoria', '#Producto_IdCategoria');
            Ocultar_Campo_Oculto();
            validacion = true;
        } else if (categoria == idPizza) {
            Mostrar_Campo_Oculto();

        } else if (categoria != idPizza) {
            Ocultar_Campo_Oculto();
        }

        resultado(validacion, '#Producto_IdCategoria');
    }
    function validateCantInsumo() {
        var cantInsumo = $('#Receta_CantInsumo').val();
        var validacion = false;
        if (cantInsumo == 0) {
            mostrarError('Por favor ponga una cantidad de insumo', '#Receta_CantInsumo');
            validacion = true;
        } else if (isNaN(cantInsumo)) {
            mostrarError('Por favor ponga una cantidad de insumo', '#Receta_CantInsumo');
            validacion = true;
        }
        resultado(validacion, '#Receta_CantInsumo');

    }

    function validateIdInsumo() {
        var insumo = $('#Receta_IdInsumo').val();
        var validacion = false;
        if (insumo == 0) {
            mostrarError('Por favor selecione un insumo', '#Receta_IdInsumo');
            validacion = true;
        }

        resultado(validacion, '#Receta_IdInsumo');

    }



    function Mostrar_Campo_Oculto() {
        $('#Tamano_Oculto').removeClass('d-none');
    }
    function Ocultar_Campo_Oculto() {
        $('#Tamano_Oculto').addClass('d-none');
    }

    function resultado(validacion, item) {
        if (validacion) {
            $(item).addClass('is-invalid'); // Agregar clase de error
            $(item).removeClass('is-valid'); // Remover clase de válido
        } else {
            $(item).removeClass('is-invalid'); // Remover clase de error
            $(item).addClass('is-valid'); // Agregar clase de válido
        }
    }
    function mostrarError(mensaje, item) {
        var errorFeedback = $(item).siblings('.invalid-feedback');
        errorFeedback.text(mensaje);
    }



});

(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Fetch all the forms we want to apply custom Bootstrap validation styles to
        var forms = document.getElementsByClassName('needs-validation');
        // Loop over them and prevent submission
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

var Nuevas_Recetas = [];
document.getElementById("miFormulario").addEventListener("submit", function (event) {
    event.preventDefault();

    var campo_idInsumo = $('#Receta_IdInsumo').val()
    var campo_cantidadInsumo = $('#Receta_CantInsumo').val()
    var idInsumo_Input = parseInt(campo_idInsumo);
    var cantidadInsumo = parseInt(campo_cantidadInsumo);

    var encontrado = false;
    var NoExiste = false;
    console.log(cantidadInsumo)
    console.log(idInsumo_Input)



    var filas = document.querySelectorAll("#Tabla1 tr");
    filas.forEach(function (fila) {
        var Idinsumo_tabla = parseInt(fila.cells[0].textContent);
        console.log("===========")
        console.log(Idinsumo_tabla)
        console.log("=============")


        if (idInsumo_Input == Idinsumo_tabla) {
            var Cantidad_Existente = parseInt(fila.cells[2].textContent);
            var total = Cantidad_Existente + cantidadInsumo;
            fila.querySelector("#CantInsumo").textContent = total;
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
                console.log(Insumo)

                if (typeof Insumo !== 'undefined' && Insumo !== null) {
                    var tabla = document.getElementById("Tabla1");
                    var nuevaFila = tabla.insertRow(-1); 
                    var idInsumo = nuevaFila.insertCell(0);
                    var nomInsumo = nuevaFila.insertCell(1);
                    var cantInsumo = nuevaFila.insertCell(2);
                    var medida = nuevaFila.insertCell(3);
                    var acciones = nuevaFila.insertCell(4);

                    idInsumo.classList.add('d-none');
                    idInsumo.innerHTML = Insumo.idInsumo;
                    nomInsumo.innerHTML = Insumo.nomInsumo;
                    cantInsumo.id = 'CantInsumo';
                    cantInsumo.innerHTML = cantidadInsumo;
                    medida.innerHTML = Insumo.medida;

                    var enlace = document.createElement('a');
                    enlace.href = '/Recetas/';
                    enlace.className = 'btn btn-outline-danger';
                    enlace.textContent = 'Remover';
                    acciones.appendChild(enlace);
                    var id_producto_texto = document.getElementById("Producto_IdProducto").value
                    var id_producto = parseInt(id_producto_texto)
                    var receta = { cantInsumo: cantidadInsumo, id_producto: id_producto, id_insumo: Insumo.idInsumo };
                    Agregar_a_la_lista(receta);
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
function Agregar_a_la_lista(objetoJSON) {
    Nuevas_Recetas.push(objetoJSON);
    console.log(Nuevas_Recetas);
}