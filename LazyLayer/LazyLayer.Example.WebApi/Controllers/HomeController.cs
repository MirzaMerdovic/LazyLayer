using System.Web.Http;

namespace LazyLayer.Example.WebApi.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Web API Started successfully";
        }
    }
}