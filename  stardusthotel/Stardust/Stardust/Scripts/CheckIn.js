
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {


    $("#buscarReserva").click(comienzaElChongo);    
}

function comienzaElChongo() {

    var reserva = $("#nroReserva").get(0).value;

    var Envio = {
        idHotel: "2",
        idReserva: reserva
    }

    jsonData = JSON.stringify(Envio);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "CheckIn",
        beforeSend: esperaRecibirDatosCheckIn(),
        success: llegadaDatosCheckIn
    });
}

function esperaRecibirDatosCheckIn(){
}

function llegadaDatosCheckIn(data) {
    console.log(data);

    result = "";

    result += '<div class="formRow"><span>Doc. de identidad</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + data.doc + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Nombre</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + data.nomb + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha de Registro</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + data.fechaReg + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha de llegada</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nroReserva>' + data.fechaLleg + '</span>';
    result += '</div><div class="clear"></div></div>';

    var lista = data.lista;

    result = "";
    var k = 0;

    var habitaciones = new Array();
    var cantXHabit = new Array();

    $.each(lista, function (i, item) {

        k++;
        result += '<h3><a href="#">' + item.nombTipoHab + '</a></h3>';
        result += '<div>';

        var numPersonas = data.nroPers;

        result += '<div class="formRow"><span>Cantidad de personas a registrar</span>';
        result += '<div class = "formRight" >';
        result += '<span id = cantHabit' + k + '>' + data.nroPers + '</span>';
        result += '</div><div class="clear"></div></div>';

        var listaHabitaciones = item.listaHab;

        var m = 0;

        $.each(lista, function (j, cosito) {

            var id = cosito.idHab;
            
            habitaciones.push(id);
            cantXHabit.push(numPersonas);

            m++;
            result += '<div class="formRow"><span>Habitacion ' + cosito.numero + '</span></div>';

            var n = 0;
            for (n = 0; n < numPersonas; n++) {

                result += '<div class="formRow"><span>Doc. de identidad</span>';
                result += '<div class = "formRight" >';
                result += '<input type="text" id="DNICliente' + cosito.idHab + '-' + n + '"/>';
                result += '</div><div class="clear"></div></div>';

                result += '<div class="formRow"><span>Nombre</span>';
                result += '<div class = "formRight" >';
                result += '<input type="text" id="nombCliente' + cosito.idHab + '-' + n + '"/>';
                result += '</div><div class="clear"></div></div>';

            }
        });

        result += '</div>';

    });

    $("#accordion").html(result);

    var dni = "DNICliente";
    var nombre = "nombCliente";

    var listaDevolver;

    habitaciones.forEach(function (elemento) {

        var idHabit = elemento;
        dni += idHabit;
        

    });


}


//result += '<h3><a href="#">Section 1</a></h3><div></div>';
//result += '<h3><a href="#">Section 1</a></h3><div></div>';
//result += '<h3><a href="#">Section 1</a></h3><div></div>';
//$("#accordion").html(result);

//<h3><a href="#">Section 1</a></h3>
//        <div>
//            <div class="formRow"><span>Nro. de reserva</span>                
//				<div class = "formRight" > 
//					<input type="text" id="num"/>
//                    </div>                
//				<div class="clear"></div>
//			</div>
//        </div>



$(function () {
    $("#accordion").accordion();
});