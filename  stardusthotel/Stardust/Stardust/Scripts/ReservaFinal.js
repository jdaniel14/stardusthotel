﻿var idUser;
var enviarReservita;

function inicializarMostreo() {

    //inicio validacion
    var continuarIngreso;

    
    if (
            ($("#FechaLlegada").get(0).value != "") &&
            ($("#FechaSalida").get(0).value != "") &&
            (parseFloat($("#Total").text()) > 0) &&
            ($("#mailNatural").get(0).value != "") &&
            ($("#password").get(0).value != "") &&
            ( ($("#razonDReserva").get(0).value != "") | ($("#nombreDReserva").get(0).value != "") ) &&
            ( ($("#telef").get(0).value != "") |  ($("#telefNatural").get(0).value != "") ) //&&
            //( ($("#nTarjeta").get(0).value != "") | ($("#nTarjetaNatural").get(0).value != "") )

        ) {
        
        continuarIngreso = 1;
    }
    else {
        
        continuarIngreso = 0;
    }

    if (continuarIngreso == 1) {

        $("#retornar").click(regresar);
        $("#pestana1").hide();
        $("#pestana2").hide();
        $("#pestana3").show();
        $("#tabs").tabs('select', '#tabs-3'); // switch to third tab
        $("#enviar").click(enviarDatos);

        var result = "";
        result = $("#FechaLlegada").get(0).value;
        $('#mostrarFechaReserva').html(result);


        result = "";
        result = $("#FechaSalida").get(0).value;
        $('#mostrarFechaSalida').html(result);

        result = "";

        result = $('#Total').text();
        $('#mostrarTotal').html(result);


        result = "";
        result = $("#nDoc").get(0).value;
        $('#mostrarNroDocumento').html(result);

        result = "";
        result = $("#ComboCliente").val();
        $('#mostrarTipoDocumento').html(result);

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
    }
    else {
        mostrarError('Faltan llenar Datos');
    }
    
}

function regresar() {
    $("#pestana1").show();
    $("#pestana2").show();
    $("#pestana3").hide();
    $("#tabs").tabs('select', '#tabs-1');
}



function enviarDatos() {

    //alert('enviando');
    result = "";
    result = $("#ComboCliente").val();
    $('#mostrarTipoDocumento').html(result);

    var nombreEnvio = "";
    var email2 = "";
    var tipoDocu = "";
    //var apellido = "";
    var telefono = "";
    var tipoDeTarjeta = "";
    var numTarjeta = "";
    var comentario = "";
    var fechaLlegada = $("#FechaLlegada").get(0).value;
    var fechaSalida = $("#FechaSalida").get(0).value;
    var checkeado = 0;

    if ($("#checkAerop").is(":checked")) {
        checkeado = 1;
    }
    else {
        checkeado = 0;
    }

    var horaLlegada;
    var aerop;
    var numVuelo;
    var numPersonas;

    horaLlegada = $("#HoraLlegada").get(0).value;
    aerop = $("#Aerolinea").get(0).value;
    numVuelo = $("#NVuelo").get(0).value;
    numPersonas = $("#CantPersonas").get(0).value;

    if (result == "RUC") {
        tipoDocu = "RUC";
        nombreEnvio = $("#razonDReserva").get(0).value;
        email2 = $("#mailNatural").get(0).value;
        telefono = $("#telef").get(0).value;
        tipoDeTarjeta = $("#tipoTarjetaJ").val();
        numTarjeta = $("#nTarjeta").get(0).value;
        comentario = $("#comment").get(0).value;
    }
    else {
        tipoDocu = "DNI";
        nombreEnvio = $("#nombreDReserva").get(0).value;
        //apellido = $("#ApellidoDReserva").get(0).value;
        email2 = $("#mailNatural").get(0).value;
        telefono = $("#telefNatural").get(0).value;
        tipoDeTarjeta = $("#tipoTarjeta").val();
        numTarjeta = $("#nTarjetaNatural").get(0).value;
        comentario = $("#comment").get(0).value;
    }

    var nDoc = $("#nDoc").get(0).value;    
    var total1 = $('#Total').text();
    var listaTipos = new Array();
    var i = 0;


    arreglosId.forEach(function (elemento) {
        var cmd = ""
        cmd += elemento + ".numHabitSelect option:selected";
        var cantHabit = $(cmd).attr("value");


        var id = elemento.substring(15);

        var habits = arreglosHabit[id];

        //        var arregloInterior = new Array();

        //        arregloInterior[0] = id;
        //        arregloInterior[1] = cantHabit;
        //        arregloInterior[2] = habits;

        cmd = "";
        cmd = "#nombre";
        cmd += id;

        var nombreTipo = $(cmd).text();

        cmd = "#precio";
        cmd += id;
        var precio2 = $(cmd).text();
//        alert(cmd);
//        alert(precio2);
      

        var arregloInterior = {
            tipo: id,
            cant: cantHabit,
            nombTipo: nombreTipo,
            precUnit: precio2,
            list: habits            
        };
        
        listaTipos[i] = arregloInterior;

        i++;
    });

   var suma = $("#Total").text();
   var passw = $("#password").get(0).value;

   if (siloEs == 1) {
       registrar = 3;
   }

   var Hotel = SendHotel;
   var clientexD = {
            tipoDoc:tipoDocu,
            nroDoc:nDoc,
            nomb:nombreEnvio,
            //apell:apellido,
            email:email2,
            telf:telefono,
            tipoTarj:tipoDeTarjeta,
            nroTarj:numTarjeta
        }

    var recojoAero = {
        hrLleg:horaLlegada,
        aero:aerop,
        vuel:numVuelo,
        nroPer:numPersonas,       
    }

    var DatosReserva = {        
        idHotel: Hotel,
        client:clientexD,
        listTip:listaTipos,
        fechaIni:fechaLlegada,
        fechaFin:fechaSalida,
        coment:comentario,
        rec:checkeado,
        datRec:recojoAero,
        total: suma,
        pass:passw,        
        idUsuario:idUser,
        tipoRegistro : registrar
    }
    var jsonData = JSON.stringify(DatosReserva);
    
    
    console.log(jsonData);
    
    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "ReservarHabitacion/cerrarReserva",
        beforeSend: esperarFinal,        
        success: finRes
    });
}

function esperarFinal() {
    mostrarEspera();
}

function finRes(data) {
    $("#espera").dialog("destroy");
    console.log(data.me);

    if (data.me == "") {
        
        console.log("se hizo");
        
        mostrarConfirmacionFinal('Reservar realizada ^_^!');
    }
    else {        
        mostrarError(data.me);
        
    }
    
}
