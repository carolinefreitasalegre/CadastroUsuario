using CadastroUsuario.Dto;
using CadastroUsuario.Models.UserModel;

namespace CadastroUsuario.Service.UserService
{
    public interface IUsuarioService
    {
        IList<Usuario> ListarUsuarios();
        Usuario? BuscarUsuarioId(int id);
        Usuario? LoginAsync(string email, string senha);
        Usuario SalvarOuAtualizarUsuario(Usuario usuario);
    }
}
