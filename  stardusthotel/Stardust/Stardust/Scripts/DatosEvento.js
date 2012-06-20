
var doc = $(document);
doc.ready(asignarListeners);

function asignarListeners() {

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

function sgteConfirma() {
    $("#pestana1").hide();
    $("#pestana2").hide();
    $("#pestana3").hide();
    $("#pestana4").show();
    $("#tabs").tabs("select", "#Vconfirmacion");
}