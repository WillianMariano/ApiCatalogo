using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.Pagination;
using X.PagedList;

namespace ApiCatalogo1.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameter produtosParams)
        {
            var produtosAsync = await GetAllAsync();
            var produtos = produtosAsync.OrderBy(x => x.ProdutoId).AsQueryable();
            var produtosOrdenados = await produtos.ToPagedListAsync( produtosParams.PageNumber, produtosParams.PageSize);
            return produtosOrdenados;
        }

        public async Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtosAsync = await GetAllAsync();
            var produtos = produtosAsync.AsQueryable();

            if (produtosFiltroPreco.Preco.HasValue && !string.IsNullOrWhiteSpace(produtosFiltroPreco.Criterio))
            {
                if (produtosFiltroPreco.Criterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(x => x.Preco > produtosFiltroPreco.Preco).OrderBy(x => x.ProdutoId);
                }
                else if (produtosFiltroPreco.Criterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(x => x.Preco < produtosFiltroPreco.Preco).OrderBy(x => x.ProdutoId);
                }
                else if (produtosFiltroPreco.Criterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(x => x.Preco == produtosFiltroPreco.Preco).OrderBy(x => x.ProdutoId);
                }
                else
                {
                    produtos = new List<Produto>().AsQueryable();
                }
            }

            return await produtos.ToPagedListAsync( produtosFiltroPreco.PageNumber, produtosFiltroPreco.PageSize);
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
        {
            var produtos = await GetAllAsync();
            return produtos.Where(x => x.CategoriaId == id);
        }
    }
}
