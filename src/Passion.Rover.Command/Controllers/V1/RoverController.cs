using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Passion.Rover.Command.Controllers.V1
{
    [Route("api/v1/rover")]
    [ApiController]
    public class RoverController : ControllerBase
    {
        public RoverController()
        {
                
        }
        
        [HttpGet("healthcheck")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        // [Authorize(nameof(GetClaimByClaimNumber))]
        public HttpStatusCode GetByTest()
        {
            return  HttpStatusCode.OK;
        }
    }
}