using MySql.Data.MySqlClient;
using projetoJapones.Dados;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projetoJapones.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public void carregaCategoria()
        {
            List<SelectListItem> categoria = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_japones;User=root;pwd=root123"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbl_Categoria order by nm_Cat;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    categoria.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.categoria = new SelectList(categoria, "Value", "Text");
        }
        acProduto acoes = new acProduto();
        modelCategoria clCategoria = new modelCategoria();



        public ActionResult Index()
        {
            carregaCategoria();
            return View();
        }

        [HttpPost]
        public ActionResult Index(modelProduto md, HttpPostedFileBase file)
        {
            carregaCategoria();
            md.cd_Cat = Request.Form["categoria"];
            string arquivo = Path.GetFileName(file.FileName);
            string file2 = "/Imagens/" + Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
            file.SaveAs(_path);
            md.img_Produto = file2;
            acoes.inserirProduto(md);
            ViewBag.msg = "Cadastro realizado";
            return RedirectToAction("ConsProduto");
        }

        public ActionResult Detalhes(string id)
        {
            return View(acoes.GetAtendProd().Find((smodel => smodel.cd_Produto == id)));
        }

        public ActionResult ExcluirProduto(int id)
        {
            acoes.DeleteProduto(id);
            return RedirectToAction("ConsProduto");
        }

        public ActionResult Edit()
        {
           // acoes.Edit(id);
            return View();
        }

        public ActionResult ConsProduto()
        {
            return View(acoes.Listar());
        }

        public ActionResult EditarProduto(string id, modelProduto cm)
        {
            carregaCategoria();
            cm.cd_Produto = id;
            acoes.GetProdutoId(cm);
            ViewBag.nm_Produto = cm.nm_Produto;
            ViewBag.desc_Produto = cm.desc_Produto;
            ViewBag.qt_Estoque = cm.qt_Estoque;
            ViewBag.vl_Produto = cm.vl_Produto;
            ViewBag.img_Produto = cm.img_Produto;
            ViewBag.cd_Cat = cm.cd_Cat;
            return View();
        }


        [HttpPost]
        public ActionResult EditarProduto(int id, modelProduto cm, HttpPostedFileBase file)
        {
            cm.cd_Produto = id.ToString();
            carregaCategoria();
            cm.cd_Cat = Request.Form["categoria"];
            string arquivo = Path.GetFileName(file.FileName);
            string file2 = "/Imagens/" + Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
            file.SaveAs(_path);
            cm.img_Produto = file2;
            acoes.AtualizaProduto(cm);
            return View();
        }
    }
}