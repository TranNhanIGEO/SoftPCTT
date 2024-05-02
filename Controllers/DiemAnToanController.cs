using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class DiemAnToanController : BaseController{
    public DiemAnToanController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DiemAnToan> diemAnToans = provider.DiemAnToan.GetDiemAnToans(mahuyen, SqlQuery);
            if (diemAnToans != null){
                return Ok(diemAnToans);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            DiemAnToan? diemAnToan = provider.DiemAnToan.GetDiemAnToan(id);
            if (diemAnToan != null){
                return Ok(diemAnToan);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(DiemAnToan obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            DiemAnToan? diemAnToan = provider.DiemAnToan.GetDiemAnToan(obj.objectid);
            if (diemAnToan != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.DiemAnToan.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                DiemAnToan GetDiemAnToan = provider.DiemAnToan.GetDiemAnToan(obj.objectid)!;
                provider.History.AddHistory(idHistory, "DiemAnToan", GetDiemAnToan.idantoan!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }
        catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, DiemAnToan obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            DiemAnToan? diemAnToan = provider.DiemAnToan.GetDiemAnToan(id);
            if (diemAnToan == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "DiemAnToan" , diemAnToan.idantoan!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.DiemAnToan.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "DiemAnToan", diemAnToan.idantoan!);
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
            DiemAnToan? diemAnToan = provider.DiemAnToan.GetDiemAnToan(id);
            if (diemAnToan == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "DiemAnToan" , diemAnToan.idantoan!, member.username, "Xóa"); 
            int ret = provider.DiemAnToan.Delete(id);
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
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.DiemAnToan.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/{mahuyen}")]
    public IActionResult Statistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DiemAnToanStatistics> statistics = provider.DiemAnToan.GetDiemAnToanStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
}