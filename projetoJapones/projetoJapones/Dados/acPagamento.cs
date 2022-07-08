using MySql.Data.MySqlClient;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projetoJapones.Dados
{
    public class acPagamento
    {
        conexao con = new conexao();

        public void inserirPagamento(modelPagamento cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_Pagamento values (default, @ds_Pagto)", con.MyConectarBD());
            cmd.Parameters.Add("@ds_Pagto", MySqlDbType.VarChar).Value = cm.ds_Pagto;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        private List<modelPagamento> ListaPagamento(MySqlDataReader retorno)
        {
            var pgmto = new List<modelPagamento>();

            while (retorno.Read())
            {
                var tempEnt = new modelPagamento()
                {
                    cd_Pagto = retorno["cd_Pagto"].ToString(),
                    ds_Pagto = retorno["ds_Pagto"].ToString(),
                };
                pgmto.Add(tempEnt);
            }

            retorno.Close();
            return pgmto;
        }

        public List<modelPagamento> Listar()
        {
            string strQuery = "select * from tbl_Pagamento;";
            MySqlDataReader retorno = con.RetornaComando(strQuery);
            return ListaPagamento(retorno);
        }     

        public bool DeletePgmto(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_Pagamento where cd_Pagto=@id", con.MyConectarBD());

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