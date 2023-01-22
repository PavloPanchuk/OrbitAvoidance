using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Aspects.Services
{
    public class HttpClientService : IHttpClientService
    {
        public async UniTask<string> Get(string uri)
        {
            var request = UnityWebRequest.Get(uri);
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                Debug.Log(request.error);

            return request.downloadHandler.text;
        }
    }
}