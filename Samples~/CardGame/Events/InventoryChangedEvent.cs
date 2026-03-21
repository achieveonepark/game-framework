using System;

namespace GameFramework.Sample
{
    /// <summary>
    /// 인벤토리 변경 시 발행되는 전역 이벤트입니다.
    /// GlobalEvent 패턴: using 블록 종료 시 자동으로 Fire됩니다.
    ///
    /// 사용 예:
    ///   using (var e = InventoryChangedEvent.Get())
    ///   {
    ///       e.ChangedItemId = item.Id;
    ///       e.IsAdded = true;
    ///   }
    /// </summary>
    public class InventoryChangedEvent : GlobalEvent<InventoryChangedEvent>, IDisposable
    {
        public int ChangedItemId;
        public bool IsAdded;

        protected override void Reset()
        {
            ChangedItemId = 0;
            IsAdded = false;
        }
    }
}
