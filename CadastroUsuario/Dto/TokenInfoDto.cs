using CadastroUsuario.Models.UserModel;

namespace CadastroUsuario.Dto
{
    public class TokenInfoDto
    {
        public string Token { get; set; }

        public Usuario Usuario { get; set; }
    }
}
