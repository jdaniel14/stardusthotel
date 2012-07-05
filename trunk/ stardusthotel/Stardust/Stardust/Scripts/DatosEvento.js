var registrar;
var idUser;
var correito = "";
var doc = $(document);
doc.ready(asignarListeners);

function asignarListeners() {



    $("#opcionR").hide();
    $("#opcionT").hide();
    $("#obtenerPass").click(sacarMail);
//    $("#pasear1").hide();
//    $("#pasear2").hide();




    $("#juridica").hide();
    $("#natural").hide();

    $("#NextC").click(sgteCliente);
    $("#NextConf").click(sgteConfirma);

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

function sgteCliente() {
    $("#tabs").tabs("select", "#Vcliente");
}

function sacarMail() {

    var correo = $("#mailNatural").get(0).value;
    var contra = $("#password").get(0).value;



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


function recibeMails(data) {
    console.log(data);
    registrar = data.me;

    var defecto = 0;
    $("#ComboCliente option[value=" + defecto + "]").attr("selected", true);
    $("#ComboCliente").trigger('change');

    $("#nDoc").attr("value", "");
    $("#nombreDReserva").attr("value", "");
    $("#razonDReserva").attr("value", "");
    //$("#ApellidoDReserva").attr("value", "");
    $("#telefNatural").attr("value", "");
    $("#telef").attr("value", "");
    $("#nTarjetaNatural").attr("value", "");
    $("#nTarjeta").attr("value", "");
    //$("#password").attr("value", "");

    $("#pasear1").show("slow");
    $("#pasear2").show("slow");

    if (data.me == 0) {
        $("#opcionR").show("slow");
        idUser = "";
        $("#opcionT").hide();
        correito = "";
    }
    else {

        idUser = data.idUsuario;

        $("#opcionT").show("slow");
        $("#opcionR").hide();
        correito = data.email;
        if (data.tipoDoc == "RUC") {
            var miValue = data.tipoDocumento
            $("#ComboCliente option[value=" + miValue + "]").attr("selected", true);
            $("#ComboCliente").trigger('change');
            //            var miValue = data.tipoDocumento
            //            $("#ComboCliente option[value=" + miValue + "]").attr("selected", true);
            $("#nDoc").attr("value", data.nroDocumento);
            $("#razonDReserva").attr("value", data.nombres);
            $("#telef").attr("value", data.celular);

            //            var miValue3 = data.tipoTarj;
            //            $("#tipoTarjetaJ option[value=" + miValue3 + "]").attr("selected", true);

            $("#nTarjeta").attr("value", data.nroTarjeta);

        }
        else {
            var miValue = data.tipoDocumento
            $("#ComboCliente option[value=" + miValue + "]").attr("selected", true);
            $("#ComboCliente").trigger('change');       
            //            var miValue2 = data.tipoDocumento
            //            $("#ComboCliente option[value=" + miValue2 + "]").attr("selected", true);
            $("#nDoc").attr("value", data.nroDocumento);
            $("#nombreDReserva").attr("value", data.nombres);
            //$("#ApellidoDReserva").attr("value", data.apell);
            $("#telefNatural").attr("value", data.celular);

            //            var miValue4 = data.tipoTarj;
            //            $("#tipoTarjeta option[value=" + miValue4 + "]").attr("selected", true);

            $("#nTarjetaNatural").attr("value", data.nroTarjeta);
        }

    }
}
