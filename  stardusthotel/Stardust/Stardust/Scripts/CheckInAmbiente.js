﻿var x = $(document);
x.ready(inicio);

function inicio() {
    $("#gogo").click(enviar);

}

function enviar() {

    var res = $("#idReserva").val();

    if (res != "") {


        var enviar = {
            idEvento:res
        }

        var jsonData = JSON.stringify(enviar);
        console.log(jsonData);
        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "RegistrarCheckin",
            beforeSend: inicioEnvio(),
            success: FinEnvio
        });
    }
    else {
        mostrarError("Ingrese la reserva");
    }
}

function inicioEnvio() {
    mostrarEspera();
}

function FinEnvio(data) {
    $("#espera").dialog("destroy");
    console.log(data.me);
    //alert();

    if (data.me == "") {

        console.log("se hizo");

        mostrarConfirmacionFinal('Realizada ^^');
    }
    else {
        mostrarError(data.me);

    }
}

