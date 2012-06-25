$("#logout").click(function () {
    localStorage.clear();
});

$(document).ready(function () {
    var token = localStorage.getItem("token");

    // Setear nombre arriba del layout
    var nombre = localStorage.getItem("nombre");
    $("#nombreUsuario").html("Bienvenido " + nombre + "!");

    var idUsuario = localStorage.getItem("idUsuario");
    if (idUsuario != null) {
        // Crear link para redirigir a tu detalle de usuario
        $("#verUsuario").attr("href", "/Usuario/Details/" + idUsuario);
        // Crear el link para el logout
        $("#logout").attr("href", "/Logout/Index/" + idUsuario);
    }

    // Setear menus que puedes ver según tu perfil
    if (token == null) {
        alert("No tienes permiso de entrar aquí");
        arma_menu("000000000000000000000000");
    } else if (token == "") {
        arma_menu("000000000000000000000000");
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