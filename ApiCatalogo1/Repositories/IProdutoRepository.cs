using ApiCatalogo.Models;
using ApiCatalogo1.Pagination;
using X.PagedList;

namespace ApiCatalogo1.Repositories
{
    public interface IProdutoRepository:IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
        Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameter produtosParams);
        Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco);
    }
}
