using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class DanhBaDTController : BaseController{
    public DanhBaDTController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string SqlQuery){
        try{
            IEnumerable<DanhBaDT> danhBaDTs = provider.DanhBaDT.GetDanhBaDTs(mahuyen, SqlQuery);
            if (danhBaDTs != null){
                return Ok(danhBaDTs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("GetOnly/{id}")]
    public IActionResult GetOnly(int id){
        try{
            DanhBaDTAddEdit? danhBaDT = provider.DanhBaDT.GetDanhBaDT(id);
            if (danhBaDT != null){
                return Ok(danhBaDT);
            }
            return BadRequest("ID không tồn tại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("getMaxId")]
    public IActionResult GetMaxId(){
        try{
            int maxObjectId = provider.DanhBaDT.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(DanhBaDTAddEdit obj, string memberid){
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            DanhBaDTAddEdit? danhBaDT = provider.DanhBaDT.GetDanhBaDT(obj.objectid);
            if (danhBaDT != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.DanhBaDT.Add(obj);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);    
                DanhBaDTAddEdit GetDanhBa = provider.DanhBaDT.GetDanhBaDT(obj.objectid)!;
                provider.History.AddHistory(idHistory, "DanhBaDT" , GetDanhBa.iddanhba!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }
        catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, DanhBaDTAddEdit obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            DanhBaDTAddEdit? danhBaDT = provider.DanhBaDT.GetDanhBaDT(id);
            if (danhBaDT == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "DanhBaDT" , danhBaDT.iddanhba!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.DanhBaDT.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "DanhBaDT", danhBaDT.iddanhba!);
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
            DanhBaDTAddEdit? danhBaDT = provider.DanhBaDT.GetDanhBaDT(id);
            if (danhBaDT == null){
                return BadRequest("ID không tồn tại");
            }  
            provider.History.AddHistory(idHistory, "DanhBaDT" , danhBaDT.iddanhba!, member.username, "Xóa");

            int ret = provider.DanhBaDT.Delete(id);
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