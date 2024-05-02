using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class HuongDiChuyenController : BaseController{
    public HuongDiChuyenController(SiteProvider provider) : base(provider){}
    [HttpGet("PDF/{mahuyen}"), AllowAnonymous]
    public IActionResult PhysicalLocation(string mahuyen){  
        try{
            string physicalPath = $"wwwroot/pdf/huongdichuyen/{mahuyen}";  
            byte[] pdfBytes = System.IO.File.ReadAllBytes(physicalPath);  
            MemoryStream ms = new MemoryStream(pdfBytes);  
            return new FileStreamResult(ms, "application/pdf");  
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){  
        try{
            IEnumerable<HuongDiChuyen> huongDiChuyens = provider.HuongDiChuyen.GetHuongDiChuyens(mahuyen, SqlQuery);
            if (huongDiChuyens != null){
                return Ok(huongDiChuyens);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }  
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            HuongDiChuyen? huongDiChuyen = provider.HuongDiChuyen.GetHuongDiChuyen(id);
            if (huongDiChuyen != null){
                return Ok(huongDiChuyen);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.HuongDiChuyen.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(HuongDiChuyen obj, string memberid){
        // try{
            int objectid = provider.HuongDiChuyen.GetMaxObjectId() + 1;
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            HuongDiChuyen? detail = provider.HuongDiChuyen.GetHuongDiChuyen(objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.HuongDiChuyen.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                HuongDiChuyen getHuongDiChuyen = provider.HuongDiChuyen.GetHuongDiChuyen(objectid)!;
                provider.History.AddHistory(idHistory, "HuongDiChuyen", getHuongDiChuyen.iddichuyen!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        // }catch{
        //     return BadRequest("Thêm mới thất bại");
        // }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, HuongDiChuyen obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            HuongDiChuyen? detail = provider.HuongDiChuyen.GetHuongDiChuyen(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "HuongDiChuyen" , detail.iddichuyen!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.HuongDiChuyen.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "HuongDiChuyen", detail.iddichuyen!);
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
            HuongDiChuyen? HuongDiChuyen = provider.HuongDiChuyen.GetHuongDiChuyen(id);
            if (HuongDiChuyen == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "HuongDiChuyen" , HuongDiChuyen.iddichuyen!, member.username, "Xóa"); 
            int ret = provider.HuongDiChuyen.Delete(id);
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