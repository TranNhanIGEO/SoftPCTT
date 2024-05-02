using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class ApThapNhietDoiController : BaseController{
    public ApThapNhietDoiController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<ApThapNhietDoi> apThapNhietDois = provider.ApThapNhietDoi.GetApThapNhietDois(mahuyen, SqlQuery);
            if (apThapNhietDois != null){
                return Ok(apThapNhietDois);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/{mahuyen}")]
    public IActionResult Statistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<TropicalDepressionStatistics> statistics = provider.ApThapNhietDoi.GetTropicalDepressionStatistics(mahuyen, SqlQuery);
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
            ApThapNhietDoiDetail? detail = provider.ApThapNhietDoi.GetApThapNhietDoi(id);
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
            int maxObjectId = provider.ApThapNhietDoi.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    } 
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(ApThapNhietDoi obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            ApThapNhietDoiDetail? detail = provider.ApThapNhietDoi.GetApThapNhietDoi(obj.objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.ApThapNhietDoi.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                ApThapNhietDoiDetail apThapNhietDoi = provider.ApThapNhietDoi.GetApThapNhietDoi(obj.objectid)!;
                provider.History.AddHistory(idHistory, "ApThapNhietDoi" , apThapNhietDoi.idapthap!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, ApThapNhietDoi obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            ApThapNhietDoiDetail? detail = provider.ApThapNhietDoi.GetApThapNhietDoi(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "ApThapNhietDoi" , detail.idapthap!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.ApThapNhietDoi.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "ApThapNhietDoi", detail.idapthap!);
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
            ApThapNhietDoiDetail? detail = provider.ApThapNhietDoi.GetApThapNhietDoi(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "ApThapNhietDoi" , detail.idapthap!, member.username, "Xóa"); 
            int ret = provider.ApThapNhietDoi.Delete(id);
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