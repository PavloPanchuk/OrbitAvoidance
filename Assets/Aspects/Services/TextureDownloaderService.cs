using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Aspects.Services
{
    public class TextureDownloaderService : ITextureDownloaderService
    {
        public async UniTask<Texture2D> GetTexture(string uri)
        {
            var request = UnityWebRequestTexture.GetTexture(uri);
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                Debug.Log(request.error);
            
            Texture2D texture2D = ((DownloadHandlerTexture) request.downloadHandler).texture;

            return texture2D;
        }
    }
}