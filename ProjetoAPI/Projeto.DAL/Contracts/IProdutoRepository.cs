using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.DAL.Entities;

namespace Projeto.DAL.Contracts
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        List<Produto> SelectAll(decimal precoMin, decimal precoMax);
    }
}
