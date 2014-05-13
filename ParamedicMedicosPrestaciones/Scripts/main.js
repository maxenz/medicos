$('#navbar-top-logout').tooltip({
	title: 'Salir',
	placement: 'right'
});

$('select').selectpicker({width: '180px'});

var data = generatedata(25);
            var source =
            {
                localdata: data,
                datatype: "array",
                datafields:
                [
                    { name: 'firstname', type: 'string' },
                    { name: 'lastname', type: 'string' },
                    { name: 'productname', type: 'string' },
                    { name: 'quantity', type: 'number' },
                    { name: 'price', type: 'number' }
                ],
                pagesize: 12
            };

            var dataAdapter = new $.jqx.dataAdapter(source);
            var editrow = -1;
            // initialize jqxGrid
            $("#jqxgrid").jqxGrid(
            {
                width: 1100,
                source: dataAdapter,
                pageable: true,
                autoheight: true,
                theme: 'arctic',
                columns: [
                  { text: 'First Name', datafield: 'firstname', width: 200 },
                  { text: 'Last Name', datafield: 'lastname', width: 200 },
                  { text: 'Product', datafield: 'productname', width: 190 },
                  { text: 'Quantity', datafield: 'quantity', width: 90, cellsalign: 'right' },
                  { text: 'Price', datafield: 'price', cellsalign: 'right', cellsformat: 'c2' }                                                                                                                           
                ]
            });

