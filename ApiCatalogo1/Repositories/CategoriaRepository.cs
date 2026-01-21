using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo1.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    { 
        public CategoriaRepository(AppDbContext context):base(context)
        { 
        }

        public PagedList<Categoria> GetCategoriaFiltroNome(CategoriasFiltroNome categoriasFiltroNome)
        {
            var categorias = GetAll().AsQueryable();
            if (!string.IsNullOrWhiteSpace(categoriasFiltroNome.Nome))
            {
                categorias = categorias.Where(x => x.Nome == categoriasFiltroNome.Nome);
            }
            return PagedList<Categoria>.ToPagedList(categorias, categoriasFiltroNome.PageNumber, categoriasFiltroNome.PageSize);
        }

        public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
        {
            var categorias = GetAll().OrderBy(x => x.CategoriaId).AsQueryable();
            var categoriasOrdenados = PagedList<Categoria>.ToPagedList(categorias, categoriasParameters.PageNumber, categoriasParameters.PageSize);
            return categoriasOrdenados;
        }
    }
}
