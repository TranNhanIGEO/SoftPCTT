using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize]
public class MemberRoleController : BaseController{
    public MemberRoleController(SiteProvider provider) : base(provider){}
    [HttpGet("{pagename}"), Authorize(Roles = "959610")]
    public IEnumerable<MemberRole> Get(string pagename){
        return provider.MemberRole.GetMemberRoles(pagename);
    }
    [HttpGet("GetRoleByMember/{id}"), Authorize(Roles = "959610")]
    public IEnumerable<RoleByMember> GetRoleByMember(string id){
        return provider.MemberRole.GetRoleByMember(id);
    }
    [HttpPost("Add"), Authorize(Roles = "856531")]
    public IActionResult Add(MemberRoleAddEdit obj){
        int result = provider.MemberRole.Add(obj);
        if (result == 0){
            return Ok();
        } 
        return BadRequest();
    }
    [HttpDelete("DeleteMemberRole/{memid}"), Authorize(Roles = "856531")]
    public IActionResult DeleteMemberRole(string memid){
        int ret = provider.MemberRole.DeleteMemberRole(memid);
        if (ret == 0){
            return Ok("Xóa quyền người dùng thành công");
        }
        return BadRequest("Xóa quyền người dùng thất bại");
    }
}