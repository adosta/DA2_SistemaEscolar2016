using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016_2_.Models
{
    public class Alumno
    {
        [Key]
        public int noMatricula { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Nombre")]
        public String nombre { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        public String apellidoP { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        public String apellidoM { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime fechNac { get; set; }

        //un alumno tiene un solo grupo
        public int grupoID { get; set; }
        virtual public Grupo grupo { get; set; }
    }
}