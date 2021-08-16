using DevOps.Controllers;
using Microsoft.Extensions.Logging;
using System;
using Xunit;

namespace Project.Tests
{
    public class UnitTest1
    {
        private readonly ILogger<WeatherForecastController> _logger;

        [Fact]
        public void Test1()
        {
            var controller = new WeatherForecastController(_logger);


                       //Act  
                       var data = controller.Get();


                    //Assert  
                       Assert.NotNull(data);
        }
    }
}
