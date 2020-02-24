using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.DAL.Entities; //importando
using Projeto.DAL.Repositories; //importando
using Projeto.Presentation.Models; //importando
using AutoMapper; //importando
using System.Text;
using Projeto.Presentation.Reports;

namespace Projeto.Presentation.Controllers
{
    public class EstoqueController : Controller
    {
        // GET: Estoque
        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(EstoqueCadastroModel model)
        {
            //verificar se os campos da model passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    Estoque estoque = Mapper.Map<Estoque>(model);

                    EstoqueRepository repository = new EstoqueRepository();
                    repository.Insert(estoque);

                    TempData["Mensagem"] = $"Estoque {estoque.Nome}, cadastrado com sucesso.";
                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }
        
        public ActionResult Consulta()
        {
            List<EstoqueConsultaModel> model = new List<EstoqueConsultaModel>();

            try
            {
                EstoqueRepository repository = new EstoqueRepository();
                model = Mapper.Map<List<EstoqueConsultaModel>>(repository.SelectAll());
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            //enviando a model para a página
            return View(model);
        }

        public ActionResult Exclusao(int id)
        {
            try
            {
                EstoqueRepository repository = new EstoqueRepository();

                if (repository.CountProdutos(id)==0)
                {
                    repository.Delete(id);
                    TempData["Mensagem"] = "Estoque excluído com sucesso";
                }
                else
                {
                    TempData["Mensagem"] = "Não é possível excluir o estoque";
                }

            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View();
        }

        public ActionResult Edicao(int id)
        {
            EstoqueEdicaoModel model = new EstoqueEdicaoModel();

            try
            {
                EstoqueRepository repository = new EstoqueRepository();
                Estoque estoque = repository.SelectById(id);

                model = Mapper.Map<EstoqueEdicaoModel>(estoque);

            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }


            return View(model);

        }

        [HttpPost]
        public ActionResult Edicao(EstoqueEdicaoModel model)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    Estoque estoque = Mapper.Map<Estoque>(model);
                    EstoqueRepository repository = new EstoqueRepository();
                    repository.Update(estoque);

                    TempData["Mensagem"] = $"Estoque {estoque.Nome}, atualizado com sucesso";
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }

            }

            return View();
        }

        public void Relatorio()
        {
            try
            {
                EstoqueRepository repository = new EstoqueRepository();
                List<Estoque> lista = repository.SelectAll();

                StringBuilder html = new StringBuilder();
                html.Append("<2>Relatório de Estoque</2>");

                html.Append($"<p>Documento gerado em: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}</p>");
                html.Append("<br/>");

                html.Append("<table border='1' width='100%'>");

                html.Append("<tr>");
                html.Append("<td>Código do Estoque</td>");
                html.Append("<td>Nome do Estoque</td>");
                html.Append("</tr>");

                foreach (var estoque in lista)
                {
                    html.Append("<tr>");
                    html.Append($"<td>{estoque.IdEstoque}</td>");
                    html.Append($"<td>{estoque.Nome}</td>");
                    html.Append("</tr>");
                }

                html.Append("<tr>");
                html.Append($"<td colspan='2'>Quantidade de registros: {lista.Count}</td>");
                html.Append("</tr>");

                html.Append("</table>");

                //fazendo a conversão do conteudo HTML para PDF
                byte[] pdf = ReportsUtil.ConvertToPdf(html.ToString());

                //DOWNLOAD..
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=estoques.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(pdf);
                Response.End();

            }
            catch(Exception e)
            {
                TempData["Mensage,"] = e.Message;
            }
        }

    }
}