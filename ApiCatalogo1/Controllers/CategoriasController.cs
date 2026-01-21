using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.DTO;
using ApiCatalogo1.Filters;
using ApiCatalogo1.Repositories;
using ApiCatalogo1.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

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
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _uof.CategoriaRepository.GetAll();

            var categoriaDtoList = categorias.ToCategoriaDTOList();

            return Ok(categoriaDtoList);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(x => x.CategoriaId == id);

            var categoriaDto = categoria.ToCategoriaDTO();
            
            return Ok(categoriaDto);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return BadRequest();

            var categoria = categoriaDto.ToCategoria();

            var categoriaBd = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();

            var novaCategoriaDto = categoriaBd.ToCategoriaDTO();

            return new CreatedAtRouteResult("ObterCategoria", new {id = novaCategoriaDto.CategoriaId}, novaCategoriaDto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return BadRequest();

            if (id != categoriaDto.CategoriaId)
                return BadRequest();

            var categoria = categoriaDto.ToCategoria();

            var categoriaBd = _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();

            var novaCategoriaDto = categoriaBd.ToCategoriaDTO();

            return Ok(novaCategoriaDto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(x => x.CategoriaId == id);

            if (categoria is null)
                return NotFound("Categoria não encontrado");

            var categoriaBd = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            var categoriaDto = categoriaBd.ToCategoriaDTO();

            return Ok(categoriaDto);
        }
    }
}
