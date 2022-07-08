using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoJapones.Models
{
    public class modelFuncionario
    {
        [Display(Name = "Código do Funcionário")]
        public string cd_Funcio { get; set; }

        [Display(Name = "Nome do Funcionário")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter até 80 caracteres")]
        public string nm_Funcio { get; set; }

        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(80, ErrorMessage = "Este campo deve conter entre 8 e 80 caracteres", MinimumLength = 80)]
        public string login_Funcio { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(2 , ErrorMessage = "Este campo deve conter entre 2 e 5 caracteres", MinimumLength = 2)]
        public string senha_Funcio { get; set; }

        [Display(Name = "Acesso do Funcionário")]
        public string codTipoUsuario { get; set; }
    }
}