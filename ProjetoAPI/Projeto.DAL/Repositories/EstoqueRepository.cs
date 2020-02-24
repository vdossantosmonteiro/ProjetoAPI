using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.DAL.Entities;
using Projeto.DAL.Contracts;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace Projeto.DAL.Repositories
{
    public class EstoqueRepository : IEstoqueRepository
    {
        //atributo
        private readonly string connectionString;

        //método construtor
        public EstoqueRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["aula"].ConnectionString;
        }

        public void Insert(Estoque obj)
        {
            string query = "insert into Estoque(Nome) values(@Nome)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Update(Estoque obj)
        {
            string query = "update Estoque set Nome = @Nome where IdEstoque = @IdEstoque";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Delete(int id)
        {
            string query = "delete from Estoque where IdEstoque = @IdEstoque";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { IdEstoque = id });
            }
        }

        public List<Estoque> SelectAll()
        {
            string query = "select * from Estoque";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<Estoque>(query).ToList();
            }
        }

        public Estoque SelectById(int id)
        {
            string query = "select * from Estoque where IdEstoque = @IdEstoque";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<Estoque>(query, new { IdEstoque = id });
            }
        }

        public int CountProdutos(int idEstoque)
        {
            string query = "select count(*) from Produto where IdEstoque = @IdEstoque ";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingle<int>(query, new { IdEstoque = idEstoque });
            }
        }
    }
}
