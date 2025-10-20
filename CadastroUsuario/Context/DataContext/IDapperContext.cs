using System.Data.Common;

namespace CadastroUsuario.Context.DataContext
{
    public interface IDapperContext
    {
        DbConnection CreateConnection();
    }
}
