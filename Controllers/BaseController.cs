using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

public abstract class BaseController : ControllerBase{
    protected SiteProvider provider;
    public BaseController(SiteProvider provider) => this.provider = provider;
}