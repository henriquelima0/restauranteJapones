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
    public class ClienteController : Controller
    {
        acCliente ac = new acCliente();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(modelCliente cm)
        {
            ac.inserirCliente(cm);
            
            ViewBag.msgCad = "Cadastro Efetuado Com Sucesso!";
            return RedirectToAction("Index", "Login");
        }

        public ActionResult ConsCliente()
        {
            return View(ac.Listar());
        }
        public ActionResult ExcluiPgmto(int id)
        {
            ac.DeleteCli(id);
            return RedirectToAction("ConsCliente");
        }
    }
}