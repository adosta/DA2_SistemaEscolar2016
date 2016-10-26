
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016_2_.Models
{
    public class Grupo
    {
        //Asigna a este atributo automaticamente como llave primaria
        public int grupoID { get; set; }
        [Display(Name="Nombre de Grupo")]
        public String nombreGrupo { get; set; }

        public String carrera { get; set; }

        //El grupo tiene muchos alumnos
        public virtual ICollection<Alumno> alumnos { get; set; }
    }
}