#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace GameFramework
{
    public partial class InitializeSettings
    {
        [CustomEditor(typeof(InitializeSettings))]
        public class InitializeSettingsEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                InitializeSettings settings = (InitializeSettings)target;

                EditorGUILayout.HelpBox("체크 시 해당 기능을 사용합니다.", MessageType.Info);
                
                EditorGUILayout.LabelField("초기화 설정", EditorStyles.boldLabel);
                settings.IsGameLogEnabled = EditorGUILayout.Toggle("게임 로그", settings.IsGameLogEnabled);
                settings.IsTimeManagerEnabled = EditorGUILayout.Toggle("시간 관리", settings.IsTimeManagerEnabled);
                settings.IsConfigManagerEnabled = EditorGUILayout.Toggle("설정 관리", settings.IsConfigManagerEnabled);
                settings.IsUIBindManagerEnabled = EditorGUILayout.Toggle("UI 바인드 관리", settings.IsUIBindManagerEnabled);

                if (GUI.changed)
                {
                    EditorUtility.SetDirty(settings);
                }
            }
        }
    }
}
#endif