/**** SERVICIOS EN CURSO ****/

// --> Apenas inicializo, seteo el horario actual
setActualTime($('#horarioActualEnCurso'));

// --> Apenas inicializo, seteo el timer para que refresque la grilla de servicios en curso cada 15 segs
setInterval("refreshOpEnCurso()", 20000);

// --> Seteo las columnas para la grilla de servicios en curso
var colGridOpEnCurso = [
                        { text: 'ID', datafield: 'ID', hidden: true },
                        { text: 'Inc', datafield: 'NroInc', width: '5%', cellsalign: 'center' },
                        { text: 'Orden', datafield: 'NroInterno', width: '10%', cellsalign: 'center' },
                        { text: 'Paciente', datafield: 'Nombre', width: '20%', cellsalign: 'center' },
                        { text: 'Domicilio', datafield: 'Domicilio', width: '20%', cellsalign: 'center' },
                        { text: 'Loc', datafield: 'Localidad', width: '5%', cellsalign: 'center' },
                        { text: 'Grado', datafield: 'Grado', width: '5%', cellsalign: 'center' },
                        { text: 'Móvil', datafield: 'WebMovil', width: '5%', cellsalign: 'center' },
                        { text: 'Arribo', datafield: 'Arribo', width: '10%', cellsalign: 'center' },
                        { text: 'Estado', datafield: 'WebEstado', width: '20%', cellsalign: 'center' }
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
    $('#grdOperativaEnCurso').jqxGrid({ source: getSourceGrdOpEnCurso()});
    setActualTime($('#horarioActualEnCurso'));
}

// --> Función para setear el horario actual
function setActualTime(objHorario) {
    objHorario.text(moment().format('HH:mm:ss'));
}

/**** TERMINA SERVICIOS EN CURSO ****/





/**** SERVICIOS FINALIZADOS ****/

// --> Filtro de fecha de servicios finalizados
$("#dtServFinalizados").jqxDateTimeInput({ width: '100%', height: '25px', theme: 'bootstrap', culture: 'es-AR', readonly: true });

$('#dtServFinalizados').on('valuechanged', function (event) {
    var jsDate = $(this).jqxDateTimeInput('getText');
    //refrescar source de grilla de servicios finalizados  
});

/**** TERMINA SERVICIOS FINALIZADOS ****/

//$('#popupValidarErroneos').modal('show');