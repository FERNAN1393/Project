$(function () {
    $("#gridTipoArticulo").dxDataGrid({
        dataSource: DsTipoArticulo,
        "export": {
            enabled: true,
            fileName: "DsTipoArticulo",
            allowExportSelectedData: true
        },
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
      
        pager: {
            allowedPageSizes: [5, 8, 15, 30],
            showInfo: true,
            showNavigationButtons: true,
            showPageSizeSelector: true,
            visible: true
        },
        paging: {
            pageSize: 10
        },
        columnChooser: {
            enabled: true,
            mode: "select"
        },
        columns: ["Descripcion"],
        selection: {
            mode: "single"
        },

        allowColumnReordering: true,
        onSelectionChanged: function (data) {
            console.log(data.selectedRowsData[0].GenTipoArticuloKey);
            Detalles(data.selectedRowsData[0].GenTipoArticuloKey);

        }
        //
    });
});


function Alta() {

    $.ajax({
        url: '/TipoArticulo/Create',
        success: function (data) {
            $('#mdlcrudtitle').html('  Agregar Tipo Articulo');
            $('#mdlContentCrudTipoArticulo').html(data);
            $('#mdlCrudTipoArticulo').modal('show');

        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Detalles(id) {

    $.ajax({
        url: '/TipoArticulo/Details/' + id,
        success: function (data) {

            $('#Current').html('');
            $('#Current').html(data);
        },
        error: function (error) {
            console.log(error);
        }
    });

}
function Eliminar(id) {

    $.ajax({
        url: '/TipoArticulo/Delete/' + id,
        success: function (data) {

            $('#mdlcrudtitle').html('Eliminar Tipo Articulo');
            $('#mdlContentCrudTipoArticulo').html(data);
            $('#mdlCrudTipoArticulo').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Editar(id) {

    $.ajax({
        url: '/TipoArticulo/Edit/' + id,
        success: function (data) {

            $('#mdlcrudtitle').html('Editar Tipo Articulo');
            $('#mdlContentCrudTipoArticulo').html(data);
            $('#mdlCrudTipoArticulo').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}