using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo1.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    { 
        public CategoriaRepository(AppDbContext context):base(context)
        { 
        } 
    }
}
