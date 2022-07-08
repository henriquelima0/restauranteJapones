using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoJapones.Models
{
    public class modelCliente
    {
        [Display(Name = "Código do Cliente")]
        public string cd_Cliente { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(80, ErrorMessage = "Este campo deve conter no máximo 80 caracteres")]
        public string nm_Cliente { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Forneça o número de telefone no formato (00) 0000-00000")]

        public string no_Telefone { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(9, ErrorMessage = "Este campo deve conter 9 caracteres", MinimumLength = 9)]
        public string no_cep { get; set; }


        [Display(Name = "Rua")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 80 caracteres")]
        public string nm_Logradouro { get; set; }

        [Display(Name = "Número do endereço")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string no_Logrardouro { get; set; }

        [Display(Name = "Complemento")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string ds_Complemento { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 30 caracteres")]
        public string nm_Bairro { get; set; }

        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(80, ErrorMessage = "Este campo deve conter entre 2 e 80 caracteres", MinimumLength = 2)]
        public string login_Cliente { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(16, ErrorMessage = "Este campo deve conter entre 2 e 16 caracteres", MinimumLength = 2)]
        public string senha_Cliente { get; set; }
        public string conf_senha { get; set; }
        public string codTipoUsuario { get; set; }
    }
}