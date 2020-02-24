using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //validações

namespace Projeto.Presentation.Models
{
    public class EstoqueCadastroModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }
    }
}