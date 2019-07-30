using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace teste_fitcard.Models
{
    public class Conta
    {
        public Int32 idConta { get; set; }
        public Int32 id_Estabelecimento { get; set; }
        public String Agencia { get; set; }
        public String Conta_ { get; set; }

        /************************************************ Inserir Conta ************************************************/
        public Boolean InserirConta(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "INSERT INTO Conta VALUES(@id_Estabelecimento, @Conta, @Agencia)";
            Comando.Parameters.AddWithValue("@id_Estabelecimento", ID);
            Comando.Parameters.AddWithValue("@Conta", this.Conta_);
            Comando.Parameters.AddWithValue("@Agencia", this.Agencia);

            Int32 resultado = Comando.ExecuteNonQuery();
            Conexao.Close();

            return (resultado > 0) ? true : false;
        }

        /************************************************ Alterar Conta ************************************************/
        public Boolean AlterarConta(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "UPDATE Conta SET Conta = @Conta, Agencia = @Agencia WHERE id_Estabelecimento = @id_Estabelecimento;";
            Comando.Parameters.AddWithValue("@Conta",this.Conta_);
            Comando.Parameters.AddWithValue("@Agencia",this.Agencia);
            Comando.Parameters.AddWithValue("@id_Estabelecimento",ID);

            Int32 ResultadoConta = Comando.ExecuteNonQuery();
            Conexao.Close();

            return (ResultadoConta > 0) ? true : false;
        }

        /************************************************ Excluir Conta ************************************************/
        public static Boolean ExcluirConta(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "DELETE FROM Conta WHERE id_Estabelecimento = @id_Estabelecimento;";
            Comando.Parameters.AddWithValue("@id_Estabelecimento", ID);

            Int32 ResultadoConta = Comando.ExecuteNonQuery();
            Conexao.Close();

            return (ResultadoConta > 0) ? true : false;
        }
    }
}