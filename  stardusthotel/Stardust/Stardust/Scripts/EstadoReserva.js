var SendHotel;
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {

    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "/ReservarHabitacion/listarHoteles",
        beforeSend: esperarHoteles(),
        success: llegadaHoteles
    });

    $("#datos").hide();
    $("#anular").hide();

    $("#buttonDatos").click(buscarReserva);
    
}


function esperarHoteles() {
}

function llegadaHoteles(data) {
    var escritor = "";
    console.log(data);
    if (data.me == "") {

        escritor += '<option value = "NN" selected = "selected">Escoja un hotel</option>';

        $.each(data.lista, function (i, item) {
            escritor += '<option value = "' + item.ID + '">' + item.nombre + '</option>';
        });

        $("#ComboHoteles").html(escritor);

        var miValue2 = "NN";
        $("#ComboHoteles option[value=" + miValue2 + "]").attr("selected", true);
        $("#ComboHoteles").trigger('change');
    }
    else {
        mostrarError(data.me);
    }
}

function buscarReserva(){

    

    SendHotel = $("#ComboHoteles").val();
    var num = $("#nroReserva").get(0).value;
    var num2 = $("#nroDocumento").get(0).value;

    if ((nroReserva != "") &&
        (nroDocumento != "") &&
        (SendHotel != "NN")) {

        var datosEnviar = {
            idHotel: SendHotel,
            idReserva: num,
            documento: num2
        }
        var jsonData = JSON.stringify(datosEnviar);
        console.log(jsonData);
        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "consultarReserva",
            beforeSend: inicioEnvioReservas(),
            success: llegadaReservas
        });
    }
    else {
        mostrarError("Faltan llenar Datos");
    }
}

function inicioEnvioReservas() {
    //accion mientras espero a que me lleguen los datos
}

function llegadaReservas(data) {
    console.log(data);

    if (data.me == "") {

        $("#consulta").hide("slow");
        $("#datos").show("slow");
        $("#buttonDatos").hide("slow");

        $("#anular").show("slow");

        var idReserva = data.idReserva;
        var fechaLlegada = data.fechaIni;
        var fechaSalida = data.fechaFin;
        var nroDoc = data.doc;
        var nombre = data.Nombre;
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
            result += '<td>' + item.nombTipHab + '</td>';
            result += '<td>' + item.nroPers + '</td>';
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
    else {
        mostrarError(data.me);
    }
}

function cancelarReserva() {

    var question = confirm("Desea eliminar el objeto seleccionado?");
    if (question != "0") {

        var res = $("#nroReserva").get(0).value;

        var ReservaEliminar = {
            idReserva: res,
            idHotel: SendHotel
        }

        var jsonData = JSON.stringify(ReservaEliminar);
        console.log(jsonData);
        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "anularReserva",
            beforeSend: waitEliminar(),
            success: eliminadoReserva
        });


    }
    
}

function waitEliminar() {
}

function eliminadoReserva(data) {
    $("#espera").dialog("destroy");
    //alert("fin");
    console.log(data.me);

    mostrarConfirmacionFinal(data.me);
    
}