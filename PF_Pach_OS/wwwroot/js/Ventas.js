//function agregarProducto() {
//    var listaProductos = document.getElementById("lista-productos");

//    var listaDetalle = document.createElement("div");
//    listaDetalle.classList.add("lista-detalles");

//    var nuevoProducto = document.createElement("select");
//    nuevoProducto.classList.add("producto"); 
//    nuevoProducto.innerHTML = listaProductos.querySelector(".lista-detalles select").innerHTML;

//    var nuevaCantidad = document.createElement("input");
//    nuevaCantidad.type = "number";

//    var nuevoPrecio = document.createElement("input");
//    nuevoPrecio.type = "number";

//    listaDetalle.appendChild(nuevoProducto);
//    listaDetalle.appendChild(nuevaCantidad);
//    listaDetalle.appendChild(nuevoPrecio);

//    listaProductos.appendChild(listaDetalle);
//}

//function removerProducto() {
//    var listaProductos = document.getElementById("lista-productos");
//    var listaDetalles = listaProductos.getElementsByClassName("lista-detalles");

//    if (listaDetalles.length > 1) {
//        var ultimoProducto = listaDetalles[listaDetalles.length - 1];
//        listaProductos.removeChild(ultimoProducto);
//    }
//}


//function capturarDatos() {
//    var detallesProductos = document.getElementsByClassName("lista-detalles");
//    listaProductos = [];

//    for (var i = 0; i < detallesProductos.length; i++) {
//        var detalle = detallesProductos[i];

//        var producto = detalle.querySelector("select").value;
//        var cantidad = detalle.querySelector("input[type='number']").value;
//        var precio = detalle.querySelectorAll("input[type='number']")[1].value;

//        var detalleVenta = {
//            IdProducto: producto,
//            CantVendida: cantidad,
//            Precio: precio,
//        };

//        listaProductos.push(detalleVenta);
//    }

//    $.ajax({
//        url: '/DetalleVentas/GuardarProductos',
//        type: 'POST',
//        data: { productos: listaProductos },
//        success: function (result) {
//            console.log("Datos enviados exitosamente");
//        },
//        error: function (error) {
//            console.log("Error al enviar los datos");
//        }
//    });
//}