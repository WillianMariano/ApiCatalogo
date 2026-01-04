using ApiCatalogo.Context;
using ApiCatalogo.Models;

namespace ApiCatalogo1.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context):base(context)
        { 
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(x=>x.CategoriaId == id);
        } 
    }
}
