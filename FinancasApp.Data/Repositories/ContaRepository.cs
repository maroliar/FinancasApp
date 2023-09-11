using FinancasApp.Data.Contexts;
using FinancasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Data.Repositories
{
    public class ContaRepository
    {
        public void Create(Conta conta)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(conta);
                dataContext.SaveChanges();
            }
        }

        public void Update(Conta conta)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(conta);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Conta conta)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Remove(conta);
                dataContext.SaveChanges();
            }
        }

        // consultar contas por periodo de datas e por usuario
        public List<Conta> GetAll(DateTime dataMin, DateTime dataMax, Guid usuarioId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Conta
                    .Include(c => c.Categoria) // LEFT JOIN COM CATEGORIA
                    .Where(C => C.Data >= dataMin && C.Data <= dataMax && C.UsuarioId == usuarioId)
                    .OrderBy(c => c.Data)
                    .ToList();
            }
        }

        public Conta? GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Conta
                    //.Include(c => c.Categoria) // LEFT JOIN COM CATEGORIA - TODO: TAVA BUGANDO O UPDATE. REMOVIDO
                    .Where(c => c.Id == id)
                    .FirstOrDefault();
            }
        }


    }
}
