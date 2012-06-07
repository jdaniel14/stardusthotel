var doc = $(document);
doc.ready(cargarSegHoja);

function cargarSegHoja() {


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
    //$("#maps").html("xD");

    $("#juridica").hide();
    $("#natural").hide();
    //$("#general").hide();
    $("#ComboCliente").change(function () {

        var valueCombo = $("#ComboCliente").val();
        //alert(valueCombo);
        if (valueCombo == "RUC") {
            $("#natural").hide("slow");
            //$("#general").hide("slow");
            $("#juridica").show("slow");
            //$("#general").show();
        }
        else if (valueCombo == "DNI") {
            $("#juridica").hide("slow");
            //$("#general").hide("slow");
            $("#natural").show("slow");
            //$("#general").show();
        }
        else if (valueCombo == "0") {
            $("#juridica").hide("slow");
            $("#natural").hide("slow");
            //$("#general").hide();
        }
    });
}