using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoJapones.Models
{
    public class modelPagamento
    {

        [Display(Name = "Código da Froma")]
        public string cd_Pagto { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string ds_Pagto { get; set; }
    }
}