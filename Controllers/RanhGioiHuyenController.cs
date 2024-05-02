using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize]
public class RanhGioiHuyenController : BaseController{
    public RanhGioiHuyenController(SiteProvider provider) : base(provider){}
    public IEnumerable<RanhGioiHuyen> Get(){
        return provider.RanhGioiHuyen.GetHuyens();
    }
    [HttpGet("{id}")]
    public IActionResult Get(string id){
        try{
            RanhGioiHuyen? huyen = provider.RanhGioiHuyen.GetHuyen(id);
            if (huyen != null){
                return Ok(huyen);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
}