using Dapper;
using System.Data;
namespace WebApi.Services;

public class HistoryRepository : BaseRepository{
    public HistoryRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<History> GetHistorys(string tablename){
        return connection.Query<History>("SELECT * FROM GetHistory(@_tablename)", new{
            _tablename = tablename
        });
    }
    public int AddHistory(string id, string tablename, string rowid, string username, string operation){
        return connection.ExecuteScalar<int>("AddHistory", new{
            _id = id,
            _tablename = tablename,
            _rowid = rowid,
            _username = username,
            _operation = operation
        }, commandType: CommandType.StoredProcedure);
    }
    public int EditHistory(string id,  string tablename, string rowid){
        return connection.ExecuteScalar<int>("EditHistory", new{
            _id = id,
            _tablename = tablename,
            _rowid = rowid
        }, commandType: CommandType.StoredProcedure);
    }
    public int DeletetHistory(string id){
        return connection.ExecuteScalar<int>("DeleteHistory", new{
            _id = id
        }, commandType: CommandType.StoredProcedure);
    }
}   