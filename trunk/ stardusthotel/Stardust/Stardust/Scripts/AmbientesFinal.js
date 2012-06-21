﻿
var doc2 = $(document);
doc2.ready(inicioFinal);

function inicioFinal() {
    $("#retornar").click(regresar);

}

function sgteConfirma() {
    $("#pestana1").hide();
    $("#pestana2").hide();
    $("#pestana3").hide();
    $("#pestana4").show();
    $("#tabs").tabs("select", "#Vconfirmacion");

    var fechaI = $("#FechaInicio").get(0).value;
    var fechaF = $("#FechaFin").get(0).value;
    var tot = $("#Total").text();
       

    $("#mostrarFechaReserva").html(fechaI);
    $("#mostrarFechaFinal").html(fechaF);
    $("#mostrarTotal").html(tot);

    result = "";
    result = $("#nDoc").get(0).value;
    $("#mostrarnDoc").html(result);

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

    $("#registrar").click(finDelChongo);

}

function finDelChongo() {

    var result = "";
    result = $("#ComboCliente").val();

    var nombreEnvio = "";
    var email2 = "";
    var tipoDocu = "";
    var apellido = "";
    var telefono = "";
    var tipoDeTarjeta = "";
    var numTarjeta = "";
    var comentario = "";
    var fechaLlegada = $("#FechaInicio").get(0).value;
    var fechaSalida = $("#FechaFin").get(0).value;

    if (result == "RUC") {
        tipoDocu = "RUC";
        nombreEnvio = $("#razonDReserva").get(0).value;
        email2 = $("#mail").get(0).value;
        telefono = $("#telef").get(0).value;
        tipoDeTarjeta = $("#tipoTarjetaJ").val();
        numTarjeta = $("#nTarjeta").get(0).value;
        comentario = $("#comment").get(0).value;
    }
    else {
        tipoDocu = "DNI";
        nombreEnvio = $("#nombreDReserva").get(0).value;
        apellido = $("#ApellidoDReserva").get(0).value;
        email2 = $("#mailNatural").get(0).value;
        telefono = $("#telefNatural").get(0).value;
        tipoDeTarjeta = $("#tipoTarjeta").val();
        numTarjeta = $("#nTarjetaNatural").get(0).value;
        comentario = $("#commentN").get(0).value;
    }

    var nDoc = $("#nDoc").get(0).value;
    var total1 = $('#Total').text();

    var Hotel = "1";
    var clientexD = {
        tipoDoc: tipoDocu,
        nroDoc: nDoc,
        nomb: nombreEnvio,
        apell: apellido,
        email: email2,
        telf: telefono,
        tipoTarj: tipoDeTarjeta,
        nroTarj: numTarjeta
    }

    var listaAmbientes = new Array();

    arregloChecks.forEach(function (elemento) {
        var n = elemento.substring(6);

        if ($(elemento).is(":checked")) {

            var idAmbi = n;
            var cmdNA = "#nombre";
            cmdNA += n;
            var nombreDelA = $(cmdNA).text();

            var cmdPA = "#precio";
            cmdPA += n;
            var precioDelA = $(cmdPA).text();

            var enviarAmbi = {
                idAmb: idAmbi,
                nombAmb: nombreDelA,
                prec: precioDelA
            }

            listaAmbientes.push(enviarAmbi);
        }

        var nombreDelE = $("#nombreEvento").get(0).value;
        var numeroDelE = $("#nParticipantes").get(0).value;
        var descDelE = $("#descEvento").get(0).value;

        var enviarEvento = {
            nomb: nombreDelE,
            descripc: descDelE,
            nroParticipantes: numeroDelE
        }


        var TeLoPaso = {
            idHotel: Hotel,
            client: clientexD,
            evento: enviarEvento,
            listAmbi: listaAmbientes,
            fechaIni:fechaLlegada, 
            fechaFin: fechaSalida,
            coment:comentario,
            total:total1
        }

        var jsonData = JSON.stringify(TeLoPaso);

        console.log(jsonData);

        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "aDondeEnvio",
            success: graciasTotales
        });
    });

}

function graciasTotales(data) {

    if (data.me == "") {
        alert('OK');
        $(location).attr('href', '../../');
    }
    else {
        alert(data.me);
    }
}

function regresar() {
    
    $("#tabs").tabs("select", "#Vambiente");
    $("#pestana4").hide();
    $("#pestana3").show();
    $("#pestana2").show();
    $("#pestana1").show();

}