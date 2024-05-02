using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class ThietHaiThienTaiController : BaseController{
    public ThietHaiThienTaiController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<ThietHaiThienTai> thietHaiThienTais = provider.ThietHaiThienTai.GetThietHaiThienTais(mahuyen, SqlQuery);
            if (thietHaiThienTais != null){
                return Ok(thietHaiThienTais);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/Detail/{mahuyen}")]
    public IActionResult DetailStatistics(string Mahuyen, string? SqlQuery){
        try{
            IEnumerable<ThietHaiThienTaiDetailStatistics> statistics = provider.ThietHaiThienTai.GetDammageDetailStatistics(Mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpGet("Statistics/Total/{mahuyen}")]
    public IActionResult TotalStatistics(string Mahuyen, string? SqlQuery){
        try{
            IEnumerable<ThietHaiThienTaiTotalStatistics> statistics = provider.ThietHaiThienTai.GetDammageTotalStatistics(Mahuyen, SqlQuery);
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
            ThietHaiThienTai? detail = provider.ThietHaiThienTai.GetThietHaiThienTai(id);
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
            int maxObjectId = provider.ThietHaiThienTai.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(ThietHaiThienTai obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            ThietHaiThienTai? detail = provider.ThietHaiThienTai.GetThietHaiThienTai(obj.objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.ThietHaiThienTai.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                ThietHaiThienTai thietHaiThienTai = provider.ThietHaiThienTai.GetThietHaiThienTai(obj.objectid)!;
                provider.History.AddHistory(idHistory, "ThietHai_ThienTai", thietHaiThienTai.idthiethai!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, ThietHaiThienTai obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            ThietHaiThienTai? detail = provider.ThietHaiThienTai.GetThietHaiThienTai(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "ThietHai_ThienTai", detail.idthiethai!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.ThietHaiThienTai.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "ThietHai_ThienTai", detail.idthiethai!);
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
            ThietHaiThienTai? detail = provider.ThietHaiThienTai.GetThietHaiThienTai(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "ThietHai_ThienTai", detail.idthiethai!, member.username, "Xóa"); 
            int ret = provider.ThietHaiThienTai.Delete(id);
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