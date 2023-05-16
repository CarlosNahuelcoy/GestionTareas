using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionTareas.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly GestionContext db = new();
        public IActionResult Index()
        {
            return View(db.Proyectos.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                if (proyecto.FechaFinPrevista < proyecto.FechaInicio)
                {
                    ModelState.AddModelError("FechaFinPrevista", "La fecha de fin prevista debe ser mayor a la fecha de inicio");
                    return View();
                }
                else {
                    db.Add(proyecto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(int? id)
        {
            //verifica si el id es distinto de null
            if (id != null)
            {
                //Find busca por la PK, es equivalente select * from marca where id = id
                var proyecto = db.Proyectos.Find(id);
                //verifica si marca encontro datos
                if (proyecto != null)
                {
                    //retorna a la vista Edit con los datos encontrados
                    return View(proyecto);
                }
            }
            //en caso de error, volver al index
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                if (proyecto.FechaFinPrevista < proyecto.FechaInicio)
                {
                    ModelState.AddModelError("FechaFinPrevista", "La fecha de fin prevista debe ser mayor a la fecha de inicio");
                    return View();
                }
                else
                {
                    db.Update(proyecto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } 
            }
            else
            {
                return View();
            }
        }

        public IActionResult Tareas(int? id)
        {
            //verifica si el id es distinto de null
            if (id != null)
            {
                //Find busca por la PK, es equivalente select * from marca where id = id
                var tareas = db.Tareas.Where(t => t.IdProyecto == id).Include(p => p.IdProyectoNavigation);
                //verifica si marca encontro datos
                if (tareas != null)
                {
                    //retorna a la vista Edit con los datos encontrados
                    return View(tareas.ToList());
                }
            }
            //en caso de error, volver al index
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var proyecto = db.Proyectos.Find(id);
                if (proyecto != null)
                {
                    // Editado por Carlos. Verificar si hay tareas asociadas al proyecto
                    var tareasAsociadas = db.Tareas.Any(t => t.IdProyecto == id);
                    if (tareasAsociadas)
                    {
                        // Editado por Carlos. Hay tareas asociadas, mostrar mensaje de error
                        TempData["ErrorMessage"] = "No se puede eliminar el proyecto porque hay tareas asociadas.";
                        return RedirectToAction("Index");
                    }

                    db.Proyectos.Remove(proyecto);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


    }
}
