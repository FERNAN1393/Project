    var articulo = '';
    var aux = 0;
    var rest = 0;
    var Precio
    function Grid(DataSource, montototal) {
        if (rest > 0) {
            console.log("rest");
            $('#txtImporteTotal').val(rest)
            aux = rest
                console.log('aux' + (aux = rest))
            console.log('rest' + rest)

        }

        else {
            console.log("AUX")
            console.log(aux);
            console.log(montototal)
            aux = aux + montototal
            console.log(DataSource);
            console.log("MontoTotal");
            $('#txtImporteTotal').val(aux)
        }
       

        $("#dgvRefacciones").dxDataGrid({
        dataSource: DataSource,

            filterRow: {
        visible: true,
                applyFilter: "auto"
            },
            headerFilter: {
        visible: true
            },
            grouping: {
        autoExpandAll: true,
            },
            groupPanel: {
        visible: true
            },
            stateStoring: {
        enabled: true,
                type: "localStorage",
                storageKey: "storage"
            },
            grouping: {
        contextMenuEnabled: true,
                expandMode: "rowClick"
            },
            groupPanel: {
        visible: true
            },
            pager: {
        allowedPageSizes: [5, 8, 15, 30],
                showInfo: true,
                showNavigationButtons: true,
                showPageSizeSelector: true,
                visible: true
            },
            paging: {
        pageSize: 5
            },
            columns: ["Codigo", "Descripcion", "Cantidad", "Descuento",
                {
        dataField: "Total",
                    caption: "Precio",
                },
                "MontoTotal"],
            selection: {
        mode: "single"
            },
            allowColumnReordering: true,
            onSelectionChanged: function (data) {
        console.log(data)

        RowId = data.selectedRowsData[0].GenArticuloLink
                console.log("ddd")
                Precio = data.selectedRowsData[0].MontoTotal
                console.log(Precio)
                console.log(Producto.GenArticuloLink)


            }

        });
        rest = 0;
        

    }

    function obtenerIva(data) {
        if ($(data).is(":checked")) {
        aux = aux * 1.16
            console.log(aux)
            $('#txtImporteTotal').val(aux)
        }
        else {
        aux = aux / 1.16
            $('#txtImporteTotal').val(aux)

        }
    }

    function AltaArticulo() {
        $.ajax({
            url: '/Cotizacion/CreateAsync',
            success: function (data) {

                $('#mdlcrudtitleDetArticulo').html('Agregar Artículo');
                $('#mdlContentAltaDetalleArticulo').html(data);
                $('#mdlCrudAltaDetalleArticulo').modal('show');

            },
            error: function (error) {
                console.log(error);
            }
        });

    };

    function BajaArticulo() {

        $.ajax({
            url: '/Cotizacion/DeleteAsync/' + RowId,
            type: "POST",
            success: function (data) {
                console.log("Monto");
                rest = aux - Precio
                console.log(rest);
                Grid(data);
                console.log("cs");
                console.log(data)
                
            },
            error: function (error) {
                console.log(error);
            }
        });

    };




