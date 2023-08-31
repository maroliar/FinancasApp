namespace FinancasApp.Presentation.Models
{
    public class AuthenticationViewModel
    {
        // Modelo de dados para armazenar as informações do usuario autenticado

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataHoraAcesso { get; set; }


    }
}
