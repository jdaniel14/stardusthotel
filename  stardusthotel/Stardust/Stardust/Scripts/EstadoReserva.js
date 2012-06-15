
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {

    $("#datos").hide();
    $("#anular").hide();

    $("#buttonDatos").click(buscarReserva);

    
}

function buscarReserva(){

    $("#datos").show("slow");

    var num = $("#nroReserva").get(0).value;
    var num2 = $("#nroDocumento").get(0).value;
    var telo = "1";

    var datosEnviar = {
        idReserva: num,
        documento: num2,
        idHotel: telo
    }
    var jsonData = JSON.stringify(datosEnviar);    
    console.log(jsonData);    
    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "ReservarHabitacion/consultarReserva",
        beforeSend: inicioEnvioReservas(),
        success: llegadaReservas
    });

}

function inicioEnvioReservas() {
    //accion mientras espero a que me lleguen los datos
}

function llegadaReservas(data) {
    console.log(data);

    $("#anular").show("slow");
    
    var idReserva = data.idReserva;
    var fechaLlegada = data.fechaLlegada;
    var fechaSalida = data.fechaSalida;
    var nroDoc = data.nroDoc;
    var nombre = data.nombre;
    var lista = data.listaHab

    result = "";

    result += '<div class="formRow"><span>Nro de Reserva</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + idReserva + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha de Llegada</span>';
    result += '<div class = "formRight" >';
    result += '<span id = fechaLlegada>' + fechaLlegada + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha de Salida</span>';
    result += '<div class = "formRight" >';
    result += '<span id = fechaSalida>' + fechaSalida + '</span>';
    result += '</div><div class="clear"></div></div>'

    result += '<div class="formRow"><span>Nro de Documento</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroDocumento>' + nroDoc + '</span>';
    result += '</div><div class="clear"></div></div>'

    result += '<div class="formRow"><span>Nombre</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nombre>' + nombre + '</span>';
    result += '</div><div class="clear"></div></div>'

    $("#insertarDatos").html(result);

    result = "";

    $.each(lista, function (i, item) {

        result += '<tr>';
        result += '<td>' + item.nombTipoHab + '</td>';
        result += '<td>' + item.cant + '</td>';
        result += '</tr>';

    });

    $("#tablaReservas").html(result);

    $("#anular").click(cancelarReserva);


//    $.each(lista, function (i, item) {
        //alert(item.codReserva);
//        cant = cant + 1;

//        result += '<tr id = "linea' + item.codReserva + '" ><td>' + item.nombCliente + '</td>';
//        result += "<td>" + item.codReserva + "</td>";
//        result += "<td>" + item.fechaReserva + "</td>";
//        result += '<td id = "opciones' + item.codReserva + '"><span id = "eliminar' + item.codReserva + '">Cancelar Reserva</span></td></tr>';


//        eliminar = "#eliminar";
//        eliminar += item.codReserva;
//        arreglosElim[item.codReserva] = eliminar;
//    });

//    $("#tablaReservas").html(result);

//    arreglosElim.forEach(function (elemento) {
//        $(elemento).click(function (event) {

//            var question = confirm("Desea eliminar el objeto seleccionado?");
//            if (question != "0") {
//                var n = elemento.substring(9); //verificar si  es la posicion correcta!!! OJO!!!
//                alert(n);
//                linea = "#linea";
//                linea += n;
//                $(linea).hide("slow");
//                //llamar al eliminar

//                var ReservaEliminar = {
//                    idReserva: ""
//                }
//                var jsonData = JSON.stringify(ReservaEliminar);
//                console.log(jsonData);
//                $.ajax({
//                    type: "POST",
//                    data: jsonData,
//                    dataType: "json",
//                    contentType: "application/json; charset=utf-8",
//                    url: "ReservarHabitacion/eliminarReserva",
//                    beforeSend: waitEliminar(),
//                    success: eliminadoReserva
//                });
//            }
//        });
//    });
}

function cancelarReserva() {

    var question = confirm("Desea eliminar el objeto seleccionado?");
    if (question != "0") {

        var res = $("#nroReserva").get(0).value;

        var ReservaEliminar = {
           idReserva: res
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
}

function waitEliminar() {
}

function eliminadoReserva(data) {
    $(location).attr('href', '../');
    alert('Eliminado');
}