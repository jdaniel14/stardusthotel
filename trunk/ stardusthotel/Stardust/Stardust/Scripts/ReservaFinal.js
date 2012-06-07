


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

    var nombreEnvio = "";
    var email2 = "";

    if (result == "RUC") {
        nombreEnvio = $("#razonDReserva").get(0).value;
        email2 = $("#mail").get(0).value;
    }
    else {
        nombreEnvio = $("#nombreDReserva").get(0).value
        email2 = $("#mailNatural").get(0).value;
    }

    var nDoc = $("#nDoc").get(0).value;
    var FechaLlegada = $("#FechaLlegada").get(0).value;
    var total1 = $('#Total').text();

    

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
        nroDoc :  nDoc,
        fechaReserva: FechaLlegada,
        total: total1,
        nombre: nombreEnvio,
        email: email2
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
