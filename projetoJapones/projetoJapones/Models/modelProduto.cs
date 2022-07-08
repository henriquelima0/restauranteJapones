using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoJapones.Models
{
    public class modelProduto
    {
        [Display(Name = "Código do Produto")]
        public string cd_Produto { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(50, ErrorMessage = "Este campo deve conter no máximo 80 caracteres")]
        public string nm_Produto { get; set; }

        [Display(Name = "Preço unitário")]
        [Required(ErrorMessage = "Preço unitário é um Campo Obrigatório!")]
        public string vl_Produto { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(400, ErrorMessage = "Este campo deve conter no máximo 140 caracteres")]
        public string desc_Produto { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string qt_Estoque { get; set; }

        [Display(Name = "Imagem ")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(255, ErrorMessage = "Este campo deve conter no máximo 255 caracteres")]
        public string img_Produto { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        [StringLength(80, ErrorMessage = "Este campo deve conter no máximo 80 caracteres")]
        public string cd_Cat { get; set; }
    }
}