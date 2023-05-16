using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Models;

public partial class Tarea
{
    public int Id { get; set; }

    [Required(ErrorMessage = "*Ingrese un Nombre")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "*Ingrese una descripción")]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "*Ingrese una fecha de creación")]
    [DataType(DataType.Date, ErrorMessage = "La fecha no es válida")]
    public DateTime? FechaCreacion { get; set; }

    [Required(ErrorMessage = "*Ingrese una fecha de límite")]
    [DataType(DataType.Date, ErrorMessage = "La fecha no es válida")]
    public DateTime? FechaLimite { get; set; }

    [Required(ErrorMessage = "*Ingrese un Estado")]
    public string? Estado { get; set; }

    public int? IdProyecto { get; set; }

    public virtual ICollection<Asignacione> Asignaciones { get; set; } = new List<Asignacione>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual Proyecto? IdProyectoNavigation { get; set; }
}
