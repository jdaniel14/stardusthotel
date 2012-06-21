﻿
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {
    $("#FechaLlegada").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#FechaSalida").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#buscame").click(iniciarFlujo);
}


function iniciarFlujo() {
    var fechaInicio = $("#FechaLlegada").attr("value");
    var fechaFinal = $("#FechaSalida").attr("value");
        
    var Hotel = {
        idHotel: "1",
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
        url: "DIRECCIONURL",
        beforeSend: esperaDatos(),
        success: llegadaDatos
    });
}


function esperaDatos() {
}


function llegadaDatos(data) {

    console.log(data);

    if (data.me == "") {
    
        var result = ""; //el poderoso result :)

        var listaHab = data.listaHab;

        var cantDias = 0;
        var j = 0;

        var el = listaHab[0];

        $.each(el.listaFechas, function (i, item) {
            cantDias++;
        });

        result += '<div class="widget"><div class = "title"><img src="../../Content/images/icons/dark/frames.png" alt="" class="titleIcon" />';
        result += '<h6>Reporte Habitaciones</h6></div>'

        result += '<table cellpadding = "0" cellspacing = "0" width = "100%" class = "sTable" >';

        result += '<thead><tr>';

        result += '<td>N° Habitacion</td>';

        for (j = 0; j < cant; j++){
            result += '<td></td>';
        }

        result += '</tr></thead>';        

        result += '<tbody>';

        $.each(listaHab, function (i, item) {

            result += '<tr>';

            result += '<td>' + elemento.nHabit + '</td>';

            var fechas = item.listaFechas;

            $.each(fechas, function (i, led) {

                result += '<td>';

                if (led.estado == 0){
                    
                   result += ':(';
                }
                else{

                    result += ':)';
                }

                result += '</td>';

            });

            result += '</tr>';

        });



        result += '</tbody>';

        result += '</table>';

        result += '</div>';

        $("#resultados").html(result);




    }
    else {
        alert(data.me);
    }

}