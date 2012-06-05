
var x;
x = $(document);
x.ready(inicializarEventos);
function inicializarEventos() {
    $("#FechaLlegada").datepicker();
    $("#FechaSalida").datepicker();
    $("#fieldAeropuerto").hide();
    $("#registrarAeropuerto").click(mostrarFieldAeropuerto);


    $("#checkAerop").click(function (event) {
        if ($(this).is(":checked")) {
            $("#checkAerop:checkbox:not(:checked)").attr("checked", "checked");
            $("#fieldAeropuerto").show("fast");
        } else {
            $("#checkAerop:checkbox:checked").removeAttr("checked");
            $("#fieldAeropuerto").hide("slow");
        }
    });
    var x;
    var idHotel = "2";
    $.ajax({
        type: "POST",
        data: JSON.stringify(idHotel),
        dataType:"json",
        contentType: "application/json; charset=utf-8",
        url: "ReservarHabitacion/infoReserva",
        beforeSend:inicioEnvioTipoHotel,
        success: llegadaTipoHabitacion
    });
    

    x = $("#FechaLlegada");
    x.focus(clickFechain);
    x = $("#FechaSalida");
    x.focus(clickFechaout);
}

function inicioEnvioTipoHotel() {
    var x = $("#tablaTipos");
    x.html('<img src="http://www.coliseogym.com/Gym/Iconos/cargando.gif">');
//	$( "#progressbar" ).progressbar({
//		value: 100
//	});
    //alert('Cargando...');
}

function llegadaTipoHabitacion(data) {    
    var result = "";
    var i = 0;
    var cant = 0;

    $.each(data, function (i, item) {
        cant = cant + 1;
        result += '<tr><td> ' + item.nombreTipoHab + '</td>';
        result += '<td>' + '<select id= "numHabitSelect' + item.idTipoHab + '" class = "numHabitSelect">';

        for (i = 0; i <= item.numPos; i++) {
            result += '<option value = "' + i + '" id = "idTipoHab'+ i + item.idTipoHab +'">' + i + '</option>';
        }

        result += '</select></td><td><span id = "subtotal' + item.idTipoHab + '">'+ 0 + '</span></td></tr>';
        

    });

    $('#tablaTipos').html(result);
        
    $('#numHabitSelect1').change(cambiarSubTotal1);
    $('#numHabitSelect2').change(cambiarSubTotal2);
    $('#numHabitSelect3').change(cambiarSubTotal3);
    $('#numHabitSelect4').change(cambiarSubTotal4);
    $('#numHabitSelect5').change(cambiarSubTotal5);

    /*$.each(data, function (i, item) {
        result += '<option value= "' + item.idTipo + '">' + item.nombreTipo + '</option>';

    });
    $('#tipoHabitacion').html(result);
    $("#labelTipoHabitacion").show();
    */  
}

function cambiarSubTotal1() {
    var id;
    var x = $('#numHabitSelect1.numHabitSelect option:selected').attr("value")
  //  alert(x);
    $('#subtotal1').text(10 * x);
//    alert(10 * x);   
    //alert(":D");    
    $('#Total').text(
        $('#numHabitSelect1.numHabitSelect option:selected').attr("value")* 10+
        $('#numHabitSelect2.numHabitSelect option:selected').attr("value")* 10+
        $('#numHabitSelect3.numHabitSelect option:selected').attr("value")* 10+
        $('#numHabitSelect4.numHabitSelect option:selected').attr("value")* 10+
        $('#numHabitSelect5.numHabitSelect option:selected').attr("value")* 10
    );
}

function cambiarSubTotal2() {
    var id;
    var x = $('#numHabitSelect2.numHabitSelect option:selected').attr("value");
    //alert(x);
    $('#subtotal2').text(10 * x);
    //alert(":D");    
    $('#Total').text(
        $('#numHabitSelect1.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect2.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect3.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect4.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect5.numHabitSelect option:selected').attr("value") * 10
    );
}

function cambiarSubTotal3() {
    var id;
    var x = $('#numHabitSelect3.numHabitSelect option:selected').attr("value");
    //alert(x);
    $('#subtotal3').text(10 * x);
    //alert(":D");    
    $('#Total').text(
        $('#numHabitSelect1.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect2.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect3.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect4.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect5.numHabitSelect option:selected').attr("value") * 10
    );
}

function cambiarSubTotal4() {
    var id;
    var x = $('#numHabitSelect4.numHabitSelect option:selected').attr("value");
    //alert(x);
    $('#subtotal4').text(10 * x);
    //alert(":D");    
    $('#Total').text(
        $('#numHabitSelect1.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect2.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect3.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect4.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect5.numHabitSelect option:selected').attr("value") * 10
    );
}

function cambiarSubTotal5() {
    var id;
    var x = $('#numHabitSelect5.numHabitSelect option:selected').attr("value");
    //alert(x);
    $('#subtotal5').text(10 * x);
    //alert(":D");    
    $('#Total').text(
        $('#numHabitSelect1.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect2.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect3.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect4.numHabitSelect option:selected').attr("value") * 10 +
        $('#numHabitSelect5.numHabitSelect option:selected').attr("value") * 10
    );
}

function clickFechain() {
    var x = $("#FechaLlegada");
    x.attr("value", "");
}

function clickFechaout() {
    var x = $("#FechaSalida");
    x.attr("value", "");
}

function mostrarFieldAeropuerto() {
    $("#fieldAeropuerto").show();
}


//     $(function () {
//         $("#FechaLlegada").datepicker();
//     });


//    $(function () {
//        $("#FechaSalida").datepicker();
//    });
