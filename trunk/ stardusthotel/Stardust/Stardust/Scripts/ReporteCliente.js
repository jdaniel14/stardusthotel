
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {
    $("#buscame").click(despierta);
}



function despierta() {

    var nombrecito = $("#nombre").get(0).value;

    var idHot = $("#ComboHoteles").get(0).value;

    
    if (idHot == 'NN') {
        mostrarError('Faltan Llenar Datos');
    }
    else {
        var enviar = {
            idHotel: idHot,
            nomb: nombrecito
        }

        jsonData = JSON.stringify(enviar);
        console.log(jsonData);

        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "../ReservarHabitacion/consultarUbicacionPersona",
            beforeSend: esperaDatos(),
            success: llegadaDatos
        });
    }
}

function esperaDatos() {
    mostrarEspera();
}

function llegadaDatos(data) {
    $("#espera").dialog("destroy");
    console.log(data);

    if (data.me == "") {

        var lista = data.lista;

        result = "";

        result += '<div class="widget"><div class = "title"><h6>Detalle de cuenta</h6></div>'

        result += '<table cellpadding = "0" cellspacing = "0" width = "100%" class = "sTable" >';

        result += '<thead><tr><td>Nombre</td><td>Dni</td><td>Nro. de Habitacion</td><td>Piso</td><td>Reserva</td></tr></thead>';

        $.each(lista, function (i, item) {

            result += '<tr>';

            result += '<td>' + item.nomb + '</td>';
            result += '<td>' + item.dni + '</td>';
            result += '<td>' + item.nroHab + '</td>';
            result += '<td>' + item.piso + '</td>';
            result += '<td>' + item.reserva + '</td>';

            result += '</tr>';

        });

        result += '</table>';

        result += '</div>';

        $("#resultado").html(result);

    }
    else {
        alert(data.me);
    }

}

