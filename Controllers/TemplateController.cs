using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), AllowAnonymous]
public class TemplateController : BaseController{
    public TemplateController(SiteProvider provider) : base(provider){}
    [HttpGet("{name}")]
    public IActionResult Template(string name){
        try{
            var filepath = $"wwwroot/Template/{name}";
            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, mimeType, name);
        }catch{
            return BadRequest("Xuất Excel thất bại");
        }
    }
}