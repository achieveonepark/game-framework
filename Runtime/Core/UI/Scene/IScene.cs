using Cysharp.Threading.Tasks;

namespace GameFramework
{
    public interface IScene
    { 
        UniTask OnSceneStart();
        UniTask OnSceneEnd();
    }
}