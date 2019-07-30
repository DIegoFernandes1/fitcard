using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace teste_fitcard.Models
{
    public class ViewModel
    {
        public String nomeCategoria { get; set; }
        public Int32 idCategoria { get; set; }
        public String Agencia { get; set; }
        public String Conta { get; set; }
        public String Logradouro { get; set; }
        public String Municipio { get; set; }
        public Int32 idCidade { get; set; }
        public Int32 idEndereco { get; set; }
        public Int32 idEstabelecimento { get; set; }
        public String razaoSocial { get; set; }
        public String nomeFantasia { get; set; }
        public String CNPJ { get; set; }
        public DateTime dataCadastro { get; set; }
        public String nomeStatus { get; set; }
        public Int32 idStatus { get; set; }
        public String UF { get; set; }
        public Int32 idUF { get; set; }
        public String Sigla { get; set; }

        /************************************************ Detalhes Estabelecimento  ************************************************/
        public ViewModel (Int32 ID)
        {
            SqlConnection Conexao = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            Conexao.Open();

            SqlCommand Comando = new SqlCommand();
            Comando.Connection = Conexao;

            Comando.CommandText = "SELECT E.idEstabelecimento, E.razaoSocial, E.nomeFantasia, E.CNPJ, E.datacadastro, S.nomeStatus, S.idStatus, EN.Logradouro, EN.idEndereco,"
                                    + " C.Municipio, C.idCidade, uf.UF, uf.Sigla, uf.idUF, CA.nomeCategoria, CA.IdCategoria, CO.Conta, CO.Agencia"
                                    + " FROM  Estabelecimento E"
                                    + " JOIN Status S ON E.id_Status = S.idStatus"
                                    + " JOIN Endereco EN ON E.id_Endereco = EN.idEndereco"
                                    + " JOIN Cidade C ON EN.id_cidade = C.idCidade"
                                    + " JOIN UF uf ON C.id_UF = uf.idUF"
                                    + " JOIN Estabelecimento_Categoria EC ON E.idEstabelecimento = EC.id_Estabelecimento"
                                    + " JOIN Categoria CA ON CA.idCategoria = EC.id_Categoria"
                                    + " JOIN Conta CO ON E.idEstabelecimento = CO.id_Estabelecimento"
                                    + " WHERE idEstabelecimento = @idEstabelecimento";

            Comando.Parameters.AddWithValue("@idEstabelecimento", ID);
            SqlDataReader Leitor = Comando.ExecuteReader();
            Leitor.Read();

            this.idEstabelecimento = Convert.ToInt32(Leitor["idEstabelecimento"].ToString());
            this.razaoSocial = Leitor["razaoSocial"].ToString();
            this.nomeFantasia = Leitor["nomeFantasia"].ToString();
            this.CNPJ = Leitor["CNPJ"].ToString();
            this.dataCadastro = Convert.ToDateTime(Leitor["datacadastro"].ToString());
            this.nomeStatus = Leitor["nomeStatus"].ToString();
            this.idStatus = Convert.ToInt32(Leitor["idStatus"].ToString());
            this.Logradouro = Leitor["Logradouro"].ToString();
            this.Municipio = Leitor["Municipio"].ToString();
            this.idEndereco = Convert.ToInt32(Leitor["idEndereco"].ToString());
            this.idCidade = Convert.ToInt32(Leitor["idcidade"].ToString());
            this.UF = Leitor["UF"].ToString();
            this.idUF = Convert.ToInt32(Leitor["idUF"].ToString());
            this.Sigla = Leitor["Sigla"].ToString();
            this.nomeCategoria = Leitor["nomeCategoria"].ToString();
            this.idCategoria = Convert.ToInt32(Leitor["IdCategoria"].ToString());
            this.Conta = Leitor["Conta"].ToString();
            this.Agencia = Leitor["Agencia"].ToString();
            
            Conexao.Close();
        }
    }
}