using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace CadastroUsuario.Context.DataContext
{
    public class DapperContext : IDapperContext
    {
        private readonly string? connectionString;

        public DapperContext(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("ConnectionString")["connection"];
        }
        public DbConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
