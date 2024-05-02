using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class SatLoLineController : BaseController{
    public SatLoLineController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<SatLoLine> satLoLines = provider.SatLoLine.GetSatLoLines(mahuyen, SqlQuery);
            if (satLoLines != null){
                return Ok(satLoLines);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/Detail/{mahuyen}")]
    public IActionResult Statistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<SatLoLineDetailStatistics> statistics = provider.SatLoLine.GetSatLoLineDetailStatistics(mahuyen, SqlQuery);
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
            IEnumerable<SatLoLineTotalStatistics> statistics = provider.SatLoLine.GetSatLoLineTotalStatistics(mahuyen, SqlQuery);
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
            SatLoLine? detail = provider.SatLoLine.GetSatLoLine(id);
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
            int maxObjectId = provider.SatLoLine.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(SatLoLine obj, string memberid){
        try{
            int objectid = provider.SatLoLine.GetMaxObjectId() + 1;
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            SatLoLine? detail = provider.SatLoLine.GetSatLoLine(objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.SatLoLine.Add(obj, objectid);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                SatLoLine getSatLoLine = provider.SatLoLine.GetSatLoLine(objectid)!;
                provider.History.AddHistory(idHistory, "SatLo_Line" , getSatLoLine.idsatlol!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, SatLoLine obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            SatLoLine? detail = provider.SatLoLine.GetSatLoLine(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "SatLo_Line" , detail.idsatlol!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.SatLoLine.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "SatLo_Line", detail.idsatlol!);
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
            SatLoLine? detail = provider.SatLoLine.GetSatLoLine(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            } 
            provider.History.AddHistory(idHistory, "SatLo_Line" , detail.idsatlol!, member.username, "Xóa"); 
            int ret = provider.SatLoLine.Delete(id);
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