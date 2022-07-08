using projetoJapones.Dados;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace projetoJapones.Controllers
{
    public class LoginController : Controller
    {
        acCliente ac = new acCliente();
        modelCliente m = new modelCliente();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(modelCliente verLogin)
        {
            ac.TestarUsuario(verLogin);

            if (verLogin.login_Cliente != null && verLogin.senha_Cliente != null)
            {
                FormsAuthentication.SetAuthCookie(verLogin.login_Cliente, false);

                Session["usuarioLogado"] = verLogin.login_Cliente.ToString();
                Session["senhaLogado"] = verLogin.senha_Cliente.ToString();
                Session["cd_Cliente"] = verLogin.cd_Cliente.ToString();
  


                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome do usuário e a senha";
                return View();

            }


        }

        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index2(modelCliente verLogin)
        {
            ac.TestarUsuario(verLogin);

            if (verLogin.login_Cliente != null && verLogin.senha_Cliente != null)
            {
                FormsAuthentication.SetAuthCookie(verLogin.login_Cliente, false);
                Session["usuarioLogado"] = verLogin.login_Cliente.ToString();
                Session["senhaLogado"] = verLogin.senha_Cliente.ToString();
                Session["codCli"] = verLogin.cd_Cliente.ToString();

                /* Tipo de Usuário
                if (verLogin.tipo == "1")
                {
                    Session["tipoLogado1"] = verLogin.tipo.ToString(); //=1;
                }
                else
                {
                    Session["tipoLogado2"] = verLogin.tipo.ToString();//=2
                }*/
                return RedirectToAction("Carrinho", "Home");
            }

            else
            {
                ViewBag.msgLogar = "Usuário não encontrado. Verifique o nome do usuário e a senha";
                return View();

            }
        }
    }
}