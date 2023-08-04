agregarProducto()

function agregarProducto() {
    var listaProductos = document.getElementById("lista-productos")

    var nuevoProducto = document.createElement("div");
    nuevoProducto.classList.add("lista-detalles");

    nuevoProducto.innerHTML = "<select>Categoria</select><select>Producto</select><input type='number' class='formularios'><label>Precio</label>";

    listaProductos.appendChild(nuevoProducto);
}

function removerProducto() {
    var listaProductos = document.getElementById("lista-productos")
    var listaDetalles = document.getElementsByClassName("lista-detalles");

    if (listaDetalles.length > 1) {
        var ultimoDetalle = listaDetalles[listaDetalles.length - 1];

        listaProductos.removeChild(ultimoDetalle)
    }
    
}