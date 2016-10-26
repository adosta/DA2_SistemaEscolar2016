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
    public class AlumnoController : Controller
    {
        //Conectarnos a la BD
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        [Authorize(Roles = "admin,capturista,visitante")]
        public ActionResult listar(String nombreBuscado)
        {
            var resultadoDeBusqueda = new List<Alumno>();
            if (!String.IsNullOrEmpty(nombreBuscado))
            {
                //Consultar la lista de alumnos
                resultadoDeBusqueda = db.alumnos.Where(a => a.nombre.Contains(nombreBuscado) 
                || a.apellidoP.Contains(nombreBuscado) || a.apellidoM.Contains(nombreBuscado)).ToList();
            }

            else
            {
                //arrojar todos los datos
                resultadoDeBusqueda = db.alumnos.ToList();
            }
           

            return View(resultadoDeBusqueda);
        }

        [HttpGet]
        [Authorize(Roles = "admin,capturista,visitante")]
        public ActionResult listar()
        {

            //Consultar la lista de alumnos
            //SELECT * FROM alumnos
            //List<Alumno> todoslosAlumnos = db.alumnos.ToList();
            var todoslosAlumnos = db.alumnos.ToList();

            //Pedirle a la vista que muestre el resultado en pantalla

            return View(todoslosAlumnos);
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
        public ActionResult crear(Alumno alumnonuevo, bool enDetallesDeGrupo = false)
        {
            //Validar si el alumno es valido
            if (ModelState.IsValid)
            {
                //Crear alumno
                db.alumnos.Add(alumnonuevo);

                //Guardar cambios
                db.SaveChanges();

                if (enDetallesDeGrupo)
                {
                    return RedirectToAction("detalles", "grupo", new { id = alumnonuevo.grupoID });
                }
                else
                {
                    return RedirectToAction("listar");
                }
                

            }
                //Si vuelve a la vista es por que se presento un error
                ViewBag.MensajeError = "Hubo un error, favor de verificar la informacion";  
                //Regresar una vista
                
                return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult eliminar(int id=0)
        {
            var alumno = db.alumnos.Find(id);
            if(alumno == null)
            {
                return RedirectToAction("listar");
            }

            return View(alumno);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ActionName("eliminar")]
        public ActionResult confirmarEliminar(int noMatricula = 0)
        {
            var alumno = db.alumnos.Find(noMatricula);
            if (alumno == null)
            {
                return RedirectToAction("listar");
            }
            db.alumnos.Remove(alumno);
            db.SaveChanges();

            return RedirectToAction("listar");
        }

        [HttpGet]
        [Authorize(Roles = "admin,capturista")]
        public ActionResult editar(int id = 0)
        {
            var alumno = db.alumnos.Find(id);
            if (alumno == null)
            {
                return RedirectToAction("listar");
            }

            //Tomar los datos con los que rellenar la lista
            var grupos = db.grupos;
            SelectList grupoID = new SelectList(grupos, "grupoID", "nombreGrupo");
            //Se envia la lista de seleccion a la vista
            ViewBag.grupoID = grupoID;
            return View(alumno);
        }

        [HttpPost]
        [Authorize(Roles = "admin,capturista")]
        public ActionResult editar(Alumno alumnoEditado)
        {
            if(ModelState.IsValid)
                {
                db.Entry(alumnoEditado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Listar");
            }
            return View();
        }


    }
}