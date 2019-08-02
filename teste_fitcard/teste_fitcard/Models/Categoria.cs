using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace teste_fitcard.Models
{
    public class Categoria
    {
        public Int32 idCategoria { get; set; }
        public Int32 id_Categoria { get; set; }
        public String nomeCategoria { get; set; }

        /************************************************ Lista Categorias  ************************************************/
        public List<Categoria> ListarCategoria()
        {
            List<Categoria> Lista = new List<Categoria>();
            try
            {
                SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
                Conexao.Open();

                SqlCommand Comando = new SqlCommand();
                Comando.Connection = Conexao;


                Comando.CommandText = "SELECT idCategoria, nomeCategoria FROM Categoria";
                SqlDataReader Leitor = Comando.ExecuteReader();

                while (Leitor.Read())
                {
                    Categoria C = new Categoria();
                    C.idCategoria = Convert.ToInt32(Leitor["idCategoria"].ToString());
                    C.nomeCategoria = Leitor["nomeCategoria"].ToString();
                    Lista.Add(C);
                }

                Conexao.Close();
                return Lista;
            }
            catch (Exception e)
            {
                return Lista;
            }

        }

        /************************************************ Inserir Categoria no estabelecimento  ************************************************/
        public Boolean InserirCategoria(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "INSERT INTO Estabelecimento_Categoria VALUES(@id_Estabelecimento, @id_Categoria);";
            Comando.Parameters.AddWithValue("@id_Estabelecimento", ID);
            Comando.Parameters.AddWithValue("@id_categoria", this.id_Categoria);

            Int32 resultado = Comando.ExecuteNonQuery();
            Conexao.Close();

            return (resultado > 0) ? true : false;
        }

        /************************************************ Alterar Categoria do estabelecimento  ************************************************/
        public Boolean AlterarCategoria(int ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "UPDATE Estabelecimento_Categoria SET id_Categoria = @id_Categoria WHERE id_Estabelecimento = @id_Estabelecimento;";
            Comando.Parameters.AddWithValue("@id_Categoria", this.id_Categoria);
            Comando.Parameters.AddWithValue("@id_Estabelecimento", ID);

            Int32 ResultadoAlterCategoria = Comando.ExecuteNonQuery();
            Conexao.Close();

            return (ResultadoAlterCategoria > 0) ? true : false;
        }

        /************************************************ Excluir Categoria do estabelecimento  ************************************************/
        public static Boolean ExcluirCategoria(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "DELETE FROM Estabelecimento_Categoria WHERE id_Estabelecimento = @id_Estabelecimento;";
            Comando.Parameters.AddWithValue("@id_Estabelecimento", ID);

            Int32 Resultado = Comando.ExecuteNonQuery();
            Conexao.Close();

            return (Resultado > 0) ? true : false;
        }
    }
}
