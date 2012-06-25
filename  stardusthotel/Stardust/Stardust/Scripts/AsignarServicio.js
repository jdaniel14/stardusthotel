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

        $("#ComboHoteles").change(pedirServicio);
    }
    else {
        mostrarError(data.me);
    }
}


function pedirServicio() {

    if (SendHotel != "NN") {

        var enviar = {
            idHotel: SendHotel
        }

        jsonData = JSON.stringify(enviar);
        console.log(jsonData);

        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "../Servicios/ConsultarServicio",
            beforeSend: esperaDatos(),
            success: llegadaDatos
        });
    }


}

function esperaDatos(){
}

function llegadaDatos(data){

    console.log(data);
    var lista = data.lista;

    var result = "";
      
    result += '<option value= "' + 'NN' + '">'  + '</option>';
 

    $.each(lista, function (i, item) {
        result += '<option value= "' + item.id + '">' + item.nombre + '</option>';
    });

    $("#ComboServicio").html(result);

    $("#registra").click(enviar);
}

function enviar() {

    var puedeEnviar;

    if (    ($("#ComboServicio").val() != "NN") &&
            ($("#nDoc").get(0).value != "") &&
            ($("#nReserva").get(0).value != "") &&
            ($("#nRecibo").get(0).value != "") &&
            ($("#monto").get(0).value != "")
        ) {
        puedeEnviar = 1;
    }
    else {
        puedeEnviar = 0;
    }


    if (puedeEnviar == 1) {



        var telo = "1";
        var idService = $("#ComboServicio").val();

        var numeroDocu = $("#nDoc").get(0).value;

        var numReser = $("#nReserva").get(0).value;

        var numRecibito = $("#nRecibo").get(0).value;

        var canti = $("#monto").get(0).value;

        var tipoS = $("#ComboRes").get(0).value;

        var enviar = {
            idHotel: SendHotel,
            idSer: idService,
            nroRes: numReser,
            dni: numeroDocu,
            nRecib: numRecibito,
            monto: canti,
            flagTipo: tipoS
        }

        jsonData = JSON.stringify(enviar);
        console.log(jsonData);

        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "../Servicios/AsignarServicioReserva",
            beforeSend: esperaConfirma(),
            success: confirma
        });
    }
    else {
        alert('Falta ingresar datos');
    }
}

function esperaConfirma() {


}

function confirma(data) {
    $("#espera").dialog("destroy");
    console.log(data.me);

    if (data.me == "") {

        console.log("se hizo");
        mostrarConfirmacionFinal('Reservar realizada ^_^!');
    }
    else {
        mostrarError(data.me);
        if (data.me == "No se pudo enviar el email") {
            $(location).attr('href', '../');
        }
    }

}