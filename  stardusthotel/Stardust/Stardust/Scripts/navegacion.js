$(document).ready(function () {
    var token = localStorage.getItem("token");

    // Setear nombre arriba del layout
    var nombre = localStorage.getItem("nombre");
    $("#nombreUsuario").html("Bienvenido " + nombre + "!");

    // Crear link para redirigir a tu detalle de usuario
    var idUsuario = localStorage.getItem("idUsuario");
    if (idUsuario != null)
        $("#verUsuario").attr("href", "/Usuario/Details/" + idUsuario);

    // Setear menus que puedes ver según tu perfil
    if (token == null) {
        alert("No tienes Acceso");
    } else if (token == "") {
        alert("No tienes Acceso");
    } else {
        arma_menu(token);
    }

    function arma_menu(token) {
        var t = localStorage.getItem("token");

        for (var i = 0; i < t.length; i++) {
            if (t[i] == '0') {
                var menu = $('#menu' + (i + 1));
                $(menu).attr("style", "display:none;");
            }
        }
    }
});