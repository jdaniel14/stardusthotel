var reserva;
var x;
var habitaciones = new Array();
var cantXHabit = new Array();
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {
    $("#footer").hide();

    $("#buscarReserva").click(comienzaElChongo);    
}

function comienzaElChongo() {

    reserva = $("#nroReserva").get(0).value;

    var Envio = {
        idHotel: "1",
        idReserva: reserva
    }

    jsonData = JSON.stringify(Envio);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "CheckIn",
        beforeSend: esperaRecibirDatosCheckIn(),
        success: llegadaDatosCheckIn
    });
}

function esperaRecibirDatosCheckIn(){
}

function llegadaDatosCheckIn(data) {
    console.log(data);

    result = "";

    result += '<div class = "widget"><div class="title"><h6>Datos de la reserva</h6></div>';

    result += '<div class="formRow"><span>Doc. de identidad</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + data.doc + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Nombre</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + data.nomb + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha de Registro</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + data.fechaReg + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha de llegada</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + data.fechaLleg + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '</div>';

    $("#imprimeDatos").html(result);

    var lista = data.lista;

    result = "";
    var k = 0;
    
    $.each(lista, function (i, item) {

        k++;
        result += '<div class = "widget">';
        result += '<div class = "title">';
        result += '<img src="../../Content/images/icons/dark/frames.png" alt="" class="titleIcon" />';
        result += '<h6>' + item.nombTipHab + '</h6></div>';


        var numPersonas = item.nroPers;

        result += '<div class="formRow"><span>Cantidad de personas a registrar</span>';
        result += '<div class = "formRight">';
        result += '<span id = "cantHabit' + k + '">' + item.nroPers + '</span>';
        result += '</div><div class="clear"></div></div>';

        var listaHabitaciones = item.lista;

        var m = 0;

        $.each(listaHabitaciones, function (j, cosito) {

            var id = cosito.idHab;

            habitaciones.push(id);
            cantXHabit.push(numPersonas);

            m++;
            result += '<div class="widget"><div class = "title"><h6>:: Habitacion ' + cosito.numero + '</h6></div>'

            result += '<table cellpadding = "0" cellspacing = "0" width = "100%" class = "sTable" >';

            result += '<thead><tr><td>Doc. de Identidad</td><td>Nombres</td></tr></thead>';

            result += '<tbody>';

            var n = 0;
            for (n = 0; n < numPersonas; n++) {


                result += '<tr><td align = "center">';
                result += '<input type="text" class = "dnicampo" id="DNICliente' + cosito.idHab + '-' + n + '"/>';
                result += '</td><td align = "center">';

                result += '<input type="text" class = "campo" id="nombCliente' + cosito.idHab + '-' + n + '"/>';
                result += '</td></tr>';
            }

            result += '</tbody></table>';
            result += '</div>';

        });

        result += '</div>';

    });

    result += '<br /><input id = "registrarReserva" type="submit" value="Registrar" class = "redB" />';
    console.log(result);
    $("#accordion").html(result);

    console.log(habitaciones);
    console.log(cantXHabit);

    $("#registrarReserva").click(enviarDatos);
}

function enviarDatos() {

    var a;
    var b;
    var n = 0;
    var listaDev = new Array();

    habitaciones.forEach(function (elemento) {



        var y = 0;
        for (y = 0; y < cantXHabit[n]; y++) {
            a = "#DNICliente";
            a += elemento;
            a += "-";

            b = "#nombCliente";
            b += elemento;
            b += "-";

            a += y;
            b += y;


            var valorA = $(a).attr("value");
            var valorB = $(b).attr("value");

            var lista = {
                idReserva: reserva,
                idHab: elemento,
                nombresYAp: valorB,
                dni: valorA
            }

            listaDev.push(lista);

        }
        n++;

    });

    console.log(listaDev);

    var pasar = {
        lista : listaDev
    }

    jsonData = JSON.stringify(pasar);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "ResgitrarClientesCheckIn",
        beforeSend: esperaConfirmacion(),
        success: Confirma
    });


}

function esperaConfirmacion(){
}

function Confirma(data) {
    console.log(data);    
    alert('OK');
}






