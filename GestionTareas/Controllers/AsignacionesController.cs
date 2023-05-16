using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace GestionTareas.Controllers
{
    public class AsignacionesController : Controller
    {
        private readonly GestionContext db = new();
        public IActionResult Index()
        {
            var asignaciones = db.Asignaciones.Include(p => p.IdTareaNavigation)
                .Include(p => p.IdUsuarioNavigation); ;
            return View(asignaciones);
        }
        public IActionResult Create()
        {
            ViewData["IdTarea"] = new SelectList(db.Tareas, "Id", "Nombre");
            ViewData["IdUsuario"] = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Asignacione asignacion)
        {
            if (ModelState.IsValid)
            {
                db.Add(asignacion);
                db.SaveChanges();
                return RedirectToAction("Index");
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
                //Find busca por la PK 
                var asignacion = db.Asignaciones.Find(id);
                //verifica si marca encontro datos
                if (asignacion != null)
                {
                    //retorna a la vista Edit con los datos encontrados
                    ViewData["IdTarea"] = new SelectList(db.Tareas, "Id", "Nombre", asignacion.IdTarea);
                    ViewData["IdUsuario"] = new SelectList(db.Usuarios, "Id", "Nombre", asignacion.IdUsuario);
                    return View(asignacion);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Asignacione asignacion)
        {
            if (ModelState.IsValid)
            {
                db.Update(asignacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(asignacion);
            }
        }


        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                var asignacion = db.Asignaciones.Find(id);
                if (asignacion != null)
                {
                    db.Asignaciones.Remove(asignacion);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


    }
}
