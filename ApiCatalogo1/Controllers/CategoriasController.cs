using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.DTO;
using ApiCatalogo1.Filters;
using ApiCatalogo1.Pagination;
using ApiCatalogo1.Repositories;
using ApiCatalogo1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        [Authorize]
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _uof.CategoriaRepository.GetAllAsync();

            var categoriaDtoList = categorias.ToCategoriaDTOList();

            return Ok(categoriaDtoList);
        }

        [HttpGet("pagination")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            var categorias = await _uof.CategoriaRepository.GetCategoriasAsync(categoriasParameters);
            if (categorias is null)
            {
                return NotFound("Produtos não encontrados");
            }

            var metadata = new
            {
                categorias.Count,
                categorias.PageSize,
                categorias.PageCount,
                categorias.TotalItemCount,
                categorias.HasNextPage,
                categorias.HasPreviousPage
            };

            Response.Headers.Append("X-Pagiation", JsonConvert.SerializeObject(metadata));

            var categoriaDtoList = categorias.ToCategoriaDTOList();

            return Ok(categoriaDtoList);
        }
        [HttpGet("filter/nome/pagination")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasFiltroNome([FromQuery] CategoriasFiltroNome categoriasFiltroNome)
        {
            var categorias = await _uof.CategoriaRepository.GetCategoriaFiltroNomeAsync(categoriasFiltroNome);
            if (categorias is null)
            {
                return NotFound("Produtos não encontrados");
            }

            var metadata = new
            {
                categorias.Count,
                categorias.PageSize,
                categorias.PageCount,
                categorias.TotalItemCount,
                categorias.HasNextPage,
                categorias.HasPreviousPage
            };

            Response.Headers.Append("X-Pagiation", JsonConvert.SerializeObject(metadata));

            var categoriaDtoList = categorias.ToCategoriaDTOList();

            return Ok(categoriaDtoList);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetAsync(x => x.CategoriaId == id);

            var categoriaDto = categoria.ToCategoriaDTO();

            return Ok(categoriaDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Post(CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return BadRequest();

            var categoria = categoriaDto.ToCategoria();

            var categoriaBd = _uof.CategoriaRepository.Create(categoria);
            await _uof.CommitAsync();

            var novaCategoriaDto = categoriaBd.ToCategoriaDTO();

            return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
                return BadRequest();

            if (id != categoriaDto.CategoriaId)
                return BadRequest();

            var categoria = categoriaDto.ToCategoria();

            var categoriaBd = _uof.CategoriaRepository.Update(categoria);
            await _uof.CommitAsync();

            var novaCategoriaDto = categoriaBd.ToCategoriaDTO();

            return Ok(novaCategoriaDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var categoria = await _uof.CategoriaRepository.GetAsync(x => x.CategoriaId == id);

            if (categoria is null)
                return NotFound("Categoria não encontrado");

            var categoriaBd = _uof.CategoriaRepository.Delete(categoria);
           await _uof.CommitAsync();

            var categoriaDto = categoriaBd.ToCategoriaDTO();

            return Ok(categoriaDto);
        }
    }
}
