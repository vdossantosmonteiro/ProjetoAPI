using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Models
{
    public class EstoqueEdicaoModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int IdEstoque { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
    }
}