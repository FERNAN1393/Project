

function Alta() {

    $.ajax({
        url: '/Cotizacion/Create',
        success: function (data) {
            $('#mdlcrudtitle').html('Agregar Cotizacion');
            $('#mdlContentCrudCotizacion').html(data);
            $('#mdlCrudCotizacion').modal('show');

        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Detalles(id) {

    $.ajax({
        url: '/Cotizacion/Details/' + id,
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
        url: '/Cotizacion/Delete/' + id,
        success: function (data) {

            $('#mdlcrudtitle').html('Eliminar Cotizacion');
            $('#mdlContentCrudCotizacion').html(data);
            $('#mdlCrudCotizacion').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}

function Editar(id) {

    $.ajax({
        url: '/Cotizacion/Edit/' + id,
        success: function (data) {

            $('#mdlcrudtitle').html('Editar Cotizacion');
            $('#mdlContentCrudCotizacion').html(data);
            $('#mdlCrudCotizacion').modal('show');
        },
        error: function (error) {
            console.log(error);
        }
    });

}