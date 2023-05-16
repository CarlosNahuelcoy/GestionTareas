using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Models;

public partial class Usuario
{
    
    public int Id { get; set; }

    [Required(ErrorMessage ="*Ingrese un Nombre")]
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "*Ingrese un Correo Electrónico")]
    public string? CorreoElectronico { get; set; }
    [Required(ErrorMessage = "*Ingrese una contraseña")]
    public string? Contrasena { get; set; }

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
}
