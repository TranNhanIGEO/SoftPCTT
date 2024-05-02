using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class MucNuocController : BaseController{
    public MucNuocController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<MucNuoc> mucNuocs = provider.MucNuoc.GetMucNuocs(mahuyen, SqlQuery);
            if (mucNuocs != null){
                return Ok(mucNuocs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            MucNuoc? detail = provider.MucNuoc.GetMucNuoc(id);
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
            int maxObjectId = provider.MucNuoc.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/Detail/{mahuyen}")]
    public IActionResult DetailStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<TidalDetailStatistics> tidalStatistics = provider.MucNuoc.GetTidalDetailStatistics(mahuyen, SqlQuery);
            if (tidalStatistics != null){
                return Ok(tidalStatistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpGet("Statistics/Total/{mahuyen}")]
    public IActionResult TotalStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<TidalTotalStatistics> tidalStatistics = provider.MucNuoc.GetTidalTotalStatistics(mahuyen, SqlQuery);
            if (tidalStatistics != null){
                return Ok(tidalStatistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(MucNuoc obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            MucNuoc? detail = provider.MucNuoc.GetMucNuoc(obj.objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.MucNuoc.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                MucNuoc mucNuoc = provider.MucNuoc.GetMucNuoc(obj.objectid)!;
                provider.History.AddHistory(idHistory, "MucNuoc" , mucNuoc.idtrammucnuoc!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, MucNuoc obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            MucNuoc? detail = provider.MucNuoc.GetMucNuoc(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "MucNuoc" , detail.idtrammucnuoc!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.MucNuoc.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "MucNuoc", detail.idtrammucnuoc!);
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
            MucNuoc? detail = provider.MucNuoc.GetMucNuoc(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "MucNuoc" , detail.idtrammucnuoc!, member.username, "Xóa"); 
            int ret = provider.MucNuoc.Delete(id);
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


   