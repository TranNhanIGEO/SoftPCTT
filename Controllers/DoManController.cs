using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class DoManController : BaseController{
    public DoManController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DoMan> DoMans = provider.DoMan.GetDoMans(mahuyen, SqlQuery);
            if (DoMans != null){
                return Ok(DoMans);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            DoMan? detail = provider.DoMan.GetDoMan(id);
            if (detail != null){
                return Ok(detail);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/Detail/{mahuyen}")]
    public IActionResult DetailStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<SalinityDetailStatistics> salinityStatistics = provider.DoMan.GetSalinityDetailStatistics(mahuyen, SqlQuery);
            if (salinityStatistics != null){
                return Ok(salinityStatistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpGet("Statistics/Total/{mahuyen}")]
    public IActionResult TotalStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<SalinityTotalStatistics> salinityStatistics = provider.DoMan.GetSalinityTotalStatistics(mahuyen, SqlQuery);
            if (salinityStatistics != null){
                return Ok(salinityStatistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.DoMan.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    } 
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(DoMan obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            DoMan? detail = provider.DoMan.GetDoMan(obj.objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.DoMan.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                DoMan doMan = provider.DoMan.GetDoMan(obj.objectid)!;
                provider.History.AddHistory(idHistory, "DoMan" , doMan.idtramman!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, DoMan obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            DoMan? detail = provider.DoMan.GetDoMan(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "DoMan" , detail.idtramman!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.DoMan.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "DoMan", detail.idtramman!);
                return Ok("Cập nhật thành công");
            }
            provider.History.DeletetHistory(idHistory);
            return BadRequest("Cập nhật thất bại");
        }catch{
            provider.History.DeletetHistory(idHistory);
            return BadRequest("Cập nhật thất bại");
        }
    }
    [HttpDelete("Delete/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Delete(int id, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            DoMan? detail = provider.DoMan.GetDoMan(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "DoMan" , detail.idtramman!, member.username, "Xóa"); 
            int ret = provider.DoMan.Delete(id);
            if (ret == 0){
                return Ok("Xóa thành công");
            }
            provider.History.DeletetHistory(idHistory);
            return BadRequest("Xóa thất bại");
        }catch{
            provider.History.DeletetHistory(idHistory);
            return BadRequest("Xóa thất bại");
        }
    }
} 