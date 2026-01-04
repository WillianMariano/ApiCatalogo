using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo1.Migrations
{
    /// <inheritdoc />
    public partial class POPULAProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl,Estoque, DataCadastro, CategoriaId) values ('Coca Cola Diet', 'Refrigerante de cola 350ml',5.45, 'cocacola.jpg',50, now(),1)");
            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl,Estoque, DataCadastro, CategoriaId) values ('Coxinha', 'Massa com frango',6.50, 'coxinha.jpg',10, now(),2)");
            migrationBuilder.Sql("Insert into Produtos(Nome, Descricao, Preco, ImagemUrl,Estoque, DataCadastro, CategoriaId) values ('Chocolate', 'Chocolate',4.90, 'chocolate.jpg',20, now(),3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Produtos");
        }
    }
}
