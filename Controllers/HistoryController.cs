using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), Authorize(Roles = "856531")]
public class HistoryController : BaseController{
    public HistoryController(SiteProvider provider) : base(provider){}
    [HttpGet("{tablename}")]
    public IActionResult Get(string tablename){
        try{
            IEnumerable<History> histories = provider.History.GetHistorys(tablename);
            if (histories != null){
                return Ok(histories);
            }
            return BadRequest("Tìm kiếm thất bại");
        }catch{
            return BadRequest("Tìm kiếm thất bại");
        }
    }
}