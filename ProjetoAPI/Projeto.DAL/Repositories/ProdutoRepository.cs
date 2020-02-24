using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.DAL.Entities; //importando
using Projeto.DAL.Contracts; //importando
using System.Configuration; //importando
using System.Data.SqlClient; //importando
using Dapper; //importando

namespace Projeto.DAL.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string connectionString;

        public ProdutoRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["aula"].ConnectionString;
        }

        public void Insert(Produto obj)
        {
            string query = "insert into Produto(Nome, Preco, Quantidade, IdEstoque) "
                         + "values(@Nome, @Preco, @Quantidade, @IdEstoque)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Update(Produto obj)
        {
            string query = "update Produto set Nome = @Nome, Preco = @Preco, Quantidade = @Quantidade, "
                         + "IdEstoque = @IdEstoque where IdProduto = @IdProduto";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Delete(int id)
        {
            string query = "delete from Produto where IdProduto = @IdProduto";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { IdProduto = id });
            }
        }

        public List<Produto> SelectAll()
        {
            var query = "select * from Produto p inner join Estoque e "
                      + "on p.IdEstoque = e.IdEstoque";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query(query,
                        (Produto p, Estoque e)
                            =>
                                {
                                    p.Estoque = e;
                                    return p;
                                },
                            splitOn: "IdEstoque")
                        .ToList();
            }
        }

        public Produto SelectById(int id)
        {
            var query = "select * from Produto p inner join Estoque e "
                      + "on p.IdEstoque = e.IdEstoque "
                      + "where IdProduto = @IdProduto";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query(query,
                        (Produto p, Estoque e)
                            =>
                            {
                                p.Estoque = e;
                                return p;
                            },
                            new { IdProduto = id },
                            splitOn: "IdEstoque")
                        .SingleOrDefault();
            }
        }

        public List<Produto> SelectAll(decimal precoMin, decimal precoMax)
        {
            var query = "select * from Produto p inner join Estoque e "                        
                      + "on p.IdEstoque = e.IdEstoque "
                      + "where Preco between @Min and @Max";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query(query,
                        (Produto p, Estoque e)
                            =>
                        {
                            p.Estoque = e;
                            return p;
                        },
                            new { Min = precoMin, Max = precoMax },
                            splitOn: "IdEstoque")
                        .ToList();
            }
        }
    }
}
