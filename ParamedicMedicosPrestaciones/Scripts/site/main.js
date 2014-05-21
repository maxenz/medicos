// --> Seteo tooltip de salir en boton salir, arriba a la derecha en el header
$('#navbar-top-logout').tooltip({
    title: 'Salir',
    placement: 'right'
});

$('#popupGrdContNoConforme').hide();

$("#popupGrdEntrada").mask("99:99", { placeholder: " " });
$("#popupGrdSalida").mask("99:99", { placeholder: " " });

var srcFtrDiaGuardias = ["Diferencia en horas valor"];

for (var k = 1; k <= 31; k++) {
    srcFtrDiaGuardias.push(k);
}

$("#dwMotivosNoConforme").jqxDropDownList({ selectedIndex: 0, source: srcFtrDiaGuardias, width: '110%', height: 32, theme: 'bootstrap' });

function showPopupGuardia() {

    $('#popupRevisarGuardias').modal('show');

}

$('input[name="holis"]').on('click', function () {

    $radio = $(this);
    if ($radio.val() == 2) {
        $('#popupGrdContNoConforme').show('slow');
    } else {
        $('#popupGrdContNoConforme').hide('slow');
    }

});

// --> Seteo localization obj para traducir grillas de ingles

var localizationobj = {};
localizationobj.pagergotopagestring = "Ir a p&aacute;gina:";
localizationobj.pagershowrowsstring = "Mostrar Filas:";
localizationobj.pagerrangestring = " de ";
localizationobj.currencysymbol = "$";

















