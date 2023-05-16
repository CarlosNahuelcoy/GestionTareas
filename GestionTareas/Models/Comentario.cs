using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Models;

public partial class Comentario
{
    public int Id { get; set; }
    [Required(ErrorMessage = "*Ingrese un comentario")]
    public string? Contenido { get; set; }

    [Required(ErrorMessage = "*Ingrese una fecha de creacion")]
    [DataType(DataType.Date, ErrorMessage = "La fecha no es válida")]
    public DateTime? FechaCreacion { get; set; }

    public int? IdTarea { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Tarea? IdTareaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
