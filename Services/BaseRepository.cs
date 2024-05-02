using System.Data;

namespace WebApi.Services;

public class BaseRepository{
    protected IDbConnection connection;
    public BaseRepository(IDbConnection connection) => this.connection = connection;
}