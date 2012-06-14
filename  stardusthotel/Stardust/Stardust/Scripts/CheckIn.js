
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {

    $("#habitaciones").hide();
    $("#buscarReserva").click(buscarHabitaciones);

}

function buscarHabitaciones() {

    $("#habitaciones").show("slow");

    var numero = $("#nroReserva").text();
    var pasar = {
        nroReserva:numero
    }
    var jsonData = JSON.stringify(pasar);

    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "ReservarHabitacion/habitacionesXReseva",
        beforeSend: inicioHabitacionesXReserva(),
        success: llegadaHabitacionesXReserva
    });

}

function inicioHabitacionesXReserva() {
    //Espero que me devulvan los datos
}

function llegadaHabitacionesXReserva(data) {

    var result = "";
    
}