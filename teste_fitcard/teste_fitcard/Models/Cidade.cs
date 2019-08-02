using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace teste_fitcard.Models
{
    public class Cidade
    {
        public int idCidade { get; set; }
        public int id_UF { get; set; }
        public string Municipio { get; set; }

        /************************************************ Listar Cidades  ************************************************/
        public List<Cidade> ListarCidade(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "SELECT * FROM Cidade WHERE id_UF = @id_UF;";
            Comando.Parameters.AddWithValue("@id_UF", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();
            List<Cidade> Cidades = new List<Cidade>();

            while (Leitor.Read())
            {
                Cidade C = new Cidade();
                C.idCidade = Convert.ToInt32(Leitor["idCidade"].ToString());
                C.Municipio = Leitor["Municipio"].ToString();
                Cidades.Add(C);
            }

            Conexao.Close();
            return Cidades;
        }
    }
}