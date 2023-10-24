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
    $('#Formurario_Modal').submit(function (event) {
        // Tu código para llenar el campo aquí
        var NomProducto = $('#Producto_NomProducto').val();
        var llenar_Nombre = document.getElementById("nom_Pro_Modal")
        llenar_Nombre.value = NomProducto;

        var precio = $('#Producto_PrecioVenta').val();
        var llenar_Precio = document.getElementById("pre_Pro_Modal")
        llenar_Precio.value = precio;

        var categoria = $('#Producto_IdCategoria').val();
        var llenar_Categoria = document.getElementById("cat_Pro_Modal")
        llenar_Categoria.value = categoria;

        var tamano = $('#Producto_IdTamano').val();
        var llenar_tamano = document.getElementById("tam_Pro_Modal")
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