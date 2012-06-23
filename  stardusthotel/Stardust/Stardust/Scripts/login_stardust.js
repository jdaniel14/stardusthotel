$(document).ready(function () {
    localStorage.clear();
    localStorage.setItem("token", "1111100000000000");

    $('#loginButton').click(function () {
        //        alert("soy negro");
        var u = $("#Usuario").val();
        var p = $("#Contrasenia").val();
        var jsonLogin = {
            user: u,
            password: p
        };
        localStorage.setItem("usuario", u);
        localStorage.setItem("pass", p);

        $.ajax({
            type: "POST",
            data: JSON.stringify(jsonLogin),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "/Usuario/LoginResult",
            success: function (data) {
                if (data.length == 0) {
                    $("#control").html("Usuario o Clave incorrecto");
                } else {

                    var idUsuario = data.ID;
                    var token = data.estado;

                    if (idUsuario)
                        localStorage.setItem("idUsuario", idUsuario);
                    else
                        localStorage.setItem("idUsuario", "noooooo u.u");

                    if (token)
                        localStorage.setItem("token", token);
                    else
                        localStorage.setItem("token", "0000000000000000");

                    var nombre = data.nombres + ' ' + data.apPat + ' ' + data.apMat;
                    localStorage.setItem("nombre", nombre);
                    
                    var url = "/Home/Index";
                    window.location.href = url;
                }
            }, error: function () {
                $("#control").html("Usuario o Clave incorrecto");
            }
        });
    });
});