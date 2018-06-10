using LazyLayer.Example.Core;
using LazyLayer.Example.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using Xunit;

namespace LazyLayer.Examples.WebApi.Test
{
    public class ValueControllerTests
    {
        private const string Url = "http://localhost";

        [Fact]
        public async Task PostTest()
        {
            var controller = CreateController(HttpMethod.Post, Url);

            var response = await controller.Post(new Value());
            Assert.IsAssignableFrom<CreatedAtRouteNegotiatedContentResult<int>>(response);
        }

        [Fact]
        public async Task GetAllTest()
        {
            var controller = CreateController(HttpMethod.Get, Url);

            var response = await controller.Get();
            Assert.IsAssignableFrom<JsonResult<object>>(response);

            var jsonResult = (JsonResult<object>)response;
            Assert.IsAssignableFrom<IEnumerable<Value>>(jsonResult.Content);
        }

        [Fact]
        public async Task GetTest()
        {
            var controller = CreateController(HttpMethod.Get, Url);

            var response = await controller.Get(2);
            Assert.IsAssignableFrom<JsonResult<object>>(response);

            var jsonResult = (JsonResult<object>)response;
            Assert.IsAssignableFrom<Value>(jsonResult.Content);
        }

        [Fact]
        public async Task NotFoundTest()
        {
            var controller = CreateController(HttpMethod.Get, Url);

            var response = await controller.Get(-1);
            Assert.IsAssignableFrom<NotFoundResult>(response);
        }

        [Fact]
        public async Task UpdateTest()
        {
            var controller = CreateController(HttpMethod.Put, Url);

            var response = await controller.Put(new Value());
            Assert.IsAssignableFrom<OkResult>(response);
        }

        [Fact]
        public async Task DeleteTest()
        {
            var controller = CreateController(HttpMethod.Delete, Url);

            var response = await controller.Delete(1);
            Assert.IsAssignableFrom<OkResult>(response);
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