using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoJapones.Models
{
    public class modelItemCarrinho
    {
        [Key]
        [Display(Name = "Código do Carrihno")]
        public Guid ItemPedidoID { get; set; }

        [Display(Name = "Código do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string ProdutoID { get; set; }

        [Display(Name = "Nome do Produto")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Produto { get; set; }

        [Display(Name = "Código do Pedido")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string PedidoID { get; set; }

        [Display(Name = "Valor do item")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public double valorUnit { get; set; }

        [Display(Name = "Valor Final")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public double valorParcial { get; set; }
        // public virtual modelCarrinho Pedido { get; set; }



        public int Qtd { get; set; }
    }
}