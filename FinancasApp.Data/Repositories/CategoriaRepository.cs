using FinancasApp.Data.Contexts;
using FinancasApp.Data.Entities;

namespace FinancasApp.Data.Repositories
{
    public class CategoriaRepository
    {
        public List<Categoria> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Categoria
                    .OrderBy(x => x.Nome)
                    .ToList();
            }
        }
    }
}
