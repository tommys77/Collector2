using Collector2.DataService.Controllers;
using Collector2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Collector2.Tests
{
    [TestClass]
    class SoftwareControllerTest
    {
        [TestMethod]
        public void PostNewSoftware_ShouldReturnCreated()
        {
            //Arrange
            var controller = new SoftwaresController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("https://collectorv2.azurewebsites.net/api/AddNewSoftware?imgId=1")
            };

            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi", routeTemplate: "api/controller/{id}", defaults: new { id = RouteParameter.Optional }
                );
            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "softwares" } });

            //Act
            var software = new Software
            {
                Title = "Another World",
                CategoryId = 1,
                FormatId = 1,
                FormatCount = 2,
                HardwareSpecId = 1,
                YearOfRelease = 1991,
                SoftwareType = "Game"
            };

            //var response = controller.PostSoftware(software, 1);

            //Assert

            Assert.Equals("https://collectorv2.azurewebsites.net/api/AddNewSoftware?imgId=1", response);
            
        }



    }
}
