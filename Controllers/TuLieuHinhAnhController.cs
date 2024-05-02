using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class TuLieuHinhAnhController : BaseController{

    public TuLieuHinhAnhController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<TuLieuHinhAnh> tuLieuHinhAnhs = provider.TuLieuHinhAnh.GetTuLieuHinhAnhs(mahuyen, SqlQuery);
            if (tuLieuHinhAnhs != null){
                return Ok(tuLieuHinhAnhs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            TuLieuHinhAnhDetail? only = provider.TuLieuHinhAnh.GetTuLieuHinhAnh(id);
            if (only != null){
                return Ok(only);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }    
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.TuLieuHinhAnh.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }    
    [HttpGet("photo/{image}"), AllowAnonymous]
    public IActionResult ShowImage(string image){  
        try{          
            byte[] b = System.IO.File.ReadAllBytes($"./wwwroot/photo/tulieuhinhanh/{image}"); 
            return File(b, "image/jpeg");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add([FromForm]TuLieuHinhAnhAddEdit obj, string memberid){
        try{
            int objectid = provider.TuLieuHinhAnh.GetMaxObjectId() + 1;
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            TuLieuHinhAnhDetail? tuLieuHinhAnh = provider.TuLieuHinhAnh.GetTuLieuHinhAnh(objectid);
            if (tuLieuHinhAnh != null){
                return BadRequest("ID đã tồn tại");
            } 
            // trường hợp người dùng có chọn file ảnh
            if (obj.file != null && !string.IsNullOrEmpty(obj.file.FileName)){
                if (obj.file!.FileName.Length > 40){
                    return BadRequest("Tên hình ảnh không được vượt quá 40 ký tự");
                }
                if (obj.file.FileName.Contains(".jpg") == false && obj.file.FileName.Contains(".JPG") == false && obj.file.FileName.Contains(".JPEG") == false && obj.file.FileName.Contains(".jpeg") == false && obj.file.FileName.Contains(".PNG") == false && obj.file.FileName.Contains(".png") == false){
                    return BadRequest("Chỉ chấp nhận file ảnh có đuôi (.jpg, .jpeg, .png)");
                }     
                int ret = provider.TuLieuHinhAnh.Add(obj, objectid, objectid + "_" + obj.file?.FileName);
                if (ret == 0){
                    // if(obj.file != null && !string.IsNullOrEmpty(obj.file.FileName)){
                    // string ext = Path.GetExtension(obj.file!.FileName).ToLower(); // .jpg
                    string? fileName = objectid + "_" + obj.file!.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "photo", "tulieuhinhanh");
                    if(!Directory.Exists(path)){
                        Directory.CreateDirectory(path);
                    }
                    using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                        obj.file.CopyTo(stream);
                    }                
                    // }
                    string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    TuLieuHinhAnhDetail getTuLieuHinhAnh = provider.TuLieuHinhAnh.GetTuLieuHinhAnh(objectid)!;
                    provider.History.AddHistory(idHistory, "TuLieuHinhAnh" , getTuLieuHinhAnh.idhinhanh!, member.username, "Thêm mới");
                    return Ok("Thêm mới thành công");
                } 
                return BadRequest("Thêm mới thất bại");    
            }
            // trường hợp người dùng không chọn file ảnh
            if (obj.file == null){
                int retnullfile = provider.TuLieuHinhAnh.Add(obj, objectid, null);
                if (retnullfile == 0){
                    string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                    TuLieuHinhAnhDetail getTuLieuHinhAnh = provider.TuLieuHinhAnh.GetTuLieuHinhAnh(objectid)!;
                    provider.History.AddHistory(idHistory, "TuLieuHinhAnh", getTuLieuHinhAnh.idhinhanh!, member.username, "Thêm mới");
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
    public IActionResult Update(int id, [FromForm]TuLieuHinhAnhAddEdit obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            TuLieuHinhAnhDetail? tuLieuHinhAnh = provider.TuLieuHinhAnh.GetTuLieuHinhAnh(id);
            if (tuLieuHinhAnh == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "TuLieuHinhAnh" , tuLieuHinhAnh.idhinhanh!, member.username, "Chỉnh sửa/cập nhật");
            // lay ten cu cua hinh anh phuc vu cho xoa anh trong wwwroot
            string? getOldName = provider.TuLieuHinhAnh.GetOldName(id);
            // khi update người dùng có chọn file ảnh
            if (obj.file != null && !string.IsNullOrEmpty(obj.file.FileName)){
                if (obj.file!.FileName.Length > 40){
                    return BadRequest("Tên hình ảnh không được vượt quá 40 ký tự");
                }
                if (obj.file.FileName.Contains(".jpg") == false && obj.file.FileName.Contains(".JPG") == false && obj.file.FileName.Contains(".JPEG") == false && obj.file.FileName.Contains(".jpeg") == false && obj.file.FileName.Contains(".PNG") == false && obj.file.FileName.Contains(".png") == false){
                    return BadRequest("Chỉ chấp nhận file ảnh có đuôi (.jpg, .jpeg, .png)");
                }
                int ret = provider.TuLieuHinhAnh.Edit(id, obj, id + "_" + obj.file?.FileName);
                if (ret == 0){
                    string? fileName = id + "_" + obj.file!.FileName;
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photo", "tulieuhinhanh");

                    // khi insert người dùng có chọn file ảnh, khi update chọn file ảnh khác => xóa ảnh cũ, thêm ảnh mới
                    if (getOldName != null && getOldName != fileName){
                        // Xoa file cu 
                        System.IO.File.Delete(Path.Combine(path, getOldName));
                        // them file moi
                        using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                            obj.file.CopyTo(stream);
                        } 
                    }
                    // khi insert người dùng không chọn file ảnh, khi update chọn file ảnh => thêm ảnh mới
                    if (getOldName == null){
                        // them file moi
                        using(Stream stream = new FileStream(Path.Combine(path, fileName),FileMode.Create)){
                            obj.file.CopyTo(stream);
                        } 
                    }
               
                    provider.History.EditHistory(idHistory, "TuLieuHinhAnh", tuLieuHinhAnh.idhinhanh!);
                    return Ok("Cập nhật thành công");
                }  
                provider.History.DeletetHistory(idHistory);
                return BadRequest("Cập nhật thất bại");
            }
            // khi update người dùng khong chọn file ảnh
            if (obj.file == null){
                // khi insert người dùng có chọn file ảnh, khi update giữ nguyên ảnh => lấy tên ảnh cũ gắn vào tenhinhanh
                if (getOldName != null){
                    int retoldfilename = provider.TuLieuHinhAnh.Edit(id, obj, getOldName);
                    if (retoldfilename == 0){
                        provider.History.EditHistory(idHistory, "TuLieuHinhAnh", tuLieuHinhAnh.idhinhanh!);
                        return Ok("Cập nhật thành công");
                    }
                }
                // khi insert người dùng không chọn file ảnh, khi update không chọn ảnh => vẫn gắn null vào tenhinhanh
                if (getOldName == null){
                    int retnullfile = provider.TuLieuHinhAnh.Edit(id, obj, null);
                    if (retnullfile == 0){
                        provider.History.EditHistory(idHistory, "TuLieuHinhAnh", tuLieuHinhAnh.idhinhanh!);
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
            TuLieuHinhAnhDetail? tuLieuHinhAnh = provider.TuLieuHinhAnh.GetTuLieuHinhAnh(id);
            if (tuLieuHinhAnh == null){
                return BadRequest("ID không tồn tại");
            }  

            provider.History.AddHistory(idHistory, "TuLieuHinhAnh" , tuLieuHinhAnh.idhinhanh!, member.username, "Xóa"); 
            // lay ten cu cua hinh anh phuc vu cho xoa anh trong wwwroot
            string? getOldName = provider.TuLieuHinhAnh.GetOldName(id);
            int ret = provider.TuLieuHinhAnh.Delete(id);
            if (ret == 0){
                // khi insert có chọn file ảnh, delete tìm file ảnh và xóa đi
                if (getOldName != null){
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photo", "tulieuhinhanh");
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