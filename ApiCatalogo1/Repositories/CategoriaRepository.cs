using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.Pagination; 
using X.PagedList;

namespace ApiCatalogo1.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    { 
        public CategoriaRepository(AppDbContext context):base(context)
        { 
        }

        public async Task<IPagedList<Categoria>> GetCategoriaFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNome)
        {
            var categoriasAsync = await GetAllAsync(); 
            if (!string.IsNullOrWhiteSpace(categoriasFiltroNome.Nome))
            {
                categoriasAsync = categoriasAsync.Where(x => x.Nome == categoriasFiltroNome.Nome);
            }
            var categoriasFiltradas = await categoriasAsync.ToPagedListAsync(categoriasFiltroNome.PageNumber, categoriasFiltroNome.PageSize);
            return categoriasFiltradas;
        }

        public async Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParameters)
        {
            var categoriasAsync = await GetAllAsync();
            var categorias = categoriasAsync.OrderBy(x => x.CategoriaId).AsQueryable();
            var categoriasOrdenados = await categorias.ToPagedListAsync(categoriasParameters.PageNumber, categoriasParameters.PageSize);
            return categoriasOrdenados;
        }
    }
}
