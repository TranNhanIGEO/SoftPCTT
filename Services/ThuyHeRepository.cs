using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class ThuyHeRepository : BaseRepository{
    public ThuyHeRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<ThuyHe> GetThuyHes(string tenhuyen){
        return connection.Query<ThuyHe>("SELECT * FROM GetThuyHes(@_tenhuyen)", new{
            _tenhuyen = tenhuyen
        }, commandType: CommandType.Text);
    }
}