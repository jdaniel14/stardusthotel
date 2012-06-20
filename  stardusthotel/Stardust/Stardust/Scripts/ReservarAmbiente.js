
var x;

x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {

    $("#FechaInicio").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#FechaFin").datepicker({ dateFormat: 'dd-mm-yy' });
   
    x = $("#FechaInicio");
    x.focus(clickFechain);
    x = $("#FechaFin");
    x.focus(clickFechaout);

    $("#NextE").click(sgteEvento);

    $("#pestana4").hide();
    

    $("#buscame").click(mostrarAmbientesDisponibles);

}

function sgteEvento() {
    $("#tabs").tabs("select", "#Vevento");
}

function mostrarAmbientesDisponibles() {
    $("#listaAmbientes").show("slow");

    var fechaInicio = $("#FechaInicio").attr("value");
    var fechaFinal = $("#FechaFin").attr("value");
    

    var Hotel = {
        idHotel: "1",
        fechaIni: fechaInicio,
        fechaFin: fechaFinal
    }
    var jsonData = JSON.stringify(Hotel);

    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "consultarAmbientesDisponibles",
        beforeSend: esperaAmbientes(),
        success: llegadaAmbientes
    });
}

function esperaAmbientes() {
}

function llegadaAmbientes(data) {
    console.log(data);
    console.log(data.me);

    if (data.me == "") {

        cantidadDeDias = data.cantDias;

        var lista = data.listaAmbientes;

        result = "";

        var arregloChecks = new Array();

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
        alert(data.me);
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

