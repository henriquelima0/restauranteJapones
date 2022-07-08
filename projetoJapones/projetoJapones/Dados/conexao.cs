using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projetoJapones.Dados
{
    public class conexao 
    {
        MySqlConnection cn = new MySqlConnection("Server=localhost; DataBase=bd_japones; User=root;pwd=root123");
        public static string msg;

        public MySqlConnection MyConectarBD()
        {

            try
            {
                cn.Open();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }

        public MySqlConnection MyDesconectarBD()
        {

            try
            {
                cn.Close();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }

        public MySqlDataReader RetornaComando(string strQuery)
        {
            cn.Open();
            var vComando = new MySqlCommand(strQuery, cn);
            return vComando.ExecuteReader();
        }

        public void ExecutaComando(string strQuery) /////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! INTRUSO
        {
            cn.Open();
            var vComando = new MySqlCommand(strQuery, cn);
            vComando.ExecuteNonQuery();
            cn.Close();
        }
    }
}