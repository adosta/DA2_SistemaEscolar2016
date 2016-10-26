using DA2_SistemaEscolar2016_2_.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA2_SistemaEscolar2016_2_.Controllers
{
    [Authorize]
    public class GrupoController : Controller
    {
        //Conectarnos a la BD
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Authorize(Roles = "admin,capturista,visitante")]
        public ActionResult listar()
        {
            //Consultar la lista de alumnos
            //SELECT * FROM alumnos
            //List<Alumno> todoslosAlumnos = db.alumnos.ToList();
            var todoslosGrupos = db.grupos.ToList();

            //Pedirle a la vista que muestre el resultado en pantalla

            return View(todoslosGrupos);
        }

        [HttpPost]
        [Authorize(Roles = "admin,capturista,visitante")]
        public ActionResult listar(string nombreBuscado)
        {
            var resultadoDeBusqueda = new List<Grupo>();
            if (!String.IsNullOrEmpty(nombreBuscado))
            {
                //Consultar la lista de alumnos
                resultadoDeBusqueda = db.grupos.Where(a => a.carrera.Contains(nombreBuscado) || a.nombreGrupo.Contains(nombreBuscado)).ToList();
            }

            else
            {
                //arrojar todos los datos
                resultadoDeBusqueda = db.grupos.ToList();
            }


            return View(resultadoDeBusqueda);
        }

        //Muestra una vista para crear un nuevo alumno
        [HttpGet]
        [Authorize(Roles = "admin,capturista")]
        public ActionResult crear()
        {
            //Tomar los datos con los que rellenar la lista
            var grupos = db.grupos;
            SelectList grupoID = new SelectList(grupos, "grupoID", "nombreGrupo");
            //Se envia la lista de seleccion a la vista
            ViewBag.grupoID = grupoID;

            return View();
        }

        //Recibe los datos para el nuevo alumno y guardarlos
        [HttpPost]
        [Authorize(Roles = "admin,capturista")]
        public ActionResult crear(Grupo gruponuevo)
        {
            //Validar si el alumno es valido
            if (ModelState.IsValid)
            {
                //Crear alumno
                db.grupos.Add(gruponuevo);

                //Guardar cambios
                db.SaveChanges();

                return RedirectToAction("listar");

            }
            //Si vuelve a la vista es por que se presento un error
            ViewBag.MensajeError = "Hubo un error, favor de verificar la informacion";
            //Regresar una vista

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult eliminar(int id = 0)
        {
            var grupo = db.grupos.Find(id);
            if (grupo == null)
            {
                return RedirectToAction("listar");
            }

            return View(grupo);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "admin")]
        public ActionResult confirmarEliminar(int grupoID = 0)
        {
            var grupo = db.grupos.Find(grupoID);
            if (grupo == null)
            {
                return RedirectToAction("listar");
            }
            db.grupos.Remove(grupo);
            db.SaveChanges();

            return RedirectToAction("listar");
        }

        [HttpGet]
        [Authorize(Roles = "admin,capturista")]
        public ActionResult editar(int id = 0)
        {
            var grupo = db.grupos.Find(id);
            if (grupo == null)
            {
                return RedirectToAction("listar");
            }

            //Tomar los datos con los que rellenar la lista
            var grupos = db.grupos;
            SelectList grupoID = new SelectList(grupos, "grupoID", "nombreGrupo");
            //Se envia la lista de seleccion a la vista
            ViewBag.grupoID = grupoID;
            return View(grupo);
        }

        [HttpPost]
        [Authorize(Roles = "admin,capturista")]
        public ActionResult editar(Alumno grupoEditado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupoEditado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listar");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult detalles(int id = 0)
        {
            var grupos = db.grupos.Find(id);

            if(grupos == null)
            {
                RedirectToAction("listar");
            }
            return View(grupos);
        }
    }
}