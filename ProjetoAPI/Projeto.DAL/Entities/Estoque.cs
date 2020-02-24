using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Entities
{
    public class Estoque
    {
        public int IdEstoque { get; set; }
        public string Nome { get; set; }

        //Relacionamento de Associação
        public List<Produto> Produtos { get; set; }
    }
}
