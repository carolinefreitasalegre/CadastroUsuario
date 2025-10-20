using CadastroUsuario.Context.DataContext;
using CadastroUsuario.Models.UserModel;
using Dapper;

namespace CadastroUsuario.Repository.UserRepository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDapperContext _dapperContext;

        public UsuarioRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public Usuario? BuscarUsuarioEmail(string email)
        {
            using(var connection = _dapperContext.CreateConnection())
            {
                var sql = @"SELECT * FROM [Usuario] WHERE [Email]=@Email";

                return connection.QueryFirstOrDefault<Usuario>(sql, new
                {
                    Email = email
                });
            }
        }

        public Usuario? BuscarUsuarioId(int id)
        {
            using(var connection =  _dapperContext.CreateConnection())
            {
                var sql = @"SELECT * FROM [Usuario] WHERE [Id]=@id";

                var usuario = connection.QueryFirstOrDefault<Usuario>(sql, new
                {
                    Id = id
                });

                if (usuario != null)
                    return usuario;
                else
                    throw new Exception("Usuário não encontrado.");
                

            }
        }

        public IList<Usuario?> ListarUsuarios()
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                var sql = @"SELECT * FROM [Usuario]";

                var usuarios = connection.Query<Usuario>(sql)?.ToList() ?? new List<Usuario>();
                return usuarios;
            }
        }

        public Usuario SalvarOuAtualizarUsuario(Usuario usuario)
        {
            using(var connection = _dapperContext.CreateConnection())
            {
                var sql = @"IF EXISTS (SELECT * FROM [Usuario] WHERE [Id]=@Id)
                            BEGIN UPDATE [Usuario] 
                            SET Nome = @Nome, Email = @Email, Telefone = @Telefone, Senha = @Senha, TipoUsuario = @TipoUsuario
                            OUTPUT INSERTED.*

                            WHERE Id = @Id;
        
                            SELECT * FROM [Usuario] WHERE [Id] = @Id;
                            END ELSE BEGIN

                            INSERT INTO [Usuario] (Nome, Email, Telefone, Senha, TipoUsuario)
                            VALUES (@Nome, @Email, @Telefone, @Senha, @TipoUsuario)
                            END";

                connection.Execute(sql, new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.Email,
                    usuario.Telefone,
                    usuario.Senha,
                    usuario.TipoUsuario
                });

                return usuario;
            }
        }
    }
}
