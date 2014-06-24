$("#btnConsultarResumen").jqxButton({ width: '100', theme: 'bootstrap', height: '26' });

// --> Seteo dropdownlist para seleccionar periodo en la grilla de servicios
// --> Uso mismo source que filtro de periodo de guardias

$("#ftrPeriodoResumen").jqxDropDownList({
    selectedIndex: 2, source: setFtrPeriodoGuardias(), displayMember: "Descripcion",
    valueMember: "Periodo", width: '110%', dropDownHeight: 80, height: 25, theme: 'bootstrap'
});

// --> Seteo dropdownlist para seleccionar medicos en la grilla de servicios
// --> Uso mismo source que filtro de medicos de guardias

$("#ftrMedicoResumen").jqxDropDownList({
    source: getSourceFiltroMedicos(), displayMember: "Nombre", selectedIndex: 0,
    valueMember: "UsuarioID", width: '110%', dropDownHeight: 150, dropDownWidth: 320, height: 25, theme: 'bootstrap'
});

function setSrcFtrCoordResumen() {

    var srcFtrCoordResumen = {
        datatype: "json",
        datafields: [
            { name: 'ID' },
            { name: 'Descripcion' }
        ],
        url: 'Medicos/getFiltroCoordinaciones?usr_id=' + getSelectedMedicoResumen(),
        async: false
    };

    var dtFtrCoordResumen = new $.jqx.dataAdapter(srcFtrCoordResumen);

    return dtFtrCoordResumen;

}

$("#ftrCoordResumen").jqxDropDownList({
    selectedIndex: 0, source: setSrcFtrCoordResumen(), displayMember: "Descripcion",
    valueMember: "ID", width: '110%', dropDownHeight: 80, height: 25, theme: 'bootstrap'
});

$('#ftrMedicoResumen').on('select', function (event) {

    $('#ftrCoordResumen').jqxDropDownList({ source: setSrcFtrCoordResumen() });

});

function getSelectedPeriodoResumen() {
    return $("#ftrPeriodoResumen").jqxDropDownList('getSelectedItem').value;
}

function getSelectedPeriodoResumenDesc() {
    return $("#ftrPeriodoResumen").jqxDropDownList('getSelectedItem').label;
}

function getSelectedCoordResumen() {
    return $('#ftrCoordResumen').jqxDropDownList('getSelectedItem').value;
}

function getSelectedMedicoResumen() {
    return $("#ftrMedicoResumen").jqxDropDownList('getSelectedItem').value;
}

/*********************************************************************************************************/


// --> Seteo datafields de grilla de resumen

var dtFieldsResumen = [{ name: 'Item', type: 'string' }, { name: 'Importe', type: 'number' }];
var dtFieldsHorarios = [{ name: 'DiaDeLaSemana', type: 'string' }, { name: 'Entrada1', type: 'string' },
                        { name: 'Salida1', type: 'string' }, { name: 'Movil1', type: 'string' },
                        { name: 'Entrada2', type: 'string' }, { name: 'Salida2', type: 'string' },
                        { name: 'Movil2', type: 'string' }];

var crItem = function (row, columnfield, value, defaulthtml, columnproperties) {
    var item = $("#grdResumen").jqxGrid('getrowdata', row).Item;
    if ((item == "Total a Facturar") || (item == "Anticipos Cobrados")) {
        return '<div style="text-align:center"><span style="line-height:25px;font-weight: bold">' + value + '</span></div>';
    }
}

var crImporte = function (row, columnfield, value, defaulthtml, columnproperties) {
    var item = $("#grdResumen").jqxGrid('getrowdata', row).Item;
    var importe = $("#grdResumen").jqxGrid('getrowdata', row).Importe.toString();
    if ((item == "Total a Facturar") || (item == "Anticipos Cobrados")) {

        if (importe.indexOf("-") == -1) {
            return '<div style="text-align:right"><span style="line-height:25px;font-weight: bold">' + '$' + numberWithCommas(parseFloat(importe).toFixed(2)) + '</span></div>';
        } else {
            importe = importe.replace("-", " ");
            return '<div style="text-align:right"><span style="line-height:25px;font-weight: bold">' + '($' + numberWithCommas(parseFloat(importe).toFixed(2)) + ')' + '</span></div>';
        }
    }
}


// --> Seteo source grilla de resumen

function getSourceGridResumen() {

    var srcGridResumen = {
        datatype: "json",
        datafields: dtFieldsResumen,
        url: 'Medicos/GetResumenLiquidacion',
        data: {
            periodo: getSelectedPeriodoResumen(),
            coordinacion: getSelectedCoordResumen(),
            medico: getSelectedMedicoResumen()
        }
    };

    var dtGridResumen = new $.jqx.dataAdapter(srcGridResumen);

    $('#txtResumenPeriodo').text('Resumen Liquidación: ' + getSelectedPeriodoResumenDesc());

    return dtGridResumen;
}

function getSourceGridHorarios() {

    var srcGridHorarios = {
        datatype: "json",
        datafields: dtFieldsHorarios,
        url: 'Medicos/GetHorarios',
        data: {
            periodo: getSelectedPeriodoResumen(),
            medico: getSelectedMedicoResumen()
        }
    };

    var dtGridHorarios = new $.jqx.dataAdapter(srcGridHorarios);

    return dtGridHorarios;
}



// --> Seteo columnas de la grilla de guardias

var colGridResumen = [
                        { text: 'Item', datafield: 'Item', width:'50%', cellsrenderer: crItem, cellsalign: 'center' },
                        { text: 'Importe', datafield: 'Importe', width: '50%',cellsrenderer: crImporte, cellsalign: 'right', cellsformat: 'c2' },
];

var colGridHorarios = [
                        { text: 'D&iacute;a', datafield: 'DiaDeLaSemana', width: '16%',  cellsalign: 'center' },
                        { text: 'Entrada 1', datafield: 'Entrada1', width: '16%', cellsalign: 'center' },
                        { text: 'Salida 1', datafield: 'Salida1', width: '14%', cellsalign: 'center' },
                        { text: 'M&oacute;vil 1', datafield: 'Movil1', width: '12%', cellsalign: 'center' },
                        { text: 'Entrada 2', datafield: 'Entrada2', width: '16%', cellsalign: 'center' },
                        { text: 'Salida 2', datafield: 'Salida2', width: '14%', cellsalign: 'center' },
                        { text: 'M&oacute;vil 2', datafield: 'Movil2', width: '12%', cellsalign: 'center' }
];

// --> Seteo objeto de la grilla de guardias con todos los valores

$("#grdResumen").jqxGrid(
{
    width: '99%',
    autoheight: true,
    source: getSourceGridResumen(),
    pageable: true,
    pagesize: 8,
    altrows: true,
    theme: 'arctic',
    columns: colGridResumen,
    pagesizeoptions: ['8']
});

$("#grdHorarios").jqxGrid(
{
    width: '99%',
    autoheight: true,
    source: getSourceGridHorarios(),
    pageable: true,
    pagesize: 8,
    altrows: true,
    theme: 'arctic',
    columns: colGridHorarios,
    pagesizeoptions: ['8']
});

$('#grdResumen').on('bindingcomplete', function (event) {
    $grid = $(this);
    $grid.jqxGrid('localizestrings', localizationobj);
    $grid.jqxGrid('gotopage', 1);
    $grid.jqxGrid('gotopage', 0);
});

$('#grdHorarios').on('bindingcomplete', function (event) {
    $grid = $(this);
    $grid.jqxGrid('localizestrings', localizationobj);
});

$('#btnConsultarResumen').on('click', function () {

    setSourceResumen();

});

function setSourceResumen() {

    $('#grdResumen').jqxGrid({ source: getSourceGridResumen() });

    $('#grdHorarios').jqxGrid({ source: getSourceGridHorarios() });

}
