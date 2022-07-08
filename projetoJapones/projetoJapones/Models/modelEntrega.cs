using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoJapones.Models
{
    public class modelEntrega
    {
        [Key]
        [Display(Name = "Código da Entrega")]
        public int cd_entrega { get; set; }

        [Display(Name = "Código/Nome Venda")]
        public string codVenda { get; set; }

        [Display(Name = "Previsão de Entrega")]
        [Required]
        public string dt_entrega { get; set; }

        [Display(Name = "Valor do Frete")]
        [Required]
        public string vl_frete { get; set; }

        [Display(Name = "CEP")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(9, ErrorMessage = "Este campo deve conter 9 caracteres", MinimumLength = 9)]
        public string no_cep { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O CEP inserido é inválido e/ou não foi encontrado!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_estado { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_cidade { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_bairro { get; set; }

        [Display(Name = "Rua")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        public string nm_rua { get; set; }

        [Display(Name = "Número do endereço")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int no_end { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(30, ErrorMessage = "Este campo deve conter no máximo 30 caracteres")]
        public string nm_complemento { get; set; }
    }
}