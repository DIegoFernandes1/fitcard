using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teste_fitcard.Models;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace teste_fitcard.Controllers
{
    public class EstabelecimentoController : Controller
    {
        /************************************************ Cadastrar Estabelecimento  ************************************************/
        public ActionResult CadastrarEstabelecimento()
        {
            DateTime date = DateTime.UtcNow;
            ViewBag.Data = date;

            Categoria C = new Categoria();
            ViewBag.ListaCategoria = C.ListarCategoria();

            Status S = new Status();
            ViewBag.ListaStatus = S.ListaStatus();

            UF UF = new UF();
            ViewBag.ListaEstados = UF.ListaEstados();

            /********** BLOCO CADASTRAR **********/
            if (Request.HttpMethod.Equals("POST"))
            {
                try
                {
                    Estabelecimento ES = new Estabelecimento();
                    ES.razaoSocial = Request.Form["razaoSocial"].ToString();
                    ES.nomeFantasia = Request.Form["nomeFantasia"].ToString();
                    ES.CNPJ = Request.Form["CNPJ"].ToString();
                    ES.Status.idStatus = Convert.ToInt32(Request.Form["status"].ToString());
                    ES.dataCadastro = date;

                    Contato CON = new Contato();
                    CON.DDD = Request.Form["ddd"].ToString();
                    CON.Numero = Request.Form["telefone"].ToString();
                    CON.Email = Request.Form["email"].ToString();

                    Endereco EN = new Endereco();
                    EN.Cidade.idCidade = Convert.ToInt32(Request.Form["cidade"].ToString());
                    EN.Cidade.id_UF = Convert.ToInt32(Request.Form["UF"].ToString());
                    EN.Logradouro = Request.Form["logradouro"].ToString();

                    Conta CO = new Conta();
                    CO.Conta_ = Request.Form["conta"].ToString();
                    CO.Agencia = Request.Form["agencia"].ToString();

                    C.id_Categoria = Convert.ToInt32(Request.Form["categoria"].ToString());

                    /*** VERIFICA EMAIL SE FOI PREENCHIDO OU NÃO ***/
                    if (!CON.Email.Equals(""))
                    {
                        /*** VERIFICA EMAIL SE É VALIDO ***/
                        if (isEmail(CON.Email))
                        {
                            /*** CADASTRA DE FATO***/
                            try
                            {
                                Int32 idEndereco = EN.InserirEndereco(EN.Cidade.idCidade);

                                if (idEndereco > 0)
                                {
                                    Int32 idEstabelecimento = ES.CadastrarEstabelecimento(idEndereco);
                                    C.InserirCategoria(idEstabelecimento);
                                    CON.InserirContato(idEstabelecimento);
                                    CO.InserirConta(idEstabelecimento);
                                    ViewBag.Sucesso = true;
                                    ViewBag.Mensagem = "Estabelecimento cadastrado com sucesso.";
                                }
                            }
                            catch
                            {
                                ViewBag.Mensagem = "Erro durante o cadastro.";
                                ViewBag.Erro = true;
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Alerta = true;
                            ViewBag.Mensagem = "E-mail inserido é inválido, digite um e-mail válido.";
                        }
                    }
                    else
                    {
                        try
                        {
                            Int32 idEndereco = EN.InserirEndereco(EN.Cidade.idCidade);

                            if (idEndereco > 0)
                            {
                                Int32 idEstabelecimento = ES.CadastrarEstabelecimento(idEndereco);
                                C.InserirCategoria(idEstabelecimento);
                                CON.InserirContato(idEstabelecimento);
                                CO.InserirConta(idEstabelecimento);
                                ViewBag.Sucesso = true;
                                ViewBag.Mensagem = "Estabelecimento cadastrado com sucesso.";
                            }
                        }
                        catch
                        {
                            ViewBag.Mensagem = "Erro durante o cadastro.";
                            ViewBag.Erro = true;
                            return View();
                        }
                    }
                }
                catch
                {
                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique se todos os campos Obrigatórios foram preenchidos.";
                    return View();
                }
            }
            return View();
        }

        /************************************************ Excluir Estabelecimento  ************************************************/
        public ActionResult ExcluirEstabelecimento(Int32 ID)
        {
            try
            {
                Conta.ExcluirConta(ID);
                Contato C = new Contato();
                C.RemoverContato(0, ID);
                Categoria.ExcluirCategoria(ID);
                Endereco.ExcluirEndereco(Estabelecimento.ExcluirEstabelecimento(ID));
                return RedirectToAction("ListarEstabelecimentos", "Estabelecimento");
            }
            catch
            {
                return RedirectToAction("ListarEstabelecimentos", "Estabelecimento");
            }

        }

        /************************************************ Pesquisar Estabelecimento  ************************************************/
        public String Pesquisar(String ID)
        {
            Estabelecimento E = new Estabelecimento();
            ViewBag.Pesquisa = true;
            return JsonConvert.SerializeObject(E.Pesquisa(ID));
        }

        /************************************************ Listar Cidades  ************************************************/
        public String SelecionaCidades(Int32 ID)
        {
            Cidade C = new Cidade();
            return JsonConvert.SerializeObject(C.ListarCidade(ID));
        }

        /************************************************ Listar Estabelecimento  ************************************************/
        public ActionResult ListarEstabelecimentos()
        {
            try
            {
                Estabelecimento E = new Estabelecimento();
                ViewBag.ListaEstabelecimentos = E.ListarEstabelecimentos();
                ViewBag.Pesquisa = false;
                return View();
            }
            catch
            {
                ViewBag.Erro = true;
                ViewBag.Mensagem = "Erro ao tentar listar estabelecimentos.";
                return View();
            }
        }

        /************************************************ Detalhes Estabelecimento  ************************************************/
        public ActionResult DetalhesEstabelecimento(Int32 ID)
        {
            try
            {
                Categoria CA = new Categoria();
                ViewBag.ListaCategoria = CA.ListarCategoria();

                Status S = new Status();
                ViewBag.ListaStatus = S.ListaStatus();

                UF UF = new UF();
                ViewBag.ListaEstados = UF.ListaEstados();

                ViewBag.Detalhes = new ViewModel(ID);
                
                Contato C = new Contato();
                ViewBag.Contatos = C.ListarContatos(ID);
                return View();
        }
            catch
            {
                return View();
    }
}

        /************************************************ Alterar Estabelecimento  ************************************************/
        public ActionResult AlterarEstabelecimento(Int32 ID)
        {
            if (Request.HttpMethod.Equals("POST"))
            {
                try
                {
                    Estabelecimento ES = new Estabelecimento();
                    ES.razaoSocial = Request.Form["razaoSocial"].ToString();
                    ES.nomeFantasia = Request.Form["nomeFantasia"].ToString();
                    ES.CNPJ = Request.Form["CNPJ"].ToString();
                    ES.Status.idStatus = Convert.ToInt32(Request.Form["status"].ToString());

                    Endereco EN = new Endereco();
                    EN.Cidade.id_UF = Convert.ToInt32(Request.Form["UF"].ToString());
                    EN.idEndereco = Convert.ToInt32(Request.Form["idEndereco"].ToString());
                    EN.Cidade.idCidade = Convert.ToInt32(Request.Form["idCidade"].ToString());
                    EN.Logradouro = Request.Form["logradouro"].ToString();

                    Conta CO = new Conta();
                    CO.Conta_ = Request.Form["conta"].ToString();
                    CO.Agencia = Request.Form["agencia"].ToString();

                    Categoria CA = new Categoria();
                    CA.id_Categoria = Convert.ToInt32(Request.Form["categoria"].ToString());
                    
                    /***CADASTRA DE FATO***/
                    try
                    {
                        EN.AlteraEndereco(EN.idEndereco);
                        ES.AlterarEstabelecimento(ID);
                        CA.AlterarCategoria(ID);
                        CO.AlterarConta(ID);
                        ViewBag.Sucesso = true;
                        ViewBag.Mensagem = "Informações alteradas com sucesso.";
                    }
                    catch
                    {
                        ViewBag.Mensagem = "Erro durante o cadastro.";
                        ViewBag.Erro = true;
                        return View();
                    }
                }
                catch
                {
                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique se todos os campos Obrigatórios foram preenchidos.";
                    return View();
                }

            }
            return RedirectToAction("DetalhesEstabelecimento", "Estabelecimento", new { @id = ID });
        }

        /************************************************ Verifica Email é valido  ************************************************/
        public static Boolean isEmail(string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            return (rg.IsMatch(email)) ? true : false;
        }
    }
}