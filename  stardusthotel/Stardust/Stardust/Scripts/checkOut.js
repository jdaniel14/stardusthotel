
var x;
x = $(document);
x.ready(inicializarEventos);

function inicializarEventos() {


    $("#buscarDoc").click(iniciarFlujo);
}


function iniciarFlujo() {

    var documento = $("#nroReserva").get(0).value;

    var enviar = {
        idReserva : documento
    }

    jsonData = JSON.stringify(enviar);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "direccionDondeRecibo",
        beforeSend: esperaCheckOut(),
        success: llegadaCheckOut
    });
}

function esperaCheckOut() {
}

function llegadaCheckOut(data) {
    var result = "";

    result += '<div class="widget"><div class="title"><h6>Datos del Cliente</h6></div>';


    result += '<div class="formRow"><span>Doc. de identidad</span>';
    result += '<div class = "formRight" >';
    result += '<span id = dni>' + data.dni + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Nombre</span>';
    result += '<div class = "formRight" >';
    result += '<span id = nombre>' + data.nombre + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha de inicio</span>';
    result += '<div class = "formRight" >';
    result += '<span id = fechaIni>' + data.fechaIni + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha Fin</span>';
    result += '<div class = "formRight" >';
    result += '<span id = fechaFin>' + data.fechaFin + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Fecha Actual</span>';
    result += '<div class = "formRight" >';
    result += '<span id = fechaHoy>' + data.fechaHoy + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '</div>';

    $("#datosCliente").html(result);

    result = "";

    var lista = data.listaDetalles;

    result += '<div class="widget"><div class = "title"><h6>Detalle de cuenta</h6></div>'

    result += '<table cellpadding = "0" cellspacing = "0" width = "100%" class = "sTable" >';

    result += '<thead><tr><td>Detalle</td><td>Cantidad</td><td>Precio Unitario</td><td>Total</td></tr></thead>';

    result += '<tfoot>';

    result += '<tr><th></th><th></th><th>Total</th><th id="subTotal">' + data.subTotal + '</th></tr>';
    result += '<tr><th></th><th></th><th>Total</th><th id="IGV">' + data.IGV + '</th></tr>';
    result += '<tr><th></th><th></th><th>Total</th><th id="total">' + data.total + '</th></tr>';    

    result += '</tfoot>';

    result += '<tbody>';

    $.each(lista, function (i, item) {

        result += '<tr>';

        result += '<td align = "center">';
        result += '<span>' + item.detalle + '</span>';
        result += '</td>';

        result += '<td align = "center">';
        result += '<span>' + item.cant + '</span>';
        result += '</td>';

        result += '<td align = "center">';
        result += '<span>' + item.pUnit + '</span>';
        result += '</td>';

        result += '<td align = "center">';
        result += '<span>' + item.totalDet + '</span>';
        result += '</td>';

        result += '</tr>';

    });

    result += '</tbody></table>';
    result += '</div>';

    $("#detalleGastos").html(result);

    result = "";

    result += '<div class="widget"><div class="title"><h6>Datos del Cliente</h6></div>';

    result += '<div class="formRow"><span>Monto Cancelado</span>';
    result += '<div class = "formRight" >';
    result += '<span id = "montoCancelado">' + data.montPagado + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Cantidad a Pagar</span>';
    result += '<div class = "formRight" >';
    result += '<span id = "faltante">' + data.faltante + '</span>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Monto Cancelado</span>';
    result += '<div class = "formRight" >';
    result += '<input type="text" id="cantPagando"/>';
    result += '</div><div class="clear"></div></div>';

    result += '<div class="formRow"><span>Vuelto</span>';
    result += '<div class = "formRight" >';
    result += '<span id = "vuelto"></span>';
    result += '</div><div class="clear"></div></div>';

    result += '</div>';

    result += '<br />';

    result += '<input id = "regPago" type="submit" value="Registrar Pago" class = "redB" />';

    $("#pago").html(result);

    $("#cantPagando").change(actualizarVuelto);
    $("#regPago").click(enviarDatos);
}

function actualizarVuelto() {
    $("#vuelto").text(
        parseFloat($("#vuelto").text()) -
        parseFloat($("#cantPagado").get(0).value)    
    );
}

function enviarDatos() {

    var documento = $("#nroReserva").get(0).value;

    var enviar = {
        idReserva: documento
    }

    jsonData = JSON.stringify(enviar);
    console.log(jsonData);

    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "direccionDondeRecibo",
        beforeSend: esperaConfirmacion(),
        success: confirma
    });

}


function esperaConfirmacion(){
}

function confirma(data) {
    alert('OK');
}


