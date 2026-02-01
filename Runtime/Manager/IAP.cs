using Cysharp.Threading.Tasks;
using GameFramework.Manager;

namespace GameFramework
{
    public class IAPManager : IManager
    {
        public UniTask Initialize()
        {
            // TODO: Add actual initialization logic
            return UniTask.CompletedTask;
        }

        public UniTask PurchaseAsync()
        {
            // TODO: Add actual purchase logic
            return UniTask.CompletedTask;
        }

        public UniTask GetPendingListAsync()
        {
            // TODO: Add actual pending list logic
            return UniTask.CompletedTask;
        }
    }
}