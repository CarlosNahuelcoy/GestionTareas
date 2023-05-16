using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionTareas.Controllers
{
    public class TareaController : Controller
    {
        private readonly GestionContext db = new();
        public IActionResult Index()
        {
            var tareas = db.Tareas.Include(p => p.IdProyectoNavigation);
            return View(tareas);
        }
        public IActionResult Create()
        {
            ViewData["IdProyecto"] = new SelectList(db.Proyectos, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Tarea tarea)
        {
            if (ModelState.IsValid)
            {

                if (tarea.FechaLimite < tarea.FechaCreacion)
                {
                    ModelState.AddModelError("FechaLimite", "La fecha límite debe ser mayor a la fecha de creación");
                    return View();
                }
                else
                {
                    db.Add(tarea);
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
                var tarea = db.Tareas.Find(id);
                //verifica si marca encontro datos
                if (tarea != null)
                {
                    //retorna a la vista Edit con los datos encontrados
                    ViewData["IdProyecto"] = new SelectList(db.Proyectos, "Id", "Nombre", tarea.IdProyecto);
                    return View(tarea);
                }
            }
            //en caso de error, volver al index
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                if (tarea.FechaLimite < tarea.FechaCreacion)
                {
                    ModelState.AddModelError("FechaLimite", "La fecha límite debe ser mayor a la fecha de creación");
                    ViewData["IdProyecto"] = new SelectList(db.Proyectos, "Id", "Nombre", tarea.IdProyecto);
                    return View(tarea);
                }
                else
                {
                    db.Update(tarea);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } 
            }
            else
            {
                return View();
            }
        }
         

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var tarea = db.Tareas.Find(id);
                if (tarea != null)
                {
                    // Editado por Carlos. Verificar si hay tareas asociadas al proyecto
                    var tareasAsociadas = db.Comentarios.Any(t => t.IdTarea == id);
                    if (tareasAsociadas)
                    {
                        // Editado por Carlos. Hay tareas asociadas, mostrar mensaje de error
                        TempData["ErrorMessage"] = "No se puede eliminar la tarea porque hay comentarios asociados.";
                        return RedirectToAction("Index");
                    }

                    db.Tareas.Remove(tarea);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
