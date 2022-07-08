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
    public class CategoriaController : Controller
    {
        acCategoria ac = new acCategoria();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TESTE()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(modelCategoria cm)
        {
                    ac.inserirCategoria(cm);
                    ViewBag.msgCad = "Cadastro Efetuado Com Sucesso!";
                    return RedirectToAction("ConsCategoria");
        }

        public ActionResult ConsCategoria()
        {
            return View(ac.Listar());
        }
        public ActionResult ExcluiPgmto(int id)
        {
            ac.Delete(id);
            return RedirectToAction("ConsCategoria");
        }
    }
}