using MySql.Data.MySqlClient;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projetoJapones.Dados
{
    public class acCategoria
    {
        conexao con = new conexao();

        public void inserirCategoria(modelCategoria cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_Categoria values (default, @nm_Cat)", con.MyConectarBD());
            cmd.Parameters.Add("@nm_Cat", MySqlDbType.VarChar).Value = cm.nm_Cat;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        private List<modelCategoria> Listar(MySqlDataReader retorno)
        {
            var ctgo = new List<modelCategoria>();

            while (retorno.Read())
            {
                var tempEnt = new modelCategoria()
                {
                    cd_Cat = retorno["cd_Cat"].ToString(),
                    nm_Cat = retorno["nm_Cat"].ToString(),

                };
                ctgo.Add(tempEnt);
            }

            retorno.Close();
            return ctgo;
        }

        public List<modelCategoria> Listar()
        {
            string strQuery = "select * from tbl_Categoria;";
            MySqlDataReader retorno = con.RetornaComando(strQuery);
            return Listar(retorno);
        }

        public bool Delete(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_Categoria where cd_Cat=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
                
            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}