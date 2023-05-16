using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Models;

public partial class Asignacione
{
    public int Id { get; set; }

    public int? IdTarea { get; set; }

    public int? IdUsuario { get; set; }

    [Required(ErrorMessage = "*Ingrese una fecha de asignacion")]
    [DataType(DataType.Date, ErrorMessage = "La fecha no es válida")]
    public DateTime? FechaAsignacion { get; set; }

    public virtual Tarea? IdTareaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
