//function BuscarNombreApi() {
//    let Identificacion = $('#Usu_Cedula').val();

//    if (Identificacion.trim().length >= 9) {
//        $.ajax({
//            url: `https://apis.gometa.org/cedulas/${Identificacion}`,
//            type: 'GET',
//            dataType: 'json',
//            success: function (res) {
//                $('#Usu_Nombre').val(res.nombre);
//            }
//        });
//    } else {
//        $('#Usu_Nombre').val("");
//    }
//}

function BuscarCorreo(funcion) {
    let Usu_Correo = $('#Usu_Correo').val();
    $('#btnProcesar').prop('disabled', true);

    if (Usu_Correo.trim() !== '') {
        $.ajax({
            url: '/Home/BuscarExisteCorreo',
            data: {
                'Usu_Correo': Usu_Correo
            },
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (funcion === "Registrar") {
                    if (res === '') $('#btnProcesar').prop('disabled', false);
                    else alert(res);
                } else if (funcion === "RecuperarContra") {
                    if (res !== '') $('#btnProcesar').prop('disabled', false);
                    else alert("El correo no se encuentra en el sistema");
                }
            }
        });
    }
}

function InactivarUsuario(Usu_Id) {
    $.ajax({
        url: '/Usuarios/InactivarUsuario',
        data: {
            "Usu_Id": Usu_Id
        },
        type: 'POST',
        dataType: 'json',
        success: function (res) {
            if (res > 0) {
                window.location.href = "/Usuarios/ConsultarUsuarios";
            }
        }
    });
}

function agregarDataTable(tabla){
    tabla.DataTable({
        'lengthMenu': [5, 10, 15, 20, 25, 30, 35]
    });
}

$(document).ready(function () {
    if ($('#TablaUsuarios').length > 0) agregarDataTable($('#TablaUsuarios'));

    if ($('#TablaServicios').length > 0) agregarDataTable($('#TablaServicios'));

    if ($('#TablaReservaciones').length > 0) agregarDataTable($('#TablaReservaciones'));

    if ($('#TablaVehiculos').length > 0) agregarDataTable($('#TablaVehiculos'));
});