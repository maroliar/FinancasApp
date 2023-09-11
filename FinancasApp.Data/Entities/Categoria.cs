using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Data.Entities
{
    // Categora nao é um Enum, pois faz sentido evoluir o projeto e cadastrar novas categorias no BD
    // por isso que TipoConta é um Enum e Categoria não!
    public class Categoria
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        // Relacionamentos
        public List<Conta> Contas { get; set; }


    }
}
