
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

    if ((documento != "") && (nReserva != "")) {

        var enviar = {
            flag: tipo,
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
            url: "PagoAdelantado",
            beforeSend: esperaDatos(),
            success: llegadaDatos
        });
    }
    else {
        mostrarError("Faltan Datos");
    }
}

function esperaDatos() {
    //esperando
}

function llegadaDatos(data) {
    console.log(data);
    if (data.mensaje == "") {

        
        $("#nombReserva").text($("#nReserva").get(0).value);
        $("#nDocu").text(data.doc);
        $("#nombre").text(data.nom);
        $("#monto").text(data.montoInicial);
        $("#montoTotal").text(data.montoTotal);
        $("#mostrarDatos").show("slow");

        if (data.estado == 1) {
            $("#aVeces2").hide();
            $("#aVeces1").show("slow");
        }
        else {
            $("#aVeces1").hide();
            $("#aVeces2").show("slow")
            $("#estado").text("Cancelado");
        }

//        $("#vuelto").text(0);
        $("#cantPagando").attr("value", "0");
        $("#cantPagando").change(asignarVuelto);

        $("#pagar").click(alcohol);
    }
    else {
        mostrarError(data.me);
    }
}


function alcohol() {

//    if (parseFloat($("#vuelto").text()) < 0) {
//        mostrarError("Cantidad insuficiente");
//    }
    //    else {


    var mmm = parseFloat($("#cantPagando").get(0).value);
    var fff = parseFloat($("#monto").text());


    if (mmm >= fff) {

        var documento = $("#nDoc").get(0).value;
        var idReserva = $("#nReserva").get(0).value;
        var tipo = $("#ComboRes").val();
        var lili = $("#cantPagando").get(0).value;
        var total = $("#montoTotal").text();
        var inicial = $("#monto").text();
        var documento = $("#nDoc").get(0).value;
        //alert(total);
        var enviar = {
            flag: tipo,
            id: idReserva,
            monto: lili,
            montoTotal: total,
            pagoInicial: inicial,
            doc: documento
        }

        jsonData = JSON.stringify(enviar);
        console.log(jsonData);

        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "RegistrarPagoAdelantado",
            beforeSend: casiConfirma(),
            success: Comunion
        });
    }
    else {
        mostrarError("Monto Insuficiente");
    }
//    }

}

function casiConfirma() {
}

function Comunion(data) {
    $("#espera").dialog("destroy");
    console.log(data.me);

    if (data.me == "") {

        console.log("se hizo");
        mostrarConfirmacionFinal('Pago Realizado ^_^!');
       
    }
    else {
        mostrarConfirmacionFinal(data.me);
    }
}


function asignarVuelto() {
    
    if ($("#cantPagando").get(0).value == "") {
        $("#cantPagando").attr("value","0");
    }

       

//    $("#vuelto").text(
//        parseFloat($("#cantPagando").get(0).value)-
//        parseFloat($("#montoTotal").text())
//    );

    

}