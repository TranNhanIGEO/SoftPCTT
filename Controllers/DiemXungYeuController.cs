using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]")]
public class DiemXungYeuController : BaseController{
    public DiemXungYeuController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DiemXungYeu> diemXungYeus = provider.DiemXungYeu.GetDiemXungYeus(mahuyen, SqlQuery);
            if (diemXungYeus != null){
                return Ok(diemXungYeus);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            DiemXungYeu? diemXungYeu = provider.DiemXungYeu.GetDiemXungYeu(id);
            if (diemXungYeu != null){
                return Ok(diemXungYeu);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.DiemXungYeu.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(DiemXungYeu obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            DiemXungYeu? diemXungYeu = provider.DiemXungYeu.GetDiemXungYeu(obj.objectid);
            if (diemXungYeu != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.DiemXungYeu.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                DiemXungYeu GetDiemXungYeu = provider.DiemXungYeu.GetDiemXungYeu(obj.objectid)!;
                provider.History.AddHistory(idHistory, "DiemXungYeu" , GetDiemXungYeu.idxungyeu!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }
        catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, DiemXungYeu obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            DiemXungYeu? diemXungYeu = provider.DiemXungYeu.GetDiemXungYeu(id);
            if (diemXungYeu == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "DiemXungYeu" , diemXungYeu.idxungyeu!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.DiemXungYeu.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "DiemXungYeu", diemXungYeu.idxungyeu!);
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
            DiemXungYeu? diemXungYeu = provider.DiemXungYeu.GetDiemXungYeu(id);
            if (diemXungYeu == null){
                return BadRequest("ID không tồn tại");
            } 
            provider.History.AddHistory(idHistory, "DiemXungYeu" , diemXungYeu.idxungyeu!, member.username, "Xóa"); 
            int ret = provider.DiemXungYeu.Delete(id);
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
    [HttpGet("Statistics/{mahuyen}")]
    public IActionResult Statistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DiemXungYeuStatistics> statistics = provider.DiemXungYeu.GetDiemXungYeuStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
}




   