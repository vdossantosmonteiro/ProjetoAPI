using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.DAL.Entities;
using Projeto.DAL.Repositories;
using Projeto.Presentation.Models;
using AutoMapper;

namespace Projeto.Presentation.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Cadastro()
        {            
            return View(CarregarDadosCadastro());
        }

        [HttpPost] //recebe requisições do tipo POST
        public ActionResult Cadastro(ProdutoCadastroModel model)
        {
            //verificar se os campos da model passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    Produto produto = Mapper.Map<Produto>(model);

                    ProdutoRepository repository = new ProdutoRepository();
                    repository.Insert(produto);

                    TempData["Mensagem"] = $"Produto {produto.Nome}, cadastrado com sucesso.";
                    ModelState.Clear();
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View(CarregarDadosCadastro());
        }

        public ActionResult Consulta()
        {
            List<ProdutoConsultaModel> model = new List<ProdutoConsultaModel>();

            try
            {
                ProdutoRepository repository = new ProdutoRepository();
                model = Mapper.Map<List<ProdutoConsultaModel>>(repository.SelectAll());
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            //enviando os dados da model para a página..
            return View(model);
        }

        [HttpPost]
        public ActionResult Consulta(decimal precoMin, decimal precoMax)
        {
            List<ProdutoConsultaModel> model = new List<ProdutoConsultaModel>();

            try
            {
                ProdutoRepository repository = new ProdutoRepository();
                model = Mapper.Map<List<ProdutoConsultaModel>>
                        (repository.SelectAll(precoMin, precoMax));
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View(model);
        }

        public ActionResult Exclusao(int id)
        {
            try
            {
                ProdutoRepository repository = new ProdutoRepository();
                repository.Delete(id);

                TempData["Mensagem"] = "Produto excluído com sucesso";
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }


            return View();

        }

        public ActionResult Edicao(int id)
        {
            ProdutoEdicaoModel model = new ProdutoEdicaoModel();

            try
            {
                ProdutoRepository repository = new ProdutoRepository();
                Produto produto = repository.SelectById(id);

                model = Mapper.Map<ProdutoEdicaoModel>(produto);

                EstoqueRepository estoquerepository = new EstoqueRepository();
                model.Estoques = Mapper.Map<List<SelectListItem>>(estoquerepository.SelectAll());
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult Edicao(ProdutoEdicaoModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Produto produto = Mapper.Map<Produto>(model);
                    ProdutoRepository repository = new ProdutoRepository();
                    repository.Update(produto);

                    TempData["Mensagem"] = $"Produto {produto.Nome} atualizado";
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }

                try
                {
                    EstoqueRepository repository = new EstoqueRepository();
                    model.Estoques = Mapper.Map<List<SelectListItem>>(repository.SelectAll());
                }
                catch(Exception e)
                {
                    TempData["Mensagem"] = e.Message;                
                }


            }
            return View(model);
        }




        private ProdutoCadastroModel CarregarDadosCadastro()
        {
            //classe de modelo utilizada na página
            ProdutoCadastroModel model = new ProdutoCadastroModel();

            try
            {
                EstoqueRepository repository = new EstoqueRepository();
                model.Estoques = Mapper.Map<List<SelectListItem>>(repository.SelectAll());
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return model;
        }
    }
}