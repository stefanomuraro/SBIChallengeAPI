using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using SBIChallengeAPI.Models;

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
        private readonly IMapper _mapper;
        private readonly HttpClient _client = new();

        public GetPostByIdHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Salida> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var path = "https://jsonplaceholder.typicode.com/posts/";
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                ServerPost[] posts = JsonConvert.DeserializeObject<ServerPost[]>(json);
                foreach (ServerPost post in posts)
                {
                    if (post.Id == request.Id)
                    {
                        return _mapper.Map<Salida>(post);
                    }
                }
            }
            return null;
        }
    }
}
