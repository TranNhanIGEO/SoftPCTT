using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]"), AllowAnonymous]
public class ReportController : BaseController{
    public ReportController(SiteProvider provider) : base(provider){}
    [HttpGet("{name}")]
    public IActionResult Report(string name){
        try{
            var filepath = $"wwwroot/Report/{name}";
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            if (name.Contains(".docx")){
                var mimeTypeW = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                return File(fileBytes, mimeTypeW, name);
            }
            var mimeTypeE = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(fileBytes, mimeTypeE, name);

        }catch{
            return BadRequest("Xuất báo cáo thất bại");
        }
    }
}

