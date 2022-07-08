using MySql.Data.MySqlClient;
using projetoJapones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace projetoJapones.Dados
{
    public class acVendas
    {
        conexao con = new conexao();

        public void inserirVenda(modelVendas cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbVenda values(default, @codCli, @datavenda, @horaVenda , @valorFinal)", con.MyConectarBD());

            cmd.Parameters.Add("@codCli", MySqlDbType.VarChar).Value = cm.UsuarioID;
            cmd.Parameters.Add("@datavenda", MySqlDbType.VarChar).Value = cm.DtVenda;
            cmd.Parameters.Add("@horaVenda", MySqlDbType.VarChar).Value = cm.horaVenda;
            cmd.Parameters.Add("@valorFinal", MySqlDbType.VarChar).Value = cm.ValorTotal;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
        public DataTable sp_CodVenda()
        {
            MySqlCommand cmd = new MySqlCommand("call sp_CodVenda();", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable selecinarUltimaVenda = new DataTable();
            da.Fill(selecinarUltimaVenda);
            con.MyDesconectarBD();
            return selecinarUltimaVenda;
        }

        public DataTable v()
        {
            MySqlCommand cmd = new MySqlCommand("select * from vw_MostraVenda;", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable selecinarUltimaVenda = new DataTable();
            da.Fill(selecinarUltimaVenda);
            con.MyDesconectarBD();
            return selecinarUltimaVenda;
        }

        public DataTable selectVenda()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbVenda ORDER BY codVenda DESC LIMIT 1;", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable selecinarUltimaVenda = new DataTable();
            da.Fill(selecinarUltimaVenda);
            con.MyDesconectarBD();
            return selecinarUltimaVenda;
        }

        public DataTable ConsVenda()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbVenda", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable ConsVenda = new DataTable();
            da.Fill(ConsVenda);
            con.MyDesconectarBD();
            return ConsVenda;
        }


        MySqlDataReader dr;
        public void buscaIdVenda(modelVendas vend)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT codVenda FROM tbVenda ORDER BY codVenda DESC limit 1", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                vend.codVenda = dr[0].ToString();
            }
            con.MyDesconectarBD();
        }
    }
}