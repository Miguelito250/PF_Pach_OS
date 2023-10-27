
var fomulario = document.getElementById('editarInsumoForm');
var nomInsumoInput = document.getElementById('NomInsumoEditar');
var nomInsumoFeedback = document.getElementById('nombreFeedbackEitar');

function EditarInsumo(event) {
    event.preventDefault();
    if (fomulario.checkValidity() && !nomInsumoInput.classList.contains('is-invalid')) {
        Swal.fire({
            title: '¡Éxito!',
            text: 'Insumo actualizado',
            timer: 1500,
            icon: 'success',
            showConfirmButton: false,
        }).then((result) => {
            console.log('Formulario válido');
            fomulario.removeEventListener('submit', EditarInsumo);
            fomulario.submit();
        })        
    } else {
        Toast.fire({
            icon: 'error',
            title: 'Formulario inválido'
        });
        console.log('Formulario inválido');
        validarNomInsumo();
    }
}

fomulario.addEventListener('submit', EditarInsumo);

nomInsumoInput.addEventListener('input', function () {
    console.log("wenps")
    validarNomInsumo();
});


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