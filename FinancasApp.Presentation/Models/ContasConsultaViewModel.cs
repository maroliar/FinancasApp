using FinancasApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace FinancasApp.Presentation.Models
{
    public class ContasConsultaViewModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public DateTime? DataFim { get; set; }

        // Listagem de contas que sera exibida na pagina apos a realização da pesquisa
        public List<Conta>? ListagemContas { get; set; }






    }
}
