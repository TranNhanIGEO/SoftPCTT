using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class MemberRoleRepository : BaseRepository{
    public MemberRoleRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<MemberRole> GetMemberRoles(string pagename){
        return connection.Query<MemberRole>("SELECT * FROM GetMemberRoles(@_pagename)", new{
            _pagename = pagename
        }, commandType: CommandType.Text);
    }
    public int Add(MemberRoleAddEdit obj){
        return connection.ExecuteScalar<int>("AddMemberRoles", new{
            _memberid = obj.memberid, 
            _roleid = obj.roleid,
            _mahuyen = obj.mahuyen,
            _malopdulieu = obj.malopdulieu
        }, commandType: CommandType.StoredProcedure);
    }
    public int DeleteMemberRole(string memid){
        return connection.ExecuteScalar<int>("DeleteMemberRole", new{
            _memberid = memid,
        }, commandType: CommandType.StoredProcedure);
    }
    public IEnumerable<RoleByMember> GetRoleByMember(string txt){
        return connection.Query<RoleByMember>("SELECT * FROM GetRoleByMember(@_memberid)", new {
            _memberid = txt
        });
    }
}