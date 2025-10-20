using CadastroUsuario.Context.DataContext;
using CadastroUsuario.Dto;
using CadastroUsuario.Repository.UserRepository;
using FluentValidation;

namespace CadastroUsuario.Validators.ValidarUsuario
{
    public class UsuarioValidator : AbstractValidator<SalvarOuAtualizarUsuarioDto>
    {
        private readonly IUsuarioRepository _repository;
        public UsuarioValidator(IUsuarioRepository repository)
        {

            _repository = repository;

            RuleFor(u => u.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.")
                .MustAsync(EmailUnico).WithMessage("O e-mail informado já está em uso.");

            RuleFor(u => u.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\d{10,11}$").WithMessage("Telefone deve conter 10 ou 11 dígitos.");

            RuleFor(u => u.Senha)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");

            RuleFor(u => u.TipoUsuario)
                .IsInEnum().WithMessage("Tipo de usuário inválido.");
        }

        private async Task<bool> EmailUnico(string email, CancellationToken token)
        {
            var usuario = _repository.BuscarUsuarioEmail(email);

            if (usuario.Email == email)
                return true;

            return usuario == null;
        }

    }
}
