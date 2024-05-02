using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class TuLieuKhacController : BaseController{
    public TuLieuKhacController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<TuLieuKhac> tuLieuKhacs = provider.TuLieuKhac.GetTuLieuKhacs(mahuyen, SqlQuery);
            if (tuLieuKhacs != null){
                return Ok(tuLieuKhacs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            TuLieuKhacDetail? only = provider.TuLieuKhac.GetTuLieuKhac(id);
            if (only != null){
                return Ok(only);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }  
    [HttpGet("PDF/{name}"), AllowAnonymous]
    public IActionResult PDF(string name){  
        try{
            string physicalPath = $"wwwroot/pdf/tulieukhac/{name}";  
            byte[] pdfBytes = System.IO.File.ReadAllBytes(physicalPath);  
            MemoryStream ms = new MemoryStream(pdfBytes);  
            return new FileStreamResult(ms, "application/pdf");  
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add([FromForm]TuLieuKhacAddEdit obj, string memberid){
        try{
            int objectid = provider.TuLieuKhac.GetMaxObjectId() + 1;
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            TuLieuKhacDetail? detail = provider.TuLieuKhac.GetTuLieuKhac(objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            } 
            // trường hợp người dùng có chọn file pdf
            if (obj.file != null && !string.IsNullOrEmpty(obj.file.FileName)){
                if (obj.file!.FileName.Length > 40){
                    return BadRequest("Tên file không được vượt quá 40 ký tự");
                }
                if (obj.file.FileName.Contains(".pdf") == false && obj.file.FileName.Contains(".PDF") == false){
                    return BadRequest("Chỉ chấp nhận file có đuôi (.pdf)");
                }     
                int ret = provider.TuLieuKhac.Add(obj, objectid, objectid + "_" + obj.file?.FileName);
                if (ret == 0){
                    string? fileName = objectid + "_" + obj.file!.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "PDF", "tulieukhac");
                    if(!Directory.Exists(path)){
                        Directory.CreateDirectory(path);
                    }
                    using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                        obj.file.CopyTo(stream);
                    }                
                    string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    TuLieuKhacDetail getTuLieuKhac = provider.TuLieuKhac.GetTuLieuKhac(objectid)!;
                    provider.History.AddHistory(idHistory, "TuLieuKhac" , getTuLieuKhac.idtulieu!, member.username, "Thêm mới");
                    return Ok("Thêm mới thành công");
                } 
                return BadRequest("Thêm mới thất bại");    
            }
            // trường hợp người dùng không chọn file pdf
            if (obj.file == null){
                int retnullfile = provider.TuLieuKhac.Add(obj, objectid, null);
                if (retnullfile == 0){
                    string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    TuLieuKhacDetail getTuLieuKhac = provider.TuLieuKhac.GetTuLieuKhac(objectid)!;
                    provider.History.AddHistory(idHistory, "TuLieuKhac", getTuLieuKhac.idtulieu!, member.username, "Thêm mới");
                    return Ok("Thêm mới thành công");
                }
                return BadRequest("Thêm mới thất bại");    
            }    
            return BadRequest("Thêm mới thất bại");    
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }    
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, [FromForm]TuLieuKhacAddEdit obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            TuLieuKhacDetail? detail = provider.TuLieuKhac.GetTuLieuKhac(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "TuLieuKhac" , detail.idtulieu!, member.username, "Chỉnh sửa/cập nhật");
            // lay ten cu cua pdf phuc vu cho xoa pdf trong wwwroot
            string? getOldName = provider.TuLieuKhac.GetOldName(id);
            // khi update người dùng có chọn file ảnh
            if (obj.file != null && !string.IsNullOrEmpty(obj.file.FileName)){
                if (obj.file!.FileName.Length > 40){
                    return BadRequest("Tên file không được vượt quá 40 ký tự");
                }
                if (obj.file.FileName.Contains(".pdf") == false && obj.file.FileName.Contains(".PDF") == false){
                    return BadRequest("Chỉ chấp nhận file có đuôi (.pdf)");
                }
                int ret = provider.TuLieuKhac.Edit(obj, id, id + "_" + obj.file?.FileName);
                if (ret == 0){
                    string? fileName = id + "_" + obj.file!.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "PDF", "tulieukhac");

                    // khi insert người dùng có chọn file pdf, khi update chọn file pdf khác => xóa pdf cũ, thêm pdf mới
                    if (getOldName != null && getOldName != fileName){
                        // Xoa file cu 
                        System.IO.File.Delete(Path.Combine(path, getOldName));
                        // them file moi
                        using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                            obj.file.CopyTo(stream);
                        } 
                    }
                    // khi insert người dùng không chọn file pdf, khi update chọn file pdf => thêm pdf mới
                    if (getOldName == null){
                        // them file moi
                        using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                            obj.file.CopyTo(stream);
                        } 
                    }
               
                    provider.History.EditHistory(idHistory, "TuLieuKhac", detail.idtulieu!);
                    return Ok("Cập nhật thành công");
                }  
                provider.History.DeletetHistory(idHistory);
                return BadRequest("Cập nhật thất bại");
            }
            // khi update người dùng có chọn file video
            if (obj.file == null){
                // khi insert người dùng có chọn file video, khi update giữ nguyên video => lấy tên video cũ gắn vào tenhinhanh
                if (getOldName != null){
                    int retoldfilename = provider.TuLieuKhac.Edit(obj, id, getOldName);
                    if (retoldfilename == 0){
                        provider.History.EditHistory(idHistory, "TuLieuKhac", detail.idtulieu!);
                        return Ok("Cập nhật thành công");
                    }
                }
                // khi insert người dùng không chọn file video, khi update không chọn video => vẫn gắn null vào tenhinhanh
                if (getOldName == null){
                    int retnullfile = provider.TuLieuKhac.Edit(obj, id, null);
                    if (retnullfile == 0){
                        provider.History.EditHistory(idHistory, "TuLieuKhac", detail.idtulieu!);
                        return Ok("Cập nhật thành công");
                    }                
                }
                provider.History.DeletetHistory(idHistory);
                return BadRequest("Cập nhật thất bại");
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
            TuLieuKhacDetail? detail = provider.TuLieuKhac.GetTuLieuKhac(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "TuLieuKhac" , detail.idtulieu!, member.username, "Xóa"); 
            // lay ten cu cua Khac phuc vu cho xoa Khac trong wwwroot
            string? getOldName = provider.TuLieuKhac.GetOldName(id);
            int ret = provider.TuLieuKhac.Delete(id);
            if (ret == 0){
                // khi insert có chọn file Khac, delete tìm file Khac và xóa đi
                if (getOldName != null){
                    string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "PDF", "tulieukhac");
                    System.IO.File.Delete(Path.Combine(path, getOldName));
                }
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