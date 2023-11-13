document.addEventListener('DOMContentLoaded', function () {
  
    var Correo = document.getElementById("Email");
    var mensaje_Correo = document.getElementById("mensaje_Correo")

   
    Correo.addEventListener('input', ValidarCorreo)

    function ValidarCorreo() {
        var ValorCorreo = Correo.value;
        const esValido = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(ValorCorreo);

        mensaje_Correo.textContent = '';
        Correo.classList.remove('is-invalid', 'is-valid');
        if (ValorCorreo.trim() === '') {
            Correo.classList.add('is-invalid');
            mensaje_Correo.textContent = 'El campo no puede ir vacio'
        } else if (!esValido) {
            Correo.classList.add('is-invalid');
            mensaje_Correo.textContent = 'Por favor ingrese un correo electrónico válido.'
        }
        $.ajax({
            url: '/AspNetUsers/NombreDuplicado',
            type: 'GET',
            data: { Coreo: Correo.value },
            success: function (result) {
                if (result === true) {
                    Correo.classList.add('is-invalid');
                    mensaje_Correo.textContent = 'El Correo ya existe'


                } else {
                    Correo.classList.remove('is-invalid');

                }
            },
            error: function () {
                // Manejo de errores si la solicitud falla
                console.log('Error en la solicitud AJAX');
            }
        });

    }
});