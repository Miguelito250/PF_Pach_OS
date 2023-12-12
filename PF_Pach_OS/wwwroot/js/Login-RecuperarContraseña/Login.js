function ValidarCampos() {
    const correo = correoInput.value.trim();
    const contrasena = contrasenaInput.value.trim();
    const divInfo = document.getElementById("botonDeshabilitado");
    const validacionCorreo = document.getElementById("validacion-correo");


    if (correo !== "" && contrasena !== "") {
        botonEnviar.disabled = false;
        divInfo.removeAttribute("title")

    } else {
        botonEnviar.disabled = true;
        divInfo.setAttribute("title", "Complete los campos para enviar la información")

    }
    if (!correo.includes('@')) {
        correoInput.classList.add("is-invalid");
        validacionCorreo.innerHTML = "Ingrese un correo que contenga '@'";
    } else {
        correoInput.classList.remove("is-invalid");
        validacionCorreo.innerHTML = "";
    }
}
function DescargarAPK() {
    fetch('../APK/app.release.apk')
        .then(response => response.blob())
        .then(blob => {
            const url = URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = 'app-release.apk';
            a.click();
            URL.revokeObjectURL(url);
        })
        .catch(error => {
            console.error('Error al descargar el archivo:', error);
        });
}

const correoInput = document.getElementById("Input_Email");
const contrasenaInput = document.getElementById("Input_Password");
const botonEnviar = document.getElementById("login-submit");
const descargarAPK = document.getElementById("apk-download");

botonEnviar.disabled = true

correoInput.addEventListener("input", ValidarCampos);
contrasenaInput.addEventListener("input", ValidarCampos);
descargarAPK.addEventListener("click", DescargarAPK);