using MCSABackend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MCSABackend.Controllers.V1.Test
{

    //[ApiController]
    [ApiVersion("1.0")] // กำหนดเวอร์ชัน 1.0
    [Route("api/v{version:apiVersion}/[controller]")]

    public class TestController : ControllerBase
    {
        //private readonly ILogger<TestV1Controller> _logger;

        private readonly ITestService _testService;
       
        public TestController(ITestService testService)
        {
            _testService=testService;
        }

        [HttpGet("CountryList")]
        public IActionResult GetCourntryList()
        {
            var result= _testService.GetCountriesList();
            return StatusCode(StatusCodes.Status200OK, result);
            
        }
        [HttpGet("TestAPI")]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status200OK, "This is version 1.0");
        }
    }
}
