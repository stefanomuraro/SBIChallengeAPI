using SBIChallengeAPI.Models;
using System.Threading.Tasks;

namespace SBIChallengeAPI.Services
{
    public interface IPostsService
    {
        Task<Salida> GetPostById(int id);
    }
}
