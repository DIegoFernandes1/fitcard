using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace teste_fitcard.Models
{
    public class UF
    {
        public Int32 idUF { get; set; }
        public String UF_ { get; set; }
        public String Sigla { get; set; }

        /************************************************ Lista estados  ************************************************/
        public List<UF> ListaEstados()
        {
            List<UF> Lista = new List<UF>();
            try
            {
                SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
                Conexao.Open();

                SqlCommand Comando = new SqlCommand();
                Comando.Connection = Conexao;

                Comando.CommandText = "SELECT DISTINCT idUF,UF,Sigla FROM UF";
                SqlDataReader Leitor = Comando.ExecuteReader();

                while (Leitor.Read())
                {
                    UF UF = new UF();
                    UF.idUF = Convert.ToInt32(Leitor["idUF"].ToString());
                    UF.UF_ = Leitor["UF"].ToString();
                    UF.Sigla = Leitor["Sigla"].ToString();
                    Lista.Add(UF);
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