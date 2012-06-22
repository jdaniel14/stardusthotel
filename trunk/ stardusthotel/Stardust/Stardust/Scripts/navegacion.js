$(document).ready(function () {
    var token = localStorage.getItem("token");

    if (token == null) {
        alert("No tienes Acceso");
    } else if (token == "") {
        alert("No tienes Acceso");
        //nsp_redirect();
    } else {
        arma_menu(token);
    }

    function arma_menu(token) {
        var t = localStorage.getItem("token");
        //alert(t); //<-------------------------- entro :D
        for (var i = 0; i < t.length; i++) {
            if (t[i] == '0') {
                $('#menu' + (i + 1)).attr("style", "display:none;");
                $('#menu' + (i + 1)).remove();
            }
        }
    }
});