using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SBIChallengeAPI.Models;
using SBIChallengeAPI.Services;

namespace SBIChallengeAPI.Requests
{
    public class GetPostById : IRequest<Salida>
    {
        public int Id { get; set; }

        public GetPostById(int id)
        {
            Id = id;
        }
    }

    public class GetPostByIdHandler : IRequestHandler<GetPostById, Salida>
    {
        private readonly IPostsService _postsService;

        public GetPostByIdHandler(IPostsService postsService)
        {
            _postsService = postsService ?? throw new ArgumentNullException(nameof(postsService));
        }

        public async Task<Salida> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            return await _postsService.GetPostById(request.Id);
        }
    }
}
