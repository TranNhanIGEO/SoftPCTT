using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class TuLieuVideoController : BaseController{
    public TuLieuVideoController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<TuLieuVideo> tuLieuVideos = provider.TuLieuVideo.GetTuLieuVideos(mahuyen, SqlQuery);
            if (tuLieuVideos != null){
                return Ok(tuLieuVideos);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            TuLieuVideoDetail? only = provider.TuLieuVideo.GetTuLieuVideo(id);
            if (only != null){
                return Ok(only);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }     
    [HttpGet("media/{video}"), AllowAnonymous]
    public IActionResult ShowVideo(string video){        
        try{    
            byte[] b = System.IO.File.ReadAllBytes($"./wwwroot/media/{video}"); 
            return File(b, "video/mp4");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add([FromForm]TuLieuVideoAddEdit obj, string memberid){
        try{
            int objectid = provider.TuLieuVideo.GetMaxObjectId() + 1;
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            TuLieuVideoDetail? detail = provider.TuLieuVideo.GetTuLieuVideo(objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            } 
            // trường hợp người dùng có chọn file video
            if (obj.file != null && !string.IsNullOrEmpty(obj.file.FileName)){
                if (obj.file!.FileName.Length > 40){
                    return BadRequest("Tên video không được vượt quá 40 ký tự");
                }
                if (obj.file.FileName.Contains(".mp4") == false && obj.file.FileName.Contains(".MP4") == false && obj.file.FileName.Contains(".mov") == false && obj.file.FileName.Contains(".MOV") == false){
                    return BadRequest("Chỉ chấp nhận file video có đuôi (.mp4, .mov)");
                }     
                int ret = provider.TuLieuVideo.Add(obj, objectid, objectid + "_" + obj.file?.FileName);
                if (ret == 0){
                    string? fileName = objectid + "_" + obj.file!.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "media");
                    if(!Directory.Exists(path)){
                        Directory.CreateDirectory(path);
                    }
                    using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                        obj.file.CopyTo(stream);
                    }                
                    string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    TuLieuVideoDetail getTuLieuVideo = provider.TuLieuVideo.GetTuLieuVideo(objectid)!;
                    provider.History.AddHistory(idHistory, "TuLieuVideo" , getTuLieuVideo.idvideo!, member.username, "Thêm mới");
                    return Ok("Thêm mới thành công");
                } 
                return BadRequest("Thêm mới thất bại");    
            }
            // trường hợp người dùng không chọn file video
            if (obj.file == null){
                int retnullfile = provider.TuLieuVideo.Add(obj, objectid, null);
                if (retnullfile == 0){
                    string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    TuLieuVideoDetail getTuLieuVideo = provider.TuLieuVideo.GetTuLieuVideo(objectid)!;
                    provider.History.AddHistory(idHistory, "TuLieuVideo", getTuLieuVideo.idvideo!, member.username, "Thêm mới");
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
    public IActionResult Update(int id, [FromForm]TuLieuVideoAddEdit obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            TuLieuVideoDetail? detail = provider.TuLieuVideo.GetTuLieuVideo(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "TuLieuVideo" , detail.idvideo!, member.username, "Chỉnh sửa/cập nhật");
            // lay ten cu cua video phuc vu cho xoa video trong wwwroot
            string? getOldName = provider.TuLieuVideo.GetOldName(id);
            // khi update người dùng có chọn file ảnh
            if (obj.file != null && !string.IsNullOrEmpty(obj.file.FileName)){
                if (obj.file!.FileName.Length > 40){
                    return BadRequest("Tên video không được vượt quá 40 ký tự");
                }
                if (obj.file.FileName.Contains(".mp4") == false && obj.file.FileName.Contains(".MP4") == false && obj.file.FileName.Contains(".mov") == false && obj.file.FileName.Contains(".MOV") == false){
                    return BadRequest("Chỉ chấp nhận file video có đuôi (.mp4, .mov)");
                } 
                int ret = provider.TuLieuVideo.Edit(obj, id, id + "_" + obj.file?.FileName);
                if (ret == 0){
                    string? fileName = id + "_" + obj.file!.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "media");

                    // khi insert người dùng có chọn file video, khi update chọn file video khác => xóa video cũ, thêm video mới
                    if (getOldName != null && getOldName != fileName){
                        // Xoa file cu 
                        System.IO.File.Delete(Path.Combine(path, getOldName));
                        // them file moi
                        using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                            obj.file.CopyTo(stream);
                        } 
                    }
                    // khi insert người dùng không chọn file video, khi update chọn file video => thêm video mới
                    if (getOldName == null){
                        // them file moi
                        using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                            obj.file.CopyTo(stream);
                        } 
                    }
               
                    provider.History.EditHistory(idHistory, "TuLieuVideo", detail.idvideo!);
                    return Ok("Cập nhật thành công");
                }  
                provider.History.DeletetHistory(idHistory);
                return BadRequest("Cập nhật thất bại");
            }
            // khi update người dùng có chọn file video
            if (obj.file == null){
                // khi insert người dùng có chọn file video, khi update giữ nguyên video => lấy tên video cũ gắn vào tenhinhanh
                if (getOldName != null){
                    int retoldfilename = provider.TuLieuVideo.Edit(obj, id, getOldName);
                    if (retoldfilename == 0){
                        provider.History.EditHistory(idHistory, "TuLieuVideo", detail.idvideo!);
                        return Ok("Cập nhật thành công");
                    }
                }
                // khi insert người dùng không chọn file video, khi update không chọn video => vẫn gắn null vào tenhinhanh
                if (getOldName == null){
                    int retnullfile = provider.TuLieuVideo.Edit(obj, id, null);
                    if (retnullfile == 0){
                        provider.History.EditHistory(idHistory, "TuLieuVideo", detail.idvideo!);
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
            TuLieuVideoDetail? detail = provider.TuLieuVideo.GetTuLieuVideo(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            }  

            provider.History.AddHistory(idHistory, "TuLieuVideo" , detail.idvideo!, member.username, "Xóa"); 
            // lay ten cu cua video phuc vu cho xoa video trong wwwroot
            string? getOldName = provider.TuLieuVideo.GetOldName(id);
            int ret = provider.TuLieuVideo.Delete(id);
            if (ret == 0){
                // khi insert có chọn file video, delete tìm file video và xóa đi
                if (getOldName != null){
                    string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "media");
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