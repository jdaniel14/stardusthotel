
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {
    $("#FechaLlegada").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#FechaSalida").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#buscame").click(iniciarFlujo);
    $("#resultados").hide();
}


function iniciarFlujo() {
    var fechaInicio = $("#FechaLlegada").attr("value");
    var fechaFinal = $("#FechaSalida").attr("value");
    var telo = $("#ComboHoteles").get(0).value;

    if ((fechaInicio != "") && (fechaFinal != "") && (telo != "")) {

        var Hotel = {
            idHotel: telo,
            fechaIni: fechaInicio,
            fechaFin: fechaFinal
        }
        var jsonData = JSON.stringify(Hotel);
        console.log(jsonData);

        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "ListaHabitacion",
            beforeSend: esperaDatos(),
            success: llegadaDatos
        });
    }
    else {
        mostrarError("Faltan Datos");
        $("#resultados").hide("slow");
    }
}


function esperaDatos() {
}


function llegadaDatos(data) {

    console.log(data);
    $("#resultados").hide("slow");
    //if (data.me == "") {
    
        var result = ""; //el poderoso result :)

        //var listaHab = data.listaHab;

        var cantDias = 0;
        var j = 0;

        //var el = listaHab[0];

        

        $.each(data[0].listaFechas, function (i, item) {
            cantDias++;
        });

        result += '<div class="widget"><div class = "title"><img src="../../Content/images/icons/dark/frames.png" alt="" class="titleIcon" />';
        result += '<h6>Reporte Habitaciones</h6></div>'

        result += '<table cellpadding = "0" cellspacing = "0" width = "100%" class = "sTable" >';

        result += '<thead><tr align = "center">';

        result += '<td align = "center">N° Habitacion</td>';

        for (j = 0; j < cantDias; j++){
            result += '<td align = "center">Dia ' + j + '</td>';
        }

        result += '</tr></thead>';        

        result += '<tbody>';

        $.each(data, function (i, item) {

            result += '<tr>';

            result += '<td align = "center">' + item.nHabit + '</td>';

            var fechas = item.listaFechas;

            $.each(fechas, function (i, led) {

//                result += '<td><span>';
//                result += led.estado;
//                result += '</span></td>';

                if (led.estado != "Libre") {
                    result += '<td align = "center"><span><font color="#FF0000">';
                    result += ':(';
                    result += '</font></span></td>';
                }
                else {
                    result += '<td align = "center"><span><font color="#008000">';
                    result += ':)';
                    result += '</font></span></td>';
                }



            });

            result += '</tr>';

        });



        result += '</tbody>';

        result += '</table>';

        result += '</div>';

        $("#resultados").html(result);
        $("#resultados").show("slow");



    //}
    //else {
    //    alert(data.me);
    //}

    }


