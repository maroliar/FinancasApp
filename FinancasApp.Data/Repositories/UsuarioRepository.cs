using FinancasApp.Data.Contexts;
using FinancasApp.Data.Entities;

namespace FinancasApp.Data.Repositories
{
    public class UsuarioRepository
    {
        public void Create(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Add(usuario);
                dataContext.SaveChanges();
            }
        }

        public void Update(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Update(usuario);
                dataContext.SaveChanges();
            }
        }

        public void Delete(Usuario usuario)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Remove(usuario);
                dataContext.SaveChanges();
            }
        }

        public List<Usuario> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Usuario
                    .OrderBy(x => x.Nome)
                    .ToList();
            }
        }



        public Usuario? GetByEmail(string email)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Usuario
                .Where(x => x.Email == email)
                .FirstOrDefault();
            }
        }

        public Usuario? GetByEmailAndSenha(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Usuario
                .Where(x => x.Email == email && x.Senha == senha)
                .FirstOrDefault();
            }
        }
    }


}