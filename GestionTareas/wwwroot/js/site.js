
document.querySelectorAll('.eliminar-registro').forEach((botonEliminar) => {
    botonEliminar.addEventListener('click', (e) => {
        event.preventDefault(); //prevent the default navigate behavior. 
        const deleteUrl = botonEliminar.dataset.deleteUrl;
        Swal.fire({
            title: 'Estás seguro que quieres eliminar?',
            text: "No podrá recuperar el registro!",
            icon: 'warning',
            showCancelButton: true,
            cancelButtonColor: '#3085d6',
            confirmButtonColor: '#d33',
            confirmButtonText: 'Eliminar!'
        }).then((result) => {
            if (result.isConfirmed) {
                window.location.href = deleteUrl;
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Registro eliminado',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        })

    })
})
 