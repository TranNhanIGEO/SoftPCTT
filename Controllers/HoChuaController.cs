using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class HoChuaController : BaseController{
    public HoChuaController(SiteProvider provider) : base(provider){}    
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<HoChua> hoChuas = provider.HoChua.GetHoChuas(mahuyen, SqlQuery);
            if (hoChuas != null){
                return Ok(hoChuas);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("ThuyHe")]
    public IActionResult ThuyHe(){
        try{
            IEnumerable<ThuyHeHoChua> thuyHeHoChuas = provider.HoChua.GetThuyHeHoChuas();
            if (thuyHeHoChuas != null){
                return Ok(thuyHeHoChuas);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }     
    }
    [HttpGet("Statistics/Detail/{mahuyen}")]
    public IActionResult DetailStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<ReservoirDetailStatistics> statistics = provider.HoChua.GetReservoirDetailStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpGet("Statistics/Total/{mahuyen}")]
    public IActionResult TotalStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<ReservoirTotalStatistics> statistics = provider.HoChua.GetReservoirTotalStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            HoChua? detail = provider.HoChua.GetHoChua(id);
            if (detail != null){
                return Ok(detail);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }    
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.HoChua.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(HoChua obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            HoChua? detail = provider.HoChua.GetHoChua(obj.objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.HoChua.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                HoChua hoChua = provider.HoChua.GetHoChua(obj.objectid)!;
                provider.History.AddHistory(idHistory, "HoChua" , hoChua.idhochua!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, HoChua obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            HoChua? detail = provider.HoChua.GetHoChua(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "HoChua" , detail.idhochua!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.HoChua.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "HoChua", detail.idhochua!);
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
            HoChua? detail = provider.HoChua.GetHoChua(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "HoChua" , detail.idhochua!, member.username, "Xóa"); 
            int ret = provider.HoChua.Delete(id);
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