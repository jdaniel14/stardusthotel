
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

    var lista = data.listaAmbientes;

    result = "";

    var arregloChecks = new Array();

    $.each(lista, function (i, item) {



        result += '<tr>';

        result += '<td><input type="checkbox" id="check' + item.idAmbiente + '"/>';

        result += '<td><span id = "nombre' + item.idAmbiente + '">' + item.nombAmbiente + '</span></td>';

        result += '<td><span id = "capacidad' + item.idAmbiente + '">' + item.capAmbiente + '</span></td>';

        result += '<td><span id = "precio' + item.idAmbiente + '">' + item.precioAmb + '</span></td>';        

        result += '<td><span id = "subTotal' + item.idAmbiente + '">' + '</span></td>';

        id = "#check";
        id += item.idAmbiente;
        arregloChecks.push(id);

        result += '</tr>';

    });

    arreglosChecks.forEach(function (elemento) {

        $(elemento).click(function (event) {

            var cmd = elemento;
            var n = cmd.substring(5);
            var precio = "#precio";
            precio += n;
            var subTotal = "#subTotal";
            subTotal += n;

            if ($(elemento).is(":checked")) {
                $(subTotal).text(
                    $("#precio").text()
                );

                arregloChecks.forEach(function (elemento) {
                    var num = elemento.substring(5);
                    var sumarle = "#precio";
                    sumarle += num;

                    $("#Total").text(
                        parseFloat($("#Total").text()) +
                        parseFloat($(sumarle).text())
                    );

                });

            } else {
                $("subTotal").text(0);

                arregloChecks.forEach(function (elemento) {
                    var num = elemento.substring(5);
                    var sumarle = "#precio";
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

