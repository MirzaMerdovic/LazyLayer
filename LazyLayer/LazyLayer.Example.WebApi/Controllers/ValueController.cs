using LazyLayer.Core.Providers;
using LazyLayer.Example.Core;
using LazyLayer.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LazyLayer.Example.WebApi.Controllers
{
    [RoutePrefix("value")]
    public class ValueController : LazyController
    {
        private readonly IValueService _service = new ValueService();

        public ValueController(ILogProvider logger) : base(null, logger)
        {
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Post(Value value)
        {
            var response = await ExecuteAsync(value, _service.CreateAsync);

            return response;
        }

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var response = await ExecuteAsync(_service.GetAsync);

            return response;
        }

        [HttpGet, Route("{id:int}", Name = "GetValueById")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var response = await ExecuteAsync(id, _service.GetAsync);

            return response;
        }

        [HttpPut, Route("")]
        public async Task<IHttpActionResult> Put(Value value)
        {
            var response = await ExecuteAsync(value, _service.Update);

            return response;
        }

        [HttpDelete, Route("id:int")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var response = await ExecuteAsync(id, _service.Delete);

            return response;
        }
    }
}