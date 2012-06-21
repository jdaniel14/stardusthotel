
var x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {


    $("#mostrarDatos").hide();

    $("#buscame").click(tonooo);


}

function tonooo() {

    

    var documento = $("#nDoc").get(0).value;
    var idReserva = $("#nReserva").get(0).value;
    var tipo = $("#ComboRes").val();

    var enviar = {
        flagTipo: tipo,
        id: idReserva,
        doc: documento
    }

    jsonData = JSON.stringify(enviar);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "DireccionURL",
        beforeSend: esperaDatos(),
        success: llegadaDatos
    });

}

function esperaDatos() {
    //esperando
}

function llegadaDatos(data) {

    if (data.me == "") {

        $("#nombReserva").text(data.dato);
        $("#nDocu").text(data.doc);
        $("#nombre").text(data.nom);
        $("#monto").text(data.montoInicial);
        $("#mostrarDatos").show("slow");

        if (data.estado == 1) {
            $("#aVeces2").hide()
        }
        else {
            $("#aVeces1").hide();
            $("#estado").text("Cancelado");
        }

        $("#vuelto").text(0);

        $("#cantPagando").change(asignarVuelto);

        $("#pagar").click(alcohol);
    }
    else {
        alert(data.me);
    }
}


function alcohol() {

    if (parseFloat($("#vuelto").text()) < 0) {
        alert("Cantidad insuficiente");
    }
    else {

        var documento = $("#nDoc").get(0).value;
        var idReserva = $("#nReserva").get(0).value;
        var tipo = $("#ComboRes").val();

        var enviar = {
            flagTipo: tipo,
            id: idReserva,
            doc: documento
        }

        jsonData = JSON.stringify(enviar);
        console.log(jsonData);

        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "DireccionURL",
            beforeSend: casiConfirma(),
            success: Comunion
        });

    }

}

function casiConfirma() {
}

function Comunion(data) {

    console.log(data.me);

    if (data.me == "") {
        alert('OK');
        console.log("se hizo");
        $(location).attr('href', '../');
    }
    else {
        alert(data.me);
    }

}


function asignarVuelto() {

    $("#vuelto").text(
        parseFloat($("#cantPagando").get(0).value) -
        parseFloat($("#faltante").text())

    );

}