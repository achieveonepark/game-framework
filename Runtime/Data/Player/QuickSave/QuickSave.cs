#if USE_QUICK_SAVE
using Achieve.QuickSave;

namespace GameFramework.Data.Player
{
    internal class QuickSave
    {
        private readonly QuickSave<Player> _quickSave = new QuickSave<Player>.Builder()
            .UseEncryption("348GJ32ndh@R*gh#")
            .UseVersion(0)
            .Build();

        internal void Save(Player player)
        {
            _quickSave.SaveData(player);
        }

        internal Player Load()
        {
            return _quickSave.LoadData();
        }
    }
}
#endif