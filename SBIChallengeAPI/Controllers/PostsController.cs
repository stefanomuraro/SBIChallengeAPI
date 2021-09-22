using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using AutoMapper;

namespace SBIChallengeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _client = new();

        public PostsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            Salida post = await ObtenerPost(id);

            if(post == null)
            {
                var nf = NotFound("El post " + id + " no existe.");
                return nf;
            }

            return Ok(post);
        }

        private async Task<Salida> ObtenerPost(int id)
        {
            var path = "https://jsonplaceholder.typicode.com/posts/";
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                ServerPost[] posts = JsonConvert.DeserializeObject<ServerPost[]>(json);
                foreach (ServerPost post in posts)
                {
                    if (post.Id == id)
                    {
                        return _mapper.Map<Salida>(post);
                    }
                }
            }
            return null;
        }
    }
}
