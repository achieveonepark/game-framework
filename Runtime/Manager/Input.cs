using Cysharp.Threading.Tasks;
using GameFramework.Manager;

namespace GameFramework
{
    public class InputManager : IManager
    {
        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }
    }
}