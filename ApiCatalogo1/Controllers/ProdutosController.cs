using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {
            var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id).ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _uof.ProdutoRepository.GetAll().ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _uof.ProdutoRepository.Get(x=>x.ProdutoId == id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
                return BadRequest();

            var produtoBd = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();
            return new CreatedAtActionResult("Get", "Produtos", new { id = produtoBd.ProdutoId }, produtoBd);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (produto is null)
                return BadRequest();

            if (id != produto.ProdutoId)
                return BadRequest();

            var produtoBd = _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok(produtoBd); 
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produtoBd = _uof.ProdutoRepository.Get(x=>x.ProdutoId == id);
            if (produtoBd is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoDel = _uof.ProdutoRepository.Delete(produtoBd);
            _uof.Commit();
            return Ok(produtoDel);
             
        }
    }
}
