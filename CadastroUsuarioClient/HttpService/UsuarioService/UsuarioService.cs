using CadastroUsuarioClient.Models;

namespace CadastroUsuarioClient.HttpService.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;

        }

        public async Task<Usuario?> AtualizarUsuario(Usuario usuario)
        {
            var response = await _http.PutAsJsonAsync($"api/usuarios/{usuario.Id}", usuario);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            return null;
        }
        public async Task<Usuario?> SalvarUsuario(Usuario usuario)
        {
            var response = await _http.PostAsJsonAsync("api/usuarios/", usuario);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            return null;
        }

        public async Task<Usuario?> BuscarUsuarioId(int id)
        {
            return await _http.GetFromJsonAsync<Usuario>($"api/usuarios/{id}");
        }

        public async Task<IList<Usuario?>> ListarUsuarios()
        {
            return await _http.GetFromJsonAsync<List<Usuario>>("api/usuarios");
            
        }     
    }
}
