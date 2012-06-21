var doc = $(document);
doc.ready(cargarSegHoja);
var registrar;

function cargarSegHoja() {

    $("#opcionR").hide();
    $("#obtenerPass").click(sacarMail);
    $("#pasear1").hide();
    $("#pasear2").hide();

    

    $("#maps").gMap({
        controls: false,
        markers: [
        { 
        controls:false,
        latitude: -12.093084,
        longitude: -77.046491,
        html: "Stardust Hotel",
        popup: true
        }],
        zoom: 14
    }
    );
    

    $("#juridica").hide();
    $("#natural").hide();

    $("#ComboCliente").change(function () {
        var valueCombo = $("#ComboCliente").val();
        
        if (valueCombo == "RUC") {
            $("#natural").hide("slow");        
            $("#juridica").show("slow");        
        }
        else if (valueCombo == "DNI") {
            $("#juridica").hide("slow");        
            $("#natural").show("slow");        
        }
        else if (valueCombo == "0") {
            $("#juridica").hide("slow");
            $("#natural").hide("slow");        
        }
    });
}

function sacarMail() {

    var correo = $("#mailNatural").get(0).value;
    var contra = $("#password").get(0).value;

    

    var pasasela = {
        mail:correo,
        pass:contra        
    }

    var jsonData = JSON.stringify(pasasela);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "URL",
        success: recibeMails
    });


}


function recibeMails(data) {
    console.log(data);
    var registrar = data.me;

    $("#pasear1").show("slow");
    $("#pasear2").show("slow");

    if (data.me == 0) {
        $("#opcionR").show("slow");
    }
    else {
        if (data.tipoDoc == "RUC") {
            var miValue = data.tipoDoc
            $("#ComboCliente option[value=" + miValue + "]").attr("selected", true);
            $("#nDoc").attr("value", data.nroDoc);
            $("#razonDReserva").attr("value".data.nomb);
            $("#telef").attr("value", data.telf);

            var miValue3 = data.tipoTarj;
            $("#tipoTarjetaJ option[value=" + miValue3 + "]").attr("selected", true);

            $("#nTarjeta").attr("value", data.nroTarj);

        }
        else {
            var miValue2 = data.tipoDoc
            $("#ComboCliente option[value=" + miValue2 + "]").attr("selected", true);
            $("#nDoc").attr("value", data.nroDoc);
            $("#nombreDReserva").attr("value".data.nomb);
            $("#ApellidoDReserva").attr("value", data.apell);
            $("#telefNatural").attr("value", data.telf);

            var miValue4 = data.tipoTarj;
            $("#tipoTarjeta option[value=" + miValue4 + "]").attr("selected", true);

            $("#nTarjetaNatural").attr("value", data.nroTarj);
        }
        
    }
}