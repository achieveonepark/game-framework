using Cysharp.Threading.Tasks;
using GameFramework.Manager;

namespace GameFramework
{
    public class SoundManager : IManager
    {
        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
    }
}