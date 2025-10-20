using CadastroUsuario.Auth;
using CadastroUsuario.Dto;
using CadastroUsuario.Models.UserModel;
using CadastroUsuario.Service.UserService;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CadastroUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;
        private readonly IConfiguration _config;
        public UsuarioController(IUsuarioService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPost("login")]
        public ActionResult Autenticar(LoginDto login)
        {
            try
            {
                Usuario? usuario = _service.LoginAsync(login.Email, login.Senha);
                if(usuario != null)
                {
                    string key = _config.GetSection("Token").Value!;
                    string infoToken = AutenticarUsuaurio.GerarToken(usuario, key);

                    return Ok(new TokenInfoDto {  Token = infoToken, Usuario = usuario});
                }
                else
                {
                    throw new Exception("Usuário não identificado!");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Usuario")]

        public ActionResult ListarUsuarios()
        {
            var usuarios = _service.ListarUsuarios();

            var resposta = usuarios.Select(u => new RespostaUsuarioDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Telefone = u.Telefone,
                TipoUsuario = u.TipoUsuario,
            }).ToList();



            return Ok(resposta);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Usuario")]

        public ActionResult BuscarUsuarioPorId(int id)
        {
            var usuario = _service.BuscarUsuarioId(id);
            if (usuario == null)
                return NotFound("Usuário não encontrado");

            var resposta = new RespostaUsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                TipoUsuario = usuario.TipoUsuario
            };

            return Ok(resposta);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdicionarUsuario(SalvarOuAtualizarUsuarioDto usuario, IValidator<SalvarOuAtualizarUsuarioDto> validator)
        {
            var validatorResult = await validator.ValidateAsync(usuario);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);

            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome.ToLower(),
                Email = usuario.Email.ToLower(),
                Telefone = usuario.Telefone,
                Senha = usuario.Senha,
                TipoUsuario = usuario.TipoUsuario,
            };

            var usuarioCriado = _service.SalvarOuAtualizarUsuario(novoUsuario);


            return Created("", usuarioCriado);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditarUsuario(SalvarOuAtualizarUsuarioDto usuario, IValidator<SalvarOuAtualizarUsuarioDto> validator)
        {
            var usuarioexistente = _service.BuscarUsuarioId(usuario.Id);
            if (usuarioexistente == null)
                throw new Exception("Usuário não encontrado.");
            else
            {
                var validatorResult = await validator.ValidateAsync(usuario);

                if (!validatorResult.IsValid)
                    return BadRequest(validatorResult.Errors);


                usuarioexistente.Nome = usuario.Nome.ToLower();
                usuarioexistente.Email = usuario.Email.ToLower();
                usuarioexistente.Telefone = usuario.Telefone;
                usuarioexistente.Senha = usuario.Senha;
                usuarioexistente.TipoUsuario = usuario.TipoUsuario;
            }

            _service.SalvarOuAtualizarUsuario(usuarioexistente);

            return Created("", usuarioexistente);
        }
    }
}
