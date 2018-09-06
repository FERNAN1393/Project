$(function () {
    $("#gridTipoCliente").dxDataGrid({
        dataSource: DsTipoCliente,
        "export": {
            enabled: true,
            fileName: "DsCliente",
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
        columns: ["Descripcion","Descuento"],
        selection: {
            mode: "single"
        },

        allowColumnReordering: true,
        onSelectionChanged: function (data) {
            console.log(data.selectedRowsData[0].GenTipoClienteKey);
            Detalles(data.selectedRowsData[0].GenTipoClienteKey);

        }
        //
    });
});


function Alta() {

    $.ajax({
        url: '/TipoCliente/Create',
        success: function (data) {
            $('#mdlcrudtitle').html('  Agregar Tipo Cliente');
            $('#mdlContentCrudTipoCliente').html(data);
            $('#mdlCrudTipoCliente').modal('show');

        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Detalles(id) {

    $.ajax({
        url: '/TipoCliente/Details/' + id,
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
        url: '/TipoCliente/Delete/' + id,
        success: function (data) {

            $('#mdlcrudtitle').html('Eliminar Tipo Cliente');
            $('#mdlContentCrudTipoCliente').html(data);
            $('#mdlCrudTipoCliente').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Editar(id) {

    $.ajax({
        url: '/TipoCliente/Edit/' + id,
        success: function (data) {

            $('#mdlcrudtitle').html('Editar Tipo Cliente');
            $('#mdlContentCrudTipoCliente').html(data);
            $('#mdlCrudTipoCliente').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}