using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace projetoJapones.Models
{
    public class modelVendas
    {
        [Display(Name = "Código do Pedido")]
        public string codVenda { get; set; }

        [Display(Name = "Data do Pedido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string DtVenda { get; set; }

        public string UsuarioID { get; set; }

        [Display(Name = "Hora do Pedido")]
        public string horaVenda { get; set; }

        [Display(Name = "Valor Total")]
        public double ValorTotal { get; set; }

        [Display(Name = "Pagamento")]
        public string cd_Pagto { get; set; }


        public List<modelItemCarrinho> ItensPedido = new List<modelItemCarrinho>();
    }
}