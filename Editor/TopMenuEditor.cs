using GameFramework;
using UnityEditor;

namespace Editor
{
    public static class TopMenuEditor
    {
        [MenuItem("GameFramework/Settings/Create InitializeSettings")]
        public static void CreateInitializeSettings()
        {
            var asset = InitializeSettings.Add<InitializeSettings>();
            InitializeSettings.PingAsset<InitializeSettings>();
        }
        
        
    }
}