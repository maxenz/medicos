setActualTime();

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

var dtFieldsOpEnCurso = [{ name: 'ID', type: 'number' }, { name: 'NroInc', type: 'string' },
                        { name: 'NroInterno', type: 'string' }, { name: 'Nombre', type: 'string' },
                        { name: 'Domicilio', type: 'string' }, { name: 'Localidad', type: 'string' },
                        { name: 'Grado', type: 'string' }, { name: 'WebMovil', type: 'string' },
                        { name: 'Arribo', type: 'string' }, { name: 'WebEstado', type: 'string' }];


function getSourceGrdOpEnCurso() {

    var src = {
        datatype: "json",
        datafields: dtFieldsOpEnCurso,
        url: 'OperativaClientes/GetServiciosEnCurso'
    };

    var dt = new $.jqx.dataAdapter(src);

    return dt;
}

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

setInterval("refreshOpEnCurso()", 20000);

function refreshOpEnCurso() {
    $('#grdOperativaEnCurso').jqxGrid({ source: getSourceGrdOpEnCurso()});
    setActualTime();
}

function setActualTime() {
    $('#horarioActual').text(moment().format('HH:mm:ss'));
}