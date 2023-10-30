document.addEventListener('DOMContentLoaded', function () {

    var fomulario = document.getElementById('myForm');
    var nomInsumoInput = document.getElementById('NomInsumo');
    var medidaInput = document.getElementById('Medida')
    var nomInsumoFeedback = document.getElementById('nombreFeedback');
    var medidaFeedback = document.getElementById('medidaFeedback')

    function CrearInsumo(event) {
        event.preventDefault();
        if (fomulario.checkValidity() && !nomInsumoInput.classList.contains('is-invalid') && !medidaInput.classList.contains('is-invalid')) {
            Swal.fire({
                title: '¡Éxito!',
                text: 'Insumo Registrado Exitosamente',
                timer: 1500,
                icon: 'success',
                showConfirmButton: false,
            }).then((result) => {
                fomulario.removeEventListener('submit', CrearInsumo);
                fomulario.submit();
            })
        } else {
            Toast.fire({
                icon: 'error',
                title: 'Formulario inválido'
            });
            validarNomInsumo();
            validarMedida();
        }
    }


    fomulario.addEventListener('submit', CrearInsumo);

    nomInsumoInput.addEventListener('input', function () {
        validarNomInsumo();
    });

    medidaInput.addEventListener('change', function () {
        validarMedida();
    });

    function validarMedida() {
        // Obtener el valor seleccionado del campo select
        var medidaSeleccionada = medidaInput.value;

        // Restablecer los estilos y mensajes de validación
        medidaInput.classList.remove('is-invalid', 'is-valid');
        medidaFeedback.textContent = '';

        // Validar si no se ha seleccionado ninguna opción
        if (medidaSeleccionada === '') {
            medidaInput.classList.add('is-invalid');
            medidaFeedback.textContent = 'Por favor, elige una opción.';
        } else {
            // El campo es válido
            medidaInput.classList.add('is-valid');
        }
    }

    function validarNomInsumo() {
        var nomInsumo = nomInsumoInput.value;

        // Restablecer los estilos y mensajes de validación
        nomInsumoInput.classList.remove('is-invalid', 'is-valid');
        nomInsumoFeedback.textContent = '';

        // Validar si el campo está vacío
        if (nomInsumo.trim() === '') {
            nomInsumoInput.classList.add('is-invalid');
            nomInsumoFeedback.textContent = 'Por favor ingrese el nombre del insumo.';
        } else if (/^\s/.test(nomInsumo)) {
            nomInsumoInput.classList.add('is-invalid');
            nomInsumoFeedback.textContent = 'No se puede comenzar con un espacio en blanco.';
        }
        // Validar si el campo contiene números
        else if (/\d/.test(nomInsumo)) {
            nomInsumoInput.classList.add('is-invalid');
            nomInsumoFeedback.textContent = 'No se pueden ingresar números.';
        }
        // Validar longitud del nombre
        else if (nomInsumo.length < 3 || nomInsumo.length > 20) {
            nomInsumoInput.classList.add('is-invalid');
            nomInsumoFeedback.textContent = 'El nombre debe tener entre 3 y 20 caracteres.';
        } else if (/[^a-zA-Z0-9\s]/.test(nomInsumo)) {
            nomInsumoInput.classList.add('is-invalid');
            nomInsumoFeedback.textContent = 'No se pueden ingresar caracteres especiales.';
        }
        else {
            // El campo es válido
            nomInsumoInput.classList.add('is-valid');
        }

        $.ajax({
            type: 'GET',
            url: '/Insumos/NombreDuplicado',
            data: { Nombre: nomInsumo },
            success: function (result) {
                if (result === true) {
                    nomInsumoInput.classList.add('is-invalid');
                    nomInsumoFeedback.textContent = 'No se puede repetir el nombre.';
                } else {
                    nomInsumoInput.classList.add('is-valid');
                }
            },
            error: function () {
                // Manejo de errores si la solicitud falla
                console.log('Error en la solicitud AJAX');
            }
        });
    }



    var tabla = document.getElementById("Datos");

    // Get all the table rows
    var filas = Array.from(tabla.getElementsByTagName("tr"));

    // Reverse the array of rows
    filas.reverse();

    // Append the reversed rows back to the table
    filas.forEach(function (row) {
        tabla.appendChild(row);
    });


    //Esta variable se usa en la funcion HabilitarDeshabilitar del archivo js 

    //Manejar el clic en los botones de Habilitar / Deshabilitar 
    $('.habilitado, .deshabilitado').on('click', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');

        $.ajax({
            type: 'GET',
            url: url,
            success: function (data) {
                HabilitarDeshabilitar(data);
            },
            error: function () {
                //Manejo de errores si la solicitud falla 
                console.log('Error en la solicitud AJAX');
            }
        });
    });
}); // Aquí se agrega el paréntesis de cierre



$(document).ready(function () {
    // Manejar el evento de cambio en el campo de búsqueda mientras se escribe
    $('#searchInput').on('input', function () {
        var searchText = $(this).val().trim().toLowerCase();

        // Filtrar las filas de la tabla que coincidan con el término de búsqueda
        $('tbody tr').each(function () {
            var productName = $(this).find('td:eq(0)').text().toLowerCase();
            var Cantiad = $(this).find('td:eq(1)').text().toLowerCase();
            var Medida = $(this).find('td:eq(2)').text().toLowerCase();


            if (productName.includes(searchText) ||
                Cantiad.includes(searchText)||
                Medida.includes(searchText)
            ) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });
});



// Manejar la respuesta JSON de la acción HabilitarDeshabilitar
function HabilitarDeshabilitar(data) {
    if (data.success) {
        Toast.fire({
            icon: 'success',
            title: ' ¡LISTO!'
        }).then((result) => {
            window.location.href = indexUrl;
        })
    } else {
        // Mostrar una alerta de SweetAlert con el mensaje de error
        Swal.fire({
            title: 'Upss...',
            timer: 2500,
            showConfirmButton: false,
            text: data.message,
            icon: 'error',
        });
    }
}

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

$(document).on("click", "button[data-target='#editarInsumoModal']", function () {
    var idInsumo = $(this).data("idinsumo");
    var modalBody = $("#editarInsumoModalBody");
    var url = "/Insumos/EditarInsumo?id=" + idInsumo;
    modalBody.empty();

    modalBody.load(url);
});

