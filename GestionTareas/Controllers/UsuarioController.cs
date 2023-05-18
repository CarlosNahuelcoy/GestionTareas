using GestionTareas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GestionTareas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly GestionContext db = new();

        public IActionResult Index() {
         return View(db.Usuarios.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Usuario usuario) 
        {
            if (usuario.CorreoElectronico != null)
            {
                if (!Regex.IsMatch(usuario.CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    ModelState.AddModelError("CorreoElectronico", "El correo electrónico no es válido");
                    return View();
                }
                else
                {
                    var existe = db.Usuarios.FirstOrDefault(x => x.Nombre == usuario.Nombre);
                    if (existe == null)
                    {
                        db.Add(usuario);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else {
                        ModelState.AddModelError("Nombre", "Este usuario ya existe");
                        return View();

                    }
                    
                }
            }
            else {
                return View();
            }
        }
        public IActionResult Edit(int? id)
        {
            //verifica si el id es distinto de null
            if (id != null)
            {
                //Find busca por la PK, es equivalente select * from marca where id = id
                var usuario = db.Usuarios.Find(id);
                //verifica si marca encontro datos
                if (usuario != null)
                {
                    //retorna a la vista Edit con los datos encontrados
                    return View(usuario);
                }
            }
            //en caso de error, volver al index
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Usuario usuario)
        {
            if (usuario.CorreoElectronico != null)
            {
                if (!Regex.IsMatch(usuario.CorreoElectronico, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    ModelState.AddModelError("CorreoElectronico", "El correo electrónico no es válido");
                    return View();
                }
                else
                {
                    var existe = db.Usuarios.FirstOrDefault(x => x.Nombre == usuario.Nombre  && x.Id!=usuario.Id);
                    if (existe == null)
                    {
                        db.Update(usuario);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Nombre", "Este usuario ya existe");
                        return View();

                    }
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
                var usuario = db.Usuarios.Find(id);
                if (usuario != null)
                {
                    // Editado por Carlos. Verificar si hay asignaciones o comentarios asociados al usuario
                    var comentariosAsociados = db.Comentarios.Any(t => t.IdUsuario == id);
                    var asignacionesAsociadas = db.Asignaciones.Any(t => t.IdUsuario == id);
                    if (comentariosAsociados || asignacionesAsociadas)
                    {
                        // Editado por Carlos. Hay asignaciones o comentarios asociados, mostrar mensaje de error
                        TempData["ErrorMessage"] = "No se puede eliminar el usuario porque hay asignaciones o comentarios asociados.";
                        return RedirectToAction("Index");
                    }
                    db.Usuarios.Remove(usuario);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


    }
}
