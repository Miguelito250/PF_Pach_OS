
$(document).ready(function () {
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

    var miModal = new bootstrap.Modal(document.getElementById('exampleModal'), {
        keyboard: false
    });

    $('#btnNuevo').click(function () {
        var modalBody = $("#modal-body");
        $('#modal-title').text('Crear Proveedor');
        var url = "/Proveedores/Create";
        modalBody.empty();
        modalBody.load(url);

        miModal.show();
    });
    $('.btnEditar').click(function () {
        let id = $(this).data("id")
        console.log(id)
        var modalBody = $("#modal-body");
        $('#modal-title').text('Editar Proveedor');
        var url = "/Proveedores/Edit?" + $.param({ id: id });
        modalBody.empty();
        modalBody.load(url);

        miModal.show();
    });

    $('#Habilitar, #Deshabilitar').on('click', function (e) {
        e.preventDefault();
        var form = $(this);

        Toast.fire({
            icon: 'success',
            title: ' ¡LISTO!'
        }).then((result) => {
            form.submit();
        })
    });

});
