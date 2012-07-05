var res;
var x = $(document);
x.ready(inicio);

function inicio() {
    $("#gogo").click(enviar);

}

function enviar() {

    var res = $("#idReserva").val();

    if (res != "") {


        var enviar = {
            idEvento: res
        }

        var jsonData = JSON.stringify(enviar);
        console.log(jsonData);
        $.ajax({
            type: "POST",
            data: jsonData,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            url: "CheckOut",
            beforeSend: inicioMostrar(),
            success: FinMostrar
        });
    }
    else {
        mostrarError("Ingrese la reserva");
    }
}

function inicioMostrar() {
}

function FinMostrar(data) {
    console.log(data);

    if (data.me == "") {

        res = data.id;
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
                
        result += '</div>';

        $("#datosCliente").html(result);

        result = "";

        var lista = data.listaDetalle;

        result += '<div class="widget"><div class = "title"><h6>Detalle de cuenta</h6></div>'

        result += '<table cellpadding = "0" cellspacing = "0" width = "100%" class = "sTable" >';

        result += '<thead><tr><td>Detalle</td><td>Cantidad</td><td>Precio Unitario</td><td>Total</td></tr></thead>';

        result += '<tbody>';

        $.each(lista, function (i, item) {

            result += '<tr>';

            result += '<td align = "center">';
            result += '<span>' + item.detalle + '</span>';
            result += '</td>';

            result += '<td align = "center">';
            result += '<span>' + item.cantidad + '</span>';
            result += '</td>';

            result += '<td align = "center">';
            result += '<span>' + item.precioUnit + '</span>';
            result += '</td>';

            result += '<td align = "center">';
            result += '<span>' + item.total + '</span>';
            result += '</td>';

            result += '</tr>';

        });

        result += '<tr><td></td><td></td><td><h6>SubTotal</h6></td><td id="subTotal">' + data.subTotal + '</td></tr>';
        result += '<tr><td></td><td></td><td><h6>IGV</h6></td><td id="IGV">' + data.igv + '</td></tr>';
        result += '<tr><td></td><td></td><td><h6>Total</h6></td><td id="total">' + data.total + '</td></tr>';

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
        
        result += '</div>';

        result += '<br />';

        result += '<input id = "regPago" type="submit" value="Registrar Pago" class = "redB" />';

        $("#pago").html(result);

        //$("#cantPagando").change(actualizarVuelto);
        $("#regPago").click(enviarviar);

    }
    else {
        mostrarError(data.me);
    }
}

function enviarviar() {
    
    var enviar = {
        idEvento: res
    }

    var jsonData = JSON.stringify(enviar);
    console.log(jsonData);
    $.ajax({
        type: "POST",
        data: jsonData,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        url: "RegistrarCheckOut",
        beforeSend: inicioEnvio(),
        success: FinEnvio
    });
}

function inicioEnvio() {
    mostrarEspera();
}

function FinEnvio(data) {
    $("#espera").dialog("destroy");
    console.log(data.me);

    if (data.me == "") {

        console.log("se hizo");

        mostrarConfirmacionFinal('Realizada ^^');
    }
    else {
        mostrarError(data.me);

    }
}

