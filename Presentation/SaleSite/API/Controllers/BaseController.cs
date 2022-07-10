using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected const string secretKey = "";
    }
}
