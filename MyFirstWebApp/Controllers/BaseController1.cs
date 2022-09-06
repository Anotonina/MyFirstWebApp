using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebApp.Controllers
{
    [Authorize]
    public  class BaseController : Controller
    {
        // GET: BaseController1

    }
}
