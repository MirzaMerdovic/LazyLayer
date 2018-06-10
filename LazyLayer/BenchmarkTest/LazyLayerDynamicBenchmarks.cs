using BenchmarkDotNet.Attributes;
using LazyLayer.Example.Core;
using LazyLayer.Example.WebApi.Controllers;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace BenchmarkTest
{
    public class LazyLayerDynamicBenchmarks
    {
        private static ValueController _controller;

        [GlobalSetup]
        public void Setup()
        {
            _controller = CreateController(HttpMethod.Get, "http://localhost");
        }

        [Benchmark]
        public async Task RunPut()
        {
            await _controller.Put(new Value());
        }

        [Benchmark]
        public async Task RunGetOne()
        {
            await _controller.Get(2);
        }

        [Benchmark]
        public async Task RunGetNone()
        {
            await _controller.Get(-1);
        }

        [Benchmark]
        public async Task RunGetAll()
        {
            await _controller.Get();
        }

        private ValueController CreateController(HttpMethod method, string url)
        {
            var controller = new ValueController(null);

            controller.ControllerContext =
                new HttpControllerContext(
                    new HttpRequestContext(),
                    new HttpRequestMessage(method, new Uri(url)),
                    new HttpControllerDescriptor(new HttpConfiguration(), "Value", typeof(ValueController)),
                    controller);

            controller.User = Thread.CurrentPrincipal;

            return controller;
        }
    }
}