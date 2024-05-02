using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class GiaoThongController : BaseController{
    public GiaoThongController(SiteProvider provider) : base(provider){}
    [HttpGet("{mahuyen}")]
    public IActionResult Get(string mahuyen){
        try{
            IEnumerable<GiaoThong> giaoThongs = provider.GiaoThong.GetGiaoThongs(mahuyen);
            if (giaoThongs != null){
                return Ok(giaoThongs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
}