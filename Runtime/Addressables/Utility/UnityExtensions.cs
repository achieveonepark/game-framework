#if USE_ADDRESSABLE
using UnityEngine;

namespace GameFramework.Addressable
{
    public static class UnityExtensions
    {
        /// <summary>
        /// GameObject를 메모리에서 해제합니다.
        /// isDestroy == true : 메모리를 해제하면서 삭제
        /// 삭제는 GameObject.Destroy() 호출 됨
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="isDestroy"></param>
        public static void Release(this GameObject obj, bool isDestroy = false)
        {
            GAddressable.Release(obj, isDestroy);
        }
    }
}
#endif