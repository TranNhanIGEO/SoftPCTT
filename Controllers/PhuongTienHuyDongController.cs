using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class PhuongTienHuyDongController : BaseController{
    public PhuongTienHuyDongController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<PhuongTienHuyDong> phuongTienHuyDongs = provider.PhuongTienHuyDong.GetPhuongTienHuyDongs(mahuyen, SqlQuery);
            if (phuongTienHuyDongs != null){
                return Ok(phuongTienHuyDongs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            PhuongTienHuyDongAddEdit? phuongTienHuyDong = provider.PhuongTienHuyDong.GetPhuongTienHuyDong(id);
            if (phuongTienHuyDong != null){
                return Ok(phuongTienHuyDong);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.PhuongTienHuyDong.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(PhuongTienHuyDongAddEdit obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            PhuongTienHuyDongAddEdit phuongTienHuyDong = provider.PhuongTienHuyDong.GetPhuongTienHuyDong(obj.objectid)!;
            if (phuongTienHuyDong != null){
                return BadRequest("ID đã tồn tại");
            }
            int ret = provider.PhuongTienHuyDong.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                PhuongTienHuyDongAddEdit getPhuongTienHuyDong = provider.PhuongTienHuyDong.GetPhuongTienHuyDong(obj.objectid)!;
                provider.History.AddHistory(idHistory, "PhuongTienHuyDong" , getPhuongTienHuyDong.idkhphuogtien!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, PhuongTienHuyDongAddEdit obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            PhuongTienHuyDongAddEdit? phuongTienHuyDong = provider.PhuongTienHuyDong.GetPhuongTienHuyDong(id);
            if (phuongTienHuyDong == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "PhuongTienHuyDong" , phuongTienHuyDong.idkhphuogtien!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.PhuongTienHuyDong.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "PhuongTienHuyDong", phuongTienHuyDong.idkhphuogtien!);
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
            PhuongTienHuyDongAddEdit? phuongTienHuyDong = provider.PhuongTienHuyDong.GetPhuongTienHuyDong(id);
            if (phuongTienHuyDong == null){
                return BadRequest("ID không tồn tại");
            } 
            provider.History.AddHistory(idHistory, "PhuongTienHuyDong" , phuongTienHuyDong.idkhphuogtien!, member.username, "Xóa"); 

            int ret = provider.PhuongTienHuyDong.Delete(id);
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
            IEnumerable<PhuongTienHuyDongDetailStatistics> statistics = provider.PhuongTienHuyDong.GetPhuongTienHuyDongDetailStatistics(mahuyen, SqlQuery);
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
            IEnumerable<PhuongTienHuyDongTotalStatistics> statistics = provider.PhuongTienHuyDong.GetPhuongTienHuyDongTotalStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
}