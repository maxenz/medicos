/**** SERVICIOS EN CURSO ****/

// --> Apenas inicializo, seteo el horario actual
setActualTime($('#horarioActualEnCurso'));

// --> Apenas inicializo, seteo el timer para que refresque la grilla de servicios en curso cada 15 segs
setInterval("refreshOpEnCurso()", 20000);

var crReclamarEnCurso = function (row, columnfield, value, defaulthtml, columnproperties) {
    var glyph = '<div style="text-align:center;margin-top:2px"><a href="#"><span class="glyphicon glyphicon-warning-sign big-icon naranja"></span></a></div>';
    return glyph;
}

// --> Seteo las columnas para la grilla de servicios en curso
var colGridOpEnCurso = [
                        { text: 'ID', datafield: 'ID', hidden: true },
                        { text: 'Inc', datafield: 'NroInc', width: '5%', cellsalign: 'center' },
                        { text: 'Orden', datafield: 'NroInterno', width: '10%', cellsalign: 'center' },
                        { text: 'Paciente', datafield: 'Nombre', width: '20%', cellsalign: 'center' },
                        { text: 'Domicilio', datafield: 'Domicilio', width: '17%', cellsalign: 'center' },
                        { text: 'Loc', datafield: 'Localidad', width: '5%', cellsalign: 'center' },
                        { text: 'Grado', datafield: 'Grado', width: '5%', cellsalign: 'center' },
                        { text: 'Móvil', datafield: 'WebMovil', width: '5%', cellsalign: 'center' },
                        { text: 'Arribo', datafield: 'Arribo', width: '10%', cellsalign: 'center' },
                        { text: 'Estado', datafield: 'WebEstado', width: '16%', cellsalign: 'center' },
                        { text: 'Reclamar', width: '7%', cellsalign: 'center', cellsrenderer: crReclamarEnCurso }
];

// --> Seteo los datafields para la grilla de servicios en curso
var dtFieldsOpEnCurso = [{ name: 'ID', type: 'number' }, { name: 'NroInc', type: 'string' },
                        { name: 'NroInterno', type: 'string' }, { name: 'Nombre', type: 'string' },
                        { name: 'Domicilio', type: 'string' }, { name: 'Localidad', type: 'string' },
                        { name: 'Grado', type: 'string' }, { name: 'WebMovil', type: 'string' },
                        { name: 'Arribo', type: 'string' }, { name: 'WebEstado', type: 'string' }];

// --> Obtengo via ajax los datos de la grilla de servicios en curso
function getSourceGrdOpEnCurso() {

    var src = {
        datatype: "json",
        datafields: dtFieldsOpEnCurso,
        url: 'OperativaClientes/GetServiciosEnCurso'
    };

    var dt = new $.jqx.dataAdapter(src);

    return dt;
}

// --> Seteo la grilla de servicios en curso con el source, columnas y datafields
$("#grdOperativaEnCurso").jqxGrid(
{
    width: '100%',
    autoheight: true,
    source: getSourceGrdOpEnCurso(),
    pageable: true,
    pagesize: 15,
    altrows: true,
    theme: 'arctic',
    columns: colGridOpEnCurso,
    pagesizeoptions: ['15']
});

// --> Función para refrescar la grilla de servicios en curso cada 20 segundos
function refreshOpEnCurso() {
    $('#grdOperativaEnCurso').jqxGrid({ source: getSourceGrdOpEnCurso() });
    setActualTime($('#horarioActualEnCurso'));
}

// --> Función para setear el horario actual
function setActualTime(objHorario) {
    objHorario.text(moment().format('HH:mm:ss'));
}

/**** TERMINA SERVICIOS EN CURSO ****/





/**** SERVICIOS FINALIZADOS ****/

// --> Filtro de fecha de servicios finalizados
$("#dtServFinalizados").jqxDateTimeInput({ width: '100%', height: '25px', theme: 'bootstrap', culture: 'es-AR', selectionMode: 'range' });

$('#dtServFinalizados').on('valuechanged', function (event) {

    $('#grdServFinalizados').jqxGrid({ source: getSourceGrdFinalizados() });
    
    //refrescar source de grilla de servicios finalizados  
});


// --> Seteo las columnas para la grilla de servicios finalizados
var colGridFinalizados = [
                        { text: 'ID', datafield: 'ID', hidden: true },
                        { text: 'Fecha', datafield: 'FecIncidente', width: '8%', cellsalign: 'center' },
                        { text: 'Inc', datafield: 'NroIncidente', width: '3%', cellsalign: 'center' },
                        { text: 'Cliente', datafield: 'ClienteID', width: '5%', cellsalign: 'center' },
                        //{ text: 'Llamada', datafield: 'Llamada', width: '3%', cellsalign: 'center' },
                        { text: 'Nro. Afiliado', datafield: 'IntegranteID', width: '8%', cellsalign: 'center' },
                        { text: 'Paciente', datafield: 'Nombre', width: '16%' },
                        { text: 'Domicilio', datafield: 'Domicilio', width: '13%' },
                        { text: 'Loc', datafield: 'Localidad', width: '9%' },
                        { text: 'Síntoma', datafield: 'Sintoma', width: '11%' },
                        { text: 'Grado', datafield: 'Grado', width: '5%', cellsalign: 'center' },
                        //{ text: 'Arribo', datafield: 'Arribo', width: '3%', cellsalign: 'center' },
                        { text: 'Diagnóstico', datafield: 'Diagnostico', width: '12%' },
                        { text: 'Deriva', datafield: 'Deriva', width: '6%', cellsalign: 'center' },
                        { text: 'Final', datafield: 'Final', width: '4%', cellsalign: 'center' }
];

// --> Seteo los datafields para la grilla de servicios finalizados
var dtFieldsFinalizados = [{ name: 'ID', type: 'number' }, { name: 'FecIncidente', type: 'string' },
                        { name: 'NroIncidente', type: 'string' }, { name: 'ClienteID', type: 'string' },
                        //{ name: 'Llamada', type: 'string' },
                        { name: 'IntegranteID', type: 'string' },
                        { name: 'Nombre', type: 'string' }, { name: 'Domicilio', type: 'string' },
                        { name: 'Localidad', type: 'string' }, { name: 'Sintoma', type: 'string' },
                        { name: 'Grado', type: 'string' },
                        //{ name: 'Arribo', type: 'string' },
                        { name: 'Diagnostico', type: 'string' }, { name: 'Deriva', type: 'string' },
                        { name: 'Final', type: 'string' }];

// --> Obtengo via ajax los datos de la grilla de servicios finalizados
function getSourceGrdFinalizados() {

    var fd = getRangoFechas(0);
    var fh = getRangoFechas(1);

    var src = {
        datatype: "json",
        datafields: dtFieldsFinalizados,
        url: 'OperativaClientes/GetServiciosFinalizados',
        data: 
            {
                fecDesde: fd,
                fecHasta: fh
            }
    };

    var dt = new $.jqx.dataAdapter(src);

    return dt;
}

function getRangoFechas(idx) {
    var jsDate = $('#dtServFinalizados').jqxDateTimeInput('getText');
    var vDate = jsDate.split("-");
    var dateCalculate = vDate[idx].trim();
    var vMesAnioDia = dateCalculate.split("/");
    return vMesAnioDia[2] + vMesAnioDia[1] + vMesAnioDia[0];
}

// --> Seteo la grilla de servicios en curso con el source, columnas y datafields

setValuesGrid($('#grdServFinalizados'),
              getSourceGrdFinalizados(),
              colGridFinalizados);


/**** TERMINA SERVICIOS FINALIZADOS ****/

//$('#popupValidarErroneos').modal('show');

function setValuesGrid(objGrid, src, columnas) {

    objGrid.jqxGrid(
        {
            width: '100%',
            autoheight: true,
            source: src,
            pageable: true,
            pagesize: 15,
            altrows: true,
            theme: 'arctic',
            columns: columnas,
            pagesizeoptions: ['15']
        });

}

function getSourceGrid(dtFields,urlRest) {

    var src = {
        datatype: "json",
        datafields: dtFields,
        url: urlRest
    };

    var dt = new $.jqx.dataAdapter(src);

    return dt;

}

$('#exportarAExcel').click(function () {

    $("#grdServFinalizados").jqxGrid('exportdata', 'xls', 'servicios_finalizados');

});

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    //$('#grdServFinalizados').jqxGrid('autoresizecolumns');
});