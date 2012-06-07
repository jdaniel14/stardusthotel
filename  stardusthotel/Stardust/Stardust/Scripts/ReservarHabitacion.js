
var x;
x = $(document);
x.ready(inicializarEventos);
function inicializarEventos() {


//    $('#contenido_pestanas div').css('position', 'absolute').not(':first').hide();
//    $('#contenido_pestanas ul li:first a').addClass('aqui');
//    $('#contenido_pestanas a').click(function(){
//        $('#contenido_pestanas a').removeClass('aqui');
//        $(this).addClass('aqui');
//        $('#contenido_pestanas div').fadeOut(350).filter(this.hash).fadeIn(350);
//        return false;
//    });

    //$("#continuarP2").click(continuar2);
    $("#pestana3").hide();
    $("#continuarP3").click(inicializarMostreo);
    $("#FechaLlegada").datepicker({dateFormat: 'dd-mm-yy'});
    $("#FechaSalida").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#fieldAeropuerto").hide();
    $("#registrarAeropuerto").click(mostrarFieldAeropuerto);
    $('#HoraLlegada').timepicker({});

    $("#checkAerop").click(function (event) {
        if ($(this).is(":checked")) {
            $("#checkAerop:checkbox:not(:checked)").attr("checked", "checked");
            $("#fieldAeropuerto").show("slow");
        } else {
            $("#checkAerop:checkbox:checked").removeAttr("checked");
            $("#fieldAeropuerto").hide("slow");
        }
    }); 

    var x;
    var Hotel = {
        idHotel : "2"
    }
    var jsonData = JSON.stringify(Hotel);
    //alert('ant');
    console.log(jsonData);
    //alert('desp');
    $.ajax({
        type: "POST",
        data: jsonData,
        dataType:"json",
        contentType: "application/json; charset=utf-8",
        url: "ReservarHabitacion/infoReserva",
        beforeSend:inicioEnvioTipoHotel(),
        success: llegadaTipoHabitacion
    });
    

    x = $("#FechaLlegada");
    x.focus(clickFechain);
    x = $("#FechaSalida");
    x.focus(clickFechaout);
}

function inicioEnvioTipoHotel() {
    //console.log(data);
    var x = $("#tablaTipos");
    x.html('<img src="http://www.coliseogym.com/Gym/Iconos/cargando.gif">');
//	$( "#progressbar" ).progressbar({
//		value: 100
//	});
    //alert('Cargando...');
}

function llegadaTipoHabitacion(data) {
    console.log(data);
    var result = "";
    var i = 0;
    var cant = 0;

    $.each(data, function (i, item) {
        cant = cant + 1;
        //result += '<div style:visibility="hidden" id="numHabitSelect' + item.idTipoHab + '_div" >' + item.precio + '</div>';
        result += '<tr><td> ' + item.nombreTipoHab + '</td>';
        result += '<td>' + '<select id= "numHabitSelect' + item.idTipoHab + '" class = "numHabitSelect">';

        for (j = 0; j <= item.numPos; j++) {
            result += '<option value = "' + j + '" id = "idTipoHab' + j + item.idTipoHab + '">' + j + '</option>';
        }
        result += '<td><span id = "precio' + i+ '">' + item.precio + '</span></td>';
        
        result += '</select></td><td><span id = "subtotal' + item.idTipoHab + '">' + 0 + '</span></td></tr>';


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
    var numHab = $('#numHabitSelect1.numHabitSelect option:selected').attr("text")
    var precio = $('#precio0').text();
    //  alert(x);
    //alert(numHab);
    //alert(precio);
    $('#subtotal1').text(precio * numHab);
//    alert(10 * x);   
    //alert(":D");    
    $('#Total').text(
        parseFloat($('#subtotal1').text()) +
        parseFloat($('#subtotal2').text()) +
        parseFloat($('#subtotal3').text()) +
        parseFloat($('#subtotal4').text()) +
        parseFloat($('#subtotal5').text())
    );
}

function cambiarSubTotal2() {
    var id;
    var x = $('#numHabitSelect2.numHabitSelect option:selected').attr("value");
    var precio = $('#precio1').text();
    //alert(x);
    $('#subtotal2').text(precio * x);
    //alert(":D");    
    $('#Total').text(
        parseFloat($('#subtotal1').text()) +
        parseFloat($('#subtotal2').text()) +
        parseFloat($('#subtotal3').text()) +
        parseFloat($('#subtotal4').text()) +
        parseFloat($('#subtotal5').text())
    );
}

function cambiarSubTotal3() {
    var id;
    var x = $('#numHabitSelect3.numHabitSelect option:selected').attr("value");
    var precio = $('#precio2').text();
    //alert(x);
    $('#subtotal3').text(precio * x);
    //alert(":D");    
    $('#Total').text(
        parseFloat($('#subtotal1').text()) +
        parseFloat($('#subtotal2').text()) +
        parseFloat($('#subtotal3').text()) +
        parseFloat($('#subtotal4').text()) +
        parseFloat($('#subtotal5').text())
    );
}

function cambiarSubTotal4() {
    var id;
    var x = $('#numHabitSelect4.numHabitSelect option:selected').attr("value");
    var precio = $('#precio3').text();
    //alert(x);
    $('#subtotal4').text(precio * x);
    //alert(":D");    
    $('#Total').text(
        parseFloat($('#subtotal1').text()) +
        parseFloat($('#subtotal2').text()) +
        parseFloat($('#subtotal3').text()) +
        parseFloat($('#subtotal4').text()) +
        parseFloat($('#subtotal5').text())
    );
}

function cambiarSubTotal5() {
    var id;
    var x = $('#numHabitSelect5.numHabitSelect option:selected').attr("value");
    var precio = $('#precio4').text();
    //alert(x);
    $('#subtotal5').text(precio * x);
    //alert(":D");
    //alert($('#subtotal1').attr("value"));
    $('#Total').text(
        parseFloat($('#subtotal1').text()) +
        parseFloat($('#subtotal2').text()) +
        parseFloat($('#subtotal3').text()) +
        parseFloat($('#subtotal4').text()) +
        parseFloat($('#subtotal5').text())
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

$(function () {
    $("#tabs").tabs();
});

function continuar2() {
    
    $("#tabs").tabs('select','#tabs-2'); // switch to third tab

}

function continuar3() {

    $("#tabs").tabs('select', '#tabs-3'); // switch to third tab

}


