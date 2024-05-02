using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "856531")]
public class AcCountController : BaseController{
    public AcCountController(SiteProvider provider) : base(provider){}
    [HttpPost("Add")]
    public IActionResult Add(AddMember obj){  
        try{
            Member? Member = provider.Member.GetMember(obj.memberid);
            if (Member != null){
                return BadRequest("ID đã tồn tại");
            } 
            int ret = provider.Member.Add(obj);
            if (ret == 0){
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }
        catch{
            return BadRequest("Thêm mới thất bại");
        }
    } 
    [HttpPut("Update/{memberid}")]
    public IActionResult Update(Member obj, string memberid){
        try{
            if (memberid != obj.memberid){
                return NotFound();
            }
            int ret = provider.Member.Update(obj, memberid);
            if (ret == 0){
                return Ok("Cập nhật thành công");
            }
            return BadRequest("Cập nhật thất bại");
        }catch{
            return BadRequest("Cập nhật thất bại");
        }
    }
    [HttpPut("ChangePassword/{id}")]
    public IActionResult ChangePassword(ChangePassword obj, string id){
        try{
            if (id != obj.MemberId){
                return NotFound();
            }
            int ret = provider.Member.ChangePassword(obj);
            if (ret == 0){
                return Ok("Thay đổi mật khẩu thành côngg");
            }
            return BadRequest("Thay đổi mật khẩu thất bại");
        }catch{
            return BadRequest("Thay đổi mật khẩu thất bại");
        }
    }

    [HttpGet]
    public IActionResult Get(){
        try{
            IEnumerable<Member> Members = provider.Member.GetMembers();
            if (Members != null){
                return Ok(Members);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("{id}")]
    public IActionResult Get(string id){
        try{
            Member? member = provider.Member.GetMember(id);            
            if (member != null){
                return Ok(member);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(string id){
        try{
            Member? member = provider.Member.GetMember(id);
            if (member == null){
                return BadRequest("ID không tồn tại");
            }  
            int ret = provider.Member.Delete(id);
            if (ret == 0){
                return Ok("Xóa thành công");
            }
            return BadRequest("Xóa thất bại");
        }catch{
            return BadRequest("Xóa thất bại");
        }     
    }
    [HttpPost("ResetPassword")]
    public IActionResult ResetPassword(ResetPassword obj){
        try{
            int ret = provider.Member.ResetPassword(obj);
            if (ret == 0){
                return Ok("Đặt lại mật khẩu thành công");
            }
            return BadRequest("Đặt lại mật khẩu thất bại");
        }catch{
            return BadRequest("Đặt lại mật khẩu thất bại");
        }
    }
}