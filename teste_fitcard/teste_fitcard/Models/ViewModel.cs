using System;
using System.Configuration;
using System.Data.SqlClient;

namespace teste_fitcard.Models
{
    public class ViewModel
    {
        public Categoria Categoria { get; set; }
        public Conta Conta { get; set; }
        public UF UF { get; set; }
        public Endereco Endereco { get; set; }
        public Cidade Cidade { get; set; }
        public Estabelecimento Estabelecimento { get; set; }
        public Status Status { get; set; }


        /************************************************ Detalhes Estabelecimento  ************************************************/
        public ViewModel(Int32 ID)
        {
            Categoria = new Categoria();
            Conta = new Conta();
            UF = new UF();
            Endereco = new Endereco();
            Cidade = new Cidade();
            Estabelecimento = new Estabelecimento();
            Status = new Status();

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

            this.Estabelecimento.idEstabelecimento = Convert.ToInt32(Leitor["idEstabelecimento"].ToString());
            this.Estabelecimento.razaoSocial = Leitor["razaoSocial"].ToString();
            this.Estabelecimento.nomeFantasia = Leitor["nomeFantasia"].ToString();
            this.Estabelecimento.CNPJ = Leitor["CNPJ"].ToString();
            this.Estabelecimento.dataCadastro = Convert.ToDateTime(Leitor["datacadastro"].ToString());
            this.Status.nomeStatus = Leitor["nomeStatus"].ToString();
            this.Status.idStatus = Convert.ToInt32(Leitor["idStatus"].ToString());
            this.Endereco.Logradouro = Leitor["Logradouro"].ToString();
            this.Cidade.idCidade = Convert.ToInt32(Leitor["idcidade"].ToString());
            this.Cidade.Municipio = Leitor["Municipio"].ToString();
            this.Endereco.idEndereco = Convert.ToInt32(Leitor["idEndereco"].ToString());
            this.UF.UF_ = Leitor["UF"].ToString();
            this.UF.idUF = Convert.ToInt32(Leitor["idUF"].ToString());
            this.UF.Sigla = Leitor["Sigla"].ToString();
            this.Categoria.nomeCategoria = Leitor["nomeCategoria"].ToString();
            this.Categoria.idCategoria = Convert.ToInt32(Leitor["IdCategoria"].ToString());
            this.Conta.Conta_ = Leitor["Conta"].ToString();
            this.Conta.Agencia = Leitor["Agencia"].ToString();

            Conexao.Close();
        }
    }
}