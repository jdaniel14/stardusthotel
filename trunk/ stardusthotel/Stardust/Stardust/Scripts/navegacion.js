$(document).ready(function () {
    var token = localStorage.getItem("token");

    // Setear nombre arriba del layout
    var nombre = localStorage.getItem("nombre");
    $("#nombreUsuario").html(nombre);

    // Crear link para redirigir a tu detalle de usuario
    var idUsuario = localStorage.getItem("idUsuario");
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

        //alert(t); //<-------------------------- entro :D
        for (var i = 0; i < t.length; i++) {
            if (t[i] == '0') {
                $('#menu' + (i + 1)).attr("style", "display:none;");
                //                $('#menu' + (i + 1)).remove();
            }
        }
    }
});