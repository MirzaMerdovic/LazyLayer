using System.Threading.Tasks;
using System.Web.Http;
using LazyLayer.Core.Providers;
using LazyLayer.Example.Core;
using LazyLayer.Http;

namespace LazyLayer.Example.WebApi.Controllers
{
    [RoutePrefix("values")]
    public class ValuesController : LazyController
    {
        private readonly ILogProvider _logger;
        private readonly IValueService _service = new ValueService();

        public ValuesController(ILogProvider logger) : base(null, logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var response = await ExecuteAsync(_service.GetAsync);

            return response;
        }

        [HttpGet, Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var response = await ExecuteAsync(id, _service.GetAsync);

            return response;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(Value value)
        {
            var response = await ExecuteAsync(value, _service.CreateAsync);

            return response;
        }
    }
}
