using MySql.Data.MySqlClient;
using projetoJapones.Dados;
using projetoJapones.Models;
using projetoJapones.ServiceReferenceCorreios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projetoJapones.Controllers
{
    public class HomeController : Controller
    {

        public void carregaPagamento()
        {
            List<SelectListItem> pagamento = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_japones;User=root;pwd=root123"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_Pagamento order by ds_Pagto;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pagamento.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.pagamento = new SelectList(pagamento, "Value", "Text");
        }

        acProduto ac = new acProduto();
        acVendas acV = new acVendas();
        acItem acI = new acItem();
        acCliente acC = new acCliente();

        public ActionResult Index()
        {
            ViewBag.usuarioLogado = Session["usuarioLogado"];
            return View(ac.GetAtendProd());
        }

        //private Context db = new Context();

        public static string codigo;

        public ActionResult AdicionarCarrinho(int id, double pre)
        {
            modelVendas carrinho = Session["Carrinho"] != null ? (modelVendas)Session["Carrinho"] : new modelVendas();
            var produto = ac.GetConsProd(id);
            codigo = id.ToString();

            modelProduto prod = new modelProduto();

            if (produto != null)
            {
                var itemPedido = new modelItemCarrinho();
                itemPedido.ItemPedidoID = Guid.NewGuid();
                itemPedido.ProdutoID = id.ToString();
                itemPedido.Produto = produto[0].nm_Produto;
                itemPedido.Qtd = 1;
                itemPedido.valorUnit = pre;

                List<modelItemCarrinho> x = carrinho.ItensPedido.FindAll(l => l.Produto == itemPedido.Produto);

                if (x.Count != 0)
                {
                    carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nm_Produto).Qtd += 1;
                    itemPedido.valorParcial = itemPedido.Qtd * itemPedido.valorUnit;
                    carrinho.ValorTotal += itemPedido.valorParcial;
                    carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nm_Produto).valorParcial = carrinho.ItensPedido.FirstOrDefault(p => p.Produto == produto[0].nm_Produto).Qtd * itemPedido.valorUnit;

                }

                else
                {
                    itemPedido.valorParcial = itemPedido.Qtd * itemPedido.valorUnit;
                    carrinho.ValorTotal += itemPedido.valorParcial;
                    carrinho.ItensPedido.Add(itemPedido);
                }

                /*carrinho.ValorTotal = carrinho.ItensPedido.Select(i => i.Produto).Sum(d => d.Valor);*/

                Session["Carrinho"] = carrinho;
            }

            return RedirectToAction("Carrinho");
        }

        public ActionResult Carrinho()
        {
            modelVendas carrinho = Session["Carrinho"] != null ? (modelVendas)Session["Carrinho"] : new modelVendas();

            return View(carrinho);
        }

        public ActionResult ExcluirItem(Guid id)
        {
            var carrinho = Session["Carrinho"] != null ? (modelVendas)Session["Carrinho"] : new modelVendas();
            var itemExclusao = carrinho.ItensPedido.FirstOrDefault(i => i.ItemPedidoID == id);

            carrinho.ValorTotal -= itemExclusao.valorParcial;

            carrinho.ItensPedido.Remove(itemExclusao);

            Session["Carrinho"] = carrinho;
            return RedirectToAction("Carrinho");
        }

        public ActionResult SalvarCarrinho(modelVendas x)
        {

            if ((Session["usuarioLogado"] == null) || (Session["senhaLogado"] == null))

            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var carrinho = Session["Carrinho"] != null ? (modelVendas)Session["Carrinho"] : new modelVendas();

                modelVendas md = new modelVendas();
                modelItemCarrinho mdV = new modelItemCarrinho();
                md.DtVenda = DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy");
                md.UsuarioID = Session["cd_Cliente"].ToString();
                md.horaVenda = DateTime.Now.ToLocalTime().ToString("HH:mm");


                md.ValorTotal = carrinho.ValorTotal;

                acV.inserirVenda(md);


                acV.buscaIdVenda(x);

                for (int i = 0; i < carrinho.ItensPedido.Count; i++)
                {

                    mdV.PedidoID = x.codVenda;
                    mdV.ProdutoID = carrinho.ItensPedido[i].ProdutoID;
                    mdV.Qtd = carrinho.ItensPedido[i].Qtd;
                    mdV.valorParcial = carrinho.ItensPedido[i].valorParcial;
                    acI.inserirItem(mdV);
                }

              //  carrinho.ValorTotal = 0;
           //     carrinho.ItensPedido.Clear();

                return RedirectToAction("entregaFinal", "Entrega");
            }
        }

        public ActionResult Finalizado(modelVendas mV, modelEntrega mE)
        {
            Carrinho();
            carregaPagamento();
            ViewBag.resultado = mE.vl_frete;
            GridView dgv = new GridView();
            dgv.DataSource = acV.selectVenda();
            dgv.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgv.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }
        public ActionResult Pedido()
        {
            return View();
        }

        public JsonResult CalculaFrete(string cep)
        {
            string cepArmazem = "06454911";

            CalcPrecoPrazoWSSoapClient wsCorreios = new CalcPrecoPrazoWSSoapClient();
            cResultado retornoCorreios = wsCorreios.CalcPrecoPrazo(
                string.Empty, string.Empty, "41106", cepArmazem, cep,
                "1", 1, 20, 20, 20, 0, "N", 0, "N"
            );

            string[] result = new string[2];
            result[0] = retornoCorreios.Servicos[0].PrazoEntrega;
            result[1] = retornoCorreios.Servicos[0].Valor;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Perfil(modelCliente cm)
        {

            ViewBag.cd_Cliente = Session["cd_Cliente"];
            ViewBag.usuarioLogado = Session["usuarioLogado"];
          
            return View();
        }
    }
}