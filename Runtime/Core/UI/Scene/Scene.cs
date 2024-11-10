using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameFramework
{
    public abstract class Scene : MonoBehaviour, IScene
    {
        public abstract UniTask OnSceneStart();
        public abstract UniTask OnSceneEnd();
    }
}