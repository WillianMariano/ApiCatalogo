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
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int id)
        {
            var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id).ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            var produtoDtoList = produtos.ToProdutoDTOList();

            return Ok(produtoDtoList);
        }

        [HttpGet("pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameter produtosParams)
        {
            var produtos = _uof.ProdutoRepository.GetProdutos(produtosParams);
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagiation", JsonConvert.SerializeObject(metadata));

            var produtoDtoList = produtos.ToProdutoDTOList();

            return Ok(produtoDtoList);
        }

        [HttpGet("filter/preco/pagination")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosFiltroPreco([FromQuery] ProdutosFiltroPreco produtosFiltroPreco)
        {
            var produtos = _uof.ProdutoRepository.GetProdutosFiltroPreco(produtosFiltroPreco);
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }

            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Append("X-Pagiation", JsonConvert.SerializeObject(metadata));

            var produtoDtoList = produtos.ToProdutoDTOList();

            return Ok(produtoDtoList);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _uof.ProdutoRepository.GetAll().ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            var produtoDtoList = produtos.ToProdutoDTOList();

            return Ok(produtoDtoList);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto = _uof.ProdutoRepository.Get(x=>x.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }
            var produtoDto = produto.ToProdutoDTO();
            return Ok(produtoDto);
        }

        [HttpPost]
        public ActionResult Post(ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
                return BadRequest();

            var produto = produtoDto.ToProduto();

            var produtoBd = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();
            return new CreatedAtActionResult("Get", "Produtos", new { id = produtoBd.ProdutoId }, produtoBd);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
                return BadRequest();

            if (id != produtoDto.ProdutoId)
                return BadRequest();

            var produto = produtoDto.ToProduto();

            var produtoBd = _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok(produtoBd); 
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produtoBd = _uof.ProdutoRepository.Get(x=>x.ProdutoId == id);
            if (produtoBd is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDel = _uof.ProdutoRepository.Delete(produtoBd);
            _uof.Commit();

            var produtoDelDto = produtoDel.ToProdutoDTO();

            return Ok(produtoDelDto);
             
        }
    }
}
