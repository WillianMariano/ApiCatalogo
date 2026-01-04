using ApiCatalogo.Models;

namespace ApiCatalogo1.Repositories
{
    public interface IProdutoRepository:IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategoria(int id); 
    }
}
