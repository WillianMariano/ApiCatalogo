using ApiCatalogo.Models;
using ApiCatalogo1.Pagination;

namespace ApiCatalogo1.Repositories
{
    public interface IProdutoRepository:IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategoria(int id); 
        PagedList<Produto> GetProdutos(ProdutosParameter produtosParams);
        PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco);
    }
}
