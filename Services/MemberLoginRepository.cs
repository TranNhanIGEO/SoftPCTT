using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class MemberLoginRepository : BaseRepository{
    public MemberLoginRepository(IDbConnection connection) : base(connection){}

    // public MemberLogin? GetMemberLogin(int id){
    //     return connection.QueryFirstOrDefault<MemberLogin>("SELECT * FROM GetMemberLogin(@_id)", new {
    //         _id = id
    //     });
    // }
    public int Add(MemberLoginAdd obj){
        return connection.ExecuteScalar<int>("AddMemberLogin", new {
            _memlogid = obj.memLogId,
            _username = obj.username
        }, commandType: CommandType.StoredProcedure);
    }
    public int Edit(string memberlogId){
        return connection.ExecuteScalar<int>("EditMemberLogin", new {
           _id = memberlogId
        }, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<CountLogin> CountLogins(){
        return connection.Query<CountLogin>("SELECT * FROM CountLogins()");
    }
} 