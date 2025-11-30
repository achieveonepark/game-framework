using UnityEngine;

namespace GameFramework
{
    public abstract class TouchableObject : MonoBehaviour
    {
        // 터치됐을 때 실행될 이벤트
        public abstract void OnTouched();
    }
}