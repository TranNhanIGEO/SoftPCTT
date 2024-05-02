using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class GiaoThongRepository : BaseRepository{
    public GiaoThongRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<GiaoThong> GetGiaoThongs(string mahuyen){
        return connection.Query<GiaoThong>("SELECT * FROM GetGiaoThongs(@_mahuyen)", new{
            _mahuyen = mahuyen
        });
    }
}