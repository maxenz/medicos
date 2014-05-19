$("#btnConsultarResumen").jqxButton({ width: '100', theme: 'bootstrap', height: '26' });

// --> Seteo dropdownlist para seleccionar periodo en la grilla de servicios
// --> Uso mismo source que filtro de periodo de guardias

$("#ftrPeriodoResumen").jqxDropDownList({
    selectedIndex: 2, source: dtFtrPeriodoGuardias, displayMember: "Descripcion",
    valueMember: "Periodo", width: '110%', dropDownHeight: 80, height: 25, theme: 'bootstrap'
});

$("#ftrCoordResumen").jqxDropDownList({
    selectedIndex: 0, source: dtFtrCoordGuardias, displayMember: "Descripcion",
    valueMember: "ID", width: '110%', dropDownHeight: 80, height: 25, theme: 'bootstrap'
});

function getSelectedPeriodoResumen() {
    return $("#ftrPeriodoResumen").jqxDropDownList('getSelectedItem').value;
}

function getSelectedCoordResumen() {
    return $('#ftrCoordResumen').jqxDropDownList('getSelectedItem').value;
}

/*********************************************************************************************************/

// --> Seteo datafields de grilla de resumen

var dtFieldsResumen = [{ name: 'Item', type: 'string' }, { name: 'Importe', type: 'number' }];

// --> Seteo source grilla de resumen

function getSourceGridResumen() {

    var srcGridResumen = {
        datatype: "json",
        datafields: dtFieldsResumen,
        url: 'Medicos/GetResumenLiquidacion',
        data: {
            periodo: getSelectedPeriodoResumen(),
            coordinacion: getSelectedCoordResumen()
        }
    };

    var dtGridResumen = new $.jqx.dataAdapter(srcGridResumen);

    return dtGridResumen;
}

var dtGridResumen = getSourceGridResumen();

// --> Seteo columnas de la grilla de guardias

var colGridResumen = [
                        { text: 'Item', datafield: 'Item', width:'50%' },
                        { text: 'Importe', datafield: 'Importe', width: '50%', cellsalign: 'right', cellsformat: 'c2' },
                    ];

// --> Seteo objeto de la grilla de guardias con todos los valores

$("#grdResumen").jqxGrid(
{
    width: '99%',
    autoheight: true,
    source: dtGridResumen,
    pageable: true,
    pagesize: 8,
    altrows: true,
    theme: 'arctic',
    columns: colGridResumen,
    pagesizeoptions: [8]
});

$('#grdResumen').on('bindingcomplete', function (event) {
    $grid = $(this);
    $grid.jqxGrid('localizestrings', localizationobj);
    $grid.jqxGrid('gotopage', 1);
    $grid.jqxGrid('gotopage', 0);
});

$('#btnConsultarResumen').on('click', function () {

    var dtGridResumen = getSourceGridResumen();

    $('#grdResumen').jqxGrid({ source: dtGridResumen });

});