using CadastroUsuario.Enum;

namespace CadastroUsuario.Dto
{
    public class SalvarOuAtualizarUsuarioDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Senha { get; set; }
        public ETipoUsuario TipoUsuario { get; set; }
    }
}
