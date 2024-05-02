using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize]
public class QuanDaoHSTSController : BaseController{
    public QuanDaoHSTSController(SiteProvider provider) : base(provider){}
    [HttpGet]
    public IActionResult Get(){
        try{
            IEnumerable<QuanDaoHSTS> QuanDaoHSTSs = provider.QuanDaoHSTS.GetQuanDaoHSTSs();
            if (QuanDaoHSTSs != null){
                return Ok(QuanDaoHSTSs);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
}