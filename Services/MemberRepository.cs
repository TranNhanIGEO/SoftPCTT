using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class MemberRepository : BaseRepository{
    public MemberRepository(IDbConnection connection) : base(connection){}
    public Member? Login(LoginModel obj){
        return connection.Query<Member>("SELECT * FROM LoginMember(@usr, @pwd)", new{
            usr = obj.Usr, 
            pwd = obj.Pwd
            //pwd = Helper.HashString(obj.Pwd)
        }, commandType: CommandType.Text).FirstOrDefault();
    }
    public string? PasswordChangeTime(MemberPassword obj){
        return connection.Query<string>("SELECT * FROM PasswordChangeTime(@MemId, @Pwd)", new {
            MemId = obj.memberid,
            Pwd = obj.password
        }, commandType: CommandType.Text).FirstOrDefault();
    }
    public int Add(AddMember obj){
        return connection.ExecuteScalar<int>("AddMember", new {
            _memberid = obj.memberid,
            _password = obj.password,
            // _password = Convert.ToBase64String(Helper.Hash(obj.password)),
            _username = obj.username,
            _fullname = obj.fullname,
            _department = obj.department,
            _unit = obj.unit,
            _phone = obj.phone,
            _email = obj.email
        }, commandType: CommandType.StoredProcedure);
    }
    public int Update(Member obj, string memberid){
        return connection.ExecuteScalar<int>("EditMember", new {
            _memberid = memberid,
            _username = obj.username,
            _fullname = obj.fullname,
            _department = obj.department,
            _unit = obj.unit,
            _phone = obj.phone,
            _email = obj.email
        }, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<Member> GetMembers(){
        return connection.Query<Member>("SELECT * FROM GetMembers()").AsEnumerable();
    }
    public Member? GetMember(string memberid){
        return connection.Query<Member>("SELECT * FROM GetMember(@_memberid)", new {
            _memberid = memberid
        }).FirstOrDefault();
    }
    public int ChangePassword(ChangePassword obj){
        return connection.ExecuteScalar<int>("CALL ChangePassword(@_memid, @_oldpwd, @_newpwd)", new {
            _memid = obj.MemberId,
            _oldpwd = obj.OldPwd,
            _newpwd = obj.NewPwd
        }, commandType: CommandType.StoredProcedure);
    }
    public int Delete(string id){
        return connection.ExecuteScalar<int>("DeleteMember", new {
            _memberid = id
        }, commandType: CommandType.StoredProcedure);
    }
    public int ResetPassword(ResetPassword obj){
        return connection.ExecuteScalar<int>("ResetPassword", new{
            _memid = obj.MemberId,
            _pwd = obj.Pwd
        }, commandType: CommandType.StoredProcedure);
    }
}