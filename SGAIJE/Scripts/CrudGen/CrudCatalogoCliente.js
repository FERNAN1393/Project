  
$(function () {
    $("#gridCliente").dxDataGrid({
        dataSource: DsCliente,
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
        columns: ["Nombre", {
            dataField: "ApellidoP",
            caption: "Apellido Paterno"
        }, {
                dataField: "ApellidoM",
                caption: "Apellido Materno"
            },
            "RFC", "Telefono", "Correo"],
        selection: {
            mode: "single"
        },

        allowColumnReordering: true,
        onSelectionChanged: function (data) {
            console.log(data.selectedRowsData[0].GenClienteKey);
            Detalles(data.selectedRowsData[0].GenClienteKey);

        }
        //
    });
});

//PRUEBA


function Alta() {

    $.ajax({
        url: '/Cliente/Create',
        success: function (data) {
            $('#mdlcrudtitle').html('Agregar Cliente');
            $('#mdlContentCrudCliente').html(data);
            $('#mdlCrudCliente').modal('show');

        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Detalles(id) {

    $.ajax({
        url: '/Cliente/Details/' + id,
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
        url: '/Cliente/Delete/' + id,
        success: function (data) {

            $('#mdlcrudtitle').html('Eliminar Cliente');
            $('#mdlContentCrudCliente').html(data);
            $('#mdlCrudCliente').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Editar(id) {

    $.ajax({
        url: '/Cliente/Edit/' + id,
        success: function (data) {

            $('#mdlcrudtitle').html('Editar Cliente');
            $('#mdlContentCrudCliente').html(data);
            $('#mdlCrudCliente').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}