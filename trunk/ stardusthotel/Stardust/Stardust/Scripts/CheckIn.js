
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {

    $("#habitaciones").hide();
    $("#xtipoHabit1").hide();
    $("#xtipoHabit2").hide();
    $("#xtipoHabit3").hide();
    $("#confirmacion").hide();

    $("#check").hide();
    $("#buscarReserva").click(buscarHabitaciones);
    $("#tipoHabit1").hide();
    $("#tipoHabit2").hide();
    $("#tipoHabit3").hide();

    $("#xtipoHabit1").click(muestra1);
    $("#xtipoHabit2").click(muestra2);
    $("#xtipoHabit3").click(muestra3);

    $("#check").click(confirmar);
}


function confirmar() {

    var x = $("#imagen");
    x.html('<img src="http://localhost:53053/Content/images/logo.png">');


    var question = confirm("Los datos son correctos??");

    if (question != "0") {
        $("#checkeo").hide("slow");

        $("#confirmacion").show("slow");
    }



}

function muestra1() {
    if ($("#tipoHabit1").css("display") == 'none') {
        // el elemento está 'hide'
        $("#tipoHabit1").show("slow");
    } else {
        // el elemento está 'show'
        $("#tipoHabit1").hide("slow");
    }
}

function muestra2() {
    if ($("#tipoHabit2").css("display") == 'none') {
        // el elemento está 'hide'
        $("#tipoHabit2").show("slow");
    } else {
        // el elemento está 'show'
        $("#tipoHabit2").hide("slow");
    }
}

function muestra3() {
    if ($("#tipoHabit3").css("display") == 'none') {
        // el elemento está 'hide'
        $("#tipoHabit3").show("slow");
    } else {
        // el elemento está 'show'
        $("#tipoHabit3").hide("slow");
    }
}



function buscarHabitaciones() {

    $("#habitaciones").show("slow");
    $("#xtipoHabit1").show("slow");
    $("#xtipoHabit2").show("slow");
    $("#xtipoHabit3").show("slow");
    $("#check").show("slow");

    //    var numero = $("#nroReserva").text();
    //    var pasar = {
    //        nroReserva:numero
    //    }
    //    var jsonData = JSON.stringify(pasar);

    //    console.log(jsonData);

    //    $.ajax({
    //        type: "POST",
    //        data: jsonData,
    //        dataType: "json",
    //        contentType: "application/json; charset=utf-8",
    //        url: "ReservarHabitacion/habitacionesXReseva",
    //        beforeSend: inicioHabitacionesXReserva(),
    //        success: llegadaHabitacionesXReserva
    //    });

}

function inicioHabitacionesXReserva() {
    //Espero que me devulvan los datos
}

function llegadaHabitacionesXReserva(data) {
    var checkIn;
    var listaHab;
    var result = "";

    //    $.each(lista, function (i, item) {
    //        checkIn = item.datosCheckIn;
    //        var tipoHab = checkIn.idTipHab;
    //        var numPersonas = checkIn.nroPers;
    //        var listaHabitaciones = checkIn.listHab;

    //        result += '<div class= "formRow"><span>Tipo de Habitacion</span>';
    //        result += '<div class = "formRight">';
    //        result += 


    //    });



}