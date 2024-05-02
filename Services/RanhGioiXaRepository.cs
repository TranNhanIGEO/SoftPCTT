using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class RanhGioiXaRepository : BaseRepository{
    public RanhGioiXaRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<RanhGioiXa> GetXas(){
        return connection.Query<RanhGioiXa>("SELECT * FROM GetXas()");
    }
}