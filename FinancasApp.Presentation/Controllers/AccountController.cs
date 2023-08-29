using FinancasApp.Data.Entities;
using FinancasApp.Data.Helpers;
using FinancasApp.Data.Repositories;
using FinancasApp.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace FinancasApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();

                    // verificar se ja existe um usuario cadastrado com o email informado
                    if (usuarioRepository.GetByEmail(model.Email) != null)
                    {
                        TempData["MensagemAlerta"] = "O email informado ja está cadastrado para outro usuário.";
                    }
                    else
                    {
                        var usuario = new Usuario
                        {
                            Id = Guid.NewGuid(),
                            Nome = model.Nome,
                            Email = model.Email,
                            Senha = SHA1Helper.Encrypt(model.Senha),
                            DataHoraCriacao = DateTime.Now
                        };


                        usuarioRepository.Create(usuario);

                        TempData["MensagemSucesso"] = "Usuario criado com sucesso!";
                        ModelState.Clear();
                    }

                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário, por favor verifique.";
            }

            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
