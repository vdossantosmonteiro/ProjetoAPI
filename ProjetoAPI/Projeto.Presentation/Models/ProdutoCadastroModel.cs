using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //validações
using System.Web.Mvc; //importando

namespace Projeto.Presentation.Models
{
    public class ProdutoCadastroModel
    {
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