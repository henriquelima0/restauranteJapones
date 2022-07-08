using MySql.Data.MySqlClient;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projetoJapones.Dados
{
    public class acProduto
    {
        conexao con = new conexao();

        public void inserirProduto(modelProduto cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_Produto(cd_Produto, nm_Produto, vl_Produto, desc_Produto, qt_Estoque, img_Produto, cd_Cat) values(default, @nm_Produto, @vl_Produto, @desc_Produto, @qt_Estoque , @img_Produto, @cd_Cat)", con.MyConectarBD());

            cmd.Parameters.Add("@nm_Produto", MySqlDbType.VarChar).Value = cm.nm_Produto;
            cmd.Parameters.Add("@vl_Produto", MySqlDbType.VarChar).Value = cm.vl_Produto;
            cmd.Parameters.Add("@desc_Produto", MySqlDbType.VarChar).Value = cm.desc_Produto;
            cmd.Parameters.Add("@qt_Estoque", MySqlDbType.VarChar).Value = cm.qt_Estoque;
            cmd.Parameters.Add("@img_Produto", MySqlDbType.VarChar).Value = cm.img_Produto;
            cmd.Parameters.Add("@cd_Cat", MySqlDbType.VarChar).Value = cm.cd_Cat;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
            public void deletarProduto(modelProduto cm)
            {
                MySqlCommand cmd = new MySqlCommand("delete from tbl_Produto " + "where cd_Produto = {0}", con.MyConectarBD());

                cmd.ExecuteNonQuery();
                con.MyDesconectarBD();
            }

            public List<modelProduto> GetAtendProd()
        {
            List<modelProduto> Produtoslist = new List<modelProduto>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_Produto", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new modelProduto
                    {
                        cd_Produto = Convert.ToString(dr["cd_Produto"]),
                        nm_Produto = Convert.ToString(dr["nm_Produto"]),
                        vl_Produto = Convert.ToString(dr["vl_Produto"]),
                        desc_Produto = Convert.ToString(dr["desc_Produto"]),
                        qt_Estoque = Convert.ToString(dr["qt_Estoque"]),
                        cd_Cat = Convert.ToString(dr["cd_Cat"]),//necessario? 
                        img_Produto = Convert.ToString(dr["img_Produto"])
                        
                    });
            }
            return Produtoslist;
        }


        public List<modelProduto> GetConsProd(int id)
        {
            List<modelProduto> Produtoslist = new List<modelProduto>();

            MySqlCommand cmd = new MySqlCommand("select * from tbl_Produto where cd_Produto=@cod", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@cod", id);
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Produtoslist.Add(
                    new modelProduto
                    {
                        cd_Produto = Convert.ToString(dr["cd_Produto"]),
                        nm_Produto = Convert.ToString(dr["nm_Produto"]),
                        vl_Produto = Convert.ToString(dr["vl_Produto"]),
                        desc_Produto = Convert.ToString(dr["desc_Produto"]),
                        qt_Estoque = Convert.ToString(dr["qt_Estoque"]),
                        cd_Cat = Convert.ToString(dr["cd_Cat"]),//necessario? 
                        img_Produto = Convert.ToString(dr["img_Produto"])
                    });
            }
            return Produtoslist;
        }

        public List<modelProduto> Listar()
        {
            string strQuery = "select * from tbl_Produto;";
            MySqlDataReader retorno = con.RetornaComando(strQuery);
            return ListaDeProduto(retorno);
        }

        private List<modelProduto> ListaDeProduto(MySqlDataReader retorno)
        {
            var entsentregas = new List<modelProduto>();

            while (retorno.Read())
            {
                var tempEnt = new modelProduto()
                {
                    cd_Produto = retorno["cd_Produto"].ToString(),
                    nm_Produto = retorno["nm_Produto"].ToString(),
                    desc_Produto = retorno["desc_Produto"].ToString(),
                    vl_Produto = retorno["vl_Produto"].ToString(),
                    qt_Estoque = retorno["qt_Estoque"].ToString(),
                    cd_Cat = retorno["cd_Cat"].ToString(),
                    img_Produto = retorno["img_Produto"].ToString()
                };
                entsentregas.Add(tempEnt);
            }

            retorno.Close();
            return entsentregas;
        }

        public bool DeleteProduto(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_Produto where cd_Produto=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public void GetProdutoId(modelProduto cm)
        {

            MySqlCommand cmd = new MySqlCommand("select * from tbl_Produto where cd_Produto=@id ", con.MyConectarBD());
            cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = cm.cd_Produto;
            MySqlDataReader dr;

            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cm.cd_Produto = Convert.ToString(dr["cd_Produto"]);
                    cm.nm_Produto = Convert.ToString(dr["nm_Produto"]);
                    cm.desc_Produto = Convert.ToString(dr["desc_Produto"]);
                    cm.qt_Estoque = Convert.ToString(dr["qt_Estoque"]);
                    cm.vl_Produto = Convert.ToString(dr["vl_Produto"]);
                    cm.img_Produto = Convert.ToString(dr["img_Produto"]);
                    cm.cd_Cat = Convert.ToString(dr["cd_Cat"]);
                }
            }
            else
            {

            }
        }
            public bool AtualizaProduto(modelProduto cm)
            {
                MySqlCommand cmd = new MySqlCommand("update tbl_Produto set nm_Produto=@nm_Produto, desc_Produto=@desc_Produto, vl_Produto=@vl_Produto, img_Produto=@img_Produto, cd_Cat=@cd_Cat", con.MyConectarBD());


                cmd.Parameters.AddWithValue("@cd_Produto", cm.cd_Produto);
                cmd.Parameters.AddWithValue("@nm_Produto", cm.nm_Produto);
                cmd.Parameters.AddWithValue("@desc_Produto", cm.desc_Produto);
                cmd.Parameters.AddWithValue("@qt_Estoque", cm.qt_Estoque);
                cmd.Parameters.AddWithValue("@vl_Produto", cm.vl_Produto);
                cmd.Parameters.AddWithValue("@img_Produto", cm.img_Produto);
                cmd.Parameters.AddWithValue("@cd_Cat", cm.cd_Cat);



                int i = cmd.ExecuteNonQuery();
                con.MyDesconectarBD();

                if (i >= 1)
                    return true;
                else
                    return false;
            }
        }
    }

