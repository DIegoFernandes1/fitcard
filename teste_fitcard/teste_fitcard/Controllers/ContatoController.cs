using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teste_fitcard.Models;
using teste_fitcard.Controllers;


namespace teste_fitcard.Controllers
{
    public class ContatoController : Controller
    {

        public ActionResult CadastrarContato()
        {
            if (Request.HttpMethod.Equals("POST"))
            {
                Contato C = new Contato();
                C.DDD = Request.Form["ddd"].ToString();
                C.Numero = Request.Form["telefone"].ToString();
                C.Email = Request.Form["email"].ToString();
                C.Estabelecimento.idEstabelecimento = Convert.ToInt32(Request.Form["id_Estabelecimento"].ToString());

                try
                {
                    if (C.Email != "")
                    {
                        if (EstabelecimentoController.isEmail(C.Email))
                        {
                            C.InserirContato(C.Estabelecimento.idEstabelecimento);
                            return RedirectToAction("DetalhesEstabelecimento", "Estabelecimento", new { @id = C.Estabelecimento.idEstabelecimento});
                        }
                    }
                    else
                    {
                        C.InserirContato(C.Estabelecimento.idEstabelecimento);
                        return RedirectToAction("DetalhesEstabelecimento", "Estabelecimento", new { @id = C.Estabelecimento.idEstabelecimento });
                    }
                }
                catch
                {
                    ViewBag.Erro = true;
                    ViewBag.Mensagem = "Erro ao alterar contato.";
                }
            }
            return View();
        }

        /************************************************ Alterar Contato  ************************************************/
        public ActionResult AlterarContato(Int32 ID)
        {
            if (Request.HttpMethod.Equals("POST"))
            {
                Contato C = new Contato();
                C.DDD = Request.Form["ddd"].ToString();
                C.Numero = Request.Form["telefone"].ToString();
                C.Email = Request.Form["email"].ToString();
                C.Estabelecimento.idEstabelecimento = Convert.ToInt32(Request.Form["id_Estabelecimento"].ToString());

                try
                {
                    if (C.Email != "")
                    {
                        if (EstabelecimentoController.isEmail(C.Email))
                        {
                            C.AlterarContato(ID);
                            return RedirectToAction("DetalhesEstabelecimento", "Estabelecimento", new { @id = C.Estabelecimento.idEstabelecimento });
                        }
                    }
                    else
                    {
                        C.AlterarContato(ID);
                        return RedirectToAction("DetalhesEstabelecimento", "Estabelecimento", new { @id = C.Estabelecimento.idEstabelecimento });
                    }
                }
                catch
                {
                    ViewBag.Erro = true;
                    ViewBag.Mensagem = "Erro ao alterar contato.";
                }
            }
            ViewBag.Contato = new Contato(ID);
            return View();
        }

        /************************************************ Remover Contato  ************************************************/
        public ActionResult RemoverContato(Int32 ID)
        {
            Contato C = new Contato();
            Int32 Id_Estabelecimento = C.RemoverContato(ID, 0);
            return RedirectToAction("DetalhesEstabelecimento", "Estabelecimento", new { @id = Id_Estabelecimento });
        }
    }
}