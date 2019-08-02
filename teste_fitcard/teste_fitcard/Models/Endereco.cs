using System;
using System.Configuration;
using System.Data.SqlClient;

namespace teste_fitcard.Models
{
    public class Endereco
    {
        public Int32 idEndereco { get; set; }
        public String Logradouro { get; set; }

        public Cidade Cidade { get; set; }

            public Endereco()
        {
            Cidade = new Cidade();
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
            Comando.Parameters.AddWithValue("@id_Cidade", this.Cidade.idCidade);
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
           
            Int32 ResultadoEndereco = Comando.ExecuteNonQuery();
            Conexao.Close();

            return (ResultadoEndereco > 0) ? true : false;
        }
    }
}