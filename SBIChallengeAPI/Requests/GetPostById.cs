using System;
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
        private readonly HttpClient _client;

        public GetPostByIdHandler(IMapper mapper, HttpClient client)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<Salida> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var path = "https://jsonplaceholder.typicode.com/posts/";
            HttpResponseMessage response = await _client.GetAsync(path, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync(cancellationToken);
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
