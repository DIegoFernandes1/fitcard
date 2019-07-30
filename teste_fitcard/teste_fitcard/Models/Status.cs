using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;

namespace teste_fitcard.Models
{
    public class Status
    {
        public Int32 idStatus { get; set; }
        public String nomeStatus { get; set; }

        public List<Status> ListaStatus()
        {
            List<Status> Lista = new List<Status>();
            try
            {
                SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
                Conexao.Open();

                SqlCommand Comando = new SqlCommand();
                Comando.Connection = Conexao;


                Comando.CommandText = "SELECT idStatus, nomeStatus FROM Status";
                SqlDataReader Leitor = Comando.ExecuteReader();

                while (Leitor.Read())
                {
                    Status S = new Status();
                    S.idStatus = Convert.ToInt32(Leitor["idStatus"].ToString());
                    S.nomeStatus = Leitor["nomeStatus"].ToString();
                    Lista.Add(S);
                }

                Conexao.Close();
                return Lista;
            }
            catch (SqlException erro)
            {
                return Lista;
            }
        }
    }
}