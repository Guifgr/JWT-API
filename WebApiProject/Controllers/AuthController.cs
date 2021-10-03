using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.DTOs;
using WebApiProject.Models;
using WebApiProject.Repository;
using WebApiProject.Repository.Interfaces;
using WebApiProject.Services;

namespace WebApiProject.Controllers
{
    public class AuthController : Controller
    {
        public readonly IUserRepository _userRepository;
        public readonly ITokenService _TokenService;

        public AuthController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _TokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> login([FromBody]UserRequestWithouRoleDto model)
        {
            var userEntity = await _userRepository.GetUserByUsername(model.Username);
            userEntity.Password = model.Password;
            var user = await _userRepository.ValidateUser(userEntity);

            // Verifica se o usuário existe
            if (user == default)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = _TokenService.GenerateToken(user);

            // Retorna os dados
            return token;
        }
        
        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<string>> Signup([FromBody]UserRequestDto model)
        {
            // Recupera o usuário
            var user = await _userRepository.CreateUser(model);

            // Gera o Token
            var token = _TokenService.GenerateToken(user);

            // Retorna os dados
            return token;
        }
        
        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "Employee,Manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Manager")]
        public string Manager() => "Gerente";
    }
}