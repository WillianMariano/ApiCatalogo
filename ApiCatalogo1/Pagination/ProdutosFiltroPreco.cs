namespace ApiCatalogo1.Pagination
{
    public class ProdutosFiltroPreco:QueryStringParamaters
    {
        public decimal? Preco {  get; set; }
        public string? Criterio {  get; set; }
    }
}
