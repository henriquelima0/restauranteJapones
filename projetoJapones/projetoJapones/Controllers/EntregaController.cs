using MySql.Data.MySqlClient;
using projetoJapones.Dados;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projetoJapones.Controllers
{
    public class EntregaController : Controller
    {
        acEntrega ac = new acEntrega();

        public void carregaVenda()
        {
            List<SelectListItem> carregaVenda = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bd_japones;User=root;pwd=root123"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("call sp_CodVenda();", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    carregaVenda.Add(new SelectListItem
                    {
                        Text = rdr[0].ToString(),
                        Value = rdr[1].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.carregaVenda = new SelectList(carregaVenda, "Value", "Text");
        }

        public ActionResult Index()
        {
            return View(ac.Listar());
        }


        public ActionResult entregaFinal()
        {
            carregaVenda();
            return View();
        }

        [HttpPost]
        public ActionResult entregaFinal(modelEntrega entrega)
        {
            carregaVenda();
            entrega.codVenda = Request.Form["carregaVenda"];
            ac.inserirEntrega(entrega);
            ViewBag.msgCad = "Cadastro Efetuado Com Sucesso!";
            return RedirectToAction("Finalizado", "Home");
        }
    }
}