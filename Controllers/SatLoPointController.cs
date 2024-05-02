using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "959610")]
public class SatLoPointController : BaseController{
    public SatLoPointController(SiteProvider provider) : base(provider){}
    [HttpGet("GetAll/{mahuyen}")]
    public IActionResult GetAll(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<SatLoPoint> satLoPoints = provider.SatLoPoint.GetSatLoPoints(mahuyen, SqlQuery);
            if (satLoPoints != null){
                return Ok(satLoPoints);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
    [HttpGet("Statistics/Detail/{mahuyen}")]
    public IActionResult Statistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<SatLoPointDetailStatistics> statistics = provider.SatLoPoint.GetSatLoPointDetailStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
    [HttpGet("Statistics/Total/{mahuyen}")]
    public IActionResult TotalStatistics(string mahuyen, string? SqlQuery){
        try{
            IEnumerable<SatLoPointTotalStatistics> statistics = provider.SatLoPoint.GetSatLoPointTotalStatistics(mahuyen, SqlQuery);
            if (statistics != null){
                return Ok(statistics);
            }
            return BadRequest("Thống kê thất bại");
        }catch{
            return BadRequest("Thống kê thất bại");
        }
    }
}

