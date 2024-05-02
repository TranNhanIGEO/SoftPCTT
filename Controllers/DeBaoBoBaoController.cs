using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class DeBaoBoBaoController : BaseController{
    public DeBaoBoBaoController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DeBaoBoBao> deBaoBoBaos = provider.DeBaoBoBao.GetDeBaoBoBaos(mahuyen, SqlQuery);
            if (deBaoBoBaos != null){
                return Ok(deBaoBoBaos);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/{mahuyen}")]
    public IActionResult Statistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<DebaoBoBaoStatistics> statistics = provider.DeBaoBoBao.GetDebaoBoBaoStatistics(mahuyen, SqlQuery);
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
            DeBaoBoBao? detail = provider.DeBaoBoBao.GetDeBaoBoBao(id);
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
            int maxObjectId = provider.DeBaoBoBao.GetMaxObjectId();
            return Ok(maxObjectId);
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpPost("Add/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Add(DeBaoBoBao obj, string memberid){
        try{
            int objectid = provider.DeBaoBoBao.GetMaxObjectId() + 1;
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            DeBaoBoBao? detail = provider.DeBaoBoBao.GetDeBaoBoBao(objectid);
            if (detail != null){
                return BadRequest("ID đã tồn tại");
            }  
            int ret = provider.DeBaoBoBao.Add(obj, objectid);
            if (ret == 0){
                string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
                DeBaoBoBao getDeBaoBoBao = provider.DeBaoBoBao.GetDeBaoBoBao(objectid)!;
                provider.History.AddHistory(idHistory, "DeBaoBoBao" , getDeBaoBoBao.idkenhmuong!, member.username, "Thêm mới");
                return Ok("Thêm mới thành công");
            }
            return BadRequest("Thêm mới thất bại");
        }catch{
            return BadRequest("Thêm mới thất bại");
        }
    }
    [HttpPut("Update/{id}/{memberid}"), Authorize(Roles = "365778")]
    public IActionResult Update(int id, DeBaoBoBao obj, string memberid){
        string idHistory = Guid.NewGuid().ToString().Replace("-", string.Empty);
        try{
            Member? member = provider.Member.GetMember(memberid);
            if (member == null){
                return BadRequest("Người dùng không tồn tại");
            }
            if (id != obj.objectid){
                return BadRequest("ID Không tồn tại");
            }
            DeBaoBoBao? detail = provider.DeBaoBoBao.GetDeBaoBoBao(id);
            if (detail == null){
                return BadRequest("ID Không tồn tại");
            }    
            provider.History.AddHistory(idHistory, "DeBaoBoBao" , detail.idkenhmuong!, member.username, "Chỉnh sửa/cập nhật");
            int ret = provider.DeBaoBoBao.Edit(id, obj);
            if (ret == 0){
                provider.History.EditHistory(idHistory, "DeBaoBoBao", detail.idkenhmuong!);
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
            DeBaoBoBao? detail = provider.DeBaoBoBao.GetDeBaoBoBao(id);
            if (detail == null){
                return BadRequest("ID không tồn tại");
            } 
            provider.History.AddHistory(idHistory, "DeBaoBoBao" , detail.idkenhmuong!, member.username, "Xóa"); 
            int ret = provider.DeBaoBoBao.Delete(id);
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