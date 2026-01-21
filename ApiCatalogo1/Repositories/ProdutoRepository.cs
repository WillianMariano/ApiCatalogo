using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.Pagination;

namespace ApiCatalogo1.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context):base(context)
        { 
        }

        public PagedList<Produto> GetProdutos(ProdutosParameter produtosParams)
        {
            var produtos = GetAll().OrderBy(x => x.ProdutoId).AsQueryable();
            var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtosParams.PageNumber, produtosParams.PageSize);
            return produtosOrdenados;
        }

        public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = GetAll().AsQueryable();

            if(produtosFiltroPreco.Preco.HasValue && !string.IsNullOrWhiteSpace(produtosFiltroPreco.Criterio))
            {
                if(produtosFiltroPreco.Criterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
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

            return PagedList<Produto>.ToPagedList(produtos, produtosFiltroPreco.PageNumber, produtosFiltroPreco.PageSize);
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(x=>x.CategoriaId == id);
        } 
    }
}
