var arregloChecks = new Array();
var x;
var SendHotel;

x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {



    $.ajax({
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        statusCode: {
            500: function () {
                $("#espera").dialog("destroy");
                mostrarError("Error inesperado... intentelo mas tarde :)");                
            }
        },
        url: "/ReservarHabitacion/listarHoteles",
        beforeSend: esperarHoteles(),
        success: llegadaHoteles
    });


    $("#FechaInicio").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#FechaFin").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#FechaInicio").datepicker("option", "minDate", new Date(year, (month - 1), daym));
    $("#FechaFin").datepicker("option", "minDate", new Date(year, (month - 1), daym + 1));
   
//    x = $("#FechaInicio");
//    x.focus(clickFechain);
//    x = $("#FechaFin");
//    x.focus(clickFechaout);

    $("#NextE").click(sgteEvento);

    $("#pestana4").hide();

    $("#listaAmbientes").hide();

    $("#buscame").click(mostrarAmbientesDisponibles);

}

function esperarHoteles() {
    mostrarEspera();
}

function llegadaHoteles(data) {
    $("#espera").dialog("destroy");
    var escritor = "";
    console.log(data);
    if (data.me == "") {

        $.each(data.lista, function (i, item) {
            escritor += '<option value = "' + item.ID + '">' + item.nombre + '</option>';
        });

        $("#ComboHoteles").html(escritor);
    }
    else {
        mostrarError(data.me);
    }
}


function sgteEvento() {
    $("#tabs").tabs("select", "#Vevento");
}

function mostrarAmbientesDisponibles() {
    

    var fechaInicio = $("#FechaInicio").attr("value");
    var fechaFinal = $("#FechaFin").attr("value");
    SendHotel = $("#ComboHoteles").val();
    

    var Hotel = {
        idHotel: SendHotel,
        fechaIni: fechaInicio,
        fechaFin: fechaFinal
    }
    var jsonData = JSON.stringify(Hotel);

    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        statusCode: {
            500: function () {
                $("#espera").dialog("destroy");
                mostrarError("Error inesperado... intentelo mas tarde :)");
            }
        },
        contentType: "application/json; charset=utf-8",
        url: "consultarAmbientesDisponibles",
        beforeSend: esperaAmbientes(),
        success: llegadaAmbientes
    });
}

function esperaAmbientes() {
    mostrarEspera();
}

function llegadaAmbientes(data) {
    $("#espera").dialog("destroy");
    console.log(data);
    console.log(data.me);
    

    if (data.me == "") {

        $("#listaAmbientes").show("slow");

        cantidadDeDias = data.cantDias;

        var lista = data.listaAmbientes;

        result = "";

        

        $.each(lista, function (i, item) {

            result += '<tr>';

            result += '<td><input type="checkbox" id="check' + item.id + '"/>';

            result += '<td><span id = "nombre' + item.id + '">' + item.nombre + '</span></td>';

            result += '<td><span id = "capacidad' + item.id + '">' + item.cap_maxima + '</span></td>';

            result += '<td><span id = "precio' + item.id + '">' + item.precioXhora + '</span></td>';

            result += '<td><span id = "subTotal' + item.id + '">0' + '</span></td>';

            id = "#check";
            id += item.id;
            arregloChecks.push(id);

            result += '</tr>';

        });

        $("#tablaTipos").html(result);

        $("#Cant").text(cantidadDeDias);

        arregloChecks.forEach(function (elemento) {

            $(elemento).click(function (event) {

                var cmd = elemento;
                var n = cmd.substring(6);

                var precio = "#precio";
                precio += n;
                var subTotal = "#subTotal";
                subTotal += n;

                if ($(elemento).is(":checked")) {
                    $(subTotal).text(
                    $(precio).text() * cantidadDeDias
                );

                    $("#Total").text(0);

                    arregloChecks.forEach(function (elemento) {
                        var num = elemento.substring(6);
                        var sumarle = "#subTotal";
                        sumarle += num;

                        $("#Total").text(
                        parseFloat($("#Total").text()) +
                        parseFloat($(sumarle).text())
                    );

                    });

                } else {
                    $(subTotal).text(0);
                    $("#Total").text(0);

                    arregloChecks.forEach(function (elemento) {
                        var num = elemento.substring(6);
                        var sumarle = "#subTotal";
                        sumarle += num;

                        $("#Total").text(
                        parseFloat($("#Total").text()) +
                        parseFloat($(sumarle).text())
                    );

                    });
                }
            });

        });
    }
    else {
        $("#FechaInicio").attr("value", "");
        $("#FechaFin").attr("value", "");
        mostrarError(data.me);
    }

}







//Funcion del Tabs
$(function () {
    $("#tabs").tabs();
});

function clickFechain() {
    var x = $("#FechaInicio");
    x.attr("value", "");
}

function clickFechaout() {
    var x = $("#FechaFin");
    x.attr("value", "");
}

