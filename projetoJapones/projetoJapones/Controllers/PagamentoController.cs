using projetoJapones.Dados;
using projetoJapones.Models;
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
    public class PagamentoController : Controller
    {
        acPagamento ac = new acPagamento();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(modelPagamento cm)
        {
            ac.inserirPagamento(cm);
            ViewBag.msgCad = "Cadastro Efetuado Com Sucesso!";
            return RedirectToAction("ConsPagamento");
        }
        public ActionResult ConsPagamento()
        {
            return View(ac.Listar());
        }
        public ActionResult ExcluiPgmto(int id)
        {
            ac.DeletePgmto(id);
            return RedirectToAction("ConsPagamento");
        }
    }
}
