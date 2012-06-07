


//$("#maps").gMap({ markers: [{ latitude: 47.660937,
//    longitude: 9.569803,
//    html: "Tettnang, Germany",
//    popup: true
//}],
//    zoom: 6
//});




function inicializarMostreo() {

    $("#retornar").click(regresar);
    $("#pestana1").hide();
    $("#pestana2").hide();
    $("#pestana3").show();
    $("#tabs").tabs('select', '#tabs-3'); // switch to third tab
    $("#enviar").click(enviarDatos);

    var result = "";
    result = $("#FechaLlegada").get(0).value;
    $('#mostrarFechaReserva').html(result);


    result = "";
    result = $("#FechaSalida").get(0).value;
    $('#mostrarFechaSalida').html(result);

    result = "";
    
    result = $('#Total').text();
    $('#mostrarTotal').html(result);
 

    result = "";
    result = $("#nDoc").get(0).value;
    $('#mostrarNroDocumento').html(result);

    result = "";
    result = $("#ComboCliente").val();
    $('#mostrarTipoDocumento').html(result);

    var nombre = "";
    var email = "";

    if (result == "RUC") {
        nombre = $("#razonDReserva").get(0).value;
        email = $("#mail").get(0).value;
    }
    else {
        nombre = $("#nombreDReserva").get(0).value
        email = $("#mailNatural").get(0).value;
    }
    $('#mostrarNombre').html(nombre);
    $('#mostrarEmail').html(email);
    
}

function regresar() {
    $("#pestana1").show();
    $("#pestana2").show();
    $("#pestana3").hide();
    $("#tabs").tabs('select', '#tabs-1');
}



function enviarDatos() {

    result = "";
    result = $("#ComboCliente").val();
    $('#mostrarTipoDocumento').html(result);

    var nombre = "";
    var email = "";

    if (result == "RUC") {
        nombre = $("#razonDReserva").get(0).value;
        email = $("#mail").get(0).value;
    }
    else {
        nombre = $("#nombreDReserva").get(0).value
        email = $("#mailNatural").get(0).value;
    }


    var DatosReserva = {
//        jFechaLlegada: "",
//        jFechaSalida: "",
//        jcantTipo1: "",
//        jcantTipo2: "",
//        jcantTipo3: "",
//        jcantTipo4: "",
//        jcantTipo5: "",
//        jContarConServicio: "",
//        jHoraLlegada: "",
//        jAerolinea: "",
//        jNroVuelo: "",
//        jCantPersonas: "",
//        jTipoDocumento: ""
        //...continuará...
        jNroDocumento :  $("#nDoc").get(0).value,
        jFechaReserva: $("#FechaLlegada").get(0).value,
        jTotal: $('#Total').text(),
        jNombre: nombre,
        jMail: mail
    };
    var jsonData = JSON.stringify(DatosReserva);
    
    
    console.log(jsonData);
    
    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "ReservarHabitacion/cerrarReserva",        
        success: finRes
    });
}
function finRes(data) {
    console.log("se hizo");
}
