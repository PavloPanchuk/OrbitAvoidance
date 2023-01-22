using Cysharp.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Aspects.Services
{
    public class JokeManiaService : IJokeManiaService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ITextureDownloaderService _textureDownloaderService;

        public JokeManiaService(
            IHttpClientService httpClientService,
            ITextureDownloaderService textureDownloaderService)
        {
            _httpClientService = httpClientService;
            _textureDownloaderService = textureDownloaderService;
        }

        public async UniTask<JokeData> GetJoke()
        {
            var responseBody = await _httpClientService.Get("https://geek-jokes.sameerkumar.website/api?format=json");
            JObject parsedJokeText = JObject.Parse(responseBody);
            string jokeText = (string) parsedJokeText.GetValue("joke");

            var imageSeed = jokeText;
            var jokeImageUrl = $"https://avatars.dicebear.com/api/adventurer-neutral/:{imageSeed}.png";
            var jokeImageTexture = await _textureDownloaderService.GetTexture(jokeImageUrl);

            return new JokeData
            {
                JokeText = jokeText,
                JokeImageTexture = jokeImageTexture
            };
        }
    }
}