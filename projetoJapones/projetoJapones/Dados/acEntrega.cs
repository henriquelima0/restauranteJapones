using MySql.Data.MySqlClient;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projetoJapones.Dados
{
    public class acEntrega
    {
        conexao con = new conexao();

        public void inserirEntrega(modelEntrega ent)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_Entrega values (default, @no_cep, @nm_estado, @nm_cidade, @nm_rua, @no_end, @nm_complemento, @nm_bairro, @codVenda)", con.MyConectarBD());

            cmd.Parameters.Add("@no_cep", MySqlDbType.VarChar).Value = ent.no_cep;
            cmd.Parameters.Add("@nm_estado", MySqlDbType.VarChar).Value = ent.nm_estado;
            cmd.Parameters.Add("@nm_cidade", MySqlDbType.VarChar).Value = ent.nm_cidade;
            cmd.Parameters.Add("@nm_rua", MySqlDbType.VarChar).Value = ent.nm_rua;
            cmd.Parameters.Add("@no_end", MySqlDbType.VarChar).Value = ent.no_end;
            cmd.Parameters.Add("@nm_complemento", MySqlDbType.VarChar).Value = ent.nm_complemento;
            cmd.Parameters.Add("@nm_bairro", MySqlDbType.VarChar).Value = ent.nm_bairro;
            cmd.Parameters.Add("@codVenda", MySqlDbType.VarChar).Value = ent.codVenda;



            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<modelEntrega> Listar()
        {
            string strQuery = "select * from tbl_entrega;";
            MySqlDataReader retorno = con.RetornaComando(strQuery);
            return ListaDeEntrega(retorno);
        }

        private List<modelEntrega> ListaDeEntrega(MySqlDataReader retorno)
        {
            var entsentregas = new List<modelEntrega>();

            while (retorno.Read())
            {
                var tempEnt = new modelEntrega()
                {
                    codVenda = retorno["codVenda"].ToString(),
                    no_cep = retorno["no_cep"].ToString(),
                    nm_estado = retorno["nm_estado"].ToString(),
                    nm_cidade = retorno["nm_cidade"].ToString(),
                    nm_bairro = retorno["nm_bairro"].ToString(),
                    nm_rua = retorno["nm_rua"].ToString(),
                    no_end = int.Parse(retorno["no_end"].ToString()),
                    nm_complemento = retorno["nm_complemento"].ToString()
                };
                entsentregas.Add(tempEnt);
            }

            retorno.Close();
            return entsentregas;
        }
    }
}