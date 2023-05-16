using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionTareas.Controllers
{
    public class ComentarioController : Controller
    {
        private readonly GestionContext db = new();
        public IActionResult Index()
        {
            var comentarios = db.Comentarios.Include(p => p.IdTareaNavigation)
               .Include(p => p.IdUsuarioNavigation); ;
            return View(comentarios);
        }
        public IActionResult Create()
        {
            ViewData["IdTarea"] = new SelectList(db.Tareas, "Id", "Nombre");
            ViewData["IdUsuario"] = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Add(comentario);
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
                var comentario = db.Comentarios.Find(id);
                //verifica si marca encontro datos
                if (comentario != null)
                {
                    //retorna a la vista Edit con los datos encontrados
                    ViewData["IdTarea"] = new SelectList(db.Tareas, "Id", "Nombre", comentario.IdTarea);
                    ViewData["IdUsuario"] = new SelectList(db.Usuarios, "Id", "Nombre", comentario.IdUsuario);
                    return View(comentario);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Update(comentario);
                db.SaveChanges();
                return RedirectToAction("Index");
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
                var comentario = db.Comentarios.Find(id);
                if (comentario != null)
                {
                    db.Comentarios.Remove(comentario);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
