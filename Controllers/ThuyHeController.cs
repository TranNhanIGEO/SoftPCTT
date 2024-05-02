using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize]
public class ThuyHeController : BaseController{
    public ThuyHeController(SiteProvider provider) : base(provider){}
    [HttpGet("{tenhuyen}")]
    public IActionResult Get(string tenhuyen){
        try{
            IEnumerable<ThuyHe> thuyHes = provider.ThuyHe.GetThuyHes(tenhuyen);
            if (thuyHes != null){
                return Ok(thuyHes);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
}