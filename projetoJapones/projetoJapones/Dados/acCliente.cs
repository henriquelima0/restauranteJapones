using MySql.Data.MySqlClient;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projetoJapones.Dados
{
    public class acCliente
    {
        conexao con = new conexao();

        public void inserirCliente(modelCliente cmCliente)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbl_Cliente values (default, @nm_Cliente, @no_Telefone, @login_Cliente, @senha_Cliente)", con.MyConectarBD());

            cmd.Parameters.Add("@nm_Cliente", MySqlDbType.VarChar).Value = cmCliente.nm_Cliente;
            cmd.Parameters.Add("@no_Telefone", MySqlDbType.VarChar).Value = cmCliente.no_Telefone;
            cmd.Parameters.Add("@login_Cliente", MySqlDbType.VarChar).Value = cmCliente.login_Cliente;
            cmd.Parameters.Add("@senha_Cliente", MySqlDbType.VarChar).Value = cmCliente.senha_Cliente;


            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
        
        public void TestarUsuario(modelCliente user)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Cliente where login_Cliente = @login_Cliente and senha_Cliente = @senha_Cliente ", con.MyConectarBD());

            cmd.Parameters.Add("@login_Cliente", MySqlDbType.VarChar).Value = user.login_Cliente;
            cmd.Parameters.Add("@senha_Cliente", MySqlDbType.VarChar).Value = user.senha_Cliente;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.login_Cliente = Convert.ToString(leitor["login_Cliente"]);
                    user.senha_Cliente = Convert.ToString(leitor["senha_Cliente"]);
                    user.cd_Cliente = Convert.ToString(leitor["cd_Cliente"]);


                }
            }

            else
            {
                user.login_Cliente = null;
                user.senha_Cliente = null;
            }

            con.MyDesconectarBD();
        }
        private List<modelCliente> ListaCli(MySqlDataReader retorno)
        {
            var cli = new List<modelCliente>();

            while (retorno.Read())
            {
                var tempEnt = new modelCliente()
                {
                    cd_Cliente = retorno["cd_Cliente"].ToString(),
                    nm_Cliente = retorno["nm_Cliente"].ToString(),
                    no_Telefone = retorno["no_Telefone"].ToString(),
                    login_Cliente = retorno["login_Cliente"].ToString()
                };
                cli.Add(tempEnt);
            }

            retorno.Close();
            return cli;
        }

        public List<modelCliente> Listar()
        {
            string strQuery = "select * from tbl_Cliente;";
            MySqlDataReader retorno = con.RetornaComando(strQuery);
            return ListaCli(retorno);
        }

        public bool DeleteCli(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_Cliente where cd_Cliente=@id", con.MyConectarBD());

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