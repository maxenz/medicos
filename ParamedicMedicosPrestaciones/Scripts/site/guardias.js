$("#btnConsultarGuardias").jqxButton({ width: '100', theme: 'bootstrap', height: '26' });

// --> Seteo dropdownlist para seleccionar periodo en la grilla de guardias

var srcFtrPeriodoGuardias = {
    datatype: "json",
    datafields: [
        { name: 'Periodo' },
        { name: 'Descripcion' }
    ],
    url: 'Medicos/getFiltroPeriodos',
    async: false
};

var dtFtrPeriodoGuardias = new $.jqx.dataAdapter(srcFtrPeriodoGuardias);

$("#ftrPeriodoGuardias").jqxDropDownList({
    selectedIndex: 2, source: dtFtrPeriodoGuardias, displayMember: "Descripcion",
    valueMember: "Periodo", width: '110%', dropDownHeight: 80, height: 25, theme: 'bootstrap'
});

/*************************************************************************************************/

// --> Seteo dropdownlist para seleccionar medico en la grilla de guardias

var urlMedicoGuardias = 'Medicos/getFiltroMedicos';

if (tipoAcceso == 1) {
    urlMedicoGuardias += '?usr_id=' + usr_id_medico;
    $('#colMedicoGuardias').hide();
    $('#colMedicoServicios').hide();
    $('#colMedicoResumen').hide();
}

var srcFtrMedicoGuardias = {
    datatype: "json",
    datafields: [
        { name: 'UsuarioID' },
        { name: 'Nombre' }
    ],
    url: urlMedicoGuardias,
    async: false
};

var dtFtrMedicoGuardias = new $.jqx.dataAdapter(srcFtrMedicoGuardias);

$("#ftrMedicoGuardias").jqxDropDownList({
    source: dtFtrMedicoGuardias, displayMember: "Nombre", selectedIndex: 0,
    valueMember: "UsuarioID", width: '110%', dropDownHeight: 150, dropDownWidth: 320, height: 25, theme: 'bootstrap'
});

// --> Cuando selecciono medico, actualizo el dropdownlist de las coordinaciones

$('#ftrMedicoGuardias').on('select', function (event) {

    var dtFtrCoordGuardias = setSrcFtrCoordGuardias();

    $('#ftrCoordGuardias').jqxDropDownList({ source: dtFtrCoordGuardias });

});

/*********************************************************************************************************/

// --> Seteo dropdownlist para seleccionar coordinacion en la grilla de guardias

function setSrcFtrCoordGuardias() {

    var srcFtrCoordGuardias = {
        datatype: "json",
        datafields: [
            { name: 'ID' },
            { name: 'Descripcion' }
        ],
        url: 'Medicos/getFiltroCoordinaciones?usr_id=' + getSelectedMedico(),
        async: false
    };

    var dtFtrCoordGuardias = new $.jqx.dataAdapter(srcFtrCoordGuardias);

    return dtFtrCoordGuardias;

}

var dtFtrCoordGuardias = setSrcFtrCoordGuardias();

$("#ftrCoordGuardias").jqxDropDownList({
    selectedIndex: 0, source: dtFtrCoordGuardias, displayMember: "Descripcion",
    valueMember: "ID", width: '110%', dropDownHeight: 80, height: 25, theme: 'bootstrap'
});

/*********************************************************************************************************/

// --> Seteo dropdownlist para seleccionar periodo en la grilla de guardias

var srcFtrDiaGuardias = ["Seleccione D&iacute;a"];

for (var k = 1; k <= 31; k++) {
    srcFtrDiaGuardias.push(k);
}

$("#ftrDiaGuardias").jqxDropDownList({ selectedIndex: 0, source: srcFtrDiaGuardias, width: '110%', height: 25, theme: 'bootstrap' });

/*********************************************************************************************************/

// -> Funciones para obtener el periodo y el dia de los filtros en la grilla de guardias

function getSelectedPeriodo() {
    return $("#ftrPeriodoGuardias").jqxDropDownList('getSelectedItem').value;
}

function getSelectedDia() {
    return $('#ftrDiaGuardias').jqxDropDownList('selectedIndex');
}

function getSelectedCoord() {
    return $('#ftrCoordGuardias').jqxDropDownList('getSelectedItem').value;
}

function getSelectedMedico() {
    return $("#ftrMedicoGuardias").jqxDropDownList('getSelectedItem').value;
}

function getDescriptionSelectedPeriodo() {
    return $("#ftrPeriodoGuardias").jqxDropDownList('getSelectedItem').label;
}

function getDescriptionSelectedMedico() {
    return $("#ftrMedicoGuardias").jqxDropDownList('getSelectedItem').label;
}

/*********************************************************************************************************/

// --> Seteo datafields de grilla de guardias

var dtFieldsGuardias = [{ name: 'ID', type: 'string' },
                        { name: 'FecMovimiento', type: 'string' },
                        { name: 'DiaDeLaSemana', type: 'string' },
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
                        { name: 'ImpFinal', type: 'number' },
                        { name: 'Estado', type: 'number' }];

// --> Seteo source grilla de guardias

function getSourceGridGuardias() {

    var srcGridGuardias = {
        datatype: "json",
        datafields: dtFieldsGuardias,
        url: 'Medicos/GetGuardias',
        data: {
            periodo: getSelectedPeriodo(),
            dia: getSelectedDia(),
            coordinacion: getSelectedCoord(),
            medico: getSelectedMedico()
        }
    };

    var dtGridGuardias = new $.jqx.dataAdapter(srcGridGuardias);

    return dtGridGuardias;
}

var dtGridGuardias = getSourceGridGuardias();

// --> Seteo cellsrenderers de grilla de guardias, para poner en rojo valores dependiendo si llegaron tarde, etc.

var crHorEntradaGuardia = function (row, columnfield, value, defaulthtml, columnproperties) {
    var minLlegadaTarde = $("#grdGuardias").jqxGrid('getrowdata', row).MinutosLlegadaTarde;
    if (minLlegadaTarde > 0) {
        return '<div style="text-align:center"><span style="line-height:25px;color: red;font-weight: bold">' + value + '</span></div>';
    }
}

var crHorSalidaGuardia = function (row, columnfield, value, defaulthtml, columnproperties) {
    var minRetiroAnticipado = $("#grdGuardias").jqxGrid('getrowdata', row).MinutosRetiroAnticipado;
    if (minRetiroAnticipado > 0) {
        return '<div style="text-align:center"><span style="line-height:25px;color: red;font-weight: bold">' + value + '</span></div>';
    }
}

var crConfirmacionGuardia = function (row, columnfield, value, defaulthtml, columnproperties) {
    var retVal = "";
    switch (value) {
        case 0:
            // --> No esta conforme
            retVal = '<a href="javascript:showPopupGuardia()" style="margin-left:34%;line-height:33px"><span class="glyphicon glyphicon-remove-circle rojo icon-right-margin big-icon "></span></a>';
            break;
        case 1:
            // --> Está conforme
            retVal = '<a href="javascript:showPopupGuardia()" style="margin-left:34%;line-height:33px"><span class="glyphicon glyphicon-ok-circle verde icon-right-margin big-icon "></span></a>';
            break;
        case 2:
            // --> No está conforme y recibió una respuesta
            retVal = '<a href="javascript:showPopupGuardia()" style="margin-left:34%;line-height:33px"><span class="glyphicon glyphicon-exclamation-sign amarillo icon-right-margin big-icon "></span></a>';
            break;
        case 3:
            // --> Reclamo aceptado
            retVal = '<a href="javascript:showPopupGuardia()" style="margin-left:34%;line-height:33px"><span class="glyphicon glyphicon-ok-circle azul icon-right-margin big-icon "></span></a>';
            break;
        case 4:
            // --> Reclamo no aceptado
            retVal = '<a href="javascript:showPopupGuardia()" style="margin-left:34%;line-height:33px"><span class="glyphicon glyphicon-remove-circle naranja icon-right-margin big-icon "></span></a>';
            break;
    }

    return retVal;

}


// --> Seteo columnas de la grilla de guardias

var colGridGuardias =
             [
              { text: 'ID', datafield: 'ID', hidden: true },
              { text: 'FecMovimiento', datafield: 'FecMovimiento', hidden: true },
              { text: 'D&iacute;a', datafield: 'DiaDeLaSemana', width: '5%', cellsalign: 'center' },
              { text: 'Tar', datafield: 'Tarifa', width: '4%', cellsalign: 'center' },
              { text: 'H.Ent', datafield: 'HorarioEntrada', width: '5%', cellsalign: 'center', cellsrenderer: crHorEntradaGuardia },
              { text: 'H.Sal', datafield: 'HorarioSalida', width: '5%', cellsalign: 'center', cellsRenderer: crHorSalidaGuardia },
              { text: 'Mov', datafield: 'Movil', width: '4%', cellsalign: 'center' },
              { text: 'T.Hs', datafield: 'HorasTrabajadas', width: '5%', cellsalign: 'center' },
              { text: 'Ro', datafield: 'Rojos', width: '5%', cellsalign: 'center' },
              { text: 'Am', datafield: 'Amarillos', width: '5%', cellsalign: 'center' },
              { text: 'Ve', datafield: 'Verdes', width: '5%', cellsalign: 'center' },
              { text: 'Tp', datafield: 'TrasladosProgramados', width: '5%', cellsalign: 'center' },
              { text: '$Hs', datafield: 'ImpTotalHoras', width: '8%', cellsalign: 'right', cellsformat: 'c2' },
              { text: '$Esp', datafield: 'ImpEspecialidad', width: '8%', cellsalign: 'right', cellsformat: 'c2' },
              { text: '$Inc', datafield: 'ImpPrestacion', width: '8%', cellsalign: 'right', cellsformat: 'c2' },
              { text: '$Exc', datafield: 'ImpPrestacionExcedente', width: '8%', cellsalign: 'right', cellsformat: 'c2' },
              { text: '$Ant', datafield: 'ImpAnticipo', width: '8%', cellsalign: 'right', cellsformat: 'c2' },
              { text: '$Tot', datafield: 'ImpFinal', width: '8%', cellsalign: 'right', cellsformat: 'c2' },
              { text: 'Est', datafield: 'Estado', width: '4%', cellsalign: 'center', cellsRenderer: crConfirmacionGuardia }
             ];


// --> Seteo objeto de la grilla de guardias con todos los valores

$("#grdGuardias").jqxGrid(
{
    width: '99%',
    autoheight: true,
    source: dtGridGuardias,
    pageable: true,
    altrows: true,
    pagesize: 12,
    theme: 'arctic',
    columns: colGridGuardias,
    pagesizeoptions: [12]
});

/*********************************************************************************************************/

// --> Seteo idioma en la grilla y totalizadores

$('#grdGuardias').on('bindingcomplete', function (event) {

    var $grid = $(this);

    var $rows = $grid.jqxGrid('getrows');
    var cantGuardias = $rows.length;
    var acumServicios = 0;
    var acumImporteTotal = 0;
    var acumHoras = 0;
    var acumMinutos = 0;

    for (var i = 0; i < $rows.length; i++) {
        var row = $rows[i];
        acumServicios += row.Rojos + row.Amarillos + row.Verdes + row.TrasladosProgramados;
        acumImporteTotal += row.ImpFinal;
        acumHoras += getHorasMinutosGuardia(row.HorasTrabajadas, 0);
        acumMinutos += getHorasMinutosGuardia(row.HorasTrabajadas, 1);
    }

    if (acumMinutos > 60) {
        acumHoras = acumHoras + parseInt((acumMinutos / 60));
        acumMinutos = acumMinutos % 60;
    }

    var totalHorasTrabajadas = acumHoras + " hs. " + acumMinutos + " min.";

    $('#cantServicios').text(acumServicios);
    $('#cantGuardias').text(cantGuardias);
    $('#impLiquidado').text(parseFloat(acumImporteTotal).toFixed(2));
    $('#totalHoras').text(totalHorasTrabajadas);
    $('#titPeriodo').text(getDescriptionSelectedPeriodo());
    $('#titMedico').text(getDescriptionSelectedMedico());

    $grid.jqxGrid('localizestrings', localizationobj);
});

function getHorasMinutosGuardia(hsTrabajadas, idx) {

    var horas = hsTrabajadas.split(":");
    return parseInt(horas[idx]);

}

/*********************************************************************************************************/

// --> Refrescar grilla cuando filtro informacion

$('#btnConsultarGuardias').on('click', function () {

    var dtGridGuardias = getSourceGridGuardias();

    $('#grdGuardias').jqxGrid({ source: dtGridGuardias });

});


/*********************************************************************************************************/

// --> Popup para reclamo de guardias


// Set datasource para el filtro de motivo de reclamo, el dropDownList en el popup de reclamo de guardia
var srcFtrMotivoReclamoGuardias = {
    datatype: "json",
    datafields: [
        { name: 'ID' },
        { name: 'Descripcion' }
    ],
    url: 'Medicos/getFiltroMotivoReclamo',
    data: { flgTipoReclamo: 2 },
    async: false
};

var dtFtrMotivoReclamoGuardias = new $.jqx.dataAdapter(srcFtrMotivoReclamoGuardias);

$("#ftrMotivoReclamoGuardias").jqxDropDownList({
    source: dtFtrMotivoReclamoGuardias, displayMember: "Descripcion", placeHolder: "Por favor seleccione:",
    valueMember: "ID", width: 266, dropDownHeight: 150, dropDownWidth: 265, height: 32, theme: 'bootstrap'
});

//Cuando selecciono un motivo de reclamo de guardia, veo si es relacionado a un horario. Solo si es así, le saco el disabled
// a los campos horario entrada y horario salida.
$('#ftrMotivoReclamoGuardias').on('select', function (event) {

    var args = event.args;
    var item = args.item;
    var vReclamo = item.value.split("/");
    var idReclamo = parseInt(vReclamo[0]);
    var difIngreso = parseInt(vReclamo[1]);
    if (difIngreso == 0) {
        $('input.popupGrdHorario').each(function () {
            $(this).prop('readonly', true);
        });
    } else {
        $('input.popupGrdHorario').each(function () {
            $(this).prop('readonly', false);
        });
    }

});

// Muestro popup de reclamo de guardia
function showPopupGuardia() {

    $('#popupRevisarGuardias').modal('show');

}

// Cuando abro el popup de reclamo de guardias ..
$('#popupRevisarGuardias').on('show.bs.modal', function (event) {

    // Limpio todos los inputs, y saco de la grilla los datos necesarios para ir a buscar al servidor y setear datos en el popup

    limpiarInputsPopupGuardia();
    var rowindex = $('#grdGuardias').jqxGrid('getselectedrowindex');
    var row = $('#grdGuardias').jqxGrid('getrowdata', rowindex);
    var fechaGuardia = row.FecMovimiento;
    var movil = row.Movil;
    var estado = row.Estado;
    console.log(row.ID);

    $('#titlePopupGuardias').text('Confirmación de la guardia del ' + fechaGuardia + ' en el móvil ' + movil);

    //Si hay conformidad, entonces escondo los demas campos que no tiene sentido mostrar.

    if (estado == 1) {
        $('input:radio[name=rdConformidad]')[0].checked = true;
        setConformidad();
    } else {

        //Si no hay conformidad, muestro los campos a llenar
        //Acá tambien tengo que setear todo lo que traigo de la base de datos, si es que fue cargado este reclamo
        $('input:radio[name=rdConformidad]')[1].checked = true;
        setNoConformidad();
    }

    //Si en el sistema se encuentra un medico...
    if (tipoAcceso == 1) {

        //Si se encuentra un administrador con readonly
    } else if (tipoAcceso == 2) {

        disableItemsForAdmins(true);
        $('#btnGuardarReclamoGuardia').prop('disabled', true);

        //Si se encuentra un administrador que puede leer y escribir..
    } else {

        disableItemsForAdmins(false);
    }

});


//Disableo todos los campos para los admins. Solo los medicos puede interactuar con la mayoria de los componentes del popup
//de reclamo de guardias.
function disableItemsForAdmins(vRta) {
    $('input:radio[name="rdConformidad"]').each(function () {
        $(this).prop('disabled', true);
    });
    $('input.popupGrdHorario').each(function () {
        $(this).val('');
    });
    $('#ftrMotivoReclamoGuardias').jqxDropDownList({ disabled: true });
    $('#popupGrdObservaciones').prop('readonly', true);
    $('#popupGrdRespuesta').prop('readonly', vRta);
}

//Limpio inputs del popup de reclamo de guardias
function limpiarInputsPopupGuardia() {
    $('input.popupGrdHorario').each(function () {
        $(this).val('');
    });
    $('#popupGrdObservaciones').text('');
    $('#popupGrdRespuesta').text('');
    $('#ftrMotivoReclamoGuardias').jqxDropDownList('clearSelection');

}

// Evento que muestra o esconde los datos del popup segun si el medico esta conforme o no...
$('input:radio[name="rdConformidad"]').on('change', function () {
    $radio = $(this);
    if ($radio.val() == 2) {
        setNoConformidad();
    } else {
        setConformidad();
    }
});

// Escondo datos, ya que el medico esta conforme y no va a reclamar.
function setConformidad() {
    $('#popupGrdContNoConforme').hide('slow');
}

// Muestro datos, para que el medico pueda reclamar.
function setNoConformidad() {
    $('#popupGrdContNoConforme').show('slow');
}
