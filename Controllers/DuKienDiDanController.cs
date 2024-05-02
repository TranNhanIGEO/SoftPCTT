
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class DuKienDiDanController : BaseController{
    public DuKienDiDanController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DuKienDiDan> duKienDiDans = provider.DuKienDiDan.GetDuKienDiDans(mahuyen, SqlQuery);
            if (duKienDiDans != null){
                return Ok(duKienDiDans);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            DuKienDiDan? duKienDiDan = provider.DuKienDiDan.GetDuKienDiDan(id);
            if (duKienDiDan != null){
                return Ok(duKienDiDan);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }  

    [HttpGet("Statistics/{mahuyen}")]
    public IActionResult Statistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DuKienDiDanStatistics> statistics = provider.DuKienDiDan.GetDuKienDiDanStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }

    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.DuKienDiDan.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(DuKienDiDan obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            DuKienDiDan? duKienDiDan = provider.DuKienDiDan.GetDuKienDiDan(obj.objectid);
            if (duKienDiDan != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.DuKienDiDan.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                DuKienDiDan dukiendidan = provider.DuKienDiDan.GetDuKienDiDan(obj.objectid)!;
                provider.History.AddHistory(idHistory, "DuKienDiDan" , dukiendidan.idkhsotan!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }
        catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, DuKienDiDan obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            DuKienDiDan? duKienDiDan = provider.DuKienDiDan.GetDuKienDiDan(id);
            if (duKienDiDan == null){
                return BadRequest("ID Không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "DuKienDiDan" , duKienDiDan.idkhsotan!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.DuKienDiDan.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "DuKienDiDan", duKienDiDan.idkhsotan!);
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
            DuKienDiDan? duKienDiDan = provider.DuKienDiDan.GetDuKienDiDan(id);
            if (duKienDiDan == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "DuKienDiDan" , duKienDiDan.idkhsotan!, member.username, "Xóa"); 
            int ret = provider.DuKienDiDan.Delete(id);
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