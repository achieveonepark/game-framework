using System.IO;
using UnityEngine;

namespace GameFramework
{
    public abstract class GScriptableObject : ScriptableObject
    {
        private static readonly string assetPath = $"Assets/Framework/Resources/Settings";
        private static readonly string resourcePath = "Settings";
        /// <summary>
        /// Assets 폴더 내에서 지정된 ScriptableObject 타입을 GUID로 조회하고, 없으면 자동으로 생성합니다.
        /// </summary>
        /// <typeparam name="T">ScriptableObject 타입</typeparam>
        /// <returns>조회되거나 생성된 ScriptableObject 인스턴스</returns>
        public static T GetOrAdd<T>() where T : ScriptableObject
        {
            // Resources 폴더에서 ScriptableObject를 검색
            T asset = Resources.Load<T>($"{resourcePath}/{typeof(T).Name}");

            if (asset == null)
            {
                // Asset이 없으면 새로 생성
                asset = CreateInstance<T>();

#if UNITY_EDITOR
                if (!Directory.Exists(assetPath))
                {
                    Directory.CreateDirectory(assetPath);
                }

                string fullPath = Path.Combine(assetPath, typeof(T).Name + ".asset");
                UnityEditor.AssetDatabase.CreateAsset(asset, fullPath);
                UnityEditor.AssetDatabase.SaveAssets();
                
                Debug.Log($"New {typeof(T).Name} created and saved at {fullPath}");
#endif
            }
            else
            {
                Debug.Log($"{typeof(T).Name} found in Resources and loaded.");
            }

            return asset;
        }
           
#if UNITY_EDITOR
        public static T Add<T>() where T : ScriptableObject
        {
            T asset = CreateInstance<T>();

            // ScriptableObject를 저장할 경로 설정
            if (!Directory.Exists(assetPath))
            {
                Directory.CreateDirectory(assetPath);
            }

            string fullPath = Path.Combine(assetPath, typeof(T).Name + ".asset");
            UnityEditor.AssetDatabase.CreateAsset(asset, fullPath);
            UnityEditor.AssetDatabase.SaveAssets();

            Debug.Log($"New {typeof(T).Name} created at {fullPath}");
            return asset;
        }
        
        /// <summary>
        /// Project 뷰에서 특정 Asset 파일을 Ping하고 강조 표시합니다.
        /// </summary>
        /// <typeparam name="T">ScriptableObject 타입</typeparam>
        public static void PingAsset<T>() where T : ScriptableObject
        {
            // AssetDatabase를 통해 Project 폴더에서 ScriptableObject 타입의 Asset을 찾음
            string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            if (guids.Length > 0)
            {
                // 첫 번째 GUID로 Asset 경로를 가져오고, Asset을 로드
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);

                // Project 뷰에서 해당 Asset 핑하기
                UnityEditor.EditorGUIUtility.PingObject(asset);
                UnityEditor.Selection.activeObject = asset;
            }
            else
            {
                Debug.LogWarning($"{typeof(T).Name} 타입의 Asset을 Project 뷰에서 찾을 수 없습니다.");
            }
        }
#endif
    }
}