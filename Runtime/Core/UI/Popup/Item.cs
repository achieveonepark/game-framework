using UnityEngine;

namespace GameFramework
{
    public class Item : MonoBehaviour
    {
        public virtual void Refresh(object data)
        {
            gameObject.SetActive(true);
        }
    }
}