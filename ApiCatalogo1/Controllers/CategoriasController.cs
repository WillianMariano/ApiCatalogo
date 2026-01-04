using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.Filters;
using ApiCatalogo1.Repositories;
using ApiCatalogo1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        public CategoriasController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _uof.CategoriaRepository.GetAll();
            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(x => x.CategoriaId == id);
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            var categoriaBd = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();
            return Ok(categoriaBd);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (categoria is null)
                return BadRequest();

            if (id != categoria.CategoriaId)
                return BadRequest();

            var categoriaBd = _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();
            return Ok(categoriaBd);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(x => x.CategoriaId == id);

            if (categoria is null)
                return NotFound("Categoria não encontrado");

            var categoriaBd = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();
            return Ok(categoriaBd);
        }
    }
}
