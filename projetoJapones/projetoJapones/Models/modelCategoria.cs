using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoJapones.Models
{
    public class modelCategoria
    {
        public string cd_Cat { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(80, ErrorMessage = "Este campo deve conter no máximo 80 caracteres")]
        public string nm_Cat { get; set; }
    }
}