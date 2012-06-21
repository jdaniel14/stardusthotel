
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {

    var idH = "1";

    var enviar = {
        idHotel : idH
    }

    jsonData = JSON.stringify(enviar);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "URL",
        beforeSend: esperaDatos(),
        success: llegadaDatos
    });
}

function esperaDatos(){
}

function llegadaDatos(data){

    console.log(data);

    var result = "";

    result += '<option value="NN" selected="selected">Escoja el Servicio</option>';

    $.each(data, function (i, item) {
        result += '<option value= "' + item.idSer + '">' + item.nomb + '</option>';
    });

    $("#ComboServicio").html(result);

    $("#registra").click(enviar);
}

function enviar() {

    var idService = $("#ComboServicio").val();

    var numeroDocu = $("#nDoc").get(0).value;

    var numReser = $("#nReserva").get(0).value;

    var numRecibito = $("#nRecibo").get(0).value;

    var canti = $("#monto").get(0).value;

    var tipoS = $("#ComboRes").get(0).value;

    var enviar = {
        idSer: idService,
        nroRes: numReser,
        dni:numeroDocu,
        nRecib:numRecibito,
        monto:canti,
        flagTipo:tipoS
    }

    jsonData = JSON.stringify(enviar);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "URL",
        beforeSend: esperaConfirma(),
        success: confirma
    });

}

function esperaConfirma() {


}

function confirma(data) {
    console.log(data.me);

    if (data.me == "") {
        alert('OK');
        console.log("se hizo");
        $(location).attr('href', '../../');
    }
    else {
        alert(data.me);
    }

}