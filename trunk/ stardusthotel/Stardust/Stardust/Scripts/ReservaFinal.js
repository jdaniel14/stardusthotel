


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

    var result = "";
    result = $("#FechaLlegada").get(0).value

    $('#mostrarFechaReserva').html(result);

}

function regresar() {
    $("#pestana1").show();
    $("#pestana2").show();
    $("#pestana3").hide();
    $("#tabs").tabs('select', '#tabs-1');
}



function enviarDatos() {
    var DatosReserva = {
        jFechaLlegada: "",
        jFechaSalida: "",
        jcantTipo1: "",
        jcantTipo2: "",
        jcantTipo3: "",
        jcantTipo4: "",
        jcantTipo5: "",
        jContarConServicio: "",
        jHoraLlegada: "",
        jAerolinea: "",
        jNroVuelo: "",
        jCantPersonas: "",
        jTipoDocumento: ""
        //...continuará...
    };
    var jsonData = JSON.stringify(DatosReserva);

}
