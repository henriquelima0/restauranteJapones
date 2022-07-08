using MySql.Data.MySqlClient;
using projetoJapones.Dados;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projetoJapones.Controllers
{
    public class FuncionarioController : Controller
    {
        acFuncionario ac = new acFuncionario();

        public void carregaTipo()
        {
            List<SelectListItem> tipos = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_japones;User=root;pwd=root123"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbTipoUsuario", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    tipos.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.tipos = new SelectList(tipos, "Value", "Text");
        }
       
        public ActionResult areaFuncionario()
        {
            carregaTipo();
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }
        public ActionResult areaFun()
        {
            ViewBag.usu = Session["usu"];
            return View();
        }
        [HttpPost]
        public ActionResult Login(modelFuncionario verLogin)
        {
            ac.TestarUsuario(verLogin); 

            if (verLogin.login_Funcio != null && verLogin.senha_Funcio != null)
            {
                FormsAuthentication.SetAuthCookie(verLogin.login_Funcio, false);
                Session["usu"] = verLogin.login_Funcio;

                if (verLogin.codTipoUsuario == "1")
                {
                    Session["tipo1"] = verLogin.codTipoUsuario;
                }

                return RedirectToAction("areaFun", "Funcionario");
            }
            else
            {
                ViewBag.msgLogar = "Usuário Incorreto. Verifique o nome de usuário e a senha";
                return View();
            }
        }


        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["tipo1"] = null;
            var carrinho = Session["Carrinho"] != null ? (modelVendas)Session["Carrinho"] : new modelVendas();
            carrinho.ValorTotal = 0;
            carrinho.ItensPedido.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            carregaTipo();
            return View();
        }

        [HttpPost]
        public ActionResult Index(modelFuncionario cm)
        {
            carregaTipo();
            cm.codTipoUsuario = Request["tipos"];
            ac.inserirFuncionario(cm);
            ViewBag.msgCad = "Cadastro Efetuado Com Sucesso!";
            return RedirectToAction("Login", "Funcionario");
        }

        public ActionResult ConsFuncionario()
        {
            return View(ac.Listar());
        }
        public ActionResult Delete(int id)
        {
            ac.Delete(id);
            return RedirectToAction("ConsFuncionario");
        }
    }
}