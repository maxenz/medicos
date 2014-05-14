$('#navbar-top-logout').tooltip({
    title: 'Salir',
    placement: 'right'
});

$('select').selectpicker({ width: '180px' });

/*********************************************************************/

var source =
{
    datatype: "json",
    datafields:
    [
        { name: 'ID', type: 'number' },
        { name: 'Periodo', type: 'string' },
        { name: 'Dia', type: 'string' },
        { name: 'Tarifa', type: 'string' },
        { name: 'HorarioEntrada', type: 'string' },
        { name: 'MinutosLlegadaTarde', type: 'number' },
        { name: 'HorarioSalida', type: 'string' },
        { name: 'MinutosRetiroAnticipado', type: 'number' },
        { name: 'Movil', type: 'string' },
        { name: 'HorasTrabajadas', type: 'string' },
        { name: 'Rojos', type: 'number' },
        { name: 'Amarillos', type: 'number' },
        { name: 'Verdes', type: 'number' },
        { name: 'TrasladosProgramados', type: 'number' },
        { name: 'ImpTotalHoras', type: 'number' },
        { name: 'ImpEspecialidad', type: 'number' },
        { name: 'ImpPrestacion', type: 'number' },
        { name: 'ImpPrestacionExcedente', type: 'number' },
        { name: 'ImpAnticipo', type: 'number' },
        { name: 'ImpFinal', type: 'number' }

    ],
    url: 'Medicos/GetGuardias',
    async: false
};

var dataAdapter = new $.jqx.dataAdapter(source);
var editrow = -1;
var crHorEntrada = function (row, columnfield, value, defaulthtml, columnproperties) {
    var minLlegadaTarde = $("#jqxgrid").jqxGrid('getrowdata', row).MinutosLlegadaTarde;
    if (minLlegadaTarde > 0) {
        return '<span style="margin-left:14px;line-height:25px;color: red;font-weight: bold">' + value + '</span>';
    }
}

var crHorSalida = function (row, columnfield, value, defaulthtml, columnproperties) {
    var minRetiroAnticipado = $("#jqxgrid").jqxGrid('getrowdata', row).MinutosRetiroAnticipado;
    if (minRetiroAnticipado > 0) {
        return '<span style="margin-left:14px;line-height:25px;color: red;font-weight: bold">' + value + '</span>';
    }
}

var crConfirmacion = function (row, columnfield, value, defaulthtml, columnproperties) {
    return '<a href="#" style="margin-left:12px;line-height:25px"><i class="fa fa-check-circle verde icon-right-margin big-icon"></i></a>';
}

var columns =
             [
              { text: 'ID', datafield: 'ID', hidden: true },
              { text: 'Per&iacute;odo', datafield: 'Periodo', width: 75, cellsalign: 'center' },
              { text: 'D&iacute;a', datafield: 'Dia', width: 75,  cellsalign: 'center', filtertype: 'list' },
              { text: 'Tarifa', datafield: 'Tarifa', width: 60, cellsalign: 'center' },
              { text: 'H. Ent', datafield: 'HorarioEntrada', width: 60,  cellsalign: 'center', cellsrenderer: crHorEntrada },
              { text: 'H. Sal', datafield: 'HorarioSalida', width: 60, cellsalign: 'center', cellsRenderer: crHorSalida },
              { text: 'Movil', datafield: 'Movil', width: 60, cellsalign: 'center' },
              { text: 'T. Hs', datafield: 'HorasTrabajadas', width: 60,  cellsalign: 'center' },
              { text: 'Ro.', datafield: 'Rojos', width: 40,  cellsalign: 'center' },
              { text: 'Am.', datafield: 'Amarillos', width: 40,  cellsalign: 'center' },
              { text: 'Ve.', datafield: 'Verdes', width: 40,  cellsalign: 'center' },
              { text: 'Tp.', datafield: 'TrasladosProgramados', width: 40,  cellsalign: 'center' },
              { text: '$ Hs', datafield: 'ImpTotalHoras', width: 75, cellsalign: 'right', cellsformat: 'c2' },
              { text: '$ Esp', datafield: 'ImpEspecialidad', width: 75, cellsalign: 'right', cellsformat: 'c2' },
              { text: '$ Incs', datafield: 'ImpPrestacion', width: 75, cellsalign: 'right', cellsformat: 'c2' },
              { text: '$ Exc', datafield: 'ImpPrestacionExcedente', width: 75,  cellsalign: 'right', cellsformat: 'c2' },
              { text: '$ Antic', datafield: 'ImpAnticipo', width: 75,  cellsalign: 'right', cellsformat: 'c2' },
              { text: '$ Total', datafield: 'ImpFinal', width: 75, cellsalign: 'right', cellsformat: 'c2' },
              { text: 'Conf', width: 40, cellsalign: 'center', cellsRenderer: crConfirmacion }
             ];

$("#jqxgrid").jqxGrid(
{
    width: 1100,
    source: dataAdapter,
    pageable: true,
    autoheight: true,
    altrows: true,
    theme: 'arctic',
    columns: columns
});

$("#jqxgrid").jqxGrid('beginupdate');
for (var index = 0; index < columns.length; index++) {
    $("#jqxgrid").jqxGrid('setcolumnproperty', columns[index].datafield, 'align', 'center');
    $("#jqxgrid").jqxGrid('setcolumnproperty', columns[index].datafield, 'cellsalign', 'center');
}
$("#jqxgrid").jqxGrid('endupdate');

var localizationobj = {};
localizationobj.pagergotopagestring = "Ir a p&aacute;gina:";
localizationobj.pagershowrowsstring = "Mostrar Filas:";
localizationobj.pagerrangestring = " de ";
localizationobj.currencysymbol = "$";

$("#jqxgrid").jqxGrid('localizestrings', localizationobj);

