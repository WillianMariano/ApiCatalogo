using ApiCatalogo.Models;
using ApiCatalogo1.Pagination;
using X.PagedList;

namespace ApiCatalogo1.Repositories
{
    public interface ICategoriaRepository:IRepository<Categoria>
    {
        Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters);

        Task<IPagedList<Categoria>> GetCategoriaFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNome);
    }
}
