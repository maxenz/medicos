/**** SERVICIOS FINALIZADOS ****/

// --> VARIABLES PARA SERVICIOS FINALIZADOS

// --> Filtro de fecha de servicios finalizados
$("#dtServErroneos").jqxDateTimeInput({ width: '100%', height: '25px', theme: 'bootstrap', culture: 'es-AR', selectionMode: 'range' });

// --> Seteo las columnas para la grilla de servicios finalizados
var colGridErroneos = [
                        { text: 'ID', datafield: 'ID', hidden: true },
                        { text: 'Fecha', datafield: 'FecIncidente', width: '8%', cellsalign: 'center' },
                        { text: 'Inc', datafield: 'NroIncidente', width: '5%', cellsalign: 'center' },
                        { text: 'Cliente', datafield: 'ClienteID', width: '5%', cellsalign: 'center' },
                        { text: 'Nro. Afiliado', datafield: 'NroAfiliado', width: '8%', cellsalign: 'center' },
                        { text: 'Orden', datafield: 'NroInterno', width: '7%', cellsalign: 'center' },
                        { text: 'Paciente', datafield: 'Nombre', width: '17%' },
                        { text: 'Domicilio', datafield: 'Domicilio', width: '17%' },
                        { text: 'Grado', datafield: 'Grado', width: '5%', cellsalign: 'center' },
                        { text: 'Error', datafield: 'DescError', width: '20%' },
                        { text: 'F/H Proces.', datafield: 'FecHorProcesamiento', width: '8%', cellsalign: 'center' }                      
];

// --> Seteo los datafields para la grilla de servicios finalizados
var dtFieldsErroneos = [{ name: 'ID', type: 'number' }, { name: 'FecIncidente', type: 'string' },
                        { name: 'NroIncidente', type: 'string' }, { name: 'ClienteID', type: 'string' },
                        { name: 'NroAfiliado', type: 'string' },
                        { name: 'NroInterno', type: 'string' }, { name: 'Nombre', type: 'string' },
                        { name: 'Domicilio', type: 'string' }, { name: 'DescError', type: 'string' },
                        { name: 'Grado', type: 'string' },
                        { name: 'FecHorProcesamiento', type: 'string' }];

// --> FUNCIONES PARA SERVICIOS FINALIZADOS

// --> Obtengo via ajax los datos de la grilla de servicios finalizados
function getSourceGrdErroneos() {

    var objDateTime = $("#dtServErroneos");
    var fd = getRangoFechas(0, objDateTime);
    var fh = getRangoFechas(1, objDateTime);

    var src = {
        datatype: "json",
        datafields: dtFieldsErroneos,
        url: 'OperativaClientes/GetServiciosClientes',
        data:
            {
                fecDesde: fd,
                fecHasta: fh,
                pWS: 3
            }
    };

    var dt = new $.jqx.dataAdapter(src);

    return dt;
}

/**** TERMINA SERVICIOS FINALIZADOS ****/

//$('#popupValidarErroneos').modal('show');

$('#dtServErroneos').on('valuechanged', function (event) {

    $('#grdServErroneos').jqxGrid({ source: getSourceGrdErroneos() });

    //refrescar source de grilla de servicios finalizados  
});

$('#exportarAExcelErroneos').click(function () {

    $("#grdServErroneos").jqxGrid('exportdata', 'xls', 'servicios_a_validar');

});

// --> EJECUTO CUANDO INICIO

// --> Seteo la grilla de servicios en curso con el source, columnas y datafields

setValuesGrid($('#grdServErroneos'),
              getSourceGrdErroneos(),
              colGridErroneos);

//console.log(moment().subtract('days',31).format('L'));

$("#dtServErroneos").jqxDateTimeInput('setRange', moment().subtract('days',31).format('L'), moment().format('L') );

$('#popupValidarErroneos').modal('show');
