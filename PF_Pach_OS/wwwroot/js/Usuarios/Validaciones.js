document.addEventListener('DOMContentLoaded', function () {

    var Rol = document.getElementById("Role");
    var mensaje_Rol = document.getElementById("mensaje_Rol")

    var TipoDocumento = document.getElementById("DocumentType");
    var mensaje_TipoDocumento = document.getElementById("mensaje_TipoDocumento")

    var NumeroDocumento = document.getElementById("DocumentNumber");
    var mensaje_NumeroDocumento = document.getElementById("mensaje_NumeroDocumento")

    var Nombre = document.getElementById("FirstName");
    var mensaje_Nombre = document.getElementById("mensaje_Nombre")

    var Apellido = document.getElementById("LastName");
    var mensaje_Apellido = document.getElementById("mensaje_Apellido")

    var Correo = document.getElementById("Email");
    var mensaje_Correo = document.getElementById("mensaje_Correo")

    var Contraseña = document.getElementById("Password");
    var mensaje_Contraseña = document.getElementById("mensaje_Contraseña")

    var ConfirmarContraseña = document.getElementById("ConfirmPassword");
    var mensaje_ConfirmarContraseña = document.getElementById("mensaje_ConfirmarContraseña")


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

    Rol.addEventListener('change', ValidarRol);
    TipoDocumento.addEventListener('change', ValidarTipoDoc);
    NumeroDocumento.addEventListener('input', ValidarNumeroDoc);
    Nombre.addEventListener('input', ValidarNombre);
    Apellido.addEventListener('input', ValidarApellido);
    Correo.addEventListener('input', ValidarCorreo);
    Contraseña.addEventListener('input', ValidarContraseña);
    ConfirmarContraseña.addEventListener('input', ValidarConfirmarContraseña);




    function ValidarRol() {
        var ValorRol = Rol.value;
        Rol.classList.remove('is-invalid', 'is-valid');
        mensaje_Rol.textContent = '';

        if (ValorRol.trim() === '') {
            Rol.classList.add('is-invalid');
            mensaje_Rol.textContent = 'Por favor seleccione un Rol';

        } else {
            Rol.classList.add('is-valid');
        }
    }

    function ValidarTipoDoc() {
        var ValorTipoDoc = TipoDocumento.value;
        TipoDocumento.classList.remove('is-invalid', 'is-valid');
        mensaje_TipoDocumento.textContent = '';

        if (ValorTipoDoc.trim() === '') {
            TipoDocumento.classList.add('is-invalid');
            mensaje_TipoDocumento.textContent = 'Por favor seleccione un Tipo de documento';

        } else {
            TipoDocumento.classList.add('is-valid');
        }
    }

    function ValidarNumeroDoc() {
        var ValorNumeroDoc = NumeroDocumento.value;
        var tipoDocumento = TipoDocumento.value;

        var minCaracteres = 5;
        var maxCaracteres = 50;
        const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(ValorNumeroDoc);


        NumeroDocumento.classList.remove('is-invalid', 'is-valid');
        mensaje_NumeroDocumento.textContent = '';

        if (ValorNumeroDoc.trim() === '') {
            NumeroDocumento.classList.add('is-invalid');
            mensaje_NumeroDocumento.textContent = 'El campo no puede estar vacío'
        } else if (ValorNumeroDoc.length < minCaracteres) {
            NumeroDocumento.classList.add('is-invalid');
            mensaje_NumeroDocumento.textContent = 'El numero documento debe de ser de al menos 6 caracteres'
        } else if (ValorNumeroDoc.length > maxCaracteres) {
            NumeroDocumento.classList.add('is-invalid');
            mensaje_NumeroDocumento.textContent = 'El numero documento debe de ser de maximo 50 caracteres'
        } else if (tipoDocumento == 'Cedula' || tipoDocumento == 'Tarjeta') {
            const hasLetters = /[a-zA-Z]/.test(ValorNumeroDoc);
            if (hasLetters) {
                NumeroDocumento.classList.add('is-invalid');
                mensaje_NumeroDocumento.textContent = 'El tipo de documento Cedula o tarjeta de identidad no pude poseer letras'

            }
        } else if (hasSpecialChar) {
            NumeroDocumento.classList.add('is-invalid');
            mensaje_NumeroDocumento.textContent = 'El numero documento no puede tener carateres especiales'

        } else {
            NumeroDocumento.classList.add('is-valid');
        }
        //************************************************/
        //Poner Funcion para Validacion de Documento Repetido
        //************************************************/

    }

    function ValidarNombre() {
        var ValorNombre = Nombre.value;

        var minCaracteres = 3;
        var maxCaracteres = 20;
        const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(ValorNombre);
        const hasNumber = /(?=.*\d)/.test(ValorNombre);

        Nombre.classList.remove('is-invalid', 'is-valid');
        mensaje_Nombre.textContent = '';

        if (ValorNombre.trim() === '') {
            Nombre.classList.add('is-invalid');
            mensaje_Nombre.textContent = 'El campo no puede estar vacío';
        } else if (ValorNombre.length < minCaracteres) {
            Nombre.classList.add('is-invalid');
            mensaje_Nombre.textContent = 'El nombre debe tener al menos 3 caracteres';
        } else if (ValorNombre.length > maxCaracteres) {
            Nombre.classList.add('is-invalid');
            mensaje_Nombre.textContent = 'El nombre debe ser menor 20 caracteres';
        } else if (hasSpecialChar) {
            Nombre.classList.add('is-invalid');
            mensaje_Nombre.textContent = 'El nombre no debe tener caracteres especiales';
        } else if (hasNumber) {
            Nombre.classList.add('is-invalid');
            mensaje_Nombre.textContent = 'El nombre no debe tener numeros';
        } else {
            Nombre.classList.add('is-valid');
        }

    }

    function ValidarApellido() {
        var ValorApellido = Apellido.value;

        var minCaracteres = 3;
        var maxCaracteres = 20;
        const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(ValorApellido);
        const hasNumber = /(?=.*\d)/.test(ValorApellido);

        Apellido.classList.remove('is-invalid', 'is-valid');
        mensaje_Apellido.textContent = '';

        if (ValorApellido.trim() === '') {
            Apellido.classList.add('is-invalid');
            mensaje_Apellido.textContent = 'El campo no puede estar vacío';
        } else if (ValorApellido.length < minCaracteres) {
            Apellido.classList.add('is-invalid');
            mensaje_Apellido.textContent = 'El apellido debe tener al menos 3 caracteres';
        } else if (ValorApellido.length > maxCaracteres) {
            Apellido.classList.add('is-invalid');
            mensaje_Apellido.textContent = 'El apellido debe ser menor 20 caracteres';
        } else if (hasSpecialChar) {
            Apellido.classList.add('is-invalid');
            mensaje_Apellido.textContent = 'El apellido no debe tener caracteres especiales';
        } else if (hasNumber) {
            Apellido.classList.add('is-invalid');
            mensaje_Apellido.textContent = 'El apellido no debe tener numeros';
        } else {
            Apellido.classList.add('is-valid');
        }
    }

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