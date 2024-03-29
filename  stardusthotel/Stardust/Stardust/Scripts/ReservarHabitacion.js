﻿var siloEs;
var correito = "";
var arreglosId = new Array();
var arreglosHabit = new Array();
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


    $("#pestana3").hide();

    $("#continuarP2").click(nextDatos);

    $("#tipos").hide();
    $("#tipos2").hide();
    $("#buscame").click(mostrarBuscame);

    $("#checkServices").click(function (event) {
        $("#TotalAjeno").text(
            $("#Total").text()
        );
        if ($(this).is(":checked")) {
            $("#checkServices:checkbox:not(:checked)").attr("checked", "checked");
            $("#ServiciosAdicionales").show("slow");
        } else {
            $("#checkServices:checkbox:checked").removeAttr("checked");
            $("#ServiciosAdicionales").hide("slow");
        }
        $("#checkDesayuno").click(AddDesayuno);
        $("#checkAlmuerzo").click(AddAlmuerzo);
        $("#checkCena").click(AddCena);

    });
    $("#ServiciosAdicionales").hide();
    $("#OfertaEvento").hide();
    $("#continuarP3").click(inicializarMostreo);
    $("#FechaLlegada").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#FechaSalida").datepicker({ dateFormat: 'dd-mm-yy' });
    //getter
    var minDate = $("#FechaLlegada").datepicker("option", "minDate");
    //setter
    $("#FechaLlegada").datepicker("option", "minDate", new Date(year, (month - 1), daym ));
    $("#FechaSalida").datepicker("option", "minDate", new Date(year, (month - 1), daym ) );
    $("#fieldAeropuerto").hide();
    $("#registrarAeropuerto").click(mostrarFieldAeropuerto);
    $("#EventoCorrecto").hide();
    $("#EventoIncorrecto").hide();
    $("#ValidarEvento").click(IniciarValidacionEvento);
    
    $('#HoraLlegada').timepicker({});

    $("#checkAerop").click(function (event) {
        
        if ($(this).is(":checked")) {
            $("#checkAerop:checkbox:not(:checked)").attr("checked", "checked");
            $("#fieldAeropuerto").show("slow");
        } else {
            $("#checkAerop:checkbox:checked").removeAttr("checked");
            $("#fieldAeropuerto").hide("slow");
        }
    });

    $("#checkEvento").click(function (event) {

        if ($(this).is(":checked")) {
            $("#checkEvento:checkbox:not(:checked)").attr("checked", "checked");
            $("#OfertaEvento").show("slow");
        } else {
            $("#checkEvento:checkbox:checked").removeAttr("checked");
            $("#OfertaEvento").hide("slow");
        }
    }); 

    var x;

    x = $("#FechaLlegada");
    x.focus(clickFechain);
    x = $("#FechaSalida");
    x.focus(clickFechaout);
}

function IniciarValidacionEvento() {
    if ($("#idEvento").get(0).value != "") {

        var enviarInvit = $("#idEvento").get(0).value;
        var enviarsela = {
            idInvitado: enviarInvit            
        }
        var jsonData = JSON.stringify(enviarsela);
        //alert'ant');
        console.log(jsonData);
        //alert('desp');
        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            statusCode: {
                500: function () {
                    $("#espera").dialog("destroy");
                    mostrarError("Error inesperado... intentelo mas tarde :)");
                    $("#idEvento").attr("value", "");
                    $("#EventoIncorrecto").hide("slow");
                    $("#EventoCorrecto").hide("slow");
                }
            },
            contentType: "application/json; charset=utf-8",
            url: "ReservarHabitacion/consultarDisponibles",
            beforeSend: esperaValidacion(),
            success: llegadaValidacion

        });
        



    }
    else {
        mostrarError("Campo no puede ser nulo");
    }
}

function esperaValidacion() {
}

function llegadaValidacion(data) {
    if (data.me == "") {
        $("#EventoIncorrecto").hide("slow");
        $("#EventoCorrecto").show("slow");
        var miValue = data.tipoDocumento
        $("#ComboCliente option[value=" + miValue + "]").attr("selected", true);
        $("#ComboCliente").trigger('change');
        $("#nDoc").attr("value", data.nroDocumento);
        $("#nombreDReserva").attr("value", data.nombres);
        $("#ComboCliente").hide();
        $("#obtenerPass").hide();
        $("#nDoc").attr("readonly", "readonly");
        $("#nombreDReserva").attr("readonly", "readonly");
        soloEs = 1;
    }
    else {
        mostrarError(data.me);
        $("#EventoIncorrecto").show("slow");
        $("#EventoCorrecto").hide("slow");
        siloEs = 0;
    }
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

function nextDatos() {
    $("#tabs").tabs("select", "#tabs-2");
    $('html,body').animate({
        scrollTop: $("#pestana1").offset().top
    }, 700);
}

function mostrarBuscame() {
    
    
    var fechaInicio = $("#FechaLlegada").attr("value");
    var fechaFinal = $("#FechaSalida").attr("value");
    SendHotel = $("#ComboHoteles").val();

    if ((fechaInicio != "") &&
        (fechaFinal != "") &&
        (SendHotel != "NN")) {

        //alert(fechaFinal);
        var Hotel = {
            idHotel: SendHotel,
            fechaIni: fechaInicio,
            fechaFin: fechaFinal
        }
        var jsonData = JSON.stringify(Hotel);
        //alert'ant');
        console.log(jsonData);
        //alert('desp');
        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            statusCode: {
                500: function () {
                    $("#espera").dialog("destroy");
                    mostrarError("Error inesperado... intentelo mas tarde :)");
                    $("#tipos").hide("slow");
                }
            },
            contentType: "application/json; charset=utf-8",
            url: "ReservarHabitacion/consultarDisponibles",
            beforeSend: inicioEnvioTipoHotel(),
            success: llegadaTipoHabitacion

        });

        $('#Total').text(0);
    }
    else {
        mostrarError("Faltan llenar Datos");
    }
}

function inicioEnvioTipoHotel() {   
    var x = $("#tablaTipos");
    x.html('<img src="http://www.coliseogym.com/Gym/Iconos/cargando.gif">');
    mostrarEspera();
}

function llegadaTipoHabitacion(data) {

    $("#espera").dialog("destroy");
    
 
    console.log(data);

    if (data.cantDias < 0) {
        mostrarError('Error: Fecha fin debe ser mayor a Fecha Inicio');
        $("#FechaInicio").attr("value", "");
        $("#FechaFin").attr("value", "");
    }
    else {
        if (data.me != "") { mostrarError(data.me); }
        else if (data.listaXTipo.length == 0) { mostrarError("No hay habitaciones para este Hotel"); }
        else {
            $("#tipos").show("slow");
            var result = "";
            var i = 0;
            var cant = 0;

            var id = "";
            var lista = data.listaXTipo;
            var cantDias = data.cantDias;

            $.each(lista, function (i, item) {
                cant = cant + 1;

                if (item.nroHab != 0)
                    result += '<tr><td><span id ="nombre' + item.idTipoHab + '">' + item.nombreTipoHab + '</span></td>';
                else
                    result += '<tr><td><font color="#FF0000"><span id ="nombre' + item.idTipoHab + '">' + item.nombreTipoHab + '(No Disponible)</span></font></td>';

                result += '<td>' + '<select id= "numHabitSelect' + item.idTipoHab + '" class = "numHabitSelect">';

                for (j = 0; j <= item.nroHab; j++) {
                    result += '<option value = "' + j + '" id = "idTipoHab' + j + item.idTipoHab + '">' + j + '</option>';
                }

                result += '</td><td><span id = "precio' + item.idTipoHab + '">' + item.precioTipoHab + '</span></td>';
                result += '</select></td><td><span id = "subtotal' + item.idTipoHab + '">' + 0 + '</span></td></tr>';

                id = "#numHabitSelect";
                id += item.idTipoHab;
                arreglosId[item.idTipoHab] = id;
                arreglosHabit[item.idTipoHab] = item.listaDisp;
            });
            $('#tablaTipos').html(result);


            if (parseInt($("#Total").text()) > 0) {
                $("#tipos2").show("slow");
            }
            else if (parseInt($("#Total").text()) < 1) {
                $("#tipos2").hide("slow");
            }

            $("#TotalAjeno").text(
    parseFloat($("#Adicional").text()) +
    parseFloat($("#Total").text())
    );

            $("#cantDias").text(cantDias);

            console.log(arreglosId);

            arreglosId.forEach(function (elemento) {
                $(elemento).change(function (event) {
                    var cmd = "";
                    var cambiar = 0;
                    cmd += elemento + ".numHabitSelect option:selected";

                    var x = $(cmd).attr("value");
                    var n = elemento.substring(15);

                    var subtotal = "#subtotal";
                    var precio = "#precio";
                    subtotal += n;
                    precio += n;
                    //                n = n - 1;

                    var y = $(precio).text();

                    $(subtotal).text(x * y * cantDias);

                    if (parseInt($("#Total").text()) > 0) {
                        cambiar = 0;
                    }
                    else {
                        cambiar = 1;
                    }
                    $("#Total").text(0);
                    arreglosId.forEach(function (elemento) {

                        var num = elemento.substring(15);
                        var sub = "#subtotal";

                        sub += num;

                        $("#Total").text(
                    parseFloat($("#Total").text()) +
                    parseFloat($(sub).text())
                );

                        $("#TotalAjeno").text(
                parseFloat($("#Adicional").text()) +
                parseFloat($("#Total").text())
                );
                    });
                    if (parseInt($("#Total").text()) > 0) {
                        $("#tipos2").show("slow");
                    }
                    else if (parseInt($("#Total").text()) < 1) {
                        $("#tipos2").hide("slow");
                    }

                });
            });
        }
    }
}

function clickFechain() {
    var x = $("#FechaLlegada");
    x.attr("value", "");
}

function clickFechaout() {
    var x = $("#FechaSalida");
    x.attr("value", "");
}

function mostrarFieldAeropuerto() {
    $("#fieldAeropuerto").show();    
}

$(function () {
    $("#tabs").tabs();
});

function continuar2() {    
    $("#tabs").tabs('select','#tabs-2'); 
}

function continuar3() {
    $("#tabs").tabs('select', '#tabs-3');
}

function AddAlmuerzo() {
    if ($(this).is(":checked")) {
        $("#checkAlmuerzo:checkbox:not(:checked)").attr("checked", "checked");
        $("#Adicional").text(
        parseFloat($('#Adicional').text()) + 10
        );

    } else {
        $("#checkAlmuerzo:checkbox:checked").removeAttr("checked");
        $("#Adicional").text(
        parseFloat($('#Adicional').text()) - 10
        );
    }

    $("#TotalAjeno").text(
        parseFloat($("#Adicional").text()) +
        parseFloat($("#Total").text())
    );
}

function AddDesayuno() {
    if ($(this).is(":checked")) {
        $("#checkDesayuno:checkbox:not(:checked)").attr("checked", "checked");
        $("#Adicional").text(
        parseFloat($('#Adicional').text()) + 10
        );

    } else {
        $("#checkDesayuno:checkbox:checked").removeAttr("checked");
        $("#Adicional").text(
        parseFloat($('#Adicional').text()) - 10
        );
    }
    $("#TotalAjeno").text(
        parseFloat($("#Adicional").text()) +
        parseFloat($("#Total").text())
    );
}

function AddCena() {
    if ($(this).is(":checked")) {
        $("#checkCena:checkbox:not(:checked)").attr("checked", "checked");
        $("#Adicional").text(
        parseFloat($('#Adicional').text()) + 10
        );

    } else {
        $("#checkCena:checkbox:checked").removeAttr("checked");
        $("#Adicional").text(
        parseFloat($('#Adicional').text()) - 10
        );
    }
    $("#TotalAjeno").text(
        parseFloat($("#Adicional").text()) +
        parseFloat($("#Total").text())
    );
}

