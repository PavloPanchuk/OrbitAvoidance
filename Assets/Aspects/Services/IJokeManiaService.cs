using Cysharp.Threading.Tasks;

namespace Aspects.Services
{
    public interface IJokeManiaService
    {
        UniTask<JokeData> GetJoke();
    }
}