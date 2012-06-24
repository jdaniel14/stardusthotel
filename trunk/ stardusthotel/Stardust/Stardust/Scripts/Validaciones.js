//Fecha Actual

var mydate = new Date();
var year = mydate.getYear();
if (year < 1000)
    year += 1900;
var day = mydate.getDay();
var month = mydate.getMonth() + 1;
var daym = mydate.getDate();
if (daym < 10)
    daym = "0" + daym;
//console.log(year);
//console.log(daym);
//console.log(month);



var iniciandoTodosLosProcesos = $(document);
iniciandoTodosLosProcesos.ready(iniciarValidaciones);

function iniciarValidaciones() {
    var input;

    $("#dialog-modal").hide();
    $("#espera").hide();
    $("#faltanDatos").hide();
    $("#error").hide();

    //Habitacion
    $("#FechaLlegada").change(validarFecha);
    $("#FechaSalida").change(validarFecha);
    

    $("#nDoc").change(validarSoloNumeros14);
//    $("#nombreDReserva").change(validarSoloTexto45);
//    $("#ApellidoDReserva").change(validarSoloTexto45);
    $("#nTarjetaNatural").change(validarSoloNumeros14);
    $("#telefNatural").change(validarSoloNumeros14);    
//    $("#razonDReserva").change(validarSoloTexto45);
    $("#telef").change(validarSoloNumeros14);
    $("#nTarjeta").change(validarSoloNumeros14);

    //Ambiente
    $("#FechaInicio").change(validarFecha);
    $("#FechaFin").change(validarFecha);

    //$("#nombreEvento").change(validarSoloTexto45);
    $("#nParticipantes").change(validarSoloNumeros14);

    //Estado Reserva
    $("#nroReserva").change(validarSoloNumeros14);
    $("#nroDocumento").change(validarSoloNumeros14);

    //Ubicacion del cliente
//    $("#nombre").change(validarSoloTexto45);

    //Pago Inicial
    $("#nReserva").change(validarSoloNumeros14);

    $("#idEvento").change(validarSoloNumeros14);
    


    //**********************+Asignar PlaceHolder's*********************


    //Habitaciones
    if ($("#FechaLlegada").length > 0) {
        input = document.getElementById("FechaLlegada");
        input.placeholder = "dd-mm-aaaa";
    }
    if ($("#FechaSalida").length > 0) {
        input = document.getElementById("FechaSalida");
        input.placeholder = "dd-mm-aaaa";
    }
    if ($("#nDoc").length > 0) {
        input = document.getElementById("nDoc");
        input.placeholder = "Sólo números";
    }
//    if ($("#nombreDReserva").length > 0) {
//        input = document.getElementById("nombreDReserva");
//        input.placeholder = "Sólo texto";
//    }
//    if ($("#ApellidoDReserva").length > 0) {
//        input = document.getElementById("ApellidoDReserva");
//        input.placeholder = "Sólo texto";
//    }
    if ($("#nTarjetaNatural").length > 0) {
        input = document.getElementById("nTarjetaNatural");
        input.placeholder = "Sólo números";
    }
    if ($("#telefNatural").length > 0) {
        input = document.getElementById("telefNatural");
        input.placeholder = "Sólo números";
    }
//    if ($("#razonDReserva").length > 0) {
//        input = document.getElementById("razonDReserva");
//        input.placeholder = "Sólo texto";
//    }
    if ($("#nTarjeta").length > 0) {
        input = document.getElementById("nTarjeta");
        input.placeholder = "Sólo números";
    }
    if ($("#telef").length > 0) {
        input = document.getElementById("telef");
        input.placeholder = "Sólo números";
    }


    //Ambiente
    if ($("#FechaInicio").length > 0) {
        input = document.getElementById("FechaInicio");
        input.placeholder = "dd-mm-aaaa";
    }
    if ($("#FechaFin").length > 0) {
        input = document.getElementById("FechaFin");
        input.placeholder = "dd-mm-aaaa";
    }
//    if ($("#nombreEvento").length > 0) {
//        input = document.getElementById("nombreEvento");
//        input.placeholder = "Sólo texto";
//    }
    if ($("#nParticipantes").length > 0) {
        input = document.getElementById("nParticipantes");
        input.placeholder = "Sólo números";
    }

    //Ubicacion de un cliente
//    if ($("#nombre").length > 0) {
//        input = document.getElementById("nombre");
//        input.placeholder = "Sólo texto";
    //    }

    //Pago Inicial
    if ($("#nReserva").length > 0) {
        input = document.getElementById("nReserva");
        input.placeholder = "Sólo números";
    }

    if ($("#idEvento").length > 0) {
        input = document.getElementById("idEvento");
        input.placeholder = "Sólo números";
    }
    

}

function validarSoloTexto45() {
    //Solo texto y menor a 55 caracteres
    var valor = $(this).get(0).value;
    var i;
    var longitud = (valor.toString()).length;
    var error = 0;

    for (i = 0; i < longitud; i++) {
        if ((valor[i] in { '1': 1, '2': 1, '3': 1, '4': 1, '5': 1, '6': 1, '7': 1, '8': 1, '9': 1, '0': 1 })) {
            error = 1;
        }
    }

    if (longitud > 45) {
        error = 1;
    }

    if (error == 1) {
        mostrarDialogo();
        $(this).attr("value", "");
    }
}

function validarFecha() {
    var valor = $(this).get(0).value;

    if ((valor[2] != '-') && (valor[4] != '-') && (valor[6] != '-')) {
        mostrarDialogo();
        $(this).attr("value", "");
    }
}

function validarSoloNumeros14() {
    //Solo numeros y cantidad menor a 14
    var valor = $(this).get(0).value;
    var i;
    var longitud = (valor.toString()).length;
    var error = 0;

    for (i = 0; i < longitud; i++) {
        if (!(valor[i] in {'1':1,'2':1,'3':1,'4':1,'5':1,'6':1,'7':1,'8':1,'9':1,'0':1})){
            error = 1;
        }       
    }

    if (longitud > 14) {
        error = 1;
    }

    if (error == 1) {
        mostrarDialogo();
        $(this).attr("value", "");
    }


}

function errorAJAX() {
    mostrarError("Error inesperado, intentelo mas tarde :)");
    $("#espera").dialog("destroy");
}

//Funcion dialogo\

function mostrarConfirmacionFinal(mensaje) {
    // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
    $("#error:ui-dialog").dialog("destroy");

    $("#error").dialog({
        height: 120,
        title: "Stardust says...",
        modal: true,

        buttons: {
            Ok: function () {
                $(this).dialog("close");
                $(location).attr('href', '../../');
            }
        }
    });

    $("#msjError").html(mensaje);

}

function mostrarDialogo() {
    // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
    $("#dialog:ui-dialog").dialog("destroy");

    $("#dialog-modal").dialog({
        height: 140,
        modal: true,

        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
}

function mostrarEspera() {
    // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
    

    $("#espera").dialog({
        height: 90,
        modal: true,
        closeOnEscape: false,
        open: function (event, ui) { $('#espera').parent().find('a.ui-dialog-titlebar-close').hide(); }
    });
}


function mostrarFaltanDatos() {
    // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
    $("#faltanDatos:ui-dialog").dialog("destroy");

    $("#daltanDatos").dialog({
        height: 140,
        modal: true,

        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });
}

function mostrarError(mensaje) {
    // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
    $("#error:ui-dialog").dialog("destroy");

    $("#error").dialog({
        height: 120,
        title: "Stardust says...", 
        modal: true,

        buttons: {
            Ok: function () {
                $(this).dialog("close");
            }
        }
    });

    $("#msjError").html(mensaje);




}