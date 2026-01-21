using ApiCatalogo.Models;

namespace ApiCatalogo1.DTO
{
    public static class ProdutoDTOMappingExtensions
    {
        public static ProdutoDTO? ToProdutoDTO(this Produto produto)
        {
            if (produto == null)
            {
                return null;
            }

            return new ProdutoDTO
            {
                ProdutoId = produto.ProdutoId,
                DataCadastro = produto.DataCadastro,
                Descricao = produto.Descricao,
                Estoque = produto.Estoque,
                Preco = produto.Preco,
                CategoriaId = produto.CategoriaId,
                Nome = produto.Nome,
                ImagemUrl = produto.ImagemUrl
            };
        }
        public static Produto? ToProduto(this ProdutoDTO produtoDto)
        {
            if (produtoDto == null)
            {
                return null;
            }

            return new Produto
            {
                ProdutoId = produtoDto.ProdutoId,
                DataCadastro = produtoDto.DataCadastro,
                Descricao = produtoDto.Descricao,
                Estoque = produtoDto.Estoque,
                Preco = produtoDto.Preco,
                CategoriaId = produtoDto.CategoriaId,
                Nome = produtoDto.Nome,
                ImagemUrl = produtoDto.ImagemUrl
            };
        }
        public static IEnumerable<ProdutoDTO> ToProdutoDTOList(this IEnumerable<Produto> produtos)
        {
            if (produtos == null || !produtos.Any())
            {
                return new List<ProdutoDTO>();
            }

            return produtos.Select(produto => new ProdutoDTO
            {
                ProdutoId = produto.ProdutoId,
                DataCadastro = produto.DataCadastro,
                Descricao = produto.Descricao,
                Estoque = produto.Estoque,
                Preco = produto.Preco,
                CategoriaId = produto.CategoriaId,
                Nome = produto.Nome,
                ImagemUrl = produto.ImagemUrl
            }).ToList();
        }
    }
}
