using CadastroUsuarioClient.Models;
using static System.Net.WebRequestMethods;

namespace CadastroUsuarioClient.HttpService.UsuarioService
{
    public interface IUsuarioService
    {
        Task<IList<Usuario?>> ListarUsuarios();
        Task<Usuario?> BuscarUsuarioId(int id);
        Task<Usuario?> SalvarUsuario(Usuario usuario);
        Task<Usuario?> AtualizarUsuario(Usuario usuario);
    }
}
