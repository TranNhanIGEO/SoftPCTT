using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class MocCanhBaoTrieuCuongController : BaseController{
    public MocCanhBaoTrieuCuongController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<MocCanhBaoTrieuCuong> mocCanhBaoTrieuCuongs = provider.MocCanhBaoTrieuCuong.GetMocCanhBaoTrieuCuongs(mahuyen, SqlQuery);
            if (mocCanhBaoTrieuCuongs != null){
                return Ok(mocCanhBaoTrieuCuongs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/{mahuyen}")]
    public IActionResult Statistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<MocCanhBaoStatistics> statistics = provider.MocCanhBaoTrieuCuong.GetMocCanhBaoStatistics(mahuyen, SqlQuery);
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
            MocCanhBaoTrieuCuong? detail = provider.MocCanhBaoTrieuCuong.GetMocCanhBaoTrieuCuong(id);
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
            int maxObjectId = provider.MocCanhBaoTrieuCuong.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(MocCanhBaoTrieuCuong obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            MocCanhBaoTrieuCuong? detail = provider.MocCanhBaoTrieuCuong.GetMocCanhBaoTrieuCuong(obj.objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.MocCanhBaoTrieuCuong.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                MocCanhBaoTrieuCuong mocCanhBaoTrieuCuong = provider.MocCanhBaoTrieuCuong.GetMocCanhBaoTrieuCuong(obj.objectid)!;
                provider.History.AddHistory(idHistory, "MocCanhBaoTrieuCuong", mocCanhBaoTrieuCuong.idmoccbtc!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, MocCanhBaoTrieuCuong obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            MocCanhBaoTrieuCuong? detail = provider.MocCanhBaoTrieuCuong.GetMocCanhBaoTrieuCuong(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "MocCanhBaoTrieuCuong", detail.idmoccbtc!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.MocCanhBaoTrieuCuong.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "MocCanhBaoTrieuCuong", detail.idmoccbtc!);
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
            MocCanhBaoTrieuCuong? detail = provider.MocCanhBaoTrieuCuong.GetMocCanhBaoTrieuCuong(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "MocCanhBaoTrieuCuong", detail.idmoccbtc!, member.username, "Xóa"); 
            int ret = provider.MocCanhBaoTrieuCuong.Delete(id);
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