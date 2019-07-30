using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace teste_fitcard.Models
{
    public class Endereco
    {
        public Int32 idEndereco { get; set; }
        public Int32 id_cidade { get; set; }
        public String Logradouro { get; set; }
        public String Numero { get; set; }

        public Int32 idCidade { get; set; }
        public Int32 id_UF { get; set; }
        public String Municipio { get; set; }

        /************************************************ Listar Cidades  ************************************************/
        public List<Endereco> ListarCidade (Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "SELECT * FROM Cidade WHERE id_UF = @id_UF;";
            Comando.Parameters.AddWithValue("@id_UF", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();
            List<Endereco> Cidades = new List<Endereco>();

            while (Leitor.Read())
            {
                Endereco E = new Endereco();
                E.idCidade = Convert.ToInt32(Leitor["idCidade"].ToString());
                E.Municipio = Leitor["Municipio"].ToString();
                Cidades.Add(E);
            }

            Conexao.Close();
            return Cidades;
        }

        /************************************************ Inserir Endereco  ************************************************/
        public Int32 InserirEndereco(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "INSERT INTO Endereco OUTPUT INSERTED.idEndereco VALUES(@id_cidade, @Logradouro)";
            Comando.Parameters.AddWithValue("@id_cidade", ID);
            Comando.Parameters.AddWithValue("@Logradouro", this.Logradouro);

            SqlDataReader Leitor = Comando.ExecuteReader();
            Leitor.Read();

            Int32 resultadoEndereco = Leitor.GetInt32(0);
            Conexao.Close();

            return (resultadoEndereco > 0) ? resultadoEndereco : 0;
        }

        /************************************************ Altera Endereco  ************************************************/
        public Boolean AlteraEndereco(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "UPDATE Endereco SET Logradouro = @Logradouro, id_Cidade = @id_Cidade WHERE idEndereco = @idEndereco";
            Comando.Parameters.AddWithValue("@Logradouro", this.Logradouro);
            Comando.Parameters.AddWithValue("@id_Cidade", this.idCidade);
            Comando.Parameters.AddWithValue("@idEndereco", ID);

            Int32 ResultadoEndereco = Comando.ExecuteNonQuery();
            Conexao.Close();
            return (ResultadoEndereco > 0) ? true : false;
        }

        /************************************************ Excluir Endereco  ************************************************/
        public static Boolean ExcluirEndereco(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "DELETE FROM Endereco OUTPUT DELETED.Id_Cidade WHERE idEndereco = @idEndereco;";
            Comando.Parameters.AddWithValue("@idEndereco", ID);

            //SqlDataReader Leitor = Comando.ExecuteReader();
            //Leitor.Read();

            //Int32 resultadoEndereco = Convert.ToInt32(Leitor["Id_Cidade"].ToString());
            //Leitor.Close();

            //Comando.CommandText = "DELETE FROM Cidade WHERE IdCidade = @IdCidade;";
            //Comando.Parameters.AddWithValue("@idCidade", resultadoEndereco);

            //Int32 ResultadoCidade = Comando.ExecuteNonQuery();
            Int32 ResultadoEndereco = Comando.ExecuteNonQuery();
            Conexao.Close();

            return (ResultadoEndereco > 0) ? true : false;
        }
    }
}