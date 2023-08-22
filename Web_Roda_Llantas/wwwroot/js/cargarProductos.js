
    function cargarProductos(tipoProductoId) {
        $.ajax({
            url: '/ObtenerProductos', // URL de la acción del controlador que obtiene los productos
            type: 'GET',
            data: { tipoProductoId: tipoProductoId },
            success: function (response) {
                // Actualiza la lista de productos en el DOM con la respuesta obtenida
            },
            error: function (error) {
                console.error('Error al cargar los productos:', error);
            }
        });
    }
