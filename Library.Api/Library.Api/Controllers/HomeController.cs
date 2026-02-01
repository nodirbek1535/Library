//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : RESTFulController
    {
        [HttpGet]
        public ActionResult<string> Get() => 
            Ok("Library API is running.");
    }
}
