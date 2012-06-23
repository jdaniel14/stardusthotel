var doc = $(document);
doc.ready(cargarSegHoja);
var registrar;

function cargarSegHoja() {

    

    $("#opcionR").hide();
    $("#opcionT").hide();

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

    $("#pasear1").hide("slow");
    $("#pasear2").hide("slow");

    var correo = $("#mailNatural").get(0).value;
    var contra = $("#password").get(0).value;



    if ((correo != "") && (contra != "")) {

        var pasasela = {
            mail: correo,
            pass: contra
        }

        var jsonData = JSON.stringify(pasasela);
        console.log(jsonData);

        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "../ReservarHabitacion/Login",
            success: recibeMails
        });
    }
    else {
        mostrarDialogo();
    }

}


function recibeMails(data) {
    console.log(data);
    var registrar = data.me;
    var defecto = 0;
    $("#ComboCliente option[value=" + defecto + "]").attr("selected", true);
    $("#ComboCliente").trigger('change');

    $("#nDoc").attr("value", "");
    $("#nombreDReserva").attr("value", "");
    $("#razonDReserva").attr("value", "");
    $("#ApellidoDReserva").attr("value", "");
    $("#telefNatural").attr("value", "");
    $("#telef").attr("value", "");
    $("#nTarjetaNatural").attr("value", "");
    $("#nTarjeta").attr("value", "");
    

    $("#pasear1").show("slow");
    $("#pasear2").show("slow");

    if (data.me == 0) {
        $("#opcionR").show("slow");

        $("#opcionT").hide();

        idUser = "";
    }
    else {

        idUser = data.idUsuario;

        $("#opcionT").show("slow");
        $("#opcionR").hide();
        if (data.tipoDoc == "RUC") {
            
            var miValue = data.tipoDocumento
            $("#ComboCliente option[value=" + miValue + "]").attr("selected", true);
            $("#ComboCliente").trigger('change');
            $("#nDoc").attr("value", data.nroDocumento);
            $("#razonDReserva").attr("value",data.nombres);
            $("#telef").attr("value", data.celular);

//            var miValue3 = data.tipoTarj;
//            $("#tipoTarjetaJ option[value=" + miValue3 + "]").attr("selected", true);

            $("#nTarjeta").attr("value", data.nroTarjeta);

        }
        else {
             
         var miValue2 = data.tipoDocumento
         $("#ComboCliente option[value=" + miValue2 + "]").attr("selected", true);
         $("#ComboCliente").trigger('change');
         //$("#ComboCliente").val("RUC");
           $("#nDoc").attr("value", data.nroDocumento);
            $("#nombreDReserva").attr("value",data.nombres);
            //$("#ApellidoDReserva").attr("value", data.apell);
            $("#telefNatural").attr("value", data.celular);

//            var miValue4 = data.tipoTarj;
//            $("#tipoTarjeta option[value=" + miValue4 + "]").attr("selected", true);

            $("#nTarjetaNatural").attr("value", data.nroTarjeta);
        }
        
    }
}



