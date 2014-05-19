﻿$("#btnConsultarServicios").jqxButton({ width: '100', theme: 'bootstrap', height: '26' });

// --> Seteo dropdownlist para seleccionar periodo en la grilla de servicios
// --> Uso mismo source que filtro de periodo de guardias

$("#ftrPeriodoServicios").jqxDropDownList({
    selectedIndex: 2, source: dtFtrPeriodoGuardias, displayMember: "Descripcion",
    valueMember: "Periodo", width: '110%', dropDownHeight: 80, height: 25, theme: 'bootstrap'
});

/*********************************************************************************************************/

// --> Seteo dropdownlist para seleccionar periodo,guardias en la grilla de servicios
// --> Uso mismo source que filtro de dia,guardias de guardias
$("#ftrDiaServicios").jqxDropDownList({ selectedIndex: 0, source: srcFtrDiaGuardias, width: '110%', height: 25, theme: 'bootstrap' });
$("#ftrCoordServicios").jqxDropDownList({
    selectedIndex: 0, source: dtFtrCoordGuardias, displayMember: "Descripcion",
    valueMember: "ID", width: '110%', dropDownHeight: 80, height: 25, theme: 'bootstrap'
});

/*********************************************************************************************************/

// -> Funciones para obtener el periodo y el dia de los filtros en la grilla de guardias

function getSelectedPeriodoServ() {
    return $("#ftrPeriodoServicios").jqxDropDownList('getSelectedItem').value;
}

function getSelectedDiaServ() {
    return $('#ftrDiaServicios').jqxDropDownList('selectedIndex');
}

function getSelectedCoordServ() {
    return $('#ftrCoordServicios').jqxDropDownList('getSelectedItem').value;
}

function getDescriptionSelectedPeriodoServ() {
    return $("#ftrPeriodoServicios").jqxDropDownList('getSelectedItem').label;
}

/*********************************************************************************************************/

// --> Seteo datafields de grilla de guardias

var dtFieldsServicios = [
                        {name : 'IncidenteID', type: 'string' },
                        { name: 'Fecha', type: 'string' },
                        { name: 'NroInc', type: 'string' },
                        { name: 'Iva', type: 'string' },
                        { name: 'Paciente', type: 'string' },
                        { name: 'Localidad', type: 'string' },
                        { name: 'Cdn', type: 'string' },
                        { name: 'Tarifa', type: 'string' },
                        { name: 'Dia', type: 'string' },
                        { name: 'Tur', type: 'string' },
                        { name: 'Grado', type: 'string' },
                        { name: 'CoPago', type: 'number' },
                        { name: 'Importe', type: 'number' },
                       ];

// --> Seteo source grilla de guardias

function getSourceGridServicios() {

    var srcGridServicios = {
        datatype: "json",
        datafields: dtFieldsServicios,
        url: 'Medicos/GetServicios',
        data: {
            periodo: getSelectedPeriodoServ(),
            dia: getSelectedDiaServ(),
            coordinacion: getSelectedCoordServ()
        }
    };

    var dtGridServicios = new $.jqx.dataAdapter(srcGridServicios);

    return dtGridServicios;
}

var dtGridServicios = getSourceGridServicios();

// --> Seteo cellsrenderers de grilla de guardias, para poner en rojo valores dependiendo si llegaron tarde, etc.

var crConfirmacionServicio = function (row, columnfield, value, defaulthtml, columnproperties) {
    return '<a href="javascript:showPopupGuardia()" style="margin-left:34%;line-height:25px"><i class="fa fa-check-circle verde icon-right-margin big-icon"></i></a>';
}


// --> Seteo columnas de la grilla de guardias

var colGridServicios =
             [
              { text: 'ID', datafield: 'IncidenteID', hidden: true },
              { text: 'Fecha', datafield: 'Fecha', width: '7%' },
              { text: 'Inc', datafield: 'NroInc', width: '7%', cellsalign: 'center' },
              { text: 'Iva', datafield: 'Iva', width: '7%', cellsalign: 'center' },
              { text: 'Paciente', datafield: 'Paciente', width: '12%', cellsalign: 'center' },
              { text: 'Loc', datafield: 'Localidad', width: '7%', cellsalign: 'center' },
              { text: 'Cdn', datafield: 'Cdn', width: '7%', cellsalign: 'center' },
              { text: 'Tarifa', datafield: 'Tarifa', width: '7%', cellsalign: 'center' },
              { text: 'D&iacute;a', datafield: 'Dia', width: '7%', cellsalign: 'center' },
              { text: 'Tur', datafield: 'Tur', width: '7%', cellsalign: 'center' },
              { text: 'Grado', datafield: 'Grado', width: '7%', cellsalign: 'center' },
              { text: 'CoPago', datafield: 'CoPago', width: '9%', cellsalign: 'right', cellsformat: 'c2' },
              { text: 'Importe', datafield: 'Importe', width: '9%', cellsalign: 'right', cellsformat: 'c2' },
              { text: 'Conf', width: '7%', cellsalign: 'center', cellsRenderer: crConfirmacionServicio }
             ];


// --> Seteo objeto de la grilla de guardias con todos los valores

$("#grdServicios").jqxGrid(
{
    width: '99%',
    autoheight: true,
    source: dtGridServicios,
    pageable: true,
    pagesize: 12,
    altrows: true,
    theme: 'arctic',
    columns: colGridServicios,
    pagesizeoptions: [12]
});

/*********************************************************************************************************/

// --> Refrescar grilla cuando filtro informacion

$('#btnConsultarServicios').on('click', function () {

    var dtGridServicios = getSourceGridServicios();

    $('#grdServicios').jqxGrid({ source: dtGridServicios });

});

function getHorasMinutosGuardia(hsTrabajadas, idx) {

    var horas = hsTrabajadas.split(":");
    return parseInt(horas[idx]);

}

// --> Seteo idioma en la grilla y totalizadores

$('#grdServicios').on('bindingcomplete', function (event) {
    $grid = $(this);
    $grid.jqxGrid('localizestrings', localizationobj);
});