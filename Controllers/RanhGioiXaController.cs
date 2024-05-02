using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize]
public class RanhGioiXaController : BaseController{
    public RanhGioiXaController(SiteProvider provider) : base(provider){}
    [HttpGet("{id}")]
    public IActionResult Get(string id){
        try{
            IEnumerable<RanhGioiXa> xa = provider.RanhGioiXa.GetXas();
            if (xa != null){
                return Ok(xa);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
}