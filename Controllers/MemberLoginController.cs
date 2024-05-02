using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;
[ApiController, Route("api/[controller]")]
public class MemberLoginController : BaseController{
    public MemberLoginController(SiteProvider provider) : base(provider){}

    // [HttpGet("{id}")]
    // public MemberLogin? Get(int id){
    //     return provider.MemberLogin.GetMemberLogin(id);
    // }
    [HttpGet("CountLogin")]
    public IEnumerable<CountLogin> CountLogin(){
        return provider.MemberLogin.CountLogins();
    }
}