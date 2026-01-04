namespace ApiCatalogo1.Services
{
    public class MeuServico:IMeuServico
    {
        public string Saudacao(string nome)
        {
            return "Bem vindo " + nome;
        }
    }
}
