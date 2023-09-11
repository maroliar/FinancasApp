using FinancasApp.Data.Entities;
using FinancasApp.Data.Helpers;
using FinancasApp.Data.Repositories;
using FinancasApp.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;

namespace FinancasApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioRepository = new UsuarioRepository();
                    var usuario = usuarioRepository.GetByEmailAndSenha(model.Email, SHA1Helper.Encrypt(model.Senha));

                    if (usuario != null)
                    {
                        // armazenar os dados do usuario autenticado
                        var authenticationViewModel = new AuthenticationViewModel
                        {
                            Id = usuario.Id,
                            Nome = usuario.Nome,
                            Email = usuario.Email,
                            DataHoraAcesso = DateTime.Now
                        };

                        var data = JsonConvert.SerializeObject(authenticationViewModel);

                        // criando a identiicacao do usuario no Asp.Net MVC (preparando para gravar em arquivo de cookie)
                        var clainIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, data) },
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        // gravando o Cookie
                        var claimsPrincipal = new ClaimsPrincipal(clainIdentity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        TempData["MensagemAlerta"] = "Acesso negado. Usuário não encontrado.";
                    }

                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário. Por favor verifique.";
            }

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

        public IActionResult Logout()
        {
            // destroi o cookie
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // destroi a session
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
