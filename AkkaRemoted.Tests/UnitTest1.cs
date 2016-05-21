using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Akka.Actor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaRemoted.Tests
{
    [TestClass]
    public class UnitTest1
    {

        public class InventoryActor : ReceiveActor
        {
            public InventoryActor()
            {
                ReceiveAny(message =>
                {
                    
                });
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            new TestHelper("http://localhost:8289/").StartWebApiServer(_ =>
            {
                using (var actorSystem1 = ActorSystem.Create("System1"))
                {
                   var a= actorSystem1.ActorOf<InventoryActor>();
                new TestHelper("http://localhost:8288/").StartWebApiServer(helper =>
                {
                    using (var actorSystem2 = ActorSystem.Create("System2"))
                    {
                        var b = actorSystem2.ActorOf<InventoryActor>();

                        var myctorSelection1 =actorSystem1.ActorSelection("akka.tcp://System1@localhost:8289/user/InventoryActor");
                        myctorSelection1.Ask("sdhgfksdgkfj").Wait();

                    }

                });
                }
            });
        }
    }

    public class TestHelper
    {
        private string Endpoint { set; get; }

        public TestHelper(string endpoint )
        {
            if (!string.IsNullOrEmpty(endpoint))
            {
                Client = new HttpClient() { BaseAddress = new Uri(endpoint) };
            }
            Endpoint = endpoint;

        }

        private HttpClient Client { set; get; }

        public async Task<IEnumerable<object>> GetProducts(string action)
        {
            var result = await Client.GetAsync("api/products/" + action);
            result.EnsureSuccessStatusCode();
            var products = result.Content.ReadAsAsync<IEnumerable<object>>().Result;
            return products;
        }

        public void Profile(Action operation)
        {
            var start = DateTime.Now;
            operation();
            var end = DateTime.Now;
            var duration = (end - start).TotalMilliseconds;
            Console.WriteLine("Operation took " + duration + " ms");
         
        }


        public void StartWebApiServer(Action operation)
        {
            StartWebApiServer((helper) => operation());
        }

        public void StartWebApiServer( Action<TestHelper> operation)
        {
            var config = new HttpSelfHostConfiguration(Endpoint);
            config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional });
            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                operation(this);
            }
        }
        public class ProductsController : ApiController
        {
            [HttpGet]
            public async Task<IEnumerable<string>> Retrieve()
            {
                return await Task.FromResult(new List<string>() {"yo!"});
            }
           
        }
}
}
