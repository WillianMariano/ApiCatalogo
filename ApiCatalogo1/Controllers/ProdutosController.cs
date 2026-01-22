using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.DTO;
using ApiCatalogo1.Pagination;
using ApiCatalogo1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCatalogo1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;

        public ProdutosController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet("produtos/{id}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorCategoria(int id)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosPorCategoriaAsync(id);
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            var produtoDtoList = produtos.ToProdutoDTOList();

            return Ok(produtoDtoList);
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameter produtosParams)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosAsync(produtosParams);
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            var metadata = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.PageCount,
                produtos.TotalItemCount,
                produtos.HasNextPage,
                produtos.HasPreviousPage
            };

            Response.Headers.Append("X-Pagiation", JsonConvert.SerializeObject(metadata));

            var produtoDtoList = produtos.ToProdutoDTOList();

            return Ok(produtoDtoList);
        }

        [HttpGet("filter/preco/pagination")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosFiltroPreco([FromQuery] ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtosFiltroPreco);
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            var metadata = new
            {
                produtos.Count,
                produtos.PageSize,
                produtos.PageCount,
                produtos.TotalItemCount,
                produtos.HasNextPage,
                produtos.HasPreviousPage
            };

            Response.Headers.Append("X-Pagiation", JsonConvert.SerializeObject(metadata));

            var produtoDtoList = produtos.ToProdutoDTOList();

            return Ok(produtoDtoList);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            var produtos = await _uof.ProdutoRepository.GetAllAsync();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            var produtoDtoList = produtos.ToProdutoDTOList();

            return Ok(produtoDtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Get(int id)
        {
            var produto = await _uof.ProdutoRepository.GetAsync(x => x.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }
            var produtoDto = produto.ToProdutoDTO();
            return Ok(produtoDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
                return BadRequest();

            var produto = produtoDto.ToProduto();

            var produtoBd = _uof.ProdutoRepository.Create(produto);
            await _uof.CommitAsync();
            return new CreatedAtActionResult("Get", "Produtos", new { id = produtoBd.ProdutoId }, produtoBd);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
                return BadRequest();

            if (id != produtoDto.ProdutoId)
                return BadRequest();

            var produto = produtoDto.ToProduto();

            var produtoBd = _uof.ProdutoRepository.Update(produto);
            await _uof.CommitAsync();
            return Ok(produtoBd);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produtoBd = await _uof.ProdutoRepository.GetAsync(x => x.ProdutoId == id);
            if (produtoBd is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDel = _uof.ProdutoRepository.Delete(produtoBd);
            await _uof.CommitAsync();

            var produtoDelDto = produtoDel.ToProdutoDTO();

            return Ok(produtoDelDto);

        }
    }
}
