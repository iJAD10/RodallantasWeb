function InactivarProveedor(Prov_Id) {
    $.ajax({
        url: '/Proveedores/InactivarProveedor',
        data: {
            "Prov_Id": Prov_Id
        },
        type: 'POST',
        dataType: 'json',
        success: function (res) {
            if (res > 0) {
                window.location.href = "/Proveedores/ConsultarProveedores";
            }

        }
    });
}