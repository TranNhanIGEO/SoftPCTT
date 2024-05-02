using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class QuanDaoHSTSRepository : BaseRepository{
    public QuanDaoHSTSRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<QuanDaoHSTS> GetQuanDaoHSTSs(){
        return connection.Query<QuanDaoHSTS>("SELECT * FROM GetQdhoangSaTruongSa()", commandType: CommandType.Text);
    }
}