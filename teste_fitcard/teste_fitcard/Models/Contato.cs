using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace teste_fitcard.Models
{
    public class Contato
    {
        public Int32 id_Estabelecimento { get; set; }
        public Int32 idContato { get; set; }
        public String Email { get; set; }
        public String DDD { get; set; }
        public String Numero { get; set; }

        public Contato() { }

        public Contato(Int32 idContato)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "SELECT idContato, id_estabelecimento, Email, DDD, Numero FROM Contato WHERE idContato = @idContato;";
            Comando.Parameters.AddWithValue("@idContato", idContato);

            SqlDataReader Leitor = Comando.ExecuteReader();
            Leitor.Read();

            this.idContato = Convert.ToInt32(Leitor["idContato"].ToString());
            this.id_Estabelecimento = Convert.ToInt32(Leitor["id_estabelecimento"].ToString());
            this.Email = Leitor["Email"].ToString();
            this.DDD = Leitor["DDD"].ToString();
            this.Numero = Leitor["Numero"].ToString();

            Conexao.Close();
        }

        /************************************************ Inserir contato  ************************************************/
        public Boolean InserirContato(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "INSERT INTO Contato VALUES(@id_Estabelecimento, @Email, @DDD, @Numero);";
            Comando.Parameters.AddWithValue("@id_Estabelecimento", ID);
            Comando.Parameters.AddWithValue("@Email", this.Email);
            Comando.Parameters.AddWithValue("@DDD", this.DDD);
            Comando.Parameters.AddWithValue("@Numero", this.Numero);

            Int32 resultado = Comando.ExecuteNonQuery();

            Conexao.Close();

            return (resultado > 0) ? true : false;

        }

        /************************************************ Alterar contato  ************************************************/
        public Boolean AlterarContato(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "UPDATE Contato SET Email = @Email, DDD = @DDD, Numero = @Numero WHERE idContato = @idContato;";
            Comando.Parameters.AddWithValue("@Email", this.Email);
            Comando.Parameters.AddWithValue("@DDD",this.DDD);
            Comando.Parameters.AddWithValue("@Numero",this.Numero);
            Comando.Parameters.AddWithValue("@idContato", ID);

            Int32 ResultadoContato = Comando.ExecuteNonQuery();
            Conexao.Close();
            return (ResultadoContato > 0) ? true : false;
        }

        /************************************************ Remover contato  ************************************************/
        public Int32? RemoverContato(Int32? idContato, Int32? id_Estabelecimento)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "DELETE FROM Contato OUTPUT DELETED.id_Estabelecimento WHERE idContato = @idContato OR id_Estabelecimento = @id_estabelecimento";
            Comando.Parameters.AddWithValue("@idContato", idContato);
            Comando.Parameters.AddWithValue("@id_estabelecimento", id_Estabelecimento);

            SqlDataReader Leitor = Comando.ExecuteReader();
            Leitor.Read();

            Int32? Resultado = Convert.ToInt32(Leitor["id_Estabelecimento"].ToString());
            Conexao.Close();

            return (Resultado > 0) ? Resultado : 0;
        }

        /************************************************ Selecionar contato  ************************************************/
        public List<Contato> ListarContatos(Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "SELECT idContato, Email,DDD,Numero FROM Contato WHERE id_Estabelecimento = @id_Estabelecimento";
            Comando.Parameters.AddWithValue("@id_Estabelecimento", ID);

            SqlDataReader Leitor = Comando.ExecuteReader();
            List<Contato> Contatos = new List<Contato>();

            while (Leitor.Read())
            {
                Contato C = new Contato();
                C.idContato = Convert.ToInt32(Leitor["idContato"].ToString());
                C.Email = Leitor["Email"].ToString();
                C.DDD = Leitor["DDD"].ToString();
                C.Numero = Leitor["Numero"].ToString();
                Contatos.Add(C);
            }

            Conexao.Close();
            return Contatos;
        }
    }
}