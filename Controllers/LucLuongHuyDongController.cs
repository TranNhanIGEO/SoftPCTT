using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class LucLuongHuyDongController : BaseController{
    public LucLuongHuyDongController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<LucLuongHuyDong> lucLuongHuyDongs = provider.LucLuongHuyDong.GetLucLuongHuyDongs(mahuyen, SqlQuery);
            if (lucLuongHuyDongs != null){
                return Ok(lucLuongHuyDongs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            LucLuongHuyDongAddEdit? lucLuongHuyDong = provider.LucLuongHuyDong.GetLucLuongHuyDong(id);
            if (lucLuongHuyDong != null){
                return Ok(lucLuongHuyDong);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("History"), Authorize(Roles = "856531")]
    public IActionResult History(){
        try{
            IEnumerable<LucLuongHuyDongHistory> lucLuongHuyDongHistorys = provider.LucLuongHuyDong.GetLucLuongHuyDongHistory();
            if (lucLuongHuyDongHistorys != null){
                return Ok(lucLuongHuyDongHistorys);
            }
                return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }         
    }
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.LucLuongHuyDong.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(LucLuongHuyDongAddEdit obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            LucLuongHuyDongAddEdit? lucLuongHuyDong = provider.LucLuongHuyDong.GetLucLuongHuyDong(obj.objectid);
            if (lucLuongHuyDong != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.LucLuongHuyDong.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                LucLuongHuyDongAddEdit getLucLuongHuyDong = provider.LucLuongHuyDong.GetLucLuongHuyDong(obj.objectid)!;
                provider.History.AddHistory(idHistory, "LucLuongHuyDong" , getLucLuongHuyDong.idkhlucluong!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }
        catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, LucLuongHuyDongAddEdit obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            LucLuongHuyDongAddEdit? lucLuongHuyDong = provider.LucLuongHuyDong.GetLucLuongHuyDong(id);
            if (lucLuongHuyDong == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "LucLuongHuyDong" , lucLuongHuyDong.idkhlucluong!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.LucLuongHuyDong.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "LucLuongHuyDong", lucLuongHuyDong.idkhlucluong!);
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
            LucLuongHuyDongAddEdit? lucLuongHuyDong = provider.LucLuongHuyDong.GetLucLuongHuyDong(id);
            if (lucLuongHuyDong == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "LucLuongHuyDong" , lucLuongHuyDong.idkhlucluong!, member.username, "Xóa"); 
            int ret = provider.LucLuongHuyDong.Delete(id);
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
    [HttpGet("Statistics/Detail/{mahuyen}")]
    public IActionResult DetailStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<LucLuongHuyDongDetailStatistics> statistics = provider.LucLuongHuyDong.GetLucLuongHuyDongDetailStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpGet("Statistics/Total/{mahuyen}")]
    public IActionResult ToTalStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<LucLuongHuyDongTotalStatistics> statistics = provider.LucLuongHuyDong.GetLucLuongHuyDongTotalStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
}