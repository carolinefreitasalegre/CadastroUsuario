using CadastroUsuario.Models.UserModel;

namespace CadastroUsuario.Repository.UserRepository
{
    public interface IUsuarioRepository
    {
        IList<Usuario?> ListarUsuarios();
        Usuario? BuscarUsuarioId(int id);
        Usuario? BuscarUsuarioEmail(string email);
        Usuario SalvarOuAtualizarUsuario(Usuario usuario);
    }
}
