using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Aspects.Services
{
    public interface ITextureDownloaderService
    {
        UniTask<Texture2D> GetTexture(string uri);
    }
}