using Microsoft.AspNetCore.Mvc;

namespace TODOAppBE.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        protected IActionResult Error()
        {
            return Problem();
        }
    }
}
