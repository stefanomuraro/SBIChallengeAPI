using System;
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
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetPostById([FromQuery] int id, CancellationToken cancellationToken)
        {
            Salida post = await _mediator.Send(new GetPostById(id), cancellationToken);
            return post != null ? Ok(post) : NotFound("El post " + id + " no existe.");
        }
    }
}
