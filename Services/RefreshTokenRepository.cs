using System.Data;
using Dapper;
using WebApi.Models;

namespace WebApi.Services;

public class RefreshTokenRepository : BaseRepository{
    public RefreshTokenRepository(IDbConnection connection) : base(connection){}

    public int AddRefreshToken(RefreshToken obj){
        return connection.ExecuteScalar<int>("AddRefreshToken", new{
            _refreshtokenid = obj.refreshtokenid,
            // _accesstoken = obj.accesstoken,
            // _memberid = obj.memberid,
            _memlogid = obj.memberloginid,
            _token = obj.token
        }, commandType: CommandType.StoredProcedure);
    }
    // public int EditRefreshToken(string NewAccessToken, string NewRefreshToken, string OldRefreshToken){
    //     return connection.ExecuteScalar<int>("EditRefreshToken", new{
    //         _newaccesstoken = NewAccessToken,
    //         _newrefreshtoken = NewRefreshToken,
    //         _oldrefreshtoken = OldRefreshToken
    //     }, commandType: CommandType.StoredProcedure);
    // }
    public RefreshToken? GetRefreshToken(string Token){
        return connection.QueryFirstOrDefault<RefreshToken>("SELECT * FROM GetRefreshToken(@_token)", new{
            _token = Token
        }, commandType: CommandType.Text);
    }
    public int DeleteRefreshToken(string memberloginid){
        return connection.ExecuteScalar<int>("DeteleRefreshToken", new{
            _memberloginid = memberloginid
        }, commandType: CommandType.StoredProcedure);
    }
}