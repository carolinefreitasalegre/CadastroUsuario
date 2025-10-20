using CadastroUsuario.Auth;
using CadastroUsuario.Dto;
using CadastroUsuario.Models.UserModel;
using CadastroUsuario.Repository.UserRepository;
using FluentValidation;

namespace CadastroUsuario.Service.UserService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario? BuscarUsuarioId(int id)
        {
            
            var usuario = _usuarioRepository.BuscarUsuarioId(id);

            return usuario ?? throw new Exception("Usuário não encontrado ou identificação inválida.");
        }

        public IList<Usuario> ListarUsuarios()
        {
            var usuarios = _usuarioRepository.ListarUsuarios();

            return usuarios ?? throw new Exception("Algo deu errado na busca por usuários.");      
        }

        public Usuario? LoginAsync(string email, string senha)
        {
            var usuario = _usuarioRepository.BuscarUsuarioEmail(email);

            if (usuario != null && usuario.Senha == senha)
                return usuario;
            else
                throw new Exception("Usuário ou senha não confere.");
        }

        public Usuario SalvarOuAtualizarUsuario(Usuario usuario)
        {
            return _usuarioRepository.SalvarOuAtualizarUsuario(usuario);            
        }
    }
}
