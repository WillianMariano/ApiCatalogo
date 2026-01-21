using ApiCatalogo.Models;
using ApiCatalogo1.Pagination;

namespace ApiCatalogo1.Repositories
{
    public interface ICategoriaRepository:IRepository<Categoria>
    {
        PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters);

        PagedList<Categoria> GetCategoriaFiltroNome(CategoriasFiltroNome categoriasFiltroNome);
    }
}
