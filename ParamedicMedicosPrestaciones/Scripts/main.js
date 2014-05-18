// --> Seteo tooltip de salir en boton salir, arriba a la derecha en el header
$('#navbar-top-logout').tooltip({
    title: 'Salir',
    placement: 'right'
});

function showPopupGuardia() {
    BootstrapDialog.show({
        title: 'Confirmaci&oacute;n con la guardia del 07/04/2014 en m&oacute;vil 15',
        message: function (dialog) {
            $content = '<form type="POST" action="Medicos/SetReclamo"> ' +
                            '<div class="row row-show-grid">' +
                            '<div class="col-md-6">' +
                                '<div class="input-group"> ' +
                                    '<span class="input-group-addon">' +
                                    '<i class="fa fa-check-circle big-icon icon-right-margin verde"></i>' +
                                        '<input type="radio" name="holis" value="1" checked>' +
                                     '</span>' +
                                    '<input type="text" readonly class="form-control" value="ESTOY CONFORME" />' +
                                '</div>' +
                             '</div>' +

                            '<div class="col-md-6">' +
                                '<div class="input-group"> ' +
                                    '<span class="input-group-addon">' +
                                    '<i class="fa fa-times-circle big-icon icon-right-margin rojo"></i>' +
                                        '<input type="radio" name="holis" value="2">' +
                                     '</span>' +
                                    '<input type="text" readonly class="form-control" value="NO ESTOY CONFORME" />' +
                                '</div>' +
                             '</div>' +
                             '</div>' +
                             '<div id="contNoConforme">' +

                                '<div class="row">' +
                                    '<div class="form-group col-md-4">' +
                                    '<label for="dwMotivosNoConforme">Seleccione Motivo:</label>' +
                                        '<div id="dwMotivosNoConforme"></div>' +
                                     '</div>' +

                                    '<div class="form-group col-md-4">' +
                                    '<label for="dwMotivosNoConforme">Su entrada (hh:mm)</label>' +
                                        '<div id="dwMotivosNoConforme"></div>' +
                                     '</div>' +

                                    '<div class="form-group col-md-4">' +

                                    '<label for="txtSalida">Su salida (hh:mm)</label>' +

                                        ' <input type="text" id="txtSalida" />' +


                                     '</div>' +

                                 '</div>' +
                             '</div>' +

                        '</form>';


            return $content;
        },
        onshown: function (dialog) {

            $("#txtSalida").mask("99:99", { placeholder: " " });

            var srcFtrDiaGuardias = ["Diferencia en horas valor"];

            for (var k = 1; k <= 31; k++) {
                srcFtrDiaGuardias.push(k);
            }

            $("#dwMotivosNoConforme").jqxDropDownList({ selectedIndex: 0, source: srcFtrDiaGuardias, width: '110%', height: 25, theme: 'bootstrap' });
        }
    });
}

//$('input[name="holis"]').on('click', function (event) {
//    console.log($(this).val());
//    if ($(this).val() == 2) {
//        $('#contNoConforme').show('slow');
//    } else {
//        $('#contNoConforme').hide('slow');
//    }
//});

// --> Seteo localization obj para traducir grilla de ingles

var localizationobj = {};
localizationobj.pagergotopagestring = "Ir a p&aacute;gina:";
localizationobj.pagershowrowsstring = "Mostrar Filas:";
localizationobj.pagerrangestring = " de ";
localizationobj.currencysymbol = "$";

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    $('#grdServicios').jqxGrid('autoresizecolumns');
    //e.target // activated tab
    //e.relatedTarget // previous tab
})














