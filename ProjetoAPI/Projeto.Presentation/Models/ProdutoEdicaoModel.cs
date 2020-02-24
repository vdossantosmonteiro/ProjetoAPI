using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Projeto.Presentation.Models
{
    public class ProdutoEdicaoModel
    {
        [Required(ErrorMessage = "Campo Id é obrigatório.")]
        public string IdProduto { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo preço é obrigatório.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo quantidade é obrigatório.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Campo estoque é obrigatório.")]
        public int IdEstoque { get; set; }

        //campo que será utilizado para gerar o DropDownList
        public List<SelectListItem> Estoques { get; set; }
    }
}