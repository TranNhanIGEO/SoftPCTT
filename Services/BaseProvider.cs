using System.Data;
using Npgsql;

namespace WebApi.Services;

public abstract class BaseProvider
{
    IDbConnection connection = null!;
    IConfiguration configuration;
    public BaseProvider(IConfiguration configuration) => this.configuration = configuration;
    protected IDbConnection Connection => connection ??= new NpgsqlConnection(configuration.GetConnectionString("PCTT"));
}