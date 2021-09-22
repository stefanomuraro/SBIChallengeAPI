using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using SBIChallengeAPI.Requests;
using SBIChallengeAPI.Models;

namespace SBIChallengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostById([FromQuery] int id, CancellationToken cancellationToken)
        {
            Salida post = await _mediator.Send(new GetPostById(id), cancellationToken);

            if (post == null)
            {
                var nf = NotFound("El post " + id + " no existe.");
                return nf;
            }

            return Ok(post);
        }
    }
}
