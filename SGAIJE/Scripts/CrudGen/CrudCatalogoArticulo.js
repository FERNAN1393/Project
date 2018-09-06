//GRID DE ARTICULOS
$(function () {
    $("#gridArticulos").dxDataGrid({
        dataSource: DsArticulo,
        "export": {
            enabled: true,
            fileName: "DsArticulos",
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
            pageSize: 10
        },
        columnChooser: {
            enabled: true,
            mode: "select"
        },
        columns: ["CodigoArticulo", "Descripcion", "Precio", "Descuento", "Existencia", "CantidadMaxima", "CantidadMinima"],
        selection: {
            mode: "single"
        },

        allowColumnReordering: true,
        onSelectionChanged: function (data) {
            console.log(data.selectedRowsData[0].GenArticuloKey);
            Detalles(data.selectedRowsData[0].GenArticuloKey);

        }
        //
    });
});


function Alta() {

    $.ajax({
        url: '/CatalogoArticulo/Create',
        success: function (data) {
            $('#mdlcrudtitleArticulos').html('Agregar articulo');
            $('#mdlContentCrudArticulos').html(data);
            $('#mdlCrudArticulos').modal('show');

        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Detalles(id) {

    $.ajax({
        url: '/CatalogoArticulo/Details/' + id,
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
        url: '/CatalogoArticulo/Delete/' + id,
        success: function (data) {

            $('#mdlcrudtitleArticulos').html('Eliminar articulo');
            $('#mdlContentCrudArticulos').html(data);
            $('#mdlCrudArticulos').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Editar(id) {

    $.ajax({
        url: '/CatalogoArticulo/Edit/' + id,
        success: function (data) {

            $('#mdlcrudtitleArticulos').html('Editar articulo');
            $('#mdlContentCrudArticulos').html(data);
            $('#mdlCrudArticulos').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}