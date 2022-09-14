using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebApp.Controllers
{
    [Authorize]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public  class BaseController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        // GET: BaseController1

    }
}
