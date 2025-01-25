using UnityEngine;

namespace GameFramework
{
    [CreateAssetMenu(fileName = "InitializeSettings", menuName = "GameFramework/InitializeSettings")]
    public partial class InitializeSettings : GScriptableObject
    {
        public bool IsGameLogEnabled;
        public bool IsTimeManagerEnabled;
        public bool IsConfigManagerEnabled;
        public bool IsUIBindManagerEnabled;
    }
}