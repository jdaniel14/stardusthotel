
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {

    var num = "2";

    var Hotel = {
        idHotel: num
    }
    var jsonData = JSON.stringify(Hotel);    
    console.log(jsonData);    
    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "mostrarReservas",
        beforeSend: inicioEnvioReservas(),
        success: llegadaReservas
    });
}

function inicioEnvioReservas() {
    //accion mientras espero a que me lleguen los datos
}

function llegadaReservas(data) {
    var result = "";
    var cant = 0;
    var eliminar = "";
    var arreglosElim = new Array();

    console.log(data);

    $.each(data, function (i, item) {
        alert(item.codReserva);
        cant = cant + 1;

        result += '<tr id = "linea' + item.codReserva + '" ><td>' + item.nombCliente + '</td>';
        result += "<td>" + item.codReserva + "</td>";
        result += "<td>" + item.fechaReserva + "</td>";
        result += '<td id = "opciones' + item.codReserva + '"><span id = "eliminar' + item.codReserva + '">Cancelar Reserva</span></td></tr>';


        eliminar = "#eliminar";
        eliminar += item.codReserva;
        arreglosElim[item.codReserva] = eliminar;
    });

    $("#tablaReservas").html(result);

    arreglosElim.forEach(function (elemento) {
        $(elemento).click(function (event) {

            var question = confirm("Desea eliminar el objeto seleccionado?");
            if (question != "0") {
                var n = elemento.substring(9); //verificar si  es la posicion correcta!!! OJO!!!
                alert(n);
                linea = "#linea";
                linea += n;
                $(linea).hide("slow");
                //llamar al eliminar

                var ReservaEliminar = {
                    idReserva: ""
                }
                var jsonData = JSON.stringify(ReservaEliminar);
                console.log(jsonData);
                $.ajax({
                    type: "POST",
                    data: jsonData,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: "ReservarHabitacion/eliminarReserva",
                    beforeSend: waitEliminar(),
                    success: eliminadoReserva
                });
            }
        });
    });
}

function waitEliminar() {
}

function eliminadoReserva(data) {
    alert(data);
}