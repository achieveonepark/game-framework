using Cysharp.Threading.Tasks;

namespace GameFramework
{
    public static partial class Core
    {
        public class IAP
        {
            public static UniTask Initialize()
            {
                return UniTask.CompletedTask;
            }
            
            public static UniTask PurchaseAsync()
            {
                return UniTask.CompletedTask;
            }

            public static UniTask GetPendingListAsync()
            {
                return UniTask.CompletedTask;
            }
        }
    }
}