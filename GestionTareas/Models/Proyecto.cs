using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionTareas.Models;

public partial class Proyecto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "*Ingrese un Nombre")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "*Ingrese una Descripción")]
    public string? Descripcion { get; set; }

    
    [Required(ErrorMessage = "*Ingrese una fecha de inicio")]
    [DataType(DataType.Date, ErrorMessage ="La fecha no es válida")]
    public DateTime? FechaInicio { get; set; }
     
    [Required(ErrorMessage = "*Ingrese una Fecha de Fin Provista")]
    [DataType(DataType.Date, ErrorMessage = "La fecha no es válida")]
    public DateTime? FechaFinPrevista { get; set; }
    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}

