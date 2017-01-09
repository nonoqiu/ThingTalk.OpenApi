using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThingTalk.OpenApi;
using ThingTalk.OpenApi.Controllers;

namespace ThingTalk.OpenApi.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest : ApiTestBase
    {
        public override string GetBaseAddress()
        {
            return "http://localhost:33203/";
        }

        //[TestMethod]
        //public void Should_get_order_successfully()
        //{
        //    var result = InvokeGetRequest<Order>("api/order");

        //    result.Name.Should().Be("name");
        //    result.Descriptions.Should().Be("descriptions");
        //    result.Id.Should().Be(1);
        //}

        //[TestMethod]
        //public void Should_post_order_successfully()
        //{
        //    var newOrder = new Order() { Name = "newOrder", Id = 100, Descriptions = "new-order-description" };

        //    var result = InvokePostRequest<Order, Order>("api/order", newOrder);

        //    result.Name.Should().Be("newOrder");
        //    result.Id.Should().Be(100);
        //    result.Descriptions.Should().Be("new-order-description");
        //}

        [TestMethod]
        public void Get()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            IEnumerable<string> result = controller.Get();

            // 断言
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            string result = controller.Get(5);

            // 断言
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Post("value");

            // 断言
        }

        [TestMethod]
        public void Put()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Put(5, "value");

            // 断言
        }

        [TestMethod]
        public void Delete()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            controller.Delete(5);

            // 断言
        }
    }
}
