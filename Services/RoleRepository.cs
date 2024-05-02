using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class RoleRepository : BaseRepository{
    public RoleRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<RoleOfMember> GetRoleOfMember(string memberid){
        return connection.Query<RoleOfMember>("SELECT * FROM GetRoleOfMember(@_memberid)", new{
            _memberid = memberid
        }, commandType: CommandType.Text).AsEnumerable();
    }
}   