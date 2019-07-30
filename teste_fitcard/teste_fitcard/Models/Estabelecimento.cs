using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace teste_fitcard.Models
{
    public class Estabelecimento
    {
        public Int32 idEstabelecimento { get; set; }
        public Int32 id_status { get; set; }
        public Int32 id_endereco { get; set; }
        public String razaoSocial { get; set; }
        public String nomeFantasia { get; set; }
        public String CNPJ { get; set; }
        public DateTime dataCadastro { get; set; }
        public String Status { get; set; }

        /************************************************ Cadastrar Estabelecimento  ************************************************/
        public Int32 CadastrarEstabelecimento(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "INSERT INTO Estabelecimento OUTPUT INSERTED.idEstabelecimento VALUES(@id_Status, @id_Endereco, @razaoSocial, @nomeFantasia, @CNPJ, @dataCadastro)";
            Comando.Parameters.AddWithValue("@id_Status", this.id_status);
            Comando.Parameters.AddWithValue("@id_Endereco", ID);
            Comando.Parameters.AddWithValue("@razaoSocial", this.razaoSocial);
            Comando.Parameters.AddWithValue("@nomeFantasia", this.nomeFantasia);
            Comando.Parameters.AddWithValue("@CNPJ", this.CNPJ);
            Comando.Parameters.AddWithValue("@dataCadastro", this.dataCadastro);

            SqlDataReader Leitor = Comando.ExecuteReader();
            Leitor.Read();
            this.idEstabelecimento = Leitor.GetInt32(0);
            Conexao.Close();

            return (idEstabelecimento > 0) ? idEstabelecimento : 0;
        }

        /************************************************ Alterar Estabelecimento  ************************************************/
        public Boolean AlterarEstabelecimento(int ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "UPDATE Estabelecimento SET id_Status = @id_Status, razaoSocial = @razaoSocial, nomeFantasia = @nomeFantasia, CNPJ = @CNPJ WHERE idEstabelecimento = @idEstabelecimento";
            Comando.Parameters.AddWithValue("@id_Status", this.id_status);
            Comando.Parameters.AddWithValue("@razaoSocial", this.razaoSocial);
            Comando.Parameters.AddWithValue("@nomeFantasia", this.nomeFantasia);
            Comando.Parameters.AddWithValue("@CNPJ", this.CNPJ);
            Comando.Parameters.AddWithValue("@idEstabelecimento", ID);

            Int32 ResultadoEstabelecimento = Comando.ExecuteNonQuery();

            Conexao.Close();
            return (ResultadoEstabelecimento > 0) ? true : false;
        }

        /************************************************ Listar Estabelecimento  ************************************************/
        public List<Estabelecimento> ListarEstabelecimentos()
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "SELECT E.idEstabelecimento, E.razaoSocial, E.nomeFantasia, E.CNPJ, E.datacadastro , S.nomeStatus FROM Estabelecimento E "
                                   + "JOIN Status S ON E.id_status = S.idStatus;";

            List<Estabelecimento> Estabelecimentos = new List<Estabelecimento>();
            SqlDataReader Leitor = Comando.ExecuteReader();

            while (Leitor.Read())
            {
                Estabelecimento E = new Estabelecimento();
                E.idEstabelecimento = Convert.ToInt32(Leitor["idEstabelecimento"].ToString());
                E.razaoSocial = Leitor["razaoSocial"].ToString();
                E.nomeFantasia = Leitor["nomeFantasia"].ToString();
                E.CNPJ = Leitor["CNPJ"].ToString();
                E.dataCadastro = Convert.ToDateTime(Leitor["datacadastro"].ToString());
                E.Status = Leitor["nomeStatus"].ToString();

                Estabelecimentos.Add(E);
            }
            Conexao.Close();
            return Estabelecimentos;
        }

        /************************************************ Excluir Estabelecimento  ************************************************/
        public static Int32 ExcluirEstabelecimento(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "DELETE FROM Estabelecimento OUTPUT DELETED.id_Endereco WHERE idEstabelecimento = @idEstabelecimento;";
            Comando.Parameters.AddWithValue("@idEstabelecimento", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();
            Leitor.Read();

            Int32 ResultadoEstabelecimento = Convert.ToInt32(Leitor["id_Endereco"].ToString());
            Conexao.Close();

            return (ResultadoEstabelecimento > 0) ? ResultadoEstabelecimento : 0;
        }

        /************************************************ Pesquisar Estabelecimento  ************************************************/
        public List<Estabelecimento>Pesquisa(String Pesquisa)
        {
            List<Estabelecimento> Estabelecimentos = new List<Estabelecimento>();
            try
            {
                SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
                Conexao.Open();

                SqlCommand Comando = new SqlCommand();
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Connection = Conexao;

                Comando.CommandText = "Pesquisa";
                Comando.Parameters.AddWithValue("@Pesquisa", Pesquisa);

                SqlDataReader Leitor = Comando.ExecuteReader();

                while (Leitor.Read())
                {
                    Estabelecimento E = new Estabelecimento();
                    E.idEstabelecimento = Convert.ToInt32(Leitor["idEstabelecimento"].ToString());
                    E.razaoSocial = Leitor["razaoSocial"].ToString();
                    E.nomeFantasia = Leitor["nomeFantasia"].ToString();
                    E.CNPJ = Leitor["CNPJ"].ToString();
                    E.dataCadastro = Convert.ToDateTime(Leitor["datacadastro"].ToString());
                    E.Status = Leitor["nomeStatus"].ToString();

                    Estabelecimentos.Add(E);
                }
                Conexao.Close();
                return Estabelecimentos;
            }
            catch
            {
                return Estabelecimentos;
            }
        }
    }
}