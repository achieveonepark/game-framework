using System.IO;
using UnityEngine;

namespace GameFramework
{
    /// <summary>
    /// 게임 프레임워크에서 사용하는 ScriptableObject의 기본 클래스입니다.
    /// ScriptableObject는 Unity에서 데이터를 저장하는 에셋(Asset) 파일을 만드는 데 사용됩니다.
    /// 씬에 종속되지 않아 게임 전체에서 공유되는 설정 데이터 저장에 적합합니다.
    /// </summary>
    public abstract class GScriptableObject : ScriptableObject
    {
        // 에디터에서 에셋 파일을 저장할 경로 (Assets 폴더 기준)
        private static readonly string assetPath = $"Assets/Framework/Resources/Settings";
        // 런타임에서 Resources.Load로 불러올 때 사용하는 경로 (Resources 폴더 기준)
        private static readonly string resourcePath = "Settings";

        /// <summary>
        /// Assets 폴더 내에서 지정된 ScriptableObject 타입을 GUID로 조회하고, 없으면 자동으로 생성합니다.
        /// 런타임과 에디터 모두에서 동작합니다.
        /// </summary>
        /// <typeparam name="T">찾거나 생성할 ScriptableObject 타입</typeparam>
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
                // 에디터에서만 실행되는 코드: 파일 시스템에 실제 에셋 파일을 저장합니다
                if (!Directory.Exists(assetPath))
                {
                    // 저장 경로가 없으면 폴더를 새로 만듭니다
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
        /// <summary>
        /// 에디터 전용: 지정한 타입의 ScriptableObject 에셋을 새로 생성하고 저장합니다.
        /// 이미 있더라도 새로 만듭니다. 에디터 도구나 메뉴에서 사용합니다.
        /// </summary>
        /// <typeparam name="T">생성할 ScriptableObject 타입</typeparam>
        /// <returns>새로 생성된 ScriptableObject 인스턴스</returns>
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
        /// 에디터 전용: Project 뷰에서 특정 Asset 파일을 Ping하고 강조 표시합니다.
        /// 에디터 창에서 해당 에셋 파일을 빠르게 찾을 때 유용합니다.
        /// </summary>
        /// <typeparam name="T">강조 표시할 ScriptableObject 타입</typeparam>
        public static void PingAsset<T>() where T : ScriptableObject
        {
            // AssetDatabase를 통해 Project 폴더에서 ScriptableObject 타입의 Asset을 찾음
            string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T).Name}");

            if (guids.Length > 0)
            {
                // 첫 번째 GUID로 Asset 경로를 가져오고, Asset을 로드
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);

                // Project 뷰에서 해당 Asset 핑하기 (에디터에서 파란색으로 깜빡여 위치를 알려줍니다)
                UnityEditor.EditorGUIUtility.PingObject(asset);
                // 해당 에셋을 선택 상태로 만들어 Inspector에 표시되도록 합니다
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
