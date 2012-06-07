var doc = $(document);
doc.ready(cargarSegHoja);

function cargarSegHoja() {


    $("#maps").gMap({ markers: [{ latitude: 47.660937,
        longitude: 9.569803,
        html: "Tettnang, Germany",
        popup: true
    }],
        zoom: 6
    });
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