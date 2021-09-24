using AutoMapper;
using Newtonsoft.Json;
using SBIChallengeAPI.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SBIChallengeAPI.Services
{
    public class PostsService : IPostsService
    {

        private readonly IMapper _mapper;
        private readonly HttpClient _client;

        public PostsService(IMapper mapper, HttpClient client)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<Salida> GetPostById(int id)
        {
            ServerPost[] posts = await GetDataFromExternalApi();
            ServerPost post = posts.FirstOrDefault(p => p.Id == id);
            return _mapper.Map<Salida>(post);
        }

        private async Task<ServerPost[]> GetDataFromExternalApi()
        {
            var path = "https://jsonplaceholder.typicode.com/posts/";
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServerPost[]>(json);
            }
            var errorMessage = $"Error API externa. Código: {(int)response.StatusCode} {response.ReasonPhrase}.";
            throw new Exception(errorMessage);
        }
    }
}
