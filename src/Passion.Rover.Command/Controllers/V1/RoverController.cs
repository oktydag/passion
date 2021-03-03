using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Passion.Rover.Command.Commands;
using Passion.Rover.Command.Domain.Services.Contracts;

namespace Passion.Rover.Command.Controllers.V1
{
    [Route("api/v1/rover")]
    [ApiController]
    public class RoverController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoverController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("healthcheck")]
        [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        // [Authorize(nameof(GetClaimByClaimNumber))]
        public HttpStatusCode GetByTest()
        {
            return HttpStatusCode.OK;
        }

        [HttpPost("takephoto")]
        public async Task<ActionResult<HttpStatusCode>> TakePhotoAsync([FromBody] TakeWhatYouSeeCommand command)
        {
            var takePhotoResponse = await _mediator.Send(command);

            if (takePhotoResponse) return Ok();
            return BadRequest();
        }
        
        [HttpPost("go")]
        public async Task<ActionResult<HttpStatusCode>> TakePhotoAsync([FromBody] GoGivenLocationCommand command)
        {
            var goGivenLocationCommand = await _mediator.Send(command);

            if (goGivenLocationCommand) return Ok();
            return BadRequest();
        }
    }
}