using MySql.Data.MySqlClient;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projetoJapones.Dados
{
    public class acFuncionario
    {
        conexao con = new conexao();

        public void inserirFuncionario(modelFuncionario cmFuncionario)
        {

            MySqlCommand cmd = new MySqlCommand("insert into tbl_Funcionario values (default, @nm_Funcio, @login_Funcio, @senha_Funcio, @codTipoUsuario)", con.MyConectarBD());

   
            cmd.Parameters.Add("@nm_Funcio", MySqlDbType.VarChar).Value = cmFuncionario.nm_Funcio;
            cmd.Parameters.Add("@login_Funcio", MySqlDbType.VarChar).Value = cmFuncionario.login_Funcio;
            cmd.Parameters.Add("@senha_Funcio", MySqlDbType.VarChar).Value = cmFuncionario.senha_Funcio;
            cmd.Parameters.Add("@codTipoUsuario", MySqlDbType.VarChar).Value = cmFuncionario.codTipoUsuario;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
        public DataTable consultaFuncionario()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Funcionario", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable cliente = new DataTable();
            da.Fill(cliente);
            con.MyDesconectarBD();
            return cliente;
        }

        public void TestarUsuario(modelFuncionario user)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbl_Funcionario where login_Funcio = @login_Funcio and senha_Funcio = @senha_Funcio ", con.MyConectarBD());

            cmd.Parameters.Add("@login_Funcio", MySqlDbType.VarChar).Value = user.login_Funcio;
            cmd.Parameters.Add("@senha_Funcio", MySqlDbType.VarChar).Value = user.senha_Funcio;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.login_Funcio = Convert.ToString(leitor["login_Funcio"]);
                    user.senha_Funcio = Convert.ToString(leitor["senha_Funcio"]);
                    user.cd_Funcio = Convert.ToString(leitor["cd_Funcio"]);
                }
            }

            else
            {
                user.login_Funcio = null;
                user.senha_Funcio = null;
            }

            con.MyDesconectarBD();
        }
        private List<modelFuncionario> Listar(MySqlDataReader retorno)
        {
            var fun = new List<modelFuncionario>();

            while (retorno.Read())
            {
                var tempEnt = new modelFuncionario()
                {
                    cd_Funcio = retorno["cd_Funcio"].ToString(),
                    codTipoUsuario = retorno["codTipoUsuario"].ToString(),
                    nm_Funcio = retorno["nm_Funcio"].ToString(),
                    login_Funcio = retorno["login_Funcio"].ToString()                 

                };
                fun.Add(tempEnt);
            }

            retorno.Close();
            return fun;
        }

        public List<modelFuncionario> Listar()
        {
            string strQuery = "select * from tbl_Funcionario;";
            MySqlDataReader retorno = con.RetornaComando(strQuery);
            return Listar(retorno);
        }

        public bool Delete(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbl_Funcionario where cd_Funcio=@id", con.MyConectarBD());

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