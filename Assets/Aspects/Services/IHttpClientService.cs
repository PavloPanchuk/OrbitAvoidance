using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Aspects.Services
{
    public interface IHttpClientService
    {
        UniTask<string> Get(string uri);
    }
}