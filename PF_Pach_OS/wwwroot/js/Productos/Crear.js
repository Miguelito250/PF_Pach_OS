
var idPizza = 1;

$(document).ready(function () {
    var tamano = $('#Categorio').val();
    if (tamano == idPizza) {
        console.log("1")
        MostrarCampoOculto();
    }
    function MostrarCampoOculto() {
        var campoOculto = document.getElementById('Tamano_Oculto');
        campoOculto.classList.remove('d-none')
    }
})


document.addEventListener('DOMContentLoaded', function () {
    //Informacion Producto
    var formulario_producto = document.getElementById("formulario1");
    var idPizza = 1;

    var nombre = document.getElementById("Nombre");
    var mensaje_nombre = document.getElementById("mensaje_nombre")

    var precio = document.getElementById("precio");
    var mensaje_precio = document.getElementById("mensaje_precio");

    var categoria = document.getElementById("Categorio");
    var mensaje_categoria = document.getElementById("mensaje_categoria");

    var tamano = document.getElementById("tamano");
    var mensaje_tamano = document.getElementById("mensaje_tamano");
    //Informacion Receta
    var formulario_receta = document.getElementById("miFormulario");

    var nombre_producto = document.getElementById("nom_Pro");
    var precio_producto = document.getElementById("pre_Pro");
    var categoria_producto = document.getElementById("cat_Pro");
    var tamano_producto = document.getElementById("tam_Pro");

    var insumo = document.getElementById('insumo');
    var mensaje_insumo = document.getElementById('mensaje_Insumo');

    var cantidadInsumo = document.getElementById('CantidadInsumo');
    var mensaje_CantInsumo = document.getElementById('mensaje_Cantinsumo');

    //Informacion Imprtar Receta
    var formulario_Importar = document.getElementById("Formurario_Modal");

    var Importar_nombre = document.getElementById("nom_Pro_Modal");
    var Importar_precio = document.getElementById("pre_Pro_Modal");
    var Importar_categoria = document.getElementById("cat_Pro_Modal");
    var Importar_tamano = document.getElementById("tam_Pro_Modal");



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
    formulario_receta.addEventListener('submit', EnvioReceta);
    insumo.addEventListener('change', ValidarInsumo);
    cantidadInsumo.addEventListener('input', ValidarCantInsumo);

    //Escuchadores Importar Receta
    formulario_Importar.addEventListener('submit', EnvioExportar)

    //validar el campo nombre del producto
    function ValidarNombre() {
        var valorNombre = nombre.value;
        const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(valorNombre);
        const hasNumber = /(?=.*\d)/.test(valorNombre);
        var caracterMinimo = 4;
        var caracterMAximo = 30;

        nombre.classList.remove('is-invalid', 'is-valid');
        mensaje_nombre.textContent = '';
        if (valorNombre.trim() === '') {
            nombre.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El campo no puede ir vacio';
        } else if (hasSpecialChar) {
            nombre.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El nombre no puede tener caracteres especiales';
        } else if (hasNumber) {
            nombre.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El nombre no puede tener numeros';
        }
        else if (valorNombre.length < caracterMinimo) {
            nombre.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El nombre debe tener más de 4 caracteres';
        } else if (valorNombre.length > caracterMAximo) {
            nombre.classList.add('is-invalid');
            mensaje_nombre.textContent = 'El nombre debe tener menos de 30 caracteres';
        } else {

            $.ajax({
                type: 'GET',
                url: '/Productos/NombreDuplicado',
                data: { Nombre: valorNombre },
                success: function (result) {
                    if (result === true) {
                        nombre.classList.add('is-invalid');
                        mensaje_nombre.textContent = 'No se puede repetir el nombre.';
                    } else {
                        nombre.classList.add('is-valid');
                    }
                },
                error: function () {
                    // Manejo de errores si la solicitud falla
                    console.log('Error en la solicitud AJAX');
                }
            });
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

    //validar el campo Insumos del recetas
    function ValidarInsumo() {
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
        var ValorCantInsumo = cantidadInsumo.value;


        cantidadInsumo.classList.remove('is-invalid', 'is-valid');
        mensaje_CantInsumo.textContent = ''

        if (ValorCantInsumo.trim() === '') {
            cantidadInsumo.classList.add('is-invalid');
            mensaje_CantInsumo.textContent = 'El campo no puede estar vacio para crear una receta'
        } else if (ValorCantInsumo < 1) {
            cantidadInsumo.classList.add('is-invalid');
            mensaje_CantInsumo.textContent = 'la cantidad de insumos deben ser mayores a 1'
        } else if (ValorCantInsumo > 2000000) {
            cantidadInsumo.classList.add('is-invalid');
            mensaje_CantInsumo.textContent = 'la cantidad de insumos deben ser menores a 2 millones'
        }else {
            cantidadInsumo.classList.add('is-valid');
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
       
        if (formulario_producto.checkValidity()
            && !nombre.classList.contains('is-invalid')
            && !precio.classList.contains('is-invalid')
            && !categoria.classList.contains('is-invalid')
            && !tamano.classList.contains('is-invalid')
        ) {
            const tablaReceta = document.querySelector('#Tabla1');
            console.log(tablaReceta.rows.length)
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

    function EnvioReceta(event) {

        IntefazRecetas()
        nombre_producto.value = nombre.value;
        precio_producto.value = precio.value;
        categoria_producto.value = categoria.value;
        tamano_producto.value = tamano.value;


    }

    function IntefazRecetas() {
        ValidarInsumo();
        ValidarCantInsumo();
    }
    function EnvioExportar(event) {
        Importar_nombre.value = nombre.value;
        Importar_precio.value = precio.value;
        Importar_categoria.value = categoria.value;
        Importar_tamano.value = tamano.value;
    }

    window.addEventListener('load', function () {
        var id = document.getElementById("Id_producto");
       
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
                        // Si el usuario confirma, redirigir a la función "Canselar" en el controlador de Productos
                        $.ajax({
                            type: 'Post',
                            url: '/Productos/Canselar',
                            data: { id: id.value },
                            success: function (result) {


                                window.location.href = cancelarBtn.getAttribute('href');
                            },
                            error: function () {
                                // Manejo de errores si la solicitud falla
                                console.log('Error en la solicitud AJAX');
                            }
                        });

                       
                    }
                });
            });
        }
    });
    enlacesMenu.forEach(enlace => {
        enlace.addEventListener('click', e => {
            console.log(enlace)
            e.preventDefault();
            var href = e.currentTarget.href;
            Swal.fire({
                title: 'Advertencia',
                text: 'Si sales de esta página, perderás los cambios. ¿Estás seguro?',
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                confirmButtonText: 'Sí, salir',
                cancelButtonText: 'Cancelar'
            }).then(result => {
                if (result.isConfirmed) {
                    
                    console.log(href)
                    window.location.href = href;

                }
            });
            
        });
    });
});


