$('#navbar-top-logout').tooltip({
	title: 'Salir',
	placement: 'right'
});

$('select').selectpicker({width: '180px'});

            var source =
            {
                datatype: "json",
                datafields:
                [
                    { name: 'ID', type: 'number' },
                    { name: 'Tarifa', type: 'string' },
                    { name: 'Movil', type: 'number' },
                    { name: 'HorarioEntrada', type: 'string' },
                    { name: 'HorarioSalida', type: 'string' }
                ],
                url:'Medicos/GetGuardias',
                pagesize: 12
            };

            var dataAdapter = new $.jqx.dataAdapter(source);
            var editrow = -1;
            // initialize jqxGrid
            $("#jqxgrid").jqxGrid(
            {
                width: 1100,
                source: dataAdapter,
                showfilterrow: true,
                filterable: true,
                pageable: true,
                autoheight: true,
                theme: 'arctic',
                columns: [
                  { text: 'ID', datafield: 'ID', width: 200, filtertype: 'textbox' },
                  { text: 'Tarifa', datafield: 'Tarifa', width: 200 },
                  { text: 'Movil', datafield: 'Movil', width: 190 },
                  { text: 'HorarioEntrada', datafield: 'HorarioEntrada', width: 90},
                  { text: 'HorarioSalida', datafield: 'HorarioSalida', width: 100 }                                                                                                                           
                ]
            });

