using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper; //importando
using Projeto.DAL.Entities; //importando
using Projeto.Presentation.Models; //importando
using System.Web.Mvc; //importando

namespace Projeto.Presentation.App_Start
{
    //REGRA 1) Herdar Profile
    public class AutoMapperConfig : Profile
    {
        //REGRA 2) Construtor para realizar os mapeamentos
        public AutoMapperConfig()
        {
            //DE -> PARA
            CreateMap<EstoqueCadastroModel, Estoque>();

            //DE -> PARA
            CreateMap<ProdutoCadastroModel, Produto>();

            //DE -> PARA
            CreateMap<Estoque, SelectListItem>()
                .AfterMap((de, para) => para.Value = de.IdEstoque.ToString())
                .AfterMap((de, para) => para.Text = de.Nome);

            //DE -> PARA
            CreateMap<Estoque, EstoqueConsultaModel>();

            //DE -> PARA
            CreateMap<Produto, ProdutoConsultaModel>();

            CreateMap<Estoque, EstoqueEdicaoModel>().ReverseMap();

            CreateMap<Produto, ProdutoEdicaoModel>().ReverseMap();
        }
    }
}