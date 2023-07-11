function validarPlaca() { //Se valida que se haya escrito una placa
    if ($('#Veh_Id').val().length > 0) {
        $('#reservarServicios').prop('disabled', false)
        $('#validar').prop('disabled', false)
    } else {
        $('#validar').prop('disabled', true)
        $('#reservarServicios').hide()
    }
}

function ConsultarPlaca() { //Se valida que la placa esté registrada a un vehículo
    $.ajax({
        url: '/Vehiculos/ConsultarPlaca?placa=' + $('#Veh_Id').val(),
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            if (res.resultado) {
                $('#vehiculoMsg').text("El vehículo se encuentra registrado").css("color", "green")
                $('#reservarServicios').show()
            } else {
                $('#vehiculoMsg').text("El vehículo no se encuentra registrado").css("color", "red")
                $('#reservarServicios').hide()
            }
        }
    });
}

function mostrarFecha(checkbox) {
    // Obtener el div que contiene el campo de fecha
    var fechaDiv = checkbox.parentNode.parentNode.querySelector('.col-4');

    // Mostrar u ocultar el campo de fecha según el estado del checkbox
    if (checkbox.checked) {
        fechaDiv.style.display = 'block';
    } else {
        fechaDiv.style.display = 'none';
    }
    var checkboxes = document.querySelectorAll('input[type="checkbox"]:checked');

    if (checkboxes.length === 0)
        $('#reservarBtn').prop('disabled', true)
    else
        validarFechas()
}

function validarFechas() {
    var checkboxes = (document.querySelectorAll('input[type="checkbox"]:checked')).length;

    var dates = document.querySelectorAll('input[type="date"]');
    var filteredDates = [];

    dates.forEach(function (date) {
        if (date.value !== '') {
            filteredDates.push(date);
        }
    });

    if (checkboxes == filteredDates.length) //Si hay una misma cantidad de fechas que de servicios seleccionados
        $('#reservarBtn').prop('disabled', false)
    else
        $('#reservarBtn').prop('disabled', true)
}